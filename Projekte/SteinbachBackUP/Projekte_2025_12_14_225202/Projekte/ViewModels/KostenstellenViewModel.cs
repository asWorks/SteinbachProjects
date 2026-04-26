using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using DAL;
using System.Collections.ObjectModel;
using System.Windows;

namespace ProjektDB.ViewModels
{
    public class KostenstellenViewModel : PropertyChangedBase
    {

        #region "Decalare"
        SteinbachEntities db;
        SI_KostenstellenFirmen kf = null;

        #endregion



        #region "Properties"
        private bool _isDirty;
        public bool isDirty
        {
            get { return _isDirty; }
            set
            {
                if (value != _isDirty)
                {
                    _isDirty = value;
                    NotifyOfPropertyChange(() => isDirty);
                    isDirty = true;
                }
            }
        }

        private SI_Kostenstellen _CurrentKS;
        public SI_Kostenstellen CurrentKS
        {
            get { return _CurrentKS; }
            set
            {
                if (value != _CurrentKS)
                {
                    _CurrentKS = value;
                    NotifyOfPropertyChange(() => CurrentKS);
                    isDirty = true;
                }
            }
        }



        #endregion


        #region "Constructors"

        public KostenstellenViewModel()
        {

            db = new SteinbachEntities();
            LoadData(0);
            Firmen = new ObservableCollection<firma>(db.firmen.Where(n => n.istFirma == 1|| n.istFirma == 11 || n.istFirma==10));


        }

        #endregion

        #region "Collections"

        private ObservableCollection<SI_Kostenstellen> _Kostenstellen;
        public ObservableCollection<SI_Kostenstellen> Kostenstellen
        {
            get { return _Kostenstellen; }
            set
            {
                if (value != _Kostenstellen)
                {
                    _Kostenstellen = value;
                    NotifyOfPropertyChange(() => Kostenstellen);
                    isDirty = true;
                }
            }
        }

