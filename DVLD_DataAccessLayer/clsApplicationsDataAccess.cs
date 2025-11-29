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
    public class clsApplicationsDataAccess
    {
        public static bool FindApplicationByID(int ApplicationID,ref int ApplicantPersonID,ref DateTime ApplicationDate
            ,ref int ApplicationTypeID,ref byte ApplicationStatus,ref DateTime LastStatusDate
            ,ref decimal PaidFees,ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
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

        public static bool InsertNewApplication(ref int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate
            ,int ApplicationTypeID,byte ApplicationStatus,DateTime LastStatusDate,decimal PaidFees,int CreatedByUserID)
        {
            string Quere = @"INSERT INTO Applications
                (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                VALUES
                (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID)
                SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("ApplicantPersonID", ApplicantPersonID, false),
                Parameters.MakeParameter("ApplicationDate", ApplicationDate, false),
                Parameters.MakeParameter("ApplicationTypeID", ApplicationTypeID, false),
                Parameters.MakeParameter("ApplicationStatus", ApplicationStatus, false),
                Parameters.MakeParameter("LastStatusDate", LastStatusDate, false),
                Parameters.MakeParameter("PaidFees", PaidFees, false),
                Parameters.MakeParameter("CreatedByUserID", CreatedByUserID, false)
            };

            return clsCRUD.AddNewRecordToTable(ref ApplicationID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }

        public static bool UpdateApplication(int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate
            ,int ApplicationTypeID,byte ApplicationStatus,DateTime LastStatusDate,decimal PaidFees,int CreatedByUserID)
        {
            string Quere = @"UPDATE Applications SET
                ApplicantPersonID = @ApplicantPersonID,
                ApplicationDate = @ApplicationDate,
                ApplicationTypeID = @ApplicationTypeID,
                ApplicationStatus = @ApplicationStatus,
                LastStatusDate = @LastStatusDate,
                PaidFees = @PaidFees,
                CreatedByUserID = @CreatedByUserID
                WHERE ApplicationID = @ApplicationID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("ApplicationID", ApplicationID, false),
                Parameters.MakeParameter("ApplicantPersonID", ApplicantPersonID, false),
                Parameters.MakeParameter("ApplicationDate", ApplicationDate, false),
                Parameters.MakeParameter("ApplicationTypeID", ApplicationTypeID, false),
                Parameters.MakeParameter("ApplicationStatus", ApplicationStatus, false),
                Parameters.MakeParameter("LastStatusDate", LastStatusDate, false),
                Parameters.MakeParameter("PaidFees", PaidFees, false),
                Parameters.MakeParameter("CreatedByUserID", CreatedByUserID, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static bool DeleteApplicationByID(int ApplicationID)
        {
            string Quere = @"DELETE FROM Applications WHERE ApplicationID = @ApplicationID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("ApplicationID", ApplicationID, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static DataTable GetAllApplications()
        {
            return clsCRUD.GetAllDataFromTable("Applications", clsPublicSystemInfos.ConnectionString);
        }

        public static byte GetAppStatusByID(int ApplicationID)
        {
            string Quere = @"select Applications.ApplicationStatus from
                            LocalDrivingLicenseApplications inner join Applications 
                            on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @ApplicationID";

            List<SqlParameter> parameters = new List<SqlParameter> { Parameters.MakeParameter("ApplicationID", ApplicationID,false) };

            return clsCRUD.ReturnbyteValueFromTableByQuere(Quere,parameters,clsPublicSystemInfos.ConnectionString);
        }

        public static bool UpdateApplicationStatus(int ApplicationID , byte ApplicationStatus)
        {
            string Quere = @"Update Applications
                             set ApplicationStatus = @ApplicationStatus ,LastStatusDate = @LastStatusDate
                             where ApplicationID = @ApplicationID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("ApplicationID", ApplicationID, false),
                 Parameters.MakeParameter("ApplicationStatus", ApplicationStatus, false),
                 Parameters.MakeParameter("LastStatusDate", DateTime.Now, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        
    }
}
