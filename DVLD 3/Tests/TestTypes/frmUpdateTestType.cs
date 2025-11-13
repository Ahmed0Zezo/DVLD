using DVLD_BusienessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_3.Tests.TestTypes
{
    public partial class frmUpdateTestType : Form
    {
        int _testTypeID;
        clsTestType _testType;
        public bool IsDataUpdated = true;
        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();
            _testTypeID = TestTypeID;
            _testType = clsTestType.FindTestTypeByID(_testTypeID);
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

            _testType.TestTypeTitle= txtTitle.Text;
            _testType.TestTypeFees = Decimal.Parse(txtFees.Text);
            _testType.TestTypeDescription = txtDescription.Text;

            if (_testType.UpdateTestTypes())
            {
                MessageBox.Show($"Test Type Updated Successfully", "Updated"
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

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            TextBox targetTextBox = (TextBox)sender;
            clsTextBoxUtil.LinkTextBoxWithErrorProvider(targetTextBox, "This field is required", string.IsNullOrEmpty(targetTextBox.Text)
                , errorProvider1, e);
        }

        void _fillFormData()
        {
            lblID.Text = _testType.TestTypeID.ToString();
            txtTitle.Text = _testType.TestTypeTitle;
            txtDescription.Text = _testType.TestTypeDescription;
            txtFees.Text = _testType.TestTypeFees.ToString();
        }

        void _fillFormDataWithDefaultValues()
        {
            lblID.Text = "???";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtFees.Text = "";
        }
        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            IsDataUpdated = false;

            if (_testType != null)
            {
                _fillFormData();
            }
            else
            {
                MessageBox.Show($"Test Type with ID ({_testTypeID} isn't exist)", "Wrong ID"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                _fillFormDataWithDefaultValues();
            }
        }
    }
}
