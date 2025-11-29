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
    public class clsLocalApp
    {
        public delegate void AddingLocalAppProccessFaild(string FailingMessage);
        public event AddingLocalAppProccessFaild SavingLocalDrivingLicenseAppFaild;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }

        public int PassedTests
        {
            get
            {
                return clsLocalAppsDataAccess.GetPassedTestsByLocalAppID(this.LocalDrivingLicenseApplicationID);
            }
        }


        public clsApplication Application { set; get; }
        public int LicenseClassID { get; set; }



        // Public constructor: empty object
        public clsLocalApp()
        {
            LocalDrivingLicenseApplicationID = 0;
            ApplicationID = 0;
            LicenseClassID = 0;
            Application = new clsApplication(1);
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
                SavingLocalDrivingLicenseAppFaild?.Invoke($@"The Person with ID ({this.Application.ApplicantPersonID}) has a new L.D.L Application 
with License Class Type ({clsLicenseClass.GetLiceseClassNameByItsID(this.LicenseClassID)})  already
please choose another class");
                return false;
            }


            if (clsPerson.IsPersonHasActiveLicenseWithClassID(this.Application.ApplicantPersonID,this.LicenseClassID))
            {
                SavingLocalDrivingLicenseAppFaild?.Invoke($@"The Person with ID ({this.Application.ApplicantPersonID}) has an active License
with License Class Type ({clsLicenseClass.GetLiceseClassNameByItsID(this.LicenseClassID)}) already
please choose another class");
                return false;
            }

            if(!this.Application.Add())
            {
                SavingLocalDrivingLicenseAppFaild?.Invoke($@"Something went wrong during saving the application,Contact the admin!");
                return false;
            }

            ApplicationID = Application.ApplicationID;


            if (clsLocalAppsDataAccess.InsertNewLocalDrivingLicenseApplication(ref newID, ApplicationID, LicenseClassID))
            {
                LocalDrivingLicenseApplicationID = newID;
                
                return true;
            }
            else
            {
                LocalDrivingLicenseApplicationID = -1;
                SavingLocalDrivingLicenseAppFaild?.Invoke($@"Something went wrong during saving the L.D.L Application ,Contact the admin!");
                clsApplication.DeleteApplicationByID(this.ApplicationID);
                return false;
            }
        }

        public bool UpdateLicenseClassID(int LicenseClassID)
        {
            if(this.Application.ApplicationStatus != clsApplication.ApplicationStatusEnum.New)
            {
                SavingLocalDrivingLicenseAppFaild?.Invoke($@"Either the application was canseled or completed you can edit new applicatoins only");
                return false;
            }

            if (this.LicenseClassID == LicenseClassID)
            {
                SavingLocalDrivingLicenseAppFaild?.Invoke($@"You choosed the same License Class,Please choose another one");
                return false;
            }

            if (clsPerson.IsPersonHasLocalNewDrivingLicenseAppWithClassID(this.Application.ApplicantPersonID, LicenseClassID))
            {
                SavingLocalDrivingLicenseAppFaild?.Invoke($@"The Person with ID ({this.Application.ApplicantPersonID}) has a new L.D.L Application 
with License Class Type ({clsLicenseClass.GetLiceseClassNameByItsID(LicenseClassID)})  already
please choose another class");
                return false;
            }


            if (clsPerson.IsPersonHasActiveLicenseWithClassID(this.Application.ApplicantPersonID, LicenseClassID))
            {
                SavingLocalDrivingLicenseAppFaild?.Invoke($@"The Person with ID ({this.Application.ApplicantPersonID}) has an active License
with License Class Type ({clsLicenseClass.GetLiceseClassNameByItsID(LicenseClassID)}) already
please choose another class");
                return false;
            }


            if (clsLocalAppsDataAccess.UpdateLocalDrivingLicenseApplicationLicenseClassID
                    (LocalDrivingLicenseApplicationID, LicenseClassID))
            {
                this.LicenseClassID = LicenseClassID;
                return true;
            }
            else
            {
                SavingLocalDrivingLicenseAppFaild?.Invoke($@"Something went wrong during saving the L.D.L Application ,Contact the admin!");
                return false;
            }
            
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

        public static int GetAppStatusByLocalAppID(int LocalAppID)
        {
            return clsApplicationsDataAccess.GetAppStatusByID(LocalAppID);
        }
        
        public bool DoesHaveNonLockedTestAppointmentByTestTypeID(int TestTypeID)
        {
            return clsLocalAppsDataAccess.DoesApplicationHaveNonLockedTestAppointmentByTestTypeID(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool DoesApplicaitionHaveNonLockedTestAppointmentByTestTypeID(int LocalAppID,int TestTypeID)
        {
            return clsLocalAppsDataAccess.DoesApplicationHaveNonLockedTestAppointmentByTestTypeID(LocalAppID, TestTypeID);
        }

        public bool DoesHavePassedTestsByTestTypeID(int TestTypeID)
        {
            return clsLocalAppsDataAccess.DoesApplicationHavePassTestsByTestTypeID(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool DoesApplicationHavePassedTestsByTestTypeID(int LocalAppID,int TestTypeID)
        {
            return clsLocalAppsDataAccess.DoesApplicationHavePassTestsByTestTypeID(LocalAppID, TestTypeID);
        }

        public static int GetApplicantPersonIDFromByLocalAppID(int LocalAppID)
        {
            return clsLocalAppsDataAccess.GetApplicantPersonID(LocalAppID);
        }
    }
}
