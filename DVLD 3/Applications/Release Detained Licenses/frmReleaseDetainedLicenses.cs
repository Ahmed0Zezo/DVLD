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

namespace DVLD_3.Applications.Release_Detained_Licenses
{
    public partial class frmReleaseDetainedLicenses : Form
    {
        private clsDetaineLicenseInfo _detainInfo;

        private decimal _appFees;
        public frmReleaseDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _resetBasicDetainInfoToDefaultValues()
        {
            lblDetainID.Text = "???";
            lblLicenseID.Text = "???";
            lblDetainDate.Text = "???";
            lblCreatedBy.Text = "???";
            lblFineFees.Text = "???";
            lblTotalFees.Text = "???";

        }

        private void _fillDetainComponentsByData()
        {
            lblDetainID.Text = _detainInfo.DetainID.ToString();
            lblLicenseID.Text = _detainInfo.LicenseID.ToString();
            lblDetainDate.Text = _detainInfo.DetainDate.ToShortDateString();
            lblCreatedBy.Text = (clsUser.FindUserByID(_detainInfo.CreatedByUserID)).UserName;
            lblFineFees.Text = _detainInfo.FineFees.ToString();
            lblApplicationFees.Text = $"{_appFees}";
            lblTotalFees.Text = $"{_appFees + _detainInfo.FineFees}";
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            lnklblShowPersonLicensesHistory.Enabled = obj != -1;

            if(obj == -1)
            {
                _resetBasicDetainInfoToDefaultValues();
                btnReleaseLicense.Enabled = false;
                return;
            }

            _detainInfo = clsDetaineLicenseInfo.FindNonReleasedDetainedLicenseInfoByLicenseID(obj);

            if (_detainInfo == null)
            {
                MessageBox.Show("License Is Not Detained!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnReleaseLicense.Enabled = false;
                return;
            }


            _fillDetainComponentsByData();
            btnReleaseLicense.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleaseDetainedLicenses_Load(object sender, EventArgs e)
        {
            _appFees = clsApplicationType.FindApplicationTypeByID(5).ApplicationFees; // release License Application ID = 5

            lblApplicationFees.Text = _appFees.ToString();

            btnReleaseLicense.Enabled = false;

        }

        private void lnklblShowPersonLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonLicensesHistory personLicensesHistory
                = new frmPersonLicensesHistory(clsPerson.GetPersonID_ByNationalNo(ctrlLicenseInfoWithFilter1.NationalNo));

            personLicensesHistory.ShowDialog();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to release this license ?", "Confirm", MessageBoxButtons.OKCancel
               , MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            clsReleaseLicenseResultInfo releaseResult 
                = _detainInfo.Release(clsPerson.GetPersonID_ByNationalNo(ctrlLicenseInfoWithFilter1.NationalNo));

            if(releaseResult.Status == false)
            {
                string ErrorMessage = "";
                switch (releaseResult.FaildReason)
                {
                    case clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.LicenseIsNotExit:
                        ErrorMessage = $"License With ID {ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID} is Not Found!";
                        break;
                    case clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.LicenseIsNotActive:
                        ErrorMessage = $"License With ID {ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID} is Not Active!";
                        break;
                    case clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.LicenseIsNotDetained:
                        ErrorMessage = $"License With ID {ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID} is Not Detained!";
                        break;
                    case clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.DetainInfoNotExit:
                        ErrorMessage = $"Datain Info is not exist!";
                        break;
                    case clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.FaildToAddReleaseApplication:
                        ErrorMessage = $"Someting went wrong during creating [Release Detained License Application]!";
                        break;
                    case clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.FaildToUpdateReleaseData:
                        ErrorMessage = $"Someting went wrong during releasing the license!";
                        break;
                    default:
                        ErrorMessage = $"An Error Occured";
                        break;

                }
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"License Released Successfully Release Application ID ({releaseResult.ReleaseApplication.ApplicantPersonID})"
                , "Succedded"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlLicenseInfoWithFilter1.FilterEnable = false;
            btnReleaseLicense.Enabled = false;
            lnklblShowNewLicenseInfo.Enabled = true;
            lblApplicationID.Text = releaseResult.ReleaseApplication.ApplicantPersonID.ToString();

        }

        private void lnklblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(ctrlLicenseInfoWithFilter1.SelectedLicense.ApplicationID);

            licenseInfo.ShowDialog();
        }
    }
}
