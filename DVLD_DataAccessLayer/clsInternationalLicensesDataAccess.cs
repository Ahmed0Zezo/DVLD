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
    public static class clsInternationalLicensesDataAccess
    {
        public static bool InsertNewInternationalLicense(ref int LicenseID,int ApplicationID ,int DriverID
            ,int IssuedUsingLocalLicenseID,DateTime IssueDate,DateTime ExpirationDate ,bool IsActive,int CreatedByUserID)
        {
            string Quere = @"insert into InternationalLicenses
                            (ApplicationID ,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate ,IsActive,CreatedByUserID)
                             Values
                            (@ApplicationID ,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate ,@IsActive,@CreatedByUserID)
                              SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("ApplicationID", ApplicationID, false),
                Parameters.MakeParameter("DriverID", DriverID, false),
                Parameters.MakeParameter("IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID, false),
                Parameters.MakeParameter("IssueDate", IssueDate, false),
                Parameters.MakeParameter("ExpirationDate", ExpirationDate, false),
                Parameters.MakeParameter("IsActive", IsActive, false),
                Parameters.MakeParameter("CreatedByUserID", CreatedByUserID, false)
            };

            return clsCRUD.AddNewRecordToTable(ref LicenseID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }

        public static bool FindByInternationalLicenseID(int LicenseID,ref int ApplicationID,ref int DriverID
            ,ref int IssuedUsingLocalLicenseID,ref DateTime IssueDate,ref DateTime ExpirationDate,ref bool IsActive,ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate =(DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
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

        public static DataTable GetPersonInternationalLicensesHistoryByPersonID(int PersonID)
        {
            string Quere = @"select InternationalLicenses.* from
                             InternationalLicenses inner join Drivers on InternationalLicenses.DriverID = Drivers.DriverID
                             where Drivers.PersonID = @PersonID order by IsActive asc";

            List<SqlParameter> parameters = new List<SqlParameter> { Parameters.MakeParameter("PersonID", PersonID, false) };

            return clsCRUD.GetAllDataFromTableByQuere(Quere,parameters, clsPublicSystemInfos.ConnectionString);

        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsCRUD.GetAllDataFromTable("InternationalLicenses",clsPublicSystemInfos.ConnectionString);
        }

        public static bool IsLocalLicenseHasAnActiveIssuedInternationalLicenseAlready(int LocalLicenseID)
        {
            string Quere = @"select Count(InternationalLicenseID) from InternationalLicenses
                              where IssuedUsingLocalLicenseID = @LocalLicenseID and IsActive = 1";

            List<SqlParameter> parameters = new List<SqlParameter> { Parameters.MakeParameter("LocalLicenseID", LocalLicenseID, false) };

            return clsCRUD.IsRecordExistInByQuereCondition(Quere,parameters,clsPublicSystemInfos.ConnectionString);

        }

    }
}
