using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB.Models
{
    public class LagerListeTemplateSource
    {

        public int id { get; set; }
        public string artikelnr { get; set; }
        public string bezeichnung { get; set; }
        public int? anzahlauflager { get; set; }
    }
}
