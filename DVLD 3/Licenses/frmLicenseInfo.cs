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
        public frmLicenseInfo(int ApplicationID)
        {
            InitializeComponent();
            _applicationID = ApplicationID;
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlLicneseInfo1.LoadLicenseInfo(_applicationID);
        }
    }
}
