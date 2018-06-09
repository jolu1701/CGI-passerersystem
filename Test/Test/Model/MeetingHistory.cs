using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    class MeetingHistory
    {
        public string Meetingholder { get; set; }
        public string MeetingHolderID { get; set; }
        public string MhDepartment { get; set; }
        public string MhGuest { get; set; }
        public string MhGuestCo { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
         
    
    }
}
