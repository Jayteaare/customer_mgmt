namespace C969_WGU_TallisJordan.Forms
{
    partial class Calendar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calendar));
            calBox = new MonthCalendar();
            SuspendLayout();
            // 
            // calBox
            // 
            calBox.Dock = DockStyle.Fill;
            calBox.Location = new Point(0, 0);
            calBox.MaxSelectionCount = 1;
            calBox.Name = "calBox";
            calBox.TabIndex = 0;
            // 
            // Calendar
            // 
            AutoScaleDimensions = new SizeF(168F, 168F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(356, 300);
            Controls.Add(calBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Calendar";
            ResumeLayout(false);
        }

        #endregion

        private MonthCalendar calBox;
    }
}