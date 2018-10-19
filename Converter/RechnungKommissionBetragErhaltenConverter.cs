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
    class RechnungKommissionBetragErhaltenConverter : IValueConverter
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
                var query = from p in db.projekt_rechnungen
                            where p.id_projekt == v
                            orderby p.idx
                            select p;
                System.Text.StringBuilder sb = new StringBuilder();

                foreach (var s in query)
                {
                    if (s.kommissionbetragerhalten == null)
                        sb.AppendLine("-");
                    else
                    {
                        string buf = string.Empty;
                        if (s.kommissionwaehrung != null)
                        {
                            buf = String.Format("{0} {1}", s.kommissionbetragerhalten.ToString(), s.kommissionwaehrung);
                        }
                        else
                        {
                            buf = s.kommissionbetragerhalten.ToString();
                        }
                        sb.AppendLine(buf);
                    }
                    }


                


                string res = sb.ToString();
                int f = res.LastIndexOf(Environment.NewLine);
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
