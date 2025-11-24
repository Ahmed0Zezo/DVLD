using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsTest
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }

        public clsTestAppointment TestAppointment{ get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        // Public constructor: empty object :must contain testAppointmentID
        public clsTest(int testAppointmentID)
        {
            TestID = 0;
            TestAppointmentID = testAppointmentID;
            TestAppointment = clsTestAppointment.FindTestAppointByID(testAppointmentID);
            TestResult = false;
            Notes = null;
            CreatedByUserID = 0;
        }

        // Private constructor: full object
        private clsTest(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestAppointment = clsTestAppointment.FindTestAppointByID(testAppointmentID);
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserID;
        }

        public bool Add()
        {
            int newID = -1;
            bool result = clsTestsDataAccess.InsertNewTest(ref newID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            if (result)
            {
                if(this.TestResult == true)
                {
                    this.TestAppointment.Lock();
                }
                TestID = newID;
                return true;
            }
            else
            {
                TestID = 0;
                return false;
            }
        }

        public static DataTable GetAllTests()
        {
            return clsTestsDataAccess.GetAllTests();
        }

        public static clsTest FindByTestAppointmentID(int testAppointmentID)
        {
            int testID = 0;
            bool testResult = false;
            string notes = null;
            int createdByUserID = 0;

            bool found = clsTestsDataAccess.FindTestByTestAppointmentID(testAppointmentID, ref testID, ref testResult, ref notes, ref createdByUserID);

            if (!found)
                return null;

            return new clsTest(testID, testAppointmentID, testResult, notes, createdByUserID);
        }

        public bool UpdateNotes(string NewNotes)
        {
            return clsTestsDataAccess.UpdateNotes(this.TestID, NewNotes);
        }
    }
}
