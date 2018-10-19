using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;


namespace ProjektDB
{
    public class CalcData
    {

        public CalcData(int id)
        {
            Positionen = new List<SummePos>();
           

            using (var db = new SteinbachEntities())
            {
               double xt = 0;
                var query = (from c in db.kalkulationstabelle_details
                            where c.id_kalkulationstabelle == id
                            select c.gesamtangebot).Sum();

                double x =  query != null ? (double)query : 0;

                var Transport = from t in db.kalkulationstabellen
                                where t.id == id
                                select t.transportverpackung;
               
               
                xt = Transport.FirstOrDefault().HasValue ? (double)Transport.FirstOrDefault() : 0;


                var s = new SummePos { Bezeichnung = "Summe", SummePositionen = x + (double)xt };

                Positionen.Add(s);

            }


        }
        public List<SummePos> Positionen { get; set; }


    
    }

    public class SummePos
    {
        public double SummePositionen { get; set; }
        public string Bezeichnung { get; set; }


    }

 


}
