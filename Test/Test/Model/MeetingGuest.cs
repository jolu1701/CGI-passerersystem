using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    class MeetingGuest
    {
        public int Guestid { get; set; }
        public int Meetingid { get; set; }
        public string Badge { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
    }
}
