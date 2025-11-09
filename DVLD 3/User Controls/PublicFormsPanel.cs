using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DVLD_3.UserControls
{
    public partial class PublicFormsPanel : UserControl
    {
        public PublicFormsPanel()
        {
            InitializeComponent();
        }

        DataTable _data;

        Form _targerFormToClose;


        public Form TargetFormToClose
        {
            set
            {
                _targerFormToClose = value;
            }
        }


        public Button OpenFormButton
        {
            get
            {
                return btnOpenForm;
            }
        }

        public Button CloseFormButton
        {
            get
            {
                return btnClose;
            }
        }

        public DataGridView DataViewer
        {
            get
            {
                return dataGridView;
            }
        }

        public Label RecordsNumberLabel
        {
            get
            {
                return lblRecordsNumber;
            }
        }

        public DataGridViewFilter DataFilter
        {
            get
            {
                return dataGridViewFilter1;
            }
        }

        public void LinkDataToGridView(DataTable dataTable)
        {
            _data = dataTable;
            dataGridView.DataSource = _data;
            lblRecordsNumber.Text = _data.Rows.Count.ToString();


        }

        public static DataGridViewTextBoxColumn MakeTextBoxColumn(string ColumnName, string HeaderText, string DataPropertyName)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

            column.Name = ColumnName;
            column.HeaderText = HeaderText;
            column.DataPropertyName = DataPropertyName;

            return column;
        }

        public static DataGridViewCheckBoxColumn MakeCheckColumn(string ColumnName, string HeaderText, string DataPropertyName)
        {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();

            column.Name = ColumnName;
            column.HeaderText = HeaderText;
            column.DataPropertyName = DataPropertyName;

            return column;
        }

        public void AddColumnsToTheDataGridView(List<DataGridViewColumn> columns)
        {
            if (columns == null)
                return;

            if (columns.Count > 0)
            {
                foreach (DataGridViewColumn column in columns)
                {
                    dataGridView.Columns.Add(column);
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_targerFormToClose != null)
                _targerFormToClose.Close();
        }



        private void PublicFormsPanel_Load(object sender, EventArgs e)
        {
            dataGridViewFilter1.FilterChanged += _filterChanged;
        }


        private void _filterChanged(string ColumnName, Type itemType, string FilterValue)
        {


            if (string.IsNullOrEmpty(FilterValue) || (itemType == typeof(bool) && FilterValue == "1,0"))//"1,0" meaning all
            {
                dataGridView.DataSource = _data;
                lblRecordsNumber.Text = _data.Rows.Count.ToString();
                return;
            }



            DataRow[] filterdRows;

            if (itemType == typeof(string))
            {

                filterdRows = _data.Select($"{ColumnName} LIKE '%{FilterValue}%'");
            }
            else
            {
                filterdRows = _data.Select($"{ColumnName} = {FilterValue}");
            }


            if (filterdRows.Count() > 0)
            {
                dataGridView.DataSource = filterdRows.CopyToDataTable();
                lblRecordsNumber.Text = filterdRows.Count().ToString();
            }
            else
            {
                lblRecordsNumber.Text = "0";
                dataGridView.DataSource = null;
            }


        }

    }
}
