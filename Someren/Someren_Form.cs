using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Someren
{
    public partial class Someren_Form : Form
    {

        private static Someren_Form instance;

        public Someren_Form() { InitializeComponent(); }

        public static Someren_Form Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Someren_Form();
                }
                return instance;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showDashboard();
            toolStripStatusLabel1.Text = DateTime.Now.ToString();
        }

        private void showDashboard()
        {
            panel1.Controls.Clear();

            groupBox1.Text = "TODO LIJST";
            Label l = new Label();
            l.Height = 500;
            l.Text = "-bierfust controleren";
            l.Text += "\r\n-kamerindeling maken";
            panel1.Controls.Add(l);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            // toon hier een message "Weet je zeker dat je wilt afsluiten?"
            // Message msg = new Message();
            if ((MessageBox.Show("Weet je zeker dat je SomerenAdministratie wilt afsluiten?", "SomerenAdministratie Afsluiten?",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                Application.Exit();
            }

        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            SomerenUI.showStudents();
        }

        private void overSomerenAppToolStripMenuItem_Click(object sender, EventArgs e)
        {


            panel1.Controls.Clear();

            groupBox1.Text = "TODO LIJST";
            Label l = new Label();
            l.Height = 500;
            l.Text = "Deze applicatie is ontwikkeld voor 1.3 Project Databases, opleiding Informatica, Hogeschool Inholland Haarlem";

            panel1.Controls.Add(l);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // er gebeurt niks als je op de menustrip drukt
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDashboard();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "Studenten";
            this.panel1.Controls.Add(SomerenUI.showStudents());

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Focus();
            this.Enabled = true;
            this.Visible = true;
        }

        private void notifyIcon1_Click(object sender, MouseEventArgs e)
        {
            this.Focus();
            this.Enabled = true;
            this.Visible = true;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toonDocentenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "Docenten";
            this.panel1.Controls.Add(SomerenUI.showDocenten());
        }

        private void drankvoorraadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Made by: Davut
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "DrankVoorraad";
            this.panel1.Controls.Add(SomerenUI.showDrankVoorraad());
        }

        //Door Joost
        private void omzetrapportageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "Omzetrapportage";

            MonthCalendar monthCalendar = new MonthCalendar();

            monthCalendar.MaxDate = DateTime.Now;
            monthCalendar.MaxSelectionCount = 31;
            monthCalendar.ShowWeekNumbers = true;
            monthCalendar.DateSelected += new DateRangeEventHandler(this.monthCalendar_DateSelected);
            this.panel1.Controls.Add(monthCalendar);
            this.panel1.Controls.Add(infolabel);
        }
        private Label infolabel = new Label();
        //Door Joost
        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            //Text = "Start = " + e.Start.ToShortDateString() +
            //    " : End = " + e.End.ToShortDateString();
            SomerenUI.getOmzetrapportage(ref infolabel, e.Start, e.End);
            infolabel.AutoSize = true;
            infolabel.Location = new Point(0, 200);
        }

        private ListView studentenList;

        private ListView drankenList;

        //Door Juan
        private void kassaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "Studenten";
            studentenList = SomerenUI.showStudents();

            drankenList = SomerenUI.showDrankVoorraad();

            drankenList.Location = new Point(85, 0);

            studentenList.HideSelection = false;
            studentenList.FullRowSelect = true;

            drankenList.HideSelection = false;
            drankenList.FullRowSelect = true;

            this.panel1.Controls.Add(studentenList);
            this.panel1.Controls.Add(drankenList);
            

            Button afrekenen = new Button();
            afrekenen.Text = "Afrekenen";
            this.panel1.Controls.Add(afrekenen);

            afrekenen.Location = new Point(490, 200);

            afrekenen.Click += afrekenenEvent;
        }

        private void afrekenenEvent(object sender, EventArgs e)
        {
            foreach (ListViewItem sitem in studentenList.SelectedItems)
            {
                SomerenModel.Student student = (SomerenModel.Student)sitem.Tag;

                foreach (ListViewItem vitem in drankenList.SelectedItems)
                {
                    SomerenModel.DrankVoorraad voorraad = (SomerenModel.DrankVoorraad)vitem.Tag;

                    SomerenDB somerenDB = new SomerenDB();
                    somerenDB.bestel(student, voorraad);

                }
            }
        }


        //Made by: Davut
        // laat alle begeleiders zien
        // kan ook begeleiders toevoegen en verwijderen
        private TextBox txt_id = new TextBox();

        public void begeleidersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "Begeleiders";
            this.panel1.Controls.Add(SomerenUI.ShowBegeleiders());

            // voeg een tekstveld zodat de gebruiker een ID kan invullen
            this.panel1.Controls.Add(txt_id);
            txt_id.Location = new Point(370, 180);
            txt_id.Width = 125;

            // Label voor het duidelijk maken van wat je kan doen
            Label lbl_id = new Label();
            lbl_id.Text = "Geef de ID van de docent/begeleider";
            lbl_id.Location = new Point(350, 165);
            lbl_id.Width = 200;
            this.panel1.Controls.Add(lbl_id);

            // button voor nieuwe begeleiders
            Button nieuweBegeleider = new Button();
            nieuweBegeleider.Text = "Nieuwe Begeleider";
            nieuweBegeleider.Width = 125;
            this.panel1.Controls.Add(nieuweBegeleider);
            nieuweBegeleider.Location = new Point(300, 200);
            nieuweBegeleider.Click += BegeleiderInsertEvent;

            // nieuwe button voor verwijderen van een begeleider
            Button begeleiderVerwijder = new Button();
            begeleiderVerwijder.Text = "Begeleider Verwijderen";
            begeleiderVerwijder.Width = 125;
            this.panel1.Controls.Add(begeleiderVerwijder);
            begeleiderVerwijder.Location = new Point(440, 200);
            begeleiderVerwijder.Click += BegeleiderDeleteEvent;

        }

        // Made By: Davut Demir
        // voert het proces van een nieuwe begeleider toevoegen uit
        // ververst ook het scherm
        public void BegeleiderInsertEvent(object sender, EventArgs e)
        {
            SomerenDB db = new SomerenDB();
            db.BegeleiderInsert(txt_id.Text);
            begeleidersToolStripMenuItem.PerformClick();
        }

        // Made By: Davut Demir
        // voert het proces van een begeleider verwijderen uit
        // ververst ook het scherm
        public void BegeleiderDeleteEvent(object sender, EventArgs e)
        {
            SomerenDB db = new SomerenDB();

            // een waarschuwings popup toegevoegd
            DialogResult dialogResult = MessageBox.Show("Weet u zeker dat u deze begeleider wilt verwijderen??", "Verwijderen", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            db.BegeleiderDelete(txt_id.Text);
            begeleidersToolStripMenuItem.PerformClick();
        }
        //gemaakt door Joost
        private void roosterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const int WIDTH = 490;
            const int HEIGHT = 380;
            const int HEADER_HEIGHT = 20;
            const int AANTAL_DAGEN = 2;

            TimeSpan startTijd = new TimeSpan(8, 0, 0);
            TimeSpan eindTijd = new TimeSpan(20, 0, 0);
            int totalMin = Convert.ToInt32((eindTijd - startTijd).TotalMinutes);
            float minPixels = (HEIGHT - HEADER_HEIGHT) / (float)totalMin;

            DateTime startDatum = new DateTime(2017, 4, 10);
            DateTime eindDatum = new DateTime(2017, 4, 11);

            //maak de picturebox
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "Rooster";
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = new Bitmap(WIDTH + 1, HEIGHT + 1);
            pictureBox.Size = new Size(WIDTH + 1, HEIGHT + 1);
            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                // draw black background
                g.Clear(Color.White);
                //make coloms
                for (int i = 0; i < AANTAL_DAGEN; i++)
                {
                    int xPosLine = WIDTH / AANTAL_DAGEN * (i + 1);
                    g.DrawLine(Pens.Black, xPosLine, 0, xPosLine, HEIGHT);
                    int xPosDag = WIDTH / AANTAL_DAGEN * i;
                    Label dag = new Label();
                    dag.Location = new Point(xPosDag + 10, 5);
                    dag.Text = ((DayOfWeek)i + 1).ToString();
                    dag.BackColor = Color.White;
                    dag.AutoSize = true;
                    this.panel1.Controls.Add(dag);
                }

                //add the rooster items
                SomerenDB db = new SomerenDB();
                List<SomerenModel.RoosterItem> rooster = db.getRoosterInfo(startDatum, eindDatum);
                foreach (SomerenModel.RoosterItem item in rooster)
                {
                    Console.Write(item);
                    int dag = Convert.ToInt32((item.getDatum() - startDatum).TotalDays);
                    int start = Convert.ToInt32((item.getStartTijd() - startTijd).TotalMinutes);
                    int duur = Convert.ToInt32((item.getEindTijd() - item.getStartTijd()).TotalMinutes);

                    //bereken positie en groote
                    Point pos = new Point((WIDTH / AANTAL_DAGEN * dag),
                        Convert.ToInt32(start * minPixels) + HEADER_HEIGHT);
                    Size size = new Size((WIDTH / AANTAL_DAGEN * (dag + 1)),
                        Convert.ToInt32(duur * minPixels));
                    Rectangle rectRItem = new Rectangle(pos, size);

                    //teken de rectang;e
                    g.FillRectangle(Brushes.Wheat, rectRItem);
                    g.DrawRectangle(Pens.Black, rectRItem);

                    Label info = new Label();
                    info.Location = new Point(pos.X + 10, pos.Y + 5);
                    //voeg de text toe
                    info.Text = String.Format("{0} - {1} \n {2} \n {3}",
                        item.getStartTijd(), item.getEindTijd(),
                        item.getActiviteit(), item.getBegeleider().getNaam());

                    info.BackColor = Color.Wheat;
                    info.AutoSize = true;
                    panel1.Controls.Add(info);
                }

                // make table
                Rectangle table = new Rectangle(0, HEADER_HEIGHT, WIDTH, HEIGHT - HEADER_HEIGHT);
                Rectangle Header = new Rectangle(0, 0, WIDTH, HEADER_HEIGHT);
                g.DrawRectangle(Pens.Black, table);
                g.DrawRectangle(Pens.Black, Header);
            }
            pictureBox1.Invalidate();
            this.panel1.Controls.Add(pictureBox);
        }
    }
}
