using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;


namespace ProjektDB
{
[ValueConversion(typeof(int),typeof(int ))]
    public class LagerlisteGesamtbestandConverter:IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
                    
            using (var db = new SteinbachEntities())
            {

                if (value == null || (int)value == 0)
                {
                    return 0;
                }

                


                int v = (int)value;
                if (v == 35)
                {
                    Console.WriteLine("");
                }
                var query = from p in db.Lagerbestaende where p.id_Lagerliste == v select p ;
                int? bestand = 0;
                foreach (var item in query)
                {
                    bestand += item.Lagerbestand;
                }

                if (bestand.HasValue)
                {
                    return (int)bestand;
                }
                else
                {
                    return 0;
                }
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

     
    }
}
