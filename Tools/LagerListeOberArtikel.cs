using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB.Tools
{
    public class LagerListeOberArtikel 
    {
        public string bezeichnung { get; set; }
        public string artikelnr { get; set; }
        public string beschreibung { get; set; }
        public string oberartikel { get; set; }
        public int id { get; set; }
        public int id_parent { get; set; }
    }
}
