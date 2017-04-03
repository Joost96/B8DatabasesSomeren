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

        public static Control addUILabel(string text)
        {
            Label l = new Label();
            l.Text = text;
            return l;
        }
        

    }
}
