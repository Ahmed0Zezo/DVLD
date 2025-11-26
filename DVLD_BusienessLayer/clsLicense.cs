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

        public clsLicenseClass LicensesClass;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public double PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        // Public constructor: empty object
        public clsLicense(int licenseClassID)
        {
            LicenseClassID = licenseClassID;
            LicensesClass = clsLicenseClass.FindLicenseClassByID(LicenseClassID);
            IssueDate = DateTime.Now;

            if (LicensesClass != null)
            {
                ExpirationDate = IssueDate.AddYears(LicensesClass.DefaultValidityLength);
            }
            else
            {
                ExpirationDate = DateTime.Now;
            }

            LicenseID = 0;
            ApplicationID = 0;
            DriverID = 0;
            Notes = null;
            PaidFees = 0;
            IsActive = false;
            IssueReason = 0;
            CreatedByUserID = 0;
        }

        // Private constructor: full object
        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClassID, DateTime issueDate,
            DateTime expirationDate, string notes, double paidFees, bool isActive, byte issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClassID = licenseClassID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
        }

        public bool AddNew()
        {
            int newID = -1;
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
            return clsLicensesDataAccess.GetAllByPersonID(personID);
        }

        public static clsLicense FindByApplicationID(int applicationID)
        {
            int licenseID = 0;
            int driverID = 0;
            int licenseClassID = 0;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = null;
            double paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserID = 0;

            bool found = clsLicensesDataAccess.FindByApplicationID(applicationID, ref licenseID, ref driverID, ref licenseClassID,
                ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID);

            if (!found)
                return null;

            return new clsLicense(licenseID, applicationID, driverID, licenseClassID, issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
        }

        public static bool IsLicenseExistByApplicationID(int applicationID)
        {
            return clsLicensesDataAccess.IsLicenseExistByApplicationID(applicationID);
        }
    }
}
