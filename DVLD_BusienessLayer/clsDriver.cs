using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusienessLayer
{
    public class clsDriver
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        // Public constructor: empty object
        public clsDriver()
        {
            DriverID = 0;
            PersonID = 0;
            CreatedByUserID = 0;
            CreatedDate = DateTime.MinValue;
        }

        // Private constructor: full object
        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
        }

        public bool AddNew()
        {
            int newID = -1;
            bool result = clsDriversDataAccess.InsertNewDriver(ref newID, PersonID, CreatedByUserID, CreatedDate);
            if (result)
            {
                DriverID = newID;
                return true;
            }
            else
            {
                DriverID = 0;
                return false;
            }
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriversDataAccess.GetAllDrivers();
        }

        public static int GetDriverIDByPersonID(int personID)
        {
            int driverID = 0;
            

            bool found = clsDriversDataAccess.FindDriverIDByPersonID(personID, ref driverID);

            if (!found)
                return -1;

            return driverID;
        }

        public static bool IsDriverExistByPersonID(int PersonID)
        {
            return clsDriversDataAccess.IsDriverExistByPersonID(PersonID);
        }
    }
}
