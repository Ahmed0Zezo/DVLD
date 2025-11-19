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

        public TestTypeEnum Type
        {
            get { return TestTypeEnumFromID(TestTypeID); }
            set { TestTypeID = TestTypeIDFromEnum(value); }
        }

        public enum TestTypeEnum : byte
        {
            Vision = 1,
            Written = 2,
            Street = 3
        }

        public static TestTypeEnum TestTypeEnumFromID(int testTypeID)
        {
            switch (testTypeID)
            {
                case 1: return TestTypeEnum.Vision;
                case 2: return TestTypeEnum.Written;
                case 3: return TestTypeEnum.Street;
                default: throw new ArgumentOutOfRangeException(nameof(testTypeID), "Invalid TestTypeID value");
            }
        }

        public static string TestTypeEnumToString(TestTypeEnum type)
        {
            switch (type)
            {
                case TestTypeEnum.Vision: return "Vision";
                case TestTypeEnum.Written: return "Written";
                case TestTypeEnum.Street: return "Street";
                default: throw new ArgumentOutOfRangeException(nameof(type), "Invalid TestTypeEnum value");
            }
        }
        public static int TestTypeIDFromEnum(TestTypeEnum type)
        {
            return (int)type;
        }
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

        public static clsTestType WhatTestTypeToTakeByLocalAppID(int LocalAppID)
        {
            int? TestTypeID = clsLocalAppsDataAccess.GetLastPassedTestTypeIDByLocalAppID(LocalAppID);

            if (TestTypeID == null)
            {
                // if there is no Passed Tests at all then will return Vision Test
                return FindTestTypeByID(1);
            }
            else if (TestTypeID == 1 || TestTypeID == 2)
            {
                //return the next of the current Test to take
                return FindTestTypeByID((int)TestTypeID + 1);
            }
            else
            {
                //if the valu isn't 1 or 2 then it's 3 (The Person Passed all Tests) so there isn't test to take
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
