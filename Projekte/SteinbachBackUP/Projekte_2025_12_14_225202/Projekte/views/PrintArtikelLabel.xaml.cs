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
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using CommonTools.Tools;
using ProjektDB.Tools;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for PrintArtikelLabel.xaml
    /// </summary>
    public partial class PrintArtikelLabel : System.Windows.Window
    {

        SteinbachEntities db;
        CollectionViewSource ViewSource;
        CollectionViewSource Kategorien;
        CollectionViewSource Lieferanten;
        private BindingListCollectionView View;
        private int ArtikelID = 0;

        public PrintArtikelLabel()
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
            ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ThisViewSource")));
            Kategorien = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Kategorien")));
            Lieferanten = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Lieferanten")));

            View = (BindingListCollectionView)ViewSource.View;
            ViewSource.Source = new ObservableCollection<lagerliste>(db.lagerlisten);
            Kategorien.Source = CommonTools.Tools.HelperTools.GetAuswahlEintraege("ArtikelKategorie",1);
            Lieferanten.Source = db.firmen.Where(f => f.istFirma == 1);

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<lagerliste> liste = c1DataGrid1.ItemsSource.OfType<lagerliste>().Where(p => p.PrintSelected == 1);
            if (liste != null)
            {
                //PrintLabelArtikel(liste);
                PrintActiveRecordsLabel(liste);
            }



            // Console.WriteLine(liste.Count());
        }

        private void PrintActiveRecordsLabel(IEnumerable<lagerliste> list)
        {
          // var pageDocument = ActiveReportsTools<int>.GetDocument(StaticMethods.GetPath(@"Etiketten\ArtikelEtikett.rdlx"));
          // pageDocument.PageReport.Report.DataSources[0].DataSourceReference = StaticMethods.GetPath("SteinbachSharedDataSource.rdsx");
         
           foreach (var item in list)
           {
               var pageDocument = ActiveReportsTools<int>.GetDocument(StaticMethods.GetPath(@"Etiketten\ArtikelEtikett.rdlx"));
               ArtikelID = item.id;
               //ActiveReportsTools<int>.SetParameter("LagerlisteID", item.id, ref pageDocument);
               pageDocument.LocateDataSource += new LocateDataSourceEventHandler(pageDocument_LocateDataSource);
               int PrintCopies = item.PrintCopies.HasValue ? (int)item.PrintCopies : 1;
               for (int i = 0; i < PrintCopies; i++)
               {
                   pageDocument.Print(false, false, false);
               }
           }

           
            
        }

        void pageDocument_LocateDataSource(object sender, LocateDataSourceEventArgs args)
        {
          

            var query = from l in db.lagerlisten
                        join f in db.firmen on l.id_lieferant equals f.id
                        where l.id == ArtikelID
                        select new
                        {
                           id =  l.id,
                           bezeichnung = l.bezeichnung,
                           artikelnr = l.artikelnr,
                           beschreibungeng = l.beschreibungeng,
                          beschreibung = l.beschreibung,
                          ortregal= l.ortregal,
                          ortbox= l.ortbox,
                          name = f.name
                        };

            args.Data = query.ToList();

        }


        private void PrintLabelArtikel(IEnumerable<lagerliste> list)
        {

            //try
            //{
            //    using (Engine btEngine = new Engine())
            //    {
            //        //btEngine.Start();

            //        //string LabelPath = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("LabelPathArtikelBasis", @"C:\Users\as\Documents\BarTender\BarTender Documents\SIArtikelBasis.btw");
            //        //LabelFormatDocument btFormat = btEngine.Documents.Open(LabelPath);

            //        //foreach (var item in list)
            //        //{

            //        //    btFormat.SubStrings["Barcode"].Value = item.artikelnr;
            //        //    btFormat.SubStrings["Bezeichnung"].Value = item.bezeichnung;
                        
            //        //    btFormat.PrintSetup.IdenticalCopiesOfLabel = item.PrintCopies.HasValue ? (int)item.PrintCopies : 1; // Wenn null dann 1 Kopie 
            //        //    Result result = btFormat.Print();

            //            //if (result == Result.Success)
            //            //{
            //            //    tblResult.Text = "Success";
            //            //}
            //            //else
            //            //{
            //            //    tblResult.Text = result == Result.Failure ? "Failure" : "Timeout";
            //            //}

            //        }

            //        //btFormat.Close(SaveOptions.DoNotSaveChanges);

            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            
        }



    }
}
