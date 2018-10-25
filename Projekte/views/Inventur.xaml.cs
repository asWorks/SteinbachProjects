using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DAL;
using ProjektDB.ObjectCollections;
using ProjektDB.Repositories;
using System.Collections.Generic;
using CommonTools.Tools;
using System.Collections.ObjectModel;




namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for Kalkulation.xaml
    /// </summary>
    public partial class Inventur : Window
    {


        ViewModels.SI_InventurenViewModel vModel;
        //private ListCollectionView InventurView;
        //private CollectionViewSource InventurViewSource;
        //private CollectionViewSource InventurDetailViewSource;
        //private BindingListCollectionView InventurDetailView;
        //int CurrentInventurID = 0;
        //string ProjektNummer = string.Empty;
        //private ProjektRepository pRepo;
        //private bool bRowEditing = false;
        //private bool bSetArtikel = false;
        //private bool bUpdatePrice = false;
        //List<string> si = new List<string>();
        //private IEnumerable<string> source;


        //kalkulationstabelle_detail CurrDetails = null;




        //private SteinbachEntities db;


        public Inventur()
        {
            //InitializeComponent();
            //db = new SteinbachEntities();
            //FillList();


        }

        public Inventur(int InvID)
        {
            InitializeComponent();
            //CurrentInventurID = InvID;
            //db = new SteinbachEntities();

            vModel = new ViewModels.SI_InventurenViewModel(InvID);
            Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);
            c1GridDetais.FilterChanged += new EventHandler<C1.WPF.DataGrid.DataGridFilterChangedEventArgs>(c1GridDetais_FilterChanged);
            

            //if (InvID == 0)
            //{
            //    var vModel = new SI_Inventuren(db);
            //   // this.DataContext = vModel;
            //    Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);
            //    db.AddToSI_Inventuren(vModel);
            //    vModel.Notiz = "neu ...";
            //    vModel.Inventurdatum = DateTime.Now;
            //    vModel.Inventurbeendet = 0;
            //    vModel.Inventurgebucht = 0;

            //    vModel.GeneratePositions();

            //}
            //else
            //{

            //    var vModel = db.SI_Inventuren.Where(n=>n.id==InvID).SingleOrDefault();
            //    //vModel.LoadPositions(db);
            //   // this.DataContext = vModel;
            //    Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);
            //    vModel.refresh();
                
            //}

          
        }

        void c1GridDetais_FilterChanged(object sender, C1.WPF.DataGrid.DataGridFilterChangedEventArgs e)
        {
            CommonTools.Tools.C1DataGridFilterSortValue.FilterState = e.NewFilteredColumns;
           
        }


        private void c1GridDetais_SortChanged(object sender, C1.WPF.DataGrid.DataGridSortChangedEventArgs e)
        {
            CommonTools.Tools.C1DataGridFilterSortValue.SortState = e.NewSortedColumns;
            //string buf = "";
            //var x = e.NewSortedColumns;
            //foreach (var item in x)
            //{
            //    buf += item.Column.Name + " : ";
            //    buf += item.Value + " - ";

            //}
            //MessageBox.Show(buf);
        }

     
        private void FillList()
        {

            ////db = new SteinbachEntities();
            //pRepo = new ProjektRepository(this.db);
            //db.SavingChanges += new EventHandler(db_SavingChanges);
            //InventurViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Inventur_ViewSource")));
            //InventurDetailViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("InventurDetail_ViewSource")));


            //InventurViewSource.Source = new ObservableCollection<SI_Inventuren>(db.SI_Inventuren.Where(id => id.id == CurrentInventurID));
            //InventurView = (ListCollectionView)InventurViewSource.View;
            //InventurDetailView = (BindingListCollectionView)InventurDetailViewSource.View;

            //if (CurrentInventurID == 0)
            //{
            //    var inv = (SI_Inventuren)(this.InventurView.AddNew());
            //    inv.Inventurdatum = DateTime.Now;

                
                
            //}


           
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

          

        }

        void db_SavingChanges(object sender, EventArgs e)
        {
            //var sb = new StringBuilder();
            //var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted | System.Data.EntityState.Unchanged);



            //foreach (var o in om)
            //{

            //    if (o.Entity is kalkulationstabelle)
            //    {

            //        var p = (kalkulationstabelle)o.Entity;
            //        if (p.EntityState != System.Data.EntityState.Deleted)
            //        {
            //            p.created = DateTime.Now;
            //            this.CurrentInventurID = p.id;
            //        }
            //    }

            //}
        }

    

        private void Save_Click(object sender, RoutedEventArgs e)
        {

          //  db.SaveChanges();

            //var sb = new StringBuilder();
            //var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);



            //foreach (var o in om)
            //{

            //    if (o.Entity is kalkulationstabelle)
            //    {

            //        var p = (kalkulationstabelle)o.Entity;
            //        if (p.EntityState != System.Data.EntityState.Deleted)
            //        {
            //            p.created = DateTime.Now;
            //            this.CurrentInventurID = p.id;
            //        }
            //    }

            //}


            //SaveChanges();

        }



    

    


    

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

    //        AddKalulationDetail();

        }

        private void DeleteDetails()
        {

         

        }

        private void cmdDeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            //DeleteDetails();
        }

        private void Details_GotFocus(object sender, EventArgs e)
        {
            //var item = (ListViewItem)sender;
            //this.c1GridDetais.SelectedItem = item.DataContext;


        }

        private void btnCopyProjekt_Click(object sender, RoutedEventArgs e)
        {

        }


     



        private void CopyToReport_Click(object sender, RoutedEventArgs e)
        {
            if (txtID.Text != string.Empty)
            {
                var report = new Reports.CalcRepViewer(int.Parse(txtID.Text));
                report.ShowDialog();

            }

        }

        private void DataGridTextColumn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //MessageBox.Show("PropertyChanged");
        }

        private void c1GridDetais_BeginningNewRow(object sender, C1.WPF.DataGrid.DataGridBeginningNewRowEventArgs e)
        {
            //bRowEditing = true;
        }

        private void c1GridDetais_BeginningRowEdit(object sender, C1.WPF.DataGrid.DataGridEditingRowEventArgs e)
        {
            // MessageBox.Show("BeginningRowEdit");
            //bRowEditing = true;

        }


     



        private void c1GridDetais_CommittingNewRow(object sender, C1.WPF.DataGrid.DataGridEndingNewRowEventArgs e)
        {
            //bRowEditing = false;
        }



        private bool DoCancel()
        {
            //var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
            //if (om.Count() > 0)
            //{
            //    var res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            //    if (res == MessageBoxResult.Yes)
            //    {
            //        if (SaveChanges())
            //        {
            //            return true;
            //            //db.Dispose();
            //            //this.Close();

            //        }
            //        else
            //        {
            //            return false;
            //        }

            //    }

            //    else if (res == MessageBoxResult.No)
            //    {
            //        return true;
            //        //db.Dispose();
            //        //this.Close();

            //    }

            //    else if (res == MessageBoxResult.Cancel)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            //}


            //else
            //{

            //    return true;
            //}

            return false;
        }


        private bool SaveChanges()
        {
            //try
            //{
                
                
            //    var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added);
            //    foreach (var o in om)
            //    {

            //        if (o.Entity is kalkulationstabelle)
            //        {

            //            var p = (kalkulationstabelle)o.Entity;
            //            if (p.EntityState == System.Data.EntityState.Added || p.EntityState == System.Data.EntityState.Modified)
            //            {


            //                //if (this.txtProjektnummer.Text == string.Empty)
            //                //{
            //                //    MessageBox.Show("Es muss eine Projektnummer eingegeben werden");
            //                //    return false;
            //                //}
            //            }
            //        }


            //    }

            //    db.SaveChanges();
            //    //db.Dispose();
            //    return true;

            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));
            //    return false;
            //}

            return false;



        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
           
        //     e.Cancel = vModel.CancelClosing();
        //}

        private void FilteredComboBox_Extended_onfcbChanged(object sender, CommonTools.UserControls.FilteredComboBoxChangedEventArgs e)
        {

        }

        private void c1GridDetais_SelectionChanged(object sender, C1.WPF.DataGrid.DataGridSelectionChangedEventArgs e)
        {

        }

     

    }
}
