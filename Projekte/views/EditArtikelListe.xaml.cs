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
using ProjektDB.ObjectCollections;
using DAL;
using System.Collections.ObjectModel;
//using Seagull.BarTender.Print;
using Syncfusion.Windows.Tools.Controls;
using CommonTools.Tools;
using DAL.Tools;
using System.Diagnostics;  


  

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for PrintArtikelLabel.xaml
    /// </summary>
    public partial class EditArtikelListe : System.Windows.Window
    {

        SteinbachEntities db;
        CollectionViewSource ViewSource;
        CollectionViewSource Kategorien;
        CollectionViewSource Lieferanten;
        CollectionViewSource WKZ;
        CollectionViewSource Mitarbeiter;
        private BindingListCollectionView View;

        public EditArtikelListe()
        {
            InitializeComponent();

            Trace.WriteLine("EditArtikelListe.Xaml.cs Constructor Start");
            Trace.Flush();
            LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListe.Xaml.cs - Constructor Start", DateTime.Now.ToLongTimeString());
            var VModel = new ViewModels.EditArtikelListeViewModel();
            Caliburn.Micro.ViewModelBinder.Bind(VModel, this, null);
            Trace.WriteLine("EditArtikelListe.Xaml.cs Constructor Stop");
            Trace.Flush();
            LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListe.Xaml.cs - Constructor Stop", DateTime.Now.ToLongTimeString());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Trace.WriteLine("EditArtikelListe.Xaml.cs Window Loaded Start");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListe.Xaml.cs - Window Loaded Start", DateTime.Now.ToLongTimeString());
                db = new SteinbachEntities();
                
                Kategorien = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Kategorien")));
                Lieferanten = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Lieferanten")));
                WKZ = ((System.Windows.Data.CollectionViewSource)(this.FindResource("WKZ")));
                Mitarbeiter = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Mitarbeiter")));

                Kategorien.Source = CommonTools.Tools.HelperTools.GetAuswahlEintraege("ArtikelKategorie", 1);
                Lieferanten.Source = db.firmen.Where(f => f.istFirma == 1);
                WKZ.Source = db.StammWaehrungen;
                Mitarbeiter.Source = db.personen;

                DoLoadColumns();

                ShowSelectedColumns();

                var tools = new libc1DatagridTools.ManageC1GridColumns(c1DataGrid1);
                tools.LoadGridSettings(Properties.Settings.Default.DatagridSettingsLagerliste);

                Trace.WriteLine("EditArtikelListe.Xaml.cs Window Loaded done");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListe.Xaml.cs - Window Loaded Stop", DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }


        }


        private void ShowSelectedColumns()
        {
            try
            {
                Trace.WriteLine("EditArtikelListe.Xaml.cs - ShowSelectedColumns Start");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListe.Xaml.cs - ShowSelectedColumns Start", DateTime.Now.ToLongTimeString());
                foreach (var item in clbColumns.Items)
                {

                    var res = (ZMetaArtikellistColumn)item;
                    this.c1DataGrid1.Columns[(int)res.ColumnIndex].Visibility = Visibility.Collapsed;

                }

                foreach (var item in clbColumns.SelectedItems)
                {

                    var res = (ZMetaArtikellistColumn)item;
                    this.c1DataGrid1.Columns[(int)res.ColumnIndex].Visibility = Visibility.Visible;

                }

                Trace.WriteLine("EditArtikelListe.Xaml.cs - ShowSelectedColumns done");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListe.Xaml.cs - ShowSelectedColumns Stop", DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ShowSelectedColumns();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (var db = new SteinbachEntities())
            {

                try
                {
                    var query = db.ZMetaArtikellistColumn;
                    foreach (var item in query)
                    {
                        item.isSelected = 0;
                    }

                    foreach (var item in clbColumns.SelectedItems)
                    {
                        var res = (ZMetaArtikellistColumn)item;
                        var i = db.ZMetaArtikellistColumn.Where(n => n.id == res.id).SingleOrDefault();
                        i.isSelected = 1;
                    }


                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    ErrorMethods.HandleStandardError(ex, "Fehler beim Abspeichern der Datagrideinstellungen");
                }



            }


            try
            {
                var tools = new libc1DatagridTools.ManageC1GridColumns(c1DataGrid1);
                Properties.Settings.Default.DatagridSettingsLagerliste = tools.SaveDataGridSettings();
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex, "Fehler beim Abspeichern der Datagrideinstellungen");
            }



        }

        private void LoadColumns_Click(object sender, RoutedEventArgs e)
        {
            //var cols = db.ZMetaArtikellistColumn.Where(n => n.isSelected == 1);

            //foreach (var item in cols)
            //{

            //    clbColumns.SelectedItems.Add(item);

            //}




            //cboList.SelectedItem = cboList.Items[1];
            //clbColumns.SelectedItems.Add(cboList.SelectedItem);

            //cboList.SelectedItem = cboList.Items[2];
            //clbColumns.SelectedItems.Add(cboList.SelectedItem);



            //ZMetaArtikellistColumn yy = db.ZMetaArtikellistColumn.Where(n => n.id == 2).SingleOrDefault();
            //clbColumns.SelectedItems.Add(yy);



            //if (cboList.SelectedItem != null)
            //{
            //    var x = (ZMetaArtikellistColumn)cboList.SelectedItem;
            //    clbColumns.SelectedItems.Add(x);
            //}

            // ShowSelectedColumns();

        }

        private void DoLoadColumns()
        {
            try
            {
                Trace.WriteLine("EditArtikelListe.Xaml.cs - DoLoadColumns Start");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListe.Xaml.cs - DoLoadColumns Start", DateTime.Now.ToLongTimeString());
                var Source = db.ZMetaArtikellistColumn.Where(n => n.isSelected == 1);
                foreach (var item in Source)
                {
                    cboList.SelectedItem = cboList.Items[(int)item.idx];
                    clbColumns.SelectedItems.Add(cboList.SelectedItem);
                }

                Trace.WriteLine("EditArtikelListe.Xaml.cs - DoLoadColumns done");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListe.Xaml.cs - DoLoadColumns Stop", DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            int index = ((btn.Parent as StackPanel).Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;
            this.c1DataGrid1.SelectedIndex = index;

        }

    }
}
