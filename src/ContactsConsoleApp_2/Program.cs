using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using ContactsBusinessLayer;

namespace ContactsConsoleApp_2
{
    internal class Program
    {


        // Contacts Operations
        public static void TestFindContact(int ContactID)
        {


            clsContact Contact = clsContact.Find(ContactID);

            if (Contact != null)
            {

                Console.WriteLine($"Contact ID = {ContactID}");
                Console.WriteLine($"Full Name  = {Contact.FirstName} {Contact.LastName}");
                Console.WriteLine($"Email      = {Contact.Email}");
                Console.WriteLine($"Phone      = {Contact.Phone}");
                Console.WriteLine($"Address    = {Contact.Address}");
                Console.WriteLine($"DateOfBirth= {Contact.DateOfBirth}");
                Console.WriteLine($"Country ID = {Contact.CountryID}");
                Console.WriteLine($"Image Path = {Contact.ImagePath}");

            }
            else
            {
                Console.WriteLine($"Contact [{ContactID}] is Not Found");
            }

        }

        public static void TestAddNewContact()
        {

            clsContact Contact1 = new clsContact();

            Contact1.FirstName = "Mustafa";
            Contact1.LastName = "Juwaed";
            Contact1.Email = "Mustafa@yahoo.com";
            Contact1.Phone = "345354";
            Contact1.Address = "6th Building";
            Contact1.DateOfBirth = new DateTime(1980, 10, 23, 1, 10, 30);
            Contact1.CountryID = 2;
            Contact1.ImagePath = null;

            if (Contact1.Save())
            {
                Console.WriteLine($"Contact is Added Successfully , ID = {Contact1.ID}");
            }


        }

        public static void TestUpdateContact(int ContactID)
        {

            clsContact Contact1 = clsContact.Find(ContactID);

            if (Contact1 != null)
            {

                Contact1.FirstName = "Yasser";
                Contact1.LastName = "AlAhmadi";
                Contact1.Email = "Yasser@gmail.com";
                Contact1.Phone = "00345433";
                Contact1.Address = "6th Building";
                Contact1.DateOfBirth = new DateTime(2000, 10, 23, 1, 10, 30);
                Contact1.CountryID = 2;
                Contact1.ImagePath = "";


                if (Contact1.Save())
                {

                    Console.WriteLine("Contact Update Successfully :-)");
                }
                else
                {
                    Console.WriteLine("Contact Update is No Update :-(");
                }

            }
            else
            {
                Console.WriteLine("Sorry , Contact is Not Found :-(");
            }
        }

        public static void TestDeleteContact(int ContactID)
        {

            if (clsContact.DeleteContact(ContactID))

                Console.WriteLine($"Delete Contact ,ID = {ContactID} Successfully");
            else
                Console.WriteLine("Delete Contact is Faild");

        }

        public static void ListContacts()
        {

            DataTable dt = clsContact.GetAllContact();

            Console.WriteLine("Contacts Data : \n");

            foreach(DataRow Row in dt.Rows)
            {
                Console.WriteLine($"ID = {Row["ContactID"]} , FULL NAME = {Row["FirstName"]} {Row["LastName"]} , " +
                    $"EMAIL = {Row["Email"]}");
            }
        }

        public static void TestIsContactExist(int ContactID)
        {
           
           
            if (clsContact.IsContactExist(ContactID))
            
                Console.WriteLine("Contact Is Exists");
            
            else
                Console.WriteLine("Contact Is Not Found");
            
        }



        // Countries Operations
        public static void TestFindCountry(int CountryID)
        {

            clsCountry Country = clsCountry.Find(CountryID);

            if (Country !=  null)
            {

                Console.WriteLine($"Country ID   = {Country.CountryID}");
                Console.WriteLine($"Country Name = {Country.CountryName}");
                Console.WriteLine($"Country Code = {Country.Code}");
                Console.WriteLine($"Phone Code   = {Country.PhoneCode}");

            }
            else
            {
                Console.WriteLine("Country Not Found");
            }


        }

        public static void TestAddNewCountry()
        {

            clsCountry Country = new clsCountry();

            Country.CountryName = "UAE";
            Country.Code = "";
            Country.PhoneCode = "";

            if (Country.Save())
            {
                Console.WriteLine($"Save Country is Successfully , CountryID = {Country.CountryID}");
            }
            else
            {
                Console.WriteLine($"Save Country is NO Success");
            }


        }

