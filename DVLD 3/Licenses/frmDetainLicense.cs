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
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void txtFineFees_TextChanged(object sender, EventArgs e)
        {
            clsTextBoxUtil.MakeTextBoxesHaveOnlyMoneyCharacters((TextBox)sender);
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            clsTextBoxUtil.LinkTextBoxWithErrorProvider((TextBox)sender, "This filed is required"
                , string.IsNullOrEmpty(((TextBox)sender).Text), errorProvider1, e);
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to detain this license ?", "Confirm", MessageBoxButtons.OKCancel
                , MessageBoxIcon.Question) == DialogResult.Cancel)
                return;


            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some data is required put the mouse on the red icon",
                    "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            clsLicenseDetainResultsInfo detainResult = clsLicense.DetainLicenseByID(ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID,
                Convert.ToDecimal(txtFineFees.Text));

            if(!detainResult.Status)
            {
                string ErrorMessage = "";
                switch (detainResult.FaildReason)
                {
                    case clsLicenseDetainResultsInfo.LicenseDetainFaildReason.LicenseIsNotFound:
                        ErrorMessage = $"License With ID {ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID} is Not Found!";
                        break;
                    case clsLicenseDetainResultsInfo.LicenseDetainFaildReason.LicenseIsNotActive:
                        ErrorMessage = $"License With ID {ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID} is Not Active!";
                        break;
                    case clsLicenseDetainResultsInfo.LicenseDetainFaildReason.LicenseAlreadyDetained:
                        ErrorMessage = $"License With ID {ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID} is Already Detained!";
                        break;
                    case clsLicenseDetainResultsInfo.LicenseDetainFaildReason.FaildToCreateDetainRecord:
                        ErrorMessage = $"Someting went wrong during creating [Detain Record]!";
                        break;
                    default:
                        ErrorMessage = $"An Error Occured";
                        break;

                }
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"License Detained Successfully With Detain ID ({detainResult.DetaineRecord.DetainID})", "Succedded"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlLicenseInfoWithFilter1.FilterEnable = false;
            btnDetainLicense.Enabled = false;
            lnklblShowNewLicenseInfo.Enabled = true;
            txtFineFees.Enabled = false;

            lblDetainID.Text = detainResult.DetaineRecord.DetainID.ToString();            
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobalInformations.CurrentLoggedUserID.ToString();
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            lblLicenseID.Text = obj.ToString();
            lnklblShowPersonLicensesHistory.Enabled = obj != -1;
        }

        private void lnklblShowPersonLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonLicensesHistory PersonLicensesHistory 
                = new frmPersonLicensesHistory(clsPerson.GetPersonID_ByNationalNo(ctrlLicenseInfoWithFilter1.NationalNo));

            PersonLicensesHistory.ShowDialog();
        }

        private void lnklblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(ctrlLicenseInfoWithFilter1.SelectedLicense.ApplicationID);

            licenseInfo.ShowDialog();
        }
    }
}
