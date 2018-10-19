using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ProjektDB.Repositories;
using ProjektDB.Tools;
using ProjektDB.views;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DAL;
using CommonTools.Views;

using System.Windows.Media;
using CommonTools.Tools;
using System.Text;
using System.Diagnostics;
using DAL.Tools;

namespace ProjektDB
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : Page
    {

        #region "Declarations"

        private SteinbachEntities db;
        private ListCollectionView ProjekteView;
        private CollectionViewSource ProjekteViewSource;
        private ProjektRepository ProjektRepo;
        public DispatcherTimer timer;
        int imagesCount;
        int n = 4;
        //   bool[] tviState;

        #endregion


        #region "Page Load"

        public Navigation()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Session.User == null)
            {
                var login = new Logon();
                login.ShowDialog();

            }

          
            if (StaticMethods.GetConfigEntry("AnzeigenKundendaten") != "0" && Session.User.rights == "su")
            {
                tviKundenBereinigen.Visibility = Visibility.Visible;
            }

            if (StaticMethods.GetConfigEntry("AnzeigenAddFirmendaten") == "0")
            {
                tviAddFirmennummern.Visibility = Visibility.Collapsed;
            }

            if (StaticMethods.GetConfigEntry("AnzeigenZahlungsbedingungen") == "0")
            {
                tviZahlungsbedingungen.Visibility = Visibility.Collapsed;
            }


         


            db = new SteinbachEntities();
            ProjektRepo = new ProjektRepository(db);
            ShowProjekte();
            InitData();
            //  this.tviAllgemein.IsExpanded = true;

            // timer = new DispatcherTimer();
            //var x = new Properties.Settings();
            // sliAnimationFrequency.Value = x.AnimationsFrequenz_ms;
            //timer.Interval = TimeSpan.FromMilliseconds(x.AnimationsFrequenz_ms);
            //timer.Tick += timer_Tick;

            // Obsolete 30.08.2012
            //imagesCount = carouselListBox.Items.Count;

            //sliAnimationFrequency.ValueChanged += sliAnimationFrequency_ValueChanged;

            MainTreeView.SetTreeViewExpandedState("Gruppe");


            //timer.Start();      //rather let the user decide whether to start or stop the timer by checking a checkbox 
        }




        private void ShowProjekte()
        {
            //var ProjektQuery = ProjektRepo.GetProjekteListe(1,20);
            var ProjektQuery = ProjektRepo.GetLikeTest2();
            ProjekteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Projekte_ViewSource")));
            ProjekteViewSource.Source = ProjektQuery;
            ProjekteView = (ListCollectionView)ProjekteViewSource.View;

        }

        #endregion


        #region LoadProjektliste

        private void LoadProjektliste(string Data, string Caption, string Struktur, string PreFilter)
        {
            LoadProjektliste(Data, Caption, Struktur, PreFilter, false);
        }
        private void LoadProjektliste(string Data, string Caption, string Struktur, string PreFilter, bool showKommissionPaid)
        {

            try
            {
                var p = new Projektliste(Data, Caption, Struktur, PreFilter, showKommissionPaid);
                NavigationService.Navigate(p);
            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
            }


        }

        private void LoadProjektliste(string Data, string Caption, string Struktur, string PreFilter, bool showKommissionPaid, ListeTypeEnum Type)
        {

            try
            {
                var p = new Projektliste(Data, Caption, Struktur, PreFilter, showKommissionPaid, Type, true);
                NavigationService.Navigate(p);
            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
            }


        }


        private void LoadProjektliste(string Data, string Caption, string Struktur, string PreFilter, bool showKommissionPaid, ListeTypeEnum Type, bool showFilterYear)
        {
            try
            {
                var p = new Projektliste(Data, Caption, Struktur, PreFilter, showKommissionPaid, Type, showFilterYear);

                NavigationService.Navigate(p);
            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
            }


        }

        #endregion


        #region "Menue Selection"


        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            var tv = (TreeView)sender;
            var tvi = (TreeViewItem)tv.SelectedItem;
            Cursor = System.Windows.Input.Cursors.Wait;

            //GetTVState();
            MainTreeView.GetTreeViewExpandedState("Gruppe");

            if (tvi != null && tvi.Tag != null)
            {
                switch (tvi.Tag.ToString())
                {
                    case "Gruppe":
                        {
                            tvi.IsExpanded = !tvi.IsExpanded;
                            Cursor = System.Windows.Input.Cursors.Arrow;
                            break;
                        }

                    case "ProjektNeu":
                        {

                            var p = new MainWindow(0);
                            //LoadProjektliste("Projekt", "Projekttabelle", "AnlagenAuftraege", "");
                            NavigationService.Navigate(p);
                            break;
                        }
                    case "Projekttabelle":
                        {

                            LoadProjektliste("Projekt", "Projekttabelle", "AnlagenAuftraege", "");
                            break;
                        }
                    case "Kalkulationstabellen":
                        {

                            //var p = new Kalkulation();

                            //NavigationService.Navigate(p);
                            LoadProjektliste("Kalkulation", "Kalkulationstabellen", "Kalkulationstabellen", "", false, ListeTypeEnum.Kalkulationliste);
                            break;
                        }

                    case "Firma":
                        {

                            //var p = new Kalkulation();

                            //NavigationService.Navigate(p);
                            LoadProjektliste("Firma", "Firmen", "Firma", "", false, ListeTypeEnum.Firma, false);
                            break;
                        }

                    case "Schiff":
                        {

                            //var p = new Kalkulation();

                            //NavigationService.Navigate(p);
                            LoadProjektliste("Schiff", "Schiffe", "Schiff", "", false, ListeTypeEnum.Schiff, false);
                            break;
                        }

                    case "Person":
                        {

                            //var p = new Kalkulation();

                            //NavigationService.Navigate(p);

                            LoadProjektliste("Person", "Personen", "Person", "", false, ListeTypeEnum.Person, false);
                            break;
                        }

                    case "Aggregat":
                        {

                            //var p = new Kalkulation();

                            //NavigationService.Navigate(p);
                            LoadProjektliste("Aggregat", "Aggregate", "Aggregat", "", false, ListeTypeEnum.Aggregat, false);
                            break;
                        }

                    case "Lagerliste":
                        {

                            //var p = new Kalkulation();

                            //NavigationService.Navigate(p);
                            LoadProjektliste("Lagerliste", "Lagerliste", "Lagerlisten", "", false, ListeTypeEnum.Artikel, false);
                            break;
                        }

                    case "Artikelliste":
                        {
                            Trace.WriteLine("Navigation.Xaml.cs Artikelliste Start");
                            Trace.Flush();
                            LoggingTool.addDatabaseLogging("", 0, 0, "Navigation.Xaml.cs Artikelliste Start", DateTime.Now.ToLongTimeString());
                            var p = new EditArtikelListe();
                            p.ShowDialog();
                            Cursor = System.Windows.Input.Cursors.Arrow;
                             Trace.WriteLine("Navigation.Xaml.cs Artikelliste stop");
                            Trace.Flush();
                            LoggingTool.addDatabaseLogging("", 0, 0, "Navigation.Xaml.cs Artikelliste stop", DateTime.Now.ToLongTimeString());
                            
                            break;
                           
                        }


                    case "DruckArtikelLabel":
                        {

                            var p = new PrintArtikelLabel() ;
                            p.ShowDialog();
                            Cursor = System.Windows.Input.Cursors.Arrow;
                            break;
                        }

                    case "BuildArtikelLookout":
                        {

                            CommonTools.Tools.HelperTools.BuildArtikelLookout();
                            Cursor = System.Windows.Input.Cursors.Arrow;
                            break;
                        }



                    case "UpdatePrices":
                        {

                            var p = new PreiseAktualisierenListe();
                            p.ShowDialog();
                            Cursor = System.Windows.Input.Cursors.Arrow;
                            break;
                        }


                    case "SI_Belege":
                        {

                            LoadProjektliste("SI_Belege", "SI Belege", "SI_Belege", "", false, ListeTypeEnum.SI_Beleg, false);
                            break;

                            //var f = new EditBelege(36);
                            //f.ShowDialog();
                            //break;
                        }

                    case "Lagerorte":
                        {

                            LoadProjektliste("Lagerorte", "Lagerorte", "Lagerorte", "", false, ListeTypeEnum.Lagerorte, false);
                            break;

                            //var f = new EditBelege(36);
                            //f.ShowDialog();
                            //break;
                        }

                    case "Textbausteine":
                        {

                            LoadProjektliste("Textbausteine", "Textbausteine", "Textbausteine", "", false, ListeTypeEnum.Textbausteine, false);
                            break;

                            //var f = new EditBelege(36);
                            //f.ShowDialog();
                            //break;
                        }

                    case "SI_Inventuren":
                        {

                            LoadProjektliste("SI_Inventuren", "SI Inventuren", "SI_Inventuren", "", false, ListeTypeEnum.SI_Inventuren, false);
                            break;

                            //var f = new Inventur(0);
                            //f.ShowDialog();
                            //break;
                        }

                    case "Kostenstellen":
                        {

                            var f = new views.KostenstellenView();
                            f.ShowDialog();
                            Cursor = System.Windows.Input.Cursors.Arrow;
                            break;

                          
                        }


                    case "TestReports":
                        {

                            var r = new Reports.SI_Belege_RepViewer();
                            r.ShowDialog();
                            break;

                        }
                    
                   


                    case "OffeneAusgaenge":
                        {

                            LoadProjektliste("OffeneEinAusgaenge", "Offene Ausgänge", "AnlagenAuftraege", "it.weiterberechnet = 1");
                            break;
                        }


                    case "OffeneEingaenge":
                        {

                            // LoadProjektliste("OffeneEinAusgaenge", "Offene Ausgänge", "AnlagenAuftraege", "((it.rechnungfaellig != null) || it.rechnungan != null) && (it.rechnungvom == null)");
                            LoadProjektliste("OffeneEinAusgaenge", "Offene Eingänge", "AnlagenAuftraege", "((it.rechnungfaellig is not null) or it.rechnungan is not null) and (it.rechnungvom is null)");

                            break;
                        }

                    case "Versionsinfo":
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.Append("Versionsinfo");
                                sb.AppendLine("");
                                sb.Append("Datenbank : ");
                                sb.Append(Session.DatabaseVersion);
                                sb.AppendLine();
                                sb.Append("Programmversion : ");
                                sb.AppendLine(Properties.Settings.Default.AppVersion);

                                CommonTools.Tools.UserMessage.NotifyUser(sb.ToString());
                                Cursor = System.Windows.Input.Cursors.Arrow;
                                break;
                            }


                    case "stammZahlungsbedingung":
                        {
                            LoadProjektliste("Zahlungsbedingung", "Stamm Zahlungsbedingungen", "StammZahlungsbedingungen", "", false, ListeTypeEnum.Zahlungsbedingung, false);

                            break;
                        }

                    case "AddFirmenNr":
                        {
                            //try
                            //{
                            //    var addF = new Temp.WriteFirmennummerToProjekt();
                            //    addF.DoWriteFirmannummer();
                            //    break;
                            //}
                            //catch (Exception ex)
                            //{

                            //    MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));
                            //}
                            break;
                        }

                  



                    case "MergeData":
                        {

                            //if (MessageBox.Show("Daten werden verändert. Dies ist eine Systemfunktion", "Sicherheitshinweis", MessageBoxButton.OKCancel) != MessageBoxResult.Cancel)
                            //{
                            //    var cc = new Temp.CleanAndCopy();
                            //    cc.CopyProjektAnlagen2SChiffAnlagen();
                            //    cc.DoAddCustomers();
                            //}
                            //Cursor = System.Windows.Input.Cursors.Arrow;

                            break;


                        }


                    case "ShowLagerListen":
                        {


                            //var r = new Reports.LagerListenViewer();
                            //r.Show();
                            //Cursor = System.Windows.Input.Cursors.Arrow;

                            break;


                        }


                    case "EditKunden":
                        {
                            //var kt = new Temp.EditJetsTabelleLandSchiff();
                            //kt.ShowDialog();

                            var kb = new Temp.EditKundentabelle1();
                            kb.ShowDialog();

                            Cursor = System.Windows.Input.Cursors.Arrow;

                            //var ad = new AddKunden();
                            //ad.DoAddCustomers();
                            //try
                            //{
                            //    var cm = new CheckMailReminder();
                            //    cm.checkMail();
                            //    // MessageBox.Show(libSendOutlookMail.OutlookMail.DisplayAccountInformation());
                            //}
                            //catch (Exception ex)
                            //{

                            //    LoggingTool.LogExeption(ex, "Navigation", "Nav");
                            //}


                            break;
                        }
                    case "TestSource":
                        {
                            // var st = new Temp.TestFtb();
                            //st.ShowDialog();




                            break;
                        }


                    case "TestMail":
                        {
                            //try
                            //{
                            //    string adress = Session.User.email;
                            //    var mail = new libSendOutlookMail.OutlookMail();
                            //    mail.sendmail(adress, "Test", "Test E-Mail");
                            //    Cursor = System.Windows.Input.Cursors.Arrow;
                            //}
                            //catch (Exception ex)
                            //{
                            //    Cursor = System.Windows.Input.Cursors.Arrow;
                            //    LoggingTool.LogExeption(ex, "Navigation.xaml.cs", "TreeView_SelectedItemChanged");
                            //}

                            var test = new Reports.RepViewer();
                            test.ShowDialog();


                            break;
                        }



                    //case "KalkulationstabelleDetail":
                    //    {

                    //        var p = new KalkulationDetails();
                    //        //LoadProjektliste("Projekt", "Projekttabelle", "AnlagenAuftraege", "");
                    //        NavigationService.Navigate(p);
                    //        break;
                    //    }
                    case "bvAngebote":
                        {
                            LoadProjektliste("Projekt", "Brunvoll Angebote", "Angebote", "it.FirmenNr  = 10014 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile')");
                            break;
                        }

                    case "bvAnlageAuftraege":
                        {

                            LoadProjektliste("Projekt", "Brunvoll AS Anlagen Aufträge", "bvAnlagenAuftraege", "it.FirmenNr  = 10014 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "bvAuftragsBestand":
                        {

                            LoadProjektliste("Projekt", "Brunvoll Auftragsbestand", "AuftragsBestand", "it.FirmenNr  = 10014 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "bvErsatzteilAuftraege":
                        {

                            LoadProjektliste("Projekt", "Brunvoll AS Ersatzteilaufträge", "bvAnlagenAuftraege", "it.FirmenNr  = 10014 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "bvServiceAuftraege":
                        {

                            LoadProjektliste("Projekt", "Brunvoll AS Serviceaufträge", "bvAnlagenAuftraege", "it.FirmenNr  = 10014 and (it.type = 'Service' or it.type = 'Ersatzteile') and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "bvVerlauf":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Brunvoll AS Projektverlauf", "bvAnlagenAuftraege", "it.FirmenNr  = 10014 and it.marker=1");
                            break;
                        }

                    case "bvReklamationen":
                        {

                            LoadProjektliste("Projekt", "Brunvoll AS Reklamationen", "bvAnlagenAuftraege", "it.FirmenNr  = 10014 and it.type = 'Reklamation'");
                            break;
                        }

                    //Jets Schiffe

                    case "jtAngebote":
                        {

                            LoadProjektliste("Projekt", "Jets AS Angebote - Schiffsanlagen", "jetAngebot", "it.FirmenNr  = 10177 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile') and it.schiff = 1");
                            break;
                        }

                    case "jtAnlageAuftraege":
                        {

                            LoadProjektliste("Projekt", "Jets AS  Anlagen Aufträge - Schiffsanlagen", "AnlagenAuftraege", "it.FirmenNr  = 10177 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1) and it.schiff = 1");
                            break;
                        }

                    case "jtAnlagenKommission":
                        {

                            LoadProjektliste("Projekt_ProjektRechnung", "Jets AS Kommission - Schiffsanlagen", "jetKommission", "it.FirmenNr  = 10177 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1) and it.schiff = 1", true);
                            break;
                        }

                    case "jtErsatzteilAuftraege":
                        {

                            LoadProjektliste("Projekt", "Jets AS Ersatzteilaufträge - Schiffsanlagen", "AnlagenAuftraege", "it.FirmenNr  = 10177 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1) and it.schiff = 1");
                            break;
                        }

                    case "jtErsatzteilKommission":
                        {
                            //LoadProjektliste("Projekt_ProjektRechnungKommission", "Jets AS Ersatzteil Kommission - Schiffsanlagen", "jetKommissionErsatzteile", "it.FirmenNr  = 10177 and it.type = 'Ersatzteile' and it.auftrag = 1 and (it.si = 0 or it.si IS NULL) and it.schiff = 1", true);

                            if (new Properties.Settings().JetsKommissionErsatzteileNeu == true)
                            {
                                LoadProjektliste("Projekt_ProjektRechnung", "Jets AS Ersatzteil Kommission - Schiffsanlagen", "jetKommissionErsatzteileNeu", "it.FirmenNr  = 10177 and it.type = 'Ersatzteile' and it.auftrag = 1 and (it.si = 0 or it.si IS NULL) and it.schiff = 1", true);
                            }
                            else
                            {
                                LoadProjektliste("Projekt_ProjektRechnung", "Jets AS Ersatzteil Kommission - Schiffsanlagen", "jetKommissionErsatzteile", "it.FirmenNr  = 10177 and it.type = 'Ersatzteile' and it.auftrag = 1 and (it.si = 0 or it.si IS NULL) and it.schiff = 1", true);
                            }



                            break;
                        }

                    case "jtVerlauf":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Jets AS Projektverlauf - Schiffsanlagen", "AlleAnlagenAuftraege", "it.FirmenNr  = 10177 and it.marker = 1 and it.schiff = 1");
                            break;
                        }

                    case "jtReklamationen":
                        {

                            LoadProjektliste("Projekt", "Jets AS Reklamationen - Schiffsanlagen", "AnlagenAuftraege", "it.FirmenNr  = 10177 and it.type = 'Reklamation' and it.schiff = 1");
                            break;
                        }


                    //Jets LAndanlagen

                    case "jtAngebote_Land":
                        {

                            LoadProjektliste("Projekt", "Jets AS Angebote - Landanlagen", "jetAngebot_Land", "it.FirmenNr  = 10177 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile') and it.landx = 1");
                            break;
                        }

                    case "jtAnlageAuftraege_Land":
                        {

                            LoadProjektliste("Projekt", "Jets AS  Anlagen Aufträge - Landanlagen", "AnlagenAuftraege_Land", "it.FirmenNr  = 10177 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1) and it.landx = 1");
                            break;
                        }

                    case "jtAnlagenKommission_Land":
                        {

                            LoadProjektliste("Projekt_ProjektRechnung", "Jets AS Kommission - Landanlagen", "jetKommission_Land", "it.FirmenNr  = 10177 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1) and it.landx = 1", true);
                            break;
                        }

                    case "jtErsatzteilAuftraege_Land":
                        {

                            LoadProjektliste("Projekt", "Jets AS Ersatzteilaufträge - Landanlagen", "AnlagenAuftraege_Land", "it.FirmenNr  = 10177 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1) and it.landx = 1");
                            break;
                        }

                    case "jtErsatzteilKommission_Land":
                        {


                            if (new Properties.Settings().JetsKommissionErsatzteileNeu == true)
                            {
                                LoadProjektliste("Projekt_ProjektRechnung", "Jets AS Ersatzteil Kommission - Schiffsanlagen", "jetKommissionErsatzteileNeu", "it.FirmenNr  = 10177 and it.type = 'Ersatzteile' and it.auftrag = 1 and (it.si = 0 or it.si IS NULL) and it.schiff = 1", true);
                            }
                            else
                            {
                                LoadProjektliste("Projekt_ProjektRechnung", "Jets AS Ersatzteil Kommission - Schiffsanlagen", "jetKommissionErsatzteile", "it.FirmenNr  = 10177 and it.type = 'Ersatzteile' and it.auftrag = 1 and (it.si = 0 or it.si IS NULL) and it.schiff = 1", true);
                            }


                            // LoadProjektliste("Projekt_ProjektRechnung", "Jets AS Ersatzteil Kommission - Landanlagen", "jetKommissionErsatzteile", "it.FirmenNr  = 10177 and it.type = 'Ersatzteile' and it.auftrag = 1 and (it.si = 0 or it.si IS NULL) and it.land = 1", true);
                            break;
                        }

                    case "jtVerlauf_Land":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Jets AS Projektverlauf - Landanlagen", "AlleAnlagenAuftraege_Land", "it.FirmenNr  = 10177 and it.marker = 1 and it.landx = 1");
                            break;
                        }

                    case "jtReklamationen_Land":
                        {

                            LoadProjektliste("Projekt", "Jets AS Reklamationen - Landanlagen", "AnlagenAuftraege_Land", "it.FirmenNr  = 10177 and it.type = 'Reklamation' and it.landx = 1");
                            break;
                        }

                    //Sperre AS

                    case "spAngebote":
                        {

                            LoadProjektliste("Projekt",   "Sperre Industri AS Angebote", "SperreAngebot",
                                "it.FirmenNr  = 10019 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile')");
                            break;
                        }

                    case "spAnlageAuftraege":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Anlagen Aufträge", "SperreAnlagenAuftraege", "it.FirmenNr  = 10019 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "spAnlagenKommission":
                        {

                            LoadProjektliste("Projekt_ProjektRechnung", "Sperre Industri AS Kommission", "SperreKommission", "it.FirmenNr  = 10019 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)", true);
                            break;
                        }

                    case "spErsatzteilAuftraege":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Ersatzteilaufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10019 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }


                    case "spVerlauf":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Sperre Industri AS Projektverlauf", "AlleAnlagenAuftraege", "it.FirmenNr  = 10019 and it.marker = 1");
                            break;
                        }

                    case "spReklamationen":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Reklamationen", "AlleAnlagenAuftraege", "it.FirmenNr  = 10019 and it.type = 'Reklamation'");
                            break;
                        }


                    //Tamrotor


                    case "tmcAngebote":
                        {

                            LoadProjektliste("Projekt", "Tamrotor Marine Compressors AS Angebote", "SperreAngebot", "it.FirmenNr  = 10016 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile')");
                            break;
                        }

                    case "tmcAnlageAuftraege":
                        {

                            LoadProjektliste("Projekt", "Tamrotor Marine Compressors AS Anlagen Aufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10016 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "tmcAnlagenKommission":
                        {

                            LoadProjektliste("Projekt_ProjektRechnung", "Tamrotor Marine Compressors AS Kommission", "SperreKommission", "it.FirmenNr  = 10016 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)", true);
                            break;
                        }

                    case "tmcErsatzteilAuftraege":
                        {

                            LoadProjektliste("Projekt", "Tamrotor Marine Compressors AS Ersatzteilaufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10016 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }


                    case "tmcVerlauf":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Tamrotor Marine Compressors AS Projektverlauf", "AlleAnlagenAuftraege", "it.FirmenNr  = 10016 and it.marker = 1");
                            break;
                        }

                    case "tmcReklamationen":
                        {

                            LoadProjektliste("Projekt", "Tamrotor Marine Compressors AS Reklamationen", "AlleAnlagenAuftraege", "it.FirmenNr  = 10016 and it.type = 'Reklamation'");
                            break;
                        }


                    //Finnoy


                    case "fiAngebote":
                        {

                            LoadProjektliste("Projekt", "Finnoy Gear and Propeller AS Angebote", "SperreAngebot", "it.FirmenNr  = 10095 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile')");
                            break;
                        }

                    case "fiAnlageAuftraege":
                        {

                            LoadProjektliste("Projekt", "Finnoy Gear and Propeller AS Anlagen Aufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10095 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "fiErsatzteilAuftraege":
                        {

                            LoadProjektliste("Projekt", "Finnoy Gear and Propeller AS Ersatzteilaufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10095 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }




                    case "fiVerlauf":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Finnoy Gear and Propeller AS Projektverlauf", "AlleAnlagenAuftraege", "it.FirmenNr  = 10095 and it.marker = 1");
                            break;
                        }

                    case "fiReklamationen":
                        {

                            LoadProjektliste("Projekt", "Finnoy Gear and Propeller AS Reklamationen", "AlleAnlagenAuftraege", "it.FirmenNr  = 10095 and it.type = 'Reklamation'");
                            break;
                        }


                    //Nyborg


                    case "nyAngebote":   //Nyborg AS
                        {

                            LoadProjektliste("Projekt", "Nyborg AS Angebote", "SperreAngebot", "it.FirmenNr  = 10164 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile')");
                            break;
                        }

                    case "nyAnlageAuftraege":
                        {

                            LoadProjektliste("Projekt", "Nyborg AS Anlagen Aufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10164 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "nyAnlagenKommission":
                        {

                            LoadProjektliste("Projekt_ProjektRechnung", "Nyborg AS Kommission", "SperreKommission", "it.FirmenNr  = 10164 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)", true);
                            break;
                        }

                    case "nyErsatzteilAuftraege":
                        {

                            LoadProjektliste("Projekt", "Nyborg AS Ersatzteilaufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10164 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }




                    case "nyVerlauf":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Nyborg AS Projektverlauf", "AlleAnlagenAuftraege", "it.FirmenNr  = 10164 and it.marker = 1");
                            break;
                        }

                    case "nyReklamationen":
                        {

                            LoadProjektliste("Projekt", "Finnoy Gear and Propeller AS Reklamationen", "AlleAnlagenAuftraege", "it.FirmenNr  = 10164 and it.type = 'Reklamation'");
                            break;
                        }



                    //Tecnicomar    Tecnicomar S.p.A.


                    case "tecAngebote":
                        {

                            LoadProjektliste("Projekt", "Tecnicomar S.p.A. Angebote", "SperreAngebot", "it.FirmenNr  = 10179 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile')");
                            break;
                        }

                    case "tecAnlageAuftraege":
                        {

                            LoadProjektliste("Projekt", "Tecnicomar S.p.A. Anlagen Aufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10179 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "tecAnlagenKommission":
                        {

                            LoadProjektliste("Projekt_ProjektRechnung", "Tecnicomar S.p.A.", "jetKommission", "it.FirmenNr  = 10179 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)", true);
                            break;
                        }


                    case "tecErsatzteilAuftraege":
                        {

                            LoadProjektliste("Projekt", "Tecnicomar S.p.A. Ersatzteilaufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10179 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "tecErsatzteilKommission":
                        {

                            LoadProjektliste("Projekt_ProjektRechnung", "Tecnicomar S.p.A. Ersatzteil Kommission", "jetKommissionErsatzteile", "it.FirmenNr  = 10179 and it.type = 'Ersatzteile' and it.auftrag = 1 and (it.si = 0 or it.si IS NULL)", true);
                            break;

                        }


                    case "tecVerlauf":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Tecnicomar S.p.A. Projektverlauf", "AlleAnlagenAuftraege", "it.FirmenNr  = 10179 and it.marker = 1");
                            break;
                        }

                    case "tecReklamationen":
                        {

                            LoadProjektliste("Projekt", "Tecnicomar S.p.A. Reklamationen", "AlleAnlagenAuftraege", "it.FirmenNr  = 10179 and it.type = 'Reklamation'");
                            break;
                        }




                    //Metizoft

                    case "mzAngebote":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Angebote", "SperreAngebot",
                                "it.FirmenNr  = 10688 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile')");
                            break;
                        }

                    case "mzAnlageAuftraege":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Anlagen Aufträge", "SperreAnlagenAuftraege", "it.FirmenNr  = 10688 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "mzAnlagenKommission":
                        {

                            LoadProjektliste("Projekt_ProjektRechnung", "Sperre Industri AS Kommission", "SperreKommission", "it.FirmenNr  = 10688 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)", true);
                            break;
                        }

                    case "mzErsatzteilAuftraege":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Ersatzteilaufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 10688 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }


                    case "mzVerlauf":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Sperre Industri AS Projektverlauf", "AlleAnlagenAuftraege", "it.FirmenNr  = 10688 and it.marker = 1");
                            break;
                        }

                    case "mzReklamationen":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Reklamationen", "AlleAnlagenAuftraege", "it.FirmenNr  = 10688 and it.type = 'Reklamation'");
                            break;
                        }



                    //MMC Green Technology

                    case "mmcAngebote":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Angebote", "SperreAngebot",
                                "it.FirmenNr  = 12022 and (it.si = 0 or it.si IS NULL) and (it.auftrag = 0 or it.auftrag IS NULL) and (it.type = 'Anlage' or it.type = 'Ersatzteile')");
                            break;
                        }

                    case "mmcAnlageAuftraege":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Anlagen Aufträge", "SperreAnlagenAuftraege", "it.FirmenNr  = 12022 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }

                    case "mmcAnlagenKommission":
                        {

                            LoadProjektliste("Projekt_ProjektRechnung", "Sperre Industri AS Kommission", "SperreKommission", "it.FirmenNr  = 12022 and it.type = 'Anlage' and (it.si = 1 or it.auftrag = 1)", true);
                            break;
                        }

                    case "mmcErsatzteilAuftraege":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Ersatzteilaufträge", "AlleAnlagenAuftraege", "it.FirmenNr  = 12022 and it.type = 'Ersatzteile' and (it.si = 1 or it.auftrag = 1)");
                            break;
                        }


                    case "mmcVerlauf":
                        {

                            LoadProjektliste("ProjektMitVerlauf", "Sperre Industri AS Projektverlauf", "AlleAnlagenAuftraege", "it.FirmenNr  = 12022 and it.marker = 1");
                            break;
                        }

                    case "mmcReklamationen":
                        {

                            LoadProjektliste("Projekt", "Sperre Industri AS Reklamationen", "AlleAnlagenAuftraege", "it.FirmenNr  = 12022 and it.type = 'Reklamation'");
                            break;
                        }




                        // Sonderprojekte

                    case "SonderprojekteAlle":
                        {
                            LoadProjektliste("Projekt", "Sonderprojekte","Sonderprojekte" , "it.FirmenNr  = 100");
                            break;
                        }


                    default:
                        Cursor = System.Windows.Input.Cursors.Arrow;
                        if (tvi.Tag.ToString() != "Gruppe")
                            MessageBox.Show("Diese Ansicht wurde nocht nicht implementiert");
                        break;
                }

                //Cursor = System.Windows.Input.Cursors.Arrow;


            }


        }

        #endregion


        #region "Animation"


        //void sliAnimationFrequency_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    timer.Interval = TimeSpan.FromMilliseconds((int)sliAnimationFrequency.Value);


        //}

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    // obsolete 30.08.2012

        //    //if (n++ >= imagesCount - 1)   //number of the images in the caroussel
        //    //    n = 1;

        //    //var pic = carouselListBox.Items[n] as BitmapImage;
        //    //carouselListBox.ScrollIntoView(pic);


        //}

        #endregion

        #region "Carousel"


        private void InitData()
        {

            // MessageBox.Show("Init started"); 
            //for (int i = 101; i <= 140; i++)
            //{

            //   string pfad = Extensions.GetAbsoluteUri(String.Format("/Resources/covers/cover{0}.jpg", i)).ToString();
            //   pfad = pfad.Replace ("/Home","");
            //    Uri ur = new Uri(pfad);
            //    //carouselListBox.Items.Add(new BitmapImage(Extensions.GetAbsoluteUri(String.Format("/Resources/covers/cover{0}.jpg", i))));
            //    carouselListBox.Items.Add(new BitmapImage(ur));


            //    bool x = System.IO.File.Exists(pfad);
            //}


            #region "Carousselcode


            // obsolete 30.08.2012

            //var pics = new string[] { "nyborg.jpg","nyborg_equip1.jpg","nyborg_equip2.jpg" ,"jets_vacuum.jpg","jets_equip1.jpg","jets_equip2.jpg",
            //    "sperre.jpg","sperre_Equip1.jpg","sperre_Equip2.jpg", "finnoy.jpg","finnoy_Equip1.jpg","finnoy_Equip2.jpg", 
            //    "brunvoll1.jpg","bv_Equip1.jpg","bv_Equip2.jpg", "tmc.jpg","tmc_equip1.gif","tmc_equip2.gif","tecnicomar.tif","nyborg.jpg","nyborg_equip1.jpg","nyborg_equip2.jpg" ,"jets_vacuum.jpg","jets_equip1.jpg","jets_equip2.jpg",
            //    "sperre.jpg","sperre_Equip1.jpg","sperre_Equip2.jpg", "finnoy.jpg","finnoy_Equip1.jpg","finnoy_Equip2.jpg", 
            //    "brunvoll1.jpg","bv_Equip1.jpg","bv_Equip2.jpg", "tmc.jpg","tmc_equip1.gif","tmc_equip2.gif","tecnicomar.tif","nyborg.jpg","nyborg_equip1.jpg","nyborg_equip2.jpg" ,"jets_vacuum.jpg","jets_equip1.jpg","jets_equip2.jpg",
            //    "sperre.jpg","sperre_Equip1.jpg","sperre_Equip2.jpg", "finnoy.jpg","finnoy_Equip1.jpg","finnoy_Equip2.jpg", 
            //    "brunvoll.jpg","bv_Equip1.jpg","bv_Equip2.jpg", "tmc.jpg","tmc_equip1.gif","tmc_equip2.gif"
            //                           };


            //foreach (string s in pics)
            //{
            //    //string buf = i.ToString();
            //    // buf = buf.PadLeft(2, '0');
            //    string datei = String.Format("/Images/{0}", s);

            //    var bmp = new BitmapImage(new Uri(datei, UriKind.Relative));
            //    //obsolete 30.08.2012
            //    //carouselListBox.Items.Add(bmp);

            //}





        }


        // Animationspeed for Caroussel
        //private void chkAnimate_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (timer == null)
        //    {
        //        timer = new DispatcherTimer();
        //    }

        //    sliAnimationFrequency.Visibility = Visibility.Visible;
        //    timer.Start();



        //}

        //private void chkAnimate_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    if (timer == null)
        //    {
        //        timer = new DispatcherTimer();
        //    }
        //    sliAnimationFrequency.Visibility = Visibility.Hidden;
        //    timer.Stop();
        //}

            #endregion


        #endregion


        #region "Auskommentierter Code - Interessehalber"

        private void ProjekteItem_GotFocus(object sender, EventArgs e)
        {
            //var item = (ListViewItem)sender;
            //this.ProjekteView.SelectedItem = item.DataContext;
        }

        private void tviAllgemein_Selected(object sender, RoutedEventArgs e)
        {

        }

        //private void RunProjekte_Click(object sender, RoutedEventArgs e)
        //{
        //    //ShowProjekte();
        //    var dg = new Datagrid();
        //    this.NavigationService.Navigate(dg);

        //}

        //private void RunProjekt_Click(object sender, RoutedEventArgs e)
        //{
        //    projekt p = (projekt)ProjekteView.CurrentItem;
        //    MainWindow mw = new MainWindow(p.id);
        //    this.NavigationService.Navigate(mw);
        //}

        //private void hlbvAuftragsbestand_Click(object sender, RoutedEventArgs e)
        //{
        //    var p = new Projektliste("BrunvollView", "Brunvoll Auftragsbestand", "Angebote", "it.FirmenNr  = 10014");
        //    NavigationService.Navigate(p);
        //}



        //private void TreeView_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    MessageBox.Show("SelectedItemChanged");
        //}

        #endregion

    }
}
