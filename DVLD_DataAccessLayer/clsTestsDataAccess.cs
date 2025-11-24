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
    public class clsTestsDataAccess
    {
        public static bool InsertNewTest(ref int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            string Quere = @"INSERT INTO Tests
                            (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                            VALUES
                            (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID)
                            SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("TestAppointmentID", TestAppointmentID, false),
                Parameters.MakeParameter("TestResult", TestResult, false),
                Parameters.MakeParameter("Notes", Notes, true),
                Parameters.MakeParameter("CreatedByUserID", CreatedByUserID, false)
            };

            return clsCRUD.AddNewRecordToTable(ref TestID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }

        public static DataTable GetAllTests()
        {
            return clsCRUD.GetAllDataFromTable("Tests", clsPublicSystemInfos.ConnectionString);
        }

        public static bool FindTestByTestAppointmentID(int TestAppointmentID, ref int TestID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"SELECT * FROM Tests WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestID = (int)reader["TestID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = reader["Notes"] == DBNull.Value ? null : (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static bool UpdateNotes(int TestID, string Notes)
        {
            string Quere = @"UPDATE Tests SET Notes = @Notes WHERE TestID = @TestID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("TestID", TestID, false),
                Parameters.MakeParameter("Notes", Notes, true)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }


    }
}
