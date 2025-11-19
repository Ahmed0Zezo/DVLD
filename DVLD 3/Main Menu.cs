using DVLD_3.Applications;
using DVLD_3.Applications.LocalDrivingLicenseApplication;
using DVLD_3.Drivers;
using DVLD_3.Tests.TestTypes;
using DVLD_3.Users;
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

namespace DVLD_3
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PeopleMainMenu PeopleForm = new PeopleMainMenu();

            PeopleForm.ShowDialog();
        }

        private void DriversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriversDetails driversDetails= new frmDriversDetails();

            driversDetails.ShowDialog();
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Users frmManageUsers = new Manage_Users();

            frmManageUsers.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails userDetails = new frmUserDetails(clsGlobalInformations.CurrentLoggedUserID);

            userDetails.ShowDialog();
        }

       
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword(clsGlobalInformations.CurrentLoggedUserID);

           

            changePassword.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure that you wanna sign out ?","Sign Out Confirm"
                ,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsGlobalInformations.CurrentLoggedUserID = -1;

                this.Close();
            }

            
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationTypes frmApplicationTypes = new frmApplicationTypes();

            frmApplicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestTypes testTypes = new frmTestTypes();

            testTypes.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageLocalApplications manageLocalApplications = new ManageLocalApplications();

            manageLocalApplications.ShowDialog();
        }

        private void newLocalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditLocalDrivingLicenseApplication addEditLocalDrivingLicenseApplication
                = new AddEditLocalDrivingLicenseApplication();

            addEditLocalDrivingLicenseApplication.ShowDialog();
        }
    }
}
