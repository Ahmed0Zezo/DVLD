using DVLD_3.Applications.Controls;
using DVLD_3.Properties;
using DVLD_3.UserControls;
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
    public partial class frmManageApplicationTestAppointments : Form
    {
        clsTestType _sceduledTestType;
        clsLocalApp _localApp;

        int _testTypeID;
        int _localAppID;

        DataTable _appointmentsData;

        public frmManageApplicationTestAppointments(int LocalAppID, int TestTypeID)
        {
            InitializeComponent();
            _testTypeID = TestTypeID;
            _localAppID = LocalAppID;

        }
        private void _refresh()
        {
            _appointmentsData = clsTestAppointment.GetAllByApplicationIDAndTestTypeID_ForTable(_localAppID,_testTypeID);
            publicFormsPanel1.LinkDataToGridView(_appointmentsData);
        }


        private void _prepareFormAccourdingToTestType()
        {
            switch (_sceduledTestType.Type)
            {
                case clsTestType.TestTypeEnum.Vision:
                    picBoxControlImage.Image = Resources.Vision_512;
                    break;
                case clsTestType.TestTypeEnum.Written:
                    picBoxControlImage.Image = Resources.Written_Test_512;
                    break;
                case clsTestType.TestTypeEnum.Street:
                    picBoxControlImage.Image = Resources.driving_test_512;
                    break;
            }
        }

        

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            _sceduledTestType = clsTestType.FindTestTypeByID(_testTypeID);

            if (_sceduledTestType == null)
            {
                MessageBox.Show("Invalid Test Type ID" , "Error" ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _localApp = clsLocalApp.FindByID(_localAppID);
            
            if (_localApp == null)
            {
                MessageBox.Show("Local App ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlLDApplicationInfo1.LoadApplicationInfo(_localApp.LocalDrivingLicenseApplicationID);

            _refresh();
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            publicFormsPanel1.DataFilter.Visible = false;

            lblHeader.Text = $"{clsTestType.TestTypeEnumToString(_sceduledTestType.Type)} Test Appointments";
            _prepareFormAccourdingToTestType();

            publicFormsPanel1.OpenFormButton.BackgroundImage = Resources.AddAppointment_32;


        }
    }
}
