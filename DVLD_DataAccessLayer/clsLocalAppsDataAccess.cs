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
    public static class clsLocalAppsDataAccess
    {
        public static bool FindLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID,
            ref int ApplicationID,
            ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
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

        public static bool InsertNewLocalDrivingLicenseApplication(ref int LocalDrivingLicenseApplicationID,
            int ApplicationID,
            int LicenseClassID)
        {
            string Quere = @"INSERT INTO LocalDrivingLicenseApplications
                (ApplicationID, LicenseClassID)
                VALUES
                (@ApplicationID, @LicenseClassID)
                SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("ApplicationID", ApplicationID, false),
                Parameters.MakeParameter("LicenseClassID", LicenseClassID, false)
            };

            return clsCRUD.AddNewRecordToTable(ref LocalDrivingLicenseApplicationID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,
            int ApplicationID,
            int LicenseClassID)
        {
            string Quere = @"UPDATE LocalDrivingLicenseApplications SET
                ApplicationID = @ApplicationID,
                LicenseClassID = @LicenseClassID
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID, false),
                Parameters.MakeParameter("ApplicationID", ApplicationID, false),
                Parameters.MakeParameter("LicenseClassID", LicenseClassID, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static bool UpdateLocalDrivingLicenseApplicationLicenseClassID(int LocalDrivingLicenseApplicationID,int NewLicenseClassID)
        {
            string Quere = @"UPDATE LocalDrivingLicenseApplications SET
                LicenseClassID = @NewLicenseClassID
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID, false),
                Parameters.MakeParameter("NewLicenseClassID", NewLicenseClassID, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            string Quere = @"DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static DataTable GetAllLocalDrivingLicenseApplication()
        {
            return clsCRUD.GetAllDataFromTable("LocalDrivingLicenseApplications_View", clsPublicSystemInfos.ConnectionString);
        }
    }
}
