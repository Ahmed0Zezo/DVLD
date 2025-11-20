using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsTestAppointment
    {
        public int TestAppointmentID { set; get; }
        public int TestTypeID { set; get; }
        public string TestTypeTitle { set; get; }
        public string LicenseClassName { set; get; }
        public int LocalAppID { set; get; }
        public string FullApplicantName { set; get; }
        public DateTime AppointmentDate { set; get; }
        public double PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int? RetakeTestApplicationID { set; get; }

        public int Trials
        {
            get
            {
                if (TestTypeID == -1 || LocalAppID == -1)
                    return 0;

                else
                {
                    return clsTestAppointmentsDataAccess.GetTestAppointmentTrialsByLocalAppIDAndTestTypeID(LocalAppID,TestTypeID);
                }
            }
        }
        private clsTestAppointment(int testAppID , int testTypeID,int localApplicatoinID,DateTime appointmentDate
            , double paidFees , int createdByUserID,bool isLocked,int? retakeTestAppID)
        {
            TestAppointmentID = testAppID; TestTypeID = testTypeID; LocalAppID = localApplicatoinID; AppointmentDate = appointmentDate;
            PaidFees = paidFees; CreatedByUserID = createdByUserID; IsLocked = isLocked; RetakeTestApplicationID = retakeTestAppID;

            TestTypeTitle = clsTestType.FindTestTypeByID(TestTypeID).TestTypeTitle;

            clsLocalApp localApp = clsLocalApp.FindByID(LocalAppID);

            FullApplicantName = clsPerson.FindByID(localApp.Application.ApplicantPersonID).FullName;
            LicenseClassName = clsLicenseClass.FindLicenseClassByID(localApp.LicenseClassID).ClassName;

        }

        public clsTestAppointment()
        {
            TestAppointmentID = -1; TestTypeID = -1; LocalAppID = -1; AppointmentDate = DateTime.MinValue;
            PaidFees = 0; CreatedByUserID = -1; IsLocked = false; RetakeTestApplicationID = null;
        }

        public static clsTestAppointment FindTestAppointByID(int testAppointmentID)
        {
            int testTypeID = 0; int localAppID = 0; DateTime testAppointmentDate = DateTime.MinValue; double paidFees = 0
                ; int createdByUserID = 0; bool isLocked = false; int? retakeTestApplicationID = null;

            bool found = clsTestAppointmentsDataAccess.FindTestAppointByID(testAppointmentID, ref testTypeID, ref localAppID
                , ref testAppointmentDate, ref paidFees, ref createdByUserID, ref isLocked, ref retakeTestApplicationID);

            if (!found)
                return null;

            return new clsTestAppointment(testAppointmentID, testTypeID, localAppID, testAppointmentDate, paidFees, createdByUserID
                , isLocked, retakeTestApplicationID);
        }
        public bool AddNew()
        {
            int NewID = -1;

            if (clsTestAppointmentsDataAccess.InsertNewTestAppointment(ref NewID,this.TestTypeID,this.LocalAppID, this.AppointmentDate
                , this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID))
            {
                this.TestAppointmentID = NewID;
                return true;
            }

            return false ;
        }

        public bool UpdatedAppointmentDate(DateTime NewDate)
        {
            return clsTestAppointmentsDataAccess.UpdateTestAppointmentDateIntoDatabase(this.TestAppointmentID,this.AppointmentDate);
        }
        public static DataTable GetAllByApplicationIDAndTestTypeID_ForTable(int localAppID,int testTypeID)
        {
            return clsTestAppointmentsDataAccess.GetAllTestAppointmentsByLocalAppIDAndTestTypeID(localAppID,testTypeID);
        }
    }
}
