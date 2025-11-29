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
    public static class clsDriversDataAccess
    {

        public static bool IsDriverExistByPersonID(int PersonID)
        {
            string Quere = @"select Count(DriverID) from Drivers
                             where PersonID = @PersonID";

            List<SqlParameter> parameters = new List<SqlParameter> { Parameters.MakeParameter("PersonID", PersonID, false) };

            return clsCRUD.IsRecordExistInByQuereCondition(Quere,parameters,clsPublicSystemInfos.ConnectionString);
        }
        public static bool InsertNewDriver(ref int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            string Quere = @"INSERT INTO Drivers
                            (PersonID, CreatedByUserID, CreatedDate)
                            VALUES
                            (@PersonID, @CreatedByUserID, @CreatedDate)
                            SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("PersonID", PersonID, false),
                Parameters.MakeParameter("CreatedByUserID", CreatedByUserID, false),
                Parameters.MakeParameter("CreatedDate", CreatedDate, false)
            };

            return clsCRUD.AddNewRecordToTable(ref DriverID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }
        public static bool FindDriverIDByPersonID(int PersonID, ref int DriverID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"SELECT DriverID FROM Drivers WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DriverID = (int)reader["DriverID"];
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


        public static bool FindDriverByID(int DriverID, ref int PersonID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"SELECT DriverID FROM Drivers WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DriverID = (int)reader["DriverID"];
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

        public static DataTable GetAllDrivers()
        {
            return clsCRUD.GetAllDataFromTable("Drivers_View", clsPublicSystemInfos.ConnectionString);
        }

        public static bool DeleteDriver(int DriverID)
        {
            string Quere = @"delete from Drivers where DriverID = @DriverID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("DriverID", DriverID, false),
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString,Quere,parameters) > 0;
        }

        public static bool IsDriverHasActiveLicenseWithClassID(int DriverID , int ClassID)
        {
            string Quere = @"select Count(LicenseID) from Licenses
                            where DriverID = @DriverID and LicenseClass = @ClassID and IsActive = 1";

            List<SqlParameter> parameters = new List<SqlParameter>
            { Parameters.MakeParameter("DriverID", DriverID, false),
              Parameters.MakeParameter("ClassID", ClassID, false)
            };

            return clsCRUD.IsRecordExistInByQuereCondition(Quere, parameters, clsPublicSystemInfos.ConnectionString);
        }
    }
}
