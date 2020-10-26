using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Academic : Customer
    {
        private bool academicBooks;

        public bool AcademicBooks { get => academicBooks; set => academicBooks = value; }
        public override void AdditionalQuestion()
        {
            Console.WriteLine(" {0}\n", (AcademicBooks) ? "Academic books needed" : "Don't want Academic study books");
        }
    }
}
