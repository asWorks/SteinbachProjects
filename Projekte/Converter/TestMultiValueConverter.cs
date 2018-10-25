using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ProjektDB
{
    
    public class TestMultiValueConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            try
            {
                var v1 = values[0].ToString();
                var v2 = values[1].ToString();
                var i2 = int.Parse(v2);

                return v1 == "0" ? (i2 * -1).ToString() : v2;
            }
            catch (Exception)
            {

                return "Fehler in MultiBindConverter";
            }
             

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
