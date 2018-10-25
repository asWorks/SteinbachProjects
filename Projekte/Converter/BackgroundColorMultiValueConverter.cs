using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace ProjektDB
{

    public class BackgroundColorMultiValueConverter : IMultiValueConverter
    {
        /// <summary>
        /// Wird an Background gebunden 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == null || values[1] == null || values[0] == DependencyProperty.UnsetValue)
            {
                return null;
            }

            try
            {
                var v1 = values[0].ToString();
                var v2 = values[1].ToString();
                var i1 = int.Parse(v1);
                var i2 = int.Parse(v2);
                if (i1 < i2)
                {
                    return System.Windows.Media.Brushes.Red;
                }
                else if ((i1 == i2))
                {
                    return System.Windows.Media.Brushes.Yellow;
                }
                else
                {
                    return null;
                }




            }
            catch (Exception ex)
            {

                return "Fehler in MultiBindConverter - " + ex.Message;
            }


        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
