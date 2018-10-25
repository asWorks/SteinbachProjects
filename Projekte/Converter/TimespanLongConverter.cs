using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;
using System.Drawing;


namespace ProjektDB
{
[ValueConversion(typeof(TimeSpan),typeof(long ))]
    public class TimespanLongConverter: IValueConverter
    {
     


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return TimeSpan.FromSeconds(0); ;
            }
            return TimeSpan.FromSeconds((long)value);
            
          
        }

        

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value== null)
            {
                return null;
            }

            TimeSpan v = TimeSpan.Parse( value.ToString());
            return v.TotalSeconds  ;

        }

     
    }
}
