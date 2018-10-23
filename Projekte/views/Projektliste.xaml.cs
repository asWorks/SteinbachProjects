using C1.WPF;
using CommonTools.Tools;
using DAL;
using ProjektDB.Repositories;
using ProjektDB.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for TestAdd.xaml
    /// </summary>
    public partial class Projektliste : Page
    {
        ContextMenu CM;
        SteinbachEntities db;

        private ListCollectionView ProjekteView;
        private CollectionViewSource ProjekteViewSource;

        private GetC1DatagridFilter c1Filter;
        private string preFilter;
        private int _recordCount;
        private string _data;
        private ListeTypeEnum ListenTyp;
        private Dictionary<string, enumDataType> DataTypes;



        public Projektliste()
        {

            InitializeComponent();
            db = new SteinbachEntities();
            this.MyPager.eventPageChanged += new PageControl.Pager.DelegatePageChanged(MyPager_eventPageChanged);
            c1Filter = new GetC1DatagridFilter();


        }

        void mi_Click(object sender, RoutedEventArgs e)
        {

            switch (ListenTyp)
            {
                case ListeTypeEnum.Projekt:
                    {
                        try
                        {
                            if (ProjekteView.CurrentItem.GetType().Equals(new DAL.projekt().GetType()))
                            {
                                var item = (projekt)this.ProjekteView.CurrentItem;
                                MessageBox.Show(item.projektnummer);
                            }
                        }
                        catch (Exception)
                        {


                        }
                        break;
                    }

                case ListeTypeEnum.Kalkulationliste:
                    break;
                case ListeTypeEnum.Schiff:
                    break;
                case ListeTypeEnum.Firma:
                    break;
                case ListeTypeEnum.Person:
                    break;
                case ListeTypeEnum.Aggregat:
                    break;
                case ListeTypeEnum.Lagerliste:
                    break;
                case ListeTypeEnum.SI_Beleg:
                    {
                        try
                        {
                            if (ProjekteView.CurrentItem.GetType().Equals(new DAL.SI_Belege().GetType()))
                            {
                                var item = (SI_Belege)this.ProjekteView.CurrentItem;
                                MessageBox.Show(item.id_Vorgang.ToString());
                            }
                        }
                        catch (Exception)
                        {


                        }
                        break;
                    }

                case ListeTypeEnum.SI_Inventuren:
                    break;
                case ListeTypeEnum.Artikel:
                    break;
                case ListeTypeEnum.Zahlungsbedingung:
                    break;
                case ListeTypeEnum.ProjektListe:
                    break;
                case ListeTypeEnum.Standard:
                    break;
                case ListeTypeEnum.Lagerorte:
                    break;
                case ListeTypeEnum.Textbausteine:
                    break;
                default:
                    break;
            }



        }


        public Projektliste(string data, string Caption, string struktur, string preFilter)
        {
            DoConstruct(data, Caption, struktur, preFilter, false, ListeTypeEnum.Projekt, true);

        }

        public Projektliste(string data, string Caption, string struktur, string preFilter, bool showKommissionFilter)
        {

            DoConstruct(data, Caption, struktur, preFilter, showKommissionFilter, ListeTypeEnum.Projekt, true);

        }

        public Projektliste(string data, string Caption, string struktur, string preFilter, bool showKommissionFilter, ListeTypeEnum Typ)
        {

            DoConstruct(data, Caption, struktur, preFilter, showKommissionFilter, Typ, true);

        }

        public Projektliste(string data, string Caption, string struktur, string preFilter, bool showKommissionFilter, ListeTypeEnum Typ, bool showFilterYear)
        {

            DoConstruct(data, Caption, struktur, preFilter, showKommissionFilter, Typ, showFilterYear);

        }

        void DoConstruct(string data, string Caption, string struktur, string preFilter, bool showKommissionFilter, ListeTypeEnum ListenTyp, bool showDatumFilter)
        {

            try
            {
                InitializeComponent();
                this.ListenTyp = ListenTyp;
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

                Format.FormatProjektliste.ShowKommissionFilter(this, showKommissionFilter);
                //if (showKommissionFilter)
                //{
                //    cboKommissionBezahlt.Visibility = System.Windows.Visibility.Visible;
                //}
                //else
                //{
                //    cboKommissionBezahlt.Visibility = Visibility.Hidden;

                //}

                Format.FormatProjektliste.ShowAddNewButton(this, ListenTyp);

                //if (ListenTyp == ListeTypeEnum.Person)
                //{
                //    if (Session.User.rights != "admin")
                //        btnNewHigh.Visibility = Visibility.Hidden;

                //}

                Format.FormatProjektliste.ShowDatumFilter(this, showDatumFilter);
                Format.FormatProjektliste.ShowKundenFilter(this, ListenTyp);
                Format.FormatProjektliste.ShowPrintButton(this, ListenTyp);

                //if (showDatumFilter == false)
                //{
                //    this.spJahrBis.Visibility = Visibility.Hidden;
                //    this.spJahrVon.Visibility = Visibility.Hidden;
                //}
                //else
                //{
                //    this.spJahrBis.Visibility = Visibility.Visible;
                //    this.spJahrVon.Visibility = Visibility.Visible;
                //}

                BuildContextMenu();



                FillList();
            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
            }


        }

        private void BuildContextMenu()
        {
            try
            {

                switch (ListenTyp)
                {
                    case ListeTypeEnum.Projekt:
                        {
                            // CM = new ContextMenu();
                            // var mi = new MenuItem();
                            // mi.Header = "Jetzt auch im Projekt";
                            // mi.Click += mi_Click;
                            // CM.Items.Add(mi);
                            // this.testListView.ContextMenu = CM;
                            //// ContextMenuService.SetShowOnDisabled(this.testListView, true);
                            break;
                        }

                    case ListeTypeEnum.Kalkulationliste:
                        break;
                    case ListeTypeEnum.Schiff:
                        break;
                    case ListeTypeEnum.Firma:
                        break;
                    case ListeTypeEnum.Person:
                        break;
                    case ListeTypeEnum.Aggregat:
                        break;
                    case ListeTypeEnum.Lagerliste:
                        break;
                    case ListeTypeEnum.SI_Beleg:
                        {


                            CM = new ContextMenu();
                            var mi = new MenuItem();
                            mi.Header = "Vorgang";
                            mi.Click += mi_Click;
                            CM.Items.Add(mi);
                            this.testListView.ContextMenu = CM;
                            //ContextMenuService.SetShowOnDisabled(Me, True) 
                            //ContextMenuService.SetShowOnDisabled(this.testListView, true);
                            break;
                        }

                    case ListeTypeEnum.SI_Inventuren:
                        break;
                    case ListeTypeEnum.Artikel:
                        break;
                    case ListeTypeEnum.Zahlungsbedingung:
                        break;
                    case ListeTypeEnum.ProjektListe:
                        break;
                    case ListeTypeEnum.Standard:
                        break;
                    case ListeTypeEnum.Lagerorte:
                        break;
                    case ListeTypeEnum.Textbausteine:
                        break;
                    default:
                        break;
                }





            }
            catch (Exception)
            {


            }




        }




        private void InitForm()
        {
            try
            {
                int year = int.Parse(DateTime.Now.ToString("yyyy"));
                for (int i = 1998; i <= year + 1; i++)
                {

                    cboJahrBis.Items.Add(i.ToString());
                    cboJahrVon.Items.Add(i.ToString());
                }

                cboJahrVon.Text = year.ToString();
                cboJahrBis.Text = year.ToString();


            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
            }

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


        /// <summary>
        /// Liefert in Abhängigkeit vom im Constructor übergebenen Parameter 'Data' die neue DataSource für die ViewSource zurück
        /// </summary>
        /// <param name="toSkip">Anzahl der zu überspringenden Datensätze</param>
        /// <param name="toTake">Anzahl der anzuzeigenden Datensätze</param>
        /// <returns>object - Generische Ansätze sind aufgrund der </returns>
        object GetData(int toSkip, int toTake)
        {

            try
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

                    case "ProjektMitVerlauf":
                        {
                            var temp = new NavigationRepository(db);
                            object ab = temp.GetProjektMitVerlaufData(c1Filter.filterString, toSkip, toTake);
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

                    case "Projekt_ProjektRechnung":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetProjektRechnungenData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    //case "Projekt_ProjektRechnungKommission":
                    //    {
                    //        var temp = new NavigationRepository(db);

                    //        object ab = temp.GetProjektRechnungenKommissionData(c1Filter.filterString, toSkip, toTake);
                    //        _recordCount = temp.RecordCount;
                    //        return ab;
                    //    }

                    case "Kalkulation":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetKalkulationenData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;

                            return ab;
                        }

                    case "Schiff":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetSchiffeData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    case "Firma":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetFirmaData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    case "Person":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetPersonData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    case "Aggregat":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetAggregatData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    case "Zahlungsbedingung":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetStammZahlungsbedingungData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    case "Lagerliste":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetLagerlisteData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    case "SI_Belege":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetSI_BelegeData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    case "Lagerorte":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetLagerorteData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    case "Textbausteine":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetTextbausteineData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    case "SI_Inventuren":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetSI_InventurenData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    // OffeneAusgaenge


                    case "OffeneEinAusgaenge":
                        {
                            var temp = new NavigationRepository(db);

                            object ab = temp.GetOffeneEingaengeData(c1Filter.filterString, toSkip, toTake);
                            _recordCount = temp.RecordCount;
                            return ab;
                        }

                    //case "OffeneEingaenge":
                    //    {
                    //        var temp = new NavigationRepository(db);

                    //        object ab = temp.GetOffeneEingaengeData(c1Filter.filterString, toSkip, toTake);
                    //        _recordCount = temp.RecordCount;
                    //        return ab;
                    //    }





                    default:
                        return null;

                }


            }
            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);
                return null;
            }


        }




        void FillList()
        {


            try
            {
                db = new SteinbachEntities();
                string filterJahr = string.Empty;
                string vJahr = String.Format("'{0}-01-01 00:00:00'", cboJahrVon.SelectedValue);
                string bJahr = String.Format("'{0}-12-31 23:59:59'", cboJahrBis.SelectedValue);

                if (ListenTyp == ListeTypeEnum.Projekt)
                {
                    filterJahr = String.Format("it.Datum >= DATETIME{0} and it.Datum <= DATETIME{1}", vJahr, bJahr);
                }
                else if (ListenTyp == ListeTypeEnum.Kalkulationliste)
                {
                    filterJahr = String.Format("it.created >= DATETIME{0} and it.created <= DATETIME{1}", vJahr, bJahr);
                }

                string filter = filterJahr;


                if (cboKommissionBezahlt.Visibility == Visibility.Visible)
                {
                    if (cboKommissionBezahlt.Text == "Offen")
                    {
                        filter += " and it.kommissionerhalten is null";
                    }
                    else
                    {
                        filter += " and it.kommissionerhalten is not null";
                    }

                }

                if (chkAlleFirmenAnzeigen.Visibility == Visibility.Visible)
                {
                    if (chkAlleFirmenAnzeigen.IsChecked == false)
                    {
                        if (filter != string.Empty)
                        {
                            filter += " and it.IstKunde == 1";
                        }
                        else
                        {
                            filter = "it.IstKunde == 1";
                        }

                    }
                }

                if (this.preFilter != string.Empty)
                {
                    filter += " and " + preFilter;
                }
                int rpp = int.Parse(cboRecordsPerPage.Text);
                c1Filter.ResetFilter(filter, this.testListView, (bool)this.chkIncludePlatzhalter.IsChecked);

                //var query = from t in db.vwProjekt_ProjektRechnungen where t.kommissionerhalten.HasValue == false select t;

                //var temp = new NavigationRepository(db);
                //var ab = temp.GetBrunvollViewData(c1Filter.filterString, 0, rpp);

                //   var query = from p in db.projekte where (p.datum >= new DateTime(2011,01,01)) && (p.firmenname == "Meier")  select p;

                ProjekteViewSource.Source = GetData(0, rpp);
                ProjekteView = (ListCollectionView)ProjekteViewSource.View;
                this.MyPager.Reset(1, rpp, _recordCount);
                // MessageBox.Show(c1Filter.filterString);
                RestoreFilter();



            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message);
                ErrorMethods.HandleStandardError(ex);
            }



        }

        #region "Copy and Link Projects"

        private void btnCopyProjekt_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Die Kopierfunktion ist im Moment noch nicht implementiert.");
            //return;

            Button btn = sender as Button;
            int index = ((btn.Parent as StackPanel).Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;

            this.testListView.SelectedIndex = index;




            switch (ListenTyp)
            {
                case ListeTypeEnum.Projekt:
                    {
                        if (MessageBox.Show(GetListenType() + "wirklich kopieren ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            try
                            {
                                Cursor = System.Windows.Input.Cursors.Wait;
                                db.SaveChanges();
                                using (var data = new SteinbachEntities())
                                {
                                    // projekt p1 = (projekt)ProjekteView.CurrentItem;
                                    var p1 = (projekt)this.testListView.SelectedItem;
                                    projekt p2 = (projekt)p1.Clone();
                                    var Rep = new ProjektRepository(data);
                                    //p2.projektnummer = "999888";
                                    p2.projektnummer = Rep.GetNewProjektnummerWithYear();
                                    p2.referenzkdbestellnummer = "Kopie von Projekt " + p1.projektnummer;
                                    p2.created = DateTime.Now;
                                    data.AddToprojekte(p2);
                                    data.SaveChanges();

                                    MainWindow mw = new MainWindow(p2.id);
                                    this.NavigationService.Navigate(mw);


                                }

                                db = new SteinbachEntities();
                                Cursor = System.Windows.Input.Cursors.Arrow;
                            }


                            catch (Exception ex)
                            {
                                Cursor = System.Windows.Input.Cursors.Arrow;
                                DAL.Tools.LoggingTool.LogExeption(ex, "Projektliste.xaml.cs", "btnCopyProjekt_Click", DAL.Tools.LoggingTool.LogState.high);
                                MessageBox.Show(String.Format("Kopieren des Projektes nicht erfolgreich.\n{0}", ex.Message));
                            }
                        }
                        break;
                    }

                case ListeTypeEnum.Kalkulationliste:
                    {
                        if (MessageBox.Show(GetListenType() + "wirklich kopieren ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {

                            try
                            {
                                Cursor = System.Windows.Input.Cursors.Wait;
                                db.SaveChanges();
                                using (var data = new SteinbachEntities())
                                {

                                    // var p1 = (kalkulationstabelle)ProjekteView.CurrentItem;
                                    var p1 = (kalkulationstabelle)this.testListView.SelectedItem;
                                    var p2 = (kalkulationstabelle)p1.Clone();

                                    p2.created = DateTime.Now;
                                    p2.projektnummer = string.Empty;

                                    data.AddTokalkulationstabellen(p2);
                                    data.SaveChanges();



                                    Kalkulation k = new Kalkulation(p2.id);
                                    k.ShowDialog();

                                }

                                db = new SteinbachEntities();
                                Cursor = System.Windows.Input.Cursors.Arrow;

                            }
                            catch (Exception ex)
                            {

                                Cursor = System.Windows.Input.Cursors.Arrow;
                                DAL.Tools.LoggingTool.LogExeption(ex, "Projektliste.xaml.cs", "btnCopyProjekt_Click", DAL.Tools.LoggingTool.LogState.high);
                                MessageBox.Show(String.Format("Kopieren des Projektes nicht erfolgreich.\n{0}", ex.Message));
                            }

                        }
                        break;
                    }

                default:
                    {

                        MessageBox.Show(string.Format("Für {0} ist kopieren nicht eingerichtet.", GetListenType()), "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        break;
                    }


            }


        }

        #endregion


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {


                Button btn = sender as Button;
                int index = ((btn.Parent as StackPanel).Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;

                this.testListView.SelectedIndex = index;




                // System.Data.Objects.DataClasses.EntityObject p = (System.Data.Objects.DataClasses.EntityObject)ProjekteView.CurrentItem;
                System.Data.Objects.DataClasses.EntityObject p = (System.Data.Objects.DataClasses.EntityObject)testListView.SelectedItem;

                var x = p.EntityKey.EntityKeyValues[0];


                switch (ListenTyp)
                {
                    case ListeTypeEnum.Projekt:
                        {



                            MainWindow mw = new MainWindow((int)x.Value);
                            this.NavigationService.Navigate(mw);


                            break;
                        }

                    case ListeTypeEnum.Kalkulationliste:
                        {
                            //System.Data.Objects.DataClasses.EntityObject p = (System.Data.Objects.DataClasses.EntityObject)ProjekteView.CurrentItem;
                            //var x = p.EntityKey.EntityKeyValues[0];

                            Kalkulation k = new Kalkulation((int)x.Value);
                            k.ShowDialog();

                            break;
                        }


                    case ListeTypeEnum.Schiff:
                        {
                            EditSchiff es = new EditSchiff((int)x.Value);
                            es.ShowDialog();


                            break;
                        }

                    case ListeTypeEnum.Firma:
                        {
                            // MessageBox.Show("Diese Ansicht ist noch nicht implmentiert");
                            EditFirma ef = new EditFirma((int)x.Value);
                            ef.ShowDialog();
                            //MainWindow mw = new MainWindow((int)x.Value);
                            //this.NavigationService.Navigate(mw);


                            break;
                        }

                    case ListeTypeEnum.Person:
                        {
                            if (Session.User.id != (int)x.Value && Session.User.rights != "admin")
                            {
                                MessageBox.Show("Es können nur die eigenen Personaleinträge bearbeitet werden");
                            }
                            else
                            {

                                EditPerson ep = new EditPerson((int)x.Value);
                                ep.ShowDialog();
                            }

                            //MessageBox.Show("Diese Ansicht ist noch nicht implmentiert");
                            //MainWindow mw = new MainWindow((int)x.Value);
                            //this.NavigationService.Navigate(mw);


                            break;
                        }

                    case ListeTypeEnum.Aggregat:
                        {

                            EditAggregat ea = new EditAggregat((int)x.Value);
                            ea.ShowDialog();

                            break;
                        }




                    case ListeTypeEnum.Artikel:
                        {

                            EditArtikel ea = new EditArtikel((int)x.Value);
                            ea.ShowDialog();

                            break;
                        }


                    case ListeTypeEnum.SI_Beleg:
                        {

                            EditBelege ea = new EditBelege((int)x.Value);
                            ea.ShowDialog();

                            break;
                        }

                    case ListeTypeEnum.Lagerorte:
                        {

                            var ea = new EditLagerorte((int)x.Value);
                            ea.ShowDialog();

                            break;
                        }

                    case ListeTypeEnum.Textbausteine:
                        {

                            var ea = new EditTextbausteine((int)x.Value);
                            ea.ShowDialog();

                            break;
                        }

                    case ListeTypeEnum.SI_Inventuren:
                        {
                            try
                            {
                                Inventur ea = new Inventur((int)x.Value);
                                ea.ShowDialog();
                            }
                            catch (Exception ex)
                            {

                                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
                            }


                            break;
                        }


                    case ListeTypeEnum.Zahlungsbedingung:
                        {

                            EditZahlungsbedingungen ea = new EditZahlungsbedingungen((int)x.Value);
                            ea.ShowDialog();

                            break;
                        }



                    default:
                        break;

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));
            }


        }

        private void btnDeleteProjekt_Click(object sender, RoutedEventArgs e)
        {

            try
            {


                Button btn = sender as Button;
                int index = ((btn.Parent as StackPanel).Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;

                this.testListView.SelectedIndex = index;
                var item = testListView.SelectedItem;
                ProjekteView.MoveCurrentTo(item);

                //var item = (ListViewItem)sender;
                //this.testListView.SelectedItem = item.DataContext;


                // System.Data.Objects.DataClasses.EntityObject p = (System.Data.Objects.DataClasses.EntityObject)ProjekteView.CurrentItem;
                //   System.Data.Objects.DataClasses.EntityObject p = (System.Data.Objects.DataClasses.EntityObject)testListView.SelectedItem;


                switch (ListenTyp)
                {

                    case ListeTypeEnum.Person:
                        {
                            if (Session.User.rights != "admin")
                            {
                                MessageBox.Show("Sie haben keine Berechtigung für diese Funktion.");

                            }
                            else
                            {
                                if (MessageBox.Show(GetListenType() + " wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {
                                    ProjekteView.Remove(ProjekteView.CurrentItem);
                                    db.SaveChanges();
                                }
                            }
                            break;
                        }

                    case ListeTypeEnum.Firma:
                        {
                            if (MessageBox.Show(GetListenType() + " wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {

                                var f = (firma)ProjekteView.CurrentItem;
                                if (db.projekte.Where(k => k.KdNr == f.KdNr).Count() == 0)
                                {
                                    ProjekteView.Remove(ProjekteView.CurrentItem);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    MessageBox.Show(string.Format("Dem Kunden {0} sind Projekte zugeordnet. Löschen ist nicht möglich.", f.name));
                                }


                            }
                            break;
                        }

                    case ListeTypeEnum.SI_Beleg:
                        {
                            if (MessageBox.Show(GetListenType() + " wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {

                                var f = (SI_Belege)ProjekteView.CurrentItem;
                                if (db.SI_BelegePositionen.Where(k => k.id_Beleg == f.id).Count() == 0)
                                {
                                    //testListView.SelectedItem;
                                    ProjekteView.Remove(ProjekteView.CurrentItem);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    MessageBox.Show(string.Format("Der Beleg {0} enthält Positionen. Löschen ist nicht möglich.", f.id));
                                }


                            }
                            break;
                        }

                    case ListeTypeEnum.SI_Inventuren:
                        {
                            if (MessageBox.Show(GetListenType() + " wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {

                                var f = (SI_Inventuren)ProjekteView.CurrentItem;
                                if (db.SI_InventurenPositionen.Where(k => k.id_inventur == f.id).Count() == 0)
                                {
                                    ProjekteView.Remove(ProjekteView.CurrentItem);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    MessageBox.Show(string.Format("Die Inventur {0} enthält Positionen. Löschen ist nicht möglich.", f.id));
                                }


                            }
                            break;
                        }


                    case ListeTypeEnum.Lagerorte:
                        {
                            if (MessageBox.Show(GetListenType() + " wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {



                                var f = (StammLagerorte)ProjekteView.CurrentItem;
                                if (db.Lagerbestaende.Where(k => k.id_Lagerort == f.id && k.Lagerbestand > 0).Count() == 0)
                                {
                                    foreach (var lb in f.Lagerbestaende.ToList())
                                    {
                                        db.DeleteObject(lb);
                                    }

                                    ProjekteView.Remove(ProjekteView.CurrentItem);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    MessageBox.Show(string.Format("Der Lagerplatz {0} enthält Positionen mit Lagerbeständen. Löschen ist nicht möglich.", f.Lagerortname));
                                }


                            }
                            break;
                        }

                    case ListeTypeEnum.Textbausteine:
                        {
                            try
                            {
                                if (MessageBox.Show(GetListenType() + " wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {

                                    var f = (StammTextbaustein)ProjekteView.CurrentItem;
                                    foreach (var BaTb in f.SI_BelegartenTextbausteine.ToList())
                                    {
                                        db.DeleteObject(BaTb);
                                    }

                                    ProjekteView.Remove(ProjekteView.CurrentItem);
                                    db.SaveChanges();

                                }
                            }
                            catch (Exception ex)
                            {

                                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Löschen des Textbausteines");
                            }

                            break;
                        }

                    default:
                        {
                            if (MessageBox.Show(GetListenType() + " wirklich löschen ?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {

                                ProjekteView.Remove(ProjekteView.CurrentItem);
                                db.SaveChanges();

                            }
                            break;
                        }



                }

            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.ShowErrorMessage(ex);
            }

        }

        private string GetListenType()
        {
            switch (ListenTyp)
            {
                case ListeTypeEnum.Projekt:
                    {

                        return "Projekt ";
                        //break;
                    }

                case ListeTypeEnum.Kalkulationliste:
                    {
                        return "Kalkulationsliste ";
                    }
                case ListeTypeEnum.Firma:
                    {
                        return "Firma ";
                    }
                case ListeTypeEnum.Schiff:
                    {
                        return "Schiff ";
                    }
                case ListeTypeEnum.Person:
                    {
                        return "Person ";
                    }

                case ListeTypeEnum.Aggregat:
                    {
                        return "Aggregat ";
                    }
                case ListeTypeEnum.Lagerliste:
                    {
                        return "Lagerliste ";
                    }
                case ListeTypeEnum.Artikel:
                    {
                        return "Artikel ";
                    }

                case ListeTypeEnum.SI_Beleg:
                    {
                        return "Beleg ";
                    }

                case ListeTypeEnum.Lagerorte:
                    {
                        return "Lagerort ";
                    }

                case ListeTypeEnum.Textbausteine:
                    {
                        return "Textbaustein ";
                    }

                case ListeTypeEnum.SI_Inventuren:
                    {
                        return "Inventur ";
                    }

                case ListeTypeEnum.Zahlungsbedingung:
                    {
                        return "Zahlungsbedingung ";
                    }


                default:
                    return "Nicht spezifizierter Typ ";
                    //break;

            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();

        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            switch (ListenTyp)
            {
                case ListeTypeEnum.Projekt:
                    {
                        var es = new MainWindow(0);
                        NavigationService.Navigate(es);

                        break;
                    }
                case ListeTypeEnum.Kalkulationliste:
                    Kalkulation k = new Kalkulation(0);
                    k.ShowDialog();
                    break;
                case ListeTypeEnum.Schiff:
                    {
                        EditSchiff es = new EditSchiff(0);
                        es.ShowDialog();

                        break;
                    }
                case ListeTypeEnum.Firma:
                    {
                        EditFirma ef = new EditFirma(0);
                        ef.ShowDialog();

                        break;
                    }

                case ListeTypeEnum.Person:
                    {
                        EditPerson ep = new EditPerson(0);
                        ep.ShowDialog();
                        break;
                    }
                case ListeTypeEnum.Artikel:
                    {
                        var ep = new EditArtikel(0);
                        ep.ShowDialog();
                        break;
                    }

                case ListeTypeEnum.SI_Beleg:
                    {
                        var ep = new EditBelege(0);
                        ep.ShowDialog();
                        break;
                    }

                case ListeTypeEnum.Lagerorte:
                    {
                        var ep = new EditLagerorte(0);
                        ep.ShowDialog();
                        break;
                    }


                case ListeTypeEnum.Textbausteine:
                    {
                        var ep = new EditTextbausteine(0);
                        ep.ShowDialog();
                        break;
                    }

                case ListeTypeEnum.SI_Inventuren:
                    {
                        var ep = new Inventur(0);
                        ep.ShowDialog();
                        break;
                    }

                case ListeTypeEnum.Zahlungsbedingung:
                    {
                        var ep = new EditZahlungsbedingungen(0);
                        ep.ShowDialog();
                        break;
                    }
                case ListeTypeEnum.Aggregat:
                    break;
                default:
                    break;
            }

        }


        private void testListView_LoadingRow(object sender, C1.WPF.DataGrid.DataGridRowEventArgs e)
        {
            try
            {
                //02.01.2013
                //var p = (person)e.Row.DataItem;
                //var r = (C1.WPF.DataGrid.DataGridRow)e.Row;

                // auskommentiert
                //---------------------------------------------------------------------------------------------
                // MessageBox.Show("Loading " + p.benutzername);

                //if (p != null)
                //{
                //    if (p.vorname == "Anja")
                //    {
                //     // MessageBox.Show(r[0].Name); 
                //    }
                //}

                //foreach (C1.WPF.DataGrid.DataGridColumn item in r.DataGrid.Columns)
                //{
                //    MessageBox.Show(item.Name);
                //}
            }
            catch (Exception)
            {


            }


        }


        private void RestoreFilter()
        {

            // CLearFilterRows();

            string buf = string.Empty;
            var filterrow = this.testListView.TopRows[0];
            foreach (var col in testListView.Columns)
            {

                var f = c1Filter.Filter;
                if (col.Name != null && f.ContainsKey(col.Name))
                {
                    bool found = f.TryGetValue(col.Name, out buf);
                    (filterrow.Presenter[col].Content as C1TextBoxBase).C1Text = buf;
                }

            }
        }

        private void CLearFilterRows()
        {
            try
            {
                var filterrow = this.testListView.TopRows[0];
                foreach (var col in testListView.Columns)
                {

                    //(filterrow.Presenter[col].Content as C1TextBoxBase).Clear();
                    (filterrow.Presenter[col].Content as C1TextBoxBase).C1Text = "";

                }
            }
            catch (Exception)
            {


            }

        }


        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {


            RestoreFilter();



        }

        private void btnUndoFilter_Click(object sender, RoutedEventArgs e)
        {
            CLearFilterRows();
            c1Filter.ResetFilter(this.testListView, (bool)this.chkIncludePlatzhalter.IsChecked);
        }

        private void ReadColumns(C1.WPF.DataGrid.C1DataGrid Grid)
        {

            System.Collections.Generic.Dictionary<string, string> Filter = new System.Collections.Generic.Dictionary<string, string>();
            Filter.Clear();

            //C1.WPF.DataGrid.DataGridColumn[] dc = Grid.FilteredColumns;
            //foreach (var d in dc)
            //{
            //    var fs = d.FilterState;
            //    Filter.Add(d.Name, fs.Tag.ToString());

            //}

            var filterrow = Grid.TopRows[0];
            foreach (var col in Grid.Columns)
            {
                if (filterrow.Presenter != null)
                {
                    if ((filterrow.Presenter[col].Content as C1TextBoxBase).C1Text != "")
                    {
                        Filter.Add(col.Name, (filterrow.Presenter[col].Content as C1TextBoxBase).C1Text);

                    }
                }
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item in Filter)
            {
                sb.Append(item.Key);
                sb.Append(" - ");
                sb.AppendLine(item.Value);

            }

            Console.WriteLine(sb.ToString());
            MessageBox.Show(sb.ToString());

        }

        private void btnReadColumn_Click(object sender, RoutedEventArgs e)
        {
            ReadColumns(this.testListView);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            svProjektliste.Height = e.NewSize.Height - 50;
            testListView.Height = e.NewSize.Height - 200;
        }

        public void Resize(double Height)
        {
            svProjektliste.Height = Height - 50;
            testListView.Height = Height - 200;
        }

        private void testListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (ProjekteView.CurrentItem.GetType().Equals(new DAL.kalkulationstabelle().GetType()))
                {
                    var item = (kalkulationstabelle)this.ProjekteView.CurrentItem;
                    int count = db.projekte.Where(p => p.projektnummer == item.projektnummer).Count();
                    if (count > 0)
                    {
                        if (count > 1)
                        {
                            MessageBox.Show(String.Format("Es wurden {0} Projekte mite der Projektnummer {1} gefunden", count, item.projektnummer), "Hinweis");
                        }

                        var query = from p in db.projekte
                                    where p.projektnummer == item.projektnummer
                                    select p.id;
                        int id = query.First();
                        var pro = new MainWindow(id);
                        NavigationService.Navigate(pro);

                    }
                    else
                    {
                        MessageBox.Show(String.Format("Das Projekt {0} existiert nicht", item.projektnummer));
                    }

                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));

            }









        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListenTyp == ListeTypeEnum.Artikel)
            {
                var p = new Reports.LagerListenViewer();
                p.Show();
            }
            else
            {
                MessageBox.Show("Für diese Liste ist keine Druckansicht implementiert");
            }

        }

        private void UpdateDateSelection(object sender, SelectionChangedEventArgs e)
        {


            //if (int.Parse(cboJahrVon.SelectedValue.ToString()) != 0 && cboJahrBis.SelectedValue != null) 
            //{

            //    FillList();
            //}

        }

        private void testListView_FilterChanged(object sender, C1.WPF.DataGrid.DataGridFilterChangedEventArgs e)
        {
            //IEnumerable<person> x = testListView.ItemsSource.OfType<person>();
            //Console.WriteLine(x.ToString());
        }

        private void Image_ImageFailed_1(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void btnSpecialAction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void testListView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            //  CM.IsOpen = true;
            //if (e.ChangedButton == System.Windows.Input.MouseButton.Right)
            //{

            //}

            //try
            //{
            //    if (ProjekteView.CurrentItem.GetType().Equals(new DAL.SI_Belege().GetType()))
            //    {
            //        var item = (SI_Belege)this.ProjekteView.CurrentItem;
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }




    }
}
