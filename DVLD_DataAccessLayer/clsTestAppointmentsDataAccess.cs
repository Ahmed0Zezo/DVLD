using DataAccess_Queries;
using DataAccessLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public static class clsTestAppointmentsDataAccess
    {
        public static bool InsertNewTestAppointment(ref int TestAppointmentID, int TestTypeID, int LocalAppID, DateTime TestAppointmentDate
            ,double PaidFees,int CreatedByUserID,bool IsLocked,int? RetakeTestApplication)
        {
            string Quere = @"INSERT INTO TestAppointments
                            (TestTypeID, LocalDrivingLicenseApplicationID
                            , AppointmentDate,PaidFees,CreatedByUserID,IsLocked,RetakeTestApplicationID)
                            VALUES
                            (@TestTypeID, @LocalAppID, @TestAppointmentDate,@PaidFees,@CreatedByUserID,@IsLocked,@RetakeTestApplication)
                            SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("TestTypeID", TestTypeID, false),
                Parameters.MakeParameter("LocalAppID", LocalAppID, false),
                Parameters.MakeParameter("TestAppointmentDate", TestAppointmentDate, false),
                Parameters.MakeParameter("PaidFees", PaidFees, false),
                Parameters.MakeParameter("CreatedByUserID", CreatedByUserID, false),
                Parameters.MakeParameter("IsLocked", IsLocked, false),
                Parameters.MakeParameter("IsLocked", RetakeTestApplication, true)

            };

            return clsCRUD.AddNewRecordToTable(ref TestAppointmentID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }

        public static DataTable GetAllTestAppointmentsByLocalAppIDAndTestTypeID(int LocalAppID,int TestTypeID)
        {
            string Quere = @"select TestAppointmentID as AppointmentID,AppointmentDate,PaidFees as [Paid Fees]
                            , IsLocked as [Is Locked] from TestAppointments
                             where LocalDrivingLicenseApplicationID = @LocalAppID and TestTypeID = @TestTypeID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("LocalAppID",LocalAppID,false),
                Parameters.MakeParameter("TestTypeID",TestTypeID,false)
            };

            return clsCRUD.GetAllDataFromTableByQuere(Quere,parameters, clsPublicSystemInfos.ConnectionString);
        }

        public static bool UpdateTestAppointmentDateIntoDatabase(int AppointmentID, DateTime AppointmentDate)
        {
            string Quere = @"Update TestAppointments
                             Set AppointmentDate = @AppointmentDate
                             Where AppointmentID = @AppointmentID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("AppointmentID", AppointmentID, false),
                Parameters.MakeParameter("AppointmentDate", AppointmentDate, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }
    }
}
