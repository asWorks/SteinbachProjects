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
    class LieferzeitPlantConverter : IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
                    
            using (var db = new SteinbachEntities())
            {

                if (value == null || (int)value == 0)
                {
                    return "-";
                }

                
                    // from t1 in db.Table1 
                    //join t2 in db.Table2 on t1.field equals t2.field 
                    //select new { t1.field2, t2.field3} 


                int v = (int)value;
                var query = from p in db.projektAggregate
                            join p2 in db.Stamm_Aggregate on p.id_aggregat equals p2.id
                            where p.id_projekt == v
                            select new { p2.aggregat, p.Kennzeichen };
               
                System.Text.StringBuilder sb = new StringBuilder();
               
                foreach (var s in query)
                {
                    if (s.aggregat  == null || s.aggregat == string.Empty)
                        sb.AppendLine("-");
                    else
                        sb.AppendLine(String.Format("{0} {1}", s.aggregat, s.Kennzeichen));
                
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
