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

namespace ProjektDB.Temp
{
    /// <summary>
    /// Interaction logic for TestFtb.xaml
    /// </summary>
    public partial class TestFtb : Window
    {
        public TestFtb()
        {
            InitializeComponent();

            IEnumerable<double> source = new double[] {1,2,2.5,2.75,3,3.25,3.5,4 };
            this.Testftb.cBox.ItemsSource = source;


        }



    }
}
