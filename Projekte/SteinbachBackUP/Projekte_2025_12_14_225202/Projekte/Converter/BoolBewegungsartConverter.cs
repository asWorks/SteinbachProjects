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
    class BoolBewegungsartConverter : IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            int v = (int)value;
            switch (v)
            {
                case 1:
                    {
                        return "Zugang";

                    }
                case 0:
                    {
                        return "Abgang";

                    }

                default:
                    return "Bitte wählen";

            }



        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            

            if (value == null)
            {
                return null;
            }
            string v = string.Empty;

            if (value.GetType() == typeof(C1.WPF.C1ComboBoxItem))
            {
                var x = (C1.WPF.C1ComboBoxItem)value;
                v = x.Content.ToString();

            }
            else
                v = (string)value;

             
            switch (v)
            {
                case "Zugang":
                    {
                        return 1;

                    }
                case "Abgang":
                    {
                        return 0;

                    }

                default:
                    return -1;

            }
        }


    }
}
