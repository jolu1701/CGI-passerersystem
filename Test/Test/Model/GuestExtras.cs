using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    class GuestExtras
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string surName { get; set; }
        public string company { get; set; }

        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public string badge { get; set; }

        public override string ToString()
        {
            return firstName + " " + surName + " " + company + " ci:" + checkIn.ToString() + " co:" + checkOut.ToString() + " B:" + badge;
        }
    }
}
