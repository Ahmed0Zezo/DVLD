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
    public partial class AddEditLocalDrivingLicenseApplication : Form
    {
        public event Action<int> ApplicationSavedSuccessfully;

        int _applicationID;
        clsLocalApp _localApp;
        clsEnumsUtil.enFormMode _mode;
        public AddEditLocalDrivingLicenseApplication(int ApplicationID = -1)
        {
            InitializeComponent();
            _applicationID = ApplicationID;

            if(_applicationID != -1)
            {
                _localApp = clsLocalApp.FindByID(_applicationID);

                if (_localApp != null)
                {
                    _mode = clsEnumsUtil.enFormMode.eUpdate;
                    _localApp.SavingLocalDrivingLicenseAppFaild += _updatingLocalApplicationFaild;
                }
            }
            else
            {
                _localApp = new clsLocalApp();
                _mode = clsEnumsUtil.enFormMode.eAddNew;
                _localApp.SavingLocalDrivingLicenseAppFaild += _addingLocalApplicationFaild;
            }


            
        }


        //////////Form Loading
        private void _prepareUIForAddMode()
        {
            btnSave.Enabled = false;
            _enableApplicationInfoControls(false);
            

            lblHeader.Text = "New Local Driving License Application";
            personDetailsWithFilter1.FilterEnabled = true;

            lblApplicationID.Text = "???";
            cbLicenseClass.SelectedIndex = 2;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = _localApp.Application.PaidFees.ToString();
            lblCreatedBy.Text = clsUser.FindUserByID(_localApp.Application.CreatedByUserID).UserName;
        }

        private void _prepareUIForUpdateMode()
        {
            btnSave.Enabled = true;
            _enableApplicationInfoControls(true);

            lblHeader.Text = "Update Local Driving License Application";
            personDetailsWithFilter1.FilterEnabled = false;

            personDetailsWithFilter1.SearchForPerson(_localApp.Application.ApplicantPersonID);

            lblApplicationID.Text = _localApp.ApplicationID.ToString();
            cbLicenseClass.SelectedIndex = _localApp.LicenseClassID -1;
            lblApplicationDate.Text = _localApp.Application.ApplicationDate.ToShortDateString();
            lblApplicationFees.Text = _localApp.Application.PaidFees.ToString();
            lblCreatedBy.Text = clsUser.FindUserByID(_localApp.Application.CreatedByUserID).UserName;
        }

        private void _fillComboBoxWithLicenseClasses()
        {
            cbLicenseClass.DataSource = clsLicenseClass.GetAllLicenseClasses();
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
        }

        private void _fillApplicationInfoForAddNew()
        {
            _localApp.Application.ApplicationDate = DateTime.Now;
            _localApp.Application.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;
            _localApp.Application.LastStatusDate = DateTime.Now;
            _localApp.Application.PaidFees = _localApp.Application.ApplicationType.ApplicationFees;
            

        }

        private void _personSelectedChanged(int PersonID)
        {
            //disable App Controls until choosing a person 
            //will ebabled after clicking on (Next) button
            if (PersonID == -1)
            {
                btnSave.Enabled = false;
                _enableApplicationInfoControls(false);

            }
        }
        private void AddEditLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            personDetailsWithFilter1.OnPersonSelected += _personSelectedChanged;
            _fillComboBoxWithLicenseClasses();
            

            switch (_mode)
            {
                case clsEnumsUtil.enFormMode.eAddNew:
                    _fillApplicationInfoForAddNew();
                    _prepareUIForAddMode();
                    break;
                case clsEnumsUtil.enFormMode.eUpdate:
                    _prepareUIForUpdateMode();
                    break;
                default:
                    MessageBox.Show("Invalid Local App ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                    
            }
        }

        



       /////////////////// Save Proccess and functions
        private void _fillLocalAppInfoFromFormForAdding()
        {
            _fillLocalAppInfoFromFormForUpdating();
            _localApp.LicenseClassID = (int)cbLicenseClass.SelectedValue;
            _localApp.Application.ApplicantPersonID = personDetailsWithFilter1.SelectedPerson.PersonID;
            
        }

        private void _fillLocalAppInfoFromFormForUpdating()
        {
            _localApp.Application.LastStatusDate = DateTime.Now;
        }

        private bool _saveInAddMode()
        {
            _fillLocalAppInfoFromFormForAdding();
            return _localApp.Add() ;
        }

        private bool _saveInUpdateMode()
        {
            _fillLocalAppInfoFromFormForUpdating();
            return _localApp.UpdateLicenseClassID((int)cbLicenseClass.SelectedValue);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            bool IsSaved = false;

            if (_mode == clsEnumsUtil.enFormMode.eAddNew)
            {
                if (_saveInAddMode())
                    IsSaved = true;
             
            }
            else
            {
                if (_saveInUpdateMode())
                    IsSaved = true;
                
            }

            if (IsSaved)
            {
                MessageBox.Show("Application Saved Successfully", "Application Saved"
                                , MessageBoxButtons.OK, MessageBoxIcon.Information);
                ApplicationSavedSuccessfully?.Invoke(_localApp.LocalDrivingLicenseApplicationID);
                this.Close();
            }
        }

        ////////Next Button
        private void _enableApplicationInfoControls(bool Enabled)
        {
            tabPage2.Enabled = Enabled;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (personDetailsWithFilter1.SelectedPerson == null)
            {
                MessageBox.Show("Please select a person", "Person not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;
            _enableApplicationInfoControls(true);

        }


        //////Faild in add local application subscribed functions
        private void _addingLocalApplicationFaild(string FailingMessage)
        {
            MessageBox.Show(FailingMessage, "Add New Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _updatingLocalApplicationFaild(string FailingMessage)
        {
            MessageBox.Show(FailingMessage, "Update Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }
}
