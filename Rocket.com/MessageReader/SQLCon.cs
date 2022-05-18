using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageReader
{
    internal class SQLCon
    {
        public SqlConnection Connect()
        {
            Console.WriteLine("Getting Connection ...");

            //your connection string 
            string connString = "Server=localhost;Database=RocketDB;User Id=sa;Password=SQLDatabase123;";

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(connString);


            try
            {
                Console.WriteLine("Opening Connection ...");

                //open connection
                conn.Open();

                Console.WriteLine("Connection successful!");
                return conn;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

                Console.Read();
                return null;
            }
        }
        public void SendLog(string Message)
        {
            var Username = Message.Split(' ').Skip(2).FirstOrDefault();
            string commandText = "INSERT INTO AccountLogs VALUES (@message, @date);";
            string connString = "Server=localhost;Database=RocketDB;User Id=sa;Password=SQLDatabase123;";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@message", SqlDbType.NVarChar);
                command.Parameters["@message"].Value = Message;

                command.Parameters.Add("@date", SqlDbType.DateTime);
                command.Parameters["@date"].Value = DateTime.Now;

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

