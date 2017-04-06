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

            TimeSpan startTijd = new TimeSpan(7, 0, 0);
            TimeSpan eindTijd = new TimeSpan(22, 0, 0);
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
                        item.getActiviteit().getOmschrijving(), item.getBegeleider().getNaam());

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

        //Door Juan
        //06-04-17, opdracht 6, variant A
        private void activiteitenlijstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "Activiteitenlijst";
            activiteit = SomerenUI.showActiviteiten();
            this.panel1.Controls.Add(activiteit);

            activiteit.HideSelection = false;
            activiteit.FullRowSelect = true;

            Button btn_activiteitToevoegen = new Button();
            btn_activiteitToevoegen.Text = "Activiteit toevoegen";
            btn_activiteitToevoegen.Width = 150;
            this.panel1.Controls.Add(btn_activiteitToevoegen);
            btn_activiteitToevoegen.Location = new Point(410, 30);

            btn_activiteitToevoegen.Click += toevoegenEvent;

            Button btn_updateActiviteit = new Button();
            btn_updateActiviteit.Text = "Activiteit wijzigen";
            btn_updateActiviteit.Width = 150;
            this.panel1.Controls.Add(btn_updateActiviteit);
            btn_updateActiviteit.Location = new Point(410, 70);


            btn_updateActiviteit.Click += wijzigenEvent;

            Button btn_deleteActiviteit = new Button();
            btn_deleteActiviteit.Text = "Activiteit verwijderen";
            btn_deleteActiviteit.Width = 150;
            this.panel1.Controls.Add(btn_deleteActiviteit);
            btn_deleteActiviteit.Location = new Point(410, 110);

            btn_deleteActiviteit.Click += deleteEvent;
        }

        private TextBox tb_naamActiviteit = new TextBox();
        private TextBox tb_aantalStudenten = new TextBox();
        private TextBox tb_aantalBegeleiders = new TextBox();

        private TextBox tbw_naamActiviteit = new TextBox();
        private TextBox tbw_aantalStudenten = new TextBox();
        private TextBox tbw_aantalBegeleiders = new TextBox();


        private void toevoegenEvent(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "Nieuwe activiteit toevoegen";

            //LABEL Naam
            Label l_naamActiviteit = new Label();
            this.panel1.Controls.Add(l_naamActiviteit);
            l_naamActiviteit.Text = "Naam activiteit";
            l_naamActiviteit.Location = new Point(10, 20);
            //TEXTBOX Naam
            tb_naamActiviteit = new TextBox();
            this.panel1.Controls.Add(tb_naamActiviteit);
            tb_naamActiviteit.Location = new Point(10, 50);

            //LABEL Aantel studenten
            Label l_aantalStudenten = new Label();
            this.panel1.Controls.Add(l_aantalStudenten);
            l_aantalStudenten.Text = "Aantal studenten";
            l_aantalStudenten.Location = new Point(10, 80);
            //TEXTBOX Aantal studenten
            tb_aantalStudenten = new TextBox();
            this.panel1.Controls.Add(tb_aantalStudenten);
            tb_aantalStudenten.Location = new Point(10, 110);

            //LABEL Aantal begeleiders
            Label l_aantalBegeleiders = new Label();
            this.panel1.Controls.Add(l_aantalBegeleiders);
            l_aantalBegeleiders.Text = "Aantal begeleiders";
            l_aantalBegeleiders.Location = new Point(10, 140);
            //TEXTBOX Aantal begeleiders
            tb_aantalBegeleiders = new TextBox();
            this.panel1.Controls.Add(tb_aantalBegeleiders);
            tb_aantalBegeleiders.Location = new Point(10, 170);

            Button btn_add = new Button();
            btn_add.Text = "Activiteit toevoegen";
            btn_add.Width = 150;
            this.panel1.Controls.Add(btn_add);
            btn_add.Location = new Point(410, 30);

            btn_add.Click += addEvent;
        }

        private void addEvent(object sender, EventArgs e)
        {
            string omschrijving = tb_naamActiviteit.Text;

            int aStudenten;
            Int32.TryParse(tb_aantalStudenten.Text, out aStudenten);

            int aBegeleiders;
            Int32.TryParse(tb_aantalBegeleiders.Text, out aBegeleiders);

            SomerenDB somerenb = new SomerenDB();
            somerenb.DB_toevoegenActiviteit(omschrijving, aStudenten, aBegeleiders);

            activiteitenlijstToolStripMenuItem.PerformClick();
        }

        private ListView activiteit;
        private SomerenModel.Activiteit huidigeBewerking;

        private void wijzigenEvent(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.groupBox1.Text = "Activiteit wijzigen";

            //LABEL Naam
            Label l_naamActiviteit = new Label();
            this.panel1.Controls.Add(l_naamActiviteit);
            l_naamActiviteit.Text = "Naam activiteit";
            l_naamActiviteit.Location = new Point(10, 20);
            //TEXTBOX Naam
            tbw_naamActiviteit = new TextBox();
            this.panel1.Controls.Add(tbw_naamActiviteit);
            tbw_naamActiviteit.Location = new Point(10, 50);

            //LABEL Aantel studenten
            Label l_aantalStudenten = new Label();
            this.panel1.Controls.Add(l_aantalStudenten);
            l_aantalStudenten.Text = "Aantal studenten";
            l_aantalStudenten.Location = new Point(10, 80);
            //TEXTBOX Aantal studenten
            tbw_aantalStudenten = new TextBox();
            this.panel1.Controls.Add(tbw_aantalStudenten);
            tbw_aantalStudenten.Location = new Point(10, 110);

            //LABEL Aantal begeleiders
            Label l_aantalBegeleiders = new Label();
            this.panel1.Controls.Add(l_aantalBegeleiders);
            l_aantalBegeleiders.Text = "Aantal begeleiders";
            l_aantalBegeleiders.Location = new Point(10, 140);
            //TEXTBOX Aantal begeleiders
            tbw_aantalBegeleiders = new TextBox();
            this.panel1.Controls.Add(tbw_aantalBegeleiders);
            tbw_aantalBegeleiders.Location = new Point(10, 170);

            Button btn_wijzig = new Button();
            btn_wijzig.Text = "Activiteit Wijzigen";
            btn_wijzig.Width = 150;
            this.panel1.Controls.Add(btn_wijzig);
            btn_wijzig.Location = new Point(410, 30);

            btn_wijzig.Click += wijzig;

            foreach (ListViewItem sitem in activiteit.SelectedItems)
            {
                huidigeBewerking = (SomerenModel.Activiteit)sitem.Tag;
            }

            tbw_aantalBegeleiders.Text = huidigeBewerking.getABegeleiders().ToString();
            tbw_aantalStudenten.Text = huidigeBewerking.getAStudenten().ToString();
            tbw_naamActiviteit.Text = huidigeBewerking.getOmschrijving();

        }

        private void wijzig(object sender, EventArgs e)
        {

            string omschrijving = tbw_naamActiviteit.Text;

            int aStudenten;
            Int32.TryParse(tbw_aantalStudenten.Text, out aStudenten);

            int aBegeleiders;
            Int32.TryParse(tbw_aantalBegeleiders.Text, out aBegeleiders);

            SomerenDB somerenb = new SomerenDB();
            somerenb.DB_wijzigActiviteit(huidigeBewerking.getActiviteitId(), omschrijving, aStudenten, aBegeleiders);

            activiteitenlijstToolStripMenuItem.PerformClick();
        }

        private void deleteEvent(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Weet u zeker dat u de geselecteerde activiteit wilt verwijderen??", "Verwijderen", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            foreach (ListViewItem sitem in activiteit.SelectedItems)
            {
                SomerenModel.Activiteit activiteit = (SomerenModel.Activiteit)sitem.Tag;

                SomerenDB somerenDB = new SomerenDB();
                somerenDB.DB_deleteActiviteit(activiteit.getActiviteitId());
            }

            activiteitenlijstToolStripMenuItem.PerformClick();
        }


    }
}
