using DataAccess_Queries;
using DataAccessLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public static class clsUsersDataAccess
    {
        public static bool InsertUserIntoDatabase(ref int NewID, string UserName, string Password, int PersonID, bool IsActive)
        {
            string Quere = @"Insert Into Users(UserName, Password, PersonID, IsActive)
                             Values(@UserName, @Password, @PersonID, @IsActive)
                             Select Scope_Identity()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("UserName", UserName, false),
                Parameters.MakeParameter("Password", Password, false),
                Parameters.MakeParameter("PersonID", PersonID, false),
                Parameters.MakeParameter("IsActive", IsActive, false)
            };

            return clsCRUD.AddNewRecordToTable(ref NewID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }

        public static DataTable GetAllUsers()
        {
            return clsCRUD.GetAllDataFromTable("Users", clsPublicSystemInfos.ConnectionString);
        }

        public static bool UpdateUsersInfoIntoDatabase(int UserID, string UserName, string Password, int PersonID, bool IsActive)
        {
            string Quere = @"Update Users
                             Set UserName = @UserName, Password = @Password, PersonID = @PersonID, IsActive = @IsActive
                             Where UserID = @UserID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("UserID", UserID, false),
                Parameters.MakeParameter("UserName", UserName, false),
                Parameters.MakeParameter("Password", Password, false),
                Parameters.MakeParameter("PersonID", PersonID, false),
                Parameters.MakeParameter("IsActive", IsActive, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static bool UpdateUserPasswordInDatabase(int UserID,string Password)
        {
            string Quere = @"Update Users
                             Set Password = @Password
                             Where UserID = @UserID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("UserID", UserID, false),
                Parameters.MakeParameter("Password", Password, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString,Quere,parameters) == 1;
        }
        public static bool DeleteUserFromDatabase(int UserID)
        {
            string Quere = @"Delete From Users
                             Where UserID = @UserID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                Parameters.MakeParameter("UserID", UserID, false)
            };

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;
        }

        public static bool FindUserByUserID(int UserID, ref string UserName, ref string Password, ref int PersonID, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"Select * From Users
                             Where UserID = @UserID";

            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = (bool)reader["IsActive"];
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

        public static bool FindUserByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"Select * From Users
                             Where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
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

        public static bool FindByUserNameAndPassword(ref int UserID,ref int PersonID ,ref bool IsActive, string UserName, string Password)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"Select * From Users
                             Where UserName = @UserName and Password = @Password";

            SqlCommand command = new SqlCommand(Quere, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
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

        public static bool IsUserExistInDatabase(int UserID)
        {
            return clsCRUD.IsRecordExistInTableByID(UserID, "UserID", "Users", clsPublicSystemInfos.ConnectionString);
        }

        public static bool IsUserExistInDatabaseByPersonID(int PersonID)
        {
            return clsCRUD.IsRecordExistInTableByID(PersonID, "PersonID", "Users", clsPublicSystemInfos.ConnectionString);
        }

        public static bool IsUserExistInDatabaseByUserName(string Username)
        {
            return clsCRUD.IsRecordExistInTableByID(Username, "UserName", "Users", clsPublicSystemInfos.ConnectionString);
        }
    }
}