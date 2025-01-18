namespace C969_WGU_TallisJordan.Forms
{
    partial class ModifyCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifyCustomer));
            cancelBtn = new Button();
            saveBtn = new Button();
            noBtn = new RadioButton();
            yesBtn = new RadioButton();
            countryText = new TextBox();
            zipText = new TextBox();
            cityText = new TextBox();
            addressText = new TextBox();
            phoneText = new TextBox();
            nameText = new TextBox();
            activeLabel = new Label();
            countryLabel = new Label();
            zipLabel = new Label();
            cityLabel = new Label();
            addressLabel = new Label();
            phoneLabel = new Label();
            nameLabel = new Label();
            addCustLabel = new Label();
            delBtn = new Button();
            SuspendLayout();
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(122, 511);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(99, 40);
            cancelBtn.TabIndex = 35;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click_1;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(23, 511);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(93, 40);
            saveBtn.TabIndex = 34;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // noBtn
            // 
            noBtn.AutoSize = true;
            noBtn.Location = new Point(222, 465);
            noBtn.Name = "noBtn";
            noBtn.Size = new Size(66, 34);
            noBtn.TabIndex = 33;
            noBtn.TabStop = true;
            noBtn.Text = "No";
            noBtn.UseVisualStyleBackColor = true;
            // 
            // yesBtn
            // 
            yesBtn.AutoSize = true;
            yesBtn.Location = new Point(142, 463);
            yesBtn.Name = "yesBtn";
            yesBtn.Size = new Size(68, 34);
            yesBtn.TabIndex = 32;
            yesBtn.TabStop = true;
            yesBtn.Text = "Yes";
            yesBtn.UseVisualStyleBackColor = true;
            // 
            // countryText
            // 
            countryText.Location = new Point(125, 405);
            countryText.Name = "countryText";
            countryText.Size = new Size(175, 35);
            countryText.TabIndex = 31;
            // 
            // zipText
            // 
            zipText.Location = new Point(125, 338);
            zipText.Name = "zipText";
            zipText.Size = new Size(175, 35);
            zipText.TabIndex = 30;
            // 
            // cityText
            // 
            cityText.Location = new Point(125, 272);
            cityText.Name = "cityText";
            cityText.Size = new Size(175, 35);
            cityText.TabIndex = 29;
            // 
            // addressText
            // 
            addressText.Location = new Point(125, 205);
            addressText.Name = "addressText";
            addressText.Size = new Size(175, 35);
            addressText.TabIndex = 28;
            // 
            // phoneText
            // 
            phoneText.Location = new Point(125, 137);
            phoneText.Name = "phoneText";
            phoneText.Size = new Size(175, 35);
            phoneText.TabIndex = 27;
            // 
            // nameText
            // 
            nameText.Location = new Point(125, 75);
            nameText.Name = "nameText";
            nameText.Size = new Size(175, 35);
            nameText.TabIndex = 26;
            // 
            // activeLabel
            // 
            activeLabel.AutoSize = true;
            activeLabel.Location = new Point(49, 465);
            activeLabel.Name = "activeLabel";
            activeLabel.Size = new Size(70, 30);
            activeLabel.TabIndex = 25;
            activeLabel.Text = "Active";
            // 
            // countryLabel
            // 
            countryLabel.AutoSize = true;
            countryLabel.Location = new Point(33, 410);
            countryLabel.Name = "countryLabel";
            countryLabel.Size = new Size(86, 30);
            countryLabel.TabIndex = 24;
            countryLabel.Text = "Country";
            // 
            // zipLabel
            // 
            zipLabel.AutoSize = true;
            zipLabel.Location = new Point(23, 343);
            zipLabel.Name = "zipLabel";
            zipLabel.Size = new Size(96, 30);
            zipLabel.TabIndex = 23;
            zipLabel.Text = "Zip Code";
            // 
            // cityLabel
            // 
            cityLabel.AutoSize = true;
            cityLabel.Location = new Point(71, 277);
            cityLabel.Name = "cityLabel";
            cityLabel.Size = new Size(48, 30);
            cityLabel.TabIndex = 22;
            cityLabel.Text = "City";
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Location = new Point(32, 210);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(87, 30);
            addressLabel.TabIndex = 21;
            addressLabel.Text = "Address";
            // 
            // phoneLabel
            // 
            phoneLabel.AutoSize = true;
            phoneLabel.Location = new Point(47, 142);
            phoneLabel.Name = "phoneLabel";
            phoneLabel.Size = new Size(72, 30);
            phoneLabel.TabIndex = 20;
            phoneLabel.Text = "Phone";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(47, 80);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(69, 30);
            nameLabel.TabIndex = 19;
            nameLabel.Text = "Name";
            // 
            // addCustLabel
            // 
            addCustLabel.AutoSize = true;
            addCustLabel.Location = new Point(84, 24);
            addCustLabel.Name = "addCustLabel";
            addCustLabel.Size = new Size(173, 30);
            addCustLabel.TabIndex = 18;
            addCustLabel.Text = "Modify Customer";
            // 
            // delBtn
            // 
            delBtn.Location = new Point(227, 510);
            delBtn.Name = "delBtn";
            delBtn.Size = new Size(98, 42);
            delBtn.TabIndex = 36;
            delBtn.Text = "Delete";
            delBtn.UseVisualStyleBackColor = true;
            delBtn.Click += delBtn_Click;
            // 
            // ModifyCustomer
            // 
            AutoScaleDimensions = new SizeF(168F, 168F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(343, 572);
            Controls.Add(delBtn);
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
            Name = "ModifyCustomer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cancelBtn;
        private Button saveBtn;
        private RadioButton noBtn;
        private RadioButton yesBtn;
        private TextBox countryText;
        private TextBox zipText;
        private TextBox cityText;
        private TextBox addressText;
        private TextBox phoneText;
        private TextBox nameText;
        private Label activeLabel;
        private Label countryLabel;
        private Label zipLabel;
        private Label cityLabel;
        private Label addressLabel;
        private Label phoneLabel;
        private Label nameLabel;
        private Label addCustLabel;
        private Button delBtn;
    }
}