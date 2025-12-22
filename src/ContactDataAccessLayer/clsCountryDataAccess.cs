using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ContactDataAccessLayer
{
    public class clsCountryDataAccess
    {

        public static bool GetCountryByID(int CountryID, ref string CountryName, 
            ref string Code, ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {

                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader(); 

                if (Reader.Read())
                {
                    isFound = true;
                    CountryName = (string)Reader["CountryName"];


                    if (Reader["Code"] != System.DBNull.Value)
                    
                        Code = (string)Reader["Code"];
                    
                    else
                        Code = "NULL";
                    


                    if (Reader["PhoneCode"] != System.DBNull.Value)
                    
                        PhoneCode = (string)Reader["PhoneCode"];
                    
                    else
                        PhoneCode = "NULL";
                    
                }

                Reader.Close();

            }
            catch (Exception ex)
            {
                isFound = false;
                Console.WriteLine(ex.ToString()); 
            }
            finally
            {
                Connection.Close(); 
            }

            return isFound;
        }

        public static bool GetCountryByName(string CountryName, ref int CountryID, ref string Code,
            ref string PhoneCode)
        {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Countries WHERE CountryName = @CountryName"; 

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    isFound = true;

                    CountryID = (int)Reader["CountryID"];
                    Code = (string)Reader["Code"];
                    PhoneCode = (string)Reader["PhoneCode"];
                }

            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                Connection.Close(); 
            }

            return isFound;


        }

        public static int AddNewCountry(string CountryName,string CountryCode, string PhoneCode)
        {

            int CountryID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"INSERT INTO Countries
                             VALUES
                             (@CountryName,@CountryCode,@PhoneCode);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryName", CountryName);


            if (CountryCode != "")
            {
                Command.Parameters.AddWithValue("@CountryCode", CountryCode);
            }
            else
            {
                Command.Parameters.AddWithValue("@CountryCode", System.DBNull.Value);
            }

            if (PhoneCode != "")
            {
                Command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            }
            else
            {
                Command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);
            }

            try
            {
                Connection.Open();

                object RESULT = Command.ExecuteScalar();

                if (RESULT != null && int.TryParse(RESULT.ToString(), out int InsertedID))
                {
                    CountryID = InsertedID;
                }


            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close(); 
            }

            return CountryID;

        }

        public static bool UpdateCountry(int CountryID, string CountryName, string Code, string PhoneCode)
        {

            int RowAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"UPDATE Countries
                            SET CountryName = @CountryName,
                                Code = @Code,
                                PhoneCode = @PhoneCode
                            WHERE CountryID = @CountryID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryName", CountryName);
            Command.Parameters.AddWithValue("@CountryID", CountryID);

            if (Code != "")
            {
                Command.Parameters.AddWithValue("@Code", Code);
            }
            else
            {
                Command.Parameters.AddWithValue("@Code", System.DBNull.Value);
            }

            if (PhoneCode != "")
            {
                Command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            }
            else
            {
                Command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);
            }

            try
            {

                Connection.Open();
                RowAffected = Command.ExecuteNonQuery(); 
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connection.Close(); 
            }

            return (RowAffected > 0);

        }

        public static bool DeleteCountry(int CountryID)
        {

            int RowAffected = 0; 

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"DELETE Countries WHERE CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {

                Connection.Open();
                RowAffected = Command.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connection.Close();
            }

            return (RowAffected > 0);

        }

        public static DataTable GetAllCountries()
        {

            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * FROM Countries"; 

            SqlCommand Command = new SqlCommand(Query,Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader(); 
                
                if (Reader.HasRows)
                {
                    dt.Load(Reader);
                }

                Reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connection.Close(); 
            }

            return dt;

        }

        public static bool isCountryExists(int CountryID)
        {
            
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Found = 1 FROM Countries WHERE CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(Query,Connection);

            Command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {

                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                isFound = Reader.HasRows;


            }
            catch (System.Exception ex)
            {
                isFound = false;
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connection.Close();
            }

            return isFound;



        }

        public static bool isCountryExistsByName(string CountryName)
        {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Found = 1 FROM Countries WHERE CountryName = @CountryName";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {

                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                isFound = Reader.HasRows;


            }
            catch (System.Exception ex)
            {
                isFound = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return isFound;



        }



    }
}
