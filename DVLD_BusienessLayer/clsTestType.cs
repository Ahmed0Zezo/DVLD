using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsTestType
    {
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public clsTestType(int testTypeID, string testTypeTitle, string testTypeDescription, decimal testTypeFees)
        {
            TestTypeID = testTypeID;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
        }

        public static clsTestType FindTestTypeByID(int TestTypeID)
        {
            string testTypeTitle = "";
            string testTypeDescription = "";
            decimal testTypeFees = 0;

            bool isFound = clsTestTypesDataAccess.FindTestTypeByID(
                TestTypeID, ref testTypeTitle, ref testTypeDescription, ref testTypeFees);

            if (isFound)
            {
                return new clsTestType(TestTypeID, testTypeTitle, testTypeDescription, testTypeFees);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesDataAccess.GetAllTestTypes();
        }

        public bool UpdateTestTypes()
        {
            return clsTestTypesDataAccess.UpdateTestTypesInfoIntoDatabase(
                TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees
            );
        }
    }
}
