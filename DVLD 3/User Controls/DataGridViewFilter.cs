using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DVLD_3.UserControls
{
    public partial class DataGridViewFilter : UserControl
    {

        public event Action<string, Type, string> FilterChanged;

        public event Action ChoosedNoneIndex;

        public DataGridViewFilter()
        {
            InitializeComponent();
        }

        bool _isFilterItemNumiricType;


        public class FilterItem
        {
            public string ItemName;
            public Type ItemType;
            public string FilterColumnName;

            public override string ToString()
            {
                return ItemName;
            }
            public FilterItem(string itemName, string filterColumnName, Type itemType)
            {
                ItemName = itemName;

                FilterColumnName = filterColumnName;


                ItemType = itemType;
            }
        }

        public Label FilterLabel
        {
            get
            {
                return lblfilter;
            }
        }

        public TextBox FilterBox
        {
            get
            {
                return txtFilteredValue;
            }
        }

        public ComboBox FilterValuesComboBox
        {
            get
            {
                return cbFilterValue;
            }
        }


        public ComboBox FilterComboBox
        {
            get
            {
                return cbFilterItems;
            }
        }

        public void AddItemsToTheFilter(List<FilterItem> filterItems)
        {

            foreach (FilterItem item in filterItems)
            {
                cbFilterItems.Items.Add(item);
            }

        }
        private void DataGridViewFilter_Load(object sender, EventArgs e)
        {

            if (cbFilterItems.SelectedIndex == cbFilterItems.FindString("None"))
            {
                txtFilteredValue.Visible = false;
                cbFilterValue.Visible = false;
            }
            else
            {
                bool IsStringOrInt = (((FilterItem)cbFilterItems.SelectedItem).ItemType == typeof(string)
                    || ((FilterItem)cbFilterItems.SelectedItem).ItemType == typeof(int));
                cbFilterValue.Visible = !IsStringOrInt;
                txtFilteredValue.Visible = IsStringOrInt;
            }

            _isFilterItemNumiricType = false;
        }

        private void txtFilteredValue_TextChanged(object sender, EventArgs e)
        {

            if (_isFilterItemNumiricType)
                clsTextBoxUtil.MakeTextBoxesHaveOnlyNumbers(txtFilteredValue);

            //Igonre None
            if (cbFilterItems.SelectedIndex != cbFilterItems.FindString("None"))
            {
                FilterChanged?.Invoke(((FilterItem)cbFilterItems.SelectedItem).FilterColumnName
                , ((FilterItem)cbFilterItems.SelectedItem).ItemType
                , txtFilteredValue.Text);
            }

        }

        private void cbFilterItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type SelectedItemType = ((FilterItem)cbFilterItems.SelectedItem).ItemType;

            txtFilteredValue.Clear();
            cbFilterValue.SelectedIndex = 0;

            if (cbFilterItems.SelectedIndex == cbFilterItems.FindString("None"))
            {
                txtFilteredValue.Visible = false;
                cbFilterValue.Visible = false;
                ChoosedNoneIndex?.Invoke();
                return;
            }


            if (SelectedItemType == typeof(string))
            {
                txtFilteredValue.Visible = true;
                cbFilterValue.Visible = false;
            }

            if (SelectedItemType == typeof(int))
            {
                _isFilterItemNumiricType = true;

                txtFilteredValue.Visible = true;
                cbFilterValue.Visible = false;
            }
            else
                _isFilterItemNumiricType = false;

            if (SelectedItemType == typeof(bool))
            {
                txtFilteredValue.Visible = false;
                cbFilterValue.Visible = true;
            }


        }

        private void cbFilterValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valueReturned = "";

            switch (cbFilterValue.Text)
            {
                case "All":
                    valueReturned = "1,0";
                    break;
                case "Yes":
                    valueReturned = "1";
                    break;
                case "No":
                    valueReturned = "0";
                    break;
            }

            FilterChanged?.Invoke(((FilterItem)cbFilterItems.SelectedItem).FilterColumnName, typeof(bool), valueReturned);

        }
    }
}
