﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;
using System.Drawing;


namespace ProjektDB
{
[ValueConversion(typeof(int),typeof(string ))]
    public class BackgroundConverter: IValueConverter
    {
     


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "Yellow";
        }

        

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

     
    }
}
