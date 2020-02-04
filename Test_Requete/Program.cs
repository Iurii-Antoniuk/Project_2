using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TestQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            //set the connection string
            string connString = @"Data Source=DESKTOP-HU4UIE0\SQLEXPRESS;Initial Catalog=GroceriesDB;Integrated Security=True;";

            //variables to store the query results
            int ID;
            string nameProduct;
            decimal prix;

            
            // query UPDATE

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    //retrieve the SQL Server instance version
                    string query = @"UPDATE Product 
                                    SET prix=30
                                    WHERE id=2;";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, connection);

                    connection.Open();

                    cmd.ExecuteNonQuery();

                    connection.Close();
                    Console.WriteLine("UPDATE statement successfully executed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }
            
            
            // query SELECT

            /*try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(connString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"SELECT *
                                     FROM Product
                                     ;
                                     ";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                    Console.WriteLine("Retrieved records:");

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ID = dr.GetInt32(0);
                            nameProduct = dr.GetString(1);
                            prix = dr.GetDecimal(2);


                            //display retrieved record
                            Console.WriteLine("{0},{1},{2}", ID.ToString(), nameProduct, prix);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception : " + ex.Message);
            } */


            // INSERT INTO





        }

    }



}
    

