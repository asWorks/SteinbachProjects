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
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for EditFirma.xaml
    /// </summary>
    public partial class EditTextbausteine : Window
    {

        SteinbachEntities db;
        int ID = 0;
        CollectionViewSource ViewSource;
        CollectionViewSource GruppenViewSource;
        CollectionViewSource SprachenViewSource;
        CollectionViewSource TextbausteineSource;
        private ListCollectionView View;
        ProjektDB.ViewModels.CheckListBoxes.ListboxSelectedBelegartenViewModel ListboxSelectedBelegartenVM;
        private DispatcherTimer ti;
        private bool OrderChanged;

        bool bNew = false;
        firma f = null;

        public EditTextbausteine()
        {
            InitializeComponent();
        }

        public EditTextbausteine(int id)
        {
            ID = id;
            InitializeComponent();
            db = new SteinbachEntities();


        }



        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {


            if (SaveChanges())
            {
               // db.Dispose();
               // this.Close();
            }



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            ti = new DispatcherTimer();
            ti.Tick += ti_Tick;
            ti.Interval = new TimeSpan(0, 0, 0, 0, 500);
            ti.Start();

            ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ThisViewSource")));
            GruppenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("GruppenViewSource")));
            SprachenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SprachenViewSource")));
            TextbausteineSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("TextBausteineSortSource")));

            GruppenViewSource.Source = CommonTools.Tools.HelperTools.GetAuswahlEintraegeList("TypTextbausteine", 1);
            SprachenViewSource.Source = CommonTools.Tools.HelperTools.GetAuswahlEintraegeList("TypSprache", 1);
            StammTextbaustein textbaustein;
            OrderChanged = false;

            if (ID == 0)
            {


                var Query = from n in db.StammTextbausteine
                            select n;

                ViewSource.Source = new TextbausteinCollection(Query, db);

                View = (ListCollectionView)ViewSource.View;
                var i = (StammTextbaustein)View.AddNew();
                i.Caption = "Neu . . . ";
                View.CommitNew();
                bNew = true;
                textbaustein = i;
                ListboxSelectedBelegarten.IsEnabled = false;
                TabItemReihenfolge.IsEnabled = false;


            }
            else
            {
                var Query = from n in db.StammTextbausteine
                            where n.id == ID
                            select n;

                ViewSource.Source = Query;
                textbaustein = Query.SingleOrDefault();

            }


            ListboxSelectedBelegartenVM = new ViewModels.CheckListBoxes.ListboxSelectedBelegartenViewModel(db, textbaustein);
            Caliburn.Micro.ViewModelBinder.Bind(ListboxSelectedBelegartenVM, this.ListboxSelectedBelegarten, null);



        }

        private IEnumerable<StammTextbaustein> GetTextbausteineSource()
        {

            if (cboSprache.SelectedItem != null)
            {
                Console.WriteLine("");
            }

            if (cboSprache.SelectedValue != null)
            {
                var lan = (int)cboSprache.SelectedValue;
                return new ObservableCollection<StammTextbaustein>(db.StammTextbausteine.Where(n => n.id_Sprache == lan).OrderBy(n => n.idx));
            }
            else
            {
                return new ObservableCollection<StammTextbaustein>(db.StammTextbausteine.OrderBy(n => n.idx));
            }


        }

        private int GetMaxIdx()
        {

            var lan = (int)cboSprache.SelectedValue;
            var res = from s in db.StammTextbausteine
                      where s.id_Sprache == lan
                      select s.idx;

            int max = (int)res.Max();
            return max;
        }
        void ti_Tick(object sender, EventArgs e)
        {
            ListboxSelectedBelegartenVM.AddSelectedNames();
            TextbausteineSource.Source = GetTextbausteineSource();
            ti.Stop();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (DoCancel())
            {
                // db.Dispose();
                this.Close();

            }


        }


        private bool DoCancel()
        {
            var om = db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
            if (om.Count() > 0)
            {

                foreach (var o in om)
                {

                    if (o.Entity is StammTextbaustein)
                    {
                        var res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                        if (res == MessageBoxResult.Yes)
                        {
                            var obj = (StammTextbaustein)o.Entity;
                            obj.lastmodified = DateTime.Now;

                            if (SaveChanges())
                            {
                                return true;


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



                }

                return true;

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

                if (View != null && View.CurrentItem != null)
                {
                    var x = (StammTextbaustein)View.CurrentItem;
                    if (x.idx == null)
                    {
                        x.idx = GetMaxIdx() + 1;
                    }
                }


                db.SaveChanges();
                ListboxSelectedBelegarten.IsEnabled = true;
                TabItemReihenfolge.IsEnabled = true;
                //db.Dispose();
                ti.Start();
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
            if (!DoCancel())
                e.Cancel = true;
            if (OrderChanged)
            {
                RefreshTextbausteinBelegarten();
            }
        }

        private void RefreshTextbausteinBelegarten()
        {

            try
            {
                using (var con = new SteinbachEntities())
                {
                    var stb = con.StammTextbausteine;
                    foreach (var item in stb)
                    {
                        var tba = con.SI_BelegartenTextbausteine.Where(n => n.id_Textbaustein == item.id);
                        foreach (var item1 in tba)
                        {
                            item1.index = item.idx;
                        }

                    }

                    con.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Schreiben des Textbausteine - Belegarten Indexes");
            }




        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var Item = (StammTextbaustein)LBTextbausteineSort.Items.CurrentItem;
                var Pos = LBTextbausteineSort.SelectedIndex;

                if (Pos > 0)
                {
                    var ItemAbove = (StammTextbaustein)LBTextbausteineSort.Items[Pos - 1];
                    Item.idx--;
                    ItemAbove.idx++;
                    db.SaveChanges();
                    TextbausteineSource.Source = GetTextbausteineSource();
                    OrderChanged = true;
                    LBTextbausteineSort.SelectedIndex = (int)Pos-1;
                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Sortieren des StammTextbausteine Indexes");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                var Item = (StammTextbaustein)LBTextbausteineSort.Items.CurrentItem;
                var Pos = LBTextbausteineSort.SelectedIndex;
                int max = GetMaxIdx() - 1;

                if (Pos < max)
                {
                    var ItemAbove = (StammTextbaustein)LBTextbausteineSort.Items[Pos + 1];
                    Item.idx++;
                    ItemAbove.idx--;
                    db.SaveChanges();
                    TextbausteineSource.Source = GetTextbausteineSource();
                    OrderChanged = true;
                    LBTextbausteineSort.SelectedIndex = (int)Pos+1;
                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Sortieren des StammTextbausteine Indexes");
            }

        }
    }
}
