using DVLD_3.Licenses;
using DVLD_BusienessLayer;
using DVLD_BusienessLayer.Result_Classes;
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
    public partial class frmAddInternationalLicenses : Form
    {
        private bool _isAddedSuccessfully;

        public bool IsAddedSuccessfully 
        {
            get
            {
                return _isAddedSuccessfully;
            }
        }
        public frmAddInternationalLicenses()
        {
            InitializeComponent();
            _isAddedSuccessfully = false;
        }

        
        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            lblLocalLicenseID.Text = obj.ToString();

            btnIssueInternationalLicense.Enabled = obj != -1;

            lnklblShowPersonLicensesHistory.Enabled = obj != -1;
        }

        private void btnIssueInternationalLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to make new international license ?"
                , "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;


            clsAddInternationalLicenseResultInfo AddResult
                = clsInternationalLicense.AddNewInternationalLicense(ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseID,
                clsPerson.GetPersonID_ByNationalNo(ctrlLicenseInfoWithFilter1.NationalNo));

            if(AddResult.Status == false)
            {
                string ErrorMessage = "";
                switch (AddResult.FaildReason)
                {
                    case clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.LocalLicesesIsNotExist:
                        ErrorMessage = "Local License ID Is Not Exist!";
                        break;
                    case clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.LocalLicenseIsNotActive:
                        ErrorMessage = "Local License Is Not Active";
                        break;
                    case clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.LocalLicenseClassIsNotOrdinary:
                        ErrorMessage = "Local License Class must be with ID 3 ,Choose another local licnse";
                        break;
                    case clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.ThereAreActiveInternatinoalLicenseOnThisLicenseAlready:
                        ErrorMessage = "Local License Has An Active Issued International License Already!";
                        break;
                    case clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.FaildToAddNewApplication:
                        ErrorMessage = "Something Went Wrong During Creating The Application";
                        break;
                    case clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.FaildToAddNewInternationalLicense:
                        ErrorMessage = "Something Went Wrong During Creating The New International License";
                        break;
                    default:
                        ErrorMessage = "An Error Occured!";
                        break;
                }

                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show($"International License Added Successfully With ID ({AddResult.NewInternationalLicense.InternationalLicenseID})"
                , "Succedded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblInternationalLicenseApplicationID.Text = AddResult.NewApplication.ApplicationID.ToString();
            lblInternationalLicensesID.Text = AddResult.NewInternationalLicense.InternationalLicenseID.ToString();
            btnIssueInternationalLicense.Enabled = false;
            lnklblShowNewLicenseInfo.Enabled = true;
            ctrlLicenseInfoWithFilter1.FilterEnable = false;

            _isAddedSuccessfully = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnklblShowPersonLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonLicensesHistory personLicensesHistory 
                = new frmPersonLicensesHistory(clsPerson.GetPersonID_ByNationalNo(ctrlLicenseInfoWithFilter1.NationalNo));

            personLicensesHistory.ShowDialog();
        }

        private void frmAddInternationalLicenses_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Today.ToShortDateString();
            lblIssueDate.Text = DateTime.Today.ToShortDateString();
            lblExpirationDate.Text = DateTime.Today.AddYears(1).ToShortDateString();

            lblFees.Text = clsApplicationType.FindApplicationTypeByID(6).ApplicationFees.ToString();
            //6 => new international license application type

            lblCreatedBy.Text = clsUser.FindUserByID(clsGlobalInformations.CurrentLoggedUserID).UserName;

            btnIssueInternationalLicense.Enabled = false;

        }
    }
}
