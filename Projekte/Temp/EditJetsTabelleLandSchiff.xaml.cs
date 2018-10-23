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
using DAL;

namespace ProjektDB.Temp
{
    /// <summary>
    /// Interaction logic for EditKundentabelle1.xaml
    /// </summary>
    public partial class EditJetsTabelleLandSchiff : Window
    {
        SteinbachEntities db;
        private ListCollectionView EditkundenView;
        private CollectionViewSource EditkundenViewSource;
        int toSkip;
        public EditJetsTabelleLandSchiff()
        {
            InitializeComponent();
            db = new SteinbachEntities();
            FillList();
            toSkip = 0;
        }

        private void FillList()
        {

            db.SaveChanges();
            db = new SteinbachEntities();
            var kto = from k in db.projekte
                      where k.firmenname == "Jets AS" && (k.landx == 0 || k.landx == null) && (k.schiff == 0 || k.schiff == null)
                      orderby k.datum
                      select k;

            EditkundenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("EditKunden_ViewSource")));
            EditkundenViewSource.Source = new ObjectCollections.ProjektCollection(kto.Skip(toSkip), db);
            EditkundenView = (ListCollectionView)EditkundenViewSource.View;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            db.SaveChanges();

        }

     


 



    }


}
