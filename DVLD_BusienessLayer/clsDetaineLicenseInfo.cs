using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsDetaineLicenseInfo
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserID { get; set; }
        public int? ReleaseApplicationID { get; set; }

        // Public constructor: empty object
        public clsDetaineLicenseInfo()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.MinValue;
            FineFees = 0;
            CreatedByUserID = 0;
            IsReleased = false;
            ReleaseDate = null;
            ReleasedByUserID = null;
            ReleaseApplicationID = null;
        }

        // Private constructor: full object
        private clsDetaineLicenseInfo(int detainID, int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID,
            bool isReleased, DateTime? releaseDate, int? releasedByUserID, int? releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
        }

        public static clsDetaineLicenseInfo FindNonReleasedDetainedLicenseInfoByLicenseID(int licenseID)
        {
            int detainID = -1;
            DateTime detainDate = DateTime.MinValue;
            decimal fineFees = 0;
            int createdByUserID = 0;
            bool isReleased = false;
            DateTime? releaseDate = null;
            int? releasedByUserID = null;
            int? releaseApplicationID = null;

            bool found = clsDetainLicensesDataAccess.FindNonReleasedDetainedLicenseInfoByLicenseID(licenseID,ref detainID,ref detainDate
                ,ref fineFees,ref createdByUserID,ref isReleased,ref releaseDate,ref releasedByUserID,ref releaseApplicationID
            );

            if (!found)
                return null;

            return new clsDetaineLicenseInfo(detainID, licenseID, detainDate, fineFees, createdByUserID,
                isReleased, releaseDate, releasedByUserID, releaseApplicationID);
        }


        public bool AddNew()
        {
            int newID = -1;
            bool result = clsDetainLicensesDataAccess.InsertNewRecord(ref newID,LicenseID,DetainDate,FineFees,CreatedByUserID,false,null,null,null);
            if (result)
            {
                DetainID = newID;
                return true;
            }
            else
            {
                DetainID = 0;
                return false;
            }
        }
        public static clsDetaineLicenseInfo FindByDetainID(int detainID)
        {
            int licenseID = 0;
            DateTime detainDate = DateTime.MinValue;
            decimal fineFees = 0;
            int createdByUserID = 0;
            bool isReleased = false;
            DateTime? releaseDate = null;
            int? releasedByUserID = null;
            int? releaseApplicationID = null;

            bool found = clsDetainLicensesDataAccess.FindByDetainID(
                detainID,
                ref licenseID,
                ref detainDate,
                ref fineFees,
                ref createdByUserID,
                ref isReleased,
                ref releaseDate,
                ref releasedByUserID,
                ref releaseApplicationID
            );

            if (!found)
                return null;

            return new clsDetaineLicenseInfo(detainID, licenseID, detainDate, fineFees, createdByUserID,
                isReleased, releaseDate, releasedByUserID, releaseApplicationID);
        }

        public static bool IsLicenseDetainedByID(int licenseID)
        {
            return clsDetainLicensesDataAccess.IsLicenseDetainedByID(licenseID);
        }

        public clsReleaseLicenseResultInfo Release(int ApplicationPersonID)
        {
            clsReleaseLicenseResultInfo releaseData = new clsReleaseLicenseResultInfo();
            releaseData.Status = true;

            if (this.DetainID == -1 || this == null)
            {
                releaseData.Status = false;
                releaseData.FaildReason = clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.DetainInfoNotExit;
                return releaseData;
            }

            clsLicense license = clsLicense.FindByLicenseID(this.LicenseID);

            if (license == null)
            {
                releaseData.Status = false;
                releaseData.FaildReason = clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.LicenseIsNotExit;
                return releaseData;
            }

            if (!license.IsActive)
            {
                releaseData.Status = false;
                releaseData.FaildReason = clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.LicenseIsNotActive;
                return releaseData;
            }

            if (!clsDetaineLicenseInfo.IsLicenseDetainedByID(license.LicenseID))
            {
                releaseData.Status = false;
                releaseData.FaildReason = clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.LicenseIsNotDetained;
                return releaseData;
            }

            releaseData.DetainedLicense = license;

            clsApplication releaseApplication = new clsApplication(5); // release app type id = 5
            releaseApplication.ApplicantPersonID = ApplicationPersonID;
            releaseApplication.ApplicationDate = DateTime.Today;
            releaseApplication.ApplicationStatus = clsApplication.ApplicationStatusEnum.Completed;
            releaseApplication.LastStatusDate = DateTime.Today;
            releaseApplication.PaidFees = releaseApplication.ApplicationType.ApplicationFees;
            releaseApplication.CreatedByUserID = clsGlobalInformations.CurrentLoggedUserID;

            if(!releaseApplication.Add())
            {
                releaseData.Status = false;
                releaseData.FaildReason = clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.FaildToAddReleaseApplication;
                return releaseData;
            }

            releaseData.ReleaseApplication = releaseApplication;

            this.ReleaseApplicationID = releaseData.ReleaseApplication.ApplicationID;
            this.IsReleased = true;
            this.ReleaseDate = DateTime.Now;
            this.ReleasedByUserID = clsGlobalInformations.CurrentLoggedUserID;

            if (!clsDetainLicensesDataAccess.UpdateReleaseDataByDetainID(this.DetainID,this.IsReleased,this.ReleaseDate
                ,this.ReleasedByUserID,this.ReleaseApplicationID))
            {
                releaseData.Status = false;
                releaseData.FaildReason = clsReleaseLicenseResultInfo.DetainedLicenseReleaseFaildReason.FaildToUpdateReleaseData;
                return releaseData;
            }

            releaseData.DetainInfo = this;

            return releaseData;

        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainLicensesDataAccess.GetAllDetainedLicenses();
        }

        
    }
}
