using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB.DTO
{
    public class dtoBelegePos
    {
        public int idx { get; set; }
        public string Artikelnummer { get; set; }
        public string  Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public decimal Menge { get; set; }
        public decimal Preis { get; set; }
        public decimal Gesamtpreis { get; set; }
    }
}
