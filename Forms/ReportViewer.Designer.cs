namespace C969_WGU_TallisJordan.Forms
{
    partial class ReportViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportViewer));
            reportGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)reportGrid).BeginInit();
            SuspendLayout();
            // 
            // reportGrid
            // 
            reportGrid.AllowUserToAddRows = false;
            reportGrid.AllowUserToDeleteRows = false;
            reportGrid.AllowUserToOrderColumns = true;
            reportGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            reportGrid.Location = new Point(12, 12);
            reportGrid.Name = "reportGrid";
            reportGrid.RowHeadersWidth = 72;
            reportGrid.Size = new Size(776, 426);
            reportGrid.TabIndex = 0;
            // 
            // ReportViewer
            // 
            AutoScaleDimensions = new SizeF(168F, 168F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(800, 450);
            Controls.Add(reportGrid);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ReportViewer";
            Text = "Report Viewer";
            ((System.ComponentModel.ISupportInitialize)reportGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView reportGrid;
    }
}