﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    class Employee
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string surName { get; set; }
        public string phoneNumber { get; set; }

        public override string ToString()
        {
            return id + " " + firstName + " " + surName;
        }
    }
}
