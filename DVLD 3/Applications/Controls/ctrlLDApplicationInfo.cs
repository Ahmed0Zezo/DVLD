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
    public partial class ctrlLDApplicationInfo : UserControl
    {
        public ctrlLDApplicationInfo()
        {
            InitializeComponent();
        }

        public event Action PersonDataUpdated;



        private void _refreshDefaultValues()
        {
            lblID.Text = "???";
            lblPassedTests.Text = "???";
            lblAppliedForLicense.Text = "???";
            lnklblShowLicenseInfo.Enabled = false;
        }

        
        private void _fillControlsWithData(clsLocalApp app)
        {
            if (!ctrlApplicationInfo1.LoadApplicationInfo(app.ApplicationID))
            {
                _refreshDefaultValues();
                return;
            } 

            lblID.Text = app.LocalDrivingLicenseApplicationID.ToString();
            lblPassedTests.Text = app.PassedTests.ToString();
            lblAppliedForLicense.Text = clsLicenseClass.GetLiceseClassNameByItsID(app.LicenseClassID);

            //handel Show License at another time
            lnklblShowLicenseInfo.Enabled = false;
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
            clsLocalApp app = clsLocalApp.FindByID(AppID);

            if (app == null)
            {
                _faildToFindApplication(AppID);
                return false;
            }

            _fillControlsWithData(app);

            return true;
        }

        private void _personDataUpdated()
        {
            PersonDataUpdated?.Invoke();
        }
        private void ctrlLDApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlApplicationInfo1.PersonDataUpdated += _personDataUpdated;
        }
    }
}
