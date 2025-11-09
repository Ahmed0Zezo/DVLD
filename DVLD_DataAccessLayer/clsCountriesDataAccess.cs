using DataAccess_Queries;
using DataAccessLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsCountriesDataAccess
    {
        public int CountryID { set; get; }

        public string CountruyName { set; get; }

        public static int GetCountryIDByName(string CountryName)
        {
            string Quere = @"select CountryID from Countries
                            where CountryName = @CountryName";

            List<SqlParameter> parms = new List<SqlParameter> { Parameters.MakeParameter("CountryName", CountryName, false) };

            Commands.Scalar scalar = new Commands.Scalar(clsPublicSystemInfos.ConnectionString, Quere, parms);

            scalar.Execute();

            if (scalar.IsExecutedSuccessfully)
                return (int)scalar.Result;

            return -1;
        }

        public static DataTable GetAllCountries()
        {
            return clsCRUD.GetAllDataFromTable("Countries", clsPublicSystemInfos.ConnectionString);
        }

        public static bool IsCountryExistInDataBase(int CountryID)
        {
            return clsCRUD.IsRecordExistInTableByID(CountryID, "CountryID", "Countries", clsPublicSystemInfos.ConnectionString);
        }

        public static bool IsCountryExistInDataBase(string CountryName)
        {
            return clsCRUD.IsRecordExistInTableByID(CountryName, "CountryName", "Countries", clsPublicSystemInfos.ConnectionString);
        }
    }
}
