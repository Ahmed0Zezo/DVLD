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

namespace DVLD_3.Test_Appointments
{
    public partial class frmAddEditTestAppointment : Form
    {
        private clsEnumsUtil.enFormMode _mode;

        private clsTestAppointment _testAppointment;
        public frmAddEditTestAppointment(int TestAppointmentID)
        {
            InitializeComponent();

            if(TestAppointmentID == -1)
            {
                _mode = clsEnumsUtil.enFormMode.eAddNew;
            }
            else
            {
                _testAppointment = clsTestAppointment.Find
            }
        }

        public frmAddEditTestAppointment(int LocalAppID,int TestTypeID)
        {
            InitializeComponent();
        }


    }
}
