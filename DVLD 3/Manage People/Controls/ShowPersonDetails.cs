using DVLD_BusienessLayer;
using DVLD_3.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.MangePeople
{
    public partial class ShowPersonDetails : UserControl
    {
        clsPerson _person;

        //penifit : if we load a wrong person id twice it will not reload default values
        private bool _isThereLoadedPerson;

        //will Invoke when the person data updated by the link label
        public event Action PersonUpdated;
        public ShowPersonDetails()
        {
            InitializeComponent();
            _isThereLoadedPerson = false;
        }

        public clsPerson SelectedPersonInfo
        {
            get
            {
                return _person;
            }
        }


        public void ResetDefaultValues()
        {
            lblName.Text = "???";

            lblPersonID.Text = "???";

            lblEmail.Text = "???";

            lblNationalNo.Text = "???";

            lblGendor.Text = "???";

            lblAddress.Text = "???";

            lblDateOfBirth.Text = "???";

            lblPhone.Text = "???";

            lblCountry.Text = "???";

            picBoxPersonImage.Image = picBoxPersonImage.Image = Resources.Male_512;

        }

        private void _refreshPersonInfo()
        {
            lblName.Text = $"{_person.FirstName} {_person.SecondName} {_person.ThirdName} {_person.LastName}";


            lblPersonID.Text = _person.PersonID.ToString();

            if (!string.IsNullOrEmpty(_person.Email))
            {
                lblEmail.Text = _person.Email;
            }

            lblNationalNo.Text = _person.NationalNo;

            lblGendor.Text = _person.Gendor.ToString();

            lblAddress.Text = _person.Address;

            lblDateOfBirth.Text = _person.DateOfBirth.ToShortDateString();

            lblPhone.Text = _person.Phone;

            lblCountry.Text = _person.NationalityCountryID.ToString();

            if (!string.IsNullOrEmpty(_person.OldImagePath))
            {
                picBoxPersonImage.ImageLocation = _person.OldImagePath;
            }
            else
            {
                if (_person.Gendor == clsPerson.enGendor.Male)
                    picBoxPersonImage.Image = Resources.Male_512;
                else
                    picBoxPersonImage.Image = Resources.Female_512;
            }
        }
        public void LoadPersonInfo(int PersonID)
        {
            _person = clsPerson.FindByID(PersonID);

            if (_person != null)
            {
                _refreshPersonInfo();
                _isThereLoadedPerson = true;
                lnklblUpdatePerson.Enabled = true;
            }
            else
            {
                MessageBox.Show($"Person With ID {PersonID} is not found", "Person not found", MessageBoxButtons.OK, MessageBoxIcon.Error);


                if (_isThereLoadedPerson)
                {
                    ResetDefaultValues();
                    _isThereLoadedPerson = false;
                    lnklblUpdatePerson.Enabled = false;
                }

            }


        }
        private void PersonDataUpdated(int PersonID)
        {
            LoadPersonInfo(_person.PersonID);
            //if the person updated by the link label will send event 
            PersonUpdated?.Invoke();
        }

        private void lnklblUpdatePerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_person != null)
            {
                AddEditPeopleForm addEditPeopleForm = new AddEditPeopleForm(_person.PersonID);

                addEditPeopleForm.OnPersonSavedSuccessfully += PersonDataUpdated;

                addEditPeopleForm.ShowDialog();

            }

        }
    }
}
