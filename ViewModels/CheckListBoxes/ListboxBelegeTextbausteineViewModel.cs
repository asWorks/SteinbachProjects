using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using DAL;
using System.Windows;
using System.Windows.Controls;


namespace ProjektDB.ViewModels.CheckListBoxes
{

    public class ListboxBelegeTextbausteineViewModel : INotifyPropertyChanged
    {
        SteinbachEntities db;
        ObservableCollection<SI_BelegartenTextbausteine> _selectedNames = new ObservableCollection<SI_BelegartenTextbausteine>();
        ObservableCollection<SI_BelegartenTextbausteine> fk;
        ViewModels.SI_BelegeViewModel BelegeViewModel;

        public event Action<bool> DataChanged;

        private void OnDataChanged(bool arg)
        {
            if (DataChanged != null)
            {
                DataChanged(arg);
            }

        }



        //   List<SI_BelegartenTextbausteine> _AvailableNames;

        public ListboxBelegeTextbausteineViewModel(SteinbachEntities db, ViewModels.SI_BelegeViewModel ViewModel)
        {
            _selectedNames.CollectionChanged += (sender, e) => UpdateCollection(sender, e);
            this.db = db;
            // AvailableNames = db.SI_BelegartenTextbausteine.Where(n => n.id_Belegart == ViewModel.Belegart).ToList();
            BelegeViewModel = ViewModel;
            UpdateAvailableNames();

        }

        void UpdateCollection(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                switch (e.Action)
                {

                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        {
                            if (e.NewItems != null && e.NewItems.Count > 0)
                            {
                                foreach (SI_BelegartenTextbausteine item in e.NewItems)
                                {
                                    if (fk.Where(k => k.id_Textbaustein == item.id).Count() == 0)
                                    {
                                        var k = new SI_BelegartenTextbausteine();
                                        // k.id_Belegart = BelegeViewModel.Belegart;
                                        k.id_Belegart = BelegeViewModel.SelectedBelegarten.id;

                                        k.id_Textbaustein = item.id_Textbaustein;
                                        k.id_Sprache = BelegeViewModel.id_Sprache;
                                        k.StammTextbausteine = item.StammTextbausteine;

                                        // BelegeViewModel.Belegtext += item.StammTextbausteine.Text;



                                        fk.Add(k);


                                        //db.AddToSI_BelegeTextbausteine(k);
                                        // db.SaveChanges();
                                        OnDataChanged(true);
                                    }

                                    SetBelegtext();

                                }
                            }

                            // SetBelegtext();

                            break;
                        }

                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        {
                            if (e.OldItems != null && e.OldItems.Count > 0)
                            {
                                foreach (SI_BelegartenTextbausteine item in e.OldItems)
                                {

                                    BelegeViewModel.Belegtext = BelegeViewModel.Belegtext.Replace(item.StammTextbausteine.Text, "");

                                    if (fk.Where(k => k.id_Textbaustein == item.id).Count() == 1)
                                    {


                                        var k = fk.Where(ka => ka.id_Textbaustein == item.id).SingleOrDefault();
                                        fk.Remove(k);

                                        // db.DeleteObject(k);
                                        //db.SaveChanges();
                                        OnDataChanged(true);
                                    }
                                }
                            }

                             SetBelegtext();

                            break;
                        }


                    default:
                        {
                            break;
                        }

                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Einfügen von Textbaustein");
            }



        }

        private void SetBelegtext()
        {
            if (BelegeViewModel != null)
            {
                

                if (SelectedNames != null)
                {
                      BelegeViewModel.Belegtext = string.Empty;

                      var sn = SelectedNames.OrderBy(n => n.index);
                     foreach (var item1 in sn)
                    {
                        if (item1.StammTextbausteine != null)
                        {
                            BelegeViewModel.Belegtext += item1.StammTextbausteine.Text;
                        }

                    }
                }


                //if (fk != null)
                //{
                   
                //}
            }
        }


        public void UpdateAvailableNames()
        {

            //AvailableNames = db.SI_BelegartenTextbausteine.Where(n => n.id_Belegart == BelegeViewModel.Belegart && n.id_Sprache == BelegeViewModel.id_Sprache).OrderBy(n=>n.index).ToList();
            if (BelegeViewModel.SelectedBelegarten != null)
            {
                AvailableNames = db.SI_BelegartenTextbausteine.Where(n => n.id_Belegart == BelegeViewModel.SelectedBelegarten.id && n.id_Sprache == BelegeViewModel.id_Sprache).OrderBy(n => n.index).ToList();
            }
            



        }


        public void AddSelectedNames()
        {
            //fk = new ObservableCollection<SI_BelegartenTextbausteine>(db.SI_BelegartenTextbausteine.Where(n => n.id_Belegart == BelegeViewModel.Belegart && n.Mandantory == 1 && n.id_Sprache == BelegeViewModel.id_Sprache).OrderBy(n=>n.index));

            if (BelegeViewModel.SelectedBelegarten != null)
            {
                fk = new ObservableCollection<SI_BelegartenTextbausteine>(db.SI_BelegartenTextbausteine.Where(n => n.id_Belegart == BelegeViewModel.SelectedBelegarten.id && n.Mandantory == 1 && n.id_Sprache == BelegeViewModel.id_Sprache).OrderBy(n => n.index));

                if (fk.Count > 0)
                {
                    SelectedNames.Clear();
                    foreach (var item in fk.ToList())
                    {

                        SelectedNames.Add(item);
                    }
                    OnPropertyChanged("SelectedNames");
                }
            }

        }



        //public IEnumerable<SI_BelegartenTextbausteine> AvailableNames
        //{
        //    get
        //    {

        //        return _AvailableNames;
        //    }
        //}


        private IEnumerable<SI_BelegartenTextbausteine> _AvailableNames;
        public IEnumerable<SI_BelegartenTextbausteine> AvailableNames
        {
            get { return _AvailableNames; }
            set
            {
                if (value != _AvailableNames)
                {
                    _AvailableNames = value;
                    OnPropertyChanged("AvailableNames");

                }
            }
        }




        public ObservableCollection<SI_BelegartenTextbausteine> SelectedNames
        {
            get
            {
                return _selectedNames;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public void isChecked(FrameworkElement e)
        {
            var x = (CheckBox)e;
            Console.WriteLine(x.ToString());
        }

        public void isUnChecked()
        {
        }

    }


}
