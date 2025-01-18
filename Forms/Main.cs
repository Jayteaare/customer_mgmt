using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using C969_WGU_TallisJordan.Classes;
using C969_WGU_TallisJordan.Resources;

namespace C969_WGU_TallisJordan.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.Activated += Main_Activated;
            apptDel.Click -= apptDel_Click;
            apptDel.Click += apptDel_Click;
            UpdateUIWithLocalizedText();
            InitializeCombo();
            UpdateAppointmentGridForAll();
            apptGrid.CellClick += new DataGridViewCellEventHandler(apptGrid_CellClick);
            apptGrid.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(apptGrid_DataBindingComplete);
        }

        private void UpdateUIWithLocalizedText()
        {
            calLabel.Text = resources.CalendarLabel;
            reportLabel.Text = resources.ReportLabel;
            custLabel.Text = resources.CustomerLabel;
            custAdd.Text = resources.Add;
            custMod.Text = resources.Modify;
            custDel.Text = resources.Delete;
            apptBtn.Text = resources.AppointmentReport;
            custBtn.Text = resources.CustomerReport;
            schBtn.Text = resources.ScheduleReport;
            apptAdd.Text = resources.Add;
            apptMod.Text = resources.Modify;
            apptDel.Text = resources.Delete;
            calBtn.Text = resources.CalendarButton;
            this.Text = resources.MainFormTitle;
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            RefreshAppointmentsGrid();
        }

        public void RefreshAppointmentsGrid()
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataTable appointments = dbAccess.GetAllAppointments();
            apptGrid.DataSource = appointments;
            if (apptGrid.Columns["appointmentId"] != null)
                apptGrid.Columns["appointmentId"].Visible = false;
            apptGrid.Refresh();
            apptGrid.ClearSelection();
        }

        private void apptGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (apptGrid.Columns.Contains("appointmentId"))
            {
                apptGrid.Columns["appointmentId"].Visible = false;
            }
        }

        private void InitializeCombo()
        {
            PopulateMonths();
            comboList.SelectedIndexChanged += ComboList_SelectedIndexChanged;
        }

        private void PopulateMonths()
        {
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                .Where(m => !string.IsNullOrEmpty(m))
                .Select((monthName, index) => new { MonthName = monthName, MonthNumber = index + 1 })
                .ToList();

            months.Insert(0, new { MonthName = resources.CalendarView, MonthNumber = 1 });
            months.Insert(0, new { MonthName = resources.AllMonths, MonthNumber = 0 });
            comboList.DataSource = months;
            comboList.DisplayMember = "MonthName";
            comboList.ValueMember = "MonthNumber";
            comboList.SelectedIndex = 0;
        }

        private void ComboList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboList.SelectedValue is int monthNumber)
            {
                if (monthNumber == 0)
                {
                    UpdateAppointmentGridForAll();
                }
                else if (monthNumber == 1)
                {
                    apptGrid.DataSource = null;
                }
                else
                {
                    DateTime selectedMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                    UpdateAppointmentGridMonth(selectedMonth);
                }
            }
        }

        private void UpdateAppointmentGridForAll()
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataTable appointments = dbAccess.GetAllAppointments();
            apptGrid.DataSource = appointments;
            apptGrid.Refresh();
        }

        private void UpdateAppointmentGridMonth(DateTime monthStart)
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataTable appointments = dbAccess.GetAppointmentsByMonth(monthStart.Year, monthStart.Month);
            apptGrid.DataSource = appointments;
            apptGrid.Refresh();
        }

        private void UpdateAppointmentGrid(DateTime monthStart)
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataTable appointments = dbAccess.GetAppointmentsByMonth(monthStart.Year, monthStart.Month);
            apptGrid.DataSource = appointments;
            apptGrid.Refresh();
        }

        private void calBtn_Click(object sender, EventArgs e)
        {
            using (Calendar calendarForm = new Calendar())
            {
                if (calendarForm.ShowDialog() == DialogResult.OK)
                {
                    DateTime selectedDate = calendarForm.SelectedDate;
                    UpdateAppointmentGrid(selectedDate);
                    comboList.SelectedIndex = 1;
                }
            }
        }

        private void custAdd_Click(object sender, EventArgs e)
        {
            using (AddCustomer addCustForm = new AddCustomer())
            {
                var result = addCustForm.ShowDialog();
            }
        }

        private void custMod_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox($"{resources.EnterCustNameMod}:", $"{resources.ModCust}", "");
            if (input == "")
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show($"{resources.FillCustName}", $"{resources.InputError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataRow customerRow = dbAccess.GetCustomerByName(input);
            if (customerRow != null)
            {
                ModifyCustomer modifyCustomerForm = new ModifyCustomer(customerRow);
                modifyCustomerForm.ShowDialog();
            }
            else
            {
                MessageBox.Show($"{resources.NoCustFound}", $"{resources.CustomerNotFound}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void custDel_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox($"{resources.EnterCustNameDel}:", $"{resources.DeleteCustomer}", "");
            if (input == "")
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show($"{resources.FillCustName}", $"{resources.InputError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            dbAccess.DeleteCustomerByName(input);
        }

        private void LoadAppointmentTypesReport()
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataTable appointmentData = dbAccess.GetAppointmentTypesByMonth();

            var reportData = appointmentData.AsEnumerable()
                .Select(row => new
                {
                    Year = Convert.ToInt32(row["Year"]),
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(row["Month"])),
                    Type = row.Field<string>("type"),
                    Count = Convert.ToInt32(row["Count"])
                })
                .ToList();

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.SetDataSource(reportData);
            reportViewer.ShowDialog();
        }

        private void LoadUserSchedulesReport(string userName)
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataTable scheduleData = dbAccess.GetScheduleForEachUser(userName);

            if (scheduleData == null || scheduleData.Rows.Count == 0)
            {
                MessageBox.Show($"{resources.NoApptsForUser} {userName}.", $"{resources.NoAppts}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var reportData = scheduleData.AsEnumerable()
                .Select(row => new
                {
                    UserName = row.Field<string>("userName"),
                    Type = row.Field<string>("type"),
                    Title = row.Field<string>("title"),
                    Start = row.Field<DateTime>("start").ToString("g"),
                    End = row.Field<DateTime>("end").ToString("g"),
                    CustomerName = row.Field<string>("customerName"),
                    Description = row.Field<string>("description")
                })
                .ToList();

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.SetDataSource(reportData);
            reportViewer.ShowDialog();
        }

        private void LoadCustomerEngagementReport()
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataTable engagementData = dbAccess.GetCustomerEngagementReport();

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.SetDataSource(engagementData);
            reportViewer.ShowDialog();
        }

        private void apptBtn_Click(object sender, EventArgs e)
        {
            LoadAppointmentTypesReport();
        }

        private void schBtn_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox($"{resources.UserScheduleName}:", $"{resources.UserScheduleReport}", "");
            if (input == "")
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show($"{resources.UserNotEmpty}", $"{resources.InputError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            if (!dbAccess.UserExists(input))
            {
                MessageBox.Show($"{resources.NoUserFound} '{input}'. {resources.NoUserFoundFix}.", $"{resources.UserNotFound}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadUserSchedulesReport(input);
        }

        private void custBtn_Click(object sender, EventArgs e)
        {
            LoadCustomerEngagementReport();
        }

        private void apptAdd_Click(object sender, EventArgs e)
        {
            AddAppointment addAppointmentForm = new AddAppointment();
            addAppointmentForm.FormClosed += (s, args) => RefreshAppointmentsGrid();
            addAppointmentForm.ShowDialog();
        }

        private void ShowModifyAppointmentForm(int appointmentId)
        {
            ModifyAppointment modifyAppointmentForm = new ModifyAppointment(appointmentId);
            modifyAppointmentForm.FormClosed += (sender, e) => RefreshAppointmentsGrid();
            modifyAppointmentForm.ShowDialog();
        }

        private void apptMod_Click(object sender, EventArgs e)
        {
            if (apptGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show($"{resources.SelectApptMod}", $"{resources.SelectReq}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int appointmentId = Convert.ToInt32(apptGrid.SelectedRows[0].Cells["appointmentId"].Value);
            ShowModifyAppointmentForm(appointmentId);
        }

        private void apptGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                apptGrid.Rows[e.RowIndex].Selected = true;
            }
        }

        private void apptDel_Click(object sender, EventArgs e)
        {
            if (apptGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show($"{resources.SelectApptDel}", $"{resources.SelectReq}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"{resources.AreYouSure}", $"{resources.ConfirmDelete}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
                apptDel.Enabled = false;
                bool isDeleted = dbAccess.DeleteAppointment(Convert.ToInt32(apptGrid.SelectedRows[0].Cells["appointmentId"].Value));
                if (isDeleted)
                {
                    MessageBox.Show($"{resources.ApptDelSuccess}", $"{resources.Deleted}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"{resources.ApptDelFail}", $"{resources.Delete}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                RefreshAppointmentsGrid();
                apptDel.Enabled = true;
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
