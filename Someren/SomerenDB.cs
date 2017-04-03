using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Someren
{
    class SomerenDB
    {  
        private SqlConnection openConnectieDB()
        {
            string host = "spartainholland.database.windows.net";
            string db = "someren_inholland_db";
            string user = "spartainholland";
            string password = "Spartalogin1";
            //string port = "3306";

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = host;
                builder.UserID = user;
                builder.Password = password;
                builder.InitialCatalog = db;

                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                
                connection.Open();
                return connection;

            }
            catch (SqlException e)
            {
                SqlConnection connection = null;
                Console.WriteLine(e.ToString());
                return connection;
            }            
        }

        private void sluitConnectieDB(SqlConnection connection)
        {
            connection.Close();
        }

        public List<SomerenModel.Student> DB_gettudents()
        {
            SqlConnection connection = openConnectieDB();
            List<SomerenModel.Student> studenten_lijst = new List<SomerenModel.Student>();

            StringBuilder sb = new StringBuilder();
            // schrijf hier een query om te zorgen dat er een lijst met studenten wordt getoond
            sb.Append("SELECT id,naam ");
            sb.Append("FROM B8_Student");

            /* VOORBEELDQUERY */
            //sb.Append("SELECT TOP 20 pc.Name as CategoryName, p.name as ProductName ");
            //sb.Append("FROM [SalesLT].[ProductCategory] pc ");
            //sb.Append("JOIN [SalesLT].[Product] p ");
            //sb.Append("ON pc.productcategoryid = p.productcategoryid;");
            /* */

            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);
            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string naam = reader.GetString(1);
                SomerenModel.Student student = new SomerenModel.Student(id,naam);
                studenten_lijst.Add(student);
            }
            sluitConnectieDB(connection);
            return studenten_lijst;
        }

        public List<SomerenModel.Docent> DB_getdocenten()
        {
            SqlConnection connection = openConnectieDB();
            List<SomerenModel.Docent> docent_lijst = new List<SomerenModel.Docent>();

            StringBuilder sb = new StringBuilder();
            // schrijf hier een query om te zorgen dat er een lijst met studenten wordt getoond
            sb.Append("SELECT id,naam ");
            sb.Append("FROM B8_Docent");

            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);
            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string naam = reader.GetString(1);
                SomerenModel.Docent docent = new SomerenModel.Docent(id, naam);
                docent_lijst.Add(docent);
            }
            sluitConnectieDB(connection);
            return docent_lijst;
        }
        // public void 
    }
}
