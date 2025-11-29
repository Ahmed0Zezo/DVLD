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

namespace DVLD_3.Applications.Controls
{
    public partial class ctrlLDApplicationInfo : UserControl
    {

        int _localAppID;

        clsLocalApp _localApp;
        public ctrlLDApplicationInfo()
        {
            InitializeComponent();
        }

        public event Action PersonDataUpdated;


        public clsLocalApp LocalApplication
        {
            get
            {
                return _localApp;
            }
        }

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
            lblPassedTests.Text = $"{app.PassedTests}/3";
            lblAppliedForLicense.Text = clsLicenseClass.GetLiceseClassNameByItsID(app.LicenseClassID);


            lnklblShowLicenseInfo.Enabled = clsLicense.IsLicenseExistByApplicationID(app.ApplicationID);
            
            
            
        }

        private void _faildToFindApplication(int AppID)
        {
            MessageBox.Show($"Application With ID ({AppID}) is not exist", "App Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _refreshDefaultValues();
        }

        public bool LoadApplicationInfo(int AppID)
        {
            _localAppID = AppID;
            if (_localAppID == -1)
            {
                _faildToFindApplication(_localAppID);
                return false;
            }
            _localApp = clsLocalApp.FindByID(_localAppID);

            if (_localApp == null)
            {
                _faildToFindApplication(_localAppID);
                return false;
            }

            _fillControlsWithData(_localApp);

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

        private void lnklblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo LicenseInfoForm = new frmLicenseInfo(_localApp.ApplicationID);

            LicenseInfoForm.ShowDialog();
        }
    }
}
