using DVLD_3.Properties;
using DVLD_BusienessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.Licenses.Controls
{
    public partial class ctrlLicneseInfo : UserControl
    {
        clsLicense _license;
        public ctrlLicneseInfo()
        {
            InitializeComponent();
        }

        string _name = "";
        string _className = "";
        string _nationalNo = "";
        bool _gender = false;
        bool _isDetained = false;
        DateTime _dateOfBirth = DateTime.MinValue;
        string _imagePath = "";
        private void _fillControlWithDefaultValus()
        {
            lblClass.Text = "???";
            lblName.Text = "???";
            lblLicenseID.Text = "???";
            lblNationaoNo.Text = "???";
            lblGender.Text = "???";
            lblIssueDate.Text = "???";
            lblIssueReason.Text = "???";
            lblIsActive.Text = "???";
            lblDateOfBirth.Text = "???";
            lblDriverID.Text = "???";
            lblExpirationDate.Text = "???";
            lblIsDetained.Text = "???";
            
            picBoxDriverImage.Image = Resources.Male_512;
        }

        private void _fillControlWithLicenseData()
        {
            lblClass.Text = _license.LicenseClass.ClassName;
            lblName.Text = _name;
            lblLicenseID.Text = _license.LicenseID.ToString();
            lblNationaoNo.Text = _nationalNo;
            lblGender.Text = _gender ? "Male": "Female";
            lblIssueDate.Text = _license.IssueDate.ToShortDateString();
            lblIssueReason.Text = _license.IssueReasonString;
            lblIsActive.Text = _license.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _dateOfBirth.ToShortDateString();
            lblDriverID.Text = _license.DriverID.ToString();
            lblExpirationDate.Text = _license.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = _isDetained ? "Yes" : "No";
            lblNotes.Text = string.IsNullOrEmpty(_license.Notes) ? "No Notes" : _license.Notes;

            if (string.IsNullOrEmpty(_imagePath))
            {
                picBoxDriverImage.Image = Resources.Male_512;
            }
            else
            {
                if (File.Exists(_imagePath))
                {
                    picBoxDriverImage.ImageLocation = _imagePath;
                }
                else
                {
                    picBoxDriverImage.Image = Resources.Male_512;
                }
            }
            
        }

        public void LoadLicenseInfo(int ApplicationID)
        {
            _license = clsLicense.FindByApplicationID(ApplicationID,ref _className
                ,ref _name,ref _gender,ref _isDetained,ref _nationalNo,ref _dateOfBirth,ref _imagePath);

            if(_license == null)
            {
                MessageBox.Show($"Can't find License with givin Application ID ({ApplicationID})"
                    ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                _fillControlWithDefaultValus();
                return;
            }

            _fillControlWithLicenseData();


        }
    }
}
