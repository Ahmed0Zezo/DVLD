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

namespace DVLD_3.Drivers
{
    public partial class frmDriversDetails : Form
    {
        DataTable _driversData;
        public frmDriversDetails()
        {
            InitializeComponent();
        }

        private void frmDriversDetails_Load(object sender, EventArgs e)
        {
            _driversData = clsDriver.GetAllDrivers();

            publicFormsPanel1.TargetFormToClose = this;

            publicFormsPanel1.OpenFormButton.Visible = false;

            publicFormsPanel1.DataViewer.ContextMenuStrip = contextMenuStrip1;

           
            publicFormsPanel1.DataViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            publicFormsPanel1.LinkDataToGridView(_driversData);

            publicFormsPanel1.DataViewer.Columns[5].Width = 145;

            List<DataGridViewFilter.FilterItem> filteritems = new List<DataGridViewFilter.FilterItem>();
            filteritems.Add(new DataGridViewFilter.FilterItem("None", "None", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Driver ID", "DriverID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Person ID", "PersonID", typeof(int)));
            filteritems.Add(new DataGridViewFilter.FilterItem("National No", "NationalNo", typeof(string)));
            filteritems.Add(new DataGridViewFilter.FilterItem("Full Name", "FullName", typeof(string)));


            publicFormsPanel1.DataFilter.AddItemsToTheFilter(filteritems);
            publicFormsPanel1.DataFilter.FilterComboBox.SelectedIndex = 0;
        }
    }
}
