using DVLD_BusienessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.Applications.Retake_Test_Applications
{
    public partial class ctrlRetakeTestApplicationInfo : UserControl
    {
        public ctrlRetakeTestApplicationInfo()
        {
            InitializeComponent();
        }
        public void LoadDataForExistRetakeTestApplication(int RetakeTestApplicationID,decimal OriginalTestFees)
        {
            //Loading data of exist RetakeTestApplication
            clsApplication _application = clsApplication.FindByID(RetakeTestApplicationID);
            lblRetakeTestAppID.Text = _application.ApplicationID.ToString();
            lblRetakeAppFees.Text = _application.PaidFees.ToString();
            lblTotalFees.Text = $"{OriginalTestFees + _application.PaidFees}";
        }

        public void LoadDataForNewRetakeTestApplication(decimal OriginalTestFees)
        {
            //Loading data for New RetakeTestApplicationn
            clsApplicationType _applicationType = clsApplicationType.FindApplicationTypeByID(7);
            lblRetakeTestAppID.Text = "N/A";
            lblRetakeAppFees.Text = _applicationType.ApplicationFees.ToString();
            lblTotalFees.Text = $"{OriginalTestFees + _applicationType.ApplicationFees}";
        }
    }
}
