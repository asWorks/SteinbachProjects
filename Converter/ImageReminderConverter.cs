using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Drawing;
using DAL;
using System.Windows.Media.Imaging;


namespace ProjektDB
{
    [ValueConversion(typeof(int), typeof(Image))]
    class ImageReminderConverter : IValueConverter
    {
        string datei = "/Images/gatuus_Nice_Light_Bulb_32.png";
        BitmapImage bmp;

        public ImageReminderConverter()
        {
            bmp = new BitmapImage(new Uri(datei, UriKind.Relative));

        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            using (var db = new SteinbachEntities())
            {

                if (value == null || (int)value == 0)
                {
                    return null;
                }

                int v = (int)value;



                var query = from p in db.projekt_verlaufe
                            where p.id_projekt == v && p.marker == 1
                            select p;

                if (query.Any())
                {
                    return bmp;
                }


                //var q = from m in query
                //        where m.marker == 1
                //        select m;




                //foreach (var res in query)
                //{
                //    if (res.marker.HasValue)
                //    {


                //        if ((int)res.marker == 1)
                //        {
                //            // string datei = "/Images/gatuus_Nice_Light_Bulb_32.png";
                //            // var bmp = new BitmapImage(new Uri(datei, UriKind.Relative));
                //            return bmp;

                //        }
                //    }
                //}

                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
