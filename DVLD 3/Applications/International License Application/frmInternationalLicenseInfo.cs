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
    public partial class frmInternationalLicenseInfo : Form
    {
        int _internationalLicenseID;

        

        public frmInternationalLicenseInfo(int internationalLicenseID)
        {
            InitializeComponent();
            _internationalLicenseID = internationalLicenseID;
        }

        private void frmInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseInfo1.LoadInternationalLicenseDataByID(_internationalLicenseID);

            ctrlInternationalLicenseInfo1.OnLicenseSelected += LicenseSelected;
        }

        public void LicenseSelected(int LicenseID)
        {
           if(LicenseID == -1)
            {
                MessageBox.Show($"Can't Find International License With ID : {LicenseID}");
            }
        }
    }
}
