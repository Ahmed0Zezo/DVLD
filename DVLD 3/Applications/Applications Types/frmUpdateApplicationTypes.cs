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

namespace DVLD_3.Applications.Applications_Types
{
    public partial class frmUpdateApplicationTypes : Form
    {
        int _applicationTypeID;

        clsApplicationType _applicationType;

        public bool IsDataUpdated;
        void _fillFormData()
        {
            lblID.Text = _applicationType.ApplicationTypeID.ToString();
            txtTitle.Text = _applicationType.ApplicationTypeTitle;
            txtFees.Text = _applicationType.ApplicationFees.ToString();
        }

        void _fillFormDataWithDefaultValues()
        {
            lblID.Text = "???";
            txtTitle.Text = "";
            txtFees.Text = "";
        }
        public frmUpdateApplicationTypes(int ApplicationTypeID)
        {
            InitializeComponent();
            _applicationTypeID = ApplicationTypeID;

            _applicationType = clsApplicationType.FindApplicationTypeByID(_applicationTypeID);

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            IsDataUpdated = false;

            if (_applicationType != null)
            {
                _fillFormData();
            }
            else
            {
                MessageBox.Show($"Application Type with ID ({_applicationTypeID} isn't exist)","Wrong ID"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                _fillFormDataWithDefaultValues();
            }

        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            TextBox targetTextBox = (TextBox)sender;
            clsTextBoxUtil.LinkTextBoxWithErrorProvider(targetTextBox, "This field is required", string.IsNullOrEmpty(targetTextBox.Text)
                , errorProvider1, e); 

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IsDataUpdated = false;
            if (!this.ValidateChildren())
            {
                MessageBox.Show($"There is an error in some fields ", "Faild to save"
                   , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _applicationType.ApplicationTypeTitle = txtTitle.Text;
            _applicationType.ApplicationFees = Decimal.Parse(txtFees.Text);


            if(_applicationType.UpdateApplicationTypes())
            {
                MessageBox.Show($"Application Type Updated Successfully", "Updated"
                  , MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsDataUpdated = true;
            }
            else
            {
                MessageBox.Show($"Something went wrong", "Faild To Update"
                 , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        private void txtFees_TextChanged(object sender, EventArgs e)
        {
            clsTextBoxUtil.MakeTextBoxesHaveOnlyMoneyCharacters((TextBox)sender);
        }
    }
}
