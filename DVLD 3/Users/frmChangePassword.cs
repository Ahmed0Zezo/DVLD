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
    public partial class frmChangePassword : Form
    {
        int _userID;

        clsUser _user;

        public event Action<string> PasswordChanged;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _userID = UserID;
        }


       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!clsTextBoxUtil.LinkTextBoxWithErrorProvider(txtCurrentPassword, "This field is required"
                 , string.IsNullOrEmpty(txtCurrentPassword.Text)
                 , errorProvider1, e))
                return;

            clsTextBoxUtil.LinkTextBoxWithErrorProvider(txtCurrentPassword, "The password isn't correct"
                , txtCurrentPassword.Text != _user.Password
                , errorProvider1, e);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            clsTextBoxUtil.LinkTextBoxWithErrorProvider(txtNewPassword, "This field is required"
                  , string.IsNullOrEmpty(txtNewPassword.Text)
                  , errorProvider1, e);

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!clsTextBoxUtil.LinkTextBoxWithErrorProvider(txtConfirmPassword, "This field is required"
                 , string.IsNullOrEmpty(txtConfirmPassword.Text)
                 , errorProvider1, e))
                return;

            clsTextBoxUtil.LinkTextBoxWithErrorProvider(txtConfirmPassword, "Passwords don't match"
                , txtConfirmPassword.Text != txtNewPassword.Text
                , errorProvider1, e);
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            user_Details1.LoadUserInfo(_userID);

            _user = user_Details1.SelectedUser;

            if (_user == null)
                this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are incorrect put the mouse over the red error and read the message"
                    , "Invalid fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _user.Password = txtNewPassword.Text;

            if(_user.UpdatePassword())
            {
                MessageBox.Show("Password Cheanged Successfully"
                    , "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PasswordChanged?.Invoke(_user.Password);


            }
            else
            {
                MessageBox.Show("Somethins went wrong!"
                    , "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }
    }
}
