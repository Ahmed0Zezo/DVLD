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
        public void LoadApplicationInfo(int AppID)
        {
            if (AppID == -1)
            {
                _faildToFindApplication(AppID);
                return;
            }
            clsApplication app = clsApplication.FindByID(AppID);

            if (app == null)
            {
                _faildToFindApplication(AppID);
                return;
            }

            _fillControlsWithData(app);
            OnApplicationSelected?.Invoke(app.ApplicationID);

        }
    }
}
