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
    public partial class frmIssueNewLicenseForFirstTime : Form
    {
        int _localAppID;

        public bool IsSavedSuccessfully;
       
        public frmIssueNewLicenseForFirstTime(int LocalAppID)
        {
            InitializeComponent();

            _localAppID = LocalAppID;
            IsSavedSuccessfully = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void frmIssueNewLicenseForFirstTime_Load(object sender, EventArgs e)
        {
            ctrlLDApplicationInfo1.LoadApplicationInfo(_localAppID);

            if (ctrlLDApplicationInfo1.LocalApplication == null)
            {
                this.Close();
                return;
            }

        }


        private void btnIssue_Click(object sender, EventArgs e)
        {
            int DriverID = -1;

            if (!clsDriver.IsDriverExistByPersonID(ctrlLDApplicationInfo1.LocalApplication.Application.ApplicantPersonID))
            {
                //if there is no driver for that person create one

                clsDriver newDriver = new clsDriver();
                newDriver.PersonID = ctrlLDApplicationInfo1.LocalApplication.Application.ApplicantPersonID;
                newDriver.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;
                newDriver.CreatedDate = DateTime.Now;

                if (!newDriver.AddNew())
                {
                    MessageBox.Show("There is an occured error during creating a driver!\nContact the developer", "Error", MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                    IsSavedSuccessfully = false;
                    this.Close();
                    return;
                }

                DriverID = newDriver.DriverID;
            }
            else
            {
                DriverID = clsDriver.GetDriverIDByPersonID(ctrlLDApplicationInfo1.LocalApplication.Application.ApplicantPersonID);
            }

            clsLicense newLicense = new clsLicense(ctrlLDApplicationInfo1.LocalApplication.LicenseClassID);

            if (newLicense.LicenseClass == null)
            {
                MessageBox.Show("Wrong License Class ID\nContact the developer", "Error", MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                IsSavedSuccessfully = false;
                this.Close();
                return;
            }

            newLicense.ApplicationID = ctrlLDApplicationInfo1.LocalApplication.ApplicationID;
            newLicense.DriverID = DriverID;
            newLicense.Notes = txtNotes.Text;
            newLicense.IsActive = true;
            newLicense.IssueReason = 1; // First Time
            newLicense.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;

            if (newLicense.Issue())
            {
                ctrlLDApplicationInfo1.LocalApplication.Application.CompleteApplication();
                MessageBox.Show($"License Issued Successfully With ID \"{newLicense.LicenseID}\"", "Issue License", MessageBoxButtons.OK
                       , MessageBoxIcon.Information);
                IsSavedSuccessfully = true;

            }
            else
            {
                MessageBox.Show("Something Went Wrong During Issueing The License\nContact the developer", "Error", MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                IsSavedSuccessfully = false;
                clsDriver.DeleteDriverByID(DriverID);
            }

            this.Close();
            return;
        }

    }
}
