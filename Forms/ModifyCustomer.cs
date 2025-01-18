using C969_WGU_TallisJordan.Classes;
using C969_WGU_TallisJordan.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969_WGU_TallisJordan.Forms
{
    public partial class ModifyCustomer : Form
    {
        private int customerId;

        public ModifyCustomer(DataRow customerRow)
        {
            InitializeComponent();
            UpdateUIWithLocalizedText();
            if (customerRow != null)
            {
                LoadCustomerData(customerRow);
            }
            else
            {
                MessageBox.Show($"{resources.CustDataNotAvail}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadCustomerData(DataRow customerRow)
        {
            if (customerRow == null)
            {
                MessageBox.Show($"{resources.CustNoDataFound}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            customerId = Convert.ToInt32(customerRow["customerId"]);
            nameText.Text = customerRow["customerName"].ToString();
            addressText.Text = customerRow["address"].ToString();
            cityText.Text = customerRow["city"].ToString();
            countryText.Text = customerRow["country"].ToString();
            zipText.Text = customerRow["postalCode"].ToString();
            phoneText.Text = customerRow["phone"].ToString();
            yesBtn.Checked = Convert.ToBoolean(customerRow["active"]);
            noBtn.Checked = !yesBtn.Checked;
        }

        private void UpdateUIWithLocalizedText()
        {
            addCustLabel.Text = resources.ModifyCustomerTitle;
            nameLabel.Text = resources.Name;
            phoneLabel.Text = resources.Phone;
            addressLabel.Text = resources.Address;
            cityLabel.Text = resources.City;
            zipLabel.Text = resources.Zip;
            countryLabel.Text = resources.Country;
            activeLabel.Text = resources.Active;
            yesBtn.Text = resources.YesButton;
            noBtn.Text = resources.NoButton;
            saveBtn.Text = resources.SaveButton;
            cancelBtn.Text = resources.CancelButton;
            delBtn.Text = resources.DeleteButton;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(nameText.Text) ||
                string.IsNullOrWhiteSpace(addressText.Text) ||
                string.IsNullOrWhiteSpace(cityText.Text) ||
                string.IsNullOrWhiteSpace(countryText.Text) ||
                string.IsNullOrWhiteSpace(zipText.Text) ||
                string.IsNullOrWhiteSpace(phoneText.Text))
            {
                MessageBox.Show($"{resources.AllFieldsFilled}", $"{resources.ValidationError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(zipText.Text, @"^\d{5}$"))
            {
                MessageBox.Show($"{resources.PostalCode}", $"{resources.ValidationError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(phoneText.Text, @"^\d{3}-\d{3}-\d{4}$"))
            {
                MessageBox.Show($"{resources.PhoneNumber}", $"{resources.ValidationError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            string cityName = cityText.Text;
            string countryName = countryText.Text;
            bool isActive = yesBtn.Checked;

            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            int countryId = dbAccess.GetCountryId(countryName);

            if (countryId == -1)
            {
                DialogResult addCountryResponse = MessageBox.Show($"{resources.Country} '{countryName}' {resources.NotFound}. {resources.ShouldAddit}", $"{resources.AddCountry}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (addCountryResponse == DialogResult.Yes)
                {
                    countryId = dbAccess.AddCountryAndGetId(countryName);
                }
                else
                {
                    MessageBox.Show($"{resources.CountryOpCancel}", $"{resources.Cancelled}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                int cityId = dbAccess.GetCityId(cityName, countryId);
                if (cityId == -1)
                {
                    DialogResult addCityResponse = MessageBox.Show($"{resources.City} '{cityName}' {resources.NotFoundIn} '{countryName}'. {resources.ShouldAddit}", $"{resources.AddCity}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (addCityResponse == DialogResult.Yes)
                    {
                        cityId = dbAccess.AddCityAndGetId(cityName, countryId);
                    }
                    else
                    {
                        MessageBox.Show($"{resources.CityOpCancel}", $"{resources.Cancelled}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                UpdateCustomerDetails(dbAccess, cityName, countryId, cityId, isActive);
            }
        }

        private void UpdateCustomerDetails(DatabaseAccess dbAccess, string cityName, int countryId, int cityId, bool isActive)
        {
            bool isUpdated = dbAccess.UpdateCustomer(customerId, nameText.Text, addressText.Text, zipText.Text, phoneText.Text, isActive);
            if (isUpdated)
            {
                MessageBox.Show($"{resources.CustModSuccess}", $"{resources.Success}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"{resources.CustModFail}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"{resources.AreYouSureCust}", $"{resources.ConfirmDelete}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
                    if (dbAccess.CustomerHasAppointments(customerId))
                    {
                        if (MessageBox.Show($"{resources.CustApptDel}", $"{resources.ConfirmDelete}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dbAccess.DeleteAllAppointmentsForCustomer(customerId);
                            dbAccess.DeleteCustomer(customerId);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        dbAccess.DeleteCustomer(customerId);
                    }
                    MessageBox.Show($"{resources.CustDelSuccess}", $"{resources.Deleted}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{resources.ErrorOccured}: {ex.Message}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
