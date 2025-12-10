using DVLD_3.MangePeople.Controls;
using DVLD_3.UserControls;
using DVLD_3.Utils;
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

namespace DVLD_3.Licenses
{
    public partial class frmPersonLicensesHistory : Form
    {
        int _personID;

        private bool _isDataUpdated;
        public bool IsDateUpdated {
            get 
            {
                return _isDataUpdated;
            }
        }
       
        public frmPersonLicensesHistory(int PersonID)
        {
            InitializeComponent();
            _personID = PersonID;
            _isDataUpdated = false;
        }

        private void PrepareLocalPublicPanelColumns()
        {
            DataTable LocalLicenses = clsLicense.GetPersonLocalLicensesHistroy(_personID);
            List<DataGridViewColumn> dataGridViewColumns = new List<DataGridViewColumn>();

            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnLicenseID", "Lic.ID", "LicenseID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnApplicationID", "App.ID", "ApplicationID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnClassName", "Class Name", "ClassName"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnIssueDate", "Issue Date", "IssueDate"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnExpirationDate", "Expiration Date", "ExpirationDate"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeCheckColumn("dataclmnIsActive", "Is Active", "IsActive"));

            publicFormsPanelLocal.AddColumnsToTheDataGridView(dataGridViewColumns);
            publicFormsPanelLocal.LinkDataToGridView(LocalLicenses);

            publicFormsPanelLocal.DataViewer.Columns[0].Width = 75;
            publicFormsPanelLocal.DataViewer.Columns[1].Width = 75;
            publicFormsPanelLocal.DataViewer.Columns[2].Width = 250;
            publicFormsPanelLocal.DataViewer.Columns[3].Width = 125;
            publicFormsPanelLocal.DataViewer.Columns[4].Width = 125;
            publicFormsPanelLocal.DataViewer.Columns[5].Width = 100;

        }

        private void PrepareInternationalPublicPanelColumns()
        {

        }
        private void PreparePublicPanels()
        {
            publicFormsPanelLocal.OpenFormButton.Visible = false;
            publicFormsPanelInternational.OpenFormButton.Visible = false;

            publicFormsPanelLocal.TargetFormToClose = this;
            publicFormsPanelInternational.TargetFormToClose = this;

            publicFormsPanelLocal.DataFilter.Visible = false;
            publicFormsPanelInternational.DataFilter.Visible = false;

            publicFormsPanelLocal.DataViewer.AutoGenerateColumns = false;
            //publicFormsPanelInternational.DataViewer.AutoGenerateColumns = false;

            publicFormsPanelLocal.DataViewer.SelectionMode=  DataGridViewSelectionMode.FullRowSelect;
            publicFormsPanelInternational.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            publicFormsPanelLocal.DataViewer.ContextMenuStrip = contextMenuStrip1;
            publicFormsPanelInternational.DataViewer.ContextMenuStrip = contextMenuStrip1;



            PrepareLocalPublicPanelColumns();
        }
        private void frmPersonLicensesHistory_Load(object sender, EventArgs e)
        {
            personDetailsWithFilter1.SearchForPerson(_personID);

            if(personDetailsWithFilter1.SelectedPerson == null)
            {
                this.Close();
                return;
            }
            personDetailsWithFilter1.OnPersonUpdated += PersonUpdated;
            personDetailsWithFilter1.FilterEnabled = false;

            PreparePublicPanels();
        }

        private void PersonUpdated()
        {
            _isDataUpdated = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(clsDataGridView.GetID_FromDataGridView(publicFormsPanelLocal.DataViewer,1));

            licenseInfo.ShowDialog();

            
        }
    }
}
