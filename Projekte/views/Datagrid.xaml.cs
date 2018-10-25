using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ProjektDB.Repositories;
using ProjektDB.Tools;
using DAL;
using System.Windows.Interop;

namespace ProjektDB
{
    /// <summary>
    /// Interaction logic for Datagrid.xaml
    /// </summary>
    public partial class Datagrid : Page
    {

        SteinbachEntities db;

        private ListCollectionView ProjekteView;
        private CollectionViewSource ProjekteViewSource;
        private Dictionary<string, string> Filter = new Dictionary<string, string>();
        private GetC1DatagridFilter c1Filter;


        public Datagrid()
        {

            InitializeComponent();
            db = new SteinbachEntities();

            this.MyPager.eventPageChanged += new PageControl.Pager.DelegatePageChanged(MyPager_eventPageChanged);
            c1Filter = new GetC1DatagridFilter();
            test();

        }



        //    //#############################################################################################
        //    Window wnd = new Window();

        //    wnd.Loaded += (sender, e) =>
        //    {
        //        HwndSource source = (HwndSource)PresentationSource.FromDependencyObject(wnd);
        //        source.AddHook(WindowProc);
        //    };

        //    wnd.Show();


        //}


        //private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{

        //    switch (msg)
        //    {

        //        case 0x11:

        //        case 0x16:

        //            Console.WriteLine("Close reason: WindowsShutDown");
        //            MessageBox.Show("Close reason: WindowsShutDown");

        //            break;



        //        case 0x112:

        //            if ((LOWORD((int)wParam) & 0xfff0) == 0xf060)

        //                Console.WriteLine("Close reason: UserClosing");
        //            MessageBox.Show("Close reason: UserClosing");

        //            break;

        //    }

        //    return IntPtr.Zero;

        //}



        //private static int LOWORD(int n)
        //{

        //    return (n & 0xffff);

        //}



        ////#####################################################################################################

        public Datagrid(string Order)
        {

            MessageBox.Show(Order);
            InitializeComponent();
            db = new SteinbachEntities();

            this.MyPager.eventPageChanged += new PageControl.Pager.DelegatePageChanged(MyPager_eventPageChanged);
            c1Filter = new GetC1DatagridFilter();
            test();
        }


        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {

            int rpp;
            c1Filter.ResetFilter(this.testListView, (bool)this.chkIncludePlatzhalter.IsChecked);

            var temp = new NavigationRepository(db);
            if (cboRecordsPerPage.Text == "alle")
                rpp = temp.RecordCount;
            else
                rpp = int.Parse(cboRecordsPerPage.Text);

            var ab = temp.GetBrunvollViewData(c1Filter.filterString, 0, rpp);
            ProjekteViewSource.Source = ab;
            ProjekteView = (ListCollectionView)ProjekteViewSource.View;
            this.MyPager.Reset(1, rpp, temp.RecordCount);


        }

        void MyPager_eventPageChanged(object sender, PageControl.PageChangedEventArgs e)
        {

            var temp = new NavigationRepository(db);
            var ab = temp.GetBrunvollViewData(c1Filter.filterString, e.toSkip, e.toTake);
            ProjekteViewSource.Source = ab;
            ProjekteView = (ListCollectionView)ProjekteViewSource.View;
        }

        void test()
        {

            try
            {

                var source = from p in db.vwBrunvollAuftragsbestand
                             where p.projektnummer == "110708"  //"Gearbox, HPU, Electronics"
                             select p;


                var x = LinQtoObsCol.Linq2DataTable(source);


                var ab = new bvAuftragsBestand(source, db);
                ProjekteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Listview_ViewSource")));
                ProjekteViewSource.Source = ab;
                ProjekteViewSource.GroupDescriptions.Add(new PropertyGroupDescription("projektnummer"));
                ProjekteView = (ListCollectionView)ProjekteViewSource.View;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }



        }


        private void RunProjekt_Click(object sender, RoutedEventArgs e)
        {

            vwBrunvollAuftragsbestand p = (vwBrunvollAuftragsbestand)ProjekteView.CurrentItem;

            if (this.testListView.IsEnabled == true)
            {

                this.testListView.IsEnabled = false;
                this.testListView.Visibility = Visibility.Collapsed;
            }
            else
            {

                this.testListView.IsEnabled = true;
                this.testListView.Visibility = Visibility.Visible;
            }

        }


        private void ProjekteItem_GotFocus(object sender, EventArgs e)
        {
            var item = (ListViewItem)sender;
            this.testListView.SelectedItem = item.DataContext;
        }




        private void testListView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    vwBrunvollAuftragsbestand p = (vwBrunvollAuftragsbestand)ProjekteView.CurrentItem;
            //    MainWindow mw = new MainWindow((int)p.id);
            //    this.NavigationService.Navigate(mw);
            //    e.Handled = true;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);

            //}

        }



        //private void readFilter()
        //{



        //}



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vwBrunvollAuftragsbestand p = (vwBrunvollAuftragsbestand)ProjekteView.CurrentItem;
                Session.CurrentProjectID = (int)p.id;
                MainWindow mw = new MainWindow((int)p.id);
                this.NavigationService.Navigate(mw);
                e.Handled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnCopyProjekt_Click(object sender, RoutedEventArgs e)
        {

        }




    }

}
