﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB.Models
{
    public class FirmenSource
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? Zahlungsbedingungen { get; set; }
    }
}
