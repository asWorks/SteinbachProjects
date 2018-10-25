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

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for SelectProjekt.xaml
    /// </summary>
    public partial class SelectProjekt : Window
    {

        public event Action<int> PushProjektID;
        CollectionViewSource KalkulationenViewSource;

        public SelectProjekt()
        {
            InitializeComponent();
        }

        public SelectProjekt(string Projektnummer)
        {
            InitializeComponent();
            try
            {
                KalkulationenViewSource = (CollectionViewSource)this.FindResource("KalkulationenViewSource");
                using (var db = new DAL.SteinbachEntities())
                {
                    KalkulationenViewSource.Source = db.projekte.Where(n => n.projektnummer == Projektnummer);
                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var res = cboProjekte.SelectedValue;
            onPushProjektId((int)res);
        }

        private void onPushProjektId(int pid)
        {
            if (PushProjektID != null)
            {
               PushProjektID(pid);
            }
        }
    }
}
