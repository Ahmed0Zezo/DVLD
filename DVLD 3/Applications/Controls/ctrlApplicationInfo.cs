using DVLD_3.MangePeople;
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

namespace DVLD_3.Applications.Controls
{
    public partial class ctrlApplicationInfo : UserControl
    {
        

        public event Action PersonDataUpdated;

        private clsApplication _app;
        public event Action<int> OnApplicationSelected;
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private void _refreshDefaultValues()
        {
            lblID.Text = "???";
            lblStatus.Text = "???";
            lblFees.Text = "???";
            lblType.Text = "???";
            lblApplicant.Text = "???";
            lblDate.Text = "???";
            lblLastUpdateDate.Text = "???";
            lblCreatedBy.Text = "???";
            lnklblPersonInfo.Enabled = false;
        }

        private void _fillControlsWithData(clsApplication app)
        {
            lblID.Text = app.ApplicationID.ToString();
            lblStatus.Text = clsApplication.StatusEnumToString(app.ApplicationStatus);
            lblFees.Text = app.PaidFees.ToString();
            lblType.Text = app.ApplicationType.ApplicationTypeTitle;
            lblApplicant.Text = clsPerson.FindByID(app.ApplicantPersonID).FullName;
            lblDate.Text = app.ApplicationDate.ToShortDateString();
            lblLastUpdateDate.Text = app.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = clsUser.FindUserByID(app.CreatedByUserID).UserName;
            lnklblPersonInfo.Enabled = true;
        }

        private void _faildToFindApplication(int AppID)
        {
            MessageBox.Show($"Application With ID ({AppID}) is not exist", "App Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _refreshDefaultValues();
        }
        public bool LoadApplicationInfo(int AppID)
        {
            if (AppID == -1)
            {
                _faildToFindApplication(AppID);
                return false;
            }
            _app = clsApplication.FindByID(AppID);

            if (_app == null)
            {
                _faildToFindApplication(AppID);
                return false;
            }

            _fillControlsWithData(_app);
            OnApplicationSelected?.Invoke(_app.ApplicationID);

            return true;

        }

        private void lnklblPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonDetailsForm frmPerson = new ShowPersonDetailsForm(_app.ApplicantPersonID);

            frmPerson.ShowDialog();

            if(frmPerson.IsPersonDataUpdated)
            {
                LoadApplicationInfo(_app.ApplicationID);
                PersonDataUpdated?.Invoke();
            }
            

        }
    }
}
