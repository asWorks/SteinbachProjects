using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DAL;
using ProjektDB.ObjectCollections;
using ProjektDB.Repositories;
using System.Collections.Generic;
using CommonTools.Tools;
using System.Windows.Media;




namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for Kalkulation.xaml
    /// </summary>
    public partial class Kalkulation : Window
    {

        #region "Declarations"

        private enum ArtikelState
        {
            ArtikelExists,
            Done,
            ArtikelNew,
            NoState
        }

        private ArtikelState ArtState;

        lagerliste ThisArtikel = null;


        private ListCollectionView KalkulationView;
        private CollectionViewSource KalkulationViewSource;
        private CollectionViewSource KalkulationDetailViewSource;
        private BindingListCollectionView KalkulationDetailView;
        private CollectionViewSource LieferantenViewSource;
        private CollectionViewSource SetsViewSource;

        int CurrentProjektID = 0;
        string ProjektNummer = string.Empty;
        private ProjektRepository pRepo;
        private bool bRowEditing = false;
        private bool bSetArtikel = false;
        private bool bUpdatePrice = false;
        List<string> si = new List<string>();
        private IEnumerable<string> source;


        kalkulationstabelle_detail CurrDetails = null;




        private SteinbachEntities db;

        #endregion


        #region "Constructors"

        public Kalkulation()
        {
            InitializeComponent();



        }

        public Kalkulation(int CalcID)
        {
            InitializeComponent();
            CurrentProjektID = CalcID;
            db = new SteinbachEntities();
            FillList();
        }
        public Kalkulation(int CalcID, string Projektnummer)
        {
            InitializeComponent();
            CurrentProjektID = CalcID;
            ProjektNummer = Projektnummer;
            db = new SteinbachEntities();
            FillList();
        }

        public Kalkulation(int CalcID, string Projektnummer, SteinbachEntities dbContext)
        {
            
            InitializeComponent();
            CurrentProjektID = CalcID;
            ProjektNummer = Projektnummer;
            db = dbContext;
            FillList();
        }

        #endregion

        #region "Prepare"

        private void FillList()
        {

            //db = new SteinbachEntities();
            ArtState = ArtikelState.NoState;
            pRepo = new ProjektRepository(this.db);
            db.SavingChanges += new EventHandler(db_SavingChanges);
            KalkulationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Kalkulation_ViewSource")));
            KalkulationDetailViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("KalkulationDetail_ViewSource")));
            SetsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SetsViewSource")));

            // var calc = from k in db.kalkulationstabellen where k.id == CurrentProjektID select k;
            KalkulationViewSource.Source = pRepo.GetKalkukationByID(CurrentProjektID);
            KalkulationView = (ListCollectionView)KalkulationViewSource.View;
            KalkulationDetailView = (BindingListCollectionView)KalkulationDetailViewSource.View;
            LieferantenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LieferantenViewSource")));
            LieferantenViewSource.Source = db.firmen.Where(id => id.istFirma == 1);
            SetsViewSource.Source = db.StammLagerorte.Where(n => n.istSet == 1);

            if (CurrentProjektID == 0)
            {
                addNewKalkulation(ProjektNummer);
                db.SaveChanges();
            }


            var con = new Comma2PointConverter();
            this.ftbAufschlag.SetBinding("procent", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged, con);
            var cbi = new C1.WPF.C1ComboBoxItem();

            // c1CboAufschlag.Items.Add((double)0);
            si.Add("0");

            for (int i = 100; i < 500; i += 5)
            {
                double res = (double)i / 10;
                //c1CboAufschlag.Items.Add(res);
                si.Add(res.ToString());
            }

            source = (IEnumerable<string>)si;
            ftbAufschlag.cBox.ItemsSource = source;

            try
            {
                KalkulationDetailView.CurrentChanged += new EventHandler(KalkulationDetailView_CurrentChanged);
                KalkulationView.CurrentChanged += new EventHandler(KalkulationView_CurrentChanged);
            }
            catch (Exception)
            {


            }
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var tools = new libc1DatagridTools.ManageC1GridColumns(c1GridDetais);
            tools.LoadGridSettings(Properties.Settings.Default.DatagridSettingsLagerliste);
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

        #endregion


        void db_SavingChanges(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted | System.Data.EntityState.Unchanged);



            foreach (var o in om)
            {

                if (o.Entity is kalkulationstabelle)
                {

                    var p = (kalkulationstabelle)o.Entity;
                    if (p.EntityState != System.Data.EntityState.Deleted)
                    {
                        p.created = DateTime.Now;
                        this.CurrentProjektID = p.id;
                    }
                }

            }
        }

        void KalkulationDetailView_CurrentChanged(object sender, EventArgs e)
        {
            CurrDetails = (kalkulationstabelle_detail)KalkulationDetailView.CurrentItem;
        }

        void KalkulationView_CurrentChanged(object sender, EventArgs e)
        {
            CurrDetails = (kalkulationstabelle_detail)KalkulationDetailView.CurrentItem;
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {

            var sb = new StringBuilder();
            var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);

            foreach (var o in om)
            {

                if (o.Entity is kalkulationstabelle)
                {

                    var p = (kalkulationstabelle)o.Entity;
                    if (p.EntityState != System.Data.EntityState.Deleted)
                    {
                        p.created = DateTime.Now;
                        this.CurrentProjektID = p.id;
                    }
                }

            }


            //db.SaveChanges();
            SaveChanges();

        }



        private void addNewKalkulation(string pNumber)
        {


            var k = new kalkulationstabelle();
            k.projektnummer = pNumber;
            k.euroumrechnung = .129;

            db.kalkulationstabellen.AddObject(k);
            db.SaveChanges();
            CurrentProjektID = k.id;
            FillList();


        }


        private void AddKalulationDetail()
        {
            if (this.KalkulationDetailView != null)
            {

                // SaveNewArtikel();



                var rech = (kalkulationstabelle_detail)this.KalkulationDetailView.AddNew();
                rech.idx = KalkulationDetailView.Count - 1;
                rech.Handelsware = 1;
                rech.umrechnungeuro = 0;
                rech.beschreibung = string.Empty;
                rech.einheit = string.Empty;
                rech.ausgeben = 1;

                this.KalkulationDetailView.CommitNew();


                this.c1GridDetais.SelectedIndex = c1GridDetais.Rows.Count() - 1;
                c1GridDetais.ScrollIntoView();

                // db.SaveChanges();
                // QueryTable(CurrentProjektID);

                //var x = rech.epnok.Value;
                // rech.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(rech_PropertyChanged);
                //CurrDetails = rech;
            }
        }


        private void AddKalulationDetail(DTO.dtoAddArtikel liste)
        {
            if (this.KalkulationDetailView != null)
            {
                var rech = (kalkulationstabelle_detail)this.KalkulationDetailView.AddNew();
                rech.idx = KalkulationDetailView.Count - 1;
                rech.id_Lagerliste = liste.id_Lagerliste;
                rech.menge = (double)liste.Menge;
                rech.artikelnr = liste.Artikelnummer;
                rech.einheit = liste.einheit;
                rech.beschreibung = liste.Bezeichnung;
                rech.umrechnungeuro = (double)liste.Bruttopreis;
                rech.id_Lieferant = liste.id_Lieferant;

                rech.Handelsware = liste.Handelsware.HasValue == false || liste.Handelsware == false ? (short)0 : (short)1;
                this.KalkulationDetailView.CommitNew();


                this.c1GridDetais.SelectedIndex = c1GridDetais.Rows.Count() - 1;
                c1GridDetais.ScrollIntoView();

            }
        }





        void rech_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                var o = (kalkulationstabelle_detail)sender;
                if (e.PropertyName == "epnok")
                {

                    o.umrechnungeuro = o.epnok.Value * .129;

                }
            }
            catch (Exception)
            {


            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            //AddKalulationDetail();


            var aa = new AddArtikel();
            aa.LagerlisteUpdated += new Action<DTO.dtoAddArtikel>(aa_LagerlisteUpdated);
            aa.ShowDialog();



        }

        void aa_LagerlisteUpdated(DTO.dtoAddArtikel obj)
        {
            AddKalulationDetail(obj);
        }

        private void DeleteDetails()
        {

            //if (this.KalkulationDetailView.IsAddingNew || this.KalkulationDetailView.IsEditingItem)
            //{
            //    MessageBox.Show("Einfügen oder Bearbeiten vor SPeichern beenden.");
            //}

            try
            {
                if (this.KalkulationDetailView.CurrentPosition > -1)
                {
                    if (MessageBox.Show("Eintrag wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        this.KalkulationDetailView.RemoveAt(this.KalkulationDetailView.CurrentPosition);
                        // this.AnlagenTypView.Remove(AnlagenTypView.CurrentItem);
                    }


                }
            }
            catch (Exception)
            {


            }

        }

        private void cmdDeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            DeleteDetails();
        }

        private void Details_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.c1GridDetais.SelectedItem = item.DataContext;


        }

        private void btnCopyProjekt_Click(object sender, RoutedEventArgs e)
        {

        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {

            double? SummeGesamt = 0;
            // double? SummeAngebot = 0;
            double? SummePos = 0;
            double? Aufschlag = 0;
            double? Einzelpreis = 0;
            double? GesamtPos = 0;
            double? ProzentVonGesamt = 0;
            double? AnteilTransport = 0;
            double? siPreis = 0;
            double? Rundungspreis = 0;


            try
            {

                //db.SaveChanges();

                if (SaveChanges())
                {



                    var query = from t in db.kalkulationstabelle_details where t.id_kalkulationstabelle == CurrentProjektID select t;
                    foreach (var item in query)
                    {
                        item.Recalc(true);
                        SummeGesamt += item.summeposition;

                    }

                    txtGesamtsumme.Text = SummeGesamt.ToString();

                    foreach (var item in query)
                    {

                        if ((item.summeposition.HasValue && item.summeposition != 0.0) && (SummeGesamt.HasValue && SummeGesamt != 0.0))
                        {

                            item.prozentvongesamt = (double)Math.Round((decimal)(item.summeposition * 100) / (decimal)SummeGesamt, 2);
                            item.transportanteil = (double)Math.Round(decimal.Parse(this.txtTransport.Text) * (decimal)(item.prozentvongesamt / 100), 2);
                            item.sieinzelpreis = (double)Math.Round((decimal)item.zuschlagpreis + (decimal)(item.transportanteil / item.menge), 2);
                            if (item.einzelpreis != null)
                            {
                                item.gesamtangebot = (double)Math.Round((decimal)item.einzelpreis * (decimal)item.menge, 2);
                                SummePos += item.gesamtangebot;
                                Aufschlag += item.zuschlag;
                                Einzelpreis += item.zuschlagpreis;
                                GesamtPos += item.gesamtpositionen;
                                ProzentVonGesamt += item.prozentvongesamt;
                                AnteilTransport += item.transportanteil;
                                siPreis += item.sieinzelpreis;
                                Rundungspreis += item.einzelpreis;
                            }
                        }
                    }

                    txtRundungspreis.Text = Rundungspreis.ToString();
                    txtGesamtPositionen.Text = SummePos.ToString();
                    // db.SaveChanges();
                    SaveChanges();

                }
            }
            catch (Exception)
            {


            }


        }



        private void CopyToReport_Click(object sender, RoutedEventArgs e)
        {
            if (txtID.Text != string.Empty)
            {
                var report = new Reports.CalcRepViewer(int.Parse(txtID.Text));
                report.ShowDialog();

            }

        }

        private void DataGridTextColumn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //MessageBox.Show("PropertyChanged");
        }

        private void c1GridDetais_BeginningNewRow(object sender, C1.WPF.DataGrid.DataGridBeginningNewRowEventArgs e)
        {
            bRowEditing = true;
        }

        private void c1GridDetais_BeginningRowEdit(object sender, C1.WPF.DataGrid.DataGridEditingRowEventArgs e)
        {
            // MessageBox.Show("BeginningRowEdit");
            bRowEditing = true;

        }


        private void c1GridDetais_CurrentCellChanged(object sender, C1.WPF.DataGrid.DataGridCellEventArgs e)
        {

            //if (e.Cell != null && bRowEditing)
            //{
            //    var r = e.Cell.Row.DataItem as kalkulationstabelle_detail;

            //    if (ArtState != ArtikelState.Done)
            //    {

            //        if (r.artikelnr != null)
            //        {
            //            using (var db = new SteinbachEntities())
            //            {
            //                var art = db.lagerlisten.Where(n => n.artikelnr == r.artikelnr.Trim()).DefaultIfEmpty(null).First();
            //                if (art != null && ArtState != ArtikelState.ArtikelNew)
            //                {
            //                    ArtState = ArtikelState.Done;

            //                    if (r.beschreibung == null)
            //                    {
            //                        r.beschreibung = art.beschreibungeng;
            //                    }
            //                    if (r.umrechnungeuro != null)
            //                    {
            //                        r.umrechnungeuro = (double)art.preisbrutto;
            //                    }
            //                    if (r.id_Lagerliste != null)
            //                    {
            //                        r.id_Lagerliste = art.id;
            //                    }
            //                }
            //                else if (art == null)
            //                {
            //                    ArtState = ArtikelState.ArtikelNew;
            //                }

            //            }


            //            if (ArtState == ArtikelState.ArtikelNew)
            //            {

            //            }

            //        }


            //    }




            //if (r.artikelnr != null && !bSetArtikel)
            //{
            //    using (SteinbachEntities dbLocal = new SteinbachEntities())
            //    {

            //if (r.beschreibung == null)
            //{
            //    var q = from b in dbLocal.lagerlisten where b.artikelnr == r.artikelnr select b.bezeichnung;
            //    if (q.Count() > 0)
            //        r.beschreibung = q != null ? q.FirstOrDefault() : "";

            //}



            //if (r.umrechnungeuro != null)
            //{


            //    if (!dbLocal.lagerlisten.Any(d => d.artikelnr == r.artikelnr))
            //    {
            //        if (MessageBox.Show("Artikelnummer nicht in Lagerliste vorhanden. Anlegen?", "Sicherheitsabfrage", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //        {
            //            var ll = new lagerliste();
            //            ll.artikelnr = r.artikelnr;
            //            ll.bezeichnung = r.beschreibung ?? "";
            //            ll.preiseuro = r.umrechnungeuro;
            //            dbLocal.AddTolagerlisten(ll);


            //        }
            //    }
            //    else
            //    {
            //        var preis = from p in dbLocal.lagerlisten where p.artikelnr == r.artikelnr select p;
            //        if (preis != null)
            //        {
            //            foreach (var item in preis)
            //            {
            //                if (r.umrechnungeuro > item.preiseuro || item.preiseuro == null)
            //                {
            //                    if (MessageBox.Show("Preis in Kalkulation ist höher als der Preis in der Lagerliste. Anpassen?", "Sicherheitsabfrage", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //                    {
            //                        item.preiseuro = r.umrechnungeuro;
            //                    }
            //                }

            //            }

            //        }
            //    }

            //}

            //        dbLocal.SaveChanges();
            //    }
            //    }

            //}
        }



        private void c1GridDetais_CommittingNewRow(object sender, C1.WPF.DataGrid.DataGridEndingNewRowEventArgs e)
        {
            bRowEditing = false;
            // SaveNewArtikel();
        }

        private void SaveNewArtikel()
        {

            try
            {
                if (ArtState == ArtikelState.ArtikelNew)
                {
                    if (ThisArtikel != null)
                    {
                        using (var dbTemp = new SteinbachEntities())
                        {
                            db.AddTolagerlisten(ThisArtikel);
                            db.SaveChanges();
                        }
                    }


                }
            }
            catch (Exception)
            {

            }
            finally
            {
                ArtState = ArtikelState.NoState;
                ThisArtikel = null;
            }




        }



        private bool DoCancel()
        {
            var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
            if (om.Count() > 0)
            {
                var res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (res == MessageBoxResult.Yes)
                {
                    if (SaveChanges())
                    {
                        return true;
                        //db.Dispose();
                        //this.Close();

                    }
                    else
                    {
                        return false;
                    }

                }

                else if (res == MessageBoxResult.No)
                {
                    return true;
                    //db.Dispose();
                    //this.Close();

                }

                else if (res == MessageBoxResult.Cancel)
                {
                    return false;
                }
                else
                {
                    return false;
                }

            }


            else
            {

                return true;
            }

        }


        private bool SaveChanges()
        {
            try
            {
                //if (this.KalkulationDetailView.IsEditingItem)
                //{
                //    this.KalkulationDetailView.CommitEdit();
                //}
                //if (this.KalkulationDetailView.IsAddingNew)
                //{
                //    this.KalkulationDetailView.CommitNew();
                //}


                var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
                foreach (var o in om)
                {

                    if (o.Entity is kalkulationstabelle)
                    {

                        var p = (kalkulationstabelle)o.Entity;
                        if (p.EntityState == System.Data.EntityState.Added || p.EntityState == System.Data.EntityState.Modified)
                        {


                            if (this.txtProjektnummer.Text == string.Empty)
                            {
                                MessageBox.Show("Es muss eine Projektnummer eingegeben werden");
                                return false;
                            }
                        }
                    }


                }

                db.SaveChanges();
                //db.Dispose();
                return true;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));
                return false;
            }





        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            try
            {
                var tools = new libc1DatagridTools.ManageC1GridColumns(c1GridDetais);
                Properties.Settings.Default.DatagridSettingsLagerliste = tools.SaveDataGridSettings();
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex, "Fehler beim Abspeichern der Datagrideinstellungen");
            }

            // SaveNewArtikel();

            if (!DoCancel())
                e.Cancel = true;
        }

        private void c1GridDetais_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bRowEditing)
                {


                    var grid = (C1.WPF.DataGrid.C1DataGrid)e.Source;
                    var r = (kalkulationstabelle_detail)grid.CurrentRow.DataItem;
                    string CellName = grid.CurrentCell.Column.Name;


                    if (grid.CurrentCell.Value != null)
                    {

                        string CellValue = grid.CurrentCell.Value.ToString();

                        using (SteinbachEntities dbLocal = new SteinbachEntities())
                        {

                            if (CellName == "artikelnr")
                            {
                                try
                                {
                                    //if (ArtState == ArtikelState.NoState)
                                    //{
                                    var q = from b in dbLocal.lagerlisten where b.artikelnr == CellValue select b;
                                    if (q.Count() > 0)
                                    {
                                        ArtState = ArtikelState.Done;
                                        r.beschreibung = q.FirstOrDefault().beschreibungeng != null ? q.FirstOrDefault().beschreibungeng : "";
                                        r.umrechnungeuro = q.FirstOrDefault().preisbrutto.HasValue ? (double)q.FirstOrDefault().preisbrutto : 0;
                                        r.einheit = q.FirstOrDefault().einheit != null ? q.FirstOrDefault().einheit : "";
                                        r.id_Lieferant = q.FirstOrDefault().id_lieferant.HasValue ? q.FirstOrDefault().id_lieferant : null;


                                    }
                                    //else
                                    //{
                                    //    ArtState = ArtikelState.ArtikelNew;
                                    //    ThisArtikel = new lagerliste();
                                    //    ThisArtikel.artikelnr = CellValue;
                                    //    grid.CurrentCell.Presenter.Background=Brushes.Yellow;
                                    //}
                                    //}
                                }
                                catch (Exception)
                                {
                                }



                            }
                            //else
                            //{
                            //    try
                            //    {
                            //        if (ArtState == ArtikelState.ArtikelNew)
                            //        {
                            //            if (CellName == "beschreibung")
                            //            {
                            //                ThisArtikel.beschreibungeng = CellValue;
                            //                ThisArtikel.bezeichnung = CellValue;
                            //            }

                            //            if (CellName == "umrechnungeuro")
                            //            {
                            //                ThisArtikel.preisbrutto = decimal.Parse(CellValue);
                            //            }

                            //            if (CellName == "id_Lieferant")
                            //            {
                            //                ThisArtikel.id_lieferant = int.Parse(CellValue);
                            //            }

                            //            if (CellName == "einheit")
                            //            {
                            //                ThisArtikel.einheit = CellValue;
                            //            }


                            //        }
                            //    }

                            //catch (Exception)
                            //{


                            //}


                        }








                    }
                }
            }
            //}
            catch (Exception)
            {


            }


        }


        private void btnAddEmptyPos_Click(object sender, RoutedEventArgs e)
        {
            AddKalulationDetail();
        }

        private void chkAusgabe_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in KalkulationDetailView)
            {
                var buf = (kalkulationstabelle_detail)item;
                buf.ausgeben = 1;
            }
        }

        private void chkAusgabe_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in KalkulationDetailView)
            {
                var buf = (kalkulationstabelle_detail)item;
                buf.ausgeben = 0;
            }
        }

        private void cboSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                var lo = (StammLagerorte)e.AddedItems[0];

                foreach (var item in lo.Lagerbestaende)
                {

                    try
                    {
                        if (this.KalkulationDetailView != null)
                        {
                            var rech = (kalkulationstabelle_detail)this.KalkulationDetailView.AddNew();
                            rech.idx = KalkulationDetailView.Count - 1;

                            this.KalkulationDetailView.CommitNew();
                            rech.artikelnr = item.Artikelnummer;
                            rech.menge = item.Sollbestand;
                            rech.beschreibung = item.Bezeichnung;
                            rech.id_Lagerliste = item.id_Lagerliste;
                            var art = db.lagerlisten.Where(n => n.id == item.id_Lagerliste).SingleOrDefault();
                            if (art != null)
                            {
                                rech.Handelsware = art.Handelsware;
                                rech.id_Lieferant = art.id_lieferant;
                                rech.umrechnungeuro = (double)art.preisbrutto;
                                rech.einheit = art.einheit;
                            }



                        }
                    }
                    catch (Exception ex)
                    {

                        CommonTools.Tools.UserMessage.NotifyUser(ex.Message);
                    }

                }

                this.c1GridDetais.SelectedIndex = c1GridDetais.Rows.Count() - 1;
                c1GridDetais.ScrollIntoView();
            }
        }

    }
}
