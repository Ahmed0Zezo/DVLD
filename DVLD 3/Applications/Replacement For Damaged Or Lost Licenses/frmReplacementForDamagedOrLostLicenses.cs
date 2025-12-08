using DVLD_3.Applications.Controls;
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

namespace DVLD_3.Applications.Replacement_For_Damaged_Or_Lost_Licenses
{
    public partial class frmReplacementForDamagedOrLostLicenses : Form
    {
        int _applicatoinTypeID;
        decimal _replacementForLostLicenseApplicationFees;
        decimal _replacementForDamagedLicenseApplicationFees;

        int _applicantPersonID;

        clsLicenseReplacementResultsInfo replacementInfo;
        public frmReplacementForDamagedOrLostLicenses()
        {
            InitializeComponent();
            _replacementForLostLicenseApplicationFees 
                = clsApplicationType.FindApplicationTypeByID(3).ApplicationFees; //3 => Re For Lost L App
            _replacementForDamagedLicenseApplicationFees
                = clsApplicationType.FindApplicationTypeByID(4).ApplicationFees; //4 => Re For Damaged L App
        }

        private void frmReplacementForDamagedOrLostLicenses_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += _licenseSelected;
            lnklblShowPersonLicensesHistory.Enabled = false;
            lnklblShowNewLicenseInfo.Enabled = false;

            rbtnForDamaged.Checked = true;
            lblApplicationDate.Text = DateTime.Today.ToShortDateString();
            lblCreatedBy.Text = clsUser.FindUserByID(clsGlobalInformations.CurrentLoggedUserID).UserName;
        }


        private void _licenseSelected(int LicenseID)
        {
            lblOldLicenseID.Text = LicenseID.ToString();
            lnklblShowPersonLicensesHistory.Enabled = LicenseID != -1;
            _applicantPersonID = clsPerson.GetPersonID_ByNationalNo(ctrlLicenseInfoWithFilter1.NationalNo);
        }

        
        private void _applicationTypeChanged()
        {
            clsApplicationType.enApplicationTypes ApplicationType = (clsApplicationType.enApplicationTypes)_applicatoinTypeID;

            switch (ApplicationType)
            {

                case clsApplicationType.enApplicationTypes.ReplacementForDamagedLicense:
                    lblHeader.Text = "Replacement For Damaged";
                    lblApplicationFees.Text = _replacementForDamagedLicenseApplicationFees.ToString();
                    break;
                case clsApplicationType.enApplicationTypes.ReplacementForLostLicense:
                    lblHeader.Text = "Replacement For Lost";
                    lblApplicationFees.Text = _replacementForLostLicenseApplicationFees.ToString();
                    break;
                default:
                    MessageBox.Show("Wrong Application Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnForDamaged_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnForDamaged.Checked)
                _applicatoinTypeID = 4;
            else if (rbtnForLost.Checked)
                _applicatoinTypeID = 3;
            _applicationTypeChanged();
        }

        private void lnklblShowPersonLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonLicensesHistory frmPersonLicensesHistory
                = new frmPersonLicensesHistory(_applicantPersonID);

            frmPersonLicensesHistory.ShowDialog();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to replace this license ?"
                , "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            replacementInfo = clsLicense.ReplacementForDamagedOrLost
                (ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID, _applicantPersonID,_applicatoinTypeID);

            if(!replacementInfo.Status)
            {
                string ErrorMessage = "";
                switch (replacementInfo.FaildReason)
                {
                    case clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseDoesNotExist:
                        ErrorMessage = "Old License ID Is Not Exist!";
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
                    case clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseIsNotActive:
                        ErrorMessage = "Old License Is Not Active";
                        break;
                    default:
                        ErrorMessage = "An Error Occured!";
                        break;
                }

                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _loadNewApplicationAndLicenseData();
            MessageBox.Show($"License Renewd Successfully With ID ({replacementInfo.NewLicense.LicenseID})"
                , "Succedded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlLicenseInfoWithFilter1.FilterEnable = false;
            btnIssueReplacement.Enabled = false;
            gbRepleacementFor.Enabled = false;
            lnklblShowNewLicenseInfo.Enabled = true;
        }

        private void _loadNewApplicationAndLicenseData()
        {
            lblReplacementApplicationID.Text = replacementInfo.NewApplication.ApplicationID.ToString();
            lblReplacedLicenseID.Text = replacementInfo.NewLicense.LicenseID.ToString();
        }

        private void lnklblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo LicenseInfoForm = new frmLicenseInfo(replacementInfo.NewLicense.ApplicationID);

            LicenseInfoForm.ShowDialog();
        }
    }
}