        private SI_Kostenstellen _SelectedKostenstellen;
        public SI_Kostenstellen SelectedKostenstellen
        {
            get { return _SelectedKostenstellen; }
            set
            {
                if (value != _SelectedKostenstellen)
                {
                    _SelectedKostenstellen = value;
                    NotifyOfPropertyChange(() => SelectedKostenstellen);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<SI_KostenstellenFirmen> _KostenstellenFirmen;
        public ObservableCollection<SI_KostenstellenFirmen> KostenstellenFirmen
        {
            get { return _KostenstellenFirmen; }
            set
            {
                if (value != _KostenstellenFirmen)
                {
                    _KostenstellenFirmen = value;
                    NotifyOfPropertyChange(() => KostenstellenFirmen);
                    isDirty = true;
                }
            }
        }

        private SI_KostenstellenFirmen _SelectedKostenstellenFirmen;
        public SI_KostenstellenFirmen SelectedKostenstellenFirmen
        {
            get { return _SelectedKostenstellenFirmen; }
            set
            {
                if (value != _SelectedKostenstellenFirmen)
                {
                    _SelectedKostenstellenFirmen = value;
                    if (value != null)
                    {
                        kf = db.SI_KostenstellenFirmen.Where(n => n.id == value.id).SingleOrDefault();

                        LoadData(value.id);
                    }
                    NotifyOfPropertyChange(() => SelectedKostenstellenFirmen);
                    isDirty = true;
                }
            }
        }


        private ObservableCollection<SI_KostenstellenGruppen> _KostenstellenGruppen;
        public ObservableCollection<SI_KostenstellenGruppen> KostenstellenGruppen
        {
            get { return _KostenstellenGruppen; }
            set
            {
                if (value != _KostenstellenGruppen)
                {
                    _KostenstellenGruppen = value;
                    NotifyOfPropertyChange(() => KostenstellenGruppen);
                    isDirty = true;
                }
            }
        }

        private SI_KostenstellenGruppen _SelectedKostenstellenGruppen;
        public SI_KostenstellenGruppen SelectedKostenstellenGruppen
        {
            get { return _SelectedKostenstellenGruppen; }
            set
            {
                if (value != _SelectedKostenstellenGruppen)
                {
                    _SelectedKostenstellenGruppen = value;

                    NotifyOfPropertyChange(() => SelectedKostenstellenGruppen);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<firma> _Firmen;
        public ObservableCollection<firma> Firmen
        {
            get { return _Firmen; }
            set
            {
                if (value != _Firmen)
                {
                    _Firmen = value;
                    NotifyOfPropertyChange(() => Firmen);
                    isDirty = true;
                }
            }
        }

        private firma _SelectedFirmen;
        public firma SelectedFirmen
        {
            get { return _SelectedFirmen; }
            set
            {
                if (value != _SelectedFirmen)
                {
                    _SelectedFirmen = value;


                    NotifyOfPropertyChange(() => SelectedFirmen);
                    isDirty = true;
                }
            }
        }





        #endregion


        #region "Functions"

        public void SetKostenstelle(SI_KostenstellenGruppen value)
        {
            if (kf != null && CurrentKS != null)
            {

                CurrentKS.id = kf.id * 10 + value.id;
                CurrentKS.Bezeichnung = kf.Bezeichnung + " " + value.Bezeichnung;
            }

        }

        private void LoadData(int id)
        {
            KostenstellenFirmen = new ObservableCollection<SI_KostenstellenFirmen>(db.SI_KostenstellenFirmen);
            KostenstellenGruppen = new ObservableCollection<SI_KostenstellenGruppen>(db.SI_KostenstellenGruppen);
            if (id == 0)
            {
                // Kostenstellen = new ObservableCollection<SI_Kostenstellen>(db.SI_Kostenstellen);
            }
            else
            {
                Kostenstellen = new ObservableCollection<SI_Kostenstellen>(db.SI_Kostenstellen.Where(n => n.id_KostenstellenFirmen == id));
            }


        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void AddKostenstellenFirmen()
        {
            var pers = new SI_KostenstellenFirmen();
            db.AddToSI_KostenstellenFirmen(pers);
            KostenstellenFirmen.Add(pers);

        }

        public void AddKostenstellenGruppen()
        {
            var pers = new SI_KostenstellenGruppen();
            db.AddToSI_KostenstellenGruppen(pers);
            KostenstellenGruppen.Add(pers);

        }

        public void AddKostenstellen()
        {
            if (Kostenstellen==null)
            {
                throw new NullReferenceException("Kostenstelle darf nicht Null sein");
            }

            var pers = new SI_Kostenstellen();
            if (kf != null)
            {
                pers.id_KostenstellenFirmen = kf.id;
            }

            db.AddToSI_Kostenstellen(pers);
            Kostenstellen.Add(pers);
            CurrentKS = pers;

        }


        public bool CheckForChanges()
        {
            var osm = db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);

            Console.WriteLine(osm.Count().ToString());

            foreach (var ose in osm)
            {
                //var res = ose.GetModifiedProperties();
                //foreach (var item in res)
                //{
                //    Console.WriteLine(item);
                //}

                if (ose.State != System.Data.EntityState.Unchanged)
                {
                    return true;
                }
            }
            return false;


        }


        #endregion


        #region "Events"
        public void CancelClosing(System.ComponentModel.CancelEventArgs e)
        {

            if (CheckForChanges())
            {
                MessageBoxResult res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel);
                switch (res)
                {
                    case MessageBoxResult.Cancel:
                        {
                            e.Cancel = true;
                            break;
                        }
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Yes:
                        {
                            db.SaveChanges();
                            break;
                        }

                    default:
                        break;
                }
            }
        }

        public void DeletePositionKostenStellenFirmen(views.KostenstellenView window)
        {

            try
            {
                var grid = window.KostenstellenFirmenGrid;
                var pos = (SI_KostenstellenFirmen)grid.SelectedItem;
                if (pos != null)
                {
                    if (MessageBox.Show(string.Format("Position {0} wirklich löschen?", pos.Bezeichnung), "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            if (pos.SI_Kostenstellen.Count() > 0)
                            {
                                foreach (var item in pos.SI_Kostenstellen.ToList())
                                {
                                    db.DeleteObject(item);
                                    Kostenstellen.Remove(item);
                                }
                            }

                            db.DeleteObject(pos);
                        }
                        catch (Exception)
                        {
                        }


                        KostenstellenFirmen.Remove(pos);
                    }



                }
            }
            catch (Exception ex )
            {
                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Löschen von Kostenstellen Firmen");
               
            }

        }

        public void DeletePositionKostenStellenGruppen(views.KostenstellenView window)
        {

            try
            {
                var grid = window.KostenStellenGruppenGrid;
                var pos = (SI_KostenstellenGruppen)grid.SelectedItem;
                if (pos != null)
                {
                    if (MessageBox.Show(string.Format("Position {0} wirklich löschen?", pos.Bezeichnung), "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                          
                            db.DeleteObject(pos);
                            KostenstellenGruppen.Remove(pos);

                        }
                        catch (Exception)
                        {
                        }
                     
                    }

                }
            }
            catch (Exception ex)
            {
                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Löschen von Kostenstellen Firmen");

            }

        }

        public void DeletePositionKostenStellen(views.KostenstellenView window)
        {

            try
            {
                var grid = window.KostenstellenGrid;
                var pos = (SI_Kostenstellen)grid.SelectedItem;
                if (pos != null)
                {
                    if (MessageBox.Show(string.Format("Position {0} wirklich löschen?", pos.Bezeichnung), "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {

                            db.DeleteObject(pos);
                            Kostenstellen.Remove(pos);

                        }
                        catch (Exception)
                        {
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Löschen von Kostenstellen Firmen");

            }

        }

        #endregion


    }
}
