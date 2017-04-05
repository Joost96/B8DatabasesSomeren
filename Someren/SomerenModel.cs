﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Someren
{
    class SomerenModel
    {
        public class Student
        {
            int id;
            string naam;

            public Student(int id, string naam)
            {
                this.id = id;
                this.naam = naam;
            }
            public void setNaam(string naamStudent)
            {
                naam = naamStudent;
            }

            public string getNaam()
            {
                return naam;
            }

            public int getId()
            {
                return id;
            }

        }
        public class Docent
        {
            int id;
            string naam;

            public Docent(int id, string naam)
            {
                this.id = id;
                this.naam = naam;
            }



            public void setNaam(string naamStudent)
            {
                naam = naamStudent;
            }

            public string getNaam()
            {
                return naam;
            }

            public int getId()
            {
                return id;
            }

        }

        public class Begeleider : Docent
        {
            public Begeleider(int id, string naam) : base(id, naam)
            {
            }
        }

        // Made By: Davut
        // de class waarvan een drankje bestaat
        public class DrankVoorraad
        {
            string naam;
            int voorraad;
            int id;

            public DrankVoorraad(int id, string naam, int voorraad)
            {

                this.naam = naam;
                this.voorraad = voorraad;
                this.id = id;
            }

            public string getNaam()
            {
                return naam;
            }

            public int getvoorraad()
            {
                return voorraad;
            }

            public int getId()
            {
                return id;
            }
        }

        public class StudentList
        {
            List<SomerenModel.Student> sl = new List<SomerenModel.Student>();

            public void addList(SomerenModel.Student s)
            {
                sl.Add(s);
            }

            public List<SomerenModel.Student> getList()
            {
                return sl;
            }
        }
    }
}
