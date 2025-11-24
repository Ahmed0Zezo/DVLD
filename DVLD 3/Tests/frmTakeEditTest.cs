using DVLD_3.Applications.Retake_Test_Applications;
using DVLD_3.Properties;
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
using static DVLD_3.clsEnumsUtil;
using static DVLD_3.Test_Appointments.frmAddEditTestAppointment;

namespace DVLD_3.Tests
{
    public partial class frmTakeEditTest : Form
    {
        clsEnumsUtil.enFormMode _mode;

        clsTest _test;

        public bool IsDataSaved = false;
        public frmTakeEditTest(int TestAppointmentID)
        {
            InitializeComponent();

            _test = clsTest.FindByTestAppointmentID(TestAppointmentID);

            if (_test == null)
            {
                _test = new clsTest(TestAppointmentID);
                if(_test.TestAppointment != null)
                {
                    _mode = enFormMode.eAddNew;
                }
            }
            else
            {
                _mode = enFormMode.eUpdate;
            }
                
        }

        void _changeHeadersAccourdingToTestType()
        {
            switch (_test.TestAppointment.TestTypeID)
            {
                case 1:
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

        void _fillTestAppointmentInfo()
        {
            lblDLAppID.Text = _test.TestAppointment.LocalAppID.ToString();
            lblDClass.Text = _test.TestAppointment.LicenseClassName;
            lblName.Text = _test.TestAppointment.FullApplicantName;
            lblTrial.Text = _test.TestAppointment.Trials.ToString();
            lblDate.Text= _test.TestAppointment.AppointmentDate.ToShortDateString();
            lblFees.Text = _test.TestAppointment.PaidFees.ToString();

        }
        void _prepareFormForAddNewMode()
        {
            lblTestID.Text = "Not Taken Yet";
            rbtnPass.Checked = true;
            txtNotes.Text = "";
        }

        void _prepareFormForUpdate()
        {
            lblTestID.Text = _test.TestID.ToString();

            lblCantChangeResult.Visible = true;

            rbtnPass.Enabled = false;
            rbtnFail.Enabled = false;

            rbtnPass.Checked = _test.TestResult;
            rbtnFail.Checked = !_test.TestResult;
            txtNotes.Text = _test.Notes;
        }

        private void frmTakeEditTest_Load(object sender, EventArgs e)
        {
            if(_test.TestAppointment != null)
            {
                _changeHeadersAccourdingToTestType();
                _fillTestAppointmentInfo();
            }

            switch (_mode)
            {
                case clsEnumsUtil.enFormMode.eAddNew:
                    _prepareFormForAddNewMode();
                    break;
                case enFormMode.eUpdate:
                    _prepareFormForUpdate();
                    break;
                default:
                    MessageBox.Show("Something went wrong ,Can't find Test Appointment data with send App ID"
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
            }


        }

        void _fillTestInfoFromFormForAddNew()
        {
            _test.Notes = txtNotes.Text;
            _test.TestResult = rbtnPass.Checked ;
            _test.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;
        }
        void _saveInAddNewMode()
        {
            _fillTestInfoFromFormForAddNew();

            IsDataSaved = _test.Add();

            if (!IsDataSaved)
            {
                MessageBox.Show("Something went wrong during adding test,Contact admin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        void _saveInUpdateMode()
        {
            if (txtNotes.Text == _test.Notes)
            {
                MessageBox.Show("You didn't change the notes,To update test try to change the notes", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsDataSaved = false;
                return;
            }

            IsDataSaved = _test.UpdateNotes(txtNotes.Text);

            if (!IsDataSaved)
            {
                MessageBox.Show("Something went wrong during updating test,Contact admin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (_mode)
            {
                case clsEnumsUtil.enFormMode.eAddNew:
                    _saveInAddNewMode();
                    break;
                case enFormMode.eUpdate:
                    _saveInUpdateMode();
                    break;
                default:
                    MessageBox.Show("Something went wrong ,Can't find Test Appointment data with sent App ID"
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
            }

            if(IsDataSaved)
            {
                MessageBox.Show("Data Saved Successfully!"
                        , "Saving Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}

