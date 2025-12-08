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

        public static clsLicense FindByLicenseID(int LicenseID, ref string ClassName, ref string ApplicantName, ref bool Gender
            , ref bool IsDetained, ref string NationalNo, ref DateTime DateOfBirth, ref string ImageLocation)
        {
            int applicationID = 0;
            int driverID = 0;
            int licenseClassID = 0;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = null;
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserID = 0;

            bool found = clsLicensesDataAccess.FindByLicenseID(LicenseID, ref applicationID, ref driverID, ref licenseClassID,
                ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID
                , ref ApplicantName, ref ClassName, ref NationalNo, ref Gender, ref IsDetained, ref DateOfBirth, ref ImageLocation);

            if (!found)
                return null;

            return new clsLicense(LicenseID, applicationID, driverID, licenseClassID, issueDate
                , expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
        }

        public static clsLicense FindByLicenseID(int LicenseID)
        {
            int applicationID = 0;
            int driverID = 0;
            int licenseClassID = 0;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = null;
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserID = 0;

            bool found = clsLicensesDataAccess.FindByLicenseID(LicenseID, ref applicationID, ref driverID, ref licenseClassID,
                ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID);

            if (!found)
                return null;

            return new clsLicense(LicenseID, applicationID, driverID, licenseClassID, issueDate
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

        public static bool DeActivateLicenseByID(int LicenseID)
        {
            return UpdateIsActiveMember(LicenseID, false);
        }

        public static bool ActivateLicenseByID(int LicenseID)
        {
            return UpdateIsActiveMember(LicenseID, true);
        }

        public static bool UpdateIsActiveMember(int LicenseID,bool Value)
        {
            return clsLicensesDataAccess.UpdateIsActiveCoulmn(LicenseID,Value);
        }

        public static clsLicenseReplacementResultsInfo RenewLicense(int OldLicenseID
            ,int ApplicantPersonID,string NewLicenseNotes)
        {
            clsLicenseReplacementResultsInfo Result = new clsLicenseReplacementResultsInfo();
            Result.Status = true;

            clsLicense oldLicese = clsLicense.FindByLicenseID(OldLicenseID);

            if (oldLicese == null)
            {
                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseDoesNotExist;
                return Result;
            }

            if (!oldLicese.IsActive)
            {
                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseIsNotActive;
                return Result;
            }

            if (oldLicese.ExpirationDate > DateTime.Today)
            {
                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseNotExpired;
                return Result;
            }

            if (!clsLicense.DeActivateLicenseByID(oldLicese.LicenseID))
            {
                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToDeActivateOldLicense;
                return Result;
            }



            clsApplication RenewApplication = new clsApplication(2);// 2=> Renew License Application Type ID

            RenewApplication.ApplicantPersonID = ApplicantPersonID;
            RenewApplication.ApplicationDate = DateTime.Now;
            RenewApplication.ApplicationStatus = clsApplication.ApplicationStatusEnum.Completed;
            RenewApplication.LastStatusDate = DateTime.Now;
            RenewApplication.PaidFees = RenewApplication.ApplicationType.ApplicationFees;
            RenewApplication.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;

            if(!RenewApplication.Add())
            {
                //Activate the olf License 
                ActivateLicenseByID(OldLicenseID);


                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToAddReplacementApplication;
                return Result;
            }

            Result.NewApplication = RenewApplication;

            clsLicense NewLicense = new clsLicense(oldLicese.LicenseClassID);
            NewLicense.DriverID = oldLicese.DriverID;
            NewLicense.ApplicationID = RenewApplication.ApplicationID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = NewLicense.IssueDate.AddYears(oldLicese.LicenseClass.DefaultValidityLength);
            NewLicense.Notes = NewLicenseNotes;
            NewLicense.PaidFees = oldLicese.LicenseClass.ClassFees;
            NewLicense.IssueReason = 2;// 2 => Issue Reason : Renew
            NewLicense.IsActive = true;
            NewLicense.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;

            if (!NewLicense.Issue())
            {
                //activate old license and delete the application
                ActivateLicenseByID(OldLicenseID);
                clsApplication.DeleteApplicationByID(RenewApplication.ApplicationID);

                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToCreateNewLicense;
                return Result;
            }

            Result.NewLicense = NewLicense;

            return Result;
        }

        public static clsLicenseReplacementResultsInfo ReplacementForDamagedOrLost(int OldLicenseID
            , int ApplicantPersonID, int ApplicationTypeID)
        {
            clsLicenseReplacementResultsInfo Result = new clsLicenseReplacementResultsInfo();
            Result.Status = true;

            clsLicense oldLicese = clsLicense.FindByLicenseID(OldLicenseID);

            if (oldLicese == null)
            {
                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseDoesNotExist;
                return Result;
            }

            if (!oldLicese.IsActive)
            {
                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.OldLicenseIsNotActive;
                return Result;
            }

            if (!clsLicense.DeActivateLicenseByID(oldLicese.LicenseID))
            {
                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToDeActivateOldLicense;
                return Result;
            }

            clsApplicationType.enApplicationTypes ApplicationType = (clsApplicationType.enApplicationTypes)ApplicationTypeID;

            if(ApplicationType != clsApplicationType.enApplicationTypes.ReplacementForLostLicense 
                && ApplicationType != clsApplicationType.enApplicationTypes.ReplacementForDamagedLicense)
            {
                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToAddReplacementApplication;
                return Result;
            }



            clsApplication ReplacementApplication = new clsApplication(ApplicationTypeID);

            ReplacementApplication.ApplicantPersonID = ApplicantPersonID;
            ReplacementApplication.ApplicationDate = DateTime.Now;
            ReplacementApplication.ApplicationStatus = clsApplication.ApplicationStatusEnum.Completed;// 
            ReplacementApplication.LastStatusDate = DateTime.Now;
            ReplacementApplication.PaidFees = ReplacementApplication.ApplicationType.ApplicationFees;
            ReplacementApplication.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;

            if (!ReplacementApplication.Add())
            {
                //Activate the old License again
                ActivateLicenseByID(OldLicenseID);

                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToAddReplacementApplication;
                return Result;
            }

            Result.NewApplication = ReplacementApplication;

            clsLicense NewLicense = new clsLicense(oldLicese.LicenseClassID);

            NewLicense.DriverID = oldLicese.DriverID;
            NewLicense.ApplicationID = ReplacementApplication.ApplicationID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = oldLicese.ExpirationDate;
            NewLicense.Notes = oldLicese.Notes;
            NewLicense.PaidFees = oldLicese.LicenseClass.ClassFees;

            NewLicense.IssueReason = ApplicationType 
                == clsApplicationType.enApplicationTypes.ReplacementForDamagedLicense ? (byte)3 : (byte)4;// 3 => Issue Reason : Damaged
            //4 => Lost

            NewLicense.IsActive = true;
            NewLicense.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;

            if (!NewLicense.Issue())
            {
                //activate old license and delete the application
                ActivateLicenseByID(OldLicenseID);
                clsApplication.DeleteApplicationByID(ReplacementApplication.ApplicationID);

                Result.Status = false;
                Result.FaildReason = clsLicenseReplacementResultsInfo.LicenseReplacementFaildReason.FaildToCreateNewLicense;
                return Result;
            }

            Result.NewLicense = NewLicense;

            return Result;
        }

    }
}
