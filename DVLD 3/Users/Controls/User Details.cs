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

namespace DVLD_3.Users.Controls
{
    public partial class User_Details : UserControl
    {
        public User_Details()
        {
            InitializeComponent();
        }
        clsUser user = null;

        public event Action<int> onUserSelected;

        public clsUser SelectedUser
        {
            get
            {
                return user;
            }
        }
        private void _fillData()
        {
            showPersonDetails1.LoadPersonInfo(user.PersonID);
            lblUserID.Text = user.UserID.ToString();
            lblUserName.Text = user.UserName;
            lblIsActive.Text = user.IsActive == true ? "Yes" : "No";
        }

        private void _returnFormToDefualt()
        {
            showPersonDetails1.ResetDefaultValues();
            lblUserID.Text = "???";
            lblUserName.Text = "???";
            lblIsActive.Text = "???";
        }

        private void _userNotFoundMessage(int UserID)
        {
            MessageBox.Show($"User With ID ({UserID}) didn't found","UnFound User",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void _userNotFoundProcess(int UserID)
        {
            _userNotFoundMessage(UserID);
            _returnFormToDefualt();
        }
        public void LoadUserInfo(int UserID)
        {
            if (UserID == -1)
            {
                _userNotFoundProcess(UserID);
                return;
            }
                

            user = clsUser.FindUserByID(UserID);

            if (user == null)
            {
                _userNotFoundProcess(UserID);
            }
            else
            {
                _fillData();
                onUserSelected?.Invoke(UserID);
            }
        }
    }
}
