using DVLD_3.Applications.Controls;
using DVLD_3.Properties;
using DVLD_3.Tests;
using DVLD_3.UserControls;
using DVLD_3.Utils;
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

            publicFormsPanel1.OpenFormButton.Click += OpenFormButtonClicked;

            _refresh();
            publicFormsPanel1.DataViewer.Sort(publicFormsPanel1.DataViewer.Columns["Is Locked"], ListSortDirection.Ascending);
            publicFormsPanel1.DataViewer.ContextMenuStrip = contextMenuStrip1;
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            publicFormsPanel1.DataFilter.Visible = false;

            publicFormsPanel1.TargetFormToClose = this;

            lblHeader.Text = $"{clsTestType.TestTypeEnumToString(_sceduledTestType.Type)} Test Appointments";
            _prepareFormAccourdingToTestType();

            publicFormsPanel1.OpenFormButton.BackgroundImage = Resources.AddAppointment_32;



        }

        private void OpenFormButtonClicked(object sender , EventArgs e)
        {
            if(_localApp.DoesHaveNonLockedTestAppointmentByTestTypeID(_testTypeID))
            {
                MessageBox.Show("The Person has already a Non-Locked test appointment already!","Error"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (_localApp.DoesHavePassedTestsByTestTypeID(_testTypeID))
            {
                MessageBox.Show("The Person has passed the test already!", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmAddEditTestAppointment frmAddEditTestAppointment = new frmAddEditTestAppointment(_localAppID,_testTypeID);

            frmAddEditTestAppointment.ShowDialog();

            if (frmAddEditTestAppointment.IsDataSaved)
            {
                _refresh();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditTestAppointment frmAddEditTestAppointment = 
                new frmAddEditTestAppointment(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer,0));

            frmAddEditTestAppointment.ShowDialog();

            if(frmAddEditTestAppointment.IsDataSaved)
            {
                _refresh();
            }
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeEditTest TakeEditTest = 
                new frmTakeEditTest(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer,0));

            TakeEditTest.ShowDialog();

            if(TakeEditTest.IsDataSaved)
            {
                _refresh();
            }

        }
    }
}
