using DVLD_3.Properties;
using DVLD_3.Test_Appointments;
using DVLD_3.UserControls;
using DVLD_3.Utils;
using DVLD_BusienessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.Applications.LocalDrivingLicenseApplication
{
    public partial class ManageLocalApplications : Form
    {

        DataTable _localDrvingLicenseApplications;
        public ManageLocalApplications()
        {
            InitializeComponent();
        }

        private void _refresh()
        {
            _localDrvingLicenseApplications = clsLocalApp.GetAll();
            publicFormsPanel1.LinkDataToGridView(_localDrvingLicenseApplications);
        }

        private void ManageApplications_Load(object sender, EventArgs e)
        {
            _localDrvingLicenseApplications = clsLocalApp.GetAll();

            publicFormsPanel1.TargetFormToClose = this;

            publicFormsPanel1.OpenFormButton.Click += addNewLocalAppFormOpen_Click;

            publicFormsPanel1.DataViewer.ContextMenuStrip = contextMenuStrip1;


            List<DataGridViewColumn> dataGridViewColumns = new List<DataGridViewColumn>();

            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnLDLAppID", "L.D.L.AppID", "LocalDrivingLicenseApplicationID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnClassName", "Class Name", "ClassName"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnNationalNo", "National No", "NationalNo"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnFullName", "Full Name", "FullName"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnApplicationDate", "Application Date", "ApplicationDate"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnPassedTests", "Passed Tests", "PassedTestCount"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnStatus", "Status", "Status"));

            

            publicFormsPanel1.DataViewer.AutoGenerateColumns = false;
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            publicFormsPanel1.AddColumnsToTheDataGridView(dataGridViewColumns);
            publicFormsPanel1.LinkDataToGridView(_localDrvingLicenseApplications);

            publicFormsPanel1.DataViewer.Columns[0].Width = 100;
            publicFormsPanel1.DataViewer.Columns[1].Width = 200;
            publicFormsPanel1.DataViewer.Columns[2].Width = 100;
            publicFormsPanel1.DataViewer.Columns[3].Width = 250;
            publicFormsPanel1.DataViewer.Columns[4].Width = 200;
            publicFormsPanel1.DataViewer.Columns[5].Width = 100;
            publicFormsPanel1.DataViewer.Columns[6].Width = 95;

            publicFormsPanel1.OpenFormButton.BackgroundImage = Resources.New_Application_64;
            
            List<DataGridViewFilter.FilterItem> filteritems = new List<DataGridViewFilter.FilterItem>();
            filteritems.Add(new DataGridViewFilter.FilterItem("None", "None", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("L.D.L.AppID", "LocalDrivingLicenseApplicationID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("National No", "NationalNo", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Full Name", "FullName", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Status", "Status", typeof(string)));
            
            publicFormsPanel1.DataFilter.AddItemsToTheFilter(filteritems);
            publicFormsPanel1.DataFilter.FilterComboBox.SelectedIndex = 0;

           
        }

        private void addNewLocalAppFormOpen_Click(object sender , EventArgs e)
        {
            AddEditLocalDrivingLicenseApplication _addEditLocalDrivingLicenseApplication = new AddEditLocalDrivingLicenseApplication();

            _addEditLocalDrivingLicenseApplication.ShowDialog();
        }

        private void updateApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditLocalDrivingLicenseApplication addEditLocalDrivingLicenseApplication
                = new AddEditLocalDrivingLicenseApplication((int)publicFormsPanel1.DataViewer.SelectedCells[0].Value);

            addEditLocalDrivingLicenseApplication.ApplicationSavedSuccessfully += applicationSaved;

            addEditLocalDrivingLicenseApplication.ShowDialog();
        }

        private void applicationSaved(int AppID)
        {
            _refresh();
        }

        private void showApplicationDetilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLocalAppInfos localAppDetails = new frmShowLocalAppInfos
                (clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer,0));

            localAppDetails.ShowDialog();

            if (localAppDetails.IsPersonDataUpdated)
            {
                _refresh();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool IsApplicationNew 
                = (clsLocalApp.GetAppStatusByLocalAppID(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer, 0)) == 1);

            updateApplicationToolStripMenuItem.Enabled = IsApplicationNew;
            deleteApplicationToolStripMenuItem.Enabled = IsApplicationNew;
            cancelApplicationToolStripMenuItem.Enabled = IsApplicationNew;
            sceduleTestToolStripMenuItem.Enabled = IsApplicationNew;

            if(IsApplicationNew)
            {

                clsTestType WhatTestTypeIDToTake = clsTestType.WhatTestTypeToTakeByLocalAppID
                (clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer, 0));

                if(WhatTestTypeIDToTake != null)
                {
                    sceduleVisionTestToolStripMenuItem.Enabled = WhatTestTypeIDToTake.TestTypeID == 1;
                    sceduleWrittenTestToolStripMenuItem.Enabled = WhatTestTypeIDToTake.TestTypeID == 2;
                    sceduleStreetTestToolStripMenuItem.Enabled = WhatTestTypeIDToTake.TestTypeID == 3;
                }
                else
                {
                    sceduleVisionTestToolStripMenuItem.Enabled = false;
                    sceduleWrittenTestToolStripMenuItem.Enabled = false;
                    sceduleStreetTestToolStripMenuItem.Enabled = false;
                }
                
            }
            
            //issureDrivingLicenseFirstTimeToolStripMenuItem.Enabled = IsApplicationNew;
            //showLicenseToolStripMenuItem.Enabled = IsApplicationNew;
            //showPersonLicensesHistoryToolStripMenuItem.Enabled = IsApplicationNew;
        }

        private void sceduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTestAppointments frmManageApplicationTestAppointments
                = new frmManageApplicationTestAppointments(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer,0),1);

            frmManageApplicationTestAppointments.ShowDialog();
        }

        private void sceduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTestAppointments frmManageApplicationTestAppointments
                = new frmManageApplicationTestAppointments(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer, 0), 2);

            frmManageApplicationTestAppointments.ShowDialog();
        }

        private void sceduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTestAppointments frmManageApplicationTestAppointments
                = new frmManageApplicationTestAppointments(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer, 0), 3);

            frmManageApplicationTestAppointments.ShowDialog();
        }
    }
}
