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
using System.Collections.ObjectModel;
using System.Collections;
using WaWi.Lagerbuchungen.Lagerbuchungen;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;






namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for EditArtikel.xaml
    /// </summary>
    public partial class EditBelege : Window
    {

      

        SteinbachEntities db;
       
        CollectionViewSource LagerListeViewSource;
        CollectionViewSource LagerListeFullViewSource;
       

        ViewModels.SI_BelegeViewModel vModel;


        #region Constructors
        /// <summary>
        /// Beleg in neue Belegart übernehmen
        /// </summary>
        /// <param name="id"></param>
        /// <param name="SourceBelegId"></param>
        /// <param name="ba"></param>
        public EditBelege(int SourceBelegId, StammBelegarten ba)
        {

            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            try
            {
                db = new SteinbachEntities();

                CommonTools.GlobalStopwatch.ReStartWatch();
                var task = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {

                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (new Action(() =>
                    {

                        vModel = new ViewModels.SI_BelegeViewModel(0, this.Dispatcher, Cursor, SourceBelegId, ba);
                        Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);

                        LagerListeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerListeViewSource")));
                        LagerListeFullViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerListeFullViewSource")));
                        LagerListeViewSource.Source = db.lagerlisten.Where(n => n.artikelnr == "0");
                        LoadLagerlisteFullViewSource(db);
                    
                        Cursor = System.Windows.Input.Cursors.Wait;

                    

                        this.ListboxBelegeTextbausteineVM.DataContext = vModel.ListboxBelegeTextbausteineVM;
                        Cursor = System.Windows.Input.Cursors.Arrow;

                    })));



                });
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }


        }

        private void LoadLagerlisteFullViewSource(SteinbachEntities DBContext)
        {
            LagerListeFullViewSource.Source = from l in DBContext.lagerlisten
                                              select new Models.LagerListeTemplateSource
                                              {
                                                  id = l.id,
                                                  artikelnr = l.artikelnr,
                                                  bezeichnung = l.bezeichnung,
                                                  anzahlauflager = l.anzahlauflager
                                              };
        }





        public EditBelege(int id)
        {

            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            try
            {
                db = new SteinbachEntities();

                CommonTools.GlobalStopwatch.ReStartWatch();
                var task = System.Threading.Tasks.Task.Factory.StartNew(() =>
                     {

                         this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (new Action(() =>
                         {



                             vModel = new ViewModels.SI_BelegeViewModel(id, this.Dispatcher, Cursor);
                             Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);

                             vModel.RingArtikelFullViewSourceChanged += () =>
                             {
                                 LagerListeFullViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerListeFullViewSource")));
                                 using (var context = new SteinbachEntities())
                                 {
                                     LoadLagerlisteFullViewSource(context);
                                      //LagerListeFullViewSource.Source = context.lagerlisten;
                                 }
                                
                             };


                             LagerListeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerListeViewSource")));
                             LagerListeFullViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerListeFullViewSource")));
                             LagerListeViewSource.Source = db.lagerlisten.Where(n => n.artikelnr == "0");
                             LagerListeFullViewSource.Source = db.lagerlisten;
                           
                             Cursor = System.Windows.Input.Cursors.Wait;


                             this.ListboxBelegeTextbausteineVM.DataContext = vModel.ListboxBelegeTextbausteineVM;
                             Cursor = System.Windows.Input.Cursors.Arrow;

                         })));



                     });
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }


        }

        public EditBelege()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            var vModel = new ViewModels.SI_BelegeViewModel();
            Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);
        }

        private IEnumerable fcbArtikelnummer_OnFCBChangedFunc(DataTypes.FilteredComboBoxChangedEventArgs arg)
        {
            try
            {
                int res = 0;
                if (int.TryParse(arg.filter, out res) == true)
                {


                   // return db.lagerlisten.Where(n => n.artikelnr.Contains(arg.filter));
                    var query = from l in db.lagerlisten
                                where l.artikelnr.Contains(arg.filter)
                                select new Models.LagerListeTemplateSource
                                {
                                    id = l.id,
                                    artikelnr = l.artikelnr,
                                    bezeichnung = l.bezeichnung,
                                    anzahlauflager = l.anzahlauflager
                                };

     
                    return query;

                }
                else
                {
                   // return db.lagerlisten.Where(n => n.bezeichnung.Contains(arg.filter));
                    var query = from l in db.lagerlisten
                                where l.bezeichnung.Contains(arg.filter)
                                select new Models.LagerListeTemplateSource
                                {
                                    id = l.id,
                                    artikelnr = l.artikelnr,
                                    bezeichnung = l.bezeichnung,
                                    anzahlauflager = l.anzahlauflager
                                };


                    return query;
                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
                return null;
            }

        }

        #endregion
        #region PreparingWindow
        private void FillProject()
        {
            //db = new SteinbachEntities();
            //dbArt = new SteinbachEntities();
            //ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ThisViewSource")));
            //DetailsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("DetailsViewSource")));
            //LagerortViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerortViewSource")));
            //BewegungsartViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("BewegungsartViewSource")));
            //ProjekteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ProjekteViewSource")));
            //BelegartenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("BelegartenViewSource")));
            //LagerListeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerListeViewSource")));
            //LagerListeFullViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerListeFullViewSource")));
            //SpracheViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SpracheViewSource")));
            //KalkulationenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("KalkulationenViewSource")));
            //FirmenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("FirmenViewSource")));

            //LagerortViewSource.Source = db.StammLagerorte;
            //BewegungsartViewSource.Source = db.StammBewegungsarten;
            //BelegartenViewSource.Source = db.StammBelegarten;
            //string buf = DateTime.Now.Year.ToString().Substring(2);
            //ProjekteViewSource.Source = db.projekte.Where(n => n.projektnummer.StartsWith(buf));

            //LagerListeViewSource.Source = db.lagerlisten.Where(n => n.artikelnr == "0");
            //LagerListeFullViewSource.Source = db.lagerlisten;
            //SpracheViewSource.Source = CommonTools.Tools.HelperTools.GetAuswahlEintraege("TypSprache", 1);
            //FirmenViewSource.Source = db.firmen.Where(n => n.IstKunde == 1);




            //if (ID == 0)
            //{


            //    var Query = from n in db.SI_Belege
            //                where n.id == 0
            //                select n;

            //    ViewSource.Source = new SI_BelegeCollection(Query, db);
            //    View = (ListCollectionView)ViewSource.View;
            //    var i = (SI_Belege)View.AddNew();
            //    i.Belegnummer = "Neu";
            //    i.created = DateTime.Now;
            //    i.Belegdatum = DateTime.Now;
            //    i.id_Ziellager = 1;
            //    i.id_Quelllager = 4;
            //    i.Belegart = "zu";
            //    i.istGebucht = 0;
            //    i.id_Sprache = 1;

            //    View.CommitNew();
            //    DetailsView = (BindingListCollectionView)DetailsViewSource.View;




            //    //Debug.WriteLine("Entered Belege");
            //    //Trace.WriteLine("Me too");
            //    //DAL.Tools.LoggingTool.LogMessage("I am the logging tool", "EditBelege", "FillProjekt", DAL.Tools.LoggingTool.LogState.high);


            //}
            //else
            //{
            //var Query = from n in db.SI_Belege
            //            where n.id == ID
            //            select n;

            //ViewSource.Source = new SI_BelegeCollection(Query, db);
            //View = (ListCollectionView)ViewSource.View;
            //var dQuery = from l in db.SI_BelegePositionen
            //             where l.id_Beleg == ID
            //             select l;


            //lblFirma.Content = Query.SingleOrDefault().StammBelegarten.BeziehungPartner;


            //BewegungsartViewSource.Source = db.StammBewegungsarten;


            //// DetailsViewSource.Source = new SI_BelegePositionenCollection(dQuery, db);
            //DetailsView = (BindingListCollectionView)DetailsViewSource.View;



            // c1GridDetails.Visibility = System.Windows.Visibility.Visible;

            //Debug.WriteLine("Entered Belege");
            //Trace.WriteLine("Me too");
            //DAL.Tools.LoggingTool.LogMessage("I am the logging tool", "EditBelege", "FillProjekt", DAL.Tools.LoggingTool.LogState.high);

        }

        private void cmdDeletePosition_Click(object sender, RoutedEventArgs e)
        {
            {
                //Button btn = sender as Button;

                //int index = ((btn.Parent as StackPanel).Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;
                //this.DataGrid_Aggregate.SelectedIndex = index;

            }
        }


        private void Details_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.SI_BelegePositionen_ListView.SelectedItem = item.DataContext;

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.SelectionStart = 0;
            tb.SelectionLength = tb.Text.Length;
        }

        private void btnRunTest_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Hitting Test");

            //using (var db = new SteinbachEntities())
            //{
            //    var query = db.C_Localizing.Where(n => n.Objektname == 95);
            //    var pers = db.personen.Where(n => n.id != 95 && n.id != 2);
            //    foreach (var p in pers)
            //    {
            //        foreach (var q in query)
            //        {
            //            var l = new C_Localizing();
            //            l.Objektname = p.id;
            //            l.Begriffname = q.Begriffname;
            //            l.Wert = q.Wert;
            //            l.id_Sprache = q.id_Sprache;
            //            db.AddToC_Localizing(l);


            //        }
            //    }

            //    db.SaveChanges();

            //}



        }

        private void ArtikelPosRabatt_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ZZPosMoveUp_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int index = ((btn.Parent as StackPanel).Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;

            this.GridZusatzzeilen.SelectedIndex = index;
        }

        private void fcbArtikelnummer_onfcbChanged(object sender, DataTypes.FilteredComboBoxChangedEventArgs e)
        {
            try
            {
                int res = 0;
                if (int.TryParse(e.filter, out res) == true)
                {


                    // return db.lagerlisten.Where(n => n.artikelnr.Contains(arg.filter));
                    var query = from l in db.lagerlisten
                                where l.artikelnr.Contains(e.filter)
                                select new Models.LagerListeTemplateSource
                                {
                                    id = l.id,
                                    artikelnr = l.artikelnr,
                                    bezeichnung = l.bezeichnung,
                                    anzahlauflager = l.anzahlauflager
                                };

                    LagerListeViewSource.Source = query;
                   

                }
                else
                {
                    // return db.lagerlisten.Where(n => n.bezeichnung.Contains(arg.filter));
                    var query = from l in db.lagerlisten
                                where l.bezeichnung.Contains(e.filter)
                                select new Models.LagerListeTemplateSource
                                {
                                    id = l.id,
                                    artikelnr = l.artikelnr,
                                    bezeichnung = l.bezeichnung,
                                    anzahlauflager = l.anzahlauflager
                                };


                    LagerListeViewSource.Source = query;
                }


            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
                
            }

        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var lb = new Lagerbuchungen(db);
        //    lb.ConsolidateLagerbestaende();
        //}

        //private void btnImport_Click(object sender, RoutedEventArgs e)
        //{
        //    var dc = (ViewModels.SI_BelegeViewModel)this.SI_BelegePositionen_ListView.DataContext;
        //    MessageBox.Show(dc.Lagerlisten.Count.ToString());

        //}
        //}


        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //FillProject();

        //}

        #endregion

        #region ControlEvents

        //private void cmdNew_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DoCancel())
        //    {
        //        ID = 0;
        //        FillProject();
        //    }
        //}

        //private void cboQuellLager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    if (cboQuellLager.SelectedIndex == cboZielLager.SelectedIndex)
        //    {
        //        var s = (ComboBox)sender;
        //        if (s.Name == "cboQuellLager")
        //        {
        //            cboZielLager.SelectedIndex = cboZielLager.SelectedIndex < cboZielLager.Items.Count - 1 ? cboZielLager.SelectedIndex + 1 : cboZielLager.SelectedIndex - 1;
        //        }

        //    }

        //}

        //private void cboBelegart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //if (View != null)
        //{
        //    var b = (SI_Belege)View.CurrentItem;
        //    if (b.StammBelegarten != null)
        //    {
        //        lblFirma.Content = b.StammBelegarten.BeziehungPartner;
        //        var bw = (StammBewegungsarten)b.StammBelegarten.StammBewegungsarten;
        //        switch (bw.id)
        //        {
        //            case "um":
        //                {
        //                    if (b.isEnabled)
        //                    {
        //                        cboQuellLager.IsEnabled = true;
        //                        cboZielLager.IsEnabled = true;
        //                    }


        //                    cboQuellLager.SelectedIndex = 0;
        //                    cboZielLager.SelectedIndex = 1;
        //                    break;
        //                }
        //            case "ohne":
        //                {

        //                    cboQuellLager.IsEnabled = false;
        //                    cboZielLager.IsEnabled = false;
        //                    cboZielLager.SelectedIndex = 3;
        //                    cboQuellLager.SelectedIndex = 3;
        //                    break;
        //                }



        //            default:
        //                {
        //                    cboQuellLager.IsEnabled = false;
        //                    if (b.isEnabled)
        //                    {
        //                        cboZielLager.IsEnabled = true;
        //                    }

        //                    cboQuellLager.SelectedIndex = 3;

        //                    cboZielLager.SelectedIndex = 0;
        //                    break;
        //                }
        //        }
        //    }



        //}


        //}





        //private void cboArtikelnummer_onfcbChanged(object sender, DataTypes.FilteredComboBoxChangedEventArgs e)
        //{
        //    int res = 0;
        //    if (int.TryParse(e.filter, out res) == true)
        //    {
        //        LagerListeViewSource.Source = db.lagerlisten.Where(n => n.artikelnr.Contains(e.filter));

        //    }
        //    else
        //    {
        //        LagerListeViewSource.Source = db.lagerlisten.Where(n => n.bezeichnung.Contains(e.filter));
        //    }


        //}

        //private void fcbProjektnummer_onfcbChanged(object sender, DataTypes.FilteredComboBoxChangedEventArgs e)
        //{
        //var res = db.projekte.Where(n => n.projektnummer.StartsWith(e.filter));
        //ProjekteViewSource.Source = res;



        //fcbProjektnummer.CBoxItemssource = res;
        //  fcbProjektnummer.FilteredItemsSource = res;

        //}

        #endregion

        //void fcbArtikel_OnFcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //var cbo = (ComboBox)sender;
        ////var i = cbo.SelectedIndex;
        //var x = (Tools.LagerListeOberArtikel)cbo.SelectedItem;
        //var l = dbArt.lagerlisten.Where(p => p.id == x.id).FirstOrDefault();

        //if (MessageBox.Show(string.Format("Artikel {0} als Unterartikel hinzufügen ?", x.bezeichnung), "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
        //{
        //    if (x.id_parent != ID)
        //    {
        //        l.id_parent = ID;
        //    }
        //}


        //dbArt.SaveChanges();




        //}



        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    if (SaveChanges())
        //    {
        //        //if (c1GridDetails.Visibility == System.Windows.Visibility.Hidden)
        //        //{
        //        //    ID = txtid.Text == "" ? 0 : int.Parse(txtid.Text);
        //        //    FillProject();
        //        //}
        //    }

        //}

        //private void btnClose_Click(object sender, RoutedEventArgs e)
        //{
        //    // SaveChanges();
        //    //this.Close();
        //}

        //private void cmdCancel_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DoCancel())
        //    {
        //        //db.Dispose();
        //        //this.Close();

        //    }


        //}








        //private bool DoCancel()
        //{
        //var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
        //if (om.Count() > 0)
        //{
        //    var res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

        //    if (res == MessageBoxResult.Yes)
        //    {
        //        if (SaveChanges())
        //        {
        //            return true;
        //            //db.Dispose();
        //            //this.Close();

        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }

        //    else if (res == MessageBoxResult.No)
        //    {
        //        return true;
        //        //db.Dispose();
        //        //this.Close();

        //    }

        //    else if (res == MessageBoxResult.Cancel)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}


        //else
        //{

        //    return true;
        //}

        //}

        //private bool SaveChanges()
        //{

        //try
        //{

        //    var sba = (StammBelegarten)cboBelegart.SelectedItem;
        //    string ba = sba.BeziehungPartner;


        //    if (cboBelegart.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Es muß eine Belegart ausgewählt werden.");
        //        return false;
        //    }
        //    //if (fcbProjektnummer.cBox.SelectedIndex == -1 && ba != "inv")
        //    //{
        //    //    MessageBox.Show("Es muß ein Projekt ausgewählt werden.");
        //    //    return false;
        //    //}

        //    //if (acpFirma.SelectedIndex == -1 && ba != "intern")
        //    //{
        //    //    MessageBox.Show(string.Format("Es muß ein {0} ausgewählt werden.", sba.BeziehungPartner));
        //    //    return false;
        //    //}


        //    if (DetailsView.Count == 0)
        //    {
        //        MessageBox.Show("Es sind keine Artikelbewegungen vorhanden.");
        //        return false;
        //    }
        //    var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
        //    foreach (var o in om)
        //    {

        //        if (o.Entity is lagerliste)
        //        {

        //            var p = (lagerliste)o.Entity;
        //            if (p.EntityState == System.Data.EntityState.Added)
        //            {
        //                bool KeyInUse = db.lagerlisten.Any(c => c.bezeichnung == p.bezeichnung);


        //                if (KeyInUse)
        //                {
        //                    MessageBox.Show(String.Format("Der Name {0} wird schon benutzt.", p.bezeichnung));
        //                    return false;
        //                }
        //            }
        //        }


        //    }

        //    db.SaveChanges();

        //    if (DetailsView != null)
        //    {
        //        if (DetailsView.Count > 0)
        //        {
        //            CommonTools.Tools.LockProcess.enumResultLockProcess Buchungen = CommonTools.Tools.LockProcess.DoLockProcess(SaveBuchungen, "Lagerbuchung_Gesperrt");
        //            SaveBuchungen();
        //        }
        //    }



        //    return true;

        //}

        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message);
        //    return false;
        //}


        //}


        //private bool SaveBuchungen()
        //{
        //bool res = true;
        //try
        //{
        //    var b = (SI_Belege)View.CurrentItem;
        //    var bw = (StammBewegungsarten)b.StammBelegarten.StammBewegungsarten;


        //    if (bw.Lagerbuchung == 1)
        //    {
        //        if (this.DetailsView != null)
        //        {

        //            var lb = new Lagerbuchungen(db);

        //            foreach (var item in DetailsView)
        //            {
        //                var pos = (SI_BelegePositionen)item;
        //                if (pos.istGebucht == 0 || pos.istGebucht == null)
        //                {

        //                    if (pos.id_Artikel != null && pos.Menge != null && pos.Menge != 0)
        //                    {
        //                        int ProId = b.id_Projekt.HasValue ? (int)b.id_Projekt : 0;

        //                        lb.Lagerbuchung((int)b.id_Quelllager, (int)b.id_Ziellager, (int)bw.WirkungQuelllager, (int)bw.WirkungZiellager, (int)pos.Menge,
        //                                                                    (int)pos.id_Artikel, bw.id, ProId, (int)pos.id);
        //                        pos.istGebucht = 1;
        //                    }



        //                }
        //            }

        //        }
        //    }


        //    b.istGebucht = 1;

        //    db.SaveChanges();
        //}
        //catch (Exception ex)
        //{

        //    MessageBox.Show(ex.Message);
        //}

        //return res;

        //}




        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        // AcceptNewRow();
        //txtid.Focus();

        //if (!DoCancel())
        //    e.Cancel = true;
        //else
        //{
        //    // createDoc = null;
        //}
        //}



        //private void c1GridUnterartikel_DeletingRows(object sender, C1.WPF.DataGrid.DataGridDeletingRowsEventArgs e)
        //{
        //    if (MessageBox.Show(string.Format("Möchten Sie den Artikel {0} wirklich aus der Unterartikelliste entfernen ?", string.Join(",", e.DeletedRows.Select(row => (row.DataItem as lagerliste).bezeichnung).ToArray())), "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
        //    {
        //        e.Cancel = true;
        //    }
        //    else
        //    {

        //        foreach (var item in e.DeletedRows.Select(row => (row.DataItem as lagerliste)).ToArray())
        //        {
        //            item.id_parent = 0;
        //        }
        //    }

        //    e.Cancel = true;
        //    dbArt.SaveChanges();


        //}



        //private void btnTest_Click(object sender, RoutedEventArgs e)
        //{

        //    using (var test = new SteinbachEntities())
        //    {
        //        var beleg = new SI_Belege();
        //        beleg.id_Quelllager = 1;
        //        beleg.id_Ziellager = 2;
        //        test.AddToSI_Belege(beleg);

        //        var bwg = test.lagerliste_addremove.Where(i => i.id == 16471).SingleOrDefault();
        //        bwg.id_Quelllager = 2;
        //        bwg.id_Ziellager = 1;
        //        //test.AddTolagerliste_addremove(bwg);

        //        test.SaveChanges();


        //    }

        // }

        //private void btnAccept_Click(object sender, RoutedEventArgs e)
        //{
        //    //c1GridDetails.EndNewRow(true);
        //}

        //private void c1GridDetails_BeginningNewRow(object sender, C1.WPF.DataGrid.DataGridBeginningNewRowEventArgs e)
        //{
        //    //var b = (SI_BelegePositionen)e.NewRow.DataItem;
        //    //MessageBox.Show(b.Preis.ToString());
        //}

        //private void c1GridDetails_BeginningRowEdit(object sender, C1.WPF.DataGrid.DataGridEditingRowEventArgs e)
        //{

        //}

        //private void c1GridDetails_BeginningEdit(object sender, C1.WPF.DataGrid.DataGridBeginningEditEventArgs e)
        //{

        //}

        //private void c1GridDetails_CommittingEdit(object sender, C1.WPF.DataGrid.DataGridEndingEditEventArgs e)
        //{
        //    //var b = (SI_BelegePositionen)e.Row.DataItem;
        //    //MessageBox.Show(b.SI_Belege.id.ToString());
        //}

        //private void c1GridDetails_CommittingNewRow(object sender, C1.WPF.DataGrid.DataGridEndingNewRowEventArgs e)
        //{
        // var b = (SI_BelegePositionen)e.NewRow.DataItem;
        //// MessageBox.Show(b.Preis.ToString());
        // var re = b.SI_BelegeReference.CreateSourceQuery();
        // db.DeleteObject(re);




        //}

        //private void c1GridDetails_CommittingRowEdit(object sender, C1.WPF.DataGrid.DataGridEditingRowEventArgs e)
        //{

        //var b = (SI_BelegePositionen)e.Row.DataItem;
        //MessageBox.Show(b.SI_Belege.id.ToString());
        //}

        //private void c1GridDetails_CurrentCellChanged(object sender, C1.WPF.DataGrid.DataGridCellEventArgs e)
        //{

        //}

        //private void AddPosition_Click(object sender, RoutedEventArgs e)
        //{
        //try
        //{
        //    var b = (SI_Belege)View.CurrentItem;
        //    //  var ba = (StammBelegarten)b.StammBelegarten;
        //    var det = (SI_BelegePositionen)DetailsView.AddNew();
        //    DetailsView.CommitNew();
        //    det.istGebucht = 0;
        //    det.created = DateTime.Now;
        //    det.SI_Belege = b;

        //}
        //catch (Exception ex)
        //{

        //    MessageBox.Show(ex.Message);
        //}




        //}
        //}

        //private void AddScannerPosition(string Artikelnummer, string menge)
        //{

        //try
        //{
        //    var b = (SI_Belege)View.CurrentItem;
        //    var det = (SI_BelegePositionen)DetailsView.AddNew();
        //    DetailsView.CommitNew();

        //    det.istGebucht = 0;
        //    det.SI_Belege = b;
        //    det.created = DateTime.Now;
        //    det.id_Artikel = GetArtikelID(Artikelnummer);

        //    int res = 0;
        //    if (int.TryParse(menge, out res))
        //    {
        //        det.Menge = res;
        //    }


        //}
        //catch (Exception ex)
        //{

        //    MessageBox.Show(ex.Message);
        //}



        //}

        //private int? GetArtikelID(string Artikelnummer)
        //{

        //var res = db.lagerlisten.Where(i => i.artikelnr == Artikelnummer).FirstOrDefault();
        //if (res != null)
        //{
        //    int? id = res.id;

        //    if (id.HasValue)
        //    {
        //        return (int)id;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //else
        //{
        //    return null;
        //}




        //}






        //private void SI_BelegePositionen_ListView_GotFocus(object sender, EventArgs e)
        //{

        //var item = (ListViewItem)sender;
        //this.SI_BelegePositionen_ListView.SelectedItem = item.DataContext;

        //}


        //private void cmdDeletePosition_Click(object sender, RoutedEventArgs e)
        //{
        //if (this.DetailsView.CurrentPosition > -1)
        //{
        //    var pos = (SI_BelegePositionen)DetailsView.CurrentItem;


        //    if ((pos.istGebucht == null || pos.istGebucht == 0) && (pos.SI_Belege.istGebucht == null || pos.SI_Belege.istGebucht == 0))
        //    {
        //        if (MessageBox.Show("Belegposition wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //        {

        //            this.DetailsView.RemoveAt(this.DetailsView.CurrentPosition);
        //        }

        //    }
        //    else
        //    {
        //        MessageBox.Show("Diese Belegposition kann nicht gelöscht werden.");
        //    }




        //    }
        //}

        //private void btnImport_Click(object sender, RoutedEventArgs e)
        //{
        //try
        //{
        //    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        //    dlg.InitialDirectory = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("ImportFolder_ScannerData", @"E:\test\Import");
        //    dlg.DefaultExt = ".txt";
        //    dlg.Filter = "Text documents (.txt)|*.txt";

        //    Nullable<bool> result = dlg.ShowDialog();
        //    if (result == true)
        //    {
        //        string filename = dlg.FileName;
        //        tblImportFile.Text = filename;
        //        importScannerData(filename, 1, 2);
        //    }
        //}
        //catch (Exception ex)
        //{

        //    MessageBox.Show(ex.Message);
        //}

        //}

        //private void importScannerData(string filename, int ArtikelNr, int Menge)
        //{

        //try
        //{
        //    StreamReader f = new StreamReader(filename);
        //    string[] cols;
        //    string line = string.Empty;

        //    while (!f.EndOfStream)
        //    {
        //        line = f.ReadLine();
        //        cols = line.Split(';');
        //        AddScannerPosition(cols[ArtikelNr], cols[Menge]);


        //    }
        //}
        //catch (FileNotFoundException)
        //{
        //    MessageBox.Show("Importdatei konnte nicht gefunden werden.");
        //}
        //catch (Exception ex)
        //{

        //    MessageBox.Show(ex.Message);
        //}



        //}

        //private void btnPrint_Click(object sender, RoutedEventArgs e)
        //{

        //    try
        //    {
        //var b = (SI_Belege)View.CurrentItem;
        //OpenBelegDruck(b.id);


        ////int pos = 8;
        ////asCreateWordDokuments.Pages.Page page = new asCreateWordDokuments.Pages.Page();
        ////StringBuilder sb = new StringBuilder();

        ////sb.AppendLine("Pos.   	Quantity    	Description     	Article No.");
        ////foreach (SI_BelegePositionen item in DetailsView)
        ////{
        ////    ++pos;
        ////    sb.Append(pos.ToString().PadRight(5));
        ////    sb.Append("EA".PadRight(5));
        ////    sb.Append(item.Menge.ToString().PadRight(5));
        ////    sb.Append(item.Bezeichnung.PadRight(30));
        ////    sb.AppendLine(item.Artikelnummer.PadRight(12));

        ////}

        ////page.Text = sb.ToString();

        ////var doc = new asCreateWordDokuments.Documents.Lieferschein();
        ////doc.RechnungsNummer = "Lieferschein_" + b.id.ToString();
        ////doc.AddSheet(page);
        ////if (createDoc == null)
        ////{
        ////    createDoc = new CreateDoc();
        ////}

        ////createDoc.run(doc);
        //}

        //catch (Exception ex)
        //{

        //    CommonTools.Tools.ErrorMethods.ShowErrorMessage(ex, true);
        //}



        //}



        //private void OpenBelegDruck(int BelegId)
        //{
        //    //var b = new BelegViewer(BelegId);
        //    //b.ShowDialog();


        //}

        //private void fcbProjektnummer_OnFcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //try
        //    //{
        //    //    var res = (projekt)fcbProjektnummer.CBoxSelectedItem;
        //    //    KalkulationenViewSource.Source = db.kalkulationstabellen.Where(n => n.projektnummer == res.projektnummer);
        //    //}
        //    //catch (Exception)
        //    //{


        //    //}

        //}




        //private void cboKalkulationstabelle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void btnGetLagerliste_Click(object sender, RoutedEventArgs e)
        //{
        //if (fcbProjektnummer.CBoxSelectedItem != null)
        //{

        //    var buf = (DAL.projekt)fcbProjektnummer.CBoxSelectedItem;
        //    var calc = db.kalkulationstabellen.Where(n => n.projektnummer.Equals(buf.projektnummer));

        //    if (calc.Count() == 1)
        //    {

        //        var k = calc.FirstOrDefault();

        //        var pos = k.kalkulationstabelle_detail.Where(n => n.ausgeben == 1 && (n.Handelsware.HasValue == false || n.Handelsware == 0));

        //        foreach (var item in pos)
        //        {

        //            AddScannerPosition(item.artikelnr, item.menge.ToString());

        //        }
        //    }
        //}
        //}

        //private void fcbFirma_onfcbChanged(object sender, DataTypes.FilteredComboBoxChangedEventArgs e)
        //{
        //var res = db.firmen.Where(n => n.name.Contains(e.filter));
        //FirmenViewSource.Source = res;
        //}

        //private void fcbFirma_OnFcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}









    }
}
