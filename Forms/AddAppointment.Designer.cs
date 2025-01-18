namespace C969_WGU_TallisJordan.Forms
{
    partial class AddAppointment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAppointment));
            cancelBtn = new Button();
            saveBtn = new Button();
            startLabel = new Label();
            typeLabel = new Label();
            userLabel = new Label();
            custLabel = new Label();
            addApptLabel = new Label();
            endLabel = new Label();
            typeCombo = new ComboBox();
            startPick = new DateTimePicker();
            endPick = new DateTimePicker();
            custCombo = new ComboBox();
            titleText = new TextBox();
            descripText = new TextBox();
            titleLabel = new Label();
            descriptLabel = new Label();
            consulCombo = new ComboBox();
            SuspendLayout();
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(310, 522);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(131, 40);
            cancelBtn.TabIndex = 35;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(173, 522);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(131, 40);
            saveBtn.TabIndex = 34;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // startLabel
            // 
            startLabel.AutoSize = true;
            startLabel.Location = new Point(110, 397);
            startLabel.Name = "startLabel";
            startLabel.Size = new Size(55, 30);
            startLabel.TabIndex = 22;
            startLabel.Text = "Start";
            // 
            // typeLabel
            // 
            typeLabel.AutoSize = true;
            typeLabel.Location = new Point(109, 210);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(56, 30);
            typeLabel.TabIndex = 21;
            typeLabel.Text = "Type";
            // 
            // userLabel
            // 
            userLabel.AutoSize = true;
            userLabel.Location = new Point(52, 142);
            userLabel.Name = "userLabel";
            userLabel.Size = new Size(113, 30);
            userLabel.TabIndex = 20;
            userLabel.Text = "Consultant";
            // 
            // custLabel
            // 
            custLabel.AutoSize = true;
            custLabel.Location = new Point(63, 80);
            custLabel.Name = "custLabel";
            custLabel.Size = new Size(102, 30);
            custLabel.TabIndex = 19;
            custLabel.Text = "Customer";
            // 
            // addApptLabel
            // 
            addApptLabel.AutoSize = true;
            addApptLabel.Location = new Point(212, 23);
            addApptLabel.Name = "addApptLabel";
            addApptLabel.Size = new Size(179, 30);
            addApptLabel.TabIndex = 18;
            addApptLabel.Text = "Add Appointment";
            // 
            // endLabel
            // 
            endLabel.AutoSize = true;
            endLabel.Location = new Point(117, 463);
            endLabel.Name = "endLabel";
            endLabel.Size = new Size(48, 30);
            endLabel.TabIndex = 23;
            endLabel.Text = "End";
            // 
            // typeCombo
            // 
            typeCombo.FormattingEnabled = true;
            typeCombo.Location = new Point(212, 202);
            typeCombo.Name = "typeCombo";
            typeCombo.Size = new Size(350, 38);
            typeCombo.TabIndex = 36;
            // 
            // startPick
            // 
            startPick.Location = new Point(212, 392);
            startPick.Name = "startPick";
            startPick.Size = new Size(350, 35);
            startPick.TabIndex = 37;
            // 
            // endPick
            // 
            endPick.Location = new Point(212, 458);
            endPick.Name = "endPick";
            endPick.Size = new Size(350, 35);
            endPick.TabIndex = 38;
            // 
            // custCombo
            // 
            custCombo.FormattingEnabled = true;
            custCombo.Location = new Point(212, 72);
            custCombo.Name = "custCombo";
            custCombo.Size = new Size(350, 38);
            custCombo.TabIndex = 39;
            // 
            // titleText
            // 
            titleText.Location = new Point(212, 267);
            titleText.Name = "titleText";
            titleText.Size = new Size(350, 35);
            titleText.TabIndex = 40;
            // 
            // descripText
            // 
            descripText.Location = new Point(212, 332);
            descripText.Name = "descripText";
            descripText.Size = new Size(350, 35);
            descripText.TabIndex = 41;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(113, 272);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(52, 30);
            titleLabel.TabIndex = 42;
            titleLabel.Text = "Title";
            // 
            // descriptLabel
            // 
            descriptLabel.AutoSize = true;
            descriptLabel.Location = new Point(47, 337);
            descriptLabel.Name = "descriptLabel";
            descriptLabel.Size = new Size(118, 30);
            descriptLabel.TabIndex = 43;
            descriptLabel.Text = "Description";
            // 
            // consulCombo
            // 
            consulCombo.FormattingEnabled = true;
            consulCombo.Location = new Point(212, 134);
            consulCombo.Name = "consulCombo";
            consulCombo.Size = new Size(350, 38);
            consulCombo.TabIndex = 44;
            // 
            // AddAppointment
            // 
            AutoScaleDimensions = new SizeF(168F, 168F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(629, 589);
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
            Controls.Add(addApptLabel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddAppointment";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cancelBtn;
        private Button saveBtn;
        private Label startLabel;
        private Label typeLabel;
        private Label userLabel;
        private Label custLabel;
        private Label addApptLabel;
        private Label endLabel;
        private ComboBox typeCombo;
        private DateTimePicker startPick;
        private DateTimePicker endPick;
        private ComboBox custCombo;
        private TextBox titleText;
        private TextBox descripText;
        private Label titleLabel;
        private Label descriptLabel;
        private ComboBox consulCombo;
    }
}