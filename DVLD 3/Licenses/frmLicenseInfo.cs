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
    public partial class frmLicenseInfo : Form
    {
        int _applicationID;

        int _licenseID;

        bool _isSearchByLicenseID;
        public frmLicenseInfo(int ApplicationID)
        {
            InitializeComponent();
            _applicationID = ApplicationID;
            _isSearchByLicenseID = false;
        }

        public frmLicenseInfo(int LicenseID ,bool IsSearchByLicenseID)
        {
            InitializeComponent();
            _isSearchByLicenseID = true;
            _licenseID = LicenseID;

        }
        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {

            if(_isSearchByLicenseID)
            {
                ctrlLicneseInfo1.LoadLicenseInfoByLicenseID(_licenseID);
                return;
            }

            ctrlLicneseInfo1.LoadLicenseInfo(_applicationID);
        }
    }
}
