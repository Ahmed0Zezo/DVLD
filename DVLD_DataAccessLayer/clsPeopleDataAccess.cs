using DataAccess_Queries;
using DataAccessLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public static class clsPeopleDataAccess
    {

        public static bool InsertPersonIntoDatabase(ref int NewID, string NationalNo, string FirstName, string SecondName, string ThirdName
            , string LastName, DateTime DateOfBirth
            , int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            string Quere = @"Insert Into People(NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,
                            Gendor,Address,Phone,Email,NationalityCountryID,ImagePath)

                            Values(@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,
                            @Gendor,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath)

                            Select Scope_Identity()";

            List<SqlParameter> parameters = new List<SqlParameter>();


            parameters.Add(Parameters.MakeParameter("NationalNo", NationalNo, false));
            parameters.Add(Parameters.MakeParameter("FirstName", FirstName, false));
            parameters.Add(Parameters.MakeParameter("SecondName", SecondName, false));
            parameters.Add(Parameters.MakeParameter("ThirdName", ThirdName, false));
            parameters.Add(Parameters.MakeParameter("LastName", LastName, false));
            parameters.Add(Parameters.MakeParameter("DateOfBirth", DateOfBirth, false));
            parameters.Add(Parameters.MakeParameter("Gendor", Gendor, false));
            parameters.Add(Parameters.MakeParameter("Address", Address, false));
            parameters.Add(Parameters.MakeParameter("Phone", Phone, false));
            parameters.Add(Parameters.MakeParameter("Email", Email, true));
            parameters.Add(Parameters.MakeParameter("NationalityCountryID", NationalityCountryID, false));
            parameters.Add(Parameters.MakeParameter("ImagePath", ImagePath, true));

            return clsCRUD.AddNewRecordToTable(ref NewID, clsPublicSystemInfos.ConnectionString, Quere, parameters);
        }

        public static DataTable GetAllPeople()
        {
            return clsCRUD.GetAllDataFromTable("People", clsPublicSystemInfos.ConnectionString);
        }

        public static bool UpdatePersonInfoIntoDataBase(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName
            , string LastName, DateTime DateOfBirth
            , int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            string Quere = @"Update People
                              Set NationalNo = @NationalNo ,FirstName = @FirstName,SecondName = @SecondName,ThirdName = @ThirdName,LastName = @LastName,
                               DateOfBirth = @DateOfBirth,Gendor = @Gendor ,Address = @Address ,Phone = @Phone,Email = @Email,
                                NationalityCountryID = @NationalityCountryID ,ImagePath = @ImagePath
                                Where PersonID = @PersonID";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(Parameters.MakeParameter("PersonID", PersonID, false));
            parameters.Add(Parameters.MakeParameter("NationalNo", NationalNo, false));
            parameters.Add(Parameters.MakeParameter("FirstName", FirstName, false));
            parameters.Add(Parameters.MakeParameter("SecondName", SecondName, false));
            parameters.Add(Parameters.MakeParameter("ThirdName", ThirdName, false));
            parameters.Add(Parameters.MakeParameter("LastName", LastName, false));
            parameters.Add(Parameters.MakeParameter("DateOfBirth", DateOfBirth, false));
            parameters.Add(Parameters.MakeParameter("Gendor", Gendor, false));
            parameters.Add(Parameters.MakeParameter("Address", Address, false));
            parameters.Add(Parameters.MakeParameter("Phone", Phone, false));
            parameters.Add(Parameters.MakeParameter("Email", Email, true));
            parameters.Add(Parameters.MakeParameter("NationalityCountryID", NationalityCountryID, false));
            parameters.Add(Parameters.MakeParameter("ImagePath", ImagePath, true));

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;

        }

        public static bool DeletePersonFromDataBase(int PersonID)
        {
            string Quere = @"Delete From People
                            Where PersonID = @PersonID";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(Parameters.MakeParameter("PersonID", PersonID, false));

            return clsCRUD.UpdateAndDeleteRecordFromTable(clsPublicSystemInfos.ConnectionString, Quere, parameters) > 0;

        }

        public static bool FindPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName
            , ref string LastName, ref DateTime DateOfBirth
            , ref int Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsPublicSystemInfos.ConnectionString);

            string Quere = @"select * from People
                             where PersonID = @PersonID";


            SqlCommand command = new SqlCommand(Quere, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    ImagePath = reader["ImagePath"] == DBNull.Value ? null : (string)reader["ImagePath"];

                    IsFound = true;
                }

            }
            catch
            {
                ///////////


            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static string GetPersonImageLocation(int PersonID)
        {
            string Quere = @"select ImagePath from People
                            where PersonID = @PersonID";

            List<SqlParameter> parameters = new List<SqlParameter> { Parameters.MakeParameter("PersonID", PersonID, false) };

            Commands.Scalar scalar = new Commands.Scalar(clsPublicSystemInfos.ConnectionString, Quere, parameters);

            scalar.Execute();

            if (scalar.IsExecutedSuccessfully)
                return scalar.Result.ToString();

            return null;

        }
        public static bool IsPersonExistInDataBase(int PersonID)
        {
            return clsCRUD.IsRecordExistInTableByID(PersonID, "PersonID", "People", clsPublicSystemInfos.ConnectionString);
        }

        public static bool IsNationalNoExist(string NationalNo)
        {
            return clsCRUD.IsRecordExistInTableByID(NationalNo, "NationalNo", "People", clsPublicSystemInfos.ConnectionString);
        }
    }
}
