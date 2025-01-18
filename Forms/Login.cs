using System;
using System.IO;
using System.Windows.Forms;
using C969_WGU_TallisJordan.Forms;
using C969_WGU_TallisJordan.Classes;
using C969_WGU_TallisJordan.Resources;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Text.RegularExpressions;

namespace C969_WGU_TallisJordan
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            ApplyLocalization();
        }

        private string GetUserLocale()
        {
            return CultureInfo.InstalledUICulture.Name;
        }

        private void ApplyLocalization()
        {
            string userLocale = GetUserLocale();

            CultureInfo culture = new CultureInfo(userLocale);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            UpdateUIWithLocalizedText();
        }

        private void UpdateUIWithLocalizedText()
        {
            loginBtn.Text = resources.LoginButton;
            exitBtn.Text = resources.ExitButton;
            welcomeLabel.Text = resources.WelcomeLabel;
            loginLabel.Text = resources.LoginLabel;
            userLabel.Text = resources.UserLabel;
            passLabel.Text = resources.PassLabel;
            this.Text = resources.LoginFormTitle;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            AttemptLogin();
        }

        private void AttemptLogin()
        {
            string userName = userText.Text.Trim();
            string password = passText.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(resources.FieldsCannotBeEmpty);
                return;
            }

            DatabaseAccess dbAccess = new DatabaseAccess(userName, password);
            if (dbAccess.TestConnection())
            {
                int userId = dbAccess.GetUserId(userName);
                if (userId > 0)
                {
                    bool isValidUser = dbAccess.IsLoginValid(userName, password);
                    LogLoginAttempt(userName, isValidUser);

                    if (isValidUser)
                    {
                        GlobalVariables.CurrentUser = userName;
                        this.Hide();

                        Main mainForm = new Main();
                        mainForm.FormClosed += (s, args) => this.Close();
                        mainForm.Show();

                        var appointmentInfo = dbAccess.GetUpcomingAppointmentInfo(userId);
                        if (appointmentInfo.IsAppointmentSoon)
                        {
                            string message = $"{resources.LoginSuccessful}\n\n{resources.AppointmentReminder} {appointmentInfo.CustomerName} {resources.InNext15Minutes}";
                            MessageBox.Show(message, $"{resources.ApptAlert}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(resources.LoginSuccessful, $"{resources.Success}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(resources.LoginFailed, $"{resources.LoginFailed}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(resources.UserNotFound, $"{resources.UserNotFound}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(resources.GenericSQLError, $"{resources.DatabaseError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogLoginAttempt(string userName, bool isSuccess)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appName = "C969_WGU_TallisJordan";
            string directoryPath = Path.Combine(appDataPath, appName);

            Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, "Login_History.txt");
            string resultText = isSuccess ? "Login attempt successful" : "Login attempt failed";
            string logEntry = $"{DateTime.Now}: {resources.LoginAttemptForUser} '{userName}' - {resultText}\n";

            File.AppendAllText(filePath, logEntry);
        }
    }
}