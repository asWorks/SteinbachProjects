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
   
        public class ListboxSelectedBelegartenViewModel : INotifyPropertyChanged
        {
            SteinbachEntities db;
            ObservableCollection<StammBelegarten> _selectedNames = new ObservableCollection<StammBelegarten>();
            ObservableCollection<SI_BelegartenTextbausteine> fk;
            StammTextbaustein textbaustein;


            List<StammBelegarten> _AvailableNames;

            public ListboxSelectedBelegartenViewModel(SteinbachEntities db,StammTextbaustein tbs)
            {
                _selectedNames.CollectionChanged += (sender, e) => UpdateCollection(sender, e);
                this.db = db;
                _AvailableNames = db.StammBelegarten.ToList();
                textbaustein = tbs;

            }

            void UpdateCollection(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        {
                            if (e.NewItems != null && e.NewItems.Count > 0)
                            {
                                foreach (StammBelegarten item in e.NewItems)
                                {
                                    if (fk.Where(k => k.id_Belegart == item.id).Count() == 0)
                                    {
                                        var k = new SI_BelegartenTextbausteine();
                                        k.id_Belegart = item.id;
                                        k.id_Textbaustein = textbaustein.id;
                                        k.id_Sprache = textbaustein.id_Sprache;
                                       
                                        k.Mandantory = item.Mandantory == true ? (short)1 :(short)0;

                                        fk.Add(k);
                                        db.AddToSI_BelegartenTextbausteine(k);
                                        db.SaveChanges();

                                    }

                                }
                            }

                            break;
                        }

                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        {
                            if (e.OldItems != null && e.OldItems.Count > 0)
                            {
                                foreach (StammBelegarten item in e.OldItems)
                                {
                                    if (fk.Where(k => k.id_Belegart == item.id).Count() == 1)
                                    {
                                        var k = fk.Where(ka => ka.id_Belegart == item.id).SingleOrDefault();
                                        fk.Remove(k);
                                        db.DeleteObject(k);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            break;
                        }


                    default:
                        {
                            break;
                        }

                }

            }


            public void AddSelectedNames()
            {

                fk = new ObservableCollection<SI_BelegartenTextbausteine>(db.SI_BelegartenTextbausteine.Where(n=>n.id_Textbaustein == textbaustein.id));
                if (fk.Count > 0)
                {
                    SelectedNames.Clear();
                    foreach (var item in fk)
                    {
                        item.StammBelegarten.Mandantory = item.Mandantory == 1 ? true : false;
                        SelectedNames.Add(item.StammBelegarten);
                    }
                    OnPropertyChanged("SelectedNames");
                }

            }



            public IEnumerable<StammBelegarten> AvailableNames
            {
                get
                {

                    return _AvailableNames;
                }
            }




            public ObservableCollection<StammBelegarten> SelectedNames
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
                var x = (CheckBox) e;
                Console.WriteLine(x.ToString());
            }

        public void isUnChecked(FrameworkElement e)
        {
        }

        }
   

}
