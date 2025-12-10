using DVLD_3.Applications.Release_Detained_Licenses;
using DVLD_3.MangePeople;
using DVLD_3.Properties;
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

namespace DVLD_3.Licenses
{
    public partial class frmManageDetainedLicenses : Form
    {

        DataTable _detainedLicenses;
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _refreshData()
        {
            _detainedLicenses = clsDetaineLicenseInfo.GetAllDetainedLicenses();
            publicFormsPanel1.LinkDataToGridView(_detainedLicenses);
        }

        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _detainedLicenses = clsDetaineLicenseInfo.GetAllDetainedLicenses();

            
            btnOpenReleaseForm.Size = publicFormsPanel1.OpenFormButton.Size;
            publicFormsPanel1.OpenFormButton.BackgroundImage = Resources.Detain_64;
            publicFormsPanel1.DataViewer.Height = 250;
            publicFormsPanel1.TargetFormToClose = this;
            publicFormsPanel1.OpenFormButton.Click += _openDetainLicenseFormButton_Clicked;
            publicFormsPanel1.DataViewer.ContextMenuStrip = contextMenuStrip1;

            List<DataGridViewColumn> dataGridViewColumns = new List<DataGridViewColumn>();

            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnDetainedID", "D.ID", "DetainID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnLicenseID", "L.ID", "LicenseID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnDetainDate", "D.Date", "DetainDate"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeCheckColumn("dataclmnIsReleased", "Is Released", "IsReleased"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnFineFees", "Fine Fees", "FineFees"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnReleaseDate", "Release Date", "ReleaseDate"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnNationalNo", "N.No", "NationalNo"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnFullName", "Full Name", "FullName"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnReleaseApplicationID", "Release App ID", "ReleaseApplicationID"));

            publicFormsPanel1.DataViewer.AutoGenerateColumns = false;
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            publicFormsPanel1.AddColumnsToTheDataGridView(dataGridViewColumns);
            publicFormsPanel1.LinkDataToGridView(_detainedLicenses);

            publicFormsPanel1.DataViewer.Columns[0].Width = 75;
            publicFormsPanel1.DataViewer.Columns[1].Width = 75;
            publicFormsPanel1.DataViewer.Columns[2].Width = 150;
            publicFormsPanel1.DataViewer.Columns[3].Width = 75;
            publicFormsPanel1.DataViewer.Columns[4].Width = 100;
            publicFormsPanel1.DataViewer.Columns[5].Width = 150;
            publicFormsPanel1.DataViewer.Columns[6].Width = 50;
            publicFormsPanel1.DataViewer.Columns[7].Width = 200;
            publicFormsPanel1.DataViewer.Columns[8].Width = 125;


            List<DataGridViewFilter.FilterItem> filteritems = new List<DataGridViewFilter.FilterItem>();
            filteritems.Add(new DataGridViewFilter.FilterItem("None", "None", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Detain ID", "DetainID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Is Released", "IsReleased", typeof(bool)));
            filteritems.Add(new DataGridViewFilter.FilterItem("National No", "NationalNo", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Full Name", "FullName", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Release Application ID", "ReleaseApplicationID", typeof(int)));

            publicFormsPanel1.DataFilter.AddItemsToTheFilter(filteritems);
            publicFormsPanel1.DataFilter.FilterComboBox.SelectedIndex = 0;


        }

        private void _openDetainLicenseFormButton_Clicked(object sender, EventArgs e)
        {
            frmDetainLicense detainLicense = new frmDetainLicense();

            detainLicense.ShowDialog();

            if(detainLicense.IsLicenseDetained)
            {
                _refreshData();
            }
        }

        private void btnOpenReleaseForm_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenses releaseDetainedLicenses = new frmReleaseDetainedLicenses();

            releaseDetainedLicenses.ShowDialog();

            if(releaseDetainedLicenses.IsLicenseReleased)
            {
                _refreshData();
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string SelectedNationalNumber = publicFormsPanel1.DataViewer.SelectedCells[6].Value.ToString();

            ShowPersonDetailsForm showPersonDetailsForm = new ShowPersonDetailsForm(clsPerson.GetPersonID_ByNationalNo(SelectedNationalNumber));

            showPersonDetailsForm.ShowDialog();

            if(showPersonDetailsForm.IsPersonDataUpdated)
            {
                _refreshData();
            }

        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)publicFormsPanel1.DataViewer.SelectedCells[1].Value;
            frmLicenseInfo licenseInfo = new frmLicenseInfo(LicenseID,true);

            licenseInfo.ShowDialog();

        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string SelectedNationalNumber = publicFormsPanel1.DataViewer.SelectedCells[6].Value.ToString();

            frmPersonLicensesHistory personLicensesHistory
                = new frmPersonLicensesHistory(clsPerson.GetPersonID_ByNationalNo(SelectedNationalNumber));

            personLicensesHistory.ShowDialog();

            if(personLicensesHistory.IsDateUpdated)
            {
                _refreshData();
            }
            
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)publicFormsPanel1.DataViewer.SelectedCells[1].Value;
            frmReleaseDetainedLicenses releaseDetainedLicenses = new frmReleaseDetainedLicenses(LicenseID);

            releaseDetainedLicenses.ShowDialog();
            
            if(releaseDetainedLicenses.IsLicenseReleased)
            {
                _refreshData();
            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int DetainID = (int)publicFormsPanel1.DataViewer.SelectedCells[0].Value;

            releaseDetainedLicenseToolStripMenuItem.Enabled = !(clsDetaineLicenseInfo.FindByDetainID(DetainID).IsReleased);
        }
    }
}
