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
    public static class clsLicensesDataAccess
    {
        public static bool AddNew(ref int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate,
           DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            string Quere = @"INSERT INTO Licenses
                            (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate
                             , Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
                            VALUES
                            (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate
                            , @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID)
                            SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("ApplicationID", ApplicationID, false),
                Parameters.MakeParameter("DriverID", DriverID, false),
                Parameters.MakeParameter("LicenseClass", LicenseClassID, false),
                Parameters.MakeParameter("IssueDate", IssueDate, false),
                Parameters.MakeParameter("ExpirationDate", ExpirationDate, false),
                Parameters.MakeParameter("Notes", Notes, true),
                Parameters.MakeParameter("PaidFees", PaidFees, false),
                Parameters.MakeParameter("IsActive", IsActive, false),
                Parameters.MakeParameter("IssueReason", IssueReason, false),
                Parameters.MakeParameter("CreatedByUserID", CreatedByUserID, false)
            };

            return clsCRUD.AddNewRecordToTable(ref LicenseID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }
                
        public static DataTable GetPersonLocalLicensesHistroyInfo(int PersonID)
        {

            string Quere = @"select * from PersonLicensesHistory_View
                            where ApplicantPersonID = @PersonID"; 
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("PersonID", PersonID, false)
            };
            return clsCRUD.GetAllDataFromTableByQuere(Quere, parameters, clsPublicSystemInfos.ConnectionString);
        }

        public static bool FindByApplicationID(int ApplicationID, ref int LicenseID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate,
            ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID
            ,ref string ApplicantName , ref string ClassName,ref string NationalNo,ref bool Gender ,ref bool IsDetained,ref DateTime DateOfBirth
            ,ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"SELECT * FROM Licenses_View WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = reader["Notes"] == DBNull.Value ? null : (string)reader["Notes"];
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    ApplicantName = (string)reader["Name"];
                    ClassName = (string)reader["ClassName"];
                    NationalNo = (string)reader["NationalNo"];
                    Gender = (byte)reader["Gendor"] == 1 ? true : false;
                    IsDetained = (bool)reader["IsDetained"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    ImagePath = reader["ImagePath"] != DBNull.Value ?(string)reader["ImagePath"] :"" ;

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

        public static bool FindByLocalApplicationID(int LocalAppID,ref int ApplicationID, ref int LicenseID
            , ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate,
            ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees
            , ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID
            , ref string ApplicantName, ref string ClassName, ref string NationalNo, ref bool Gender, ref bool IsDetained, ref DateTime DateOfBirth
            ,ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"select Licenses_View.* from 
                            Licenses_View inner join LocalDrivingLicenseApplications 
                            on Licenses_View.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalAppID";
            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@LocalAppID", LocalAppID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = reader["Notes"] == DBNull.Value ? null : (string)reader["Notes"];
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    ApplicantName = (string)reader["Name"];
                    ClassName = (string)reader["ClassName"];
                    NationalNo = (string)reader["NationalNo"];
                    Gender = (bool)reader["Gendor"];
                    IsDetained = (bool)reader["IsDetained"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";

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

        public static bool IsLicenseExistByApplicationID(int ApplicationID)
        {
            return clsCRUD.IsRecordExistInTableByID(ApplicationID, "ApplicationID", "Licenses", clsPublicSystemInfos.ConnectionString);
        }
    }
}
