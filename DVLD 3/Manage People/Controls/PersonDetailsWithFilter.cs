using DVLD_BusienessLayer;
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

namespace DVLD_3.MangePeople.Controls
{
    public partial class PersonDetailsWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;

        public event Action OnPersonUpdated;
        public PersonDetailsWithFilter()
        {
            InitializeComponent();
        }

        int _currentPersonID;

        private bool _filterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _filterEnabled;
            }

            set
            {
                _filterEnabled = value;
                gbFilter.Enabled = _filterEnabled;
            }
        }

        private bool _showAddPerson = true;

        public bool ShowAddPerson
        {
            get
            {
                return _showAddPerson;
            }

            set
            {
                _showAddPerson = value;
                btnAddPerson.Visible = _showAddPerson;
            }
        }

        public clsPerson SelectedPerson
        {
            get
            {
                return showPersonDetails1.SelectedPersonInfo;
            }
        }

        private void PersonDetailsWithFilter_Load(object sender, EventArgs e)
        {
            _currentPersonID = -1;
            dataGridViewFilter1.FilterLabel.Text = "Find By";

            List<DataGridViewFilter.FilterItem> filterItems = new List<DataGridViewFilter.FilterItem>();

            filterItems.Add(new DataGridViewFilter.FilterItem("Person ID", "PersonID", typeof(int)));

            dataGridViewFilter1.AddItemsToTheFilter(filterItems);
            dataGridViewFilter1.FilterComboBox.SelectedIndex = 0;
            showPersonDetails1.PersonUpdated += PersonDataUpdated;
        }



        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dataGridViewFilter1.FilterBox.Text))
            {
                int PersonID = int.Parse(dataGridViewFilter1.FilterBox.Text);

                if (PersonID != _currentPersonID)
                {
                    showPersonDetails1.LoadPersonInfo(PersonID);

                    //make sure that we found the person then Invoke
                    if (showPersonDetails1.SelectedPersonInfo != null)
                    {

                        OnPersonSelected?.Invoke(PersonID);
                    }
                    else
                    {

                        //if person not found will send -1 as a ref for not found
                        OnPersonSelected?.Invoke(-1);
                    }
                    _currentPersonID = PersonID;
                }

            }

        }

        private void PersonDataUpdated()
        {
            OnPersonUpdated?.Invoke();
        }
        public void SearchForPerson(int PersonID)
        {
            //search for the given person id
            showPersonDetails1.LoadPersonInfo(PersonID);
            dataGridViewFilter1.FilterBox.Text = PersonID.ToString();
            _currentPersonID = PersonID;
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddEditPeopleForm addEditPeopleForm = new AddEditPeopleForm();

            addEditPeopleForm.OnPersonSavedSuccessfully += SearchForPerson;

            addEditPeopleForm.ShowDialog();
        }
    }
}
