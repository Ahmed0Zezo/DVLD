using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DVLD_DataAccessLayer;

namespace DVLD_BusienessLayer
{
    public class clsPerson
    {

        public enum enGendor { Male = 0, Female = 1, None = 2 }
        public int PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public enGendor Gendor { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { set; get; }

        public string OldImagePath { set; get; }
        public string NewImagePath { set; get; }

        private int _convertGendorIntoInt(enGendor Gendor)
        {
            return Gendor == enGendor.Male ? 0 : 1;
        }

        private static enGendor _convertIntIntoGendor(int Int)
        {
            if (Int == 0)
                return enGendor.Male;
            else if (Int == 1)
                return enGendor.Female;

            return enGendor.None;
        }
        private int _addNew()
        {
            int NewID = -1;

            if (clsPeopleDataAccess.InsertPersonIntoDatabase(ref NewID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName
                , this.DateOfBirth, _convertGendorIntoInt(this.Gendor), this.Address, this.Phone, this.Email, this.NationalityCountryID, this.NewImagePath))
            {
                return NewID;
            }

            return -1;
        }

        private bool _update(string SavedImagePath)
        {
            // Updating User Infos
            //SaveImagePath var to choose what Image Path To Save . The old one or the new


            return clsPeopleDataAccess.UpdatePersonInfoIntoDataBase(this.PersonID, this.NationalNo, this.FirstName, this.SecondName
                , this.ThirdName, this.LastName, this.DateOfBirth, _convertGendorIntoInt(this.Gendor), this.Address, this.Phone, this.Email
                , this.NationalityCountryID, SavedImagePath);

        }



        private void _saveImageProccess(ref string ImagePath)
        {
            if (!Directory.Exists(clsPublicSystemInfos.ImageSavingFolderPath))
            {
                Directory.CreateDirectory(clsPublicSystemInfos.ImageSavingFolderPath);
            }


            if (File.Exists(ImagePath))
            {
                string NewPath = clsPublicSystemInfos.ImageSavingFolderPath + Guid.NewGuid()
                    + Path.GetExtension(ImagePath);

                File.Copy(ImagePath, NewPath, true);

                ImagePath = NewPath;
            }
        }

        private bool _isValidPersonAge(DateTime DateOfBirth)
        {
            //make sure that the person is 18+ old

            return DateOfBirth <= DateTime.Now.AddYears(-18);


        }

        private bool _areInfosValidForAdding(bool CheckForNationalNo = true)
        {
            //check string Fields
            if (string.IsNullOrEmpty(this.FirstName) || string.IsNullOrEmpty(this.SecondName) || string.IsNullOrEmpty(this.ThirdName) ||
                string.IsNullOrEmpty(this.LastName) || string.IsNullOrEmpty(this.Phone) || string.IsNullOrEmpty(this.Address))
            {
                return false;
            }

            if (this.DateOfBirth == null || !_isValidPersonAge(this.DateOfBirth))
                return false;

            //Gender Check
            if (_convertGendorIntoInt(this.Gendor) > 1 || _convertGendorIntoInt(this.Gendor) < 0)
                return false;

            //Email Check // Email is nullable
            if (!string.IsNullOrEmpty(this.Email))
            {
                if (!(clsEmail.IsEmailInCorrectForm(this.Email)))
                    return false;
            }

            //National No Check
            if (CheckForNationalNo)
            {
                if (clsPeopleDataAccess.IsNationalNoExist(this.NationalNo))
                    return false;
            }

            if (!clsCountriesDataAccess.IsCountryExistInDataBase(this.NationalityCountryID))
                return false;

            return true;
        }

        public clsPerson()
        {
            this.PersonID = -1; this.NationalNo = ""; this.Gendor = enGendor.None; this.NationalityCountryID = -1;

            this.FirstName = ""; this.SecondName = ""; this.ThirdName = ""; this.LastName = "";
            this.Address = ""; this.Email = ""; this.Phone = "";

            //when User make an empty Object will Add the image in NewImagePath
            this.NewImagePath = "";

            this.DateOfBirth = DateTime.Now;
        }

        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName
            , string LastName, DateTime DateOfBirth
            , int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID; this.NationalNo = NationalNo; this.Gendor = _convertIntIntoGendor(Gendor);
            this.NationalityCountryID = NationalityCountryID;

            this.FirstName = FirstName; this.SecondName = SecondName; this.ThirdName = ThirdName; this.LastName = LastName;
            this.Address = Address; this.Email = Email; this.Phone = Phone;

            //Get Image Path In Old Image bec it is already in database
            this.OldImagePath = ImagePath;

            this.DateOfBirth = DateOfBirth;
        }

        public static clsPerson FindByID(int ID)
        {
            string NationalNo = ""; int Gendor = -1; int NationalityCountryID = -1;

            string FirstName = ""; string SecondName = ""; string ThirdName = ""; string LastName = "";
            string Address = ""; string Email = ""; string ImagePath = ""; string Phone = "";

            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleDataAccess.FindPersonByID(ID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName
                , ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(ID, NationalNo, FirstName, SecondName, ThirdName, LastName
                , DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }



        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.GetAllPeople();
        }


        public static bool IsPersonExist(int PersonID)
        {
            return clsPeopleDataAccess.IsPersonExistInDataBase(PersonID);
        }

        public static bool IsNationalNumberExist(string NationalNo)
        {
            return clsPeopleDataAccess.IsNationalNoExist(NationalNo);
        }


        public bool AddNew()
        {
            if (!_areInfosValidForAdding())
                return false;



            if (!string.IsNullOrEmpty(this.NewImagePath))
            {
                string ImagePath = this.NewImagePath;
                _saveImageProccess(ref ImagePath);
                this.NewImagePath = ImagePath;
            }

            this.PersonID = this._addNew();



            return true;
        }


        public bool Update()
        {
            if (!_areInfosValidForAdding(false))
                return false;

            if (!IsPersonExist(this.PersonID))
                return false;

            //Make sure that there is not an old Image path in database
            if (string.IsNullOrEmpty(this.OldImagePath))
                OldImagePath = clsPeopleDataAccess.GetPersonImageLocation(this.PersonID);

            //Decinde which ImagePath To Save (old or new)
            string SavedImagePath = this.OldImagePath;

            if (this.OldImagePath != this.NewImagePath)
            {
                if (!string.IsNullOrEmpty(this.OldImagePath))
                {

                    if (!string.IsNullOrEmpty(this.NewImagePath))
                    {
                        if (File.Exists(this.OldImagePath))
                        {
                            try
                            {
                                File.Delete(this.OldImagePath);
                            }
                            catch (IOException ex)
                            {

                            }
                        }

                    }
                }

                if (!string.IsNullOrEmpty(this.NewImagePath))
                {
                    SavedImagePath = this.NewImagePath;
                    _saveImageProccess(ref SavedImagePath);
                }
                else
                {
                    SavedImagePath = null;
                }
            }



            return _update(SavedImagePath);

        }


        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleDataAccess.DeletePersonFromDataBase(PersonID);
        }

        public static bool IsPersonHasLocalNewDrivingLicenseAppWithClassID(int PersonID,int ClassID)
        {
            return clsPeopleDataAccess.IsPersonHasActiveLocalDrivingLicenseApplicationWithClassID(PersonID, ClassID);
        }

        public static bool IsPersonHasActiveLicenseWithClassID(int PersonID , int ClassID)
        {
            return clsPeopleDataAccess.IsPersonHasActiveLicenseWithClassID(PersonID, ClassID);
        }
    }
}
