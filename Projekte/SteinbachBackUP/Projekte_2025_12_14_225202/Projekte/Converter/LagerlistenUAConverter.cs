using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;


namespace ProjektDB
{
[ValueConversion(typeof(int),typeof(string ))]
    class LagerlistenUAConverter : IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
                    
            using (var db = new SteinbachEntities())
            {

                if (value == null || (int)value == 0)
                {
                    return "-";
                }

                


                int v = (int)value;
                var query = from p in db.lagerlisten where p.id == v select p;
                
                var ll =  query.FirstOrDefault();
                return String.Format("{0} : {1} : {2}", ll.bezeichnung, ll.artikelnr, ll.beschreibung);

            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

     
    }
}
