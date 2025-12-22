using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactDataAccessLayer;
using System.Runtime.Remoting.Messaging;
using System.Data;

namespace ContactsBusinessLayer
{

    public class clsContact
    {

        public enum enMode { AddNew = 1, Update = 2 }
        private enMode Mode = enMode.AddNew;


        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryID { get; set; }
        public string ImagePath { get; set; }


        public clsContact ()
        {

            this.ID = -1; ;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsContact(int ID, string FirstName, string LastName, string Email, string Phone, string Address,
            DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;
        }


        
        public static clsContact Find(int ContactID)
        {

            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = 0;

            if (clsContactDataAccess.GetContactByID(ContactID, ref FirstName, ref LastName, ref Email, ref Phone, ref Address,
                ref DateOfBirth, ref CountryID, ref ImagePath))
            
                return new clsContact (ContactID, FirstName, LastName, Email, Phone, Address, 
                    DateOfBirth, CountryID, ImagePath);
            else

                return null;
            
        }

        private bool _AddNewContact()
        {

            this.ID = clsContactDataAccess.AddNewContact(this.FirstName,this.LastName,this.Email,this.Phone,this.Address,
                this.DateOfBirth,this.CountryID,this.ImagePath);

            return (this.ID != -1);

        }

        private bool _UpdateContact()
        {

            return clsContactDataAccess.UpdateContact(this.ID, this.FirstName, this.LastName, this.Email, this.Phone, this.Address,
                this.DateOfBirth, this.CountryID, this.ImagePath);

        }

        public static bool DeleteContact(int ContactID)
        {
            return clsContactDataAccess.DeleteContact(ContactID);
        }

        public static DataTable GetAllContact()
        {
            return clsContactDataAccess.GetAllContacts();
        }

        public static bool IsContactExist(int ContactID)
        {

            return clsContactDataAccess.IsContactExist(ContactID);

        }



        public bool Save()
        {

            switch (Mode)
            {


                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                case enMode.Update:

                    return _UpdateContact();

            }

            return false;


        }



    }


}
