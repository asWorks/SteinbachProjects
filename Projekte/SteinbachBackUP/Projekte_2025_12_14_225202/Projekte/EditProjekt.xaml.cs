using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ProjektDB.Repositories;
using DAL;
using DAL.Tools;
using ProjektDB.Format;


namespace ProjektDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class EditProjekt : Window
    {
        #region "Declarations"
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
        private CollectionViewSource RechnungenViewSource;
        private BindingListCollectionView RechnungenView;
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

        //private GetData gt;

        #endregion



        public EditProjekt()
        {
            CurrentProjektID = Session.CurrentProjectID;
            InitializeComponent();
        }

        public EditProjekt(int ProjID)
        {
            CurrentProjektID = ProjID;
            InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            db = new SteinbachEntities();
            db.SavingChanges += new EventHandler(db_SavingChanges);
            ProjektRepo = new ProjektRepository(db);
            VerlaufRepo = new VerlaufRepository(db);

            if (CurrentProjektID != 0)
                FillProjekt(CurrentProjektID);

            var x = new Properties.Settings();
            ExpandProjekt(x.ProjekteAusklappen);
            chkExpandProject.IsChecked = x.ProjekteAusklappen;

            FormatEditProjekt.SetSI((bool)chkSI.IsChecked, (bool)chkAuftrag.IsChecked, this);
            FormatEditProjekt.SetFirma((int)cboFirma.SelectedValue, this);
            FormatEditProjekt.SetType(this.cboType.Text, this);

        }

        void db_SavingChanges(object sender, EventArgs e)
        {
            foreach (var entry in db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted))
            {
                if (entry.Entity is projekt)
                {
                    var p = (projekt)entry.Entity;
                    p.created = DateTime.Now;

                }
                if (entry.Entity is projekt_verlauf)
                {
                    var v = (projekt_verlauf)entry.Entity;
                    v.id_personchange = Session.User.id;
                }
            }
        }





        private void FillProjekt(int ProjektID)
        {

            ProjektViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Projekt_ViewSource")));
            var ProjektQuery = ProjektRepo.GetProjekteByID(ProjektID);
            ProjektViewSource.Source = ProjektQuery;
            ProjektView = (ListCollectionView)ProjektViewSource.View;
            projekt p = (projekt)ProjektView.CurrentItem;
            FirmaLookupViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("FirmaLookup")));
            FirmaLookupViewSource.Source = ProjektRepo.GetFirmen();

            KundenLookupViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("KundenLookup")));
            KundenLookupViewSource.Source = ProjektRepo.GetKunden();


            var AggLookup = ((System.Windows.Data.CollectionViewSource)(this.FindResource("AggregateLookup")));
            var aquery = from a in db.Stamm_Aggregate
                         select a;
            AggLookup.Source = aquery;

            var UrLookup = ((System.Windows.Data.CollectionViewSource)(this.FindResource("UrsprungLookUp")));
            UrLookup.Source = ProjektRepo.GetUntergeordneteProjekte(ProjektID);

            var schiffeLookUp = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SchiffLookup")));
            schiffeLookUp.Source = ProjektRepo.GetSchiff();

            var TypeLookUp = ((System.Windows.Data.CollectionViewSource)(this.FindResource("TypLookUp")));
            TypeLookUp.Source = ProjektRepo.GetProjektTyp();

            AggregateViewSource = ((CollectionViewSource)(this.FindResource("Aggregate_ViewSource")));
            AggregateView = (BindingListCollectionView)(AggregateViewSource.View);

            VerlaufViewSource = ((CollectionViewSource)(this.FindResource("ProjektVerlauf_ViewSource")));
            VerlaufView = (BindingListCollectionView)(VerlaufViewSource.View);

            AnlagenTypViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("AnlagenTyp_ViewSource")));
            AnlagenTypView = (BindingListCollectionView)(AnlagenTypViewSource.View);

            RechnungenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Rechnungen_ViewSource")));
            RechnungenView = (BindingListCollectionView)(RechnungenViewSource.View);

            AnlagenLieferzeitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("AnlagenLieferzeit_ViewSource")));
            AnlagenLieferzeitView = (BindingListCollectionView)(AnlagenLieferzeitViewSource.View);

            ProjektRechnungViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ProjektRechnung_ViewSource")));
            ProjektRechnungView = (BindingListCollectionView)(ProjektRechnungViewSource.View);

            AusgangsRechnungenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("AusgangsRechnungen_ViewSource")));
            AusgangsRechnungenView = (BindingListCollectionView)(AusgangsRechnungenViewSource.View);

            ErsatzteileGutschriftViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ErsatzteileGutschrift_ViewSource")));
            ErsatzteileGutschriftView = (BindingListCollectionView)(ErsatzteileGutschriftViewSource.View);

            LieferantenRechnungenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LieferantenRechnungen_ViewSource")));
            LieferantenRechnungenView = (BindingListCollectionView)(LieferantenRechnungenViewSource.View);

            LieferantenGutschriftenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LieferantenGutschriften_ViewSource")));
            LieferantenGutschriftenView = (BindingListCollectionView)(LieferantenGutschriftenViewSource.View);

            AusgangsGutschriftenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("AusgangsGutschriften_ViewSource")));
            AusgangsGutschriftenView = (BindingListCollectionView)(AusgangsGutschriftenViewSource.View);

            SI_RgKundeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SI_RgKunde_ViewSource")));
            SI_RgKundeView = (BindingListCollectionView)(SI_RgKundeViewSource.View);

            SI_GSKundeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SI_GSKunde_ViewSource")));
            SI_GSKundeView = (BindingListCollectionView)(SI_GSKundeViewSource.View);

            SI_RgLieferantViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SI_RgLieferant_ViewSource")));
            SI_RgLieferantView = (BindingListCollectionView)(SI_RgLieferantViewSource.View);

            SI_GSLieferantViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SI_GSLieferant_ViewSource")));
            SI_GSLieferantView = (BindingListCollectionView)(SI_GSLieferantViewSource.View);




            if (ProjektID > 0)
            {
                this.ExpAnlagenTyp.Header = "AnlagenTyp : " + AnlagenTypView.Count.ToString();
                this.ExpProjektverlauf.Header = "Projektverlauf : " + VerlaufView.Count;



                this.ExpAusgangsGutschriften.Header = "AusgangsGutschriften : " + AusgangsGutschriftenView.Count;
                this.ExpAusgangsRechnungen.Header = "AusgangsRechnungenView : " + AusgangsRechnungenView.Count;
                this.ExpErsatzteileGutschrift.Header = "ErsatzteileGutschriftView : " + ErsatzteileGutschriftView.Count;
                this.ExpKomponenten.Header = "Anlagen Lieferzeit : " + AnlagenLieferzeitView.Count;
                this.ExpLieferantenRechnungen.Header = "LieferantenRechnungen : " + LieferantenRechnungenView.Count;
                this.ExpLieferantenGutschriften.Header = "LieferantenGutschriften : " + LieferantenGutschriftenView.Count;
                this.ExpErsatzteileGutschrift.Header = "ErsatzteileGutschrift : " + ErsatzteileGutschriftView.Count;
                this.ExpProjektRechnung.Header = " ProjektRechnung : " + ProjektRechnungView.Count;

                this.ExpSI_GSKunde.Header = "Gutschriften an Kunden : " + SI_GSKundeView.Count;
                this.ExpSI_RgKunde.Header = "Ausgangsrechnungen : " + SI_RgKundeView.Count;
                this.ExpSI_GSLieferant.Header = "Gutschriften an Lieferanten : " + SI_GSLieferantView.Count;
                this.ExpSI_RGLieferant.Header = "Lieferantenrechnungen  : " + SI_RgLieferantView.Count;



            }





            ProjektView.CurrentChanged += new EventHandler(ProjektView_CurrentChanged);




        }

        void ProjektView_CurrentChanged(object sender, EventArgs e)
        {
            VerlaufView = (BindingListCollectionView)(VerlaufViewSource.View);
            AnlagenTypView = (BindingListCollectionView)(AnlagenTypViewSource.View);
            RechnungenView = (BindingListCollectionView)(RechnungenViewSource.View);
            AnlagenLieferzeitView = (BindingListCollectionView)(AnlagenLieferzeitViewSource.View);
            ProjektRechnungView = (BindingListCollectionView)(ProjektRechnungViewSource.View);
            AusgangsRechnungenView = (BindingListCollectionView)(AusgangsRechnungenViewSource.View);
            ErsatzteileGutschriftView = (BindingListCollectionView)(ErsatzteileGutschriftViewSource.View);
            LieferantenRechnungenView = (BindingListCollectionView)(LieferantenRechnungenViewSource.View);
            LieferantenGutschriftenView = (BindingListCollectionView)(LieferantenGutschriftenViewSource.View);
            AusgangsGutschriftenView = (BindingListCollectionView)(AusgangsGutschriftenViewSource.View);

            ////projekt p = (projekt)ProjektView.CurrentItem;
            ////if (p != null && !p.projekt_verlauf.IsLoaded)
            ////   p.projekt_verlauf.Load();



        }


        #region "GotFocus"

        private void VerlaufItem_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.projekt_verlaufListView.SelectedItem = item.DataContext;
        }

        private void TypItem_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.AnlagenTyp_ListView.SelectedItem = item.DataContext;

        }

        private void RechnungItem_GotFocus(object sender, EventArgs e)
        {

            var item = (ListViewItem)sender;
            this.AusgangsRechnungen_ListView.SelectedItem = item.DataContext;

        }

        private void Aggregat_GotFocus(object sender, EventArgs e)
        {

            var item = (ListViewItem)sender;
            this.Aggregate_ListView.SelectedItem = item.DataContext;

        }

        private void AnlagenLieferzeit_GotFocus(object sender, EventArgs e)
        {

            var item = (ListViewItem)sender;
            this.Komponenten_ListView.SelectedItem = item.DataContext;

        }




        #endregion

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                LoggingTool.LogExeption(ex, "EditProjekt", "Save_Click");
            }

        }


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
            var proj = (projekt)(this.ProjektView.AddNew());
            proj.projektnummer = ProjektRepo.GetNewProjektnummerWithYear();
            FormatEditProjekt.SetSI(false, false, this);
            // FormatEditProjekt.SetType(this.cboType.Text, this);

            this.ProjektView.CommitNew();
        }


        private void AddAnlagenTyp_Click(object sender, RoutedEventArgs e)
        {
            if (this.AnlagenTypView != null)
            {
                var at = (projekt_anlagentyp)(this.AnlagenTypView.AddNew());
                //verl.id_personchange = Session.User.id;
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
            if (this.RechnungenView != null)
            {
                var rech = (projekt_ausgang_rechnung)(this.RechnungenView.AddNew());
                // verl.id_personchange = Session.User.id;
                rech.idx = RechnungenView.Count - 1;
                this.RechnungenView.CommitNew();
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

        #endregion

        #region "DeleteEntries"

        private void cmdDeleteTyp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.AnlagenTypView.CurrentPosition > -1)
                {
                    this.AnlagenTypView.RemoveAt(this.AnlagenTypView.CurrentPosition);
                    // this.AnlagenTypView.Remove(AnlagenTypView.CurrentItem);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdDeleteVerlauf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.VerlaufView.CurrentPosition > -1)
                {
                    this.VerlaufView.RemoveAt(this.VerlaufView.CurrentPosition);
                    //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void cmdDeleteRechnungen_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (this.RechnungenView.CurrentPosition > -1)
                {
                    this.RechnungenView.RemoveAt(this.RechnungenView.CurrentPosition);
                    //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmdDeleteAggregate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.AggregateView.CurrentPosition > -1)
                {
                    this.AggregateView.RemoveAt(this.AggregateView.CurrentPosition);
                    //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void cmdDeleteKomponente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.AnlagenLieferzeitView.CurrentPosition > -1)
                {
                    this.AnlagenLieferzeitView.RemoveAt(this.AnlagenLieferzeitView.CurrentPosition);
                    //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region "HilfsFunktionenTemp"


        private void ProjectByID_Click(object sender, RoutedEventArgs e)
        {

            int ID = int.Parse(this.ProjektID.Text);
            FillProjekt(ID);
        }

        private void ProjectByNumber_Click(object sender, RoutedEventArgs e)
        {

            int ID = ProjektRepo.GetProjektIDFromNumber(this.Projektnummer.Text);
            FillProjekt(ID);
        }

        //private void brunvoll_Click(object sender, RoutedEventArgs e)
        //{
        //    FormatEditProjekt.SetFirma("brunvoll", this);
        //}

        //private void jets_Click(object sender, RoutedEventArgs e)
        //{
        //    FormatEditProjekt.SetFirma("jets", this);
        //}

        private void SI_Click(object sender, RoutedEventArgs e)
        {
            //FormatEditProjekt mw = new FormatEditProjekt();
            //mw.SetSI(true, this);

        }

        private void Auftrag_Click(object sender, RoutedEventArgs e)
        {
            //FormatEditProjekt mw = new FormatEditProjekt();
            //mw.SetSI(false, this);
        }

        #endregion

        #region "FormatWindow"


        private void chkAuftrag_Checked(object sender, RoutedEventArgs e)
        {
            if (chkSI.IsChecked == true)
            {
                chkSI.IsChecked = false;
            }

            FormatEditProjekt.SetSI((bool)chkSI.IsChecked, (bool)chkAuftrag.IsChecked, this);
        }

        private void chkSI_Checked(object sender, RoutedEventArgs e)
        {
            if (chkAuftrag.IsChecked == true)
            {
                chkAuftrag.IsChecked = false;
            }
            FormatEditProjekt.SetSI((bool)chkSI.IsChecked, (bool)chkAuftrag.IsChecked, this);
        }

        private void chkAnfrage_Checked(object sender, RoutedEventArgs e)
        {


        }

        private void chkAuftrag_Unchecked(object sender, RoutedEventArgs e)
        {
            FormatEditProjekt.SetSI((bool)chkSI.IsChecked, (bool)chkAuftrag.IsChecked, this);
        }

        private void chkSI_Unchecked(object sender, RoutedEventArgs e)
        {
            FormatEditProjekt.SetSI((bool)chkSI.IsChecked, (bool)chkAuftrag.IsChecked, this);
        }

        private void chkExpandProject_Click(object sender, RoutedEventArgs e)
        {
            ExpandProjekt(chkExpandProject.IsChecked.Value);
        }

        private void cboFirma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FormatEditProjekt.SetFirma((int)cboFirma.SelectedValue, this);
        }

        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbo = (ComboBox)sender;
            StammProjektTyp spt = (StammProjektTyp)cbo.SelectedItem;

            if (spt != null)
                FormatEditProjekt.SetType(spt.Projekttyp, this);

        }

        void ExpandProjekt(bool state)
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



        }



        #endregion



        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);
           

            if (om.Count() > 0)
            {
                var res = (MessageBox.Show("Daten wurden verändert. Änderungen speichern?", "Sicherheitsabfrage", MessageBoxButton.YesNo));

                switch (res)
                {

                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Yes:
                        this.db.SaveChanges();
                        break;
                    default:
                        break;
                }

            }


        }

        private void Page_LostFocus(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Lost focus");
        }











    }
}