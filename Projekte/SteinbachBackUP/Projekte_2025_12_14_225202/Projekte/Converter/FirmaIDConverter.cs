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
    class FirmaIDConverter:IValueConverter
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
                var query = from p in db.firmen where p.id == v select p.name;
                return query.FirstOrDefault();
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

     
    }
}
