using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using ProjektDB.Repositories;
using DAL;
using DAL.Tools;
using ProjektDB.Tools;
using ProjektDB.views;
using System.Collections;
using ProjektDB.Format;
using CommonTools.Tools;
using System.ComponentModel;
using AutoMapper;


namespace ProjektDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xamlF
    /// </summary>
    /// 
    public partial class MainWindow : Page
    {

        #region "Declarations"
        private int CurrentProjektID;
        private projekt CurrentProjekt;
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
        //private CollectionViewSource RechnungenViewSource;
        //private BindingListCollectionView RechnungenView;
        private CollectionViewSource FirmaLookupViewSource;
        private CollectionViewSource KundenLookupViewSource;


        private CollectionViewSource AnlagenLieferzeitViewSource;
        private BindingListCollectionView AnlagenLieferzeitView;
        private CollectionViewSource ProjektRechnungViewSource;
        private BindingListCollectionView ProjektRechnungView;
        private CollectionViewSource AusgangsRechnungenViewSource;
        private BindingListCollectionView AusgangsRechnungenView;
        private CollectionViewSource ErsatzteileGutschriftViewSource;
        private BindingListCollectionView ErsatzteileGutschriftView;
        private CollectionViewSource LieferantenRechnungenViewSource;
        private BindingListCollectionView LieferantenRechnungenView;
        private CollectionViewSource LieferantenGutschriftenViewSource;
        private BindingListCollectionView LieferantenGutschriftenView;
        private CollectionViewSource AusgangsGutschriftenViewSource;
        private BindingListCollectionView AusgangsGutschriftenView;



        private CollectionViewSource ProjektRechnungenKundeViewSource;
        private BindingListCollectionView ProjektRechnungenKundeView;

        private CollectionViewSource ProjektGutschriftenKundeViewSource;
        private BindingListCollectionView ProjektGutschriftenKundeView;

        private CollectionViewSource ProjektRechnungenLieferantViewSource;
        private BindingListCollectionView ProjektRechnungenLieferantView;

        private CollectionViewSource ProjektGutschriftenLieferantViewSource;
        private BindingListCollectionView ProjektGutschriftenLieferantView;


        private CollectionViewSource SI_RgKundeViewSource;
        private BindingListCollectionView SI_RgKundeView;

        private CollectionViewSource SI_RgLieferantViewSource;
        private BindingListCollectionView SI_RgLieferantView;

        private CollectionViewSource SI_GSKundeViewSource;
        private BindingListCollectionView SI_GSKundeView;

        private CollectionViewSource SI_GSLieferantViewSource;
        private BindingListCollectionView SI_GSLieferantView;

        private CollectionViewSource AggregateViewSource;
        private BindingListCollectionView AggregateView;

        private CollectionViewSource KalkulationViewSource;
        private ListCollectionView KalkulationView;

        private IEnumerable<DAL.kalkulationstabelle> calcQuery;

        private CollectionViewSource LieferadressenLookUp;
        private CollectionViewSource KostenstellenLookUp;

        private CollectionViewSource SpracheLookUp;


        private System.Linq.IQueryable ArtikelQuery;

        internal bool AreChangesHandled = false;
        NavigationService ns;

        bool bNoClick = false;





        #endregion

        #region "Main and Load"

        //private void LoadLieferadresse()
        //{

        //    Firmen_Adressen la = null;



        //    if (CurrentProjekt != null)
        //    {
        //        if (CurrentProjekt.id_Lieferadresse == null || CurrentProjekt.id_Lieferadresse == 0)
        //        {
        //            la = new Firmen_Adressen();
        //            db.AddToFirmen_Adressen(la);
        //            la.Typ = 4;
        //            CurrentProjekt.Firmen_Adressen = la;

        //        }
        //        else
        //        {
        //            la = db.Firmen_Adressen.Where(n => n.id == CurrentProjekt.id_Lieferadresse).SingleOrDefault();
        //        }

        //        Lieferadresse = new CommonTools.ViewModels.AdressenViewModel();
        //        Mapper.CreateMap<Firmen_Adressen, CommonTools.ViewModels.AdressenViewModel>();
        //        ////Mapper.AssertConfigurationIsValid(); 
        //        Mapper.Map<Firmen_Adressen, CommonTools.ViewModels.AdressenViewModel>(la, Lieferadresse);
        //        Lieferadresse.isDirty = false;
        //    }


        //}

        //private void SaveLieferadresse()
        //{
        //    if (CurrentProjekt != null)
        //    {
        //        if (Lieferadresse != null)
        //        {

        //            if (Lieferadresse.isDirty)
        //            {
        //                Mapper.CreateMap<CommonTools.ViewModels.AdressenViewModel, Firmen_Adressen>();
        //                Mapper.Map<CommonTools.ViewModels.AdressenViewModel, Firmen_Adressen>(Lieferadresse, CurrentProjekt.Firmen_Adressen);
        //                Lieferadresse.isDirty = false;
        //            }
        //        }


        //    }

        //}


        private void FillProjekt(int ProjektID)
        {
            try
            {
                CurrentProjektID = ProjektID;
                //db = new SteinbachEntities();
                ProjektViewSource = ((CollectionViewSource)(this.FindResource("Projekt_ViewSource")));
                var ProjektQuery = ProjektRepo.GetProjekteByID(ProjektID);
                ProjektViewSource.Source = ProjektQuery;
                ProjektView = (ListCollectionView)ProjektViewSource.View;

                SetCurrentProjekt();


                FirmaLookupViewSource = ((CollectionViewSource)(this.FindResource("FirmaLookup")));
                FirmaLookupViewSource.Source = ProjektRepo.GetFirmen();

                KundenLookupViewSource = ((CollectionViewSource)(this.FindResource("KundenLookup")));
                KundenLookupViewSource.Source = ProjektRepo.GetKunden();

                //var CalcLookUp = ((System.Windows.Data.CollectionViewSource)(this.FindResource("KalkulationLookUp")));
                //calcQuery = ProjektRepo.GetCalculationType(this.CurrentProjektID);
                //CalcLookUp.Source = calcQuery;
                KalkulationViewSource = ((CollectionViewSource)(this.FindResource("KalkulationLookUp")));
                KalkulationViewSource.Source = ProjektRepo.GetCalculationType(this.CurrentProjektID);
                KalkulationView = (ListCollectionView)(KalkulationViewSource.View);


                var LagerListenLookUp = ((CollectionViewSource)(this.FindResource("LagerlistenLookUp")));
                ArtikelQuery = (IQueryable)ProjektRepo.GetLagerlistenBewegung(ProjektRepo.GetProjektNumberFromID(this.CurrentProjektID));
                if (ArtikelQuery == null)
                {

                    LagerListenLookUp = null;
                }
                else
                {
                    LagerListenLookUp.Source = ArtikelQuery;
                    ///
                }

                var WaehrungenLookUp = ((CollectionViewSource)(this.FindResource("WaehrungenLookUp")));
                var curQuery = from c in db.StammWaehrungen select c;
                var cQ = db.StammWaehrungen;
                WaehrungenLookUp.Source = curQuery;


                var AggLookup = ((CollectionViewSource)(this.FindResource("AggregateLookup")));
                var aquery = from a in db.Stamm_Aggregate
                             select a;
                AggLookup.Source = aquery;

                var UrLookup = ((CollectionViewSource)(this.FindResource("UrsprungLookUp")));
                UrLookup.Source = ProjektRepo.GetUntergeordneteProjekte(ProjektID);

                //var RekLookup = ((CollectionViewSource)(this.FindResource("ReklamationLookUp")));
                //RekLookup.Source = ProjektRepo.GetReklamationen(ProjektID);

                var schiffeLookUp = ((CollectionViewSource)(this.FindResource("SchiffLookup")));
                schiffeLookUp.Source = ProjektRepo.GetSchiff();
                ftbSchiff.cBox.ItemsSource = RefreshShippComboBox(string.Empty);
                ftbSchiff.SetSimpleBinding("projekt_schiff", "name", "name", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);

                fcbKunde.cBox.ItemsSource = RefreshKundeComboBox(string.Empty);
                fcbKunde.SetSimpleBinding("KdNr", "KdNr", "name", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);

                LieferadressenLookUp = ((CollectionViewSource)(this.FindResource("LieferadressenLookUp")));
                LieferadressenLookUp.Source = RefreshLieferadresseComboBox(string.Empty);
                
                SpracheLookUp = ((CollectionViewSource)(this.FindResource("SpracheLookUp")));
                SpracheLookUp.Source = db.AuswahlEintraege.Where(n => n.Gruppe == "TypSprache");

                KostenstellenLookUp = ((CollectionViewSource)(this.FindResource("KostenstellenLookUp")));
                if (CurrentProjekt != null)
                {
                    var fa = db.firmen.Where(n => n.KdNr == CurrentProjekt.FirmenNr).SingleOrDefault();

                    SI_KostenstellenFirmen KSFirmen = null;



                    if (CurrentProjekt.FirmenNr == 10177)
                    {


                        if (CurrentProjekt.schiff == 1 && (!CurrentProjekt.landx.HasValue || CurrentProjekt.landx == 0))
                        {
                            KSFirmen = db.SI_KostenstellenFirmen.Where((n => n.id_Firma == fa.id && n.Schiff == 1)).SingleOrDefault();
                        }
                        else if (CurrentProjekt.landx == 1 && (!CurrentProjekt.schiff.HasValue || CurrentProjekt.schiff == 0))
                        {
                            KSFirmen = db.SI_KostenstellenFirmen.Where(n => n.id_Firma == fa.id && n.Land == 1).SingleOrDefault();
                        }
                        else
                        {
                            KSFirmen = null;
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("Kennzeichen Land oder Schiff sind nicht eindeutig gesetzt.");
                            sb.AppendLine("Kostenstellen konnten daher nicht ermittelt werden.");
                            sb.AppendLine("Bitte Kennzeichen setzen, Projekt speichern, schließen und erneut öffnen");
                            CommonTools.Tools.UserMessage.NotifyUser(sb.ToString());
                        }

                    }
                    else
                    {
                        KSFirmen = db.SI_KostenstellenFirmen.Where(n => n.id_Firma == fa.id).SingleOrDefault();
                    }


                    if (KSFirmen != null)
                    {

                        KostenstellenLookUp.Source = db.SI_Kostenstellen.Where(n => n.id_KostenstellenFirmen == KSFirmen.id || n.id_KostenstellenFirmen == 99);

                    }

                }






                var TypeLookUp = ((CollectionViewSource)(this.FindResource("TypLookUp")));
                TypeLookUp.Source = ProjektRepo.GetProjektTyp();

                AggregateViewSource = ((CollectionViewSource)(this.FindResource("Aggregate_ViewSource")));
                AggregateView = (BindingListCollectionView)(AggregateViewSource.View);

                VerlaufViewSource = ((CollectionViewSource)(this.FindResource("ProjektVerlauf_ViewSource")));
                VerlaufView = (BindingListCollectionView)(VerlaufViewSource.View);

                AnlagenTypViewSource = ((CollectionViewSource)(this.FindResource("AnlagenTyp_ViewSource")));
                AnlagenTypView = (BindingListCollectionView)(AnlagenTypViewSource.View);

                //RechnungenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Rechnungen_ViewSource")));
                //RechnungenView = (BindingListCollectionView)(RechnungenViewSource.View);

                AnlagenLieferzeitViewSource = ((CollectionViewSource)(this.FindResource("AnlagenLieferzeit_ViewSource")));
                AnlagenLieferzeitView = (BindingListCollectionView)(AnlagenLieferzeitViewSource.View);

                ProjektRechnungViewSource = ((CollectionViewSource)(this.FindResource("ProjektRechnung_ViewSource")));
                ProjektRechnungView = (BindingListCollectionView)(ProjektRechnungViewSource.View);

                AusgangsRechnungenViewSource = ((CollectionViewSource)(this.FindResource("AusgangsRechnungen_ViewSource")));
                AusgangsRechnungenView = (BindingListCollectionView)(AusgangsRechnungenViewSource.View);

                ErsatzteileGutschriftViewSource = ((CollectionViewSource)(this.FindResource("ErsatzteileGutschrift_ViewSource")));
                ErsatzteileGutschriftView = (BindingListCollectionView)(ErsatzteileGutschriftViewSource.View);

                LieferantenRechnungenViewSource = ((CollectionViewSource)(this.FindResource("LieferantenRechnungen_ViewSource")));
                LieferantenRechnungenView = (BindingListCollectionView)(LieferantenRechnungenViewSource.View);

                LieferantenGutschriftenViewSource = ((CollectionViewSource)(this.FindResource("LieferantenGutschriften_ViewSource")));
                LieferantenGutschriftenView = (BindingListCollectionView)(LieferantenGutschriftenViewSource.View);

                AusgangsGutschriftenViewSource = ((CollectionViewSource)(this.FindResource("AusgangsGutschriften_ViewSource")));
                AusgangsGutschriftenView = (BindingListCollectionView)(AusgangsGutschriftenViewSource.View);

                SI_RgKundeViewSource = ((CollectionViewSource)(this.FindResource("SI_RgKunde_ViewSource")));
                SI_RgKundeView = (BindingListCollectionView)(SI_RgKundeViewSource.View);

                SI_GSKundeViewSource = ((CollectionViewSource)(this.FindResource("SI_GSKunde_ViewSource")));
                SI_GSKundeView = (BindingListCollectionView)(SI_GSKundeViewSource.View);

                SI_RgLieferantViewSource = ((CollectionViewSource)(this.FindResource("SI_RgLieferant_ViewSource")));
                SI_RgLieferantView = (BindingListCollectionView)(SI_RgLieferantViewSource.View);

                SI_GSLieferantViewSource = ((CollectionViewSource)(this.FindResource("SI_GSLieferant_ViewSource")));
                SI_GSLieferantView = (BindingListCollectionView)(SI_GSLieferantViewSource.View);

                ProjektRechnungenKundeViewSource = ((CollectionViewSource)(this.FindResource("Projekt_RechnungenKunde")));
                ProjektRechnungenKundeView = (BindingListCollectionView)(ProjektRechnungenKundeViewSource.View);

                ProjektGutschriftenKundeViewSource = ((CollectionViewSource)(this.FindResource("Projekt_GutschriftenKunde")));
                ProjektGutschriftenKundeView = (BindingListCollectionView)(ProjektGutschriftenKundeViewSource.View);

                ProjektRechnungenLieferantViewSource = ((CollectionViewSource)(this.FindResource("Projekt_RechnungenLieferant")));
                ProjektRechnungenLieferantView = (BindingListCollectionView)(ProjektRechnungenLieferantViewSource.View);

                ProjektGutschriftenLieferantViewSource = ((CollectionViewSource)(this.FindResource("Projekt_GutschriftenLieferant")));
                ProjektGutschriftenLieferantView = (BindingListCollectionView)(ProjektGutschriftenLieferantViewSource.View);


                WriteProjektItemHeaders(ProjektID);

                var x = DAL.Session.ProjekteAufklappen;

                ExpandProjekt(x);
                chkExpandProject.IsChecked = x;

                //CommonTools.ViewModels.AdressenViewModel la = new CommonTools.ViewModels.AdressenViewModel();
                //la.Postfach = "23879";
                //la.Straße = "WKW";
                //la.Ort = "Mölln";
                //Lieferadresse = la;

                //LoadLieferadresse();

                ProjektView.CurrentChanged += new EventHandler(ProjektView_CurrentChanged);

            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
            }






        }

        private void SetCurrentProjekt()
        {
            if (CurrentProjektID != 0)
            {
                var p = (projekt)this.ProjektView.CurrentItem;
                // this.CurrentProjektID = p.id;
                this.CurrentProjekt = p;
            }
        }

        private void WriteProjektItemHeaders(int ProjektID)
        {
            if (ProjektID > 0)
            {
                this.ExpAnlagenTyp.Header = "AnlagenTyp : " + AnlagenTypView.Count.ToString();
                this.ExpProjektverlauf.Header = "Projektverlauf : " + VerlaufView.Count;



                this.ExpAusgangsGutschriften.Header = "Ausgangsgutschriften : " + AusgangsGutschriftenView.Count;
                this.ExpAusgangsRechnungen.Header = "Ausgangsrechnungen : " + AusgangsRechnungenView.Count;
                this.ExpErsatzteileGutschrift.Header = "Ersatzteilegutschriften : " + ErsatzteileGutschriftView.Count;
                this.ExpKomponenten.Header = "Anlagen Lieferzeit : " + AnlagenLieferzeitView.Count;
                this.ExpLieferantenRechnungen.Header = "Lieferantenrechnungen : " + LieferantenRechnungenView.Count;
                this.ExpLieferantenGutschriften.Header = "Lieferantengutschriften : " + LieferantenGutschriftenView.Count;
                this.ExpErsatzteileGutschrift.Header = "Gutschriften : " + ErsatzteileGutschriftView.Count;
                this.ExpProjektRechnung.Header = " Rechnungen : " + ProjektRechnungView.Count;

                this.ExpSI_GSKunde.Header = "SI Gutschriften an Kunden : " + SI_GSKundeView.Count;
                this.ExpSI_RgKunde.Header = "SI Ausgangsrechnungen : " + SI_RgKundeView.Count;
                this.ExpSI_GSLieferant.Header = "SI Gutschriften an Lieferanten : " + SI_GSLieferantView.Count;
                this.ExpSI_RGLieferant.Header = "SI Lieferantenrechnungen  : " + SI_RgLieferantView.Count;
                //this.expKalkulationstabellen.Header = "Kalkulationstabellen : " + calcQuery.Count();
                this.expKalkulationstabellen.Header = "Kalkulationstabellen : " + KalkulationView.Count;
                this.Exp_ProjektRechnungKunde.Header = "Rechnungen Kunde : " + ProjektRechnungenKundeView.Count;
                this.Exp_ProjektGutschriftKunde.Header = "Gutschriften Kunde : " + ProjektGutschriftenKundeView.Count;
                this.Exp_ProjektRechnungLieferant.Header = "Rechnungen Lieferant : " + ProjektRechnungenLieferantView.Count;
                this.Exp_ProjektGutschriftLieferant.Header = "Gutschriften Lieferant : " + ProjektGutschriftenLieferantView.Count;


                if (ArtikelQuery == null)
                {
                    this.expLagerlisten.Header = "Lagerlisten : 0";
                }
                else
                {
                    this.expLagerlisten.Header = "Lagerlisten : " + ArtikelQuery.Count();
                }



            }
        }


        public MainWindow()
        {
            CurrentProjektID = Session.CurrentProjectID;
            InitializeComponent();
        }

        public MainWindow(int ProjID)
        {
            CurrentProjektID = ProjID;
            InitializeComponent();
            //InitializeComponent();
            //this.Loaded += delegate { NavigationService.Navigating += new NavigatingCancelEventHandler(NavigationService_Navigating); };
            //this.Unloaded += delegate { NavigationService.Navigating -= new NavigatingCancelEventHandler(NavigationService_Navigating); }; 

            //RoutedEventHandler CancelNavigationPage_Loaded = null;


            //this.Loaded += new RoutedEventHandler(CancelNavigationPage_Loaded);
            //this.Unloaded += new RoutedEventHandler(CancelNavigationPage_Unloaded);


            //Application.Current.Navigating += new NavigatingCancelEventHandler(Current_Navigating);




        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            db = new SteinbachEntities();
            db.SavingChanges += new EventHandler(db_SavingChanges);
            ProjektRepo = new ProjektRepository(db);
            VerlaufRepo = new VerlaufRepository(db);
            //ns = this.NavigationService;




            //var x = new Properties.Settings();
            //var x = DAL.Session.ProjekteAufklappen;

            //ExpandProjekt(x);
            //chkExpandProject.IsChecked = x;

            if (CurrentProjektID != 0)
                FillProjekt(CurrentProjektID);
            else
            {
                FillProjekt(0);
                addNewProjekt();
            }

            try
            {
                FormatMainWindow.SetSI((bool)chkSI.IsChecked, (bool)chkAuftrag.IsChecked, this, false);
                //FormatMainWindow.SetFirma(Firma_FCB_X.cBox.Text, this);
                FormatMainWindow.SetFirma(Firma_FCB_X.SelectedValue == null ? 0 : (int)Firma_FCB_X.SelectedValue, this);

                FormatMainWindow.SetType(this.cboType.Text, this);
                // FormatMainWindow.SetAngebot((DateTime)dtpDatum.SelectedDate, this);
                //FormatMainWindow.SetAngebot((DateTime)dtpDatum.SelectedDate, this);
                FormatMainWindow.SetAngebot((DateTime)dtpDatum.DateTime, this);
                FormatMainWindow.SetAuftragErsatzteil(cboType.Text, this);
                FormatMainWindow.SetJetsLandanlagen((bool)rbLand.IsChecked, this);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));
            }



            // cboKunde.ItemConverter = new CustomerConverter();

        }

        #endregion




        #region "Navigation_And Save"

        private void btnSaveInitData_Click(object sender, RoutedEventArgs e)
        {
            if (ManageChanges(true))
            {

                var p = (projekt)this.ProjektView.CurrentItem;
                this.CurrentProjektID = p.id;
                FillProjekt(p.id);
            }


        }



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Projekt schließen und alle Änderungen verwerfen?", "Sicherheitsabfrage", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                AreChangesHandled = true;
                this.NavigationService.GoBack();
            }

        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ManageChanges(true))
                {
                    if (this.CurrentProjektID == 0)
                    {
                        var p = (projekt)this.ProjektView.CurrentItem;
                        this.CurrentProjektID = p.id;
                        this.CurrentProjekt = p;
                        FillProjekt(p.id);
                    }

                }


            }
            catch (Exception ex)
            {

                LoggingTool.LogExeption(ex, "MainWindow", "Save_Click");
            }

        }

        void db_SavingChanges(object sender, EventArgs e)
        {


            var sb = new StringBuilder();
            var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted | System.Data.EntityState.Unchanged);



            foreach (var o in om)
            {

                if (o.Entity is projekt)
                {

                    var p = (projekt)o.Entity;
                    if (p.EntityState != System.Data.EntityState.Deleted)
                    {
                        p.created = DateTime.Now;
                        this.CurrentProjektID = p.id;
                    }
                }

                if (o.Entity is projekt_verlauf && o.State == System.Data.EntityState.Modified)
                {
                    var v = (projekt_verlauf)o.Entity;
                    if (v.EntityState != System.Data.EntityState.Deleted)
                    {
                        v.id_personchange = Session.User.id;
                    }
                }

                if (o.Entity is projekt_anlage_lieferzeit && (o.State == System.Data.EntityState.Modified || o.State == System.Data.EntityState.Added))
                {
                    var v = (projekt_anlage_lieferzeit)o.Entity;
                    if (v.EntityState != System.Data.EntityState.Deleted)
                    {
                        v.id_personchange = Session.User.id;

                    }

                }

            }



        }


        #endregion






        #region "CheckAndChange"



        public bool CheckForChanges()
        {
            var osm = db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);

            foreach (var ose in osm)
            {
                var res = ose.GetModifiedProperties();
                foreach (var item in res)
                {
                    Console.WriteLine(item);
                }

                if (ose.State != System.Data.EntityState.Unchanged)
                {
                    return true;
                }
            }
            return false;


        }

        public bool ManageChanges(bool changeAnyway = false)
        {

            try
            {
                if (changeAnyway)                   // wenn flag 'Immer Speichern' = true
                {
                    if (!RequiredDataSet(true))     //  Mindestfelder nicht gesetzt 
                    {
                        return false;             //  - Rückmeldung - No Success
                    }

                    // SaveLieferadresse();
                    db.SaveChanges();          // sonst - speichern  


                    return true;                // Rückmeldung - Success
                }



                if (!CheckForChanges())         // Daten unverändert
                {
                    return true;               //  : Rückmeldung - Success
                }




                var res = MessageBox.Show("Anderungen im Projekt speichern?", "Sicherheitsabfrage", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)    // Benutzer bestätigt speichern.
                {
                    if (!RequiredDataSet())         // //  Mindestfelder nicht gesetzt
                    {
                        return false;               // //  - Rückmeldung - No Success
                    }
                    //SaveLieferadresse();
                    db.SaveChanges();               //  // sonst - speichern  

                    return true;                    // Rückmeldung - Success
                }

                else if (res == MessageBoxResult.No)    // Benutzer verneint speichern.
                {

                    return true;                        // Rückmeldung - Success
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {
                int hr = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                if (hr == -2146233087)
                {
                    CommonTools.Tools.ErrorMethods.ShowErrorMessage("Eingegebene Zeichenfolge ist zu lang zum speichern. Bitte korrigieren");
                }
                else
                {
                    CommonTools.Tools.ErrorMethods.HandleStandardError(ex);

                }

            }

            return false;
        }







        bool RequiredDataSet(bool TellUser = true)
        {

            if (checkDoubleProjektNumber())
            {
                return false;                      // Doppelte Projektnummer - No Success
            }

            var fi = (firma)Firma_FCB_X.SelectedItem;
            //if (Firma_FCB_X.Text.ToLower() == "jets as" && rbLand.IsChecked == false && rbSchiff.IsChecked == false)
            if (fi != null)
            {
                if (fi.KdNr == 10177 && rbLand.IsChecked == false && rbSchiff.IsChecked == false)
                {
                    if (TellUser)
                    {
                        MessageBox.Show("Für Jets AS Projekte muß Land oder Schiff ausgewählt werden.");
                    }

                    return false;
                }
            }




            if (Firma_FCB_X.Text == string.Empty || cboType.Text == string.Empty || txtProjektnummer.Text == string.Empty)
            {
                StringBuilder sb = new StringBuilder();
                if (TellUser)
                {
                    sb.AppendLine("Es wurden nicht alle erforderlichen Daten zum Speichern des Projektes eingegeben.");
                    sb.AppendLine();
                    sb.AppendLine("Notwendig sind mindestens Firma, Projekttyp, und Projektnummer");
                    sb.AppendLine();
                    sb.AppendLine("Bitte Daten ergänzen oder Projekt abbrechen.");

                    MessageBox.Show(sb.ToString());

                }
                return false;
            }
            return true;


        }

        bool checkDoubleProjektNumber()
        {

            using (var db = new SteinbachEntities())
            {
                if (CurrentProjektID == 0)
                {
                    var res = db.projekte.Where(n => n.projektnummer == txtProjektnummer.Text).FirstOrDefault();
                    if (res != null)
                    {
                        if (MessageBox.Show("Speichern nicht möglich. Die Projektnummer ist schon vergeben. Neue Projektnummer erzeugen ?", "Plausibilitätsprüfung", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                        {

                            this.txtProjektnummer.Text = new ProjektRepository(db).GetNewProjektnummerWithYear();
                            if (checkDoubleProjektNumber())
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
                else
                {
                    return false;
                }

            }


        }

        #endregion



        #region "Projekt"

        void ProjektView_CurrentChanged(object sender, EventArgs e)
        {
            VerlaufView = (BindingListCollectionView)(VerlaufViewSource.View);
            AnlagenTypView = (BindingListCollectionView)(AnlagenTypViewSource.View);
            //RechnungenView = (BindingListCollectionView)(RechnungenViewSource.View);
            AnlagenLieferzeitView = (BindingListCollectionView)(AnlagenLieferzeitViewSource.View);
            ProjektRechnungView = (BindingListCollectionView)(ProjektRechnungViewSource.View);
            AusgangsRechnungenView = (BindingListCollectionView)(AusgangsRechnungenViewSource.View);
            ErsatzteileGutschriftView = (BindingListCollectionView)(ErsatzteileGutschriftViewSource.View);
            LieferantenRechnungenView = (BindingListCollectionView)(LieferantenRechnungenViewSource.View);
            LieferantenGutschriftenView = (BindingListCollectionView)(LieferantenGutschriftenViewSource.View);
            AusgangsGutschriftenView = (BindingListCollectionView)(AusgangsGutschriftenViewSource.View);

            AggregateView = (BindingListCollectionView)(AggregateViewSource.View);
            ////projekt p = (projekt)ProjektView.CurrentItem;
            ////if (p != null && !p.projekt_verlauf.IsLoaded)
            ////   p.projekt_verlauf.Load();



        }


        void ExpandProjekt(bool state)
        {
            if (state == false)
            {
                ExpAnlagenTyp.IsExpanded = state;
                ExpProjektverlauf.IsExpanded = state;
                this.ExpAusgangsRechnungen.IsExpanded = state;
                this.ExpAnlagenTyp.IsExpanded = state;
                this.ExpAuftrag.IsExpanded = state;
                this.ExpAusgangsGutschriften.IsExpanded = state;
                this.ExpAusgangsRechnungen.IsExpanded = state;
                this.ExpErsatzteileGutschrift.IsExpanded = state;
                this.ExpKomponenten.IsExpanded = state;
                this.ExpLieferantenRechnungen.IsExpanded = state;
                this.ExpLieferantenGutschriften.IsExpanded = state;
                this.ExpErsatzteileGutschrift.IsExpanded = state;
                this.ExpProjektRechnung.IsExpanded = state;
                this.ExpSI_RGLieferant.IsExpanded = state;
                this.ExpSI_RgKunde.IsExpanded = state;
                this.ExpSI_GSLieferant.IsExpanded = state;
                this.ExpSI_GSKunde.IsExpanded = state;
                this.ExpSIAuftrag.IsExpanded = state;
                this.ExpService.IsExpanded = state;



                this.expKalkulationstabellen.IsExpanded = false;
                this.expLagerlisten.IsExpanded = false;
            }
            else
            {
                if (AnlagenTypView != null)
                {
                    this.ExpAnlagenTyp.IsExpanded = AnlagenTypView.Count > 0 ? state : false;
                    this.ExpProjektverlauf.IsExpanded = VerlaufView.Count > 0 ? state : false;
                    this.ExpAusgangsRechnungen.IsExpanded = AusgangsRechnungenView.Count > 0 ? state : false;
                    this.ExpAusgangsGutschriften.IsExpanded = AusgangsGutschriftenView.Count > 0 ? state : false;
                    this.ExpAusgangsRechnungen.IsExpanded = AusgangsRechnungenView.Count > 0 ? state : false;
                    this.ExpErsatzteileGutschrift.IsExpanded = ErsatzteileGutschriftView.Count > 0 ? state : false;
                    this.ExpKomponenten.IsExpanded = AnlagenLieferzeitView.Count > 0 ? state : false;
                    this.ExpLieferantenRechnungen.IsExpanded = LieferantenRechnungenView.Count > 0 ? state : false;
                    this.ExpLieferantenGutschriften.IsExpanded = LieferantenGutschriftenView.Count > 0 ? state : false;
                    this.ExpErsatzteileGutschrift.IsExpanded = ErsatzteileGutschriftView.Count > 0 ? state : false;
                    this.ExpProjektRechnung.IsExpanded = ProjektRechnungView.Count > 0 ? state : false;
                    this.ExpSI_RGLieferant.IsExpanded = SI_RgLieferantView.Count > 0 ? state : false;
                    this.ExpSI_RgKunde.IsExpanded = SI_RgKundeView.Count > 0 ? state : false;
                    this.ExpSI_GSLieferant.IsExpanded = SI_GSLieferantView.Count > 0 ? state : false;
                    this.ExpSI_GSKunde.IsExpanded = SI_GSKundeView.Count > 0 ? state : false;
                    this.ExpSIAuftrag.IsExpanded = state;
                    this.ExpService.IsExpanded = state;
                    // this.expKalkulationstabellen.IsExpanded = calcQuery.Count() > 0 ? state : false;
                    this.expKalkulationstabellen.IsExpanded = KalkulationView.Count > 0 ? state : false;
                    if (this.ArtikelQuery == null)
                    {
                        this.expLagerlisten.IsExpanded = false;
                    }
                    else
                    {
                        this.expLagerlisten.IsExpanded = ArtikelQuery.Count() > 0 ? state : false;
                    }

                    this.ExpAuftrag.IsExpanded = state;
                }
                else
                {
                    this.ExpAnlagenTyp.IsExpanded = state;
                    this.ExpProjektverlauf.IsExpanded = state;
                    this.ExpAusgangsRechnungen.IsExpanded = state;
                    this.ExpAnlagenTyp.IsExpanded = state;
                    this.ExpAuftrag.IsExpanded = state;
                    this.ExpAusgangsGutschriften.IsExpanded = state;
                    this.ExpAusgangsRechnungen.IsExpanded = state;
                    this.ExpErsatzteileGutschrift.IsExpanded = state;
                    this.ExpKomponenten.IsExpanded = state;
                    this.ExpLieferantenRechnungen.IsExpanded = state;
                    this.ExpLieferantenGutschriften.IsExpanded = state;
                    this.ExpErsatzteileGutschrift.IsExpanded = state;
                    this.ExpProjektRechnung.IsExpanded = state;
                    this.ExpSI_RGLieferant.IsExpanded = state;
                    this.ExpSI_RgKunde.IsExpanded = state;
                    this.ExpSI_GSLieferant.IsExpanded = state;
                    this.ExpSI_GSKunde.IsExpanded = state;
                    this.ExpSIAuftrag.IsExpanded = state;
                    this.ExpService.IsExpanded = state;
                    this.expKalkulationstabellen.IsExpanded = false;
                    this.expLagerlisten.IsExpanded = false;
                }

            }



            //this.ExpAnlagenTyp.Header = "AnlagenTyp : " + AnlagenTypView.Count.ToString();
            //this.ExpProjektverlauf.Header = "Projektverlauf : " + VerlaufView.Count;



            //this.ExpAusgangsGutschriften.Header = "Ausgangsgutschriften : " + AusgangsGutschriftenView.Count;
            //this.ExpAusgangsRechnungen.Header = "Ausgangsrechnungen : " + AusgangsRechnungenView.Count;
            //this.ExpErsatzteileGutschrift.Header = "Ersatzteilegutschriften : " + ErsatzteileGutschriftView.Count;
            //this.ExpKomponenten.Header = "Anlagen Lieferzeit : " + AnlagenLieferzeitView.Count;
            //this.ExpLieferantenRechnungen.Header = "Lieferantenrechnungen : " + LieferantenRechnungenView.Count;
            //this.ExpLieferantenGutschriften.Header = "Lieferantengutschriften : " + LieferantenGutschriftenView.Count;
            //this.ExpErsatzteileGutschrift.Header = "Gutschriften : " + ErsatzteileGutschriftView.Count;
            //this.ExpProjektRechnung.Header = " Rechnungen : " + ProjektRechnungView.Count;

            //this.ExpSI_GSKunde.Header = "SI Gutschriften an Kunden : " + SI_GSKundeView.Count;
            //this.ExpSI_RgKunde.Header = "SI Ausgangsrechnungen : " + SI_RgKundeView.Count;
            //this.ExpSI_GSLieferant.Header = "SI Gutschriften an Lieferanten : " + SI_GSLieferantView.Count;
            //this.ExpSI_RGLieferant.Header = "SI Lieferantenrechnungen  : " + SI_RgLieferantView.Count;




        }

        #endregion



        #region "GotFocus"

        private void projekt_verlaufListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.projekt_verlaufListView.SelectedItem = item.DataContext;

        }

        private void AnlagenTyp_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.AnlagenTyp_ListView.SelectedItem = item.DataContext;

        }



        private void RechnungItem_GotFocus(object sender, EventArgs e)
        {

            var item = (ListViewItem)sender;
            item.IsSelected = true;
            this.AusgangsRechnungen_ListView.SelectedItem = item.DataContext;

        }

        private void Aggregate_ListView_GotFocus(object sender, EventArgs e)
        {

            var item = (ListViewItem)sender;
            this.Aggregate_ListView.SelectedItem = item.DataContext;

        }

        private void AnlagenLieferzeit_GotFocus(object sender, EventArgs e)
        {

            var item = (ListViewItem)sender;
            this.Komponenten_ListView.SelectedItem = item.DataContext;

        }

        private void SI_RgKunde_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.SI_RgKunde_ListView.SelectedItem = item.DataContext;

        }

        private void ListViewItem_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            MessageBox.Show(item.Name);

            //this.SI_RgKunde_ListView.SelectedItem = item.DataContext;

        }

        private void AusgangsGutschriften_Listview_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.AusgangsGutschriften_ListView.SelectedItem = item.DataContext;

        }

        private void AusgangsRechnungen_Listview_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.AusgangsRechnungen_ListView.SelectedItem = item.DataContext;

        }

        private void LieferantenRechnungen_Listview_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.LieferantenRechnungen_ListView.SelectedItem = item.DataContext;

        }

        private void LieferantenGutschriften_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.LieferantenGutschriften_ListView.SelectedItem = item.DataContext;

        }

        private void ErsatzteileGutschrift_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.ErsatzteileGutschrift_ListView.SelectedItem = item.DataContext;

        }

        private void SI_GSLieferant_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.SI_GSLieferant_ListView.SelectedItem = item.DataContext;

        }

        private void SI_RGLieferant_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.SI_RGLieferant_ListView.SelectedItem = item.DataContext;

        }

        private void SI_GSKunde_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.SI_GSKunde_ListView.SelectedItem = item.DataContext;

        }

        private void KalkulationListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.KalkulationListView.SelectedItem = item.DataContext;

        }


        private void ProjektRechnungKunde_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.ProjektRechnungKunde_ListView.SelectedItem = item.DataContext;

        }

        private void ProjektGutschriftKunde_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.ProjektGutschriftKunde_ListView.SelectedItem = item.DataContext;

        }


        private void ProjektRechnungLieferant_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.ProjektRechnungLieferant_ListView.SelectedItem = item.DataContext;

        }

        private void ProjektGutschriftLieferant_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.ProjektGutschriftLieferant_ListView.SelectedItem = item.DataContext;

        }




        //ProjektGutschriftKunde_ListView_GotFocus

        #endregion




        #region "menue"

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MenuItem Click");
        }

        private void Neu_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show ("Neu Click");
            this.ExpProjektverlauf.Visibility = System.Windows.Visibility.Collapsed;
            this.SP4.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Ende_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Ende Click");
            this.ExpProjektverlauf.Visibility = System.Windows.Visibility.Visible;
            this.SP4.Visibility = System.Windows.Visibility.Visible;
        }

        #endregion

        #region "addEntries"

        private void addProjekt_Click(object sender, RoutedEventArgs e)
        {
            //  var proj = (projekt)(this.ProjektView.AddNew());
            //  proj.projektnummer = ProjektRepo.GetNewProjektnummerWithYear();
            //  //proj.firmenname = "Brunvoll AS";
            //  //db.SaveChanges();

            ////  FillProjekt(proj.id);

            //  FormatMainWindow.SetSI(false, false, this);
            //  // FormatMainWindow.SetType(this.cboType.Text, this);

            //  this.ProjektView.CommitNew();
            addNewProjekt();
        }

        private void addNewProjekt()
        {
            try
            {
                var proj = (projekt)(this.ProjektView.AddNew());

                proj.projektnummer = ProjektRepo.GetNewProjektnummerWithYear();
                proj.datum = DateTime.Now;
                proj.id_personchange = Session.User.id;
                // wird in DAL Constructor erledigt.
                //proj.auftrag = 0;
                //proj.si = 0;
                //proj.landx = 0;
                //proj.schiff = 0;



                //proj.firmenname = "Brunvoll AS";
                //db.SaveChanges();

                //  FillProjekt(proj.id);

                FormatMainWindow.SetSI(false, false, this, false);
                // FormatMainWindow.SetType(this.cboType.Text, this);

                this.ProjektView.CommitNew();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ErrorMethods.GetExceptionMessageInfo("Beim Versuch eine neue Projektnummer zu erzeugen ist folgender Fehler aufgetreten: ", ex));
            }

        }


        private void AddAnlagenTyp_Click(object sender, RoutedEventArgs e)
        {
            if (this.AnlagenTypView != null)
            {
                var at = (projekt_anlagentyp)(this.AnlagenTypView.AddNew());

                int id = db.StammWaehrungen.Where(n => n.istStandardWaehrung == 1).SingleOrDefault().id;
                at.id_WKZ = id;
                this.AnlagenTypView.CommitNew();
            }
        }

        private void AddProjektVerlauf_Click(object sender, RoutedEventArgs e)
        {
            if (this.VerlaufView != null)
            {
                var verl = (projekt_verlauf)(this.VerlaufView.AddNew());
                verl.id_personchange = Session.User.id;
                verl.idx = VerlaufView.Count - 1;
                this.VerlaufView.CommitNew();
            }
        }

        private void AddRechnungen_Click(object sender, RoutedEventArgs e)
        {
            if (this.AusgangsRechnungenView != null)
            {
                var rech = (projekt_ausgang_rechnung)(this.AusgangsRechnungenView.AddNew());

                rech.idx = AusgangsRechnungenView.Count - 1;
                this.AusgangsRechnungenView.CommitNew();
            }
        }


        private void AddAggregate_Click(object sender, RoutedEventArgs e)
        {
            if (this.AggregateView != null)
            {
                var agg = (projektAggregat)(this.AggregateView.AddNew());
                this.AggregateView.CommitNew();

            }
        }

        private void AddKomponenten_Click(object sender, RoutedEventArgs e)
        {
            if (this.AnlagenLieferzeitView != null)
            {
                var agg = (projekt_anlage_lieferzeit)(this.AnlagenLieferzeitView.AddNew());
                this.AnlagenLieferzeitView.CommitNew();

            }
        }


        private void AddSI_RgKunde_Click(object sender, RoutedEventArgs e)
        {
            if (this.SI_RgKundeView != null)
            {
                var agg = (projekt_si_rgkunde)(this.SI_RgKundeView.AddNew());
                this.SI_RgKundeView.CommitNew();

            }
        }

        private void cmd_AddListViewItem_Click(object sender, RoutedEventArgs e)
        {
            var cmd = (Button)sender;

            switch (cmd.Name)
            {
                case "AddSI_GSKunde":
                    {
                        if (this.SI_GSKundeView != null)
                        {
                            var typ = (projekt_si_gutschriftkunde)(this.SI_GSKundeView.AddNew());
                            this.SI_GSKundeView.CommitNew();

                        }
                        break;
                    }

                case "AddSI_GSLieferant":
                    {
                        if (this.SI_GSLieferantView != null)
                        {
                            var typ = (projekt_si_gutschriftlieferant)(this.SI_GSLieferantView.AddNew());
                            this.SI_GSLieferantView.CommitNew();

                        }
                        break;
                    }

                case "AddSI_RGLieferant":
                    {
                        if (this.SI_RgLieferantView != null)
                        {
                            var typ = (projekt_si_rglieferant)(this.SI_RgLieferantView.AddNew());
                            this.SI_RgLieferantView.CommitNew();

                        }
                        break;
                    }

                case "AddProjektRechnung":
                    {
                        if (this.ProjektRechnungView != null)
                        {
                            var typ = (projekt_rechnung)(this.ProjektRechnungView.AddNew());
                            FormatMainWindow.SetRechnungFactor(typ, this);
                            FormatMainWindow.SetRechnungInterneNo(typ, this);
                            this.ProjektRechnungView.CommitNew();
                        }
                        break;
                    }


                case "AddErsatzteileGutschrift":
                    {
                        if (this.ErsatzteileGutschriftView != null)
                        {
                            var typ = (projekt_ersatzteile_gutschrift)(this.ErsatzteileGutschriftView.AddNew());
                            this.ErsatzteileGutschriftView.CommitNew();

                        }
                        break;
                    }

                case "AddLieferantenRechnungen":
                    {
                        if (this.LieferantenRechnungenView != null)
                        {
                            var typ = (projekt_lieferant_rechnung)(this.LieferantenRechnungenView.AddNew());
                            typ.erledigt = 0;
                            typ.weiterberechnet = 0;

                            this.LieferantenRechnungenView.CommitNew();

                        }
                        break;
                    }

                case "AddLieferantenGutschriften":
                    {
                        if (this.LieferantenGutschriftenView != null)
                        {
                            var typ = (projekt_lieferant_grechnung)(this.LieferantenGutschriftenView.AddNew());
                            this.LieferantenGutschriftenView.CommitNew();

                        }
                        break;
                    }

                case "AddAusgangsGutschriften":
                    {
                        if (this.AusgangsGutschriftenView != null)
                        {
                            var typ = (projekt_ausgang_grechnung)(this.AusgangsGutschriftenView.AddNew());
                            this.AusgangsGutschriftenView.CommitNew();

                        }
                        break;
                    }

                case "Add_ProjektRechnungKunde":
                    {
                        if (this.ProjektRechnungenKundeView != null)
                        {
                            var typ = (projekt_rechnungkunde)(this.ProjektRechnungenKundeView.AddNew());
                            this.ProjektRechnungenKundeView.CommitNew();

                        }
                        break;
                    }

                case "Add_ProjektGutschriftKunde":
                    {
                        if (this.ProjektGutschriftenKundeView != null)
                        {
                            var typ = (projekt_gutschriftkunde)(this.ProjektGutschriftenKundeView.AddNew());
                            this.ProjektGutschriftenKundeView.CommitNew();

                        }
                        break;
                    }

                case "Add_ProjektRechnungLieferant":
                    {
                        if (this.ProjektRechnungenLieferantView != null)
                        {
                            var typ = (projekt_rechnunglieferant)(this.ProjektRechnungenLieferantView.AddNew());
                            this.ProjektRechnungenLieferantView.CommitNew();

                        }
                        break;
                    }


                case "Add_ProjektGutschriftLieferant":
                    {
                        if (this.ProjektGutschriftenKundeView != null)
                        {
                            var typ = (projekt_gutschriftlieferant)(this.ProjektGutschriftenLieferantView.AddNew());
                            this.ProjektGutschriftenLieferantView.CommitNew();

                        }
                        break;
                    }



                default:
                    MessageBox.Show("Unbekannte Auswahl in AddEntries");
                    break;
            }
            //MessageBox.Show(cmd.Name);
        }

        #endregion

        #region "DeleteEntries"


        private void Delete_ListViewItem_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Eintrag wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var s = (Button)sender;
                switch (s.Name)
                {
                    case "cmdDeleteTyp":
                        {
                            if (this.AnlagenTypView.CurrentPosition > -1)
                            {
                                this.AnlagenTypView.RemoveAt(this.AnlagenTypView.CurrentPosition);
                                // this.AnlagenTypView.Remove(AnlagenTypView.CurrentItem);

                            }
                            break;
                        }


                    case "cmdDeleteVerlauf":
                        {
                            if (this.VerlaufView.CurrentPosition > -1)
                            {
                                this.VerlaufView.RemoveAt(this.VerlaufView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteAggregate":
                        {
                            if (this.AggregateView.CurrentPosition > -1)
                            {
                                this.AggregateView.RemoveAt(this.AggregateView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteKomponente":
                        {
                            if (this.AnlagenLieferzeitView.CurrentPosition > -1)
                            {
                                this.AnlagenLieferzeitView.RemoveAt(this.AnlagenLieferzeitView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }




                    case "cmdDeleteProjektRechnungen":
                        {
                            if (this.ProjektRechnungView.CurrentPosition > -1)
                            {
                                this.ProjektRechnungView.RemoveAt(this.ProjektRechnungView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteErsatzteileGutschrift":
                        {
                            if (this.ErsatzteileGutschriftView.CurrentPosition > -1)
                            {
                                this.ErsatzteileGutschriftView.RemoveAt(this.ErsatzteileGutschriftView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteLieferantenRechnungen":
                        {
                            if (this.LieferantenRechnungenView.CurrentPosition > -1)
                            {
                                this.LieferantenRechnungenView.RemoveAt(this.LieferantenRechnungenView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteLieferantenGutschriften":
                        {
                            if (this.LieferantenGutschriftenView.CurrentPosition > -1)
                            {
                                this.LieferantenGutschriftenView.RemoveAt(this.LieferantenGutschriftenView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteAusgangsRechnungen":
                        {
                            if (this.AusgangsRechnungenView.CurrentPosition > -1)
                            {
                                this.AusgangsRechnungenView.RemoveAt(this.AusgangsRechnungenView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteAusgangsGutschriften":
                        {
                            if (AusgangsGutschriftenView.CurrentPosition > -1)
                            {
                                this.AusgangsGutschriftenView.RemoveAt(this.AusgangsGutschriftenView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteSI_RgKunde":
                        {
                            if (this.SI_RgKundeView.CurrentPosition > -1)
                            {
                                this.SI_RgKundeView.RemoveAt(this.SI_RgKundeView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }


                    case "cmdDeleteSI_GSKunde":
                        {
                            if (this.SI_GSKundeView.CurrentPosition > -1)
                            {
                                this.SI_GSKundeView.RemoveAt(this.SI_GSKundeView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteSI_RgLieferant":
                        {
                            if (this.SI_RgLieferantView.CurrentPosition > -1)
                            {
                                this.SI_RgLieferantView.RemoveAt(this.SI_RgLieferantView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                            }
                            break;
                        }

                    case "cmdDeleteSI_GSLieferant":
                        {
                            if (this.SI_GSLieferantView.CurrentPosition > -1)
                            {
                                this.SI_GSLieferantView.RemoveAt(this.SI_GSLieferantView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);
                            }
                            break;
                        }
                    case "cmdDeleteCalcDetail":
                        {
                            if (this.KalkulationView.CurrentPosition > -1)
                            {
                                this.KalkulationView.RemoveAt(this.KalkulationView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);
                            }
                            break;
                        }

                    case "cmdDeleteProjektRechnungKunde":
                        {
                            if (this.ProjektRechnungenKundeView.CurrentPosition > -1)
                            {
                                this.ProjektRechnungenKundeView.RemoveAt(this.ProjektRechnungenKundeView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);
                            }
                            break;
                        }

                    case "cmdDeleteProjektGutschriftKunde":
                        {
                            if (this.ProjektGutschriftenKundeView.CurrentPosition > -1)
                            {
                                this.ProjektGutschriftenKundeView.RemoveAt(this.ProjektGutschriftenKundeView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);
                            }
                            break;
                        }


                    case "cmdDeleteProjektRechnungLieferant":
                        {
                            if (this.ProjektRechnungenLieferantView.CurrentPosition > -1)
                            {
                                this.ProjektRechnungenLieferantView.RemoveAt(this.ProjektRechnungenLieferantView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);
                            }
                            break;
                        }


                    case "cmdDeleteProjektGutschriftLieferant":
                        {
                            if (this.ProjektGutschriftenLieferantView.CurrentPosition > -1)
                            {
                                this.ProjektGutschriftenLieferantView.RemoveAt(this.ProjektGutschriftenLieferantView.CurrentPosition);
                                //this.VerlaufView.Remove(VerlaufView.CurrentItem);
                            }
                            break;
                        }



                    default:
                        break;
                }

            }


        }
        #endregion

        #region "HilfsFunktionenTemp"


        //private void ProjectByID_Click(object sender, RoutedEventArgs e)
        //{

        //    int ID = int.Parse(this.ProjektID.Text);
        //    FillProjekt(ID);
        //}

        //private void ProjectByNumber_Click(object sender, RoutedEventArgs e)
        //{

        //    int ID = ProjektRepo.GetProjektIDFromNumber(this.Projektnummer.Text);
        //    FillProjekt(ID);
        //}

        //private void brunvoll_Click(object sender, RoutedEventArgs e)
        //{
        //    FormatMainWindow.SetFirma("brunvoll", this);
        //}

        //private void jets_Click(object sender, RoutedEventArgs e)
        //{
        //    FormatMainWindow.SetFirma("jets", this);
        //}


        #endregion

        #region "FormatWindow"

        private void chkAuftrag_Click(object sender, RoutedEventArgs e)
        {
            ToggleSI_Auftrag_Clicked(chkAuftrag, chkSI);
            FormatMainWindow.SetAuftragErsatzteil(cboType.Text, this);
        }

        private void chkSI_Click(object sender, RoutedEventArgs e)
        {
            ToggleSI_Auftrag_Clicked(chkSI, chkAuftrag);
        }


        private void ToggleSI_Auftrag_Clicked(CheckBox A, CheckBox B)
        {

            if ((bool)A.IsChecked)
            {
                if ((bool)(B.IsChecked))
                {
                    if (MessageBox.Show("Wirklich zwischen Auftrag und SI umschalten ?", "Daten werden entfernt", MessageBoxButton.YesNo) != MessageBoxResult.No)
                    {
                        B.IsChecked = false;
                    }
                    else
                    {
                        A.IsChecked = false;
                    }

                }
            }
            else
            {
                if (MessageBox.Show("Wirklich Auftrag/SI ausblenden ?", "Daten werden entfernt", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    A.IsChecked = true;
                }

            }


            FormatMainWindow.SetSI((bool)chkSI.IsChecked, (bool)chkAuftrag.IsChecked, this, false);

        }



        private void chkExpandProject_Click(object sender, RoutedEventArgs e)
        {
            ExpandProjekt(chkExpandProject.IsChecked.Value);
        }

        private void cboFirma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (cboFirma.SelectedIndex == -1)
            //        return;

            //    int kdnr = (int)cboFirma.SelectedValue;
            //    string firma = this.txtFirmenname.Text;
            //    string FirmaNr = db.firmen.Where(f => f.KdNr == kdnr).Single().name;
            //    if (!firma.Equals(FirmaNr))
            //    {
            //        this.txtFirmenname.Text = FirmaNr;
            //    }
            //    //this.txtFirmenname.Text = db.firmen.Where(f => f.KdNr == kdnr).Single().name;
            //    FormatMainWindow.SetFirma(kdnr, this);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));

            //}

            //FormatMainWindow.SetFirma(cboFirma.SelectedValue.ToString(), this);

        }

        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbo = (ComboBox)sender;
            StammProjektTyp spt = (StammProjektTyp)cbo.SelectedItem;

            if (spt != null)
            {
                FormatMainWindow.SetType(spt.Projekttyp, this);
                FormatMainWindow.SetAuftragErsatzteil(spt.Projekttyp, this);


            }

        }





        #endregion

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (ManageChanges(false))
            {
                AreChangesHandled = true;
                this.NavigationService.GoBack();
            }
        }

        private void hylSchiffuebernehmen_Click(object sender, RoutedEventArgs e)
        {

            var prepo = new ProjektRepository(db);
            prepo.insertShipData(this, AggregateView);

        }

        private void cboKunde_EditCompleted(object sender, EventArgs e)
        {
            //  MessageBox.Show(cboKunde.Text);

            var box = (C1.WPF.C1ComboBox)sender;
            //  cboKunde.IsDropDownOpen = true;
            //this event will fire every time the value is changed so we check if the item exist or not
            var item = from c in db.firmen where c.name == box.Text select c;

            if (item.Count() == 0)
            {
                var f = new firma();
                f.name = box.Text;
                f.created = DateTime.Now;
                f.istFirma = 2;

                db.firmen.AddObject(f); //add an item here
                //rebind the Combobox after saving the changes to reflect new items..
            }

        }

        private void hylKalkulation_Click(object sender, RoutedEventArgs e)
        {
            var lbi = (Hyperlink)sender;
            var p = new Kalkulation((int)lbi.CommandParameter);
            p.ShowDialog();




            // NavigationService.Navigate(p);


            // MessageBox.Show(lbi.Name);
        }

        private void btnAddCalculation_Click(object sender, RoutedEventArgs e)
        {
            var p = new Kalkulation(0, this.txtProjektnummer.Text);
            p.ShowDialog();

        }


        private void btnDeckblatt_Click(object sender, RoutedEventArgs e)
        {
            if (txtProjektnummer.Text != string.Empty && int.Parse(txtID.Text) != 0)
            {
                int fNr = int.Parse(tblFirmenNr.Text);
                if (fNr != 0)
                {
                    var report = new Reports.RepViewer(txtProjektnummer.Text, int.Parse(txtID.Text), fNr, (bool)chkAuftrag.IsChecked, (bool)chkSI.IsChecked, cboType.Text);
                    report.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Es ist kein Kunde ausgewählt.");
                }


            }
            else
            {
                MessageBox.Show("Deckblatt für nicht gespeichertes Projekt kann nicht angezeigt werden.");
            }


        }



        private void cboKunde_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // cboKunde.IsDropDownOpen = true;
        }

        private void cboKunde_GotFocus(object sender, RoutedEventArgs e)
        {
            //  cboKunde.IsDropDownOpen = true;
        }

        private void AddArtikel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hylLagerlisten_Click(object sender, RoutedEventArgs e)
        {
            var lbi = (Hyperlink)sender;
            var p = new EditArtikel((int)lbi.CommandParameter);
            p.ShowDialog();
        }

        private void DoCalculateKommission(object sender, TextChangedEventArgs e)
        {

            var v = (projekt_rechnung)ProjektRechnungView.CurrentItem;
            if (v.faktor != null && v.betrag != null)
            {
                double x = (double)v.betrag;
                double y = (double)v.faktor;
                v.kommission = (x * y) / 100;
            }




        }

        private void fcbSchiff_onfcbChanged(object sender, UserControls.FilteredComboBoxChangedEventArgs e)
        {
            ftbSchiff.cBox.ItemsSource = RefreshShippComboBox(e.filter);

        }

        private IEnumerable RefreshShippComboBox(string filter)
        {
            if (filter == string.Empty)
            {
                var boat = from s in db.schiffe
                           orderby s.name
                           where s.name != string.Empty
                           select new { s.name };

                return boat.ToList();

            }
            else
            {
                var boat = from s in db.schiffe
                           orderby s.name
                           where s.name != string.Empty && s.name.Contains(filter)
                           select new { s.name };

                return boat.ToList();

            }




        }

        private void btnCallAusgangsRechnung_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.SI_RgKundeView.CurrentPosition > -1)
                {
                    var ag = (projekt_si_rgkunde)this.SI_RgKundeView.CurrentItem;

                    if (this.tblFirmenNr.Text.Trim() == "10177")
                    {
                        string buf = rbLand.IsChecked == true ? "JTL" : "JTS";

                        var cr = new Tools.CallRechnung(ag.rechnungsnr, (DateTime)ag.rechnungfaellig, this.ftbSchiff.tBox.Text, this.Firma_FCB_X.Text, this.txtProjektnummer.Text, buf);
                    }
                    else
                    {
                        var cr = new Tools.CallRechnung(ag.rechnungsnr, (DateTime)ag.rechnungfaellig, this.ftbSchiff.tBox.Text, this.Firma_FCB_X.Text, this.txtProjektnummer.Text);
                    }



                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void hylMakeService_Click(object sender, RoutedEventArgs e)
        {

            if (ManageChanges())
            {


                projekt p1 = (projekt)ProjektView.CurrentItem;
                int res = 0;
                if (p1.type.ToLower() == "service")
                {
                    var query = from p in db.projekte
                                where p.projektnummer == p1.projektnummer && p.type.ToLower() == "ersatzteile"
                                select p.id;

                    res = query.FirstOrDefault();



                }
                else if (p1.type.ToLower() == "ersatzteile")
                {
                    var query = from p in db.projekte
                                where p.projektnummer == p1.projektnummer && p.type.ToLower() == "service"
                                select p.id;

                    res = query.FirstOrDefault();
                }

                if (res != 0)
                {
                    FillProjekt(res);
                }
                else
                {



                    try
                    {
                        Cursor = System.Windows.Input.Cursors.Wait;
                        // SaveLieferadresse();
                        db.SaveChanges();
                        using (var data = new SteinbachEntities())
                        {
                            //projekt p1 = (projekt)ProjektView.CurrentItem;
                            projekt p2 = (projekt)p1.Clone();
                            if (p1.type.ToLower() == "service")
                            {
                                p2.type = "Ersatzteile";
                            }
                            else if (p1.type.ToLower() == "ersatzteile")
                            {
                                p2.type = "Service";
                                p2.projekt_ausgang_grechnung.Clear();
                                p2.projekt_ausgang_rechnung.Clear();
                                p2.projekt_ersatzteile_gutschrift.Clear();
                                p2.projekt_lieferant_grechnung.Clear();
                                p2.projekt_lieferant_rechnung.Clear();
                                p2.projekt_rechnung.Clear();
                                p2.projekt_reklamation_rechnung.Clear();
                                p2.projekt_si_gutschriftkunde.Clear();
                                p2.projekt_si_gutschriftlieferant.Clear();
                                p2.projekt_si_rgkunde.Clear();
                                p2.projekt_si_rglieferant.Clear();
                            }


                            p2.created = DateTime.Now;
                            data.AddToprojekte(p2);
                            data.SaveChanges();

                            FillProjekt(p2.id);

                        }

                        db = new SteinbachEntities();
                        Cursor = System.Windows.Input.Cursors.Arrow;

                    }
                    catch (Exception ex)
                    {
                        Cursor = System.Windows.Input.Cursors.Arrow;
                        MessageBox.Show(String.Format("Kopieren des Projektes nicht erfolgreich.\n{0}", ex.Message));
                    }

                }
            }
        }

        private void fcbKunde_onfcbChanged(object sender, UserControls.FilteredComboBoxChangedEventArgs e)
        {
            //fcbKunde.cBox.ItemsSource = RefreshKundeComboBox(e.filter);
            fcbKunde.ComboBoxViewSource = RefreshKundeComboBox(e.filter);
        }

        private IEnumerable RefreshKundeComboBox(string filter)
        {

            if (filter != string.Empty)
            {
                var count = db.firmen.Count(c => c.name.Contains(filter));

                if (count == 0)
                {

                    if (MessageBox.Show("Eintrag existiert nicht in Kundentabelle - Als neuen Kunden anlegen?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        using (var data = new SteinbachEntities())
                        {

                            var kunde = new firma();
                            kunde.name = filter;
                            kunde.istFirma = 3;
                            kunde.IstKunde = 1;
                            //kunde.KdNr = (int)db.firmen.Max(f => f.KdNr) + 1;
                            kunde.KdNr = (int)db.firmen.Max(i => i.id) + 10001;
                            kunde.istVerarbeitet = 1;
                            // kunde.KdNr = KdNr;
                            kunde.created = DateTime.Now;
                            data.AddTofirmen(kunde);
                            data.SaveChanges();

                            var id = db.firmen.Max(i => i.id);
                            var x = new EditFirma(id);
                            x.ShowDialog();
                        }
                    }
                    else
                    {
                        return RefreshKundeComboBox(string.Empty);

                    }
                }

            }


            IEnumerable boat = null;

            if (filter == string.Empty)
            {
                boat = from s in db.firmen
                       orderby s.name
                       where s.name != string.Empty && s.IstKunde == 1
                       select s; // new { s.name, s.KdNr };

                return (IEnumerable)boat;

            }
            else
            {
                boat = from s in db.firmen
                       orderby s.name
                       where s.name != string.Empty && s.name.Contains(filter) && s.IstKunde == 1
                       select s; // new { s.name,s.KdNr };

                return (IEnumerable)boat;

            }

        }

        private void TestData_Click(object sender, RoutedEventArgs e)
        {
            var tui = new Temp.TestUIWindow();
            tui.Show();
        }

        private void ExpSIAuftrag_Expanded(object sender, RoutedEventArgs e)
        {

        }

        private void rbSchiff_Checked(object sender, RoutedEventArgs e)
        {
            FormatMainWindow.SetJetsLandanlagen(false, this);
        }

        private void rbLand_Checked(object sender, RoutedEventArgs e)
        {
            FormatMainWindow.SetJetsLandanlagen(true, this);
        }

        private void fcbKunde_OnFcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(fcbKunde.cBox.SelectedValue.ToString() + " - " + fcbKunde.cBox.SelectedItem);

            try
            {
                var proj = (projekt)(this.ProjektView.CurrentItem);
                var KdName = (firma)fcbKunde.cBox.SelectedItem;
                proj.kundenname = KdName.name;
                proj.ZusatzInfo = KdName.name;
                //LieferadressenLookUp.Source = RefreshLieferadresseComboBox(string.Empty);

                //proj.id_Lieferadresse = 0;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));

            }


        }

        private void hylUrsprungsprojekt_Click(object sender, RoutedEventArgs e)
        {
            if (txtUrsprungsprojekt.Text != string.Empty)
            {
                try
                {
                    // int? res;
                    if (ManageChanges(true))
                    {
                        AreChangesHandled = true;

                        //  res = db.projekte.Where(s => s.projektnummer == txtUrsprungsprojekt.Text).FirstOrDefault().id;

                        var buf = db.projekte.Where(s => s.projektnummer == txtUrsprungsprojekt.Text).FirstOrDefault();

                        if (buf != null)
                        {

                            FillProjekt(buf.id);
                        }
                        else
                        {

                            MessageBox.Show(string.Format("Es existiert kein Projekt mit der Projektnummer {0}", txtUrsprungsprojekt.Text));
                        }

                    }

                }
                catch (Exception ex)
                {

                    ErrorMethods.HandleStandardError(ex);
                }

            }

        }

        private void hylReklamationenUrsprung_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (ManageChanges(true))
                {
                    AreChangesHandled = true;

                    var hl = (Hyperlink)sender;
                    // var mw = new MainWindow((int)(hl.CommandParameter));           // Probleme mit neuer Instanz des Formulars  ???
                    // NavigationService.Navigate(mw);
                    //  db = new SteinbachEntities();
                    FillProjekt((int)(hl.CommandParameter));
                }
            }
            catch (Exception ex)
            {
                ErrorMethods.HandleStandardError(ex);
                //MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));
            }


        }

        private void Firma_FCB_X_OnFcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                int kdnr = (int)Firma_FCB_X.SelectedValue;
                string firma = this.txtFirmenname.Text;
                string FirmaNr = db.firmen.Where(f => f.KdNr == kdnr).Single().name;
                if (!firma.Equals(FirmaNr))
                {
                    this.txtFirmenname.Text = FirmaNr;
                }
                //this.txtFirmenname.Text = db.firmen.Where(f => f.KdNr == kdnr).Single().name;
                FormatMainWindow.SetFirma(kdnr, this);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void SelectAddress(object sender, RoutedEventArgs e)
        {

            TextBox tb = (sender as TextBox);

            if (tb != null)
            {

                tb.SelectAll();

            }

        }



        private void SelectivelyIgnoreMouseButton(object sender,

            MouseButtonEventArgs e)
        {

            TextBox tb = (sender as TextBox);

            if (tb != null)
            {

                if (!tb.IsKeyboardFocusWithin)
                {

                    e.Handled = true;

                    tb.Focus();

                }

            }

        }

        private void ExpErsatzteileGutschrift_Expanded(object sender, RoutedEventArgs e)
        {

        }




        private IEnumerable RefreshLieferadresseComboBox(string filter)
        {
            var proj = (projekt)(this.ProjektView.CurrentItem);
            var Kd = (firma)fcbKunde.cBox.SelectedItem;

            try
            {

                if (filter == string.Empty)
                {

                    var boat = db.Firmen_Adressen.Where(s => s.id_Firma == Kd.id && s.Typ == 3);

                    return boat.ToList();

                }
                else
                {

                    var boat = from s in db.Firmen_Adressen
                               orderby s.Standort
                               where s.id_Firma == Kd.id && s.Typ == 3 && s.Postfach.Contains(filter)
                               select s; // new { s.Standort,s.PLZ,s.Ort,s.Straße };


                    return boat.ToList();

                }


            }
            catch (Exception)
            {

                return null;
            }






        }

        private void fcbLieferadresse_onfcbChanged(object sender, DataTypes.FilteredComboBoxChangedEventArgs e)
        {
            LieferadressenLookUp.Source = RefreshLieferadresseComboBox(e.filter);
        }
















    }
}