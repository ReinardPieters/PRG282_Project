﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282Project.BusinessProcess
{
    internal class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }

        public override string ToString()
        {
            return $"{StudentID},{Name},{Age},{Course}";
        }
    }

}
