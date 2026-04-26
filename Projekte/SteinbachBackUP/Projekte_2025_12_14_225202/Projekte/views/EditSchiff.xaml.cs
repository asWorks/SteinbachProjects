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

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for Editschiff.xaml
    /// </summary>
    public partial class EditSchiff : Window
    {

        SteinbachEntities db;
        int ID = 0;
        CollectionViewSource ViewSource;
        CollectionViewSource AggregateViewSource;

        private ListCollectionView View;
        private BindingListCollectionView AggregateView;

        bool bNew = false;
        schiff f = null;

        public EditSchiff()
        {
            InitializeComponent();
        }

        public EditSchiff(int id)
        {
            ID = id;
            InitializeComponent();
        }



        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {


            if (SaveChanges())
            {
               
                ID = int.Parse( this.txtID.Text);
                LoadShip();
            }



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            db = new SteinbachEntities();
            ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ThisViewSource")));
            AggregateViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Aggregate_ViewSource")));
            AggregateView = (BindingListCollectionView)AggregateViewSource.View;
            var AggLookup = ((System.Windows.Data.CollectionViewSource)(this.FindResource("AggregateLookup")));
            var aquery = from a in db.Stamm_Aggregate
                         select a;
            AggLookup.Source = aquery;

            ftbOwner.cBox.ItemsSource = RefreshOwnerComboBox(string.Empty);
            ftbOwner.SetSimpleBinding("owner", "name", "name", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
           // ftbOwner.MaxTextLength = 255;

            
            Format.FormatSchiffe.AltePlantEinträgeEingeblendet(this, new Properties.Settings().AltePlantEintraegeEinblenden);

            LoadShip();

        }

        private void LoadShip()
        {
            if (ID == 0)
            {


                var Query = from n in db.schiffe
                            select n;

                ViewSource.Source = new SchiffCollection(Query, db);
                View = (ListCollectionView)ViewSource.View;
                AggregateView = (BindingListCollectionView)AggregateViewSource.View;
                var i = (schiff)View.AddNew();
                // i.name = "schiff Neu";
                View.CommitNew();
                bNew = true;

            }
            else
            {
                var Query = from n in db.schiffe
                            where n.id == ID
                            select n;

                ViewSource.Source = Query;
                AggregateView = (BindingListCollectionView)AggregateViewSource.View;

            }
        }


     

           private void fcbSchiff_onfcbChanged(object sender, UserControls.FilteredComboBoxChangedEventArgs e)
        {
            ftbOwner.cBox.ItemsSource = RefreshOwnerComboBox(e.filter);

        }

           private System.Collections.IEnumerable RefreshOwnerComboBox(string filter)
           {
               if (filter == string.Empty)
               {
                   var owner = from f in db.firmen
                              orderby f.name
                              where f.name != string.Empty
                              select new { f.name };

                   return owner.ToList();

               }
               else
               {
                   var owner = from f in db.firmen
                              orderby f.name
                              where f.name != string.Empty && f.name.Contains(filter)
                              select new { f.name };

                   return owner.ToList();

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

                if (o.Entity is schiff)
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

                    if (o.Entity is schiff)
                    {

                        var p = (schiff)o.Entity;
                        if (p.imonummer == null || p.imonummer.Equals(string.Empty) || Regex.IsMatch(p.imonummer.ToString(), "\\D"))
                        {
                            MessageBox.Show("Bitte eine gültige Imonummer oder 0 eingeben.");
                            return false;
                        }

                        if (p.EntityState == System.Data.EntityState.Added)
                        {


                            bool KeyInUse = db.schiffe.Any(c => c.imonummer == p.imonummer && (c.imonummer != null && c.imonummer != 0));

                            if (KeyInUse)
                            {
                                var query = from s in db.schiffe
                                            where s.imonummer == p.imonummer && (s.imonummer != null && s.imonummer != 0)
                                            select s;
                                schiff Schiff = query.FirstOrDefault();


                                MessageBox.Show(String.Format("Die Imonummer {0} wird schon benutzt von Schiff {1}, ID {2} .", p.imonummer, Schiff.name, Schiff.id));
                                return false;
                            }

                            bool NameInUse = db.schiffe.Any(c => c.name == p.name);

                            if (NameInUse)
                            {
                                var query = from s in db.schiffe
                                            where s.name == p.name
                                            select s;

                                schiff Schiff = query.FirstOrDefault();

                                var Result = MessageBox.Show(String.Format("Der Name {0} wird schon benutzt von dem Schiff mit der Imonummer {1}, ID {2}. Trotzdem speichern ?", p.name, Schiff.imonummer, Schiff.id), "", MessageBoxButton.YesNo);
                                if (Result == MessageBoxResult.Yes)
                                {
                                    db.SaveChanges();
                                    return true;
                                }
                                else
                                    return false;
                            }
                        }

                        if (p.EntityState == System.Data.EntityState.Modified)
                        {
                            if (p.imonummer != 0)
                            {


                                object Tt = o.OriginalValues["name"];
                                //MessageBox.Show(Tt.ToString());
                                string sName = Tt.ToString();
                                var query = from s in db.schiffe
                                            where s.imonummer == p.imonummer && s.id != p.id
                                            select s;
                                schiff Schiff = query.FirstOrDefault();

                                if (Schiff != null)
                                {

                                    MessageBox.Show(String.Format("Änderung nicht möglich. Die Imonummer {0} wird schon benutzt von Schiff {1}, ID {2} .", p.imonummer, Schiff.name, Schiff.id));
                                    return false;
                                }
                            }

                            var query1 = from s in db.schiffe
                                         where s.name == p.name && s.id != p.id
                                         select s;

                            schiff Schiff1 = query1.FirstOrDefault();

                            if (Schiff1 != null)
                            {
                                var Result = MessageBox.Show(String.Format("Der Name {0} wird schon benutzt von dem Schiff mit der Imonummer {1}, ID {2}. Trotzdem speichern ?", p.name, Schiff1.imonummer, Schiff1.id), "", MessageBoxButton.YesNo);
                                if (Result == MessageBoxResult.Yes)
                                {
                                    db.SaveChanges();
                                    return true;
                                }
                                else
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

        private void Aggregate_ListView_GotFocus(object sender, EventArgs e)
        {

            var item = (ListViewItem)sender;
            this.Aggregate_ListView.SelectedItem = item.DataContext;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DoCancel())
                e.Cancel = true;
        }

        private void AddAggregate_Click(object sender, RoutedEventArgs e)
        {
            if (this.AggregateView != null)
            {
                var agg = (SchiffAnlage)(this.AggregateView.AddNew());
                this.AggregateView.CommitNew();

            }
        }

        private void cmdDeleteAggregate_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Eintrag wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (this.AggregateView.CurrentPosition > -1)
                {
                    this.AggregateView.RemoveAt(this.AggregateView.CurrentPosition);
                    //this.VerlaufView.Remove(VerlaufView.CurrentItem);

                }
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
