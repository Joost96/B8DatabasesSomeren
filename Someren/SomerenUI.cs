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
            //SomerenDB.
            SomerenModel.Student s1 = new SomerenModel.Student();
            s1.setNaam("Henk");
            SomerenModel.Student s2 = new SomerenModel.Student();
            s2.setNaam("Piet");
            SomerenModel.Student s3 = new SomerenModel.Student();
            s3.setNaam("Amber");

            sl.Add(s1);
            sl.Add(s2);
            sl.Add(s3);

            int aantal = sl.Count();
            ListView c = new ListView();
            c.Height = 1000;
            ListViewItem li = new ListViewItem();
            li.SubItems.Add(s1.getNaam());
            li.SubItems.Add(s1.getId().ToString());
            c.Items.Add(s1.getNaam());
            c.Items.Add(s2.getNaam());
            c.Items.Add(s3.getNaam());
            c.Items.Add(li);

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
