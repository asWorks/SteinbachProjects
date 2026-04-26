using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;


namespace ProjektDB
{
    [ValueConversion(typeof(string), typeof(string))]
    class WarenartConverter : IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "Bitte wählen";
            }

            string v = value.ToString();
            return v;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            

            if (value == null)
            {
                return "Bitte wählen";
            }
            string v = string.Empty;

            if (value.GetType() == typeof(C1.WPF.C1ComboBoxItem))
            {
                var x = (C1.WPF.C1ComboBoxItem)value;
                v = x.Content.ToString();

            }
            else
                v = (string)value;

            return v;
   
          
        }


    }
}
