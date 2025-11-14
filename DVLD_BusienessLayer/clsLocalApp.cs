using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusienessLayer
{
    internal class clsLocalApp
    {
        public delegate void AddingLocalAppProccessFaild(string FailingMessage);
        public event AddingLocalAppProccessFaild AddingLocalAppFaild;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }

        
        public clsApplication Application { set; get; }
        public int LicenseClassID { get; set; }



        // Public constructor: empty object
        public clsLocalApp()
        {
            LocalDrivingLicenseApplicationID = 0;
            ApplicationID = 0;
            LicenseClassID = 0;
            Application = new clsApplication();
        }

        // Private constructor: full object
        private clsLocalApp(int localDrivingLicenseApplicationID, int applicationID, int licenseClassID)
        {
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            ApplicationID = applicationID;
            Application = clsApplication.FindByID(ApplicationID);
            LicenseClassID = licenseClassID;
        }

        public bool Add()
        {
            int newID = -1;

            if (clsPerson.IsPersonHasLocalNewDrivingLicenseAppWithClassID(this.Application.ApplicantPersonID, this.LicenseClassID))
            {
                AddingLocalAppFaild?.Invoke($@"The Person with ID ({this.Application.ApplicantPersonID}) has a new L.D.L Application 
                                            with License Class Type ({clsLicenseClass.GetLiceseClassNameByItsID(this.LicenseClassID)} already
                                            please choose another class)");
                return false;
            }


            if (clsPerson.IsPersonHasActiveLicenseWithClassID(this.Application.ApplicantPersonID,this.LicenseClassID))
            {
                AddingLocalAppFaild?.Invoke($@"The Person with ID ({this.Application.ApplicantPersonID}) has an active License
                                            with License Class Type ({clsLicenseClass.GetLiceseClassNameByItsID(this.LicenseClassID)} already
                                            please choose another class)");
                return false;
            }

            if(!this.Application.Add())
            {
                AddingLocalAppFaild?.Invoke($@"Something went wrong during saving the application,Contact the admin!");
                return false;
            }
           
                 
            if (clsLocalAppsDataAccess.InsertNewLocalDrivingLicenseApplication(ref newID, ApplicationID, LicenseClassID))
            {
                LocalDrivingLicenseApplicationID = newID;
                
                return true;
            }
            else
            {
                LocalDrivingLicenseApplicationID = -1;
                AddingLocalAppFaild?.Invoke($@"Something went wrong during saving the L.D.L Application ,Contact the admin!");
                clsApplication.DeleteApplicationByID(this.ApplicationID);
                return false;
            }
        }

        public bool Update()
        {
            return clsLocalAppsDataAccess.UpdateLocalDrivingLicenseApplicationLicenseClassID(LocalDrivingLicenseApplicationID, LicenseClassID);
        }

        public bool DeleteByID(int LocalAppID)
        {
            if(!clsApplication.DeleteApplicationByID(this.ApplicationID))
            {
                return false;
            }


            return clsLocalAppsDataAccess.DeleteLocalDrivingLicenseApplication(LocalAppID);
        }

        public static DataTable GetAll()
        {
            return clsLocalAppsDataAccess.GetAllLocalDrivingLicenseApplication();
        }

        public static clsLocalApp FindByID(int localDrivingLicenseApplicationID)
        {
            int applicationID = 0;
            int licenseClassID = 0;

            bool found = clsLocalAppsDataAccess.FindLocalDrivingLicenseApplicationByID(localDrivingLicenseApplicationID, ref applicationID, ref licenseClassID);

            if (!found)
                return null;

            return new clsLocalApp(localDrivingLicenseApplicationID, applicationID, licenseClassID);
        }
    }
}
