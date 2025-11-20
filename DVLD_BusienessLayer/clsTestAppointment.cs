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
        int TestAppointmentID { set; get; }
        int TestTypeID { set; get; }
        int LocalAppID { set; get; }
        DateTime AppointmentDate { set; get; }
        double PaidFees { set; get; }
        int CreatedByUserID { set; get; }
        bool IsLocked { set; get; }
        int? RetakeTestApplicationID { set; get; }

        private clsTestAppointment(int testAppID , int testTypeID,int localApplicatoinID,DateTime appointmentDate
            , double paidFees , int createdByUserID,bool isLocked,int? retakeTestAppID)
        {
            TestAppointmentID = testAppID; TestTypeID = testTypeID; LocalAppID = localApplicatoinID; AppointmentDate = appointmentDate;
            PaidFees = paidFees; CreatedByUserID = createdByUserID; IsLocked = isLocked; RetakeTestApplicationID = retakeTestAppID;
        }

        public clsTestAppointment()
        {
            TestAppointmentID = -1; TestTypeID = -1; LocalAppID = -1; AppointmentDate = DateTime.MinValue;
            PaidFees = 0; CreatedByUserID = -1; IsLocked = false; RetakeTestApplicationID = null;
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
        public static DataTable GetAll_ForAppointmentsTable()
        {
            return clsTestAppointmentsDataAccess.GetAllTestAppointments();
        }
    }
}
