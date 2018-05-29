using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class Meeting
    {
        public int MeetingID { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string MeetingHolder { get; set; }
        public string Note { get; set; }

        public override string ToString()
        {
            return "Möte nr." + MeetingID.ToString() + " med " + MeetingHolder;
        }
    }
}