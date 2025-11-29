using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsLicense
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClass;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }

        public string IssueReasonString
        {
            get
            {
                return _convertIssueReasonByteIntoString();
            }
        }
        public int CreatedByUserID { get; set; }


        private string _convertIssueReasonByteIntoString()
        {
            string Result = "";
            switch (IssueReason)
            {
                case 1:
                    Result = "First Time";
                    break;
                case 2:
                    Result = "Renew";
                    break;
                case 3:
                    Result = "Replacement for damaged";
                    break;
                case 4:
                    Result = "Replacement for lost";
                    break;
                default:
                    Result = "Non Valid";
                    break;
            }
            return Result;
        }

        // Public constructor: empty object
        public clsLicense(int licenseClassID)
        {
            LicenseClassID = licenseClassID;
            LicenseClass = clsLicenseClass.FindLicenseClassByID(LicenseClassID);
            IssueDate = DateTime.Now;

            if (LicenseClass != null)
            {
                ExpirationDate = IssueDate.AddYears(LicenseClass.DefaultValidityLength);
                PaidFees = LicenseClass.ClassFees;
            }
            else
            {
                ExpirationDate = DateTime.Now;
                PaidFees = 0;
            }

            LicenseID = 0;
            ApplicationID = 0;
            DriverID = 0;
            Notes = null;
            
            IsActive = false;
            IssueReason = 0;
            CreatedByUserID = 0;
        }

        // Private constructor: full object
        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClassID, DateTime issueDate,
            DateTime expirationDate, string notes, decimal paidFees, bool isActive, byte issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClassID = licenseClassID;

            LicenseClass = clsLicenseClass.FindLicenseClassByID(LicenseClassID);

            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;


        }

        public bool Issue()
        {
            int newID = -1;

            if(this.LicenseClass == null)
            {
                return false;
            }

            //we can't make two active licenses with the same class to the same Driver
            if (clsDriver.IsDriverHasActiveLicenseWithClassID(this.DriverID,this.LicenseClassID))
            {
                return false;
            }

            bool result = clsLicensesDataAccess.AddNew(ref newID, ApplicationID, DriverID, LicenseClassID, IssueDate,
                ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            if (result)
            {
                LicenseID = newID;
                return true;
            }
            else
            {
                LicenseID = 0;
                return false;
            }
        }

        public static DataTable GetAllByPersonID(int personID)
        {
            return clsLicensesDataAccess.GetPersonLocalLicensesHistroyInfo(personID);
        }

        public static clsLicense FindByApplicationID(int applicationID,ref string ClassName , ref string ApplicantName ,ref bool Gender 
            ,ref bool IsDetained ,ref string NationalNo,ref DateTime DateOfBirth,ref string ImageLocation)
        {
            int licenseID = 0;
            int driverID = 0;
            int licenseClassID = 0;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = null;
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserID = 0;

            bool found = clsLicensesDataAccess.FindByApplicationID(applicationID, ref licenseID, ref driverID, ref licenseClassID,
                ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID
                ,ref ApplicantName,ref ClassName,ref NationalNo,ref Gender,ref IsDetained,ref DateOfBirth,ref ImageLocation);

            if (!found)
                return null;

            return new clsLicense(licenseID, applicationID, driverID, licenseClassID, issueDate
                , expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
        }

        public static clsLicense FindByLocalApplicationID(int LocalAppID
            , ref string ClassName, ref string ApplicantName, ref bool Gender
            , ref bool IsDetained, ref string NationalNo, ref DateTime DateOfBirth,ref string ImagePath)
        {
            int applicationID = 0;
            int licenseID = 0;
            int driverID = 0;
            int licenseClassID = 0;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = null;
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserID = 0;

            bool found = clsLicensesDataAccess.FindByLocalApplicationID(LocalAppID, ref applicationID, ref licenseID, ref driverID, ref licenseClassID,
                ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID
                , ref ApplicantName, ref ClassName, ref NationalNo, ref Gender, ref IsDetained, ref DateOfBirth,ref ImagePath);

            if (!found)
                return null;

            return new clsLicense(licenseID, applicationID, driverID, licenseClassID
                , issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
        }

        public static bool IsLicenseExistByApplicationID(int applicationID)
        {
            return clsLicensesDataAccess.IsLicenseExistByApplicationID(applicationID);
        }

        public static DataTable GetPersonLocalLicensesHistroy(int PersonID)
        {
            return clsLicensesDataAccess.GetPersonLocalLicensesHistroyInfo(PersonID);
        }
    }
}
