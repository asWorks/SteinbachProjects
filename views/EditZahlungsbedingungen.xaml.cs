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
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for Editschiff.xaml
    /// </summary>
    public partial class EditZahlungsbedingungen : Window
    {

        SteinbachEntities db;
        int ID = 0;
        CollectionViewSource ViewSource;
        CollectionViewSource ZBViewSource;
        CollectionViewSource ZB_SprachenViewSource;
        CollectionViewSource SprachenLookup;

        private ListCollectionView View;
        private BindingListCollectionView ZBView;
        private BindingListCollectionView ZBSprachenView;

        bool bNew = false;
        schiff f = null;

        public EditZahlungsbedingungen()
        {
            InitializeComponent();
        }

        public EditZahlungsbedingungen(int id)
        {
            ID = id;
            InitializeComponent();
        }



        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {


            if (SaveChanges())
            {

                ID = int.Parse(this.txtID.Text);
                LoadShip();
            }



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            db = new SteinbachEntities();
            ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ThisViewSource")));
            ZBViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ZB_ViewSource")));
            SprachenLookup = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SprachenLookup")));
            ZBView = (BindingListCollectionView)ZBViewSource.View;
            ZB_SprachenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ZB_SprachenViewSource")));
            ZBSprachenView = (BindingListCollectionView)ZB_SprachenViewSource.View;

            //var ZBLookup = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ZBLookup")));
            //var ZBquery = from a in db.Stamm_Aggregate
            //             select a;
            //ZBLookup.Source = ZBquery;

            //ftbOwner.cBox.ItemsSource = RefreshOwnerComboBox(string.Empty);
            //ftbOwner.SetSimpleBinding("owner", "name", "name", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
            //ftbOwner.MaxTextLength = 255;

            SprachenLookup.Source = db.AuswahlEintraege.Where(n => n.Gruppe == "TypSprache");


            LoadShip();

        }

        private void LoadShip()
        {
            if (ID == 0)
            {


                var Query = from n in db.StammZahlungsbedingungen
                            select n;

                ViewSource.Source = new ZahlungsbedingungCollection(Query, db);
                View = (ListCollectionView)ViewSource.View;
                ZBView = (BindingListCollectionView)ZBViewSource.View;
                ZBSprachenView = (BindingListCollectionView)ZB_SprachenViewSource.View;
                var i = (StammZahlungsbedingungen)View.AddNew();
                // i.name = "schiff Neu";
                View.CommitNew();
                bNew = true;
                this.ZahlungsbedingungenSprachenGrid.IsEnabled = false;

            }
            else
            {
                var Query = from n in db.StammZahlungsbedingungen
                            where n.id == ID
                            select n;

                ViewSource.Source = Query;
                ZBView = (BindingListCollectionView)ZBViewSource.View;
                ZBSprachenView = (BindingListCollectionView)ZB_SprachenViewSource.View;

            }
        }






        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!DoCancel())
            {
                // db.Dispose();
                this.Close();

            }


        }


        private bool DoCancel()
        {
            var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
            //if (om.Count() > 0)
            //{


            foreach (var o in om)
            {

                if (o.Entity is StammZahlungsbedingungen || o.Entity is SI_Zahlungsbedingungen_Sprache || o.Entity is StammZahlungsfristen)
                {

                    var res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                    if (res == MessageBoxResult.Yes)
                    {
                        if (SaveChanges())
                        {
                            return false;
                            //db.Dispose();
                            //this.Close();

                        }
                        else
                        {
                            return true;
                        }

                    }

                    else if (res == MessageBoxResult.No)
                    {
                        return false;
                        //db.Dispose();
                        //this.Close();

                    }

                    else if (res == MessageBoxResult.Cancel)
                    {
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

            return false;
        }


        private bool SaveChanges()
        {
            try
            {

                var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
                foreach (var o in om)
                {

                    if (o.Entity is StammZahlungsbedingungen)
                    {

                        //var p = (schiff)o.Entity;
                        //if (p.imonummer == null || p.imonummer.Equals(string.Empty) || Regex.IsMatch(p.imonummer.ToString(), "\\D"))
                        //{
                        //    MessageBox.Show("Bitte eine gültige Imonummer oder 0 eingeben.");
                        //    return false;
                        //}

                        //if (p.EntityState == System.Data.EntityState.Added)
                        //{


                        //    bool KeyInUse = db.schiffe.Any(c => c.imonummer == p.imonummer && (c.imonummer != null && c.imonummer != 0));

                        //    if (KeyInUse)
                        //    {
                        //        var query = from s in db.schiffe
                        //                    where s.imonummer == p.imonummer && (s.imonummer != null && s.imonummer != 0)
                        //                    select s;
                        //        schiff Schiff = query.FirstOrDefault();


                        //        MessageBox.Show(String.Format("Die Imonummer {0} wird schon benutzt von Schiff {1}, ID {2} .", p.imonummer, Schiff.name, Schiff.id));
                        //        return false;
                        //    }

                        //    bool NameInUse = db.schiffe.Any(c => c.name == p.name);

                        //    if (NameInUse)
                        //    {
                        //        var query = from s in db.schiffe
                        //                    where s.name == p.name
                        //                    select s;

                        //        schiff Schiff = query.FirstOrDefault();

                        //        var Result = MessageBox.Show(String.Format("Der Name {0} wird schon benutzt von dem Schiff mit der Imonummer {1}, ID {2}. Trotzdem speichern ?", p.name, Schiff.imonummer, Schiff.id), "", MessageBoxButton.YesNo);
                        //        if (Result == MessageBoxResult.Yes)
                        //        {
                        //            db.SaveChanges();
                        //            return true;
                        //        }
                        //        else
                        //            return false;
                        //    }
                        //}

                        //if (p.EntityState == System.Data.EntityState.Modified)
                        //{
                        //    if (p.imonummer != 0)
                        //    {


                        //        object Tt = o.OriginalValues["name"];
                        //        //MessageBox.Show(Tt.ToString());
                        //        string sName = Tt.ToString();
                        //        var query = from s in db.schiffe
                        //                    where s.imonummer == p.imonummer && s.id != p.id
                        //                    select s;
                        //        schiff Schiff = query.FirstOrDefault();

                        //        if (Schiff != null)
                        //        {

                        //            MessageBox.Show(String.Format("Änderung nicht möglich. Die Imonummer {0} wird schon benutzt von Schiff {1}, ID {2} .", p.imonummer, Schiff.name, Schiff.id));
                        //            return false;
                        //        }
                        //    }

                        //    var query1 = from s in db.schiffe
                        //                 where s.name == p.name && s.id != p.id
                        //                 select s;

                        //    schiff Schiff1 = query1.FirstOrDefault();

                        //    if (Schiff1 != null)
                        //    {
                        //        var Result = MessageBox.Show(String.Format("Der Name {0} wird schon benutzt von dem Schiff mit der Imonummer {1}, ID {2}. Trotzdem speichern ?", p.name, Schiff1.imonummer, Schiff1.id), "", MessageBoxButton.YesNo);
                        //        if (Result == MessageBoxResult.Yes)
                        //        {
                        //            db.SaveChanges();
                        //            return true;
                        //        }
                        //        else
                        //            return false;
                        //    }
                        //}
                    }
                }

                db.SaveChanges();
                // db.Dispose();
                this.ZahlungsbedingungenSprachenGrid.IsEnabled = true;
                return true;

            }

            catch (System.Data.UpdateException ex)
            {
                CommonTools.Tools.UserMessage.NotifyUser("Sprache kann nicht mehrfach vorhanden sein");
                return false;
            }

            //catch (System.Data.SqlClient.SqlException ex)
            //{
            //    CommonTools.Tools.UserMessage.NotifyUser("Sprache kann nicht mehrfach vorhanden sein");
            //    return false;
            //}

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }





        }

        private void ZB_ListView_GotFocus(object sender, EventArgs e)
        {

            var item = (ListViewItem)sender;
            // this.LB_ListView.SelectedItem = item.DataContext;
            this.ZB_ListView.SelectedItem = item.DataContext;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DoCancel())
                e.Cancel = true;
        }

        private void AddAggregate_Click(object sender, RoutedEventArgs e)
        {
            if (this.ZB_ListView != null)
            {
                var agg = (StammZahlungsfristen)(this.ZBView.AddNew());
                this.ZBView.CommitNew();

            }
        }




        private void cmdDeleteAggregate_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Eintrag wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (this.ZBView.CurrentPosition > -1)
                {
                    this.ZBView.RemoveAt(this.ZBView.CurrentPosition);
                    //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                }
            }
        }




        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Details_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.ZahlungsbedingungenSprachenGrid.SelectedItem = item.DataContext;

        }

        private IEnumerable fcbArtikelnummer_OnFCBChangedFunc(DataTypes.FilteredComboBoxChangedEventArgs arg)
        {
            int res = 0;
            if (int.TryParse(arg.filter, out res) == true)
            {


                return db.lagerlisten.Where(n => n.artikelnr.Contains(arg.filter));

            }
            else
            {
                return db.lagerlisten.Where(n => n.bezeichnung.Contains(arg.filter));
            }
        }

        private void btnAddPosition_Click(object sender, RoutedEventArgs e)
        {
            if (this.ZBSprachenView != null)
            {
                var agg = (SI_Zahlungsbedingungen_Sprache)(this.ZBSprachenView.AddNew());
                this.ZBSprachenView.CommitNew();

            }
        }

        private void cmdDeleteSprachenPosition_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Eintrag wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (this.ZBSprachenView.CurrentPosition > -1)
                {
                    this.ZBSprachenView.RemoveAt(this.ZBSprachenView.CurrentPosition);
                    //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                }
            }
        }

    }
}
