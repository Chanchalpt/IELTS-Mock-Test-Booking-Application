using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    [XmlInclude(typeof(General)), XmlInclude(typeof(Academic))]
    public class Appointment : IComparable<Appointment>
    {
        private string apptTime;
        private Customer myData;
        private int slotNumber;

        public string ApptTime { get => apptTime; set => apptTime = value; }
        public Customer MyData { get => myData; set => myData = value; }
        public int SlotNumber { get => slotNumber; set => slotNumber = value; }

        public int CompareTo(Appointment appt)
        {
            return MyData.Age.CompareTo(appt.myData.Age);
        }
        public override string ToString()
        {
            return string.Format(" \nBooked an appointment at {0} {1}", ApptTime, myData);
        }
    }
}
