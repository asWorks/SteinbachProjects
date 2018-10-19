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

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for EditFirma.xaml
    /// </summary>
    public partial class EditFirma : Window
    {

        SteinbachEntities db;
        int ID = 0;
        CollectionViewSource ViewSource;
        private ListCollectionView View;

        CollectionViewSource ZahlungsbedingungenLookup;

        bool bNew = false;
        firma f = null;

        public EditFirma()
        {
            InitializeComponent();
        }

        public EditFirma(int id)
        {
            ID = id;
            InitializeComponent();
        }



        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {


            if (SaveChanges())
            {
                db.Dispose();
                this.Close();
            }
            


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            db = new SteinbachEntities();
            ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ThisViewSource")));
            ZahlungsbedingungenLookup = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ZahlungsbedingungenLookup")));
            ZahlungsbedingungenLookup.Source = db.StammZahlungsbedingungen;

            if (ID == 0)
            {

                
                var Query = from n in db.firmen
                            select n;

                ViewSource.Source = new FirmaCollection(Query, db);
                View = (ListCollectionView)ViewSource.View;
               
                var i = (firma)View.AddNew();
                i.name = "Neue Firma";
               
                i.istFirma = 3;
                i.IstKunde = 1;
                //kunde.KdNr = (int)db.firmen.Max(f => f.KdNr) + 1;
                i.KdNr = (int)db.firmen.Max(id => id.id) + 10001;
                i.istVerarbeitet = 1;
                // kunde.KdNr = KdNr;
                i.created = DateTime.Now;
                View.CommitNew();
                bNew = true;

            }
            else
            {
                var Query = from n in db.firmen
                            where n.id == ID
                            select n;

                ViewSource.Source = Query;

            }

        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (DoCancel())
            {
                db.Dispose();
                this.Close();

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

                var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
                foreach (var o in om)
                {

                    if (o.Entity is firma)
                    {

                        var p = (firma)o.Entity;
                        if (p.EntityState == System.Data.EntityState.Added)
                        {

                            bool KeyInUse = db.firmen.Any(c => c.name == p.name);
                            
                            if (KeyInUse)
                            {
                                MessageBox.Show(String.Format("Der Name {0} wird schon benutzt.", p.name));
                                return false;
                            }
                        }
                    }


                }

                db.SaveChanges();
                db.Dispose();
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
        }
    }
}
