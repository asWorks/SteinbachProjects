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
using GrapeCity.ActiveReports.Expressions.ExpressionObjectModel;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using CommonTools.Tools;
using ProjektDB.Tools;
using DAL.Tools;
using System.Globalization;


namespace ProjektDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BelegViewer : Window
    {
        GrapeCity.ActiveReports.Document.PageDocument pageDocument;
        int BelID = 0;
        string Belegname = string.Empty;
        private ViewModels.SI_BelegeViewModel BelegvModel;
        Belege.SI_InvoiceStandard rpt = null;

        public BelegViewer()
        {
            viewer1 = new GrapeCity.ActiveReports.Viewer.Wpf.Viewer();
            InitializeComponent();
        }

        public BelegViewer(int BelegID)
        {
            BelID = BelegID;
            viewer1 = new GrapeCity.ActiveReports.Viewer.Wpf.Viewer();
            Belegname = @"Belege\Brunvoll_Report.rdlx";
            InitializeComponent();
        }

        public BelegViewer(ViewModels.SI_BelegeViewModel vModel)
        {
            BelegvModel = vModel;
            viewer1 = new GrapeCity.ActiveReports.Viewer.Wpf.Viewer();
            Belegname = @"Belege\Brunvoll_Report.rdlx";
            InitializeComponent();
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {


            ShowReport();
        }

        void viewer1_LoadCompleted(object sender, EventArgs e)
        {


            //LoggingTool.LogMessage("Entering LoadCompleted","BelegViewer","LoadCompleted", LoggingTool.LogState.low);

            //int res = CommonTools.Tools.ConfigEntry<int>.GetConfigEntry("DoPdfExportBelege", "1");
            //if (res == 1)
            //{
            //    string fName = ConfigEntry<string>.GetConfigEntry("BelegePFDExport", "e:\\test\\exportPDF\\TestAR.pdf");
            //    fName += String.Format("Beleg_{0}.pdf", BelID);
            //    GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport PdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
            //    PdfExport1.Export(pageDocument, fName);

            //}

            //LoggingTool.LogMessage("Done LoadCompleted", "BelegViewer", "LoadCompleted", LoggingTool.LogState.low);

        }

        private void btnEtikett_Click(object sender, RoutedEventArgs e)
        {

            pageDocument = ActiveReportsTools<int>.GetDocument(StaticMethods.GetPath(@"Belege\PageReport1.rdlx"));
            viewer1.LoadDocument(pageDocument);
        }

        private void cboReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string HeaderPictureName = string.Empty;
            string FooterPictureName = string.Empty;
            string AlternateHeaderPictureName = string.Empty;
            string AlternateFooterPictureName = string.Empty;

            var sel = (ComboBoxItem)e.AddedItems[0];
            switch (sel.Content.ToString())
            {

                case "SI_Beleg":
                    {
                        HeaderPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("HeaderPicture_SI_Beleg", "H_SI-Brief-Kopf-II.jpg", "");
                        FooterPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("FooterPicture_SI_Beleg", "F_SI-Brief-Fuss.jpg", "");
                        AlternateHeaderPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("AlternateHeaderPicture_SI_Beleg", "H_SI-Brief-Kopf-II.jpg", "");
                        AlternateFooterPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("AlternateFooterPicture_SI_Beleg", "F_SI-Brief-Fuss.jpg", "");

                        // ShowReportSIStandard();
                        break;
                    }
                case "SI_Handelgesellschaft_Beleg":
                    {
                        HeaderPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("HeaderPicture_SI_Handelgesellschaft_Beleg", "H_SI-Brief-Hndlsgs_Kopf-II.jpg", "");
                        FooterPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("FooterPicture_SI_Handelgesellschaft_Beleg", "F_SI-Brief-Hndlsgs_Fuss-I.jpg", "");
                        AlternateHeaderPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("AlternateHeaderPicture_SI_Handelgesellschaft_Beleg", "H_SI-Brief-Hndlsgs_Kopf-II.jpg", "");
                        AlternateFooterPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("AlternateFooterPicture_SI_Handelgesellschaft_Beleg", "F_SI-Brief-Hndlsgs_Fuss-I.jpg", "");

                        // ShowReportSIStandard();
                        break;
                    }

                case "SI_Brunvoll_Beleg":
                    {
                        HeaderPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("HeaderPicture_SI_Brunvoll_Beleg", "H_SI-Brunvoll-Kopf.jpg", "");
                        FooterPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("FooterPicture_SI_Brunvoll_Beleg", "F_SI-Brunvoll-Fuss.jpg", "");
                        AlternateHeaderPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("AlternateHeaderPicture_SI_Brunvoll_Beleg", "H_SI-Brunvoll-Kopf.jpg", "");
                        AlternateFooterPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("AlternateFooterPicture_SI_Brunvoll_Beleg", "F_SI-Brunvoll-Fuss.jpg", "");

                        // ShowReportSIStandard();
                        break;
                    }


                case "Blanko_Beleg":
                    {
                        HeaderPictureName = CommonTools.Tools.ConfigEntry<string>
                            .GetConfigEntry("HeaderPicture_Blanko", "Blanko", "Wenn Name ==  'Blanko' wird kein Bild geladen.");
                        FooterPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("FooterPicture_Blanko", "Blanko", "Wenn Name ==  'Blanko' wird kein Bild geladen.");
                        AlternateHeaderPictureName = CommonTools.Tools.ConfigEntry<string>
                           .GetConfigEntry("AlternateHeaderPicture_Blanko", "Blanko", "Wenn Name ==  'Blanko' wird kein Bild geladen.");
                        AlternateFooterPictureName = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("AlternateFooterPicture_Blanko", "Blanko", "Wenn Name ==  'Blanko' wird kein Bild geladen.");


                        // ShowReportSIStandard();
                        break;
                    }

                default:
                    break;
            }

            string PicturePath = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("SI_Belege_HeaderFooterPicture_Path", @"F:\ALLGEMEIN\EDV\Datenbank_Neu\BilderBelege\", "");

            string HeaderPicturePath = HeaderPictureName != "Blanko" ? PicturePath + HeaderPictureName : HeaderPictureName;
            string FooterPicturePath = FooterPictureName != "Blanko" ? PicturePath + FooterPictureName : FooterPictureName;
            string AlternateHeaderPicturePath = AlternateHeaderPictureName != "Blanko" ? PicturePath + AlternateHeaderPictureName : AlternateHeaderPictureName;
            string AlternateFooterPicturePath = AlternateFooterPictureName != "Blanko" ? PicturePath + AlternateFooterPictureName : AlternateFooterPictureName;


            ShowReportSIStandard(HeaderPicturePath, FooterPicturePath, AlternateHeaderPicturePath, AlternateFooterPicturePath);


        }



        private void ShowReportSIStandard(string HeaderPicName, string FooterPicName, string AltHeaderPicName, string AltFooterPicName)
        {
            rpt = new Belege.SI_InvoiceStandard(BelegvModel, HeaderPicName, FooterPicName, AltHeaderPicName, AltFooterPicName);
            rpt.DataSource = GetDataSource();

            viewer1.LoadDocument(rpt);

        }

        private List<Belege.Values> GetDataSource()
        {
            CultureInfo ci;
            if (BelegvModel.BelegePositionen == null)
            {
                return null;
            }




            string Head_ArtikelNr;
            string Head_ArtikelBez;
            string Head_Menge;
            string Head_WKZ;
            string Head_ZuAbschlag;
            string Head_Preis;
            string Head_GesamtPreis;
            string Head_SummePositionen;
            string Head_SummeBeleg;
            string Head_Unit;
            string DateFormatString;
            string Head_VAT;

            string belegart;
            string CultureString;

            using (var db = new DAL.SteinbachEntities())
            {
                var loc = db.C_Localizing.Where(n => n.id_Sprache == BelegvModel.id_Sprache && n.Objektname == 1);
                Head_ArtikelNr = loc.Where(n => n.Begriffname == "Header_ArtikelNr").SingleOrDefault().Wert;
                Head_ArtikelBez = loc.Where(n => n.Begriffname == "Header_ArtikelBez").SingleOrDefault().Wert;
                Head_Menge = loc.Where(n => n.Begriffname == "Header_Menge").SingleOrDefault().Wert;
                Head_WKZ = loc.Where(n => n.Begriffname == "Header_WKZ").SingleOrDefault().Wert;
                Head_ZuAbschlag = loc.Where(n => n.Begriffname == "Header_ZuAbschlag").SingleOrDefault().Wert;
                Head_Preis = loc.Where(n => n.Begriffname == "Header_Preis").SingleOrDefault().Wert;
                Head_GesamtPreis = loc.Where(n => n.Begriffname == "Header_GesamtPreis").SingleOrDefault().Wert;
                Head_SummePositionen = loc.Where(n => n.Begriffname == "Header_SummePositionen").SingleOrDefault().Wert;
                Head_SummeBeleg = loc.Where(n => n.Begriffname == "Header_SummeBeleg").SingleOrDefault().Wert;
                Head_Unit = loc.Where(n => n.Begriffname == "Header_Unit").SingleOrDefault().Wert;
                Head_VAT = loc.Where(n => n.Begriffname == "Header_VAT").SingleOrDefault().Wert;

            }

            using (var db = new DAL.SteinbachEntities())
            {
                var loc = db.C_Localizing.Where(n => n.id_Sprache == BelegvModel.id_Sprache && n.Objektname == 2);
                //var ba = loc.Where(n => n.Begriffname == BelegvModel.Belegart).SingleOrDefault();
                var ba = loc.Where(n => n.Begriffname == BelegvModel.SelectedBelegarten.id).SingleOrDefault();

                belegart = ba == null ? "Belegart konnte nicht übersetzt werden." : ba.Wert;
                ci = GetCultureInfo(db);
            }

            using (var db = new DAL.SteinbachEntities())
            {
                var loc = db.C_Localizing.Where(n => n.id_Sprache == BelegvModel.id_Sprache && n.Objektname == 4);
                var ba = loc.Where(n => n.Begriffname == "DateFormatString").SingleOrDefault();
                DateFormatString = ba == null ? "d. MMMM yyyy" : ba.Wert;

            }


            //string addresse = GetAdresse(Head_VAT);

            // CultureInfo ci = new CultureInfo(CultureString);
            var dt = (DateTime)BelegvModel.Belegdatum;

            belegart += "  " + BelegvModel.Belegnummer;
            int Wirkung = BelegvModel.SelectedBelegarten.Wirkung != -1 ? 1 : -1;
            var query = from b in BelegvModel.BelegePositionen
                        select new Belege.Values

                        {


                            Header_Unit = Head_Unit,
                            Header_ArtikelNr = Head_ArtikelNr,
                            Header_ArtikelBez = Head_ArtikelBez,
                            Header_Menge = Head_Menge,
                            Header_WKZ = Head_WKZ,
                            Header_ZuAbschlag = Head_ZuAbschlag,
                            Header_Preis = Head_Preis,
                            Header_GesamtPreis = Head_GesamtPreis,
                            Header_SummeBeleg = Head_SummeBeleg,
                            Header_SummePositionen = Head_SummePositionen,
                            Belegdatum = dt.ToString(DateFormatString, ci),


                            Bemerkung = BelegvModel.Bemerkung,
                            Belegart_Datum = belegart,
                            Pos = b.Pos.ToString(),
                            Unit = b.Einheit,
                            ZuAbschlag = b.Rabatt.HasValue ? decimal.Parse((b.Rabatt / 100).ToString()).ToString("P") : decimal.Parse("0").ToString("P"),
                            GesamtPreis = b.Endpreis.HasValue ? decimal.Parse((b.Endpreis * Wirkung).ToString()).ToString("C") : decimal.Parse("0").ToString("C"),
                            Adress = GetAdresse(Head_VAT),
                            Bearbeiter = GetBetreuer(),
                            PrefixBearbeiter = GetPrefixAdresse(),
                            Belegnummer = BelegvModel.Belegnummer,
                            Disclaimer1 = BelegvModel.Belegtext,
                            ArtikelNr = b.Artikelnummer,
                            ArtikelBez = b.Bezeichnung,
                            Menge = (b.Menge * Wirkung).ToString(),
                            WKZ = b.wkz,
                            Preis = b.Preis.HasValue ? decimal.Parse(b.Preis.ToString()).ToString("C") : decimal.Parse("0").ToString("C"),
                            Belegart = BelegvModel.BelegartName,
                            Signatur1 = BelegvModel.Signatur1,
                            Signatur2 = BelegvModel.Signatur2,
                            Zahlungsbedingungen = GetPaymentTerms(),
                            SummePositionen = (BelegvModel.SummeBelegPositionen * Wirkung).ToString("C"),
                            SummeBeleg = (BelegvModel.Belegsumme * Wirkung) .ToString("C")

                        };



            return query == null ? null : query.ToList();

        }

        private CultureInfo GetCultureInfo(DAL.SteinbachEntities db)
        {
            CultureInfo ci;
            string CultureString;
            var cs = db.AuswahlEintraege.Where(n => n.Gruppe == "TypSprache" && n.id_This_int == BelegvModel.id_Sprache).SingleOrDefault();
            CultureString = cs.ai_string == null || cs.ai_string == string.Empty ? "de-DE" : cs.ai_string;
            try
            {
                ci = new CultureInfo(CultureString);
            }
            catch (Exception)
            {
                ci = new CultureInfo("de-DE");

            }

            return ci;
        }


        string GetPaymentTerms()
        {



            if (BelegvModel.SelectedBelegarten != null)
            {
                if (BelegvModel.SelectedBelegarten.ZahlungsbedingungenSetzen == 1)
                {

                    StringBuilder sb = new StringBuilder();
                    string terms = "Payment terms : ";
                    using (var db = new DAL.SteinbachEntities())
                    {
                        var loc = db.C_Localizing.Where(n => n.id_Sprache == BelegvModel.id_Sprache && n.Objektname == 1 && n.Begriffname == "HeaderPaymentTerms");
                        if (loc.Any())
                        {
                            terms = loc.SingleOrDefault().Wert;
                        }

                    }

                    if (BelegvModel.ZahlungsbedingungText != string.Empty)
                    {
                        sb.Append(terms);
                        sb.AppendLine(BelegvModel.ZahlungsbedingungText);
                    }


                    return sb.ToString();
                }
            }

            return string.Empty;




        }


        private string GetBetreuer()
        {

            var sb = new StringBuilder();
            var pers = DAL.Session.User;
            
            sb.AppendLine(pers.benutzername);
            sb.AppendLine(pers.email);
            sb.AppendLine(pers.Telefon);
            return sb.ToString();

        }

        private string GetPrefixAdresse()
        {
            using (var db = new DAL.SteinbachEntities())
            {
                var sb = new StringBuilder();
                var pers = DAL.Session.User;
                var details = db.C_Localizing.Where(n => n.Objektname == pers.id && n.id_Sprache == BelegvModel.id_Sprache);

                sb.AppendLine(details.Where(n => n.Begriffname == "Prefix_Anrede_Beleg").SingleOrDefault().Wert);
                // sb.AppendLine(pers.benutzername);
                sb.AppendLine(details.Where(n => n.Begriffname == "Prefix_Email_Beleg").SingleOrDefault().Wert);
                // sb.AppendLine(pers.email);
                sb.AppendLine(details.Where(n => n.Begriffname == "Prefix_Durchwahl_Beleg").SingleOrDefault().Wert);
                //  sb.AppendLine(pers.Telefon);
                return sb.ToString();
            }
        }

        private string GetAdresse(string Head_VAT)
        {
            var sb = new StringBuilder();
            if (BelegvModel.ZusatzInfo != string.Empty && BelegvModel.ZusatzInfo != null)
            {
                sb.AppendLine(BelegvModel.ZusatzInfo);
            }
            if (BelegvModel.ZusatzInfo2 != string.Empty && BelegvModel.ZusatzInfo2 != null)
            {
                sb.AppendLine(BelegvModel.ZusatzInfo2);
            }
            if (BelegvModel.ZusatzInfo3 != string.Empty && BelegvModel.ZusatzInfo3 != null)
            {
                sb.AppendLine(BelegvModel.ZusatzInfo3);
            }

            sb.AppendLine(BelegvModel.Straße);
            sb.Append(BelegvModel.PLZ + " ");
            sb.AppendLine(BelegvModel.Ort);
            if (BelegvModel.Land != string.Empty)
            {
                sb.AppendLine(BelegvModel.Land);
            }
            if (BelegvModel.VAT != null && BelegvModel.VAT != string.Empty)
            {
                sb.Append(Head_VAT);
                sb.AppendLine(BelegvModel.VAT);
            }


            return sb.ToString();

        }

        void ShowReport()
        {
            try
            {

                int BelegID = BelID;

                pageDocument = ActiveReportsTools<int>.GetDocument(StaticMethods.GetPath(Belegname));
                pageDocument.LocateDataSource += new LocateDataSourceEventHandler(pageDocument_LocateDataSource);

                int res = CommonTools.Tools.ConfigEntry<int>.GetConfigEntry("DoPdfExportBelege", "0");
                if (res == 1)
                {
                    string fName = ConfigEntry<string>.GetConfigEntry("BelegePFDExport", @"F:\ALLGEMEIN\EDV\Datenbank_Neu\Export\Belege\");
                    fName += String.Format("{0}.pdf", BelegvModel.Belegnummer.Trim());
                    GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport PdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                    PdfExport1.Export(pageDocument, fName);

                }
                viewer1.LoadDocument(pageDocument);


                //LoggingTool.LogMessage("Calling Getdocument", "BelegViewer", "ShowReport", LoggingTool.LogState.low);
                //pageDocument = ActiveReportsTools<int>.GetDocument(StaticMethods.GetPath(Belegname));
                //LoggingTool.LogMessage("Calling LocateDataSource", "BelegViewer", "ShowReport", LoggingTool.LogState.low);
                //pageDocument.LocateDataSource += new LocateDataSourceEventHandler(pageDocument_LocateDataSource);
                //LoggingTool.LogMessage("Calling LoadDocument", "BelegViewer", "ShowReport", LoggingTool.LogState.low);
                //viewer1.LoadDocument(pageDocument);
                ////LoggingTool.LogMessage("Calling LoadCompleted", "BelegViewer", "ShowReport", LoggingTool.LogState.low);
                ////viewer1.LoadCompleted += new GrapeCity.ActiveReports.Document.LoadCompletedEventHandler(viewer1_LoadCompleted);
                //LoggingTool.LogMessage("Done ShowReport", "BelegViewer", "ShowReport", LoggingTool.LogState.low);


            }
            catch (System.IO.IOException ex)
            {
                ErrorMethods.HandleStandardError(ex);
            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
            }



        }




        void ShowReport1()
        {
            try
            {


                int BelegID = BelID;


                pageDocument = ActiveReportsTools<int>.GetDocument(StaticMethods.GetPath(Belegname));

                pageDocument.PageReport.Report.DataSources[0].DataSourceReference = StaticMethods.GetPath("SteinbachSharedDataSource.rdsx");

                ActiveReportsTools<int>.SetParameter("BelegID", BelegID, ref pageDocument);

                viewer1.LoadDocument(pageDocument);

                viewer1.LoadCompleted += new GrapeCity.ActiveReports.Document.LoadCompletedEventHandler(viewer1_LoadCompleted);

            }
            catch (Exception ex)
            {

                ErrorMethods.ShowErrorMessageToUser(ex, "");
            }
        }

        void pageDocument_LocateDataSource(object sender, LocateDataSourceEventArgs args)
        {
            LoggingTool.LogMessage("Entering LocateDataSource", "BelegViewer", "LocateDataSource", LoggingTool.LogState.low);


            var query = from b in BelegvModel.BelegePositionen
                        select new

                          {
                              id = BelegvModel.id,
                              ZusatzInfo = BelegvModel.ZusatzInfo,
                              ZusatzInfo2 = BelegvModel.ZusatzInfo2,
                              ZusatzInfo3 = BelegvModel.ZusatzInfo3,
                              Ort = BelegvModel.Ort,
                              Straße = BelegvModel.Straße,
                              PLZ = BelegvModel.PLZ,
                              Belegdatum = BelegvModel.Belegdatum,
                              Belegnummer = BelegvModel.Belegnummer,
                              Bemerkung = BelegvModel.Bemerkung,
                              Artikelnummer = b.Artikelnummer,
                              Beschreibung = b.Beschreibung,
                              Description = b.Description,
                              Menge = b.Menge,
                              wkz = b.wkz,
                              Preis = b.Preis,
                              id_Sprache = BelegvModel.id_Sprache,
                              Belegart = BelegvModel.BelegartName
                          };


            args.Data = query.ToList();

            //                       PLZ = b.PLZ,
            //                       Ort = a.Ort,
            //                       Straße = a.Straße,
            //                       Land = a.Land,
            //                       Zusatzinfo = a.ZusatzInfo,
            //                       VAT = a.VAT,



            //using (var db = new DAL.SteinbachEntities())
            //{

            //    var bel = db.SI_Belege.Where(i => i.id == BelID).SingleOrDefault();
            //    if (bel.Belegart == "inv" || bel.Belegart == "um")
            //    {

            //var query = from b in db.SI_Belege
            //            join p in db.SI_BelegePositionen on b.id equals p.id_Beleg
            //            join s in db.StammBelegarten on b.Belegart equals s.id
            //            where b.id == BelID
            //            select new
            //            {
            //                id = b.id,
            //                Belegdatum = b.Belegdatum,
            //                Belegnummer = b.Belegnummer,
            //                Bemerkung = b.Bemerkung,
            //                Artikelnummer = p.Artikelnummer,
            //                Beschreibung = p.Beschreibung,
            //                Description = p.Description,
            //                Menge = p.Menge,
            //                wkz = p.wkz,
            //                Preis = p.Preis,
            //                Belegart = s.Belegart
            //            };


            //        args.Data = query.ToList();

            //    }
            //    else
            //    {
            //        var query = from b in db.SI_Belege
            //                    join p in db.SI_BelegePositionen on b.id equals p.id_Beleg
            //                    join pro in db.projekte on b.id_Projekt equals pro.id
            //                    join a in db.Firmen_Adressen on pro.id_Lieferadresse equals a.id
            //                    join s in db.StammBelegarten on b.Belegart equals s.id
            //                    where b.id == BelID
            //                    select new
            //                    {
            //                        id = b.id,
            //                        Belegdatum = b.Belegdatum,
            //                        Belegnummer = b.Belegnummer,
            //                        Bemerkung = b.Bemerkung,
            //                        Artikelnummer = p.Artikelnummer,
            //                        Beschreibung = p.Beschreibung,
            //                        Description = p.Description,
            //                        Menge = p.Menge,
            //                        wkz = p.wkz,
            //                        Preis = p.Preis,
            //                        PLZ = a.PLZ,
            //                        Ort = a.Ort,
            //                        Straße = a.Straße,
            //                        Land = a.Land,
            //                        Zusatzinfo = a.ZusatzInfo,
            //                        VAT = a.VAT,
            //                        Belegart = s.Belegart
            //                    };


            //        args.Data = query.ToList();
            //    }





            //    LoggingTool.LogMessage("Done LocateDataSource", "BelegViewer", "LocateDataSource", LoggingTool.LogState.low);


            //}

        }

        private void PDFExport_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (rpt != null)
                {
                    string fName = ConfigEntry<string>.GetConfigEntry("BelegePFDExport", @"F:\ALLGEMEIN\EDV\Datenbank_Neu\Export\Belege\");
                    fName += String.Format("{0}.pdf", BelegvModel.Belegnummer.Trim());
                    GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport PdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                    PdfExport1.Export(rpt.Document, fName);
                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler bei PDF Erzeugung.");
            }




        }


    }
}
