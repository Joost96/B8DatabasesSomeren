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
                SomerenModel.Student student = new SomerenModel.Student(id, naam);
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


        //Made by: Davut
        // pakt de drankjes en de hoeveelheid voorraad uit de database
        public List<SomerenModel.DrankVoorraad> DB_GetDrankVoorraad()
        {
            SqlConnection connection = openConnectieDB();
            List<SomerenModel.DrankVoorraad> drankVoorraad_lijst = new List<SomerenModel.DrankVoorraad>();

            StringBuilder sb = new StringBuilder();
            // de query die zoekt welke drankjes er getoont moet worden
            sb.Append("SELECT naam, voorraad, drankId ");
            sb.Append("FROM dbo.B8_Voorraad ");
            sb.Append("WHERE voorraad > 1 AND prijs > 1.00 AND naam <> 'Water' AND naam <> 'Sinas' AND naam <> 'Kersensap' ");
            sb.Append("ORDER BY voorraad, prijs, aantalVerkocht");

            String sql = sb.ToString();

            // connection maken met database
            SqlCommand command = new SqlCommand(sql, connection);
            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();

            // leest alle data van de db tabellen op en vult een list hiermee
            while (reader.Read())
            {
                string naam = reader.GetString(0);
                int voorraad = reader.GetInt32(1);
                int drankId = reader.GetInt32(2);
                SomerenModel.DrankVoorraad drankVoorraad = new SomerenModel.DrankVoorraad(drankId, naam, voorraad);
                drankVoorraad_lijst.Add(drankVoorraad);
            }
            sluitConnectieDB(connection);

            // de gevulde list wordt gereturnt
            return drankVoorraad_lijst;
        }

        //Door Joost
        public int aantalKlanten(DateTime from, DateTime to)
        {
            SqlConnection connection = openConnectieDB();
            int result = 0;

            StringBuilder sb = new StringBuilder();
            // schrijf hier een query om te zorgen dat er een lijst met studenten wordt getoond
            sb.Append("SELECT COUNT(DISTINCT student) ");
            sb.Append("FROM B8_Verkopen ");
            sb.Append("WHERE datum > @dfrom AND datum < @dto");

            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);

            SqlParameter dfrom = new SqlParameter("@dfrom", System.Data.SqlDbType.DateTime);
            SqlParameter dto = new SqlParameter("@dto", System.Data.SqlDbType.DateTime);
            dfrom.Value = from;
            dto.Value = to;
            command.Parameters.Add(dfrom);
            command.Parameters.Add(dto);

            command.Prepare();
            result = (int)command.ExecuteScalar();

            sluitConnectieDB(connection);
            return result;
        }

        //Door Joost
        public int afzet(DateTime from, DateTime to)
        {
            SqlConnection connection = openConnectieDB();
            int result = 0;

            StringBuilder sb = new StringBuilder();
            // schrijf hier een query om te zorgen dat er een lijst met studenten wordt getoond
            sb.Append("SELECT SUM(aantal) AS aantal ");
            sb.Append("FROM B8_Verkopen ");
            sb.Append("WHERE datum > @dfrom AND datum < @dto");

            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);

            SqlParameter dfrom = new SqlParameter("@dfrom", System.Data.SqlDbType.DateTime);
            SqlParameter dto = new SqlParameter("@dto", System.Data.SqlDbType.DateTime);
            dfrom.Value = from;
            dto.Value = to;
            command.Parameters.Add(dfrom);
            command.Parameters.Add(dto);

            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (!reader.IsDBNull(0))
                    result = reader.GetInt32(0);
            }

            sluitConnectieDB(connection);
            return result;
        }

        //Door Joost
        public double omzet(DateTime from, DateTime to)
        {
            SqlConnection connection = openConnectieDB();
            double result = 0;

            StringBuilder sb = new StringBuilder();
            // schrijf hier een query om te zorgen dat er een lijst met studenten wordt getoond
            sb.Append("SELECT SUM(vo.prijs * CAST(ve.aantal AS DECIMAL(7,2))) ");
            sb.Append("FROM B8_Verkopen ve ");
            sb.Append("JOIN Voorraad vo ON ve.drankId = vo.drankId ");
            sb.Append("WHERE datum > @dfrom AND datum < @dto");

            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);

            SqlParameter dfrom = new SqlParameter("@dfrom", System.Data.SqlDbType.DateTime);
            SqlParameter dto = new SqlParameter("@dto", System.Data.SqlDbType.DateTime);
            dfrom.Value = from;
            dto.Value = to;
            command.Parameters.Add(dfrom);
            command.Parameters.Add(dto);

            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (!reader.IsDBNull(0))
                    result = (double)reader.GetDecimal(0);
            }

            sluitConnectieDB(connection);
            return result;
        }

        public void bestel(SomerenModel.Student student, SomerenModel.DrankVoorraad voorraad)
        {
            SqlConnection connection = openConnectieDB();

            StringBuilder sb = new StringBuilder();
            // schrijf hier een query om te zorgen dat er een lijst met studenten wordt getoond
            sb.Append("INSERT INTO [dbo].[B8_Verkopen] ([student] ,[datum] ,[drankId] ,[aantal])");
            sb.Append(" VALUES (@student, @datum, @drankId, @aantal)");
         
            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);

            SqlParameter studentParam = new SqlParameter("@student", System.Data.SqlDbType.Int);
            SqlParameter datum = new SqlParameter("@datum", System.Data.SqlDbType.DateTime);
            SqlParameter drankId = new SqlParameter("@drankId", System.Data.SqlDbType.Int);
            SqlParameter aantal = new SqlParameter("@aantal", System.Data.SqlDbType.Int);

            studentParam.Value = student.getId();
            datum.Value = DateTime.Now;
            drankId.Value = voorraad.getId();
            aantal.Value = 1;

            command.Parameters.Add(studentParam);
            command.Parameters.Add(datum);
            command.Parameters.Add(drankId);
            command.Parameters.Add(aantal);



            command.Prepare();
            command.ExecuteNonQuery();

            sluitConnectieDB(connection);
        }
    
    }
}
