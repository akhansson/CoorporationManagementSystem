﻿using CooperationApp.Coorperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CooperationApp.People
{
    public class Person
    {
        public string FullName { get; private set; }

        public Person(string fullName)
        {
            FullName = fullName;
        }
    }
}
