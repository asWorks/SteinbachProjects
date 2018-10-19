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
    public partial class EditLagerorte : Window
    {

       
        SteinbachEntities db;
        CollectionViewSource LagerListeViewSource;
        CollectionViewSource LagerListeFullViewSource;
       

        #region Constructors

        public EditLagerorte(int id)
        {

            InitializeComponent();
            db = new SteinbachEntities();
            LagerListeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerListeViewSource")));
            LagerListeFullViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("LagerListeFullViewSource")));
            LagerListeViewSource.Source = db.lagerlisten.Where(n => n.artikelnr == "0");
            LagerListeFullViewSource.Source = db.lagerlisten;
            var vModel = new ViewModels.LagerbestaendeViewModel(id,this.Dispatcher);
            Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);
           
        }

        //public EditLagerorte()
        //{
        //    InitializeComponent();

        //    var vModel = new ViewModels.SI_BelegeViewModel();
        //    Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);
        //}

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

        #endregion
        #region PreparingWindow
        private void FillProject()
        {
          

        }

        private void cmdDeletePosition_Click(object sender, RoutedEventArgs e)
        {
           
        }


        private void Details_ListView_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.LagerorteBestaende.SelectedItem = item.DataContext;

        }


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

      

    }
}
