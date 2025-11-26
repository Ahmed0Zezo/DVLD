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
           DateTime ExpirationDate, string Notes, double PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            string Quere = @"INSERT INTO Licenses
                            (ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate
                             , Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
                            VALUES
                            (@ApplicationID, @DriverID, @LicenseClassID, @IssueDate, @ExpirationDate
                            , @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID)
                            SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("ApplicationID", ApplicationID, false),
                Parameters.MakeParameter("DriverID", DriverID, false),
                Parameters.MakeParameter("LicenseClassID", LicenseClassID, false),
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
                
        public static DataTable GetAllByPersonID(int PersonID)
        {
            string Quere = @"select Licenses.* from 
                            Licenses inner join Drivers on Licenses.DriverID = Drivers.DriverID
                            where Drivers.PersonID = @PersonID"; 
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("PersonID", PersonID, false)
            };
            return clsCRUD.GetAllDataFromTableByQuere(Quere, parameters, clsPublicSystemInfos.ConnectionString);
        }

        public static bool FindByApplicationID(int ApplicationID, ref int LicenseID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate,
            ref DateTime ExpirationDate, ref string Notes, ref double PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"SELECT * FROM Licenses WHERE ApplicationID = @ApplicationID";
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
                    LicenseClassID = (int)reader["LicenseClassID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = reader["Notes"] == DBNull.Value ? null : (string)reader["Notes"];
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
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

        public static bool IsLicenseExistByApplicationID(int ApplicationID)
        {
            return clsCRUD.IsRecordExistInTableByID(ApplicationID, "ApplicationID", "Licenses", clsPublicSystemInfos.ConnectionString);
        }
    }
}
