using C969_WGU_TallisJordan.Classes;
using C969_WGU_TallisJordan.Resources;
using MySql.Data.MySqlClient;
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
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
            UpdateUIWithLocalizedText();
            saveBtn.Click += new EventHandler(saveBtn_Click);
        }

        private void UpdateUIWithLocalizedText()
        {
            addCustLabel.Text = resources.AddCustomer;
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

            string customerName = nameText.Text.Trim();
            string address = addressText.Text.Trim();
            string postalCode = zipText.Text.Trim();
            string cityName = cityText.Text.Trim();
            string countryName = countryText.Text.Trim();
            string phone = phoneText.Text.Trim();
            bool isActive = yesBtn.Checked;

            DatabaseAccess dbAccess = new DatabaseAccess(GlobalVariables.CurrentUser, "test");
            var result = dbAccess.AddCustomer(customerName, address, postalCode, cityName, countryName, phone, isActive);

            if (result.CustomerAlreadyExists)
            {
                MessageBox.Show($"{resources.CustExists}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (result.Success)
            {
                MessageBox.Show($"{resources.CustAddSuccess}", $"{resources.Success}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                if (result.CountryNotFound)
                {
                    if (MessageBox.Show($"{resources.Country} '{result.CountryName}' {resources.NotFound}. {resources.ShouldAddit}", $"{resources.AddCountry}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int newCountryId = dbAccess.AddCountryAndGetId(countryName);
                        if (newCountryId != -1)
                        {
                            MessageBox.Show($"{resources.CountryAddSuccess}", $"{resources.Success}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"{resources.CountryAddFail}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (result.CityNotFound)
                {
                    if (MessageBox.Show($"{resources.City} '{result.CityName}' {resources.NotFoundIn} '{countryName}'. {resources.ShouldAddit}", $"{resources.AddCity}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int countryId = dbAccess.GetCountryId(countryName);
                        int newCityId = dbAccess.AddCityAndGetId(cityName, countryId);
                        if (newCityId != -1)
                        {
                            MessageBox.Show($"{resources.CityAddSuccess}", $"{resources.Success}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"{resources.CityAddFail}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }


        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
