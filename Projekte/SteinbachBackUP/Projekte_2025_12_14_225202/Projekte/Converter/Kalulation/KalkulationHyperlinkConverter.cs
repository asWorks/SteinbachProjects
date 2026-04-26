using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections;
using DAL;
using System.Windows.Documents;


namespace ProjektDB
{
    [ValueConversion(typeof(String), typeof(C1.WPF.C1HyperlinkButton))]
    class KalkulationHyperlinkConverter : IValueConverter
    {

    
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            try
            {

               // Paragraph parx = new Paragraph();
               // Run run1 = new Run("Text preceeding the hyperlink.");
               // //Run run2 = new Run("Text following the hyperlink.");
               // Run run3 = new Run(value.ToString());

               // Hyperlink hyperl = new Hyperlink(run3);
               // hyperl.NavigateUri = new Uri( "MainWindow.xaml",UriKind.Relative);

               // parx.Inlines.Add(run1);
               // parx.Inlines.Add(hyperl);
               //// parx.Inlines.Add(run2);

                var b = new C1.WPF.C1HyperlinkButton();
              //  b.NavigateUri = new Uri(@"ProjektDB/MainWindow.xaml", UriKind.Relative);
                b.TargetName = "MainWindow";
                b.Content = value.ToString();

                
                return b;
            }
            catch (Exception ex)
            {

               // System.Windows.MessageBox.Show(ex.Message);
                return value;
            }

        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {



            return null;




        }


    }
}