        public static void UpdateCountry(int CountryID)
        {

            clsCountry Country = clsCountry.Find(CountryID); 

            if (Country != null)
            {

                Country.CountryName = "United Emarites";
                Country.Code = "UAE";
                Country.PhoneCode = "967";

                if (Country.Save())
                {
                    Console.WriteLine("Country Updated Sussessfully :-)");
                }
                else
                {
                    Console.WriteLine("Update Country is Failed");
                }
            }
            else
            {
                Console.WriteLine("Country is Not Found");
            }

        }

        public static void TestDeleteCountry(int CountryID)
        {

            if (clsCountry.DeleteCountry(CountryID))
            {
                Console.WriteLine($"Country With ID = {CountryID} is Deleted Successfully");
            }
            else
            {
                Console.WriteLine($"Country With ID = {CountryID} Deleted is Failed");
            }

        }

        public static void ListCountries()
        {

            DataTable CountriesData = clsCountry.GetAllCountries();

            Console.WriteLine("Countries : \n");

            foreach (DataRow Row in CountriesData.Rows)
            {
                Console.WriteLine($"{Row["CountryID"]}, {Row["CountryName"]}, {Row["Code"]}, {Row["PhoneCode"]}");
            }

        }

        public static void TestCountryExist(int CountryID)
        {

            if (clsCountry.IsCountryExists(CountryID))

                Console.WriteLine("Yes, Country is Exists");

            else
                Console.WriteLine("No, Country is Not Exists");



        }

        public static void TestCountryExistsByName(string CountryName)
        {

            if (clsCountry.IsCountryExistsByName(CountryName))

                Console.WriteLine($"Country {CountryName} is Found");
            else
                Console.WriteLine($"Country {CountryName} is Not Found");

        }

        public static void TestFindCountryByName(string CountryName)
        {

            clsCountry Country1 = clsCountry.Find(CountryName); 

            if (Country1 != null)
            {
               
                Console.WriteLine("Country is Found :-) ");

                Console.WriteLine("{0} , {1} , {2} , {3}", Country1.CountryID,
                    Country1.CountryName, Country1.Code, Country1.PhoneCode);
            }
            else
            {
                Console.WriteLine("Country is Not Found :-(");
            }

        }

        public static int[] FillArray(int ArrLength)
        {
            int[] Arr = new int[ArrLength];

            for (int i = 0; i < ArrLength; i++)
            {
                Arr[i] = i + 3;
            }

            return Arr;
        }

        public static int SumEvenNumbersOfArray(int[] arr)
        {
            int SumEvenArr = 0;

            for (int i = 0; i < arr.Length; i++)
            {

                if (arr[i] % 2 == 0) 
                {
                    SumEvenArr += arr[i];
                    continue;
                }
            }

            return SumEvenArr;
        }

        public static int SumOddNumbersOfArray(int[] arr)
        {
            int SumOddArr = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 !=  0)
                {
                    SumOddArr += arr[i];
                }
            }


            return SumOddArr;
        }

        public static void PrintResultSumArr(string WhoIsPrint,int Sumarry)
        {
            Console.WriteLine("Sum " + WhoIsPrint + " Arr = {0}", Sumarry);
        }

        public static void PrintArray(int[] Arr)
        {
            Console.Write("Array Elements : ");

            foreach (int i in Arr)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine("\n");
        }

        static void Main(string[] args)
        {

            int[] Arr = FillArray(20);
            PrintArray(Arr);
            PrintResultSumArr("Even", SumEvenNumbersOfArray(Arr));
            PrintResultSumArr("Odd", SumOddNumbersOfArray(Arr));

            /*
            //TestFindContact(1);
            //TestAddNewContact();
            //TestUpdateContact(1);
            //TestDeleteContact(18);
            //ListContacts();
            //TestIsContactExist(2);


            //TestFindCountry(2);
            //TestAddNewCountry();
            //UpdateCountry(10);
            //TestDeleteCountry(11);
            //ListCountries();

            //TestCountryExist(1);
            //TestCountryExistsByName("Canada");

            //TestFindCountryByName("Yemen");
            */


        }



    }
}
