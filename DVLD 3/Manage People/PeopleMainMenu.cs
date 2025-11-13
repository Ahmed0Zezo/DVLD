using DVLD_3.MangePeople;
using DVLD_3.UserControls;
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

namespace DVLD_3
{
    public partial class PeopleMainMenu : Form
    {
        private DataTable _peopleData;

        public PeopleMainMenu()
        {
            InitializeComponent();
        }

        private void _refreshPeopleData()
        {
            _peopleData = clsPerson.GetAllPeople();

            publicFormsPanel1.LinkDataToGridView(_peopleData);
        }

        private int _getSelectedPersonIDFromDataGridView()
        {
            return (int)publicFormsPanel1.DataViewer.SelectedCells[0].Value;
        }
        private void _personSavedSuccessfully(int PersonID)
        {
            MessageBox.Show($"Person With ID : {PersonID} Saved Successfully", "Saving Person", MessageBoxButtons.OK
                , MessageBoxIcon.Information);

            _refreshPeopleData();
        }
        private void PeopleMainMenu_Load(object sender, EventArgs e)
        {
            // Preparing Form
            _peopleData = clsPerson.GetAllPeople();

            publicFormsPanel1.TargetFormToClose = this;

            publicFormsPanel1.OpenFormButton.Click += addNewPersonFormOpen_Click;

            publicFormsPanel1.DataViewer.ContextMenuStrip = contextMenuStrip1;


            List<DataGridViewColumn> dataGridViewColumns = new List<DataGridViewColumn>();

            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnPersonID", "PersonID", "PersonID"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnNationalNo", "NationalNo", "NationalNo"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnFirstName", "FirstName", "FirstName"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnSecondName", "SecondName", "SecondName"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnThirdName", "ThirdName", "ThirdName"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnLastName", "LastName", "LastName"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnDateOfBirth", "DateOfBirth", "DateOfBirth"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnGendor", "Gendor", "Gendor"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnAddress", "Address", "Address"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnPhone", "Phone", "Phone"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnEmail", "Email", "Email"));
            dataGridViewColumns.Add(PublicFormsPanel.MakeTextBoxColumn("dataclmnNationalityCountryID", "NationalityCountryID", "NationalityCountryID"));

            publicFormsPanel1.DataViewer.AutoGenerateColumns = false;
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            publicFormsPanel1.AddColumnsToTheDataGridView(dataGridViewColumns);
            publicFormsPanel1.LinkDataToGridView(_peopleData);


            List<DataGridViewFilter.FilterItem> filteritems = new List<DataGridViewFilter.FilterItem>();
            filteritems.Add(new DataGridViewFilter.FilterItem("None", "None", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Person ID", "PersonID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("National No", "NationalNo", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("First Name", "FirstName", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Second Name", "SecondName", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Third Name", "ThirdName", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Last Name", "LastName", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Nationality", "NationalityCountryID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Gendor", "Gendor", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Phone", "Phone", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Email", "Email", typeof(string)));

            publicFormsPanel1.DataFilter.AddItemsToTheFilter(filteritems);
            publicFormsPanel1.DataFilter.FilterComboBox.SelectedIndex = 0;
        }


        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = _getSelectedPersonIDFromDataGridView();

            ShowPersonDetailsForm showPersonDetailsForm
                = new ShowPersonDetailsForm(PersonID);

            showPersonDetailsForm.ShowDialog();

            if (showPersonDetailsForm.IsPersonDataUpdated)
            {
                _personSavedSuccessfully(PersonID);
            }

        }

        private void addNewPersonFormOpen_Click(object sender, EventArgs e)
        {
            AddEditPeopleForm addEditPeople = new AddEditPeopleForm();

            addEditPeople.OnPersonSavedSuccessfully += _personSavedSuccessfully;

            addEditPeople.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPeopleForm addEditPeople = new AddEditPeopleForm(_getSelectedPersonIDFromDataGridView());

            addEditPeople.OnPersonSavedSuccessfully += _personSavedSuccessfully;

            addEditPeople.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("We didn't integrate this feature yet , but we will do", "Upcoming feature"
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void callToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("We didn't integrate this feature yet , but we will do", "Upcoming feature"
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonID = _getSelectedPersonIDFromDataGridView();
            if (MessageBox.Show($"Are you sure that you want to delete person with ID ({SelectedPersonID}) ?"
                , "Deleting Person Validation"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(SelectedPersonID))
                {
                    MessageBox.Show($"Person Deleted Successfully"
                        , "Deleting Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _refreshPeopleData();
                }
                else
                {
                    MessageBox.Show($@"The Person didn't be deleted ,Person With ID ({SelectedPersonID}) has another Informations in another tables"
                        , "Deleting Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int SelectedPersons = publicFormsPanel1.DataViewer.SelectedRows.Count;

            if (SelectedPersons == 0)
            {
                showDetailsToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = false;
            }
            else if (SelectedPersons == 1)
            {
                showDetailsToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = true;
            }
            else if (SelectedPersons > 1)
            {
                showDetailsToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = false;
            }
        }

       
    }
}
