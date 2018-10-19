using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;


namespace ProjektDB
{
[ValueConversion(typeof(short),typeof(string ))]
    class BoolGenderConverter:IValueConverter
    {



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value== null)
            {
                return null;
            }

            short v = (short)value;
            switch (v)
            {
                case 1:
                    {
                        return "Herr";
                       
                    }
                case 0:
                    {
                        return "Frau";

                    }

                default:
                    return "Nicht definiertes Geschlecht";
                    
            }
           


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value == null)
            {
                return null;
            }

            string v = (string)value;
            switch (v)
            {
                case "Herr":
                    {
                        return 1;

                    }
                case "Frau":
                    {
                        return 0;

                    }

                default:
                    return -1;

            }
        }

     
    }
}
