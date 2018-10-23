using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;


namespace ProjektDB
{
    [ValueConversion(typeof(int), typeof(string))]
    class SchiffAnlagenListeConverter : IValueConverter
    {

        System.Text.StringBuilder sb;


        public SchiffAnlagenListeConverter()
        {
            sb = new StringBuilder();
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            using (var db = new SteinbachEntities())
            {

                if (value == null || (int)value == 0)
                {
                    return "-";
                }

                sb.Clear();
                int c = 0;

                int v = (int)value;
                var query = from s in db.SchiffAnlage
                            where s.id_Schiff == v
                            orderby s.id
                            select s;


                foreach (var ship in query)
                {
                    if (ship.Kennzeichen != null && ship.Kennzeichen != string.Empty)
                    {
                        c++;
                         if (c > 4)
                        {
                            sb.AppendLine();
                            c = 0;
                        }

                        sb.Append(ship.Kennzeichen + " / ");
                       
                       
                    }
                }


                string res = sb.ToString();
                int f = res.LastIndexOf(" / ");
                if (f > 0)
                    res = res.Remove(f);

                return res;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }


    }
}
