using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsLicenseReplacementResultsInfo
    {
        public enum LicenseReplacementFaildReason {FaildToAddReplacementApplication = 1,FaildToDeActivateOldLicense = 2
                ,OldLicenseNotExpired = 3,FaildToCreateNewLicense =4 ,OldLicenseDoesNotExist= 5,OldLicenseIsNotActive = 6}

        public clsLicense NewLicense { set; get; }

        public clsApplication NewApplication { set; get; }

        public bool Status { set; get; }

        public LicenseReplacementFaildReason FaildReason;


    }
}
