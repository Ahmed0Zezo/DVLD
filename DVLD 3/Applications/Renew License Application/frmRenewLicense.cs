using DVLD_3.Licenses;
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

namespace DVLD_3.Applications.Renew_License_Application
{
    public partial class frmRenewLicense : Form
    {
        decimal _applicationFees;

        clsLicenseReplacementResultsInfo renewInfo;
        public frmRenewLicense()
        {
            InitializeComponent();
        }

        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Today.ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblCreatedBy.Text = clsUser.FindUserByID(clsGlobalInformations.CurrentLoggedUserID).UserName;
            _applicationFees = clsApplicationType.FindApplicationTypeByID(2).ApplicationFees;
            lblApplicationFees.Text = _applicationFees.ToString();
            btnRenew.Enabled = false;
            lnklblShowNewLicenseInfo.Enabled = false;
            lnklblShowPersonLicensesHistory.Enabled = false;
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += LicenseSelected;
        }
        private void LicenseSelected(int LicenseID)
        {
            lblOldLicenseID.Text = LicenseID.ToString();

            if (LicenseID == -1)
            {
                _resetToDefaultData();
                btnRenew.Enabled = false;
                lnklblShowPersonLicensesHistory.Enabled = false;
                return;
            }

            _loadRenewAppData();
            btnRenew.Enabled = true;
            lnklblShowPersonLicensesHistory.Enabled = true;
        }

        private void _resetToDefaultData()
        {
            lblExpirationDate.Text = "???";
            lblLicenseFees.Text = "???";
            lblTotalFees.Text = "???"; 
        }

        private void _loadRenewAppData()
        {
            lblExpirationDate.Text = DateTime.Today.AddYears(ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseClass.DefaultValidityLength).ToShortDateString();
            lblLicenseFees.Text = ctrlLicenseInfoWithFilter1.SelectedLicense.PaidFees.ToString();
            lblTotalFees.Text = $"{ctrlLicenseInfoWithFilter1.SelectedLicense.PaidFees + _applicationFees}";
        }

        private void _loadNewApplicationAndLicenseData()
        {
            lblRenewApplicationID.Text = renewInfo.NewApplication.ApplicationID.ToString();
            lblRenewdLicenseID.Text = renewInfo.NewLicense.LicenseID.ToString();
            lblExpirationDate.Text = renewInfo.NewLicense.ExpirationDate.ToShortDateString();
        }
       

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to renew this license ?"
                , "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;


            renewInfo = clsLicense.RenewLicense(ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID
                , clsPerson.GetPersonID_ByNationalNo(ctrlLicenseInfoWithFilter1.NationalNo), txtNotes.Text);

            if(!renewInfo.Status)
            {
                string ErrorMessage = "";
                switch (renewInfo.FaildReason)
                {
                    case clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseDoesNotExist:
                        ErrorMessage = "Old License ID Is Not Exist!";
                        break;
                    case clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseNotExpired:
                        ErrorMessage = "Old License Is Not Expired Yet!";
                        break;
                    case clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToDeActivateOldLicense:
                        ErrorMessage = "Something Went Wrong During Deactivating The Old License!";
                        break;
                    case clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToAddReplacementApplication:
                        ErrorMessage = "Something Went Wrong During Creating Renew License Application";
                        break;
                    case clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToCreateNewLicense:
                        ErrorMessage = "Something Went Wrong During Creating The New License";
                        break;
                    case clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseIsNotActive
:
                        ErrorMessage = "Old License Is Not Active";
                        break;
                    default :
                        ErrorMessage = "An Error Occured!";
                        break;
                }

                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _loadNewApplicationAndLicenseData();
            MessageBox.Show($"License Renewd Successfully With ID ({renewInfo.NewLicense.LicenseID})"
                , "Succedded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlLicenseInfoWithFilter1.FilterEnable = false;
            btnRenew.Enabled = false;
            lnklblShowNewLicenseInfo.Enabled = true;
        }

        private void lnklblShowPersonLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonLicensesHistory showPersonLicensesHistory 
                = new frmPersonLicensesHistory(clsPerson.GetPersonID_ByNationalNo(ctrlLicenseInfoWithFilter1.NationalNo));

            showPersonLicensesHistory.ShowDialog();

        }

        private void lnklblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(renewInfo.NewApplication.ApplicationID);

            frmLicenseInfo.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

