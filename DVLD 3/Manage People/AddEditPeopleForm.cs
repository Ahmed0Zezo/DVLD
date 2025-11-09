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
    public partial class AddEditPeopleForm : Form
    {
        public AddEditPeopleForm(int PersonID = -1)
        {
            InitializeComponent();

            _personID = PersonID;

            if (_personID == -1)
                _enFormMode = clsEnumsUtil.enFormMode.eAddNew;
            else
            {
                _person = clsPerson.FindByID(_personID);

                if (_person != null)
                {
                    _enFormMode = clsEnumsUtil.enFormMode.eUpdate;
                }
                else
                {
                    MessageBox.Show($"Person With ID ({_personID}) was not found,Add new Person", "Person not found"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _enFormMode = clsEnumsUtil.enFormMode.eAddNew;
                }

            }

        }


        public event Action<int> OnPersonSavedSuccessfully;

        private int _personID;

        private clsPerson _person = new clsPerson();

        private bool _isPicBoxHasImage;

        private clsEnumsUtil.enFormMode _enFormMode;
        private void _getDefaultImageInPicBoxAccourdingToGenderRatioBtn()
        {
            if (rbtnFemale.Checked)
                picBoxPersonImage.Image = Resources.Female_512;
            else
                picBoxPersonImage.Image = Resources.Male_512;
        }

        private void _prepareFormAtAddNewMode()
        {
            picBoxPersonImage.Image = Resources.Male_512;
            cbCountry.SelectedValue = 51;
            _isPicBoxHasImage = false;
            lblHeader.Text = "Add New Person";
            lnklblRemoveImage.Visible = false;
        }

        private void _prepareFormAtUpdateMode()
        {
            lblHeader.Text = "Update Person Info";

            txtFirstName.Text = _person.FirstName;
            txtSecondName.Text = _person.SecondName;
            txtThirdName.Text = _person.ThirdName;
            txtLastName.Text = _person.LastName;

            txtNationalNo.Text = _person.NationalNo;

            txtEmail.Text = string.IsNullOrWhiteSpace(_person.Email) ? "" : _person.Email;

            txtPhone.Text = _person.Phone;
            txtAddress.Text = _person.Address;

            datePickerDateOfBirth.Value = _person.DateOfBirth;

            if (_person.Gendor == clsPerson.enGendor.Male)
                rbtnFemale.Checked = false;
            else
                rbtnFemale.Checked = true;

            cbCountry.SelectedValue = _person.NationalityCountryID;

            if (!string.IsNullOrEmpty(_person.OldImagePath))
            {
                picBoxPersonImage.ImageLocation = _person.OldImagePath;
                _isPicBoxHasImage = true;
                lnklblRemoveImage.Visible = true;
            }
            else
            {
                lnklblRemoveImage.Visible = false;
                _isPicBoxHasImage = false;
            }
        }

        private void _fillPersonFromForm()
        {
            if (_person == null)
            {
                _person = new clsPerson();
            }


            _person.FirstName = txtFirstName.Text;
            _person.SecondName = txtSecondName.Text;
            _person.ThirdName = txtThirdName.Text;
            _person.LastName = txtLastName.Text;

            _person.NationalNo = txtNationalNo.Text;

            _person.Email = txtEmail.Text;

            _person.Phone = txtPhone.Text;
            _person.Address = txtAddress.Text;

            _person.DateOfBirth = datePickerDateOfBirth.Value;

            if (rbtnMale.Checked)
                _person.Gendor = clsPerson.enGendor.Male;
            else
                _person.Gendor = clsPerson.enGendor.Female;

            _person.NationalityCountryID = (int)cbCountry.SelectedValue;

            if (_isPicBoxHasImage)
            {
                _person.NewImagePath = picBoxPersonImage.ImageLocation;
            }
            else
            {
                _person.NewImagePath = null;
            }
        }

        private bool _saveInAddNewMode()
        {
            _fillPersonFromForm();

            return _person.AddNew();
        }

        private bool _saveInUpdateMode()
        {
            _fillPersonFromForm();

            return _person.Update();
        }
        private void AddEditPeopleForm_Load(object sender, EventArgs e)
        {
            txtFirstName.Focus();

            cbCountry.DataSource = clsCountry.GetAllCountries();
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "CountryID";
            datePickerDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);

            switch (_enFormMode)
            {
                case clsEnumsUtil.enFormMode.eUpdate:
                    _prepareFormAtUpdateMode();
                    break;

                default:
                    _prepareFormAtAddNewMode();
                    break;
            }
        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isPicBoxHasImage)
            {
                _getDefaultImageInPicBoxAccourdingToGenderRatioBtn();
            }

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            TextBox TargetTextBox = (TextBox)sender;

            if (string.IsNullOrEmpty(TargetTextBox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TargetTextBox, "This filed is required");
            }
            else
            {
                if ((clsPerson.IsNationalNumberExist(txtNationalNo.Text) && txtNationalNo.Text != _person.NationalNo))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(TargetTextBox, "This national number is already exists,Please enter another one");
                }
                else
                {
                    errorProvider1.SetError(TargetTextBox, "");
                }
            }




        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            if (!clsTextBoxUtil.LinkTextBoxWithErrorProvider(txtEmail, "This email isn't in a correct form,Please enter a valid email"
                , !clsEmail.IsEmailInCorrectForm(txtEmail.Text)
                , errorProvider1, e))
                return;

            clsTextBoxUtil.LinkTextBoxWithErrorProvider(txtEmail, "This field is required"
                , string.IsNullOrEmpty(txtEmail.Text)
                , errorProvider1, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            switch (_enFormMode)
            {
                case clsEnumsUtil.enFormMode.eUpdate:
                    if (_saveInUpdateMode())
                    {
                        MessageBox.Show("Person Updated Successfully", "Updating Person");
                        this.Close();
                        OnPersonSavedSuccessfully?.Invoke(_person.PersonID);
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong", "Updating Person");
                    }
                    break;

                default:
                    if (_saveInAddNewMode())
                    {
                        MessageBox.Show("Person Added Successfully", "Adding Person");
                        this.Close();
                        OnPersonSavedSuccessfully?.Invoke(_person.PersonID);
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong", "Adding Person");
                    }
                    break;
            }
        }

        private void lnklblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picBoxPersonImage.ImageLocation = openFileDialog1.FileName;
                _isPicBoxHasImage = true;
                lnklblRemoveImage.Visible = true;
            }
        }

        private void lnklblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_isPicBoxHasImage)
            {
                _getDefaultImageInPicBoxAccourdingToGenderRatioBtn();

                _isPicBoxHasImage = false;

                lnklblRemoveImage.Visible = false;
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            clsTextBoxUtil.MakeTextBoxesHaveOnlyNumbers((TextBox)sender);
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            TextBox TargetTextBox = (TextBox)sender;

            if (string.IsNullOrEmpty(TargetTextBox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(TargetTextBox, "This filed is required");
            }
            else
            {

                errorProvider1.SetError(TargetTextBox, "");
            }
        }
    }
}
