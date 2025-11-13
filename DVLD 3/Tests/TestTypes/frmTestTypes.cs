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

namespace DVLD_3.Tests.TestTypes
{
    public partial class frmTestTypes : Form
    {
        public frmTestTypes()
        {
            InitializeComponent();
        }

        DataTable _testTypesData;

        void _refresh()
        {
            _testTypesData = clsTestType.GetAllTestTypes();
            publicFormsPanel1.LinkDataToGridView(_testTypesData);
        }

        private void frmTestTypes_Load(object sender, EventArgs e)
        {
            _testTypesData = clsTestType.GetAllTestTypes();

            publicFormsPanel1.DataFilter.Visible = false;
            publicFormsPanel1.DataFilter.Enabled = false;

            publicFormsPanel1.OpenFormButton.Visible = false;
            publicFormsPanel1.OpenFormButton.Enabled = false;

            publicFormsPanel1.TargetFormToClose = this;

            List<DataGridViewColumn> dataGridViewColumns = new List<DataGridViewColumn>()
            {
                PublicFormsPanel.MakeTextBoxColumn("dataclmnTestTypeID", "Test Type ID", "TestTypeID"),
                PublicFormsPanel.MakeTextBoxColumn("dataclmnTestTypeTitle", "Test Type Title", "TestTypeTitle"),
                PublicFormsPanel.MakeTextBoxColumn("dataclmnTestTypeDescription", "Test Type Description", "TestTypeDescription"),
                PublicFormsPanel.MakeTextBoxColumn("dataclmnTestTypeFees", "Test Type Fees", "TestTypeFees")
            };

            publicFormsPanel1.AddColumnsToTheDataGridView(dataGridViewColumns);

            publicFormsPanel1.DataViewer.Columns[0].Width = 100;
            publicFormsPanel1.DataViewer.Columns[1].Width = 150;
            publicFormsPanel1.DataViewer.Columns[2].Width = 300;
            publicFormsPanel1.DataViewer.Columns[3].Width = 140;

            publicFormsPanel1.DataViewer.Height = 310;

            publicFormsPanel1.DataViewer.ContextMenuStrip = contextMenuStrip1;
            publicFormsPanel1.DataViewer.AutoGenerateColumns = false;
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (_testTypesData.Rows.Count > 0)
            {
                publicFormsPanel1.LinkDataToGridView(_testTypesData);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType updateTestType
                = new frmUpdateTestType((int)publicFormsPanel1.DataViewer.SelectedCells[0].Value);

            updateTestType.ShowDialog();

            if (updateTestType.IsDataUpdated)
            {
                _refresh();
            }
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType updateTestType = new frmUpdateTestType((int)publicFormsPanel1.DataViewer.SelectedCells[0].Value);
            updateTestType.ShowDialog();

            if (updateTestType.IsDataUpdated)
            {
                _refresh();
            }
        }
    }
}
