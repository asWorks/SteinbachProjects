using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace ProjektDB.DTO
{
    public class dtoAddArtikel : Screen
    {

        #region "Properties"""
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

        private int _id_Lagerliste;
        public int id_Lagerliste
        {
            get { return _id_Lagerliste; }
            set
            {
                if (value != _id_Lagerliste)
                {
                    _id_Lagerliste = value;
                    NotifyOfPropertyChange(() => id_Lagerliste);

                }
            }
        }

        private string _Artikelnummer;
        public string Artikelnummer
        {
            get { return _Artikelnummer; }
            set
            {
                if (value != _Artikelnummer)
                {
                    _Artikelnummer = value;
                    NotifyOfPropertyChange(() => Artikelnummer);

                }
            }
        }

        private string _Bezeichnung;
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set
            {
                if (value != _Bezeichnung)
                {
                    _Bezeichnung = value;
                    NotifyOfPropertyChange(() => Bezeichnung);

                }
            }
        }

        private string _Beschreibung;
        public string Beschreibung
        {
            get { return _Beschreibung; }
            set
            {
                if (value != _Beschreibung)
                {
                    _Beschreibung = value;
                    NotifyOfPropertyChange(() => Beschreibung);

                }
            }
        }

        private decimal _Nettopreis;
        public decimal Nettopreis
        {
            get { return _Nettopreis; }
            set
            {
                if (value != _Nettopreis)
                {
                    _Nettopreis = value;
                    NotifyOfPropertyChange(() => Nettopreis);

                }
            }
        }

        private decimal _Bruttopreis;
        public decimal Bruttopreis
        {
            get { return _Bruttopreis; }
            set
            {
                if (value != _Bruttopreis)
                {
                    _Bruttopreis = value;
                    NotifyOfPropertyChange(() => Bruttopreis);

                }
            }
        }

        private int? _id_Lieferant;

        public int? id_Lieferant
        {
            get { return _id_Lieferant; }
            set
            {
                _id_Lieferant = value;
                NotifyOfPropertyChange(() => id_Lieferant);

            }
        }


        private bool? _HandelsWare;

        public bool? Handelsware
        {
            get { return _HandelsWare; }
            set
            {
                _HandelsWare = value;
                NotifyOfPropertyChange(() => Handelsware);
            }
        }

        private string _einheit;
        public string einheit
        {
            get { return _einheit; }
            set
            {
                if (value != _einheit)
                {
                    _einheit = value;
                    NotifyOfPropertyChange(() => einheit);

                }
            }
        }

        #endregion

        #region "Functions"

        private bool FillDtoArtikel(int id_Lagerliste, string ArtNr, string Bezeichnung, string Beschreibung, string einheit, decimal? Nettopreis, decimal? Bruttopreis, decimal Menge, int id_Lieferant, short? Handelsware)
        {

            this.id_Lagerliste = id_Lagerliste;
            this.Artikelnummer = ArtNr ?? "";
            this.Bezeichnung = Bezeichnung ?? "";
            this.Beschreibung = Beschreibung ?? "";
            this.einheit = einheit ?? "";

            this.Nettopreis = !Nettopreis.HasValue ? 0m : (decimal)Nettopreis;

            this.Bruttopreis = !Bruttopreis.HasValue ? 0m : (decimal)Bruttopreis;
            this.Menge = Menge == null ? 0m : Menge;
            this.id_Lieferant = id_Lieferant;
            this.Handelsware = Handelsware.HasValue ? Convert.ToBoolean(Handelsware) : false;

            return true;
        }
        #endregion



        #region "Constructors"

        public dtoAddArtikel()
        {
        }

        public dtoAddArtikel(int id_Lagerliste, string ArtNr, string Bezeichnung, string Beschreibung, string einheit, decimal? Nettopreis, decimal? Bruttopreis, decimal Menge, int id_Lieferant, short? Handelsware)
        {
            this.id_Lagerliste = id_Lagerliste;
            this.Artikelnummer = ArtNr ?? "";
            this.Bezeichnung = Bezeichnung ?? "";
            this.Beschreibung = Beschreibung ?? "";
            this.einheit = einheit ?? "";

            this.Nettopreis = !Nettopreis.HasValue ? 0m : (decimal)Nettopreis;

            this.Bruttopreis = !Bruttopreis.HasValue ? 0m : (decimal)Bruttopreis;
            this.Menge = Menge == null ? 0m : Menge;
            this.id_Lieferant = id_Lieferant;
            this.Handelsware = Handelsware.HasValue ? Convert.ToBoolean(Handelsware) : false;
        }
        #endregion


    }
}
