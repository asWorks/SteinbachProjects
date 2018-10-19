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
using ProjektDB.Temp;
using DAL;
using ProjektDB.ObjectCollections;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for EditFirma.xaml
    /// </summary>
    public partial class EditPerson : Window
    {
     
        SteinbachEntities db;
        int ID = 0;
        CollectionViewSource ViewSource;
        private ListCollectionView View;

        //bool bNew = false;
        //person f = null;

        public EditPerson()
        {
            InitializeComponent();
        }

        public EditPerson(int id)
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

            

            if (ID == 0)
            {

                
                var Query = from n in db.personen
                            select n;

                ViewSource.Source = new PersonCollection(Query, db);
                View = (ListCollectionView)ViewSource.View;
                var i = (person)View.AddNew();
               
                View.CommitNew();
               

            }
            else
            {
                if (Session.User.rights != "admin")
                {
                    this.SPBerechtigungen.Visibility = Visibility.Collapsed;
                    this.SPPasswort.Visibility = Visibility.Collapsed;
                    this.SPMailTimerActive.Visibility = Visibility.Collapsed;
                    this.SPMailTimerInterval.Visibility = Visibility.Collapsed;
                }
                var Query = from n in db.personen
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

                    if (o.Entity is person)
                    {

                        var p = (person)o.Entity;
                        if (p.EntityState == System.Data.EntityState.Added)
                        {

                            bool KeyInUse = db.personen.Any(c => c.nachname == p.nachname);
                            
                            if (KeyInUse)
                            {
                                MessageBox.Show(String.Format("Der Name {0} wird schon benutzt.", p.nachname));
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
