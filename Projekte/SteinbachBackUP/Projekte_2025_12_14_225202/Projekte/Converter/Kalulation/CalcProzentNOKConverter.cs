using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;
using DAL.Tools;
using ProjektDB.Tools;


namespace ProjektDB
{
    [ValueConversion(typeof(int), typeof(double))]
    class CalcProzentNOKConverter : IValueConverter
    {





        private double GetProzent(int id)
        {
            using (var db = new SteinbachEntities())
            {
                var query = from p in db.kalkulationstabellen where p.id == id select p.euroumrechnung;
                return (double)query.FirstOrDefault();

            }
        }




        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            using (var db = new SteinbachEntities())
            {

                if (value == null || (int)value == 0)
                {
                    return 0;
                }

                int v = (int)value;
                var query = from p in db.kalkulationstabelle_details
                            where p.id == v
                            select p;

                try
                {
                    var q = query.FirstOrDefault();
               
                    if (q.epnok.HasValue)
                    {
                        if (q.epnok != 0)
                        {
                            return (double)q.epnok * GetProzent((int)q.id_kalkulationstabelle);
                        }
                    }

                    if (q.umrechnungeuro.HasValue)
                    {
                        return q.umrechnungeuro;
                    }


                }

                catch (TimeoutException)
                { }
                catch (Exception ex)
                {

                    LoggingTool.LogExeption(ex, "CalcProzentNOKConverter", "Conver");
                }

                return 0;





            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
