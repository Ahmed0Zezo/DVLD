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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_3.Licenses.Controls
{
    public partial class ctrlLicneseInfo : UserControl
    {
        clsLicense _license;

        public delegate void LicenseSelected(int LicenseID);

        public event LicenseSelected OnLicenseSelected;
        public ctrlLicneseInfo()
        {
            InitializeComponent();
        }

        public clsLicense SelectedLicense
        {
            get
            {
                return _license;
            }
        }
        // we could got the data by Composite the data into License class
        //but that will load unneccessary data in different senarios
        public string   _name = "";
        public string   _className = "";
        public string   _nationalNo = "";
        public bool     _gender = false;
        public bool     _isDetained = false;
        public DateTime _dateOfBirth = DateTime.MinValue;
        public string   _imagePath = "";

        public string PersonName {
            get
            {
                return _name;
            } 
        }
        public string ClassName {
            get
            {
                return _className;
            }
        }
        public string NationalNo {
            
            get
            {
                return _nationalNo;
            }
        }
        public bool Gender {
           
            get
            {
                return _gender;
            }
        }
        public bool IsDetained {
            
            get
            {
                return _isDetained;
            }
        }
        public DateTime DateOfBirth {
            
            get
            {
                return _dateOfBirth;
            }
        }
        public string ImagePath {
            
            get
            {
                return _imagePath;
            }
        }


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
            lblNotes.Text = "???";
            picBoxDriverImage.Image = Resources.Male_512;
        }

        private void _fillControlWithLicenseData()
        {
            lblClass.Text = _license.LicenseClass.ClassName;
            lblName.Text = _name;
            lblLicenseID.Text = _license.LicenseID.ToString();
            lblNationaoNo.Text = _nationalNo;
            lblGender.Text = _gender ? "Female" : "Male";
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
                picBoxDriverImage.Image = _gender == false ? Resources.Male_512 : Resources.Female_512;
            }
            else
            {
                if (File.Exists(_imagePath))
                {
                    picBoxDriverImage.ImageLocation = _imagePath;
                }
                else
                {
                    picBoxDriverImage.Image = _gender == false ? Resources.Male_512 : Resources.Female_512;
                }
            }
            
        }


        private void _LoadData(string ErrorMessage)
        {
            if (_license == null)
            {
                MessageBox.Show($"{ErrorMessage}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _fillControlWithDefaultValus();
                OnLicenseSelected?.Invoke(-1);
                return;
            }
            OnLicenseSelected?.Invoke(_license.LicenseID);
            _fillControlWithLicenseData();
        }

        public void LoadLicenseInfo(int ApplicationID)
        {
            _license = clsLicense.FindByApplicationID(ApplicationID,ref _className
                ,ref _name,ref _gender,ref _isDetained,ref _nationalNo,ref _dateOfBirth,ref _imagePath);

            _LoadData($"Can't find License with givin Application ID ({ApplicationID})");
        }

        public void LoadLicenseInfoByLicenseID(int LicenseID)
        {
            _license = clsLicense.FindByLicenseIDWithMoreLicenseInfoData(LicenseID, ref _className
                , ref _name, ref _gender, ref _isDetained, ref _nationalNo, ref _dateOfBirth, ref _imagePath);

            _LoadData($"Can't find License with givin License ID ({LicenseID})");
        }
    }
}
