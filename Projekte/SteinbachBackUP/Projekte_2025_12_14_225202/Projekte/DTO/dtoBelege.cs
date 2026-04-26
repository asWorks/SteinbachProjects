using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB.DTO
{
    public class dtoBelege
    {

        public string Kundenanschrift { get; set; }
        public string Absenderdaten { get; set; }
        public string Datum { get; set; }

        public dtoBelege()
        {
            Kundenanschrift = string.Empty;
            Absenderdaten = string.Empty;
            Datum = string.Empty;
        }

        public dtoBelege(string CustomerAdress,String SenderData,string SendDate)
        {
            Kundenanschrift = CustomerAdress;
            Absenderdaten = SenderData;
            Datum = SendDate;
        }


    }
}
