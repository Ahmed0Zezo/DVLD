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
using System.Xml.Linq;

namespace DVLD_3.Applications.Controls
{
    public partial class ctrlLicenseInfoWithFilter : UserControl
    {
        int _selectedLicenseID;

        public Button SearchButton
        {
            get
            {
                return btnSearch;
            }
        }

       
        public clsLicense SelectedLicense
        {
            get 
            {
                return ctrlLicneseInfo1.SelectedLicense;
            }
        }

        public bool FilterEnable
        { 
            set
            {
                gbFilter.Enabled = value;
            }
        }


        public string PersonName
        {
            get
            {
                return ctrlLicneseInfo1.PersonName;
            }
        }
        public string ClassName
        {
            get
            {
                return ctrlLicneseInfo1.ClassName;
            }
        }
        public string NationalNo
        {

            get
            {
                return ctrlLicneseInfo1.NationalNo;
            }
        }
        public bool Gender
        {

            get
            {
                return ctrlLicneseInfo1.Gender;
            }
        }
        public bool IsDetained
        {

            get
            {
                return ctrlLicneseInfo1.IsDetained;
            }
        }
        public DateTime DateOfBirth
        {

            get
            {
                return ctrlLicneseInfo1.DateOfBirth;
            }
        }
        public string ImagePath
        {

            get
            {
                return ctrlLicneseInfo1.ImagePath;
            }
        }

        public event Action<int> OnLicenseSelected;


        public ctrlLicenseInfoWithFilter()
        {
            InitializeComponent();
            _selectedLicenseID = -1;
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            clsTextBoxUtil.MakeTextBoxesHaveOnlyNumbers((TextBox)sender);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                MessageBox.Show("No License ID Selected","Empty Filed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            int SearchForLicenseID;

            if(!int.TryParse(txtFilterValue.Text,out SearchForLicenseID))
            {
                MessageBox.Show("Invalid License ID Form", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(SearchForLicenseID == _selectedLicenseID)
            {
                MessageBox.Show("You already see this license info\nSearch for another one", "Same License"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrlLicneseInfo1.LoadLicenseInfoByLicenseID(SearchForLicenseID);

            
        }

        private void ctrlLicneseInfo1_OnLicenseSelected(int LicenseID)
        {
            _selectedLicenseID = LicenseID;
            OnLicenseSelected?.Invoke(_selectedLicenseID);
        }
    }
}
