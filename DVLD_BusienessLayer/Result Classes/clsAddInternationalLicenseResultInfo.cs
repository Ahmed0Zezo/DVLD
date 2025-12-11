using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer.Result_Classes
{
    public class clsAddInternationalLicenseResultInfo
    {
        public enum AddInternationalLicenseFaildReason
        {
            LocalLicesesIsNotExist = 1, LocalLicenseIsNotActive = 2, LocalLicenseClassIsNotOrdinary = 3, FaildToAddNewApplication = 4
                ,FaildToAddNewInternationalLicense = 5,ThereAreActiveInternatinoalLicenseOnThisLicenseAlready = 6
        }

        public clsInternationalLicense NewInternationalLicense;
        public clsApplication NewApplication{ set; get; }
        public bool Status { set; get; }

        public AddInternationalLicenseFaildReason FaildReason;
    }
}
