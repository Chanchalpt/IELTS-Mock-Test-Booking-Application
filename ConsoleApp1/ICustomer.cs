using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface ICustomer
    {
        string FirstName { get;set; }
        string LastName { get; set; }
        string Gender { get; set; }
        long PhoneNumber { get; set; }
        string ModuleType { get; set; }
        int Age { get; set; }
        string ViewCreditCardNumber { get; }
        CustomerFunction MyDelegate { get; set; }

        void Status();
        void AdditionalQuestion();
    }
}
