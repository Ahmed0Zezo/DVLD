using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsLicenseClass
    {
        public int LicenseClassID { get; set; }

        public string ClassName { get; set; }
        public string ClassDescription { get; set; }

        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }

        public decimal ClassFees { get; set; }

        clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription
            , int MinimumAllowedAge,int DefaultValidityLength, decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID; this.ClassName = ClassName;
            this.ClassDescription = ClassDescription; this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength; this.ClassFees = ClassFees;
        }
        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesDataAccess.GetAllLicenseClasses();
        }

      

        public static clsLicenseClass FindLicenseClassByID(int LicenseClassID)
        {
            string className = "";
            string classDescription = "";

            int minimumAllowedAge = 0;
            int defaultValidityLength = 0;

            decimal classFees = 0;



            if (clsLicenseClassesDataAccess.FindLicenseClassByID(
                LicenseClassID, ref className, ref classDescription, ref minimumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clsLicenseClass(LicenseClassID, className, classDescription, minimumAllowedAge,defaultValidityLength,classFees);
            }

            return null;
        }
    }
}
