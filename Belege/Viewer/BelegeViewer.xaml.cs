using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjektDB.Belege.Viewer
{
    public enum EnumReportType
    {
        Brunvoll,
        Steinbach
        
    }

    /// <summary>
    /// Interaction logic for BelegeViewer.xaml
    /// </summary>
    public partial class BelegeViewer : Window
    {
        public BelegeViewer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenSectionReport(EnumReportType type)
        {

            switch (type)
            {
                case EnumReportType.Brunvoll:
                    {
                      
                        break;
                    }
                    
                case EnumReportType.Steinbach:
                    break;
                default:
                    break;
            }
        }
    }
}
