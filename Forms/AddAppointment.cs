using C969_WGU_TallisJordan.Classes;
using C969_WGU_TallisJordan.Resources;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969_WGU_TallisJordan.Forms
{
    public partial class AddAppointment : Form
    {
        public AddAppointment()
        {
            InitializeComponent();
            UpdateUIWithLocalizedText();
            PopulateCustomerCombo();
            PopulateConsultantCombo();
            PopulateTypeCombo();
            SetupDateTimePickers();
        }

        private void UpdateUIWithLocalizedText()
        {
            cancelBtn.Text = resources.CancelTitle;
            saveBtn.Text = resources.SaveTitle;
            startLabel.Text = resources.StartTitle;
            typeLabel.Text = resources.TypeTitle;
            userLabel.Text = resources.ConsultantTitle;
            custLabel.Text = resources.CustomerTitle;
            addApptLabel.Text = resources.AddApptTitle;
            endLabel.Text = resources.EndTitle;
            titleLabel.Text = resources.TitleTitle;
            descriptLabel.Text = resources.DescripTitle;
        }

        private void PopulateCustomerCombo()
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataTable customers = dbAccess.GetAllCustomers();

            custCombo.DisplayMember = "customerName";
            custCombo.ValueMember = "customerId";
            custCombo.DataSource = customers;

            if (custCombo.Items.Count > 0)
                custCombo.SelectedIndex = 0;
        }

        private void PopulateConsultantCombo()
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataTable users = dbAccess.GetAllUsers();

            consulCombo.DisplayMember = "userName";
            consulCombo.ValueMember = "userId";
            consulCombo.DataSource = users;

            if (consulCombo.Items.Count > 0)
                consulCombo.SelectedIndex = 0;
        }

        private void PopulateTypeCombo()
        {
            typeCombo.Items.Clear();
            typeCombo.Items.Add("Not Needed");
            typeCombo.Items.Add("Initial");
            typeCombo.Items.Add("Follow-Up");
            typeCombo.Items.Add("Presentation");
            typeCombo.SelectedIndex = 0;
        }

        private void SetupDateTimePickers()
        {
            startPick.Format = DateTimePickerFormat.Custom;
            startPick.CustomFormat = "yyyy-MM-dd HH:mm";

            endPick.Format = DateTimePickerFormat.Custom;
            endPick.CustomFormat = "yyyy-MM-dd HH:mm";

            startPick.ShowUpDown = true;
            endPick.ShowUpDown = true;
        }

        private void cmbAppointmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = typeCombo.SelectedItem.ToString();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            int customerId = Convert.ToInt32(custCombo.SelectedValue);
            int userId = Convert.ToInt32(consulCombo.SelectedValue);
            string appointmentType = typeCombo.SelectedItem.ToString();
            string title = titleText.Text;
            string description = descripText.Text;
            DateTime startTime = startPick.Value;
            DateTime endTime = endPick.Value;

            if (endTime <= startTime)
            {
                MessageBox.Show($"{resources.EndAfterStart}", $"{resources.InputError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");

            if (dbAccess.HasOverlappingAppointments(userId, startTime, endTime))
            {
                MessageBox.Show($"{resources.OverlappingConsAppt}", $"{resources.ScheduleConflict}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dbAccess.HasOverlappingCustomerAppointments(customerId, startTime, endTime))
            {
                MessageBox.Show($"{resources.OverlappingCustAppt}", $"{resources.ScheduleConflict}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool result = dbAccess.AddAppointment(customerId, userId, appointmentType, title, description, startTime, endTime);
            if (result)
            {
                MessageBox.Show($"{resources.ApptAddSuccess}", $"{resources.Success}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"{resources.ApptAddFail}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (typeCombo.SelectedItem == null)
            {
                MessageBox.Show($"{resources.SelectApptType}", $"{resources.ValidationError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (startPick.Value >= endPick.Value)
            {
                MessageBox.Show($"{resources.StartBeforeEnd}", $"{resources.ValidationError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DateTime estStartTime = TimeZoneInfo.ConvertTime(startPick.Value, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            DateTime estEndTime = TimeZoneInfo.ConvertTime(endPick.Value, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

            if (estStartTime.DayOfWeek == DayOfWeek.Saturday || estStartTime.DayOfWeek == DayOfWeek.Sunday ||
                estEndTime.DayOfWeek == DayOfWeek.Saturday || estEndTime.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show($"{resources.ScheduleWeekday}", $"{resources.ValidationError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (estStartTime.Hour < 9 || estEndTime.Hour > 17 || estStartTime.Hour > 17 || estEndTime.Hour < 9)
            {
                MessageBox.Show($"{resources.ScheduleTimes}", $"{resources.ValidationError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }








    }
}
