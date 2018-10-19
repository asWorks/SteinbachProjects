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
using System.Collections.ObjectModel;
using CommonTools.Tools;
using ProjektDB.Tools;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for ViewerInventurliste.xaml
    /// </summary>
    public partial class ViewerInventurliste : Window
    {
        public enum InventurlistenArt
        {
            Zaehliste,
            Differenzliste,
            Artikelliste
        }

        ObservableCollection<SI_InventurenPositionen> InvPositionen;
        IEnumerable<SI_InventurenPositionen> InvPositionenEnum;
        IEnumerable<lagerliste> lagerliste;
        int InvID = 0;
        DateTime? InvDatum;
        InventurlistenArt ListenArt;
        bool DoExportExcel;


        public ViewerInventurliste()
        {
            InitializeComponent();
        }

        public ViewerInventurliste(ObservableCollection<SI_InventurenPositionen> InvPos, DateTime? Datum)
        {
            //InvPositionen = InvPos;
            //viewer1 = new GrapeCity.ActiveReports.Viewer.Wpf.Viewer();
            //InvDatum = Datum;
            //InitializeComponent();
            //PrintActiveRecordsLabel();

        }

        public ViewerInventurliste(IEnumerable<SI_InventurenPositionen> InvPosEnum, DateTime? Datum, InventurlistenArt ListenArt)
        {
            InvPositionenEnum = InvPosEnum;
            viewer1 = new GrapeCity.ActiveReports.Viewer.Wpf.Viewer();
            InvDatum = Datum;
            InitializeComponent();
            PrintActiveRecordsLabel(ListenArt);

        }

        public ViewerInventurliste(IEnumerable<lagerliste> liste, DateTime? Datum, InventurlistenArt ListenArt, bool DoExport)
        {
            //InvPositionenEnum = InvPosEnum;
            lagerliste = liste;
            viewer1 = new GrapeCity.ActiveReports.Viewer.Wpf.Viewer();
            InvDatum = Datum;
            this.ListenArt = ListenArt;
            DoExportExcel = DoExport;
            InitializeComponent();
            PrintActiveRecordsLabel(ListenArt);

        }




        private void PrintActiveRecordsLabel(InventurlistenArt la)
        {

            PageDocument pageDocument = null;
            switch (la)
            {
                case InventurlistenArt.Zaehliste:
                    {
                        pageDocument = ActiveReportsTools<int>.GetDocument(StaticMethods.GetPath(@"Drucklisten\Inventurliste.rdlx"));
                        pageDocument.LocateDataSource += new GrapeCity.ActiveReports.LocateDataSourceEventHandler(pageDocument_LocateDataSource);
                        break;
                    }

                case InventurlistenArt.Differenzliste:
                    {
                        pageDocument = ActiveReportsTools<int>.GetDocument(StaticMethods.GetPath(@"Drucklisten\Differenzliste.rdlx"));
                        pageDocument.LocateDataSource += new GrapeCity.ActiveReports.LocateDataSourceEventHandler(pageDocument_LocateDataSource);
                        break;
                    }
                case InventurlistenArt.Artikelliste:
                    {
                        pageDocument = ActiveReportsTools<int>.GetDocument(StaticMethods.GetPath(@"Drucklisten\Artikelliste.rdlx"));
                        pageDocument.LocateDataSource += new GrapeCity.ActiveReports.LocateDataSourceEventHandler(pageDocument_LocateDataSource);
                        if (DoExportExcel)
                        {
                            string fName = ConfigEntry<string>.GetConfigEntry("ArtikellisteExport", @"F:\ALLGEMEIN\EDV\Datenbank_Neu\Export\Artikellisten\liste.xls");
                            GrapeCity.ActiveReports.Export.Excel.Section.XlsExport XLSExport1 = new GrapeCity.ActiveReports.Export.Excel.Section.XlsExport();
                            XLSExport1.Export(pageDocument, fName);
                        }
                        
                        break;
                    }


                default:
                    break;
            }




            viewer1.LoadDocument(pageDocument);

        }

        void pageDocument_LocateDataSource(object sender, GrapeCity.ActiveReports.LocateDataSourceEventArgs args)
        {

            string buffer = "Ohne Datum";
            if (InvDatum.HasValue)
            {
                var d = (DateTime)InvDatum;
                buffer = d.ToShortDateString();
            }



            using (var db = new DAL.SteinbachEntities())
            {

                if (this.ListenArt == InventurlistenArt.Artikelliste)
                {
                    var query = from p in lagerliste
                                join f in db.firmen on p.id_lieferant equals f.id

                                select new
                                {
                                    Artikelname = p.bezeichnung,
                                    Artikelnummer = p.artikelnr,
                                    BeschreibungEng = p.beschreibungeng,
                                    Beschreibung = p.beschreibung,
                                    Lieferant = f.name,
                                    MinBestand = p.anzahlmin,
                                    PreisNetto = p.preiseuro,
                                    id_artikel = p.id


                                };

                    args.Data = query.ToList();

                }
                else
                {
                    var query = from p in InvPositionenEnum
                                join l in db.lagerlisten on p.id_artikel equals l.id

                                select new
                                {
                                    Artikelname = p.Artikelname,
                                    Artikelnummer = p.Artikelnummer,
                                    Sollmenge = p.Sollmenge,
                                    Zaehlmenge = p.Zaehlmenge,
                                    Differenz = p.Differenz,
                                    Datum = buffer,
                                    Ort = String.Format("{0} / {1}", l.ortregal, l.ortbox),
                                    id_artikel = p.id_artikel,
                                    id = p.id
                                }
                              ;

                    args.Data = query.ToList();
                }
            }
        }


    }
}
