using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsLicenseDetainResultsInfo
    {
        public enum LicenseDetainFaildReason
        {
            LicenseIsNotActive = 1, FaildToCreateDetainRecord = 2,LicenseIsNotFound =3 ,LicenseAlreadyDetained = 4
        }

        public clsDetaineLicenseInfo DetaineRecord{ set; get; }

        public clsLicense DetainedLicense { set; get; }
        public bool Status { set; get; }

        public LicenseDetainFaildReason FaildReason;
    }
}
