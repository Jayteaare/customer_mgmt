namespace C969_WGU_TallisJordan.Forms
{
    partial class AddCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCustomer));
            addCustLabel = new Label();
            nameLabel = new Label();
            phoneLabel = new Label();
            addressLabel = new Label();
            cityLabel = new Label();
            zipLabel = new Label();
            countryLabel = new Label();
            activeLabel = new Label();
            nameText = new TextBox();
            phoneText = new TextBox();
            addressText = new TextBox();
            cityText = new TextBox();
            zipText = new TextBox();
            countryText = new TextBox();
            yesBtn = new RadioButton();
            noBtn = new RadioButton();
            saveBtn = new Button();
            cancelBtn = new Button();
            SuspendLayout();
            // 
            // addCustLabel
            // 
            addCustLabel.AutoSize = true;
            addCustLabel.Location = new Point(101, 23);
            addCustLabel.Name = "addCustLabel";
            addCustLabel.Size = new Size(146, 30);
            addCustLabel.TabIndex = 0;
            addCustLabel.Text = "Add Customer";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(45, 82);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(69, 30);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "Name";
            // 
            // phoneLabel
            // 
            phoneLabel.AutoSize = true;
            phoneLabel.Location = new Point(45, 144);
            phoneLabel.Name = "phoneLabel";
            phoneLabel.Size = new Size(72, 30);
            phoneLabel.TabIndex = 2;
            phoneLabel.Text = "Phone";
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Location = new Point(30, 212);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(87, 30);
            addressLabel.TabIndex = 3;
            addressLabel.Text = "Address";
            // 
            // cityLabel
            // 
            cityLabel.AutoSize = true;
            cityLabel.Location = new Point(69, 279);
            cityLabel.Name = "cityLabel";
            cityLabel.Size = new Size(48, 30);
            cityLabel.TabIndex = 4;
            cityLabel.Text = "City";
            // 
            // zipLabel
            // 
            zipLabel.AutoSize = true;
            zipLabel.Location = new Point(21, 345);
            zipLabel.Name = "zipLabel";
            zipLabel.Size = new Size(96, 30);
            zipLabel.TabIndex = 5;
            zipLabel.Text = "Zip Code";
            // 
            // countryLabel
            // 
            countryLabel.AutoSize = true;
            countryLabel.Location = new Point(31, 412);
            countryLabel.Name = "countryLabel";
            countryLabel.Size = new Size(86, 30);
            countryLabel.TabIndex = 6;
            countryLabel.Text = "Country";
            // 
            // activeLabel
            // 
            activeLabel.AutoSize = true;
            activeLabel.Location = new Point(47, 467);
            activeLabel.Name = "activeLabel";
            activeLabel.Size = new Size(70, 30);
            activeLabel.TabIndex = 7;
            activeLabel.Text = "Active";
            // 
            // nameText
            // 
            nameText.Location = new Point(123, 77);
            nameText.Name = "nameText";
            nameText.Size = new Size(175, 35);
            nameText.TabIndex = 8;
            // 
            // phoneText
            // 
            phoneText.Location = new Point(123, 139);
            phoneText.Name = "phoneText";
            phoneText.Size = new Size(175, 35);
            phoneText.TabIndex = 9;
            // 
            // addressText
            // 
            addressText.Location = new Point(123, 207);
            addressText.Name = "addressText";
            addressText.Size = new Size(175, 35);
            addressText.TabIndex = 10;
            // 
            // cityText
            // 
            cityText.Location = new Point(123, 274);
            cityText.Name = "cityText";
            cityText.Size = new Size(175, 35);
            cityText.TabIndex = 11;
            // 
            // zipText
            // 
            zipText.Location = new Point(123, 340);
            zipText.Name = "zipText";
            zipText.Size = new Size(175, 35);
            zipText.TabIndex = 12;
            // 
            // countryText
            // 
            countryText.Location = new Point(123, 407);
            countryText.Name = "countryText";
            countryText.Size = new Size(175, 35);
            countryText.TabIndex = 13;
            // 
            // yesBtn
            // 
            yesBtn.AutoSize = true;
            yesBtn.Location = new Point(140, 465);
            yesBtn.Name = "yesBtn";
            yesBtn.Size = new Size(68, 34);
            yesBtn.TabIndex = 14;
            yesBtn.TabStop = true;
            yesBtn.Text = "Yes";
            yesBtn.UseVisualStyleBackColor = true;
            // 
            // noBtn
            // 
            noBtn.AutoSize = true;
            noBtn.Location = new Point(220, 467);
            noBtn.Name = "noBtn";
            noBtn.Size = new Size(66, 34);
            noBtn.TabIndex = 15;
            noBtn.TabStop = true;
            noBtn.Text = "No";
            noBtn.UseVisualStyleBackColor = true;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(39, 513);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(131, 40);
            saveBtn.TabIndex = 16;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(176, 513);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(131, 40);
            cancelBtn.TabIndex = 17;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // AddCustomer
            // 
            AutoScaleDimensions = new SizeF(168F, 168F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(343, 572);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(noBtn);
            Controls.Add(yesBtn);
            Controls.Add(countryText);
            Controls.Add(zipText);
            Controls.Add(cityText);
            Controls.Add(addressText);
            Controls.Add(phoneText);
            Controls.Add(nameText);
            Controls.Add(activeLabel);
            Controls.Add(countryLabel);
            Controls.Add(zipLabel);
            Controls.Add(cityLabel);
            Controls.Add(addressLabel);
            Controls.Add(phoneLabel);
            Controls.Add(nameLabel);
            Controls.Add(addCustLabel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddCustomer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label addCustLabel;
        private Label nameLabel;
        private Label phoneLabel;
        private Label addressLabel;
        private Label cityLabel;
        private Label zipLabel;
        private Label countryLabel;
        private Label activeLabel;
        private TextBox nameText;
        private TextBox phoneText;
        private TextBox addressText;
        private TextBox cityText;
        private TextBox zipText;
        private TextBox countryText;
        private RadioButton yesBtn;
        private RadioButton noBtn;
        private Button saveBtn;
        private Button cancelBtn;
    }
}