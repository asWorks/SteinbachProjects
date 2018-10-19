using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;


namespace ProjektDB
{
    [ValueConversion(typeof(double), typeof(string))]
    class Comma2PointConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            try
            {
                string buf = value.ToString();
                buf = buf.Replace(",", ".");
                return buf;
            }
            catch (Exception)
            {

                return null;
            }
            
        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            try
            {
                string buf = value.ToString();
                buf = buf.Replace(".", ",");
                return double.Parse(buf);
            }
            catch (Exception)
            {

                return null;
            }
            



        }


    }
}
