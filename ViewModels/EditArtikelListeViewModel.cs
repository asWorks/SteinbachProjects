using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using DAL;
using System.Collections.ObjectModel;
using System.Windows;
using ProjektDB.Models;
using System.Windows.Controls;
using DAL.Tools;
using System.Diagnostics;

namespace ProjektDB.ViewModels
{
    public class EditArtikelListeViewModel : Screen
    {

        #region "Declarations"

        SteinbachEntities db;

        #endregion

        #region "UI"
        private bool _isVisible;
        public bool isVisible
        {
            get { return _isVisible; }
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
                    NotifyOfPropertyChange(() => isVisible);
                    isDirty = true;
                }
            }
        }

        private CheckListBoxColumns _clbColumns;
        public CheckListBoxColumns clbColumns
        {
            get { return _clbColumns; }
            set
            {
                if (value != _clbColumns)
                {
                    _clbColumns = value;
                    NotifyOfPropertyChange(() => clbColumns);
                    isDirty = true;
                }
            }
        }


        private bool _bExportExcel;
        public bool bExportExcel
        {
            get { return _bExportExcel; }
            set
            {
                if (value != _bExportExcel)
                {
                    _bExportExcel = value;
                    NotifyOfPropertyChange(() => bExportExcel);
                    isDirty = true;
                }
            }
        }

        //private ObservableCollection<ArtikellistColumn> _ListColumns;
        //public ObservableCollection<ArtikellistColumn> ListColumns
        //{
        //    get { return _ListColumns; }
        //    set
        //    {
        //        if (value != _ListColumns)
        //        {
        //            _ListColumns = value;
        //            NotifyOfPropertyChange(() => ListColumns);
        //            isDirty = true;
        //        }
        //    }
        //}

        //private ArtikellistColumn _SelectedListColumns;
        //public ArtikellistColumn SelectedListColumns
        //{
        //    get { return _SelectedListColumns; }
        //    set
        //    {
        //        if (value != _SelectedListColumns)
        //        {
        //            _SelectedListColumns = value;
        //            NotifyOfPropertyChange(() => SelectedListColumns);
        //            isDirty = true;
        //        }
        //    }
        //}


        #endregion

        #region "ObservableCollections"

        private ObservableCollection<lagerliste> _Artikelliste;
        public ObservableCollection<lagerliste> Artikelliste
        {
            get { return _Artikelliste; }
            set
            {
                if (value != _Artikelliste)
                {
                    _Artikelliste = value;
                    NotifyOfPropertyChange(() => Artikelliste);
                    isDirty = true;
                }
            }
        }

        private lagerliste _SelectedArtikelliste;
        public lagerliste SelectedArtikelliste
        {
            get { return _SelectedArtikelliste; }
            set
            {
                if (value != _SelectedArtikelliste)
                {
                    _SelectedArtikelliste = value;
                    NotifyOfPropertyChange(() => SelectedArtikelliste);
                    isDirty = true;
                }
            }
        }



        private ObservableCollection<firma> _Lieferanten;
        public ObservableCollection<firma> Lieferanten
        {
            get { return _Lieferanten; }
            set
            {
                if (value != _Lieferanten)
                {
                    _Lieferanten = value;
                    NotifyOfPropertyChange(() => Lieferanten);
                    isDirty = true;
                }
            }
        }

        private firma _SelectedLieferanten;
        public firma SelectedLieferanten
        {
            get { return _SelectedLieferanten; }
            set
            {
                if (value != _SelectedLieferanten)
                {
                    _SelectedLieferanten = value;
                    NotifyOfPropertyChange(() => SelectedLieferanten);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<StammWaehrungen> _WKZ;
        public ObservableCollection<StammWaehrungen> WKZ
        {
            get { return _WKZ; }
            set
            {
                if (value != _WKZ)
                {
                    _WKZ = value;
                    NotifyOfPropertyChange(() => WKZ);
                    isDirty = true;
                }
            }
        }

        private StammWaehrungen _SelectedWKZ;
        public StammWaehrungen SelectedWKZ
        {
            get { return _SelectedWKZ; }
            set
            {
                if (value != _SelectedWKZ)
                {
                    _SelectedWKZ = value;
                    NotifyOfPropertyChange(() => SelectedWKZ);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<StammWaehrungen> _WKennzeichen;
        public ObservableCollection<StammWaehrungen> WKennzeichen
        {
            get { return _WKennzeichen; }
            set
            {
                if (value != _WKennzeichen)
                {
                    _WKennzeichen = value;
                    NotifyOfPropertyChange(() => WKennzeichen);
                    isDirty = true;
                }
            }
        }

        private StammWaehrungen _SelectedWKennzeichen;
        public StammWaehrungen SelectedWKennzeichen
        {
            get { return _SelectedWKennzeichen; }
            set
            {
                if (value != _SelectedWKennzeichen)
                {
                    _SelectedWKennzeichen = value;
                    NotifyOfPropertyChange(() => SelectedWKennzeichen);
                    isDirty = true;
                }
            }
        }



        private ObservableCollection<person> _Mitarbeiter;
        public ObservableCollection<person> Mitarbeiter
        {
            get { return _Mitarbeiter; }
            set
            {
                if (value != _Mitarbeiter)
                {
                    _Mitarbeiter = value;
                    NotifyOfPropertyChange(() => Mitarbeiter);
                    isDirty = true;
                }
            }
        }

        private person _SelectedMitarbeiter;
        public person SelectedMitarbeiter
        {
            get { return _SelectedMitarbeiter; }
            set
            {
                if (value != _SelectedMitarbeiter)
                {
                    _SelectedMitarbeiter = value;
                    NotifyOfPropertyChange(() => SelectedMitarbeiter);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<AuswahlEintraege> _Kategorien;
        public ObservableCollection<AuswahlEintraege> Kategorien
        {
            get { return _Kategorien; }
            set
            {
                if (value != _Kategorien)
                {
                    _Kategorien = value;
                    NotifyOfPropertyChange(() => Kategorien);
                    isDirty = true;
                }
            }
        }

        private AuswahlEintraege _SelectedKategorien;
        public AuswahlEintraege SelectedKategorien
        {
            get { return _SelectedKategorien; }
            set
            {
                if (value != _SelectedKategorien)
                {
                    _SelectedKategorien = value;
                    NotifyOfPropertyChange(() => SelectedKategorien);
                    isDirty = true;
                }
            }
        }




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

        private DateTime? _created;
        public DateTime? created
        {
            get { return _created; }
            set
            {
                if (value != _created)
                {
                    _created = value;
                    NotifyOfPropertyChange(() => created);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_parent;
        public Int32? id_parent
        {
            get { return _id_parent; }
            set
            {
                if (value != _id_parent)
                {
                    _id_parent = value;
                    NotifyOfPropertyChange(() => id_parent);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_lieferant;
        public Int32? id_lieferant
        {
            get { return _id_lieferant; }
            set
            {
                if (value != _id_lieferant)
                {
                    _id_lieferant = value;
                    NotifyOfPropertyChange(() => id_lieferant);
                    isDirty = true;
                }
            }
        }

        private Int32? _idx;
        public Int32? idx
        {
            get { return _idx; }
            set
            {
                if (value != _idx)
                {
                    _idx = value;
                    NotifyOfPropertyChange(() => idx);
                    isDirty = true;
                }
            }
        }

        private String _bezeichnung;
        public String bezeichnung
        {
            get { return _bezeichnung; }
            set
            {
                if (value != _bezeichnung)
                {
                    _bezeichnung = value;
                    NotifyOfPropertyChange(() => bezeichnung);
                    isDirty = true;
                }
            }
        }

        private String _artikelnr;
        public String artikelnr
        {
            get { return _artikelnr; }
            set
            {
                if (value != _artikelnr)
                {
                    _artikelnr = value;
                    NotifyOfPropertyChange(() => artikelnr);
                    isDirty = true;
                }
            }
        }

        private String _produktnr;
        public String produktnr
        {
            get { return _produktnr; }
            set
            {
                if (value != _produktnr)
                {
                    _produktnr = value;
                    NotifyOfPropertyChange(() => produktnr);
                    isDirty = true;
                }
            }
        }

        private String _beschreibung;
        public String beschreibung
        {
            get { return _beschreibung; }
            set
            {
                if (value != _beschreibung)
                {
                    _beschreibung = value;
                    NotifyOfPropertyChange(() => beschreibung);
                    isDirty = true;
                }
            }
        }

        private String _beschreibungeng;
        public String beschreibungeng
        {
            get { return _beschreibungeng; }
            set
            {
                if (value != _beschreibungeng)
                {
                    _beschreibungeng = value;
                    NotifyOfPropertyChange(() => beschreibungeng);
                    isDirty = true;
                }
            }
        }

        private Int32? _anzahlauflager;
        public Int32? anzahlauflager
        {
            get { return _anzahlauflager; }
            set
            {
                if (value != _anzahlauflager)
                {
                    _anzahlauflager = value;
                    NotifyOfPropertyChange(() => anzahlauflager);
                    isDirty = true;
                }
            }
        }

        private Int32? _anzahlmin;
        public Int32? anzahlmin
        {
            get { return _anzahlmin; }
            set
            {
                if (value != _anzahlmin)
                {
                    _anzahlmin = value;
                    NotifyOfPropertyChange(() => anzahlmin);
                    isDirty = true;
                }
            }
        }

        private Int32? _kommissionanzahl;
        public Int32? kommissionanzahl
        {
            get { return _kommissionanzahl; }
            set
            {
                if (value != _kommissionanzahl)
                {
                    _kommissionanzahl = value;
                    NotifyOfPropertyChange(() => kommissionanzahl);
                    isDirty = true;
                }
            }
        }

        private Int32? _kaufanzahl;
        public Int32? kaufanzahl
        {
            get { return _kaufanzahl; }
            set
            {
                if (value != _kaufanzahl)
                {
                    _kaufanzahl = value;
                    NotifyOfPropertyChange(() => kaufanzahl);
                    isDirty = true;
                }
            }
        }

        private Decimal? _preiseuro;
        public Decimal? preiseuro
        {
            get { return _preiseuro; }
            set
            {
                if (value != _preiseuro)
                {
                    _preiseuro = value;
                    NotifyOfPropertyChange(() => preiseuro);
                    isDirty = true;
                }
            }
        }

        private Decimal? _preisbrutto;
        public Decimal? preisbrutto
        {
            get { return _preisbrutto; }
            set
            {
                if (value != _preisbrutto)
                {
                    _preisbrutto = value;
                    NotifyOfPropertyChange(() => preisbrutto);
                    isDirty = true;
                }
            }
        }

        private Int16? _sonderpreis;
        public Int16? sonderpreis
        {
            get { return _sonderpreis; }
            set
            {
                if (value != _sonderpreis)
                {
                    _sonderpreis = value;
                    NotifyOfPropertyChange(() => sonderpreis);
                    isDirty = true;
                }
            }
        }

        private Double? _preisnok;
        public Double? preisnok
        {
            get { return _preisnok; }
            set
            {
                if (value != _preisnok)
                {
                    _preisnok = value;
                    NotifyOfPropertyChange(() => preisnok);
                    isDirty = true;
                }
            }
        }

        private Double? _euroumrechnung;
        public Double? euroumrechnung
        {
            get { return _euroumrechnung; }
            set
            {
                if (value != _euroumrechnung)
                {
                    _euroumrechnung = value;
                    NotifyOfPropertyChange(() => euroumrechnung);
                    isDirty = true;
                }
            }
        }

        private Double? _prozent;
        public Double? prozent
        {
            get { return _prozent; }
            set
            {
                if (value != _prozent)
                {
                    _prozent = value;
                    NotifyOfPropertyChange(() => prozent);
                    isDirty = true;
                }
            }
        }

        private DateTime? _preisvom;
        public DateTime? preisvom
        {
            get { return _preisvom; }
            set
            {
                if (value != _preisvom)
                {
                    _preisvom = value;
                    NotifyOfPropertyChange(() => preisvom);
                    isDirty = true;
                }
            }
        }

        private String _ortregal;
        public String ortregal
        {
            get { return _ortregal; }
            set
            {
                if (value != _ortregal)
                {
                    _ortregal = value;
                    NotifyOfPropertyChange(() => ortregal);
                    isDirty = true;
                }
            }
        }

        private String _ortbox;
        public String ortbox
        {
            get { return _ortbox; }
            set
            {
                if (value != _ortbox)
                {
                    _ortbox = value;
                    NotifyOfPropertyChange(() => ortbox);
                    isDirty = true;
                }
            }
        }

        private Double? _hinzugefuegtanzahl;
        public Double? hinzugefuegtanzahl
        {
            get { return _hinzugefuegtanzahl; }
            set
            {
                if (value != _hinzugefuegtanzahl)
                {
                    _hinzugefuegtanzahl = value;
                    NotifyOfPropertyChange(() => hinzugefuegtanzahl);
                    isDirty = true;
                }
            }
        }

        private DateTime? _hinzugefuegtdatum;
        public DateTime? hinzugefuegtdatum
        {
            get { return _hinzugefuegtdatum; }
            set
            {
                if (value != _hinzugefuegtdatum)
                {
                    _hinzugefuegtdatum = value;
                    NotifyOfPropertyChange(() => hinzugefuegtdatum);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_kategorie;
        public Int32? id_kategorie
        {
            get { return _id_kategorie; }
            set
            {
                if (value != _id_kategorie)
                {
                    _id_kategorie = value;
                    NotifyOfPropertyChange(() => id_kategorie);
                    isDirty = true;
                }
            }
        }

        private String _Bemerkung;
        public String Bemerkung
        {
            get { return _Bemerkung; }
            set
            {
                if (value != _Bemerkung)
                {
                    _Bemerkung = value;
                    NotifyOfPropertyChange(() => Bemerkung);
                    isDirty = true;
                }
            }
        }

        private String _Warentarifnummer;
        public String Warentarifnummer
        {
            get { return _Warentarifnummer; }
            set
            {
                if (value != _Warentarifnummer)
                {
                    _Warentarifnummer = value;
                    NotifyOfPropertyChange(() => Warentarifnummer);
                    isDirty = true;
                }
            }
        }

        private String _EANNummer;
        public String EANNummer
        {
            get { return _EANNummer; }
            set
            {
                if (value != _EANNummer)
                {
                    _EANNummer = value;
                    NotifyOfPropertyChange(() => EANNummer);
                    isDirty = true;
                }
            }
        }

        private String _wkz;
        public String wkz
        {
            get { return _wkz; }
            set
            {
                if (value != _wkz)
                {
                    _wkz = value;
                    NotifyOfPropertyChange(() => wkz);
                    isDirty = true;
                }
            }
        }

        private Int16? _PrintSelected;
        public Int16? PrintSelected
        {
            get { return _PrintSelected; }
            set
            {
                if (value != _PrintSelected)
                {
                    _PrintSelected = value;
                    NotifyOfPropertyChange(() => PrintSelected);
                    isDirty = true;
                }
            }
        }

        private Int32? _PrintCopies;
        public Int32? PrintCopies
        {
            get { return _PrintCopies; }
            set
            {
                if (value != _PrintCopies)
                {
                    _PrintCopies = value;
                    NotifyOfPropertyChange(() => PrintCopies);
                    isDirty = true;
                }
            }
        }

        private Int32? _PreisAenderungMitarbeiter;
        public Int32? PreisAenderungMitarbeiter
        {
            get { return _PreisAenderungMitarbeiter; }
            set
            {
                if (value != _PreisAenderungMitarbeiter)
                {
                    _PreisAenderungMitarbeiter = value;
                    NotifyOfPropertyChange(() => PreisAenderungMitarbeiter);
                    isDirty = true;
                }
            }
        }

        private Int16? _Handelsware;
        public Int16? Handelsware
        {
            get { return _Handelsware; }
            set
            {
                if (value != _Handelsware)
                {
                    _Handelsware = value;
                    NotifyOfPropertyChange(() => Handelsware);
                    isDirty = true;
                }
            }
        }

        #endregion


        #region "Constructors"

        public EditArtikelListeViewModel()
        {
            InitModel();
        }

        #endregion

        #region "Commands"
        public void btnSave()
        {
            DoSaveChanges();

        }

        public void btnAddArtikel()
        {

            Artikelliste = new ObservableCollection<lagerliste>(db.lagerlisten.Where(n => n.artikelnr == ""));
            var art = new lagerliste();

            db.AddTolagerlisten(art);
            Artikelliste.Add(art);
        }

        public void btnShowAll()
        {
            Artikelliste = new ObservableCollection<lagerliste>(db.lagerlisten);
        }
        #endregion

        #region "Functions"



        private void InitModel()
        {

            try
            {
                Trace.WriteLine("EditArtikelListeViewModel.cs - InitModel Start");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListeViewModel.cs - InitModel Start", DateTime.Now.ToLongTimeString());
                isVisible = false;
                db = new SteinbachEntities();

                Artikelliste = new ObservableCollection<lagerliste>(db.lagerlisten);
                Kategorien = new ObservableCollection<AuswahlEintraege>(db.AuswahlEintraege.Where(n => n.Gruppe == "ArtikelKategorie"));
                Lieferanten = new ObservableCollection<firma>(db.firmen.Where(f => f.istFirma == 1));
                WKZ = new ObservableCollection<StammWaehrungen>(db.StammWaehrungen);
                WKennzeichen = new ObservableCollection<StammWaehrungen>(db.StammWaehrungen);
                Mitarbeiter = new ObservableCollection<person>(db.personen);
                clbColumns = new CheckListBoxColumns(db.ZMetaArtikellistColumn);
                bExportExcel = false;
                Trace.WriteLine("EditArtikelListeViewModel.cs - InitModel done");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListeViewModel.cs - InitModel Stop", DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }


        }

        private void DoSaveChanges()
        {
            SavingChanges();
            db.SaveChanges();
        }

        void SavingChanges()
        {
            var om = this.db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);

            foreach (var o in om)
            {

                if (o.Entity is lagerliste)
                {

                    var p = (lagerliste)o.Entity;
                    if (p.EntityState != System.Data.EntityState.Deleted)
                    {
                        p.PreisAenderungMitarbeiter = Session.User.id;
                        p.preisvom = DateTime.Now;
                    }
                }


            }
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



        public void GetListeSource(Window w)
        {
            try
            {
                Trace.WriteLine("EditArtikelListeViewModel.cs - GetListeSource Start");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListeViewModel.cs - GetListeSource Start", DateTime.Now.ToLongTimeString());
                var x = (views.EditArtikelListe)w;
                var grid = x.c1DataGrid1;
                var dt = DateTime.Now;

                // IEnumerable<SI_InventurenPositionen> liste = grid.ItemsSource.OfType<SI_InventurenPositionen>();

                var buf = GetFilteredGridRows(grid);

                var x1 = new views.ViewerInventurliste(buf, dt, views.ViewerInventurliste.InventurlistenArt.Artikelliste, bExportExcel);
                x1.ShowDialog();
                Trace.WriteLine("EditArtikelListeViewModel.cs - GetListeSource Done");
                Trace.Flush();
                LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListeViewModel.cs - GetListeSource Stop", DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }


        }

        private static IEnumerable<lagerliste> GetFilteredGridRows(C1.WPF.DataGrid.C1DataGrid grid)
        {

            Trace.WriteLine("EditArtikelListeViewModel.cs - GetFilteredGridRows Start");
            Trace.Flush();
            LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListeViewModel.cs - GetFilteredGridRows Start", DateTime.Now.ToLongTimeString());
            try
            {

                var res = grid.Rows;
                var li = new List<lagerliste>();
                foreach (var item in res)
                {
                    if (item.DataItem != null)
                    {
                        li.Add((lagerliste)item.DataItem);
                    }

                }

                var buf = (IEnumerable<lagerliste>)li;
                Trace.WriteLine("EditArtikelListeViewModel.cs - GetFilteredGridRows done");
                Trace.Flush();
                    LoggingTool.addDatabaseLogging("", 0, 0, "EditArtikelListeViewModel.cs - GetFilteredGridRows Stop", DateTime.Now.ToLongTimeString());
                    
                return buf;
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
                return new List<lagerliste>();
            }

        }
        #endregion

        #region "Events"
        public void Window_Closing(System.ComponentModel.CancelEventArgs e)
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

        public void DeletingRows(C1.WPF.DataGrid.DataGridDeletingRowsEventArgs e)
        {
            if (e.DeletedRows.Count() > 0)
            {
                if (MessageBox.Show(string.Format("{0} Artikel wirklich löschen ?", e.DeletedRows.Count()), "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (var item in e.DeletedRows)
                    {
                        var data = (lagerliste)item.DataItem;

                        // Artikelliste.Remove(data);
                        db.DeleteObject(data);
                    }
                }
            }
        }

        public void DeletingDataRows(object button, Window w)
        {
            views.EditArtikelListe view = (views.EditArtikelListe)w;
            lagerliste res;

            //  Button btn = button as Button;
            // int index = (btn.Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;
            //  int index =  (view.c1DataGrid1 as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;
            //  int index = ((btn.Parent as StackPanel).Parent as C1.WPF.DataGrid.DataGridCellPresenter).Row.Index;

            try
            {
                res = (lagerliste)view.c1DataGrid1.SelectedItem;
                if (MessageBox.Show(string.Format(" Artikel {0} - {1} wirklich löschen ?", res.artikelnr, res.beschreibungeng), "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    foreach (var lb in res.Lagerbestaende.ToList())
                    {
                        db.DeleteObject(lb);
                    }
                    foreach (var ip in res.SI_InventurenPositionen.ToList())
                    {
                        db.DeleteObject(ip);
                    }

                    Artikelliste.Remove(res);
                    db.DeleteObject(res);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void EditArtikel(Window w)
        {

            views.EditArtikelListe view = (views.EditArtikelListe)w;
            lagerliste res;

            res = (lagerliste)view.c1DataGrid1.SelectedItem;

            var ea = new views.EditArtikel((int)res.id);
            ea.ShowDialog();

        }




    }
        #endregion


}
