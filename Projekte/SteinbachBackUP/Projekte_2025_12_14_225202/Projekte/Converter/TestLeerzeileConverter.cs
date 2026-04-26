using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;


namespace ProjektDB
{
[ValueConversion(typeof(string),typeof(string ))]
    class TestLeerzeileConverter:IValueConverter
    {
    string buffer = string.Empty;


    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {

        if (value== null)
        {
            return null;
        }
        string res = string.Empty;

        if (value.ToString().Equals( buffer))
        {
            res = "-";
        }
        else
        {
            res = value.ToString();
        }

        buffer = value.ToString();
        return res;

    }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }

     
    }
}
