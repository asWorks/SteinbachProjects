using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB.DTO
{
    public class dtoBelege_DataSource
    {
        int idx = 0;
        public List<dtoBelege> ListBelege { get; set; }
        public List<dtoBelegePos> ListPositionen { get; set; }

        public dtoBelege_DataSource()
        {
            ListBelege = new List<dtoBelege>();
            ListPositionen = new List<dtoBelegePos>();
        }

        public dtoBelege_DataSource(dtoBelege Beleg)
        {
            ListBelege = new List<dtoBelege>();
            ListBelege.Add(Beleg);
            ListPositionen = new List<dtoBelegePos>();
        }

        public void AddPos(string ArtikelNr,string Bezeichnung,decimal Menge,decimal Preis,int pos = 0)
        {
            var BelPos = new DTO.dtoBelegePos();
            BelPos.Artikelnummer = ArtikelNr;
            BelPos.Bezeichnung = Bezeichnung;
            BelPos.Menge = Menge;
            BelPos.Preis = Preis;
            BelPos.Gesamtpreis = BelPos.Menge * BelPos.Preis;
            if (pos == 0)
            {
                BelPos.idx = ++idx;
            }
            else
            {
                BelPos.idx = pos;
            }

            ListPositionen.Add(BelPos);

        }
    
    }
}
