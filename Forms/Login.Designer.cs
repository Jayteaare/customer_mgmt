namespace C969_WGU_TallisJordan
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            userLabel = new Label();
            passLabel = new Label();
            userText = new TextBox();
            passText = new TextBox();
            loginBtn = new Button();
            exitBtn = new Button();
            welcomeLabel = new Label();
            loginLabel = new Label();
            SuspendLayout();
            // 
            // userLabel
            // 
            userLabel.AutoSize = true;
            userLabel.Location = new Point(388, 300);
            userLabel.Name = "userLabel";
            userLabel.Size = new Size(106, 30);
            userLabel.TabIndex = 0;
            userLabel.Text = "Username";
            // 
            // passLabel
            // 
            passLabel.AutoSize = true;
            passLabel.Location = new Point(397, 342);
            passLabel.Name = "passLabel";
            passLabel.Size = new Size(99, 30);
            passLabel.TabIndex = 1;
            passLabel.Text = "Password";
            // 
            // userText
            // 
            userText.Location = new Point(505, 297);
            userText.Name = "userText";
            userText.Size = new Size(185, 35);
            userText.TabIndex = 2;
            // 
            // passText
            // 
            passText.Location = new Point(505, 339);
            passText.Name = "passText";
            passText.PasswordChar = '*';
            passText.Size = new Size(185, 35);
            passText.TabIndex = 3;
            // 
            // loginBtn
            // 
            loginBtn.Location = new Point(454, 395);
            loginBtn.Name = "loginBtn";
            loginBtn.Size = new Size(138, 43);
            loginBtn.TabIndex = 4;
            loginBtn.Text = "Login";
            loginBtn.UseVisualStyleBackColor = true;
            loginBtn.Click += loginBtn_Click;
            // 
            // exitBtn
            // 
            exitBtn.Location = new Point(598, 395);
            exitBtn.Name = "exitBtn";
            exitBtn.Size = new Size(138, 43);
            exitBtn.TabIndex = 5;
            exitBtn.Text = "Exit";
            exitBtn.UseVisualStyleBackColor = true;
            exitBtn.Click += exitBtn_Click;
            // 
            // welcomeLabel
            // 
            welcomeLabel.AutoSize = true;
            welcomeLabel.Location = new Point(534, 114);
            welcomeLabel.Name = "welcomeLabel";
            welcomeLabel.Size = new Size(105, 30);
            welcomeLabel.TabIndex = 6;
            welcomeLabel.Text = "Welcome!";
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Location = new Point(493, 144);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(185, 30);
            loginLabel.TabIndex = 7;
            loginLabel.Text = "Please login below";
            // 
            // Login
            // 
            AcceptButton = loginBtn;
            AutoScaleDimensions = new SizeF(168F, 168F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1208, 682);
            Controls.Add(loginLabel);
            Controls.Add(welcomeLabel);
            Controls.Add(exitBtn);
            Controls.Add(loginBtn);
            Controls.Add(passText);
            Controls.Add(userText);
            Controls.Add(passLabel);
            Controls.Add(userLabel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label userLabel;
        private Label passLabel;
        private TextBox userText;
        private TextBox passText;
        private Button loginBtn;
        private Button exitBtn;
        private Label welcomeLabel;
        private Label loginLabel;
    }
}
