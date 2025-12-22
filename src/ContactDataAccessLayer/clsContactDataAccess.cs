using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactDataAccessLayer
{
    public class clsContactDataAccess
    {

        public static bool GetContactByID(int ID, ref string FirstName, ref string LastName, ref string Email, ref string Phone,
            ref string Address, ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ContactID", ID);

            try
            {

                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader(); 

                if (Reader.Read())
                {

                    isFound = true;

                    FirstName = (string)Reader["FirstName"];
                    LastName = (string)Reader["LastName"];
                    Email = (string)Reader["Email"];
                    Phone = (string)Reader["Phone"];
                    Address = (string)Reader["Address"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    CountryID = (int)Reader["CountryID"];

                    if (Reader["ImagePath"] != System.DBNull.Value)
                    {
                        ImagePath = (string)Reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                }
                else
                {
                    isFound = false;
                }

                Reader.Close();

            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                Connection.Close(); 
            }

            return isFound;

        }


        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone, string Address,
            DateTime DateOfBirth, int CountryID, string ImagePath)
        {

            int ContactID = -1; 

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Insert Into Contacts
                            VALUES (@FirstName,@LastName,@Email,@Phone,@Address,
                                    @DateOfBirth,@CountryID,@ImagePath);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != null)
            {
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            }




            try
                {

                    Connection.Open();

                    object RESULT = Command.ExecuteScalar();

                    if (RESULT != null && int.TryParse(RESULT.ToString(), out int InsertedID))
                    {
                        ContactID = InsertedID;
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Connection.Close();
                }

            return ContactID;

        }


        public static bool UpdateContact(int ContactID, string FirstName, string LastName, string Email, string Phone,
            string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {

            int RowAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"

                            UPDATE Contacts
                               SET FirstName = @FirstName,
                                  LastName = @LastName,
                                  Email = @Email,
                                  Phone = @Phone,
                                  Address = @Address,
                                  DateOfBirth = @DateOfBirth,
                                  CountryID = @CountryID,
                                  ImagePath = @ImagePath
                             WHERE ContactID = @ContactID";


            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

            if (ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }


            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@ContactID", ContactID);


            try
            {

                Connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return (RowAffected > 0);

        }

        
        public static bool DeleteContact(int ContactID)
        {

            int RowAffected = 0; 

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"DELETE Contacts WHERE ContactID = @ContactID"; 

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ContactID", ContactID);

            try
            {

                Connection.Open();
                RowAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connection.Close();
            }

            return RowAffected > 0;

        }

        public static DataTable GetAllContacts()
        {

            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM Contacts"; 
            SqlCommand Command = new SqlCommand(Query, Connection);
            
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connection.Close(); 
            }

            return dt;





        }

        public static bool IsContactExist(int ContactID)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Found = 1 FROM Contacts WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@ContactID",ContactID);

            try
            {

                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                IsFound = Reader.HasRows;
                Reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connection.Close(); 
            }

            return IsFound;
        }


    }
}
