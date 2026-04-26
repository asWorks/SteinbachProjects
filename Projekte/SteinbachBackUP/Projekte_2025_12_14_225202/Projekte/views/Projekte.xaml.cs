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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DAL;
using ProjektDB.ObjectCollections;
using ProjektDB.Repositories;
using System.Collections.ObjectModel;

using System.Xml;
using System.IO;
using System.Windows.Markup;
using ProjektDB.Tools;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for TestAdd.xaml
    /// </summary>
    public partial class Projekte : Window
    {

        SteinbachEntities db;

        private ListCollectionView ProjekteView;
        private CollectionViewSource ProjekteViewSource;

        private GetC1DatagridFilter c1Filter;
        private string preFilter;
        private int _recordCount;
        private string _data;


        public Projekte()
        {

            InitializeComponent();
            db = new SteinbachEntities();
            this.MyPager.eventPageChanged += new PageControl.Pager.DelegatePageChanged(MyPager_eventPageChanged);
            c1Filter = new GetC1DatagridFilter();






            //TestCloneXaml();
            //test();
        }

        public Projekte(string data, string Caption, string struktur, string preFilter)
        {

            InitializeComponent();
            db = new SteinbachEntities();
            this.MyPager.eventPageChanged += new PageControl.Pager.DelegatePageChanged(MyPager_eventPageChanged);
            c1Filter = new GetC1DatagridFilter();
            tbDescription.Text = Caption;
            this.preFilter = preFilter;
            _data = data;
            InitForm();

            // c1Filter.ResetFilter(preFilter, this.testListView, (bool)chkIncludePlatzhalter.IsChecked);
            ProjekteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Listview_ViewSource")));
            // TestCloneXaml();

            ColumnDescription.AddStructure(this.testListView, struktur);
            FillList();

        }

        private void InitForm()
        {
            int year = int.Parse(DateTime.Now.ToString("yyyy"));
            for (int i = 2004; i <= year + 1; i++)
            {

                cboJahrBis.Items.Add(i.ToString());
                cboJahrVon.Items.Add(i.ToString());
            }

            cboJahrVon.Text = year.ToString();
            cboJahrBis.Text = year.ToString();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {

            //int rpp = int.Parse(cboRecordsPerPage.Text);
            //c1Filter.ResetFilter(this.preFilter,this.testListView, (bool)this.chkIncludePlatzhalter.IsChecked);

            //var temp = new NavigationRepository(db);

            //var ab = temp.GetLikeTest2(c1Filter.filterString, 0, rpp);
            //ProjekteViewSource.Source = ab;
            //ProjekteView = (ListCollectionView)ProjekteViewSource.View;
            //this.MyPager.Reset(1, rpp, temp.RecordCount);

            FillList();
        }

        void MyPager_eventPageChanged(object sender, PageControl.PageChangedEventArgs e)
        {

            ProjekteViewSource.Source = GetData(e.toSkip, e.toTake);
            ProjekteView = (ListCollectionView)ProjekteViewSource.View;
        }

        object GetData(int toSkip, int toTake)
        {
            switch (_data)
            {
                case "BrunvollView":
                    {
                        var temp = new NavigationRepository(db);
                        object ab = temp.GetBrunvollViewData(c1Filter.filterString, toSkip, toTake);
                        _recordCount = temp.RecordCount;
                        return ab;

                    }
                case "Projekt":
                    {
                        var temp = new NavigationRepository(db);
                        object ab = temp.GetProjektData(c1Filter.filterString, toSkip, toTake);
                        _recordCount = temp.RecordCount;
                        return ab;
                    }

                case "ProjektAnlagentyp":
                    {
                        var temp = new NavigationRepository(db);
                        object ab = temp.GetProjektAnlagentypData(c1Filter.filterString, toSkip, toTake);
                        _recordCount = temp.RecordCount;
                        return ab;
                    }


                default:
                    return null;

            }


        }




        void FillList()
        {


            try
            {
                string vJahr = String.Format("'{0}-01-01 00:00:00'", cboJahrVon.SelectedValue);
                string bJahr = String.Format("'{0}-12-31 23:59:59'", cboJahrBis.SelectedValue);
                string filterJahr = String.Format("it.Datum >= DATETIME{0} and it.Datum <= DATETIME{1}", vJahr, bJahr);
                string filter = filterJahr;
                if (this.preFilter != string.Empty)
                {
                    filter += " and " + preFilter;
                }
                int rpp = int.Parse(cboRecordsPerPage.Text);
                c1Filter.ResetFilter(filter, this.testListView, (bool)this.chkIncludePlatzhalter.IsChecked);

                //var temp = new NavigationRepository(db);
                //var ab = temp.GetBrunvollViewData(c1Filter.filterString, 0, rpp);

                //   var query = from p in db.projekte where (p.datum >= new DateTime(2011,01,01)) && (p.firmenname == "Meier")  select p;

                ProjekteViewSource.Source = GetData(0, rpp);
                ProjekteView = (ListCollectionView)ProjekteViewSource.View;
                this.MyPager.Reset(1, rpp, _recordCount);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }



        }


        private void btnCopyProjekt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //vwBrunvollAuftragsbestand p = (vwBrunvollAuftragsbestand)ProjekteView.CurrentItem;
            System.Data.Objects.DataClasses.EntityObject p = (System.Data.Objects.DataClasses.EntityObject)ProjekteView.CurrentItem;

            var x = p.EntityKey.EntityKeyValues[0];

            this.Visibility = Visibility.Hidden;
            var ep = new EditProjekt((int)x.Value);
            ep.ShowDialog();
            this.Visibility = Visibility.Visible;

           // MainWindow mw = new MainWindow((int)x.Value);
           //// this.NavigationService.Navigate(mw);
        }


    }
}