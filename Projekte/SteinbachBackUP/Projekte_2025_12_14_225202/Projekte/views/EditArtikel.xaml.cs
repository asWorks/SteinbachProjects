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
using ProjektDB.ObjectCollections;
using System.Collections;
using System.Collections.ObjectModel;
namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for EditArtikel.xaml
    /// </summary>
    public partial class EditArtikel : Window
    {

        public enum BewegungsArt
        {
            Zugang,
            Abgang
        }

        private enum rbRangeListe
        {
            ToDay = 1,
            Weeks4 = 30,
            Month3 = 90,
            Month6 = 180,
            All = 18500

        }

        SteinbachEntities db;
        SteinbachEntities dbArt;
        int ID = 0;
        CollectionViewSource ViewSource;
        private ListCollectionView View;
        CollectionViewSource DetailsViewSource;
        private ListCollectionView DetailsView;
        CollectionViewSource UnterartikelViewSource;
        private ListCollectionView UnterartikelView;
        CollectionViewSource ParentartikelViewSource;
        CollectionViewSource OberArtikelViewSource;
        CollectionViewSource LagerortViewSource;
        CollectionViewSource BewegungsartViewSource;
        CollectionViewSource LieferantenViewSource;
        CollectionViewSource KategorieViewSource;

        private string ArtikelInput = string.Empty;

        public EditArtikel()
        {
            InitializeComponent();
        }

        public EditArtikel(int id)
        {
            ID = id;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillProject();

        }
        private void FillProject()
        {
            db = new SteinbachEntities();
            dbArt = new SteinbachEntities();
            ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ThisViewSource")));
            DetailsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("DetailsViewSource")));
            UnterartikelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("UnterartikelViewSource")));
            ParentartikelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ParentartikelViewSource")));
            OberArtikelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("OberArtikelViewSource")));
            LagerortViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerortViewSource")));
            BewegungsartViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("BewegungsartViewSource")));
            LieferantenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LieferantenViewSource")));
            KategorieViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("KategorieViewSource")));


            var pTemplate = (DataTemplate)this.FindResource("ParentsTemplate");
            var oaTemplate = (DataTemplate)this.FindResource("OberArtikelTemplate");

            //OberartkelComboBox.onfcbChanged += new UserControls.FilteredComboBox.FiteredBoxChanged(OberartkelComboBox_onfcbChanged);
            //OberartkelComboBox.OnFcb_SelectionChanged += new UserControls.FilteredComboBox.FilteredBoxSelectionChanged(OberartkelComboBox_OnFcb_SelectionChanged);

            //OberartkelComboBox.SetSimpleBinding("id_parent", "id", pTemplate, BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);


            //fcbArtikel.cBoxItemTemplate = pTemplate;
            //fcbArtikel.onfcbChanged += new UserControls.FilteredComboBox.FiteredBoxChanged(fcbArtikel_onfcbChanged);
            //fcbArtikel.OnFcb_SelectionChanged += new UserControls.FilteredComboBox.FilteredBoxSelectionChanged(fcbArtikel_OnFcb_SelectionChanged);


            LieferantenViewSource.Source = db.firmen.Where(id => id.istFirma == 1);
            KategorieViewSource.Source = CommonTools.Tools.HelperTools.GetAuswahlEintraege("ArtikelKategorie", 1);
            var WaehrungenLookUp = ((CollectionViewSource)(this.FindResource("WaehrungenLookUp")));
            var curQuery = from c in db.StammWaehrungen select c;
            var cQ = db.StammWaehrungen;
            WaehrungenLookUp.Source = curQuery;



            if (ID == 0)
            {


                var Query = from n in db.lagerlisten
                            select n;

                ViewSource.Source = new ArtikelCollection(Query, db);
                View = (ListCollectionView)ViewSource.View;
                var i = (lagerliste)View.AddNew();
                i.bezeichnung = "Artikel Neu";
                i.einheit = "Pcs";
                View.CommitNew();

                c1GridDetails.Visibility = System.Windows.Visibility.Hidden;

                // bNew = true;

            }
            else
            {
                var Query = from n in db.lagerlisten
                            where n.id == ID
                            select n;

                ViewSource.Source = Query;


                CheckStandardRadioButtonListeBewegungen();

                // LoadBewegungen(90);

                //this.fcbArtikel.ComboBoxViewSource = FillParentArtikelListe("", ID);
                // this.OberartkelComboBox.ComboBoxViewSource = FillParentArtikelListe("", ID);
                c1GridDetails.Visibility = System.Windows.Visibility.Visible;
                var tools = new libc1DatagridTools.ManageC1GridColumns(c1GridDetails);
                tools.LoadGridSettings(Properties.Settings.Default.DatagridSettingsArtikelDetails);

            }
        }

        private void CheckStandardRadioButtonListeBewegungen()
        {
            //var res = CommonTools.Tools.ConfigEntry<int>.GetConfigEntry("StandardRadioButtonListeBewegungen", "30", "Mogliche Werte = 1,30,90,180,18000");
            var res = Properties.Settings.Default.rbRangeListeArtikelbewegungen;

            switch (res)
            {

                case (int)rbRangeListe.ToDay:
                    {
                        rbRangeListeToDay.IsChecked = true;
                        break;
                    }
                case (int)rbRangeListe.Weeks4:
                    {
                        rbRangeListe4Weeks.IsChecked = true;
                        break;
                    }
                case (int)rbRangeListe.Month3:
                    {
                        rbRangeListe3Month.IsChecked = true;
                        break;
                    }
                case (int)rbRangeListe.Month6:
                    {
                        rbRangeListe6Month.IsChecked = true;
                        break;
                    }
                case (int)rbRangeListe.All:
                    {
                        rbRangeListeAll.IsChecked = true;
                        break;
                    }

                default:
                    break;
            }

        }

        /// <summary>
        /// Bewegungen laden
        /// </summary>
        /// <param name="daysLookBack"> Wieviel Tage rückwirkend laden</param>
        private void LoadBewegungen(int daysLookBack)
        {

            DateTime dlb = DateTime.Now.AddDays(daysLookBack * -1);


            var dQuery = from l in db.lagerliste_addremove
                         where l.id_lagerliste == ID && l.created >= dlb
                         orderby l.id descending
                         select l;


            var test = new ObservableCollection<ViewModels.lagerliste_addremoveViewModel>();
            foreach (var item in dQuery)
            {
                var lar = new ViewModels.lagerliste_addremoveViewModel(item.id);
                test.Add(lar);
            }


            OberArtikelViewSource.Source = FillParentArtikelListe("", ID);
            RefreshUnterartikelListe();

            //LagerortViewSource.Source = db.StammLagerorte;
            //BewegungsartViewSource.Source = db.StammBewegungsarten;


            //DetailsViewSource.Source = new LagerlisteAddRemoveCollection(dQuery, db);
            // DetailsViewSource.Source = new ObservableCollection<vw_Lagerlisten_AddRemove>(dQuery);
            DetailsViewSource.Source = test;
            DetailsView = (ListCollectionView)DetailsViewSource.View;
        }


        void OberartkelComboBox_OnFcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var cbo = (ComboBox)sender;
            //var i = cbo.SelectedIndex;
            //var x = (lagerliste)cbo.SelectedItem;

            ////var l = dbArt.lagerlisten.Where(p => p.id == x.id).FirstOrDefault();

            //if (x.id_parent != ID)
            //{
            //    x.id_parent = ID;
            //}

            //dbArt.SaveChanges();
        }

        void OberartkelComboBox_onfcbChanged(object sender, UserControls.FilteredComboBoxChangedEventArgs e)
        {
            string filter = e.filter;
            //OberartkelComboBox.ComboBoxViewSource = FillParentArtikelListe(filter, ID);
        }

        void RefreshUnterartikelListe()
        {
            var uaQuery = from u in dbArt.lagerlisten
                          where u.id_parent == ID
                          select u;
            UnterartikelViewSource.Source = new LagerlisteCollection(uaQuery, dbArt);
            UnterartikelView = (ListCollectionView)UnterartikelViewSource.View;

            //var pr = new Repositories.ProjektRepository(dbArt);
            //var loa = pr.GetLagerListenMitOberArtikel("Screw",ID);
            //foreach (Tools.LagerListeOberArtikel item in loa)
            //{
            //    Console.WriteLine(item.bezeichnung, item.beschreibung, item.oberartikel);
            //}


        }

        IOrderedQueryable FillOberArtikelListe(string filter, int id)
        {
            var pr = new Repositories.ProjektRepository(dbArt);
            IOrderedQueryable paQuery;
            if (filter.Equals(string.Empty))
            {
                paQuery = (IOrderedQueryable)pr.GetLagerListenGroupBezeichnung(string.Empty, id);

            }
            else
            {
                string f = filter;
                paQuery = (IOrderedQueryable)pr.GetLagerListenGroupBezeichnung(filter, id);
            }

            return paQuery;

        }

        IOrderedQueryable FillParentArtikelListe(string filter, int id)
        {
            var pr = new Repositories.ProjektRepository(dbArt);
            IOrderedQueryable paQuery;
            if (filter.Equals(string.Empty))
            {
                paQuery = (IOrderedQueryable)pr.GetLagerListenMitOberArtikel(string.Empty, id);
                //paQuery = from p in dbArt.lagerlisten
                //          where p.id_parent != ID
                //          orderby p.bezeichnung
                //          select p;

            }
            else
            {
                string f = filter;
                paQuery = (IOrderedQueryable)pr.GetLagerListenMitOberArtikel(filter, id);

                //paQuery = from p in dbArt.lagerlisten
                //          where p.id_parent != ID && p.bezeichnung.Contains(f)
                //          orderby p.bezeichnung
                //          select p;


            }

            return paQuery;



        }


        void fcbArtikel_OnFcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var cbo = (ComboBox)sender;
            //var i = cbo.SelectedIndex;
            var x = (Tools.LagerListeOberArtikel)cbo.SelectedItem;
            var l = dbArt.lagerlisten.Where(p => p.id == x.id).FirstOrDefault();

            if (MessageBox.Show(string.Format("Artikel {0} als Unterartikel hinzufügen ?", x.bezeichnung), "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                if (x.id_parent != ID)
                {
                    l.id_parent = ID;
                }
            }


            dbArt.SaveChanges();
            RefreshUnterartikelListe();



        }

        void fcbArtikel_onfcbChanged(object sender, UserControls.FilteredComboBoxChangedEventArgs e)
        {
            string filter = e.filter;
            //fcbArtikel.ComboBoxViewSource = FillParentArtikelListe(filter, ID);

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveChanges())
            {
                if (c1GridDetails.Visibility == System.Windows.Visibility.Hidden)
                {
                    ID = txtid.Text == "" ? 0 : int.Parse(txtid.Text);
                    FillProject();
                }
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // SaveChanges();
            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (DoCancel())
            {
                db.Dispose();
                this.Close();

            }


        }


        private void c1GridDetails_BeginningNewRow(object sender, C1.WPF.DataGrid.DataGridBeginningNewRowEventArgs e)
        {
            var lar = e.Item as lagerliste_addremove;
            lar.id_lagerliste = ID;
            lar.created = DateTime.Now;

        }

        private void c1GridDetails_CommittedNewRow(object sender, C1.WPF.DataGrid.DataGridRowEventArgs e)
        {



        }

        private void c1GridDetails_CommittingNewRow(object sender, C1.WPF.DataGrid.DataGridEndingNewRowEventArgs e)
        {

            var lar = e.NewRow.DataItem as lagerliste_addremove;

            if (lar.anzahl == null || lar.anzahl < 1)
            {
                MessageBox.Show("Bitte Bewegungsmenge prüfen. Nur positive Eingaben sind möglich.");
                e.Cancel = true;
            }
            else
            {

                if (lar.type != "Verkauf" && lar.type != "Kommissionsware")
                {
                    MessageBox.Show("Bitte den Bewegungstyp angeben.");
                    e.Cancel = true;
                }
                else
                {
                    if (lar.addtype == null)
                    {
                        MessageBox.Show("Bitte Zugang/Abgang wählen.");
                        e.Cancel = true;
                    }
                    else
                    {
                        if (!EditStoreData(lar.addtype == "ab" ? BewegungsArt.Abgang : BewegungsArt.Zugang, lar))
                        {
                            //MessageBox.Show("Bewegung konnte wegen mangelnden Lagerbestandes nicht durchgeführt werden");
                            e.Cancel = true;
                        }

                    }

                }



            }

        }

        private bool EditStoreData(BewegungsArt bewegungsart, lagerliste_addremove lar)
        {

            //int result = 0;
            //int ba = (bewegungsart == BewegungsArt.Zugang ? 1 : -1);


            //int menge = (int)lar.anzahl * ba;

            //if (lar.type == "Verkauf")
            //{
            //    //result += addValue(menge, txtAnzahlKauf);
            //    //result += addValue(menge, txtAnzahlLager);
            //    return AddStockItemEntry(menge, txtAnzahlLager, txtAnzahlKauf);
            //}
            //else if (lar.type == "Kommissionsware")
            //{
            //    //result += addValue(menge, txtAnzahlLager);
            //    //result += addValue(menge, txtAnzahlKomm);
            //    return AddStockItemEntry(menge, txtAnzahlLager, txtAnzahlKomm);
            //}

            //if (result > 0)
            //{
            //    return false;
            //}

            return true;

        }

        /// <summary>
        /// Lagerbuchung durchführen immer für Lager und entweder Verkauf oder Kommissionsware  
        /// </summary>
        /// <param name="value">Bewegungsmenge</param>
        /// <param name="Lager">Lager Allgemein</param>
        /// <param name="LagerArt">Lager speziell</param>
        /// <returns></returns>
        private bool AddStockItemEntry(int value, TextBox Lager, TextBox LagerArt)
        {
            int lg = Lager.Text == "" ? 0 : int.Parse(Lager.Text);
            int la = LagerArt.Text == "" ? 0 : int.Parse(LagerArt.Text);
            if (lg + value < 0 || la + value < 0)
            {
                var q = from c in db.config where c.mkey == "NegativeLagerbestaende" select c.value;
                var r = int.Parse(q.FirstOrDefault());


                if (r == 0)
                {
                    MessageBox.Show("Bewegung konnte wegen mangelnden Lagerbestandes nicht durchgeführt werden");
                    return false;
                }


            }

            lg += value;
            la += value;
            Lager.Text = lg.ToString();
            LagerArt.Text = la.ToString();
            return true;


        }

        //private int addValue(int value, TextBox tb)
        //{
        //    int n = int.Parse(tb.Text);
        //    if (n + value < 0)
        //    {
        //        return 1;
        //    }

        //    n += value;
        //    tb.Text = n.ToString();
        //    return 0;

        //}



        private void c1GridDetails_DeletingRows(object sender, C1.WPF.DataGrid.DataGridDeletingRowsEventArgs e)
        {


            if (MessageBox.Show(string.Format("Möchten Sie die Bewegung {0} wirklich löschen ?", string.Join(",", e.DeletedRows.Select(row => (row.DataItem as lagerliste_addremove).id).ToArray())), "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {

                foreach (var item in e.DeletedRows.Select(row => (row.DataItem as lagerliste_addremove)).ToArray())
                {
                    if (item.type != "Verkauf" && item.type != "Kommissionsware")
                    {
                        MessageBox.Show("Bitte den Bewegungstyp angeben.");
                        e.Cancel = true;
                    }
                    else
                    {
                        //
                        if (!EditStoreData(item.addtype == "ab" ? BewegungsArt.Zugang : BewegungsArt.Abgang, item))
                        {
                            // MessageBox.Show("Bewegung konnte wegen mangelnden Lagerbestandes nicht durchgeführt werden");
                            e.Cancel = true;
                        };
                    }
                }

            };
            //}
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            AcceptNewRow();

        }

        private void AcceptNewRow()
        {
            c1GridDetails.EndNewRow(true);
        }

        private void c1GridDetails_BeganEdit(object sender, C1.WPF.DataGrid.DataGridBeganEditEventArgs e)
        {

        }

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {

            ID = 0;
            FillProject();

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
                AcceptNewRow();
                var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
                foreach (var o in om)
                {

                    if (o.Entity is lagerliste)
                    {

                        var p = (lagerliste)o.Entity;
                        if (p.EntityState == System.Data.EntityState.Added)
                        {
                            bool KeyInUse = db.lagerlisten.Any(c => c.bezeichnung == p.bezeichnung);


                            if (KeyInUse)
                            {
                                MessageBox.Show(String.Format("Der Name {0} wird schon benutzt.", p.bezeichnung));
                                return false;
                            }
                        }
                    }


                }

                db.SaveChanges();
                // db.Dispose();
                return true;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }





        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AcceptNewRow();
            txtid.Focus();
            var tools = new libc1DatagridTools.ManageC1GridColumns(c1GridDetails);
            string result = tools.SaveDataGridSettings();
            Properties.Settings.Default.DatagridSettingsArtikelDetails = result;
            Properties.Settings.Default.Save();


            if (!DoCancel())
                e.Cancel = true;
        }



        private void c1GridUnterartikel_DeletingRows(object sender, C1.WPF.DataGrid.DataGridDeletingRowsEventArgs e)
        {
            if (MessageBox.Show(string.Format("Möchten Sie den Artikel {0} wirklich aus der Unterartikelliste entfernen ?", string.Join(",", e.DeletedRows.Select(row => (row.DataItem as lagerliste).bezeichnung).ToArray())), "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {

                foreach (var item in e.DeletedRows.Select(row => (row.DataItem as lagerliste)).ToArray())
                {
                    item.id_parent = 0;
                }
            }

            e.Cancel = true;
            dbArt.SaveChanges();
            RefreshUnterartikelListe();

        }




        private void c1GridDetails_CommittingEdit(object sender, C1.WPF.DataGrid.DataGridEndingEditEventArgs e)
        {
            //var buf = (lagerliste_addremove)e.Row.DataItem;
            //if (buf.addtype == "um")
            //{
            //    this.dgcbcQuelllager.Visibility = System.Windows.Visibility.Visible;
            //}
            //else
            //{
            //    this.dgcbcQuelllager.Visibility = System.Windows.Visibility.Collapsed;
            //}
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {

            using (var test = new SteinbachEntities())
            {
                var beleg = new SI_Belege();
                beleg.id_Quelllager = 1;
                beleg.id_Ziellager = 2;
                test.AddToSI_Belege(beleg);

                var bwg = test.lagerliste_addremove.Where(i => i.id == 16471).SingleOrDefault();
                bwg.id_Quelllager = 2;
                bwg.id_Ziellager = 1;
                //test.AddTolagerliste_addremove(bwg);

                test.SaveChanges();


            }

        }

        private void c1GridDetails_CommittingRowEdit(object sender, C1.WPF.DataGrid.DataGridEditingRowEventArgs e)
        {

        }

        private void rbRangeListe_Checked(object sender, RoutedEventArgs e)
        {
            var S = (RadioButton)sender;
            int Result = 0;

            switch (S.Name)
            {
                case "rbRangeListeToDay":
                    {
                        Result = (int)rbRangeListe.ToDay;
                        //LoadBewegungen(1);
                        break;
                    }

                case "rbRangeListe4Weeks":
                    {
                        Result = (int)rbRangeListe.Weeks4;
                        //LoadBewegungen(30);
                        break;
                    }
                case "rbRangeListe3Month":
                    {
                        Result = (int)rbRangeListe.Month3;
                        //LoadBewegungen(90);
                        break;
                    }

                case "rbRangeListe6Month":
                    {
                        Result = (int)rbRangeListe.Month6;
                        //LoadBewegungen(180);
                        break;
                    }
                case "rbRangeListeAll":
                    {
                        Result = (int)rbRangeListe.All;
                        // LoadBewegungen(18500);
                        break;
                    }
                default:
                    {
                        Result = (int)rbRangeListe.Weeks4;
                        break;
                    }

            }

            LoadBewegungen(Result);
            Properties.Settings.Default.rbRangeListeArtikelbewegungen = Result;
          //  Properties.Settings.Default.Save();


        }


    }
}
