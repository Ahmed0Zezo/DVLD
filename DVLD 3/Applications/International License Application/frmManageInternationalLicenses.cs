using DVLD_3.Licenses;
using DVLD_3.MangePeople;
using DVLD_3.Properties;
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

namespace DVLD_3.Applications.International_License_Application
{
    public partial class frmManageInternationalLicenses : Form
    {
        DataTable _internationalLicenses;
        public frmManageInternationalLicenses()
        {
            InitializeComponent();
        }

        private void _refreshData()
        {
            _internationalLicenses = clsInternationalLicense.GetAll();
            publicFormsPanel1.LinkDataToGridView(_internationalLicenses);
        }

        private void frmManageInternationalLicenses_Load(object sender, EventArgs e)
        {
            _internationalLicenses = clsInternationalLicense.GetAll();

            publicFormsPanel1.OpenFormButton.BackgroundImage = Resources.New_Application_64;

            publicFormsPanel1.TargetFormToClose = this;

            publicFormsPanel1.OpenFormButton.Click += addNewInternationalLicenseAppFormOpen_Clicked;

            publicFormsPanel1.DataViewer.ContextMenuStrip = contextMenuStrip1;


            List<DataGridViewColumn> dataGridViewColumns = new List<DataGridViewColumn>();

            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnInternationalLicenseID", "Int.License ID", "InternationalLicenseID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnApplicationID", "Application ID", "ApplicationID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnDriverID", "Driver ID", "DriverID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnLocalLicenseID", "L.License ID", "IssuedUsingLocalLicenseID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnIssueDate", "Issue Date", "IssueDate"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnExpirationDate", "Expiration Date", "ExpirationDate"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeCheckColumn("dataclmnIsActive", "Is Active", "IsActive"));



            publicFormsPanel1.DataViewer.AutoGenerateColumns = false;
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            publicFormsPanel1.AddColumnsToTheDataGridView(dataGridViewColumns);
            publicFormsPanel1.LinkDataToGridView(_internationalLicenses);

            publicFormsPanel1.DataViewer.Columns[0].Width = 100;
            publicFormsPanel1.DataViewer.Columns[1].Width = 100;
            publicFormsPanel1.DataViewer.Columns[2].Width = 100;
            publicFormsPanel1.DataViewer.Columns[3].Width = 100;
            publicFormsPanel1.DataViewer.Columns[4].Width = 150;
            publicFormsPanel1.DataViewer.Columns[5].Width = 150;
            publicFormsPanel1.DataViewer.Columns[6].Width = 100;

            List<DataGridViewFilter.FilterItem> filteritems = new List<DataGridViewFilter.FilterItem>();
            filteritems.Add(new DataGridViewFilter.FilterItem("None", "None", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("International License ID", "InternationalLicenseID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Application ID", "ApplicationID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Driver ID", "DriverID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Local License ID", "IssuedUsingLocalLicenseID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Is Active", "IsActive", typeof(bool)));

            publicFormsPanel1.DataFilter.AddItemsToTheFilter(filteritems);
            publicFormsPanel1.DataFilter.FilterComboBox.SelectedIndex = 0;

        }

        private void addNewInternationalLicenseAppFormOpen_Clicked(object sender, EventArgs e)
        {
            frmAddInternationalLicenses addInternationalLicenses = new frmAddInternationalLicenses();

            addInternationalLicenses.ShowDialog();

            if(addInternationalLicenses.IsAddedSuccessfully)
            {
                _refreshData();
            }
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsApplication.FindByID(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer,1)).ApplicantPersonID;

            ShowPersonDetailsForm personDetailsForm = new ShowPersonDetailsForm(PersonID);

            personDetailsForm.ShowDialog();

            if(personDetailsForm.IsPersonDataUpdated)
            {
                _refreshData();
            }
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseInfo internationalLicenseInfo 
                = new frmInternationalLicenseInfo(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer, 0));

            internationalLicenseInfo.ShowDialog();
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsApplication.FindByID(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer, 1)).ApplicantPersonID;

            frmPersonLicensesHistory personLicensesHistory = new frmPersonLicensesHistory(PersonID);

            personLicensesHistory.ShowDialog();

            if (personLicensesHistory.IsDateUpdated)
            {
                _refreshData();
            }
        }
    }
}
