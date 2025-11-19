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

namespace DVLD_3.Test_Appointments
{
    public partial class frmTestAppointments : Form
    {
        clsTestType _sceduledTestType;

       
        private void _prepareFormAccourdingToTestType()
        {
            switch (_sceduledTestType.Type)
            {
                case clsTestType.TestTypeEnum.Vision:
                    picBoxControlImage.BackgroundImage = Resources.Vision_512;
                    break;
                case clsTestType.TestTypeEnum.Written:
                    picBoxControlImage.BackgroundImage = Resources.Written_Test_512;
                    break;
                case clsTestType.TestTypeEnum.Street:
                    picBoxControlImage.BackgroundImage = Resources.driving_test_512;
                    break;
            }
        }

        public frmTestAppointments(int LocalAppID,int TestTypeID)
        {
            InitializeComponent();
            _sceduledTestType = clsTestType.FindTestTypeByID(TestTypeID);

            if(_sceduledTestType == null)
            {
                this.Close();
            }
            else
            {
                lblHeader.Text = $"{clsTestType.TestTypeEnumToString(_sceduledTestType.Type)} Test Appointments";
                _prepareFormAccourdingToTestType();


            }
        }

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            publicFormsPanel1.OpenFormButton.BackgroundImage = Resources.AddAppointment_32;
        }
    }
}
