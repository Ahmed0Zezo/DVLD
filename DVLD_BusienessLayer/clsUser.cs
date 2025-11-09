using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.IO;

namespace DVLD_BusienessLayer
{
    public class clsUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PersonID { get; set; }
        public bool IsActive { get; set; }

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.PersonID = -1;
            this.IsActive = false;
        }

        private clsUser(int UserID, string UserName, string Password, int PersonID, bool IsActive)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.PersonID = PersonID;
            this.IsActive = IsActive;
        }
        private int _addNew()
        {
            int NewID = -1;

            if (clsUsersDataAccess.InsertUserIntoDatabase(ref NewID, this.UserName, this.Password, this.PersonID, this.IsActive))
            {
                return NewID;
            }

            return -1;
        }

        private bool _update()
        {
            return clsUsersDataAccess.UpdateUsersInfoIntoDatabase(this.UserID, this.UserName, this.Password, this.PersonID, this.IsActive);
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccess.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUsersDataAccess.DeleteUserFromDatabase(UserID);
        }

        public static clsUser FindUserByID(int UserID)
        {
            string UserName = string.Empty;
            string Password = string.Empty;
            int PersonID = -1;
            bool IsActive = false;

            if (clsUsersDataAccess.FindUserByUserID(UserID, ref UserName, ref Password, ref PersonID, ref IsActive))
            {
                return new clsUser
                {
                    UserID = UserID,
                    UserName = UserName,
                    Password = Password,
                    PersonID = PersonID,
                    IsActive = IsActive
                };
            }

            return null;
        }

        public static clsUser FindUserByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = string.Empty;
            string Password = string.Empty;
            bool IsActive = false;

            if (clsUsersDataAccess.FindUserByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser
                {
                    UserID = UserID,
                    UserName = UserName,
                    Password = Password,
                    PersonID = PersonID,
                    IsActive = IsActive
                };
            }

            return null;
        }

        public bool Update()
        {
            return _update();
        }

        public  bool UpdatePassword()
        {
            return clsUsersDataAccess.UpdateUserPasswordInDatabase(this.UserID , this.Password);
        }
        public bool AddNew()
        {
            this.PersonID = _addNew();
            if (this.PersonID == -1)
            {
                return false;
            }
            return true;
        }
        public static clsUser FindByUsernameAndPassword(string Username, string Password)
        {
            int PersonID = -1; int UserID = -1; bool IsActive = false;

            if (clsUsersDataAccess.FindByUserNameAndPassword(ref UserID, ref PersonID, ref IsActive, Username, Password))
            {
                return new clsUser(UserID ,Username,Password,PersonID,IsActive);
            }
            else
            {
                return null;
            }

           
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUsersDataAccess.IsUserExistInDatabase(UserID);
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUsersDataAccess.IsUserExistInDatabaseByPersonID(PersonID);
        }

        public static bool IsUserNameExist(string UserName)
        {
            return clsUsersDataAccess.IsUserExistInDatabaseByUserName(UserName);
        }
    }
}