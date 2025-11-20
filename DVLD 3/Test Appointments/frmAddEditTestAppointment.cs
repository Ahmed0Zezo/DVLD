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

        public enum enTestAttemptType { FirstTestAppointment= 1 , ReTakeTestAppointment = 2}

        enTestAttemptType _testAttemptType;

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
                _testAppointment = clsTestAppointment.FindTestAppointByID(TestAppointmentID);

                if (_testAppointment == null)
                    _mode = clsEnumsUtil.enFormMode.eAddNew;
                else
                    _mode = clsEnumsUtil.enFormMode.eUpdate;
            }
        }

        public frmAddEditTestAppointment(int LocalAppID,int TestTypeID)
        {
            InitializeComponent();

            _mode = clsEnumsUtil.enFormMode.eAddNew;

            _testAppointment = new clsTestAppointment();

            _testAppointment.LocalAppID = LocalAppID;

            _testAppointment.TestTypeID = TestTypeID;

        }

        void _prepareUIForAddNewMode()
        {

        }

        void _prepareUIForAddUpdate()
        {

        }

        private void frmAddEditTestAppointment_Load(object sender, EventArgs e)
        {


            switch (_mode)
            {
                case clsEnumsUtil.enFormMode.eAddNew:
                    _prepareUIForAddNewMode();
                    break;
                case clsEnumsUtil.enFormMode.eUpdate:
                    _prepareUIForAddUpdate();
                    break;
            }

        }
    }
}
