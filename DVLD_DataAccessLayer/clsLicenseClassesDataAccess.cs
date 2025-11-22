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
    public class clsLicenseClassesDataAccess
    {
        public static DataTable GetAllLicenseClasses()
        {
            return clsCRUD.GetAllDataFromTable("LicenseClasses", clsPublicSystemInfos.ConnectionString);
        }

        

        public static bool FindLicenseClassByID(int LicenseClassID,
            ref string ClassName, ref string ClassDescription,ref int MinimumAllowedAge ,ref int DefaulyValidityLength,ref decimal ClassFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"Select * From LicenseClasses
                             Where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    DefaulyValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    ClassFees = (decimal)reader["ClassFees"];
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

        public static bool GetLiceseClassNameByItsID(int LicenseClasseID,ref string ClassName)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"select ClassName from LicenseClasses
                             where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClasseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ClassName = (string)reader["ClassName"];
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
