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
    public static class clsTestTypesDataAccess
    {
        public static DataTable GetAllTestTypes()
        {
            return clsCRUD.GetAllDataFromTable("TestTypes", clsPublicSystemInfos.ConnectionString);
        }

        public static bool UpdateTestTypesInfoIntoDatabase(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            string Quere = @"Update TestTypes
                             Set TestTypeTitle = @TestTypeTitle, TestTypeDescription = @TestTypeDescription, TestTypeFees = @TestTypeFees
                             Where TestTypeID = @TestTypeID";

            var parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("TestTypeID", TestTypeID, false),
                Parameters.MakeParameter("TestTypeTitle", TestTypeTitle, false),
                Parameters.MakeParameter("TestTypeDescription", TestTypeDescription, false),
                Parameters.MakeParameter("TestTypeFees", TestTypeFees, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static bool FindTestTypeByID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref decimal TestTypeFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"Select * From TestTypes
                             Where TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = (decimal)reader["TestTypeFees"];
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
    }
}
