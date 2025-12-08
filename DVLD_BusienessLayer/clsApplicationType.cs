using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsApplicationType
    {
        public enum enApplicationTypes {NewLocalDrivingLicense = 1,RenewDrivingLicense =2 
                ,ReplacementForLostLicense =3, ReplacementForDamagedLicense= 4,ReleaseDetainedLicense= 5,NewInternationalLicense = 6,RetakeTest = 7 }
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        clsApplicationType(int ApplicatoinTypeID,string ApplicationTypeTitle,decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicatoinTypeID;this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }

        public static int ConvertFromEnumTypeToID_Type(enApplicationTypes TheEnum)
        {
            return (int)TheEnum;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationTypes();
        }

        public bool UpdateApplicationTypes()
        {
            return clsApplicationTypesDataAccess.UpdateApplicationTypesInfoIntoDatabase(
                ApplicationTypeID, ApplicationTypeTitle, ApplicationFees
            );
        }

        public static clsApplicationType FindApplicationTypeByID(int ApplicationTypeID)
        {
            string applicationTypeTitle = "";
            decimal applicationFees = 0;

            

            if (clsApplicationTypesDataAccess.FindApplicationTypeByID(
                ApplicationTypeID, ref applicationTypeTitle, ref applicationFees))
            {
                return new clsApplicationType(ApplicationTypeID,applicationTypeTitle, applicationFees);
            }

            return null;
        }
    }
}
