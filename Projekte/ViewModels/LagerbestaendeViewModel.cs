using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using DAL;
using System.Collections.ObjectModel;
using System.Windows;
using System.Diagnostics;
using AutoMapper;
using System.Windows.Threading;

namespace ProjektDB.ViewModels
{
    public class LagerbestaendeViewModel : PropertyChangedBase, IDisposable
    {

        #region "Declarations"
        SteinbachEntities db;
        StammLagerorte lagerort = null;
        Dispatcher ThisDispatcher = null;


        #endregion

        #region "Properties"
        public bool isDirty { get; set; }
        private Int32 _id;
        public Int32 id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyOfPropertyChange(() => id);
                    isDirty = true;
                }
            }
        }

        private String _Lagerortname;
        public String Lagerortname
        {
            get { return _Lagerortname; }
            set
            {
                if (value != _Lagerortname)
                {
                    _Lagerortname = value;
                    NotifyOfPropertyChange(() => Lagerortname);
                    isDirty = true;
                }
            }
        }

        private String _Lagerortkurzname;
        public String Lagerortkurzname
        {
            get { return _Lagerortkurzname; }
            set
            {
                if (value != _Lagerortkurzname)
                {
                    _Lagerortkurzname = value;
                    NotifyOfPropertyChange(() => Lagerortkurzname);
                    isDirty = true;
                }
            }
        }

        private String _Memo;
        public String Memo
        {
            get { return _Memo; }
            set
            {
                if (value != _Memo)
                {
                    _Memo = value;
                    NotifyOfPropertyChange(() => Memo);
                    isDirty = true;
                }
            }
        }

        private Int16? _istStandardLagerort;
        public Int16? istStandardLagerort
        {
            get { return _istStandardLagerort; }
            set
            {
                if (value != _istStandardLagerort)
                {
                    _istStandardLagerort = value;
                    NotifyOfPropertyChange(() => istStandardLagerort);
                    isDirty = true;
                }
            }
        }

        private Int16? _ShowInZiellager;
        public Int16? ShowInZiellager
        {
            get { return _ShowInZiellager; }
            set
            {
                if (value != _ShowInZiellager)
                {
                    _ShowInZiellager = value;
                    NotifyOfPropertyChange(() => ShowInZiellager);
                    isDirty = true;
                }
            }
        }

        private Int16? _aktiv;
        public Int16? aktiv
        {
            get { return _aktiv; }
            set
            {
                if (value != _aktiv)
                {
                    _aktiv = value;
                    NotifyOfPropertyChange(() => aktiv);
                    isDirty = true;
                }
            }
        }

        private Int16? _istSet;
        public Int16? istSet
        {
            get { return _istSet; }
            set
            {
                if (value != _istSet)
                {
                    _istSet = value;
                    NotifyOfPropertyChange(() => istSet);
                    isDirty = true;
                }
            }
        }

        #endregion


        #region "ObservableCollections"
        private ObservableCollection<Lagerbestaende> _Bestaende;
        public ObservableCollection<Lagerbestaende> Bestaende
        {
            get { return _Bestaende; }
            set
            {
                if (value != _Bestaende)
                {
                    _Bestaende = value;
                    NotifyOfPropertyChange(() => Bestaende);
                    isDirty = true;
                }
            }
        }

        private Lagerbestaende _SelectedBestaende;
        public Lagerbestaende SelectedBestaende
        {
            get { return _SelectedBestaende; }
            set
            {
                if (value != _SelectedBestaende)
                {
                    _SelectedBestaende = value;
                    NotifyOfPropertyChange(() => SelectedBestaende);
                    isDirty = true;
                }
            }
        }



        #endregion


        # region "Constructors"
        public LagerbestaendeViewModel(int id, Dispatcher dispatcher)
        {
            db = new SteinbachEntities();
            ThisDispatcher = dispatcher;
            InitModel(id);



        }



        public LagerbestaendeViewModel()
        {

        }


        #endregion



        #region "Commands"
        public void btnSaveA()
        {
            DoSaveChanges();

        }




        #endregion


        #region "Functions"

        public void InitModel(int id)
        {

            System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
            w.Start();


            db = new SteinbachEntities();
            if (id == 0)
            {
                lagerort = new StammLagerorte();
                lagerort.aktiv = 1;
                lagerort.istStandardLagerort = 0;
                lagerort.istSet = 1;
                lagerort.Lagerortname = "Neu : " + DateTime.Now;
                db.AddToStammLagerorte(lagerort);
                if (Bestaende == null)
                {
                    Bestaende = new ObservableCollection<Lagerbestaende>();
                }

            }
            else
            {

                lagerort = db.StammLagerorte.Where(n => n.id == id).SingleOrDefault();
                var task1 = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {

                    ThisDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (new System.Action(() =>
                    {


                        Bestaende = new ObservableCollection<Lagerbestaende>(db.Lagerbestaende.Where(n => n.id_Lagerort == id));

                        isDirty = false;

                    })));



                });



            }

            Trace.WriteLine("InitBeleg : " + w.ElapsedMilliseconds);


            Mapper.CreateMap<StammLagerorte, LagerbestaendeViewModel>();
            Mapper.Map<StammLagerorte, LagerbestaendeViewModel>(lagerort, this);
            isDirty = false;


            Trace.WriteLine("Nach Automapper : " + w.ElapsedMilliseconds);

        }

        public bool DoSaveChanges()
        {
            Mapper.CreateMap<LagerbestaendeViewModel, StammLagerorte>();
            Mapper.Map<LagerbestaendeViewModel, StammLagerorte>(this, lagerort);


            foreach (var item in Bestaende)
            {
                if (item.newItem)
                {
                    db.AddToLagerbestaende(item);
                }


            }

            db.SaveChanges();
            isDirty = false;
            return true;
        }

        public void btnAddPosition(Window w)
        {
            if (Bestaende == null)
            {
                Bestaende = new ObservableCollection<Lagerbestaende>();
            }

            var pos = new Lagerbestaende();
            pos.StammLagerorte = lagerort;
            pos.Lagerbestand = 0;
            pos.Sollbestand = 1;
            pos.Mindestbestand = 0;
            pos.newItem = true;
            db.AddToLagerbestaende(pos);
            Bestaende.Add(pos);

            //view.SI_BelegePositionen_ListView.SelectedIndex = view.SI_BelegePositionen_ListView.Items.Count - 1;
            var view = (views.EditLagerorte)w;
            view.LagerorteBestaende.ScrollIntoView(pos);


        }


        public void DeletePosition(views.EditLagerorte window)
        {
            var grid = window.LagerorteBestaende;
            var pos = (Lagerbestaende)grid.SelectedItem;

            if (pos != null)
            {
                if (pos.Lagerbestand.HasValue && pos.Lagerbestand > 0)
                {
                    MessageBox.Show("Positionen mit Lagerbestand können nicht gelöscht werden.");
                }
                else
                {
                    if (MessageBox.Show(string.Format("Position {0} wirklich löschen?", pos.Artikelnummer), "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {


                        try
                        {
                            db.DeleteObject(pos);
                        }
                        catch (Exception)
                        {
                        }


                        Bestaende.Remove(pos);

                    }
                }
                

            }


        }

        public void btnClose(views.EditLagerorte window)
        {
            window.Close();
        }


        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }
        #endregion



        #region "Events"
        public void CancelClosing(System.ComponentModel.CancelEventArgs e)
        {

            if (isDirty)
            {

                var res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel, System.Windows.MessageBoxImage.Warning);
                if (res == System.Windows.MessageBoxResult.Yes)
                {

                    e.Cancel = !DoSaveChanges();

                }

                else if (res == MessageBoxResult.No)
                {
                    e.Cancel = false;
                }

                else if (res == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }

            }
            else
            {

                e.Cancel = false;
            }

        }



        #endregion


    }
}
