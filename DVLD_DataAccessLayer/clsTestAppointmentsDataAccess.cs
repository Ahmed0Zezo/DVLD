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
            , decimal PaidFees,int CreatedByUserID,bool IsLocked,int? RetakeTestApplication)
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
                Parameters.MakeParameter("RetakeTestApplication", RetakeTestApplication, true)

            };

            return clsCRUD.AddNewRecordToTable(ref TestAppointmentID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }

        public static bool FindTestAppointByID(int TestAppointmentID,ref int TestTypeID,ref int LocalAppID,ref DateTime TestAppointmentDate
            ,ref decimal PaidFees,ref int CreatedByUserID,ref bool IsLocked,ref int? RetakeTestApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                    TestAppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    RetakeTestApplicationID = reader["RetakeTestApplicationID"] == DBNull.Value ? (int?)null : (int)reader["RetakeTestApplicationID"];
                    IsFound = true;
                }
            }
            catch
            {
                // Handle exceptions if necessary
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
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
                             Where TestAppointmentID = @AppointmentID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("AppointmentID", AppointmentID, false),
                Parameters.MakeParameter("AppointmentDate", AppointmentDate, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static int GetTestAppointmentTrialsByLocalAppIDAndTestTypeID(int LocalAppID,int TestTypeID)
        {
            string Quere = @"select Count(Tests.TestID) as Trials 
                             from Tests inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             where TestAppointments.LocalDrivingLicenseApplicationID = @LocalAppID and TestAppointments.TestTypeID =@TestTypeID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("LocalAppID", LocalAppID, false),
                Parameters.MakeParameter("TestTypeID", TestTypeID, false)
            };

            return clsCRUD.ReturnIntValueFromTableByQuere(Quere,parameters,clsPublicSystemInfos.ConnectionString);
        }

    }
}
