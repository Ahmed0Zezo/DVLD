using DVLD_BusienessLayer;
using DVLD_3.MangePeople;
using DVLD_3.Properties;
using DVLD_3.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_3.Utils;

namespace DVLD_3.Users
{
    public partial class Manage_Users : Form
    {
        private DataTable _usersData;
        public Manage_Users()
        {
            InitializeComponent();
        }
        
        private void _refreshUsersData()
        {
            _usersData = clsUser.GetAllUsers();

            publicFormsPanel1.LinkDataToGridView(_usersData);
        }
        private void Manage_Users_Load(object sender, EventArgs e)
        {
            _usersData = clsUser.GetAllUsers();

            publicFormsPanel1.OpenFormButton.BackgroundImage = Resources.Add_New_User_32;
            publicFormsPanel1.DataViewer.AutoGenerateColumns = false;
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            publicFormsPanel1.TargetFormToClose = this;

            publicFormsPanel1.OpenFormButton.Click += OpenAddNewUserForm_Click;

            publicFormsPanel1.DataViewer.ContextMenuStrip = contextMenuStrip1;

            List<DataGridViewColumn> dataGridViewColumns = new List<DataGridViewColumn>
            {
                PublicFormsPanel.MakeTextBoxColumn("dataclmnUserID", "User ID", "UserID"),
                PublicFormsPanel.MakeTextBoxColumn("dataclmnPersonID", "Person ID", "PersonID"),
                PublicFormsPanel.MakeTextBoxColumn("dataclmnUsername", "Username", "UserName"),
                PublicFormsPanel.MakeCheckColumn("dataclmnIsActive", "Is Active", "IsActive")
            };

            publicFormsPanel1.AddColumnsToTheDataGridView(dataGridViewColumns);
            publicFormsPanel1.LinkDataToGridView(_usersData);

            List<DataGridViewFilter.FilterItem> filteritems = new List<DataGridViewFilter.FilterItem>();
            filteritems.Add(new DataGridViewFilter.FilterItem("User ID", "UserID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Person ID", "PersonID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Username", "UserName", typeof(string)));

            filteritems.Add(new DataGridViewFilter.FilterItem("Is Active", "IsActive", typeof(bool)));

            publicFormsPanel1.DataFilter.AddItemsToTheFilter(filteritems);
            publicFormsPanel1.DataFilter.FilterComboBox.SelectedIndex = 0;
        }

        private void _userUpdatedSuccessfully(int UserID)
        {
            _refreshUsersData();
        }
            

        private void OpenAddNewUserForm_Click(object sender, EventArgs e)
        {
            frmAddEditUser addEditUser = new frmAddEditUser();

            addEditUser.UserUpdatedSuccessfully += _userUpdatedSuccessfully;

            addEditUser.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser addEditUser = new frmAddEditUser(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer,0));

            addEditUser.UserUpdatedSuccessfully += _userUpdatedSuccessfully;

            addEditUser.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedID = clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer, 0);
            if (MessageBox.Show($"Are you sure that you want to delete user with ID ({selectedID}) ? ","Confirm deleting user"
                ,MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser(selectedID))
                {
                    MessageBox.Show("User Deleted Sucessully", "Deleting User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _refreshUsersData();
                }
                else
                {
                    MessageBox.Show("User didn't be deleted beacause it attached to another tabels in the system", "Deleting User"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails userDetails
                = new frmUserDetails(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer, 0));

            userDetails.ShowDialog();
        }

        private void ChangePasswordtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword
                = new frmChangePassword(clsDataGridView.GetID_FromDataGridView(publicFormsPanel1.DataViewer, 0));

            changePassword.ShowDialog();
        }
    }
}
