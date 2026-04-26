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
    /// Interaction logic for TestUIWindow.xaml
    /// </summary>
    public partial class TestUIWindow : Window
    {


        private SteinbachEntities db;
        private ListCollectionView ProjektView;
        private CollectionViewSource ProjektViewSource;
        private int ProjektID;
        public TestUIWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProjektViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Projekt_ViewSource")));
            db = new SteinbachEntities();
            ProjektID = 10846;

            //var result = from p in db.projekte          //.Include("projekt_verlauf")
            //             where p.id == ProjektID
            //             select p;

            var ProjektRepo = new Repositories.ProjektRepository(db);
            var ProjektQuery = ProjektRepo.GetProjekteByID(ProjektID);
            ProjektViewSource.Source = ProjektQuery;

            //ProjektViewSource.Source = result;


        }
    }
}
