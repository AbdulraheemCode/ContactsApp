using ContactDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBusinessLayer
{
    public class clsCountry
    {

        public enum enMode { AddNew = 1 , Update = 2}
        enMode Mode = enMode.AddNew;


        public int CountryID {  get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }


        private clsCountry(int CounryID, string CountryName, string Code, string PhoneCode)
        {
            this.CountryID = CounryID;
            this.CountryName = CountryName;
            this.Code = Code;
            this.PhoneCode = PhoneCode;

            Mode = enMode.Update;
        }

        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = "";
            this.Code = "";
            this.PhoneCode = "";


            Mode = enMode.AddNew;

        }



        public static clsCountry Find(int CountryID)
        {

            string CountryName = "", Code = "", PhoneCode = "";

            if (clsCountryDataAccess.GetCountryByID(CountryID, ref CountryName, ref Code, ref PhoneCode))

                return new clsCountry(CountryID, CountryName, Code, PhoneCode);

            else
                return null;

        }

        public static clsCountry Find(string CountryName)
        {

            int CountryID = -1;
            string Code = "", PhoneCode = "";

            if (clsCountryDataAccess.GetCountryByName(CountryName, ref CountryID, ref Code, ref PhoneCode))
            
                return new clsCountry(CountryID, CountryName, Code, PhoneCode);
            
            else
                return null;
            

        }

        private bool _AddNewCountry()
        {

            this.CountryID = clsCountryDataAccess.AddNewCountry(this.CountryName, this.Code, this.PhoneCode);
            return (this.CountryID != -1);
        }

        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(this.CountryID,this.CountryName,this.Code,this.PhoneCode);
        }

        public static bool DeleteCountry(int CountryID)
        {
            return clsCountryDataAccess.DeleteCountry(CountryID);
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }

        public static bool IsCountryExists(int CountryID)
        {
            return clsCountryDataAccess.isCountryExists(CountryID);
        }

        public static bool IsCountryExistsByName(string CountryName)
        {
            return clsCountryDataAccess.isCountryExistsByName(CountryName);
        }


        public bool Save()
        {

            switch(Mode)
            {

                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    
                    return _UpdateCountry();

            }

            return false;

        }



    }
}
