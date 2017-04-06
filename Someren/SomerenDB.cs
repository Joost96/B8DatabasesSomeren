using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;


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
        //Door Juan
        public void bestel(SomerenModel.Student student, SomerenModel.DrankVoorraad voorraad)
        {
            SqlConnection connection = openConnectieDB();

            StringBuilder sb = new StringBuilder();

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


        // Made By: Davut Demir
        // Selecteerd alle huidige begeleiders uit de database
        public List<SomerenModel.Begeleider> BegeleiderSelect()
        {
            SqlConnection connection = openConnectieDB();
            List<SomerenModel.Begeleider> Begeleider_lijst = new List<SomerenModel.Begeleider>();

            StringBuilder sb = new StringBuilder();
            // de query die zoekt welke begeleiders er zijn
            sb.Append("SELECT DocentId, naam ");
            sb.Append("FROM dbo.B8_Docent ");
            sb.Append("INNER JOIN dbo.B8_Begeleider ");
            sb.Append("ON DocentId = id ");
            sb.Append("Where DocentId = id");

            String sql = sb.ToString();

            // connection maken met database
            SqlCommand command = new SqlCommand(sql, connection);
            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();

            // leest alle data van de db tabellen op en vult een list hiermee
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string naam = reader.GetString(1);
                SomerenModel.Begeleider begeleider = new SomerenModel.Begeleider(id, naam);
                Begeleider_lijst.Add(begeleider);
            }
            sluitConnectieDB(connection);

            // de gevulde list wordt gereturnt
            return Begeleider_lijst;
        }

        // Made By: Davut Demir
        // Selecteerd begeleider op id uit de database
        public SomerenModel.Begeleider getBegeleiderById(int id)
        {
            SqlConnection connection = openConnectieDB();
            SomerenModel.Begeleider begeleider = null;

            StringBuilder sb = new StringBuilder();
            // de query die zoekt welke begeleiders er zijn
            sb.Append("SELECT naam ");
            sb.Append("FROM dbo.B8_Docent ");
            sb.Append("INNER JOIN dbo.B8_Begeleider ");
            sb.Append("ON DocentId = id ");
            sb.Append("Where DocentId = @id");

            String sql = sb.ToString();

            // connection maken met database
            SqlCommand command = new SqlCommand(sql, connection);
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
            idParam.Value = id;

            command.Parameters.Add(idParam);
            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();

            // leest alle data van de db tabellen op en vult een list hiermee
            while (reader.Read())
            {
                string naam = reader.GetString(0);
                begeleider = new SomerenModel.Begeleider(id, naam);
            }
            sluitConnectieDB(connection);

            // de gevulde list wordt gereturnt
            return begeleider;
        }

        // Made By: Davut Demir
        // voegt een nieuwe begeleider toe
        public void BegeleiderInsert(string id)
        {
            SqlConnection connection = openConnectieDB();

            StringBuilder sb = new StringBuilder();
            // de query die een nieuwe begeleider toevoegd
            sb.Append("INSERT INTO B8_Begeleider (DocentId) ");
            sb.Append("VALUES (@id)");

            String sql = sb.ToString();

            // connection maken met database
            SqlCommand command = new SqlCommand(sql, connection);

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
            try
            {
                idParam.Value = Int32.Parse(id);

                command.Parameters.Add(idParam);

                command.Prepare();

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write("Verkeerde invoer, moet een getal zijn die een docent heeft.");
            }
            sluitConnectieDB(connection);
        }

        // Made By: Davut Demir
        // verwijderd een begeleider van de database
        public void BegeleiderDelete(string id)
        {
            SqlConnection connection = openConnectieDB();

            StringBuilder sb = new StringBuilder();
            // de query die de begeleider verwijderd uit de database
            // de docent blijft wel een docent
            sb.Append("DELETE FROM B8_Begeleider ");
            sb.Append("WHERE DocentId = @id");

            String sql = sb.ToString();

            // connection maken met database
            SqlCommand command = new SqlCommand(sql, connection);

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
            try
            {
                idParam.Value = Int32.Parse(id);


                command.Parameters.Add(idParam);

                command.Prepare();

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write("Verkeerde invoer, moet een getal zijn die een docent heeft.");
            }
            sluitConnectieDB(connection);
        }
        //door Joost
        public List<SomerenModel.RoosterItem> getRoosterInfo(DateTime from, DateTime to)
        {
            SqlConnection connection = openConnectieDB();
            List<SomerenModel.RoosterItem> rooster = new List<SomerenModel.RoosterItem>();

            StringBuilder sb = new StringBuilder();
            // schrijf hier een query om te zorgen dat er een lijst met studenten wordt getoond
            sb.Append("SELECT Activiteit,Begeleider,Datum,tijdStart,tijdEind ");
            sb.Append("FROM B8_Rooster ");
            sb.Append("WHERE datum >= @dfrom AND datum <= @dto");

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
            while (reader.Read())
            {
                int activiteitId = reader.GetInt32(0);
                int begeleiderId = reader.GetInt32(1);
                DateTime datum = reader.GetDateTime(2);
                TimeSpan start = reader.GetTimeSpan(3);
                TimeSpan eind = reader.GetTimeSpan(4);
                SomerenModel.Begeleider begeleider = getBegeleiderById(begeleiderId);
                SomerenModel.Activiteit activiteit = getActiviteitById(activiteitId);
                SomerenModel.RoosterItem item = new SomerenModel.RoosterItem(activiteit, begeleider,
                    datum, start, eind);
                rooster.Add(item);
            }

            sluitConnectieDB(connection);
            return rooster;
        }

        //Door Juan
        //06-04-17, opdracht 6, variant A
        public List<SomerenModel.Activiteit> DB_getActiviteit()
        {
            SqlConnection connection = openConnectieDB();
            List<SomerenModel.Activiteit> activiteitenLijst = new List<SomerenModel.Activiteit>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT id, Omschrijving, aantalStudenten, aantalBegeleiders ");
            sb.Append("FROM dbo.B8_Activiteit");
            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);
            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string omschrijving = reader.GetString(1);
                int aantalStudenten = reader.GetInt32(2);
                int aantalBegeleiders = reader.GetInt32(3);
                SomerenModel.Activiteit activiteiten = new SomerenModel.Activiteit(id, omschrijving, aantalStudenten, aantalBegeleiders);
                activiteitenLijst.Add(activiteiten);
            }
            sluitConnectieDB(connection);

            return activiteitenLijst;
        }

        public SomerenModel.Activiteit getActiviteitById(int id)
        {
            SqlConnection connection = openConnectieDB();
            SomerenModel.Activiteit activiteit = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Omschrijving, aantalStudenten, aantalBegeleiders ");
            sb.Append("FROM dbo.B8_Activiteit ");
            sb.Append("WHERE id = @id ");
            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);

            idParam.Value = id;

            command.Parameters.Add(idParam);
            command.Prepare();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string omschrijving = reader.GetString(0);
                int aantalStudenten = reader.GetInt32(1);
                int aantalBegeleiders = reader.GetInt32(2);
                activiteit = new SomerenModel.Activiteit(id, omschrijving, aantalStudenten, aantalBegeleiders);
            }
            sluitConnectieDB(connection);

            return activiteit;
        }

        public void DB_toevoegenActiviteit(string omschrijving, int aStudenten, int aBegeleiders)
        {
            SqlConnection connection = openConnectieDB();

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO [dbo].[B8_Activiteit] ([Omschrijving] ,[aantalStudenten] ,[aantalBegeleiders])");
            sb.Append(" VALUES (@oms, @stud, @beg)");

            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);

            // SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
            SqlParameter omsParam = new SqlParameter("@oms", System.Data.SqlDbType.NVarChar, 50);
            SqlParameter studParam = new SqlParameter("@stud", System.Data.SqlDbType.Int);
            SqlParameter begParam = new SqlParameter("@beg", System.Data.SqlDbType.Int);

            omsParam.Value = omschrijving;
            studParam.Value = aStudenten;
            begParam.Value = aBegeleiders;


            command.Parameters.Add(omsParam);
            command.Parameters.Add(studParam);
            command.Parameters.Add(begParam);

            command.Prepare();
            command.ExecuteNonQuery();

            sluitConnectieDB(connection);

        }

        public void DB_deleteActiviteit(int id)
        {
            SqlConnection connection = openConnectieDB();

            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE FROM [dbo].[B8_Activiteit] ");
            sb.Append("WHERE id = @id ");
            //sb.Append(" ON DELETE CASCADE");

            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);

            idParam.Value = id;

            command.Parameters.Add(idParam);

            command.Prepare();
            try
            {
                command.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                MessageBox.Show("Could not delete, used in Rooster");
            }

            sluitConnectieDB(connection);
        }

        public void DB_wijzigActiviteit(int id, string omschrijving, int aStudenten, int aBegeleiders)
        {

            SqlConnection connection = openConnectieDB();

            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE [dbo].[B8_Activiteit] ");
            sb.Append("SET omschrijving = @oms, aantalStudenten = @stud, aantalBegeleiders = @beg ");
            sb.Append("WHERE id = @id");

            String sql = sb.ToString();

            SqlCommand command = new SqlCommand(sql, connection);

            // SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int);
            SqlParameter omsParam = new SqlParameter("@oms", System.Data.SqlDbType.NVarChar, 50);
            SqlParameter studParam = new SqlParameter("@stud", System.Data.SqlDbType.Int);
            SqlParameter begParam = new SqlParameter("@beg", System.Data.SqlDbType.Int);

            idParam.Value = id;
            omsParam.Value = omschrijving;
            studParam.Value = aStudenten;
            begParam.Value = aBegeleiders;

            command.Parameters.Add(idParam);
            command.Parameters.Add(omsParam);
            command.Parameters.Add(studParam);
            command.Parameters.Add(begParam);

            command.Prepare();
            command.ExecuteNonQuery();

            sluitConnectieDB(connection);
        }
    }
}
