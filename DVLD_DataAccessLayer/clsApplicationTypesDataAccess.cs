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
    public static class clsApplicationTypesDataAccess
    {
        public static DataTable GetAllApplicationTypes()
        {
            return clsCRUD.GetAllDataFromTable("ApplicationTypes", clsPublicSystemInfos.ConnectionString);
        }

        public static bool UpdateApplicationTypesInfoIntoDatabase(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            string Quere = @"Update ApplicationTypes
                             Set ApplicationTypeTitle = @ApplicationTypeTitle, ApplicationFees = @ApplicationFees
                             Where ApplicationTypeID = @ApplicationTypeID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("ApplicationTypeID", ApplicationTypeID, false),
                Parameters.MakeParameter("ApplicationTypeTitle", ApplicationTypeTitle, false),
                Parameters.MakeParameter("ApplicationFees", ApplicationFees, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static bool FindApplicationTypeByID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref decimal ApplicationFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"Select * From ApplicationTypes
                             Where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = (decimal)reader["ApplicationFees"];
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
