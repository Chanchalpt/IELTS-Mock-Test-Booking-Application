using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public enum ModuleSelection
    {
        General = 1,
        Academic
    }
    public delegate void CustomerFunction();
    public abstract class Customer : ICustomer
    {
        private string firstName;
        private string lastName;
        private string gender;
        private long phoneNumber;
        private string moduleType;
        private int age;
        private string creditCardNumber;
        private CustomerFunction myDelegate;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Gender { get => gender; set => gender = value; }
        public long PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string ModuleType { get => moduleType; set => moduleType = value; }
        public int Age { get => age; set => age = value; }
        public string CreditCardNumber { get => creditCardNumber; set => creditCardNumber = value; }
        [XmlIgnore]
        public CustomerFunction MyDelegate { get => myDelegate; set => myDelegate = value; }
        public string ViewCreditCardNumber { get => ConcealedCreditCardNumber(); }

        public Customer()
        {
            myDelegate = Status;
            myDelegate += AdditionalQuestion;
        }
        public void Status()
        {
            Console.WriteLine(" Mock test booked");
        }

        public abstract void AdditionalQuestion();
        private string ConcealedCreditCardNumber()
        {
            char[] passArray = CreditCardNumber.ToCharArray();
            for (int i = 4; i < 12; i++)
            {
                passArray[i] = 'X';
            }
            string concealedCreditCardNumber = string.Empty;
            for (int i = 0; i < passArray.Length; i++)
            {
                if (i % 4 == 0)
                {
                    concealedCreditCardNumber = concealedCreditCardNumber + " " + passArray[i];
                }
                else
                {
                    concealedCreditCardNumber = concealedCreditCardNumber + passArray[i];
                }

            }
            return concealedCreditCardNumber;
        }

    }
}
