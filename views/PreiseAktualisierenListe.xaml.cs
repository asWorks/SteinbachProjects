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
using ProjektDB.ObjectCollections;
using DAL;
using System.Collections.ObjectModel;
//using Seagull.BarTender.Print;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for PrintArtikelLabel.xaml
    /// </summary>
    public partial class PreiseAktualisierenListe : System.Windows.Window
    {

        SteinbachEntities db;
        CollectionViewSource ViewSource;
        CollectionViewSource Kategorien;
        CollectionViewSource Lieferanten;
        CollectionViewSource WKZ;
        CollectionViewSource Mitarbeiter;
        private BindingListCollectionView View;

        public PreiseAktualisierenListe()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitWindow();
        }

        private void InitWindow()
        {
            db = new SteinbachEntities();
            db.SavingChanges += new EventHandler(db_SavingChanges);
            ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ThisViewSource")));
            Kategorien = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Kategorien")));
            Lieferanten = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Lieferanten")));
            WKZ = ((System.Windows.Data.CollectionViewSource)(this.FindResource("WKZ")));
            Mitarbeiter = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Mitarbeiter")));

            View = (BindingListCollectionView)ViewSource.View;
            ViewSource.Source = new ObservableCollection<lagerliste>(db.lagerlisten);
            Kategorien.Source = CommonTools.Tools.HelperTools.GetAuswahlEintraege("ArtikelKategorie", 1);
            Lieferanten.Source = db.firmen.Where(f => f.istFirma == 1);
            WKZ.Source = db.StammWaehrungen;
            Mitarbeiter.Source = db.personen;
        }

        void db_SavingChanges(object sender, EventArgs e)
        {
            var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);



            foreach (var o in om)
            {

                if (o.Entity is lagerliste)
                {

                    var p = (lagerliste)o.Entity;
                    if (p.EntityState != System.Data.EntityState.Deleted)
                    {
                        p.PreisAenderungMitarbeiter = Session.User.id;
                        p.preisvom = DateTime.Now;
                    }
                }


            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<lagerliste> liste = c1DataGrid1.ItemsSource.OfType<lagerliste>().Where(p => p.PrintSelected == 1);
            if (liste != null)
            {
                PrintLabelArtikel(liste);
            }



            // Console.WriteLine(liste.Count());
        }

        private void PrintLabelArtikel(IEnumerable<lagerliste> list)
        {

            //try
            //{
            //    using (Engine btEngine = new Engine())
            //    {
            //        btEngine.Start();

            //        string LabelPath = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("LabelPathArtikelBasis", @"C:\Users\as\Documents\BarTender\BarTender Documents\SIArtikelBasis.btw");
            //        LabelFormatDocument btFormat = btEngine.Documents.Open(LabelPath);

            //        foreach (var item in list)
            //        {

            //            btFormat.SubStrings["Barcode"].Value = item.artikelnr;
            //            btFormat.SubStrings["Bezeichnung"].Value = item.bezeichnung;

            //            btFormat.PrintSetup.IdenticalCopiesOfLabel = item.PrintCopies.HasValue ? (int)item.PrintCopies : 1; // Wenn null dann 1 Kopie 
            //            Result result = btFormat.Print();

            //            //if (result == Result.Success)
            //            //{
            //            //    tblResult.Text = "Success";
            //            //}
            //            //else
            //            //{
            //            //    tblResult.Text = result == Result.Failure ? "Failure" : "Timeout";
            //            //}

            //        }

            //        btFormat.Close(SaveOptions.DoNotSaveChanges);

            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }


        public bool CheckForChanges()
        {
            var osm = db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);

            Console.WriteLine(osm.Count().ToString());

            foreach (var ose in osm)
            {
                //var res = ose.GetModifiedProperties();
                //foreach (var item in res)
                //{
                //    Console.WriteLine(item);
                //}

                if (ose.State != System.Data.EntityState.Unchanged)
                {
                    return true;
                }
            }
            return false;


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CheckForChanges())
            {
                MessageBoxResult res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel);
                switch (res)
                {
                    case MessageBoxResult.Cancel:
                        {
                            e.Cancel = true;
                            break;
                        }
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Yes:
                        {
                            db.SaveChanges();
                            break;
                        }

                    default:
                        break;
                }
            }
        }



    }
}
