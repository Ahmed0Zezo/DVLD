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
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        clsApplicationType(int ApplicatoinTypeID,string ApplicationTypeTitle,decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicatoinTypeID;this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
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
