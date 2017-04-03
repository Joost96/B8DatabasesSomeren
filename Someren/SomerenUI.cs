using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Someren
{
    public static class SomerenUI
    {
        public static Control showStudents()
        {
            List<SomerenModel.Student> sl = new List<SomerenModel.Student>();
            SomerenDB somerendb = new SomerenDB();
            sl = somerendb.DB_gettudents();

            ListView c = new ListView();
            c.Height = 1000;
            foreach (SomerenModel.Student student in sl)
            {
                ListViewItem li = new ListViewItem(student.getNaam());
                li.SubItems.Add(student.getId().ToString());
                c.Items.Add(li);
            }
            return c;
        }
        public static Control showDocenten()
        {
            List<SomerenModel.Docent> dl = new List<SomerenModel.Docent>();
            SomerenDB somerendb = new SomerenDB();
            dl = somerendb.DB_getdocenten();

            ListView c = new ListView();
            c.Height = 1000;
            foreach (SomerenModel.Docent docent in dl)
            {
                ListViewItem li = new ListViewItem(docent.getNaam());
                li.SubItems.Add(docent.getId().ToString());
                c.Items.Add(li);
            }
            return c;
        }

        // Made By: Davut
        // laat de drankjes zien in de vorm van een tabel
        // en of er genoeg voorraad is per drankje
        public static Control showDrankVoorraad()
        {
            List<SomerenModel.DrankVoorraad> dl = new List<SomerenModel.DrankVoorraad>();
            SomerenDB somerendb = new SomerenDB();

            // hier halen we de drankjes op van de database
            dl = somerendb.DB_GetDrankVoorraad();

            ListView c = new ListView();
            c.Height = 1000;
            c.Width = 500;
            c.View = View.Details;
            c.Columns.Add("Dranken", 200, HorizontalAlignment.Left);
            c.Columns.Add("Voorraad", 200, HorizontalAlignment.Left);

            // hier vullen we de lijst/tabel met informatie
            foreach (SomerenModel.DrankVoorraad voorraad in dl)
            {
                string naam = voorraad.getNaam();
                ListViewItem li = new ListViewItem(naam);
                if (voorraad.getvoorraad() < 10)
                {
                    string aantalVoorraad = "Voorraad bijna op!";
                    li.SubItems.Add(aantalVoorraad, System.Drawing.Color.Red, System.Drawing.Color.Red, null);
                }
                else
                {
                    string aantalVoorraad = "Voldoende voorraad.";
                    li.SubItems.Add(aantalVoorraad, System.Drawing.Color.Green, System.Drawing.Color.Green, null);
                }

                c.Items.Add(li);
            }
            return c;
        }

        //Door Joost
        public static void getOmzetrapportage(ref Label label, DateTime from, DateTime to)
        {
            SomerenDB somerendb = new SomerenDB();
            int aantalKlanten = somerendb.aantalKlanten(from, to);
            int afzet = somerendb.afzet(from, to);
            double omzet = somerendb.omzet(from, to);
            label.Text = String.Format(
                "aantal klanten : {0}\n" +
                "afzet : {1}\n" +
                "omzet: {2:C2}", aantalKlanten, afzet, omzet);
        }

        public static Control addUILabel(string text)
        {
            Label l = new Label();
            l.Text = text;
            return l;
        }
    }
}
