using DVLD_3.Utils;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_3.Login
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        
        

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {

            if (clsFilesUtil.CreateGlobalInformationsFilesAndDirectoresIfNotExist(clsGlobalInformations.ProjectGolbalInformationsFolderName
                , clsGlobalInformations.RemeberMeFilePath))
            {
                //if we create the directory or the file then we don't need to get the data
                //because it was never exist!
                return;
            }

            string[] Lines = File.ReadAllLines(clsGlobalInformations.RemeberMeFilePath);

            if (Lines.Length == 2)
            {
                txtUsername.Text = Lines[0];
                txtPassword.Text = Lines[1];
            }


            chkboxRememberMe.Checked = true;
        }



        private void _remeberMeProccess(string UserName, string Password)
        {
            if (!clsFilesUtil.CreateGlobalInformationsFilesAndDirectoresIfNotExist(clsGlobalInformations.ProjectGolbalInformationsFolderName
                , clsGlobalInformations.RemeberMeFilePath))
            {
                //clear rememer me file if it was exist
                clsFilesUtil.ClearFile(clsGlobalInformations.RemeberMeFilePath);
            }

            string[] SavedLines = { txtUsername.Text, txtPassword.Text };
            //add username and password lines
            for (int i = 0; i < SavedLines.Length; i++)
            {
                File.AppendAllText(clsGlobalInformations.RemeberMeFilePath, SavedLines[i] + Environment.NewLine);
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are empty make sure that they have valus", "Empty fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          
            clsUser user = clsUser.FindByUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());

            if (user != null)
            {

                if (!user.IsActive)
                {
                    MessageBox.Show("User is not active.\nContact with the Admin", "Faild To Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (chkboxRememberMe.Checked)
                {
                    _remeberMeProccess(user.UserName, user.Password);
                }
                else
                {
                    clsFilesUtil.ClearFile(clsGlobalInformations.RemeberMeFilePath);
                }

                clsGlobalInformations.CurrentLoggedUserID = user.UserID;

                this.Hide();

                Main_Menu frmMainMenu = new Main_Menu();

                frmMainMenu.FormClosed += _mainMenuClose;

                frmMainMenu.ShowDialog();
            }
            else
            {
                MessageBox.Show("Username/Password is wrong","Faild To Login",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void _mainMenuClose(object sender, EventArgs e)
        {
            clsGlobalInformations.CurrentLoggedUserID = -1;

            if (!chkboxRememberMe.Checked)
            {
                txtUsername.Clear();
                txtPassword.Clear();
            }

            this.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBoxes_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBoxSend = ((TextBox)sender);
            if (string.IsNullOrEmpty(textBoxSend.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxSend, "This filed is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBoxSend, "");
            }

        }

        private void frmLoginScreen_FormClosing(object sender, FormClosingEventArgs e)
        {

            string Username = txtUsername.Text;
            string Password = txtPassword.Text;

            if (chkboxRememberMe.Checked)
            {
                _remeberMeProccess(Username, Password);
            }
            else
            {
                clsFilesUtil.ClearFile(clsGlobalInformations.RemeberMeFilePath);
            }
        }
    }
}
