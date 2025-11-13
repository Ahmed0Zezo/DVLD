using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsApplication
    {
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationType { get; set; }
        public ApplicationStatusEnum ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }


        public clsApplication()
        {
            ApplicationID = 0;
            ApplicantPersonID = 0;
            ApplicationDate = DateTime.MinValue;
            ApplicationTypeID = 0;
            ApplicationStatus = ApplicationStatusEnum.New;
            LastStatusDate = DateTime.MinValue;
            PaidFees = 0;
            CreatedByUserID = 0;
        }

        
        private clsApplication(int applicationID,int applicantPersonID,DateTime applicationDate
            ,int applicationTypeID,ApplicationStatusEnum applicationStatus,DateTime lastStatusDate,decimal paidFees,int createdByUserID)
        {
            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;

            ApplicationType = clsApplicationType.FindApplicationTypeByID(ApplicationTypeID);

            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
        }
        public enum ApplicationStatusEnum : byte
        {
            New = 1,
            Cancled = 2,
            Completed = 3
        }

        public static byte StatusEnumToByte(ApplicationStatusEnum status)
        {
            return (byte)status;
        }

        public static ApplicationStatusEnum ByteToStatusEnum(byte status)
        {
            switch (status)
            {
                case 1: return ApplicationStatusEnum.New;
                case 2: return ApplicationStatusEnum.Cancled;
                case 3: return ApplicationStatusEnum.Completed;
                default: throw new ArgumentOutOfRangeException(nameof(status), "Invalid status value");
            }
        }

        public bool Add()
        {
            int NewID = -1;

            if (clsApplicationsDataAccess.InsertNewApplication(ref NewID,ApplicantPersonID,ApplicationDate,ApplicationTypeID
                ,StatusEnumToByte(ApplicationStatus),LastStatusDate,PaidFees,CreatedByUserID))
            {
                this.ApplicantPersonID = NewID;
                return true;
            }
            else
            {
                this.ApplicantPersonID = -1;
                return false ;
            }
                
        }

        public bool Update()
        {
            return clsApplicationsDataAccess.UpdateApplication(ApplicationID,ApplicantPersonID,ApplicationDate,ApplicationTypeID
                ,StatusEnumToByte(ApplicationStatus),LastStatusDate,PaidFees,CreatedByUserID);
        }

        public static bool DeleteApplicationByID(int ApplicationID)
        {
            return clsApplicationsDataAccess.DeleteApplicationByID(ApplicationID);
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationsDataAccess.GetAllApplications();
        }

        public static clsApplication FindByID(int applicationID)
        {
            int applicantPersonID = 0;
            DateTime applicationDate = DateTime.MinValue;
            int applicationTypeID = 0;
            byte applicationStatus = 1;
            DateTime lastStatusDate = DateTime.MinValue;
            decimal paidFees = 0;
            int createdByUserID = 0;

            bool found = clsApplicationsDataAccess.FindApplicationByID(applicationID,ref applicantPersonID,ref applicationDate,ref applicationTypeID
                ,ref applicationStatus,ref lastStatusDate,ref paidFees,ref createdByUserID);

            if (!found)
                return null;

            return new clsApplication
            (applicationID,applicantPersonID,applicationDate,applicationTypeID,ByteToStatusEnum(applicationStatus),lastStatusDate
            ,paidFees,createdByUserID);
        }

       
    }
}
