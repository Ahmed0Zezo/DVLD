using DataAccess_Queries;
using DataAccessLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer
{
    public static class clsDetainLicensesDataAccess
    {
        public static bool InsertNewRecord(ref int DetainID,int LicenseID,DateTime DetainDate,decimal FineFees,int CreatedByUserID,bool IsReleased
            ,DateTime? ReleaseDate,int? ReleasedByUserID,int? ReleaseApplicationID)
        {
            string Quere = @"INSERT INTO DetainedLicenses
                (LicenseID,DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID)
                VALUES
                (@LicenseID,@DetainDate, @FineFees, @CreatedByUserID, @IsReleased, @ReleaseDate, @ReleasedByUserID, @ReleaseApplicationID)
                SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("LicenseID", LicenseID, false),
                Parameters.MakeParameter("DetainDate", DetainDate, false),
                Parameters.MakeParameter("FineFees", FineFees, false),
                Parameters.MakeParameter("CreatedByUserID", CreatedByUserID, false),
                Parameters.MakeParameter("IsReleased", IsReleased, false),
                Parameters.MakeParameter("ReleaseDate", ReleaseDate, true),
                Parameters.MakeParameter("ReleasedByUserID", ReleasedByUserID, true),
                Parameters.MakeParameter("ReleaseApplicationID", ReleaseApplicationID, true)
            };

            return clsCRUD.AddNewRecordToTable(ref DetainID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }

        public static bool UpdateReleaseDataByDetainID(int DetainID,bool IsReleased
            ,DateTime? ReleaseDate,int? ReleasedByUserID,int? ReleaseApplicationID)
        {
            string Quere = @"Update DetainedLicenses
                             set IsReleased = @IsReleased,
                             ReleaseDate = @ReleaseDate, 
                             ReleasedByUserID = @ReleasedByUserID,
                             ReleaseApplicationID = @ReleaseApplicationID
                             where DetainID = @DetainID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("DetainID", DetainID, false),
                Parameters.MakeParameter("IsReleased", IsReleased, false),
                Parameters.MakeParameter("ReleaseDate", ReleaseDate, true),
                Parameters.MakeParameter("ReleasedByUserID", ReleasedByUserID, true),
                Parameters.MakeParameter("ReleaseApplicationID", ReleaseApplicationID, true)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static bool FindNonReleasedDetainedLicenseInfoByLicenseID(int LicenseID,ref int DetainID
            , ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased
            , ref DateTime? ReleaseDate, ref int? ReleasedByUserID, ref int? ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"select top 1 * from DetainedLicenses
                             where LicenseID = @LicenseID and IsReleased = 0";

            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    ReleaseDate = reader["ReleaseDate"] == DBNull.Value ? null :(DateTime?)reader["ReleaseDate"];
                    ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value ? null : (int?)reader["ReleasedByUserID"];
                    ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value ? null : (int?)reader["ReleaseApplicationID"];
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

        public static bool FindByDetainID(int DetainID, ref int LicenseID
            , ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased
            , ref DateTime? ReleaseDate, ref int? ReleasedByUserID, ref int? ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"select * from DetainedLicenses
                             where DetainID = @DetainID and IsReleased = 0";

            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    ReleaseDate = reader["ReleaseDate"] == DBNull.Value ? null : (DateTime?)reader["ReleaseDate"];
                    ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value ? null : (int?)reader["ReleasedByUserID"];
                    ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value ? null : (int?)reader["ReleaseApplicationID"];
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

        public static bool IsLicenseDetainedByID(int LicenseID)
        {
            string Quere = @"select top 1 Count(DetainID) from DetainedLicenses
                             where LicenseID = @LicenseID and IsReleased = 0";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("LicenseID", LicenseID, false)
            };

            return clsCRUD.IsRecordExistInByQuereCondition(Quere, parameters, clsPublicSystemInfos.ConnectionString);
        }
    }
}
