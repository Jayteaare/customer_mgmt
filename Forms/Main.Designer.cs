namespace C969_WGU_TallisJordan.Forms
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            apptGrid = new DataGridView();
            calLabel = new Label();
            reportLabel = new Label();
            custLabel = new Label();
            custAdd = new Button();
            custMod = new Button();
            apptBtn = new Button();
            schBtn = new Button();
            apptAdd = new Button();
            apptMod = new Button();
            custDel = new Button();
            custBtn = new Button();
            apptDel = new Button();
            comboList = new ComboBox();
            calBtn = new Button();
            exitBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)apptGrid).BeginInit();
            SuspendLayout();
            // 
            // apptGrid
            // 
            apptGrid.AllowUserToAddRows = false;
            apptGrid.AllowUserToDeleteRows = false;
            apptGrid.AllowUserToOrderColumns = true;
            apptGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            apptGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            apptGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            apptGrid.Location = new Point(247, 140);
            apptGrid.Name = "apptGrid";
            apptGrid.ReadOnly = true;
            apptGrid.RowHeadersVisible = false;
            apptGrid.RowHeadersWidth = 72;
            apptGrid.Size = new Size(949, 389);
            apptGrid.TabIndex = 0;
            // 
            // calLabel
            // 
            calLabel.AutoSize = true;
            calLabel.Location = new Point(665, 47);
            calLabel.Name = "calLabel";
            calLabel.Size = new Size(95, 30);
            calLabel.TabIndex = 1;
            calLabel.Text = "Calendar";
            // 
            // reportLabel
            // 
            reportLabel.AutoSize = true;
            reportLabel.Location = new Point(56, 353);
            reportLabel.Name = "reportLabel";
            reportLabel.Size = new Size(83, 30);
            reportLabel.TabIndex = 2;
            reportLabel.Text = "Reports";
            // 
            // custLabel
            // 
            custLabel.AutoSize = true;
            custLabel.Location = new Point(42, 142);
            custLabel.Name = "custLabel";
            custLabel.Size = new Size(111, 30);
            custLabel.TabIndex = 3;
            custLabel.Text = "Customers";
            // 
            // custAdd
            // 
            custAdd.Location = new Point(33, 175);
            custAdd.Name = "custAdd";
            custAdd.Size = new Size(131, 40);
            custAdd.TabIndex = 4;
            custAdd.Text = "Add";
            custAdd.UseVisualStyleBackColor = true;
            custAdd.Click += custAdd_Click;
            // 
            // custMod
            // 
            custMod.Location = new Point(33, 221);
            custMod.Name = "custMod";
            custMod.Size = new Size(131, 40);
            custMod.TabIndex = 5;
            custMod.Text = "Modify";
            custMod.UseVisualStyleBackColor = true;
            custMod.Click += custMod_Click;
            // 
            // apptBtn
            // 
            apptBtn.Location = new Point(21, 386);
            apptBtn.Name = "apptBtn";
            apptBtn.Size = new Size(155, 40);
            apptBtn.TabIndex = 7;
            apptBtn.Text = "Appointments";
            apptBtn.UseVisualStyleBackColor = true;
            apptBtn.Click += apptBtn_Click;
            // 
            // schBtn
            // 
            schBtn.Location = new Point(33, 432);
            schBtn.Name = "schBtn";
            schBtn.Size = new Size(131, 40);
            schBtn.TabIndex = 8;
            schBtn.Text = "Schedules";
            schBtn.UseVisualStyleBackColor = true;
            schBtn.Click += schBtn_Click;
            // 
            // apptAdd
            // 
            apptAdd.Location = new Point(512, 535);
            apptAdd.Name = "apptAdd";
            apptAdd.Size = new Size(131, 40);
            apptAdd.TabIndex = 10;
            apptAdd.Text = "Add";
            apptAdd.UseVisualStyleBackColor = true;
            apptAdd.Click += apptAdd_Click;
            // 
            // apptMod
            // 
            apptMod.Location = new Point(649, 535);
            apptMod.Name = "apptMod";
            apptMod.Size = new Size(131, 40);
            apptMod.TabIndex = 11;
            apptMod.Text = "Modify";
            apptMod.UseVisualStyleBackColor = true;
            apptMod.Click += apptMod_Click;
            // 
            // custDel
            // 
            custDel.Location = new Point(33, 267);
            custDel.Name = "custDel";
            custDel.Size = new Size(131, 40);
            custDel.TabIndex = 15;
            custDel.Text = "Delete";
            custDel.UseVisualStyleBackColor = true;
            custDel.Click += custDel_Click;
            // 
            // custBtn
            // 
            custBtn.Location = new Point(33, 479);
            custBtn.Name = "custBtn";
            custBtn.Size = new Size(131, 40);
            custBtn.TabIndex = 16;
            custBtn.Text = "Customers";
            custBtn.UseVisualStyleBackColor = true;
            custBtn.Click += custBtn_Click;
            // 
            // apptDel
            // 
            apptDel.Location = new Point(786, 535);
            apptDel.Name = "apptDel";
            apptDel.Size = new Size(131, 40);
            apptDel.TabIndex = 17;
            apptDel.Text = "Delete";
            apptDel.UseVisualStyleBackColor = true;
            apptDel.Click += apptDel_Click;
            // 
            // comboList
            // 
            comboList.FormattingEnabled = true;
            comboList.Location = new Point(607, 96);
            comboList.Name = "comboList";
            comboList.Size = new Size(212, 38);
            comboList.TabIndex = 18;
            // 
            // calBtn
            // 
            calBtn.Location = new Point(996, 94);
            calBtn.Name = "calBtn";
            calBtn.Size = new Size(200, 40);
            calBtn.TabIndex = 19;
            calBtn.Text = "Calendar View";
            calBtn.UseVisualStyleBackColor = true;
            calBtn.Click += calBtn_Click;
            // 
            // exitBtn
            // 
            exitBtn.Location = new Point(1065, 630);
            exitBtn.Name = "exitBtn";
            exitBtn.Size = new Size(131, 40);
            exitBtn.TabIndex = 22;
            exitBtn.Text = "Exit";
            exitBtn.UseVisualStyleBackColor = true;
            exitBtn.Click += exitBtn_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(168F, 168F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1208, 682);
            Controls.Add(exitBtn);
            Controls.Add(calBtn);
            Controls.Add(comboList);
            Controls.Add(apptDel);
            Controls.Add(custBtn);
            Controls.Add(custDel);
            Controls.Add(apptMod);
            Controls.Add(apptAdd);
            Controls.Add(schBtn);
            Controls.Add(apptBtn);
            Controls.Add(custMod);
            Controls.Add(custAdd);
            Controls.Add(custLabel);
            Controls.Add(reportLabel);
            Controls.Add(calLabel);
            Controls.Add(apptGrid);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main Screen";
            ((System.ComponentModel.ISupportInitialize)apptGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView apptGrid;
        private Label calLabel;
        private Label reportLabel;
        private Label custLabel;
        private Button custAdd;
        private Button custMod;
        private Button apptBtn;
        private Button schBtn;
        private Button apptAdd;
        private Button apptMod;
        private Button custDel;
        private Button custBtn;
        private Button apptDel;
        private ComboBox comboList;
        private Button calBtn;
        private Button exitBtn;
    }
}