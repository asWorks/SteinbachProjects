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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjektDB.Repositories;
using ProjektDB.ObjectCollections;
using ProjektDB.Data;
using DAL;
using ProjektDB.Tools;
using CommonTools.Views;


namespace ProjektDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindowCopy : Page
    {
        private int CurrentProjektID;
        private SteinbachEntities db;
        
        //private CollectionViewSource ProjektViewSource;
        private CollectionViewSource VerlaufViewSource;
        private ListCollectionView ProjektView;
        private CollectionViewSource ProjektViewSource;
        private BindingListCollectionView VerlaufView;
        private ProjektRepository ProjektRepo;
        private VerlaufRepository VerlaufRepo;
        private CollectionViewSource AnlagenTypViewSource;
        private BindingListCollectionView AnlagenTypView;

     
        private GetData gt;
     

        public MainWindowCopy()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var login = new Logon();
            login.ShowDialog();

            db = new SteinbachEntities(); 
            ProjektRepo = new ProjektRepository(db);
            VerlaufRepo = new VerlaufRepository(db);
           
            
            //CurrentProjektID = 8194;
            CurrentProjektID = ProjektRepo.GetProjektIDFromNumber("110278");

            gt = new GetData();


        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
           // db = new SteinbachEntities();
            ProjektViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Projekt_ViewSource")));
            var ProjektQuery = ProjektRepo.GetProjekteByID(CurrentProjektID);
             ProjektViewSource.Source = ProjektQuery;
             ProjektView = (ListCollectionView)ProjektViewSource.View;
             projekt p = (projekt)ProjektView.CurrentItem;
             var PersonLookup = ((System.Windows.Data.CollectionViewSource)(this.FindResource("PersonLookUp")));
             var query = from pers in db.personen
                         select pers;
             PersonLookup.Source = query;

                
            VerlaufViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ProjektVerlauf_ViewSource")));
            VerlaufView = (BindingListCollectionView)(VerlaufViewSource.View);

            AnlagenTypViewSource= ((System.Windows.Data.CollectionViewSource)(this.FindResource("AnlagenTyp_ViewSource")));
            AnlagenTypView = (BindingListCollectionView)(AnlagenTypViewSource.View);
          
           

            ProjektView.CurrentChanged += new EventHandler(ProjektView_CurrentChanged);
        }

        void ProjektView_CurrentChanged(object sender, EventArgs e)
        {
            VerlaufView = (BindingListCollectionView)(VerlaufViewSource.View);
            AnlagenTypView = (BindingListCollectionView)(AnlagenTypViewSource.View);

            ////projekt p = (projekt)ProjektView.CurrentItem;
            ////if (p != null && !p.projekt_verlauf.IsLoaded)
            ////   p.projekt_verlauf.Load();
          
        }

        private void VerlaufItem_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.projekt_verlaufListView.SelectedItem = item.DataContext;
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Neu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ende_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addProjekt_Click(object sender, RoutedEventArgs e)
        {
            var proj = (projekt)(this.ProjektView .AddNew());
            this.ProjektView.CommitNew();
        }

        private void addVerlauf_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerlaufView != null)
            {
                var verl = (projekt_verlauf)(this.VerlaufView.AddNew());
                verl.id_personchange = Session.User.id; 
                this.VerlaufView.CommitNew();
            }
        }
        
     
        private void cmdDeleteVerlauf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.VerlaufView.CurrentPosition > -1)
                {
                    // this.VerlaufView.RemoveAt(this.VerlaufView.CurrentPosition);
                    this.VerlaufView.Remove(VerlaufView.CurrentItem);

                }


               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

                     
        }

        private void testIch(object sender, RoutedEventArgs e)
        {
           
        }

        //private void DeleteVerlauf_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {

        //        projekt_verlauf pc = (projekt_verlauf)this.VerlaufView.CurrentItem;
        //        MessageBox.Show(pc.bemerkung);

        //        //if (this.VerlaufView.CurrentPosition > -1)
        //        //{
        //        //    this.VerlaufView.RemoveAt(this.VerlaufView.CurrentPosition);
        //        //   // this.VerlaufView.Remove(VerlaufView.CurrentItem);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}



    }
}
