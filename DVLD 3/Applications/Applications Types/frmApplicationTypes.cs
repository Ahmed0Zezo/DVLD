using DVLD_3.Applications.Applications_Types;
using DVLD_3.UserControls;
using DVLD_BusienessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.Applications
{
    public partial class frmApplicationTypes : Form
    {
        public frmApplicationTypes()
        {
            InitializeComponent();
        }

        DataTable _applicationTypesData ;

        void _refresh ()
        {
            _applicationTypesData = clsApplicationType.GetAllApplicationTypes();

            publicFormsPanel1.LinkDataToGridView(_applicationTypesData); 
        }
        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {
            _applicationTypesData = clsApplicationType.GetAllApplicationTypes();

            publicFormsPanel1.DataFilter.Visible = false;
            publicFormsPanel1.DataFilter.Enabled = false;

            publicFormsPanel1.OpenFormButton.Visible = false;
            publicFormsPanel1.OpenFormButton.Enabled = false;

            publicFormsPanel1.TargetFormToClose = this;

            List<DataGridViewColumn> dataGridViewColumns = new List<DataGridViewColumn>()
            {
                PublicFormsPanel.MakeTextBoxColumn("dataclmnApplicationTypeID", "Application Type ID", "ApplicationTypeID"),
                PublicFormsPanel.MakeTextBoxColumn("dataclmnApplicationTypeTitle", "Application Type Title", "ApplicationTypeTitle"),
                PublicFormsPanel.MakeTextBoxColumn("dataclmnApplicationFees", "Application Fees", "ApplicationFees")
            };

            publicFormsPanel1.AddColumnsToTheDataGridView(dataGridViewColumns);

            publicFormsPanel1.DataViewer.Columns[0].Width = 100;
            publicFormsPanel1.DataViewer.Columns[1].Width = 300;
            publicFormsPanel1.DataViewer.Columns[2].Width = 160;

            publicFormsPanel1.DataViewer.Height = 310;

            publicFormsPanel1.DataViewer.ContextMenuStrip = contextMenuStrip1;
            publicFormsPanel1.DataViewer.AutoGenerateColumns = false;
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (_applicationTypesData.Rows.Count > 0)
            {
                publicFormsPanel1.LinkDataToGridView(_applicationTypesData);
            }
            


        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationTypes updateApplicationTypes 
                = new frmUpdateApplicationTypes((int)publicFormsPanel1.DataViewer.SelectedCells[0].Value);

            updateApplicationTypes.ShowDialog();

            if(updateApplicationTypes.IsDataUpdated)
            {
                _refresh();
            }
        }
    }
}
