using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.Applications.LocalDrivingLicenseApplication
{
    public partial class frmShowLocalAppInfos : Form
    {
        int _localAppID;

        public bool IsPersonDataUpdated;
        public frmShowLocalAppInfos(int LocalAppID)
        {
            InitializeComponent();
            IsPersonDataUpdated = false;
            _localAppID = LocalAppID;
        }

        private void _personDataUpdated()
        {
            IsPersonDataUpdated = true;
        }

        private void frmShowLocalAppInfos_Load(object sender, EventArgs e)
        {
            ctrlLDApplicationInfo1.LoadApplicationInfo(_localAppID);
            ctrlLDApplicationInfo1.PersonDataUpdated += _personDataUpdated;
        }
    }
}
