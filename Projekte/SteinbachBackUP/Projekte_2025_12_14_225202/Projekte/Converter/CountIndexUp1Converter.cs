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
    class CountIndexUp1Converter:IValueConverter
    {
       private int counter = 0;


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
                counter++; 
                return counter.ToString();
        }

        

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

     
    }
}
