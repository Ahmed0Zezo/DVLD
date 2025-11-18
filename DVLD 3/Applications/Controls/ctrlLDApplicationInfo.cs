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

        private void _refreshDefaultValues()
        {
            lblID.Text = "???";
            lblPassedTests.Text = "???";
            lblAppliedForLicense.Text = "???";
            lnklblShowLicenseInfo.Enabled = false;
        }

        private void _fillControlsWithData(clsLocalApp app)
        {
            lblID.Text = app.ApplicationID.ToString();
            lblPassedTests.Text = app.PassedTests.ToString();
            lblAppliedForLicense.Text =clsLicenseClass.GetLiceseClassNameByItsID(app.LicenseClassID);

            lnklblShowLicenseInfo.Enabled = false;
        }


        public void LoadApplicationInfo(int AppID)
        {
            //if (AppID == -1)
            //{
            //    _faildToFindApplication(AppID);
            //    return;
            //}
            //clsApplication app = clsApplication.FindByID(AppID);

            //if (app == null)
            //{
            //    _faildToFindApplication(AppID);
            //    return;
            //}

            //_fillControlsWithData(app);
            //OnApplicationSelected?.Invoke(app.ApplicationID);

        }
    }
}
