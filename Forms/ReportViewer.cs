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
    public partial class ReportViewer : Form
    {
        public ReportViewer()
        {
            InitializeComponent();
            UpdateUIWithLocalizedText();
        }

        public void SetDataSource(object dataSource)
        {
            reportGrid.DataSource = dataSource;
        }

        private void UpdateUIWithLocalizedText()
        {
            this.Text = Resources.resources.ReportViewer;
        }
    }
}
