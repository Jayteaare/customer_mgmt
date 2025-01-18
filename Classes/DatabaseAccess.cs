using C969_WGU_TallisJordan.Resources;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace C969_WGU_TallisJordan.Classes
{
    public class DatabaseAccess
    {
        private readonly string server = "127.0.0.1";
        private readonly string database = "client_schedule";
        private readonly string userName;
        private readonly string password;

        public DatabaseAccess(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        private string GetConnectionString()
        {
            return $"Server={server}; database={database}; uid=sqlUser; pwd=Passw0rd!";
        }

        public bool TestConnection()
        {
            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"{resources.GenericSQLError}: {ex.Number}, {ex.Message}");
                return false;
            }
        }

        public int GetUserId(string userName)
        {
            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    var query = "SELECT userId FROM user WHERE userName = @UserName;";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        var result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"{resources.ErrorOccured}: {ex.Message}");
                return -1;
            }
        }

        public bool IsLoginValid(string userName, string password)
        {
            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    var query = "SELECT COUNT(*) FROM user WHERE userName = @UserName AND password = @Password;";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@Password", password);

                        var result = Convert.ToInt32(cmd.ExecuteScalar());
                        return result > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"{resources.LoginError}: {ex.Message}");
                return false;
            }
        }

        public (bool IsAppointmentSoon, string CustomerName) GetUpcomingAppointmentInfo(int userId)
        {
            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    var query = @"
                                SELECT a.userId, a.start, c.customerName
                                FROM appointment a
                                JOIN customer c ON a.customerId = c.customerId
                                WHERE a.start > NOW() AND a.start <= DATE_ADD(NOW(), INTERVAL 1 DAY);";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int fetchedUserId = reader.GetInt32("userId");
                                if (fetchedUserId == userId)
                                {
                                    DateTime estStart = DateTime.SpecifyKind(reader.GetDateTime("start"), DateTimeKind.Unspecified);
                                    DateTime localStart = TimezoneHelper.ConvertEasternToLocal(estStart);
                                    if (localStart <= DateTime.Now.AddMinutes(15))
                                    {
                                        var customerName = reader.GetString("customerName");
                                        return (true, customerName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{resources.ApptFetchError}: {ex.Message}");
            }

            return (false, null);
        }

        public DataTable GetAppointmentsByMonth(int year, int month)
        {
            DataTable appointments = new DataTable();
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string sql = @"
                                 SELECT c.CustomerName, a.Title, a.Description, a.Start, a.End
                                 FROM appointment a
                                 JOIN customer c ON a.CustomerId = c.CustomerId
                                 WHERE YEAR(a.Start) = @Year AND MONTH(a.Start) = @Month";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Month", month);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(appointments);
                        foreach (DataRow row in appointments.Rows)
                        {
                            row["Start"] = TimezoneHelper.ConvertEasternToLocal(Convert.ToDateTime(row["Start"]));
                            row["End"] = TimezoneHelper.ConvertEasternToLocal(Convert.ToDateTime(row["End"]));
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"{resources.DatabaseError}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{resources.GeneralError}: {ex.Message}");
                }
            }
            return appointments;
        }

        public DataTable GetAllAppointments()
        {
            DataTable appointments = new DataTable();
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string sql = @"
                           SELECT a.appointmentId, c.CustomerName, a.Title, a.Description, a.Start, a.End
                           FROM appointment a
                           JOIN customer c ON a.CustomerId = c.CustomerId";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(appointments);
                        foreach (DataRow row in appointments.Rows)
                        {
                            row["Start"] = TimezoneHelper.ConvertEasternToLocal(Convert.ToDateTime(row["Start"]));
                            row["End"] = TimezoneHelper.ConvertEasternToLocal(Convert.ToDateTime(row["End"]));
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"{resources.ApptFetchError}: {ex.Message}");
                }
            }
            return appointments;
        }

        public class AddCustomerResult
        {
            public bool Success { get; set; }
            public bool CountryNotFound { get; set; }
            public bool CityNotFound { get; set; }
            public bool CustomerAlreadyExists { get; set; }
            public string CountryName { get; set; }
            public string CityName { get; set; }
        }

        public AddCustomerResult AddCustomer(string customerName, string address, string postalCode, string cityName, string countryName, string phone, bool isActive)
        {
            var result = new AddCustomerResult();

            if (CustomerExists(customerName))
            {
                result.CustomerAlreadyExists = true;
                return result;
            }

            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    string countryQuery = "SELECT countryId FROM country WHERE country = @CountryName;";
                    MySqlCommand countryCmd = new MySqlCommand(countryQuery, conn);
                    countryCmd.Parameters.AddWithValue("@CountryName", countryName);
                    object countryIdResult = countryCmd.ExecuteScalar();
                    if (countryIdResult == null)
                    {
                        result.CountryNotFound = true;
                        result.CountryName = countryName;
                        return result;
                    }
                    int countryId = Convert.ToInt32(countryIdResult);

                    string cityQuery = "SELECT cityId FROM city WHERE city = @CityName AND countryId = @CountryId;";
                    MySqlCommand cityCmd = new MySqlCommand(cityQuery, conn);
                    cityCmd.Parameters.AddWithValue("@CityName", cityName);
                    cityCmd.Parameters.AddWithValue("@CountryId", countryId);
                    object cityIdResult = cityCmd.ExecuteScalar();
                    if (cityIdResult == null)
                    {
                        result.CityNotFound = true;
                        result.CityName = cityName;
                        return result;
                    }
                    int cityId = Convert.ToInt32(cityIdResult);

                    string insertAddress = @"
                                           INSERT INTO address (address, address2, postalCode, cityId, phone, createDate, createdBy, lastUpdate, lastUpdateBy)
                                           VALUES (@Address, '', @PostalCode, @CityId, @Phone, NOW(), @Username, NOW(), @Username);";
                    MySqlCommand cmd = new MySqlCommand(insertAddress, conn);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                    cmd.Parameters.AddWithValue("@CityId", cityId);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Username", GlobalVariables.CurrentUser);
                    cmd.ExecuteNonQuery();

                    string insertCustomer = @"
                                            INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy)
                                            VALUES (@CustomerName, LAST_INSERT_ID(), @Active, NOW(), @Username, NOW(), @Username);";
                    cmd = new MySqlCommand(insertCustomer, conn);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@Active", isActive);
                    cmd.Parameters.AddWithValue("@Username", GlobalVariables.CurrentUser);
                    cmd.ExecuteNonQuery();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{resources.FailCustAdd}: {ex.Message}");
                result.Success = false;
            }
            return result;
        }

        public bool HasOverlappingCustomerAppointments(int customerId, DateTime startTime, DateTime endTime, int? excludeAppointmentId = null)
        {
            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    DateTime estStartTime = TimezoneHelper.ConvertLocalToEastern(startTime);
                    DateTime estEndTime = TimezoneHelper.ConvertLocalToEastern(endTime);
                    string query = @"
                           SELECT COUNT(*) FROM appointment
                           WHERE customerId = @CustomerId AND 
                           (start < @EndTime AND end > @StartTime)
                           AND (@ExcludeAppointmentId IS NULL OR appointmentId <> @ExcludeAppointmentId);";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@StartTime", estStartTime);
                    cmd.Parameters.AddWithValue("@EndTime", estEndTime);
                    cmd.Parameters.AddWithValue("@ExcludeAppointmentId", excludeAppointmentId ?? (object)DBNull.Value);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public bool AddCustomer(string customerName)
        {
            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string insertCustomer = @"
                                            INSERT INTO customer (customerName, active, createDate, createdBy, lastUpdate, lastUpdateBy)
                                            VALUES (@CustomerName, 1, NOW(), @Username, NOW(), @Username);";
                    MySqlCommand cmd = new MySqlCommand(insertCustomer, conn);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@Username", GlobalVariables.CurrentUser);

                    int result = cmd.ExecuteNonQuery();
                    return result == 1;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"{resources.SQLError}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{resources.GeneralError}: {ex.Message}");
                return false;
            }
        }

        public int GetCityId(string cityName, int countryId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT cityId FROM city WHERE city = @CityName AND countryId = @CountryId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CityName", cityName);
                cmd.Parameters.AddWithValue("@CountryId", countryId);

                object result = cmd.ExecuteScalar();
                if (result != null)
                    return Convert.ToInt32(result);
                else
                    return -1;
            }
        }

        public int AddCityAndGetId(string cityName, int countryId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string insertQuery = "INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@CityName, @CountryId, NOW(), 'admin', NOW(), 'admin');";
                MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@CityName", cityName);
                cmd.Parameters.AddWithValue("@CountryId", countryId);
                cmd.ExecuteNonQuery();

                return (int)cmd.LastInsertedId;
            }
        }

        public int GetCountryId(string countryName)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT countryId FROM country WHERE country = @CountryName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CountryName", countryName);

                object result = cmd.ExecuteScalar();
                if (result != null)
                    return Convert.ToInt32(result);
                else
                    return -1;
            }
        }

        public int AddCountryAndGetId(string countryName)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string insertCountry = "INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@CountryName, NOW(), @Username, NOW(), @Username);";
                MySqlCommand cmd = new MySqlCommand(insertCountry, conn);
                cmd.Parameters.AddWithValue("@CountryName", countryName);
                cmd.Parameters.AddWithValue("@Username", GlobalVariables.CurrentUser);
                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }
        }

        public bool AddCity(string cityName, int countryId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string insertCity = "INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@CityName, @CountryId, NOW(), @Username, NOW(), @Username);";
                MySqlCommand cmd = new MySqlCommand(insertCity, conn);
                cmd.Parameters.AddWithValue("@CityName", cityName);
                cmd.Parameters.AddWithValue("@CountryId", countryId);
                cmd.Parameters.AddWithValue("@Username", GlobalVariables.CurrentUser);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public DataTable GetAllCustomers()
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT customerId, customerName FROM customer ORDER BY customerName;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataRow GetCustomerByName(string customerName)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = @"
                               SELECT 
                                   c.customerId, c.customerName, c.active, 
                                   a.address, a.address2, a.postalCode, a.phone,
                                   ci.city, co.country
                               FROM customer c
                               INNER JOIN address a ON c.addressId = a.addressId
                               INNER JOIN city ci ON a.cityId = ci.cityId
                               INNER JOIN country co ON ci.countryId = co.countryId
                               WHERE c.customerName = @CustomerName";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "DELETE FROM customer WHERE customerId = @CustomerId;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool UpdateCustomer(int customerId, string name, string address, string postalCode, string phone, bool isActive)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string updateQuery = @"
                                     UPDATE customer SET 
                                         customerName = @Name, 
                                         active = @IsActive
                                     WHERE customerId = @CustomerId;
            
                                     UPDATE address SET 
                                         address = @Address, 
                                         postalCode = @PostalCode, 
                                         phone = @Phone
                                     WHERE addressId = (SELECT addressId FROM customer WHERE customerId = @CustomerId);";

                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                cmd.Parameters.AddWithValue("@Phone", phone);

                int affectedRows = cmd.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public bool CustomerHasAppointments(int customerId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM appointment WHERE customerId = @CustomerId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public void DeleteAllAppointmentsForCustomer(int customerId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "DELETE FROM appointment WHERE customerId = @CustomerId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.ExecuteNonQuery();
            }
        }

        public int GetCustomerIdByName(string customerName)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT customerId FROM customer WHERE customerName = @CustomerName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        public void DeleteCustomerByName(string customerName)
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            int customerId = dbAccess.GetCustomerIdByName(customerName);

            if (customerId == -1)
            {
                MessageBox.Show($"{resources.CustomerNotFound}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"{resources.DeleteCustomer}", $"{resources.ConfirmDelete}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dbAccess.CustomerHasAppointments(customerId))
                {
                    if (MessageBox.Show($"{resources.DeleteCustAppt}", $"{resources.ConfirmDelete}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbAccess.DeleteAllAppointmentsForCustomer(customerId);
                    }
                    else
                    {
                        return;
                    }
                }

                if (dbAccess.DeleteCustomer(customerId))
                {
                    MessageBox.Show($"{resources.CustDelSuccess}", $"{resources.Deleted}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"{resources.FailedCustDel}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public DataTable GetAppointmentTypesByMonth()
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = @"
                               SELECT 
                                   YEAR(start) AS Year, 
                                   MONTH(start) AS Month, 
                                   type, 
                                   COUNT(*) AS Count
                               FROM appointment
                               GROUP BY YEAR(start), MONTH(start), type
                               ORDER BY YEAR(start), MONTH(start), type;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetScheduleForEachUser(string userName)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = @"
                               SELECT 
                                   u.userName,
                                   a.type,
                                   a.title,
                                   a.start,
                                   a.end,
                                   c.customerName,
                                   a.description
                               FROM appointment a
                               JOIN user u ON a.userId = u.userId
                               JOIN customer c ON a.customerId = c.customerId
                               WHERE u.userName LIKE @UserName
                               ORDER BY a.start;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserName", $"%{userName}%");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool UserExists(string userName)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM user WHERE userName LIKE @UserName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserName", $"%{userName}%");

                int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                return userCount > 0;
            }
        }

        public bool CustomerExists(string customerName)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM customer WHERE customerName = @CustomerName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);

                int customerCount = Convert.ToInt32(cmd.ExecuteScalar());
                return customerCount > 0;
            }
        }

        public DataTable GetCustomerEngagementReport()
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = @"
                               SELECT 
                                   c.customerName,
                                   COUNT(a.appointmentId) AS NumberOfAppointments,
                                   GROUP_CONCAT(DISTINCT a.type) AS AppointmentTypes,
                                   MAX(a.start) AS LastAppointmentDate
                               FROM customer c
                               JOIN appointment a ON c.customerId = a.customerId
                               GROUP BY c.customerId
                               ORDER BY NumberOfAppointments DESC;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool AddAppointment(int customerId, int userId, string type, string title, string description, DateTime start, DateTime end)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                DateTime estStart = TimezoneHelper.ConvertLocalToEastern(start);
                DateTime estEnd = TimezoneHelper.ConvertLocalToEastern(end);
                string query = @"
                               INSERT INTO appointment (customerId, userId, type, title, description, location, contact, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy)
                               VALUES (@CustomerId, @UserId, @Type, @Title, @Description, '', '', '', @Start, @End, NOW(), @CreatedBy, NOW(), @LastUpdatedBy);";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Start", estStart);
                    cmd.Parameters.AddWithValue("@End", estEnd);
                    cmd.Parameters.AddWithValue("@CreatedBy", GlobalVariables.CurrentUser);
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", GlobalVariables.CurrentUser);

                    int result = cmd.ExecuteNonQuery();
                    return result == 1;
                }
            }
        }

        public bool UpdateAppointment(int appointmentId, string title, string description, DateTime start, DateTime end, string type, string location, string contact, string url, int userId)
        {
            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    DateTime estStart = TimezoneHelper.ConvertLocalToEastern(start);
                    DateTime estEnd = TimezoneHelper.ConvertLocalToEastern(end);
                    string query = @"
                                   UPDATE appointment
                                   SET
                                       title = @Title,
                                       description = @Description,
                                       start = @Start,
                                       end = @End,
                                       type = @Type,
                                       location = @Location,
                                       contact = @Contact,
                                       url = @Url,
                                       lastUpdate = NOW(),
                                       lastUpdateBy = @LastUpdatedBy,
                                       userId = @UserId
                                   WHERE appointmentId = @AppointmentId;";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Start", estStart);
                    cmd.Parameters.AddWithValue("@End", estEnd);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    cmd.Parameters.AddWithValue("@Url", url);
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", GlobalVariables.CurrentUser);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{resources.FailUpdateAppt}: {ex.Message}");
                return false;
            }
        }

        public DataTable GetAllUsers()
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT userId, userName FROM user ORDER BY userName;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public int GetUserIdByName(string userName)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT userId FROM user WHERE userName = @UserName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserName", userName);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        public bool HasOverlappingAppointments(int consultantId, DateTime startTime, DateTime endTime, int? appointmentId = null)
        {
            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    DateTime estStartTime = TimezoneHelper.ConvertLocalToEastern(startTime);
                    DateTime estEndTime = TimezoneHelper.ConvertLocalToEastern(endTime);
                    string excludeAppointment = appointmentId.HasValue ? " AND appointmentId != @AppointmentId" : "";
                    string query = $@"
                            SELECT COUNT(*) FROM appointment
                            WHERE userId = @ConsultantId 
                            AND (start < @EndTime AND end > @StartTime){excludeAppointment};";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ConsultantId", consultantId);
                    cmd.Parameters.AddWithValue("@StartTime", estStartTime);
                    cmd.Parameters.AddWithValue("@EndTime", estEndTime);
                    if (appointmentId.HasValue)
                        cmd.Parameters.AddWithValue("@AppointmentId", appointmentId.Value);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public DataRow GetAppointmentById(int appointmentId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = @"
                               SELECT a.appointmentId, a.title, a.description, a.start, a.end, 
                                      a.type, a.location, a.contact, a.url, 
                                      c.customerName, u.userName AS consultantName, a.userId
                               FROM appointment a
                               JOIN customer c ON a.customerId = c.customerId
                               JOIN user u ON a.userId = u.userId
                               WHERE a.appointmentId = @AppointmentId;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        DateTime estStart = DateTime.SpecifyKind((DateTime)row["start"], DateTimeKind.Unspecified);
                        DateTime estEnd = DateTime.SpecifyKind((DateTime)row["end"], DateTimeKind.Unspecified);
                        TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                        row["start"] = TimeZoneInfo.ConvertTime(estStart, easternZone, TimeZoneInfo.Local);
                        row["end"] = TimeZoneInfo.ConvertTime(estEnd, easternZone, TimeZoneInfo.Local);
                        return row;
                    }
                    else
                        return null;
                }
            }
        }

        public bool DeleteAppointment(int appointmentId)
        {
            using (var conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    string query = "DELETE FROM appointment WHERE appointmentId = @AppointmentId;";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    cmd.Transaction = transaction;

                    int result = cmd.ExecuteNonQuery();
                    transaction.Commit();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"{resources.FailDelAppt}: {ex.Message}");
                    return false;
                }
            }
        }
    }
}