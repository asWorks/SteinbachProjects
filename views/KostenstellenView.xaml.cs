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

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for KostenstellenView.xaml
    /// </summary>
    public partial class KostenstellenView : Window
    {


        ViewModels.KostenstellenViewModel vModel;
        CollectionViewSource FirmenViewSource;
        CollectionViewSource KostenstellenGruppenViewSource;
        CollectionViewSource KostenstellenFirmenViewSource;

        public KostenstellenView()
        {
            InitializeComponent();
            vModel = new ViewModels.KostenstellenViewModel();
            Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);



            FirmenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("FirmenViewSource")));
            KostenstellenGruppenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("KostenstellenGruppenViewSource")));
            KostenstellenFirmenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("KostenstellenFirmenViewSource")));

            LoadData();

        }

        private void LoadData()
        {
            FirmenViewSource.Source = vModel.Firmen;
            KostenstellenGruppenViewSource.Source = vModel.KostenstellenGruppen;
            KostenstellenFirmenViewSource.Source = vModel.KostenstellenFirmen;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DAL.SI_Kostenstellen ks = null;

            try
            {

                ComboBox cbo = sender as ComboBox;
                int index = (cbo.Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;
                //var xx = (cbo.Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row;
                //Console.WriteLine(xx == null);
                this.KostenstellenGrid.SelectedIndex = index;

                ks = (DAL.SI_Kostenstellen)KostenstellenGrid.SelectedItem;
                // Console.WriteLine(ks == null);

            }
            catch (Exception)
            {


            }

            try
            {
                if (e.AddedItems.Count == 1)
                {
                    vModel.CurrentKS = ks;
                    var x = (DAL.SI_KostenstellenGruppen)e.AddedItems[0];
                    vModel.SetKostenstelle(x);
                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }


        }

        private void DeleteKostenstellenFirmen_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int index = (btn.Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;
            this.KostenstellenFirmenGrid.SelectedIndex = index;

        }

        private void DeleteKostenstellenGruppen_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int index = (btn.Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;
            this.KostenStellenGruppenGrid.SelectedIndex = index;
        }

        private void DeleteKostenstellen_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int index = (btn.Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;
            this.KostenstellenGrid.SelectedIndex = index;
        }
    }
}
