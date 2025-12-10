using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsReleaseLicenseResultInfo
    {
        public enum DetainedLicenseReleaseFaildReason
        {
            LicenseIsNotExit = 1, LicenseIsNotDetained = 2
                , LicenseIsNotActive = 3, DetainInfoNotExit = 4, FaildToUpdateReleaseData = 5,FaildToAddReleaseApplication = 6
        }

        public clsDetaineLicenseInfo DetainInfo { set; get; }

        public clsLicense DetainedLicense { set; get; }
        public clsApplication ReleaseApplication { set; get; }

        public bool Status { set; get; }

        public DetainedLicenseReleaseFaildReason FaildReason;
    }
}
