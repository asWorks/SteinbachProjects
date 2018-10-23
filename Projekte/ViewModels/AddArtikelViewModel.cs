using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using DAL;


namespace ProjektDB.ViewModels
{
    public class AddArtikelViewModel : Screen
    {

        lagerliste result = null;
        public event System.Action<DTO.dtoAddArtikel> ListeUpdated;

        private decimal _Menge;
        public decimal Menge
        {
            get { return _Menge; }
            set
            {
                if (value != _Menge)
                {
                    _Menge = value;
                    NotifyOfPropertyChange(() => Menge);
                }
            }
        }






        #region "Collections"
        private ObservableCollection<lagerliste> _ArtikelLookoutFull;
        public ObservableCollection<lagerliste> ArtikelLookoutFull
        {
            get { return _ArtikelLookoutFull; }
            set
            {
                if (value != _ArtikelLookoutFull)
                {
                    _ArtikelLookoutFull = value;
                    NotifyOfPropertyChange(() => ArtikelLookoutFull);

                }
            }
        }

        private lagerliste _SelectedArtikelLookoutFull;
        public lagerliste SelectedArtikelLookoutFull
        {
            get { return _SelectedArtikelLookoutFull; }
            set
            {
                if (value != _SelectedArtikelLookoutFull)
                {
                    _SelectedArtikelLookoutFull = value;
                    if (value != null)
                    {


                        var da = FillDtoArtikel(value);



                        OnListeUpdated(da);
                    }

                    NotifyOfPropertyChange(() => SelectedArtikelLookoutFull);

                }
            }
        }

        private DTO.dtoAddArtikel FillDtoArtikel(lagerliste value)
        {
            var da = new DTO.dtoAddArtikel();
            da.id_Lagerliste = value.id;
            da.Artikelnummer = value.artikelnr == null ? "" : value.artikelnr;
            da.Bezeichnung = value.bezeichnung == null ? "" : value.bezeichnung;
            da.Beschreibung = value.beschreibung == null ? "" : value.beschreibung;
            da.einheit = value.einheit == null ? "" : value.einheit;

            da.Nettopreis = !value.preiseuro.HasValue ? 0m : (decimal)value.preiseuro;

            da.Bruttopreis = !value.preisbrutto.HasValue ? 0m : (decimal)value.preisbrutto;
            da.Menge = Menge == null ? 0m : Menge;
            da.id_Lieferant = value.id_lieferant;
            da.Handelsware = value.Handelsware.HasValue ? Convert.ToBoolean(value.Handelsware) : false; ;
            return da;
        }


        private ObservableCollection<lagerliste> _ArtikelLookoutFiltered;
        public ObservableCollection<lagerliste> ArtikelLookoutFiltered
        {
            get { return _ArtikelLookoutFiltered; }
            set
            {
                if (value != _ArtikelLookoutFiltered)
                {
                    _ArtikelLookoutFiltered = value;
                    NotifyOfPropertyChange(() => ArtikelLookoutFiltered);

                }
            }
        }

        private lagerliste _SelectedArtikelLookoutFiltered;
        public lagerliste SelectedArtikelLookoutFiltered
        {
            get { return _SelectedArtikelLookoutFiltered; }
            set
            {
                if (value != _SelectedArtikelLookoutFiltered)
                {
                    _SelectedArtikelLookoutFiltered = value;
                    NotifyOfPropertyChange(() => SelectedArtikelLookoutFiltered);


                }
            }
        }
        #endregion


        #region "Constructors"

        public AddArtikelViewModel()
        {
            Init();
        }
        #endregion


        #region "Functions"

        private void Init()
        {

            using (var db = new SteinbachEntities())
            {
                ArtikelLookoutFull = new ObservableCollection<lagerliste>(db.lagerlisten.Where(n => n.id == 0));
                ArtikelLookoutFiltered = new ObservableCollection<lagerliste>(db.lagerlisten.Where(n => n.id == 0));


            }

        }
        #endregion


        #region "Events"

        public void fcbChanged(DataTypes.FilteredComboBoxChangedEventArgs e)
        {
            string buf = e.filter;
            using (var db = new SteinbachEntities())
            {

                int res = 0;
                if (int.TryParse(e.filter, out res) == true)
                {
                    ArtikelLookoutFiltered = new ObservableCollection<lagerliste>(db.lagerlisten.Where(n => n.artikelnr.Contains(e.filter)));
                }
                else
                {
                    ArtikelLookoutFiltered = new ObservableCollection<lagerliste>(db.lagerlisten.Where(n => n.bezeichnung.Contains(e.filter) || n.beschreibung.Contains(e.filter)));
                }
            }
        }
        #endregion

        void OnListeUpdated(DTO.dtoAddArtikel liste)
        {
            if (ListeUpdated != null)
            {
                ListeUpdated(liste);
            }

        }

    }



}
