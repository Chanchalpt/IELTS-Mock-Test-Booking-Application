using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class General : Customer
    {
        private bool generalBooks;

        public bool GeneralBooks { get => generalBooks; set => generalBooks = value; }
        public override void AdditionalQuestion()
        {
            Console.WriteLine(" {0}\n", (GeneralBooks) ? "General books needed" : "Don't want General study books");
        }
    }
}
