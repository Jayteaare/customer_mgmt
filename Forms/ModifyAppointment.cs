using C969_WGU_TallisJordan.Classes;
using C969_WGU_TallisJordan.Resources;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class ModifyAppointment : Form
    {
        private int appointmentId;

        private DatabaseAccess dbAccess;

        public ModifyAppointment(int appointmentId)
        {
            InitializeComponent();
            UpdateUIWithLocalizedText();
            delBtn.Click += new EventHandler(delBtn_Click);
            this.appointmentId = appointmentId;
            LoadAppointmentDetails();
            SetupDateTimePickers();
            dbAccess = new DatabaseAccess("test", "test");
            InitializeComboBoxes();
            PopulateComboBoxWithStaticItems();
        }

        private void UpdateUIWithLocalizedText()
        {
            cancelBtn.Text = resources.CancelTitle;
            saveBtn.Text = resources.SaveTitle;
            startLabel.Text = resources.StartTitle;
            typeLabel.Text = resources.TypeTitle;
            userLabel.Text = resources.ConsultantTitle;
            custLabel.Text = resources.CustomerTitle;
            modApptLabel.Text = resources.ModApptTitle;
            endLabel.Text = resources.EndTitle;
            titleLabel.Text = resources.TitleTitle;
            descriptLabel.Text = resources.DescripTitle;
            locationLabel.Text = resources.LocationTitle;
            urlLabel.Text = resources.URLTitle;
            contactLabel.Text = resources.ContactTitle;
            delBtn.Text = resources.DeleteTitle;
        }

        private void LoadAppointmentDetails()
        {
            DatabaseAccess dbAccess = new DatabaseAccess("test", "test");
            DataRow appointmentDetails = dbAccess.GetAppointmentById(appointmentId);
            if (appointmentDetails != null)
            {
                titleText.Text = appointmentDetails["title"].ToString();
                descripText.Text = appointmentDetails["description"].ToString();

                DateTime startTimeUtc = Convert.ToDateTime(appointmentDetails["start"]);
                DateTime endTimeUtc = Convert.ToDateTime(appointmentDetails["end"]);

                DateTime startTimeLocal = TimezoneHelper.ConvertEasternToLocal(startTimeUtc);
                DateTime endTimeLocal = TimezoneHelper.ConvertEasternToLocal(endTimeUtc);

                startPick.Value = startTimeLocal;
                endPick.Value = endTimeLocal;
                typeCombo.Text = appointmentDetails["type"].ToString();
                locationText.Text = appointmentDetails["location"].ToString();
                contactText.Text = appointmentDetails["contact"].ToString();
                urlText.Text = appointmentDetails["url"].ToString();
                custCombo.SelectedIndex = custCombo.FindStringExact(appointmentDetails["customerName"].ToString());
                consulCombo.SelectedIndex = consulCombo.FindStringExact(appointmentDetails["consultantName"].ToString());
            }
            else
            {
                MessageBox.Show($"{resources.ApptNotFound}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateComboBoxWithStaticItems()
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
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            string title = string.IsNullOrWhiteSpace(titleText.Text) ? "" : titleText.Text;
            string description = string.IsNullOrWhiteSpace(descripText.Text) ? "" : descripText.Text;
            string location = string.IsNullOrWhiteSpace(locationText.Text) ? "" : locationText.Text;
            string contact = string.IsNullOrWhiteSpace(contactText.Text) ? "" : contactText.Text;
            string url = string.IsNullOrWhiteSpace(urlText.Text) ? "" : urlText.Text;

            DateTime startTime = startPick.Value;
            DateTime endTime = endPick.Value;

            if (endTime <= startTime)
            {
                MessageBox.Show($"{resources.EndAfterStart}", $"{resources.InputError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userId = Convert.ToInt32(consulCombo.SelectedValue);
            int customerId = Convert.ToInt32(custCombo.SelectedValue);

            DatabaseAccess dbAccess = new DatabaseAccess(GlobalVariables.CurrentUser, "test");

            if (dbAccess.HasOverlappingAppointments(userId, startTime, endTime, appointmentId))
            {
                MessageBox.Show($"{resources.OverlappingConsAppt}", $"{resources.ScheduleConflict}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dbAccess.HasOverlappingCustomerAppointments(customerId, startTime, endTime, appointmentId))
            {
                MessageBox.Show($"{resources.OverlappingCustAppt}", $"{resources.ScheduleConflict}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool updateSuccess = dbAccess.UpdateAppointment(appointmentId, title, description, startTime, endTime, typeCombo.SelectedItem.ToString(), location, contact, url, userId);

            if (updateSuccess)
            {
                MessageBox.Show($"{resources.ApptModSuccess}", $"{resources.Success}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"{resources.ApptModFailed}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (typeCombo.SelectedItem == null)
            {
                MessageBox.Show($"{resources.SelectType}", $"{resources.ValidationError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (consulCombo.SelectedItem == null)
            {
                MessageBox.Show($"{resources.SelectConsul}", $"{resources.ValidationError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void InitializeComboBoxes()
        {
            PopulateCustomerComboBox();
            PopulateConsultantComboBox();
        }

        private void PopulateCustomerComboBox()
        {
            try
            {
                DataTable customers = dbAccess.GetAllCustomers();
                custCombo.DisplayMember = "customerName";
                custCombo.ValueMember = "customerId";
                custCombo.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{resources.FailCustLoad}: {ex.Message}", $"{resources.DatabaseError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateConsultantComboBox()
        {
            DatabaseAccess dbAccess = new DatabaseAccess(GlobalVariables.CurrentUser, "test");
            DataTable users = dbAccess.GetAllUsers();

            consulCombo.DisplayMember = "userName";
            consulCombo.ValueMember = "userId";
            consulCombo.DataSource = users;

            if (consulCombo.Items.Count > 0)
                consulCombo.SelectedIndex = 0;
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (delBtn.Enabled)
            {
                if (MessageBox.Show($"{resources.AreYouSureCust}", $"{resources.ConfirmDelete}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool isDeleted = dbAccess.DeleteAppointment(appointmentId);
                    if (isDeleted)
                    {
                        MessageBox.Show($"{resources.ApptDelSuccess}", $"{resources.Deleted}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        delBtn.Enabled = false;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"{resources.ApptDelFailLink}", $"{resources.Error}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
