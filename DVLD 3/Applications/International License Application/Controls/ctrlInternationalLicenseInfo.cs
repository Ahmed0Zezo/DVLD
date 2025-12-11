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

namespace DVLD_3.Applications.International_License_Application.Controls
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        private clsInternationalLicense _internationalLicense;

        private clsPerson _personDetails;

        public clsInternationalLicense SelectedLicense
        {
            get
            {
                return _internationalLicense;
            }
        }

        public event Action<int> OnLicenseSelected;
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _resetToDefaultValus()
        {
            string DefaultValue = "???";
            lblName.Text = DefaultValue;
            lblLocalLicenseID.Text = DefaultValue;
            lblGender.Text = DefaultValue;
            lblExpirationDate.Text = DefaultValue;
            lblIssueDate.Text = DefaultValue;
            lblInternationalLIcenseID.Text = DefaultValue;
            lblNationaoNo.Text = DefaultValue;
            lblApplicationID.Text = DefaultValue;
            lblIsActive.Text = DefaultValue;
            lblDateOfBirth.Text = DefaultValue;
            lblDriverID.Text = DefaultValue;

            picBoxDriverImage.Image = Resources.Male_512;
        }

        private void LoadPersonImage()
        {
            if (_personDetails.OldImagePath == null)
            {
                picBoxDriverImage.Image = _personDetails.Gendor == clsPerson.enGendor.Male ? Resources.Male_512 : Resources.Female_512;
            }
            else
            {
                if(File.Exists(_personDetails.OldImagePath))
                {
                    picBoxDriverImage.Image = Image.FromFile(_personDetails.OldImagePath);
                }
                else
                {
                    picBoxDriverImage.Image = _personDetails.Gendor == clsPerson.enGendor.Male ? Resources.Male_512 : Resources.Female_512;
                }
            }
        }
        private void _fillCompenetsWithData()
        {
            lblName.Text = _personDetails.FullName;
            lblLocalLicenseID.Text = _internationalLicense.LocalLicenseID.ToString();
            lblGender.Text = _personDetails.Gendor == clsPerson.enGendor.Male ? "Male" : "Female";
            lblExpirationDate.Text = _internationalLicense.ExpirationDate.ToShortDateString();
            lblIssueDate.Text = _internationalLicense.IssueDate.ToShortDateString(); ;
            lblInternationalLIcenseID.Text = _internationalLicense.InternationalLicenseID.ToString();
            lblNationaoNo.Text = _personDetails.NationalNo;
            lblApplicationID.Text = _internationalLicense.ApplicationID.ToString() ;
            lblIsActive.Text = _internationalLicense.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _personDetails.DateOfBirth.ToShortDateString() ;
            lblDriverID.Text = _internationalLicense.DriverID.ToString();

            LoadPersonImage();
        }
        public void LoadInternationalLicenseDataByID(int IntLicenseID)
        {
            _internationalLicense = clsInternationalLicense.FindByID(IntLicenseID);

            if(_internationalLicense == null)
            {
                _resetToDefaultValus();
                OnLicenseSelected?.Invoke(-1);
                return;
            }

            _personDetails = clsPerson.FindByID
                (clsApplication.FindByID(_internationalLicense.ApplicationID).ApplicantPersonID);

            if(_personDetails == null)
            {
                _resetToDefaultValus();
                OnLicenseSelected?.Invoke(-1);
                return;
            }

            _fillCompenetsWithData();
            OnLicenseSelected?.Invoke(_internationalLicense.LocalLicenseID);
        }

    }
}
