namespace C969_WGU_TallisJordan.Forms
{
    partial class ModifyAppointment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifyAppointment));
            delBtn = new Button();
            consulCombo = new ComboBox();
            descriptLabel = new Label();
            titleLabel = new Label();
            descripText = new TextBox();
            titleText = new TextBox();
            custCombo = new ComboBox();
            endPick = new DateTimePicker();
            startPick = new DateTimePicker();
            typeCombo = new ComboBox();
            cancelBtn = new Button();
            saveBtn = new Button();
            endLabel = new Label();
            startLabel = new Label();
            typeLabel = new Label();
            userLabel = new Label();
            custLabel = new Label();
            modApptLabel = new Label();
            locationLabel = new Label();
            locationText = new TextBox();
            contactLabel = new Label();
            contactText = new TextBox();
            urlLabel = new Label();
            urlText = new TextBox();
            SuspendLayout();
            // 
            // delBtn
            // 
            delBtn.Location = new Point(388, 705);
            delBtn.Name = "delBtn";
            delBtn.Size = new Size(131, 40);
            delBtn.TabIndex = 63;
            delBtn.Text = "Delete";
            delBtn.UseVisualStyleBackColor = true;
            delBtn.Click += delBtn_Click;
            // 
            // consulCombo
            // 
            consulCombo.FormattingEnabled = true;
            consulCombo.Location = new Point(197, 128);
            consulCombo.Name = "consulCombo";
            consulCombo.Size = new Size(350, 38);
            consulCombo.TabIndex = 62;
            // 
            // descriptLabel
            // 
            descriptLabel.AutoSize = true;
            descriptLabel.Location = new Point(35, 331);
            descriptLabel.Name = "descriptLabel";
            descriptLabel.Size = new Size(118, 30);
            descriptLabel.TabIndex = 61;
            descriptLabel.Text = "Description";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(101, 266);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(52, 30);
            titleLabel.TabIndex = 60;
            titleLabel.Text = "Title";
            // 
            // descripText
            // 
            descripText.Location = new Point(197, 326);
            descripText.Name = "descripText";
            descripText.Size = new Size(350, 35);
            descripText.TabIndex = 59;
            // 
            // titleText
            // 
            titleText.Location = new Point(197, 261);
            titleText.Name = "titleText";
            titleText.Size = new Size(350, 35);
            titleText.TabIndex = 58;
            // 
            // custCombo
            // 
            custCombo.FormattingEnabled = true;
            custCombo.Location = new Point(197, 66);
            custCombo.Name = "custCombo";
            custCombo.Size = new Size(350, 38);
            custCombo.TabIndex = 57;
            // 
            // endPick
            // 
            endPick.Location = new Point(197, 452);
            endPick.Name = "endPick";
            endPick.Size = new Size(350, 35);
            endPick.TabIndex = 56;
            // 
            // startPick
            // 
            startPick.Location = new Point(197, 386);
            startPick.Name = "startPick";
            startPick.Size = new Size(350, 35);
            startPick.TabIndex = 55;
            // 
            // typeCombo
            // 
            typeCombo.FormattingEnabled = true;
            typeCombo.Location = new Point(197, 196);
            typeCombo.Name = "typeCombo";
            typeCombo.Size = new Size(350, 38);
            typeCombo.TabIndex = 54;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(251, 705);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(131, 40);
            cancelBtn.TabIndex = 53;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(114, 705);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(131, 40);
            saveBtn.TabIndex = 52;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // endLabel
            // 
            endLabel.AutoSize = true;
            endLabel.Location = new Point(105, 457);
            endLabel.Name = "endLabel";
            endLabel.Size = new Size(48, 30);
            endLabel.TabIndex = 51;
            endLabel.Text = "End";
            // 
            // startLabel
            // 
            startLabel.AutoSize = true;
            startLabel.Location = new Point(98, 391);
            startLabel.Name = "startLabel";
            startLabel.Size = new Size(55, 30);
            startLabel.TabIndex = 50;
            startLabel.Text = "Start";
            // 
            // typeLabel
            // 
            typeLabel.AutoSize = true;
            typeLabel.Location = new Point(97, 204);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(56, 30);
            typeLabel.TabIndex = 49;
            typeLabel.Text = "Type";
            // 
            // userLabel
            // 
            userLabel.AutoSize = true;
            userLabel.Location = new Point(40, 136);
            userLabel.Name = "userLabel";
            userLabel.Size = new Size(113, 30);
            userLabel.TabIndex = 48;
            userLabel.Text = "Consultant";
            // 
            // custLabel
            // 
            custLabel.AutoSize = true;
            custLabel.Location = new Point(51, 74);
            custLabel.Name = "custLabel";
            custLabel.Size = new Size(102, 30);
            custLabel.TabIndex = 47;
            custLabel.Text = "Customer";
            // 
            // modApptLabel
            // 
            modApptLabel.AutoSize = true;
            modApptLabel.Location = new Point(212, 19);
            modApptLabel.Name = "modApptLabel";
            modApptLabel.Size = new Size(206, 30);
            modApptLabel.TabIndex = 46;
            modApptLabel.Text = "Modify Appointment";
            // 
            // locationLabel
            // 
            locationLabel.AutoSize = true;
            locationLabel.Location = new Point(61, 520);
            locationLabel.Name = "locationLabel";
            locationLabel.Size = new Size(92, 30);
            locationLabel.TabIndex = 65;
            locationLabel.Text = "Location";
            // 
            // locationText
            // 
            locationText.Location = new Point(197, 515);
            locationText.Name = "locationText";
            locationText.Size = new Size(350, 35);
            locationText.TabIndex = 64;
            // 
            // contactLabel
            // 
            contactLabel.AutoSize = true;
            contactLabel.Location = new Point(68, 585);
            contactLabel.Name = "contactLabel";
            contactLabel.Size = new Size(85, 30);
            contactLabel.TabIndex = 67;
            contactLabel.Text = "Contact";
            // 
            // contactText
            // 
            contactText.Location = new Point(197, 580);
            contactText.Name = "contactText";
            contactText.Size = new Size(350, 35);
            contactText.TabIndex = 66;
            // 
            // urlLabel
            // 
            urlLabel.AutoSize = true;
            urlLabel.Location = new Point(103, 650);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new Size(50, 30);
            urlLabel.TabIndex = 69;
            urlLabel.Text = "URL";
            // 
            // urlText
            // 
            urlText.Location = new Point(197, 645);
            urlText.Name = "urlText";
            urlText.Size = new Size(350, 35);
            urlText.TabIndex = 68;
            // 
            // ModifyAppointment
            // 
            AutoScaleDimensions = new SizeF(168F, 168F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(629, 757);
            Controls.Add(urlLabel);
            Controls.Add(urlText);
            Controls.Add(contactLabel);
            Controls.Add(contactText);
            Controls.Add(locationLabel);
            Controls.Add(locationText);
            Controls.Add(delBtn);
            Controls.Add(consulCombo);
            Controls.Add(descriptLabel);
            Controls.Add(titleLabel);
            Controls.Add(descripText);
            Controls.Add(titleText);
            Controls.Add(custCombo);
            Controls.Add(endPick);
            Controls.Add(startPick);
            Controls.Add(typeCombo);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(endLabel);
            Controls.Add(startLabel);
            Controls.Add(typeLabel);
            Controls.Add(userLabel);
            Controls.Add(custLabel);
            Controls.Add(modApptLabel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ModifyAppointment";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button delBtn;
        private ComboBox consulCombo;
        private Label descriptLabel;
        private Label titleLabel;
        private TextBox descripText;
        private TextBox titleText;
        private ComboBox custCombo;
        private DateTimePicker endPick;
        private DateTimePicker startPick;
        private ComboBox typeCombo;
        private Button cancelBtn;
        private Button saveBtn;
        private Label endLabel;
        private Label startLabel;
        private Label typeLabel;
        private Label userLabel;
        private Label custLabel;
        private Label modApptLabel;
        private Label locationLabel;
        private TextBox locationText;
        private Label contactLabel;
        private TextBox contactText;
        private Label urlLabel;
        private TextBox urlText;
    }
}