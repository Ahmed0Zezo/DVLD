using DVLD_3.MangePeople.Controls;
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

namespace DVLD_3.Users
{
    public partial class frmAddEditUser : Form
    {
        int _userID;

       
        clsEnumsUtil.enFormMode _enMode;

       public event Action<int> UserUpdatedSuccessfully;

        clsUser _currentUser ;
        public frmAddEditUser(int UserID = -1)
        {
            InitializeComponent();
            _userID = UserID;

            if (UserID == -1)
                _enMode = clsEnumsUtil.enFormMode.eAddNew;
            else
            {
                _enMode = clsEnumsUtil.enFormMode.eUpdate;
            }
        }

        private void _enableUserInfoControls(bool Enabled)
        {
            tabPage2.Enabled = Enabled;
        }

        


        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            personDetailsWithFilter1.OnPersonSelected += _personSelectedChanged;

            switch (_enMode)
            {
                case clsEnumsUtil.enFormMode.eAddNew:
                    _prepareUIForAddMode();
                    break;
                case clsEnumsUtil.enFormMode.eUpdate:
                    _prepareUIForUpdateMode();
                    break;
            }
        }

        private void _personSelectedChanged(int PersonID)
        {

            if (PersonID != -1)
            {
                if(clsUser.IsUserExistByPersonID(PersonID))
                {
                    btnSave.Enabled = false;
                    _enableUserInfoControls(false);
                }
                
            }
            else
            {
                btnSave.Enabled = false;
                _enableUserInfoControls(false);
                
            }
        }
        private void _prepareUIForAddMode()
        {
            _currentUser = new clsUser();

            btnSave.Enabled = false;
            _enableUserInfoControls(false);

            lblHeader.Text = "Add New User";
            personDetailsWithFilter1.FilterEnabled = true;

            lblUserID.Text = "???";
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            chkBoxIsActive.Checked = true;
        }

        private void _prepareUIForUpdateMode()
        {
            _currentUser = clsUser.FindUserByID(_userID);

            if (_currentUser == null)
            {
                MessageBox.Show($"User With ID ({_userID}) is not found.Add the user then update it","Can't find user"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                _enMode = clsEnumsUtil.enFormMode.eAddNew;
                _prepareUIForAddMode();
                return;
            }

            lblHeader.Text = "Update User";
            personDetailsWithFilter1.FilterEnabled = false;


            personDetailsWithFilter1.PersonAdded(_currentUser.PersonID);
            lblUserID.Text = _currentUser.UserID.ToString();
            txtUsername.Text = _currentUser.UserName;
            txtPassword.Text = _currentUser.Password;
            txtConfirmPassword.Text = _currentUser.Password;
            chkBoxIsActive.Checked = _currentUser.IsActive;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(personDetailsWithFilter1.SelectedPerson == null)
            {
                MessageBox.Show("Please select a person" , "Person not selected" ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if(clsUser.IsUserExistByPersonID(personDetailsWithFilter1.SelectedPerson.PersonID))
            {
                MessageBox.Show("This person has already user.Select another person", "User already exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;
            _enableUserInfoControls(true);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _fillUserInfo()
        {
            if(_currentUser == null)
            {
                _currentUser = new clsUser();
            }
            _currentUser.UserName = txtUsername.Text;
            _currentUser.Password = txtPassword.Text;
            _currentUser.PersonID = personDetailsWithFilter1.SelectedPerson.PersonID;
            _currentUser.IsActive = chkBoxIsActive.Checked;
        }
        private bool _saveInAddMode()
        {
            _fillUserInfo();

            return _currentUser.AddNew();
        }

        private bool _saveInUpdateMode()
        {
            _fillUserInfo();
            return _currentUser.Update();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are incorrect put the mouse over the red error and read the message"
                    ,"Invalid fileds",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            switch (_enMode)
            {
                case clsEnumsUtil.enFormMode.eAddNew:
                    if(_saveInAddMode())
                    {
                        MessageBox.Show("User Saved Successfully", "User Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        UserUpdatedSuccessfully?.Invoke(_currentUser.UserID);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong","User is't saved",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                        break;
                case clsEnumsUtil.enFormMode.eUpdate:
                    if (_saveInUpdateMode())
                    {
                        MessageBox.Show("User Saved Successfully", "User Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UserUpdatedSuccessfully?.Invoke(_currentUser.UserID);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong", "User is't saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }

        }

        private void EmpyTextBoxCheck_Validating(object sender, CancelEventArgs e)
        {
            TextBox targetTextBox = (TextBox)sender;

            clsTextBoxUtil.LinkTextBoxWithErrorProvider(targetTextBox, "This field is required"
                , string.IsNullOrEmpty(targetTextBox.Text)
                , errorProvider1, e);
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (!clsTextBoxUtil.LinkTextBoxWithErrorProvider(txtUsername, "This field is required"
                 , string.IsNullOrEmpty(txtUsername.Text)
                 , errorProvider1, e))
                return;

            clsTextBoxUtil.LinkTextBoxWithErrorProvider(txtUsername, "This Username is already exist.Try another one"
                , clsUser.IsUserNameExist(txtUsername.Text) && txtUsername.Text != _currentUser.UserName
                , errorProvider1, e);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            TextBox targetTextBox = (TextBox)sender;

            if (!clsTextBoxUtil.LinkTextBoxWithErrorProvider(targetTextBox, "This field is required"
                , string.IsNullOrEmpty(targetTextBox.Text)
                , errorProvider1, e))
                return;

            clsTextBoxUtil.LinkTextBoxWithErrorProvider(targetTextBox, "Passwords are not the same"
                , targetTextBox.Text != txtPassword.Text
                , errorProvider1, e);
        }
    }
}
