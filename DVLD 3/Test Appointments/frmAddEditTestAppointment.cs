using DVLD_3.Properties;
using DVLD_BusienessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.Test_Appointments
{
    public partial class frmAddEditTestAppointment : Form
    {
        public bool IsDataSaved= false;

        private clsEnumsUtil.enFormMode _mode;
        public enum enTestAttemptType { FirstTestAppointment= 1 , ReTakeTestAppointment = 2}

        enTestAttemptType _testAttemptType;

        int _trials;

        private clsTestAppointment _testAppointment;


        public frmAddEditTestAppointment(int TestAppointmentID)
        {
            InitializeComponent();

            if(TestAppointmentID != -1)
            {
                _testAppointment = clsTestAppointment.FindTestAppointByID(TestAppointmentID);

                if (_testAppointment != null)
                {
                    _mode = clsEnumsUtil.enFormMode.eUpdate;
                    _trials = _testAppointment.Trials;

                    if (_testAppointment.RetakeTestApplicationID == null)
                    {
                        _testAttemptType = enTestAttemptType.FirstTestAppointment;
                    }
                    else
                    {
                        _testAttemptType = enTestAttemptType.ReTakeTestAppointment;
                    }
                }
                    
            }

        }

        public frmAddEditTestAppointment(int LocalAppID,int TestTypeID)
        {
            InitializeComponent();

            _mode = clsEnumsUtil.enFormMode.eAddNew;

            _testAppointment = new clsTestAppointment(LocalAppID,TestTypeID);

            _trials = _testAppointment.Trials;

            if (_trials == 0)
            {
                _testAttemptType = enTestAttemptType.FirstTestAppointment;
            }
            else
            {
                _testAttemptType = enTestAttemptType.ReTakeTestAppointment;
            }
        }

        private void _fillFormsDataFromTestAppointment()
        {
            if(_mode == clsEnumsUtil.enFormMode.eAddNew || _testAppointment.AppointmentDate > DateTime.Today)
            {
                datePickerTestDate.MinDate = DateTime.Today;
            }
            else 
            {
                datePickerTestDate.MinDate = _testAppointment.AppointmentDate;
            }
           


                lblDLAppID.Text = _testAppointment.LocalAppID.ToString();
            lblDClass.Text = _testAppointment.LicenseClassName;
            lblName.Text = _testAppointment.FullApplicantName;
            lblTrial.Text = _trials.ToString();
            datePickerTestDate.Value = _testAppointment.AppointmentDate;
            lblFees.Text = _testAppointment.PaidFees.ToString();
        }
        void _prepareFormForAddNewMode()
        {
            _fillFormsDataFromTestAppointment();


            if (_testAttemptType == enTestAttemptType.FirstTestAppointment)
            {
                ctrlRetakeTestApplicationInfo1.Enabled = false;
            }
            else
            {
                ctrlRetakeTestApplicationInfo1.Enabled = true;
                //7 => Application With Type Retake (ID)
                ctrlRetakeTestApplicationInfo1.LoadInfo(_testAppointment.PaidFees,7);

            }
        }

        void _prepareFormForUpdateMode()
        {
            _fillFormsDataFromTestAppointment();

            if (_testAttemptType == enTestAttemptType.FirstTestAppointment)
            {
                ctrlRetakeTestApplicationInfo1.Enabled = false;
            }
            else
            {
                
                if(_testAppointment.RetakeTestApplicationID == null)
                {
                    ctrlRetakeTestApplicationInfo1.Enabled = false;
                }
                else
                {
                    ctrlRetakeTestApplicationInfo1.Enabled = true;
                    ctrlRetakeTestApplicationInfo1.LoadInfo((int)_testAppointment.RetakeTestApplicationID, _testAppointment.PaidFees);
                }

                    
            }

            if (_testAppointment.IsLocked)
            {
                btnSave.Enabled = false;
                datePickerTestDate.Enabled = false;
                lblAppontmentLocked.Visible = true;
            }
        }

        void _changeHeaderTextAccourdingToTestAttempt()
        {
            string IsRetake = "";

            switch (_testAttemptType)
            {
                case enTestAttemptType.FirstTestAppointment:
                    IsRetake = " ";
                    break;
                case enTestAttemptType.ReTakeTestAppointment:
                    IsRetake = " Retake ";
                    break;
            }

            lblHeader.Text = $"Scedule{IsRetake}Test";
        }

        void _changeHeadersAccourdingToTestType()
        {
            switch (_testAppointment.TestTypeID)
            {
                case 1 :
                    picBoxControlImage.Image = Resources.Vision_512;
                    groupBox1.Text = "Vision Test";
                    break;
                case 2:
                    picBoxControlImage.Image = Resources.Written_Test_512;
                    groupBox1.Text = "Writing Test";
                    break;
                case 3:
                    picBoxControlImage.Image = Resources.driving_test_512;
                    groupBox1.Text = "driving Test";
                    break;
            }

        }



        private void frmAddEditTestAppointment_Load(object sender, EventArgs e)
        {


            _changeHeaderTextAccourdingToTestAttempt();
            _changeHeadersAccourdingToTestType();



            switch (_mode)
            {
                case clsEnumsUtil.enFormMode.eAddNew:
                    _prepareFormForAddNewMode();
                    break;
                case clsEnumsUtil.enFormMode.eUpdate:
                    _prepareFormForUpdateMode();
                    break;
                default:
                    MessageBox.Show("Can't Find Test Appointment with Selected ID\nChoose another one","Error" 
                        ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                    this.Close();
                    break;
            }

        }
        private bool _fillTestAppointmentInfoFromForm()
        {
            _testAppointment.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;
            _testAppointment.AppointmentDate = datePickerTestDate.Value;
            _testAppointment.IsLocked = false;


            if(_testAttemptType == enTestAttemptType.ReTakeTestAppointment)
            {
                clsApplication RetakeTestApplication = new clsApplication(7);
                RetakeTestApplication.ApplicationDate = DateTime.Now;
                RetakeTestApplication.ApplicationStatus = clsApplication.ApplicationStatusEnum.Completed;
                RetakeTestApplication.ApplicantPersonID = clsLocalApp.FindByID(_testAppointment.LocalAppID).Application.ApplicantPersonID;
                RetakeTestApplication.LastStatusDate = DateTime.Now;
                RetakeTestApplication.PaidFees = RetakeTestApplication.ApplicationType.ApplicationFees;
                RetakeTestApplication.CreatedByUserID = _testAppointment.CreatedByUserID;

                if(RetakeTestApplication.Add())
                {
                    _testAppointment.RetakeTestApplicationID = RetakeTestApplication.ApplicationID;
                    return true;
                }

                return false;
            }
            else
            {
                _testAppointment.RetakeTestApplicationID = null;
                return true;
            }
        }

        private void _saveInAddNewMode()
        {
            if(_fillTestAppointmentInfoFromForm())
            {
                if (_testAppointment.AddNew())
                {
                    IsDataSaved = true;
                    MessageBox.Show("Data Saved Successfully", "Adding TestAppointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    IsDataSaved = false;
                    MessageBox.Show("Something went wrong", "Adding TestAppointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                IsDataSaved = false;
                MessageBox.Show("Something went wrong", "Adding TestAppointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void _saveInUpdateMode()
        {
            if(datePickerTestDate.Value != _testAppointment.AppointmentDate)
            {
                if (_testAppointment.UpdatedAppointmentDate(datePickerTestDate.Value))
                {
                    IsDataSaved = true;
                    MessageBox.Show("Data Saved Successfully", "Updating TestAppointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    IsDataSaved = false;
                    MessageBox.Show("Something went wrong", "Updating TestAppointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                IsDataSaved = false;
                MessageBox.Show("The date didn't changed please choose another date to update"
                    , "Updating TestAppointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure that you want to save the test appointment ?","Saving Test Appointment"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                switch (_mode)
                {
                    case clsEnumsUtil.enFormMode.eAddNew:
                        _saveInAddNewMode();
                        break;
                    case clsEnumsUtil.enFormMode.eUpdate:
                        _saveInUpdateMode();
                        break;
                    default:
                        MessageBox.Show("Can't Find Test Appointment with Selected ID\nChoose another one", "Error"
                            , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        break;
                }

                this.Close();
            }

        }
    }
}
