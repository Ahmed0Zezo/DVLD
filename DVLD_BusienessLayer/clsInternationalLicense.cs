using DVLD_BusienessLayer.Result_Classes;
using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsInternationalLicense
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int LocalLicenseID { get; set; }
        public int DriverID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        // Public constructor: empty object
        public clsInternationalLicense()
        {
            InternationalLicenseID = 0;
            ApplicationID = 0;
            LocalLicenseID = 0;
            DriverID = 0;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            IsActive = false;
            CreatedByUserID = 0;
        }

        // Private constructor: full object
        private clsInternationalLicense(int internationalLicenseID, int applicationID, int localLicenseID, int driverID,
            DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            InternationalLicenseID = internationalLicenseID;
            ApplicationID = applicationID;
            LocalLicenseID = localLicenseID;
            DriverID = driverID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserID;
        }

        public static clsInternationalLicense FindByID(int internationalLicenseID)
        {
            int applicationID = 0;
            int localLicenseID = 0;
            int driverID = 0;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            bool isActive = false;
            int createdByUserID = 0;

            bool found = clsInternationalLicensesDataAccess.FindByInternationalLicenseID(internationalLicenseID,ref applicationID
                ,ref localLicenseID,ref driverID,ref issueDate,ref expirationDate,ref isActive,ref createdByUserID
            );

            if (!found)
                return null;

            return new clsInternationalLicense(internationalLicenseID,applicationID,localLicenseID,driverID
                ,issueDate,expirationDate,isActive,createdByUserID
            );
        }

        public bool AddNew()
        {
            int newID = -1;
            bool result = clsInternationalLicensesDataAccess.InsertNewInternationalLicense(ref newID,ApplicationID,DriverID
                ,LocalLicenseID,IssueDate,ExpirationDate,IsActive,CreatedByUserID);
            if (result)
            {
                InternationalLicenseID = newID;
                return true;
            }
            else
            {
                InternationalLicenseID = 0;
                return false;
            }
        }
        public static clsAddInternationalLicenseResultInfo AddNewInternationalLicense(int LocalLicenseID , int ApplicantPersonID)
        {
            clsAddInternationalLicenseResultInfo resultInfo = new clsAddInternationalLicenseResultInfo();
            resultInfo.Status = true;

            clsLicense LocalLicense = clsLicense.FindByLicenseID(LocalLicenseID);

            if (LocalLicense == null)
            {
                resultInfo.Status = false;
                resultInfo.FaildReason = clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.LocalLicesesIsNotExist;
                return resultInfo;
            }

            if (!LocalLicense.IsActive)
            {
                resultInfo.Status = false;
                resultInfo.FaildReason = clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.LocalLicenseIsNotActive;
                return resultInfo;
            }

            if (LocalLicense.LicenseClassID != 3)//3 => ordinary driver license
            {
                resultInfo.Status = false;
                resultInfo.FaildReason = clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.LocalLicenseClassIsNotOrdinary;
                return resultInfo;
            }

            if (clsInternationalLicense.IsLocalLicenseHasAnActiveIssuedInternationalLicenseAlready(LocalLicense.LicenseID))
            {
                resultInfo.Status = false;
                resultInfo.FaildReason = clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.ThereAreActiveInternatinoalLicenseOnThisLicenseAlready;
                return resultInfo;
            }

            clsApplication AddNewInternationalLicenseApplication = new clsApplication(6);

            AddNewInternationalLicenseApplication.ApplicantPersonID = ApplicantPersonID;
            AddNewInternationalLicenseApplication.ApplicationDate = DateTime.Now;
            AddNewInternationalLicenseApplication.ApplicationStatus = clsApplication.ApplicationStatusEnum.Completed;
            AddNewInternationalLicenseApplication.LastStatusDate = AddNewInternationalLicenseApplication.ApplicationDate;
            AddNewInternationalLicenseApplication.PaidFees = AddNewInternationalLicenseApplication.ApplicationType.ApplicationFees;
            AddNewInternationalLicenseApplication.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;

            if(!AddNewInternationalLicenseApplication.Add())
            {
                resultInfo.Status = false;
                resultInfo.FaildReason = clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.FaildToAddNewApplication;
                return resultInfo;
            }

            resultInfo.NewApplication = AddNewInternationalLicenseApplication;

            clsInternationalLicense NewInternationalLicense = new clsInternationalLicense();

            NewInternationalLicense.LocalLicenseID = LocalLicense.LicenseID;
            NewInternationalLicense.DriverID = LocalLicense.DriverID;
            NewInternationalLicense.ApplicationID = resultInfo.NewApplication.ApplicationID;
            NewInternationalLicense.IssueDate = DateTime.Now; NewInternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            NewInternationalLicense.IsActive = true;
            NewInternationalLicense.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;

            if(!NewInternationalLicense.AddNew())
            {
                clsApplication.DeleteApplicationByID(resultInfo.NewApplication.ApplicationID);
                resultInfo.Status = false;
                resultInfo.FaildReason = clsAddInternationalLicenseResultInfo.AddInternationalLicenseFaildReason.FaildToAddNewInternationalLicense;
                return resultInfo;
            }

            resultInfo.NewInternationalLicense = NewInternationalLicense;

            return resultInfo;
        }

        public static DataTable GetAll()
        {
            return clsInternationalLicensesDataAccess.GetAllInternationalLicenses();
        }

        public static DataTable GetPersonInternationalLicensesHistory(int PersonID)
        {
            return clsInternationalLicensesDataAccess.GetPersonInternationalLicensesHistoryByPersonID(PersonID);
        }

        public static bool IsLocalLicenseHasAnActiveIssuedInternationalLicenseAlready(int LocalLicenseID)
        {
            return clsInternationalLicensesDataAccess.IsLocalLicenseHasAnActiveIssuedInternationalLicenseAlready(LocalLicenseID);
        }
    }
}
