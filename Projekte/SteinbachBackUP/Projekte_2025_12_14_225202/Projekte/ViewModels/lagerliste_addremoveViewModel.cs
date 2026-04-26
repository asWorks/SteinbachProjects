using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Caliburn.Micro;
using DAL;

namespace ProjektDB.ViewModels
{
    public class lagerliste_addremoveViewModel : PropertyChangedBase
    {


        #region "Addional Properties"
        private string _Bewegungsart;
        public string Bewegungsart
        {
            get { return _Bewegungsart; }
            set
            {
                if (value != _Bewegungsart)
                {
                    _Bewegungsart = value;
                    NotifyOfPropertyChange(() => Bewegungsart);
                    isDirty = true;
                }
            }
        }

        private string _Belegart;
        public string Belegart
        {
            get { return _Belegart; }
            set
            {
                if (value != _Belegart)
                {
                    _Belegart = value;
                    NotifyOfPropertyChange(() => Belegart);
                    isDirty = true;
                }
            }
        }




        private string _SchiffUndProjekt;
        public string SchiffUndProjekt
        {
            get { return _SchiffUndProjekt; }
            set
            {
                if (value != _SchiffUndProjekt)
                {
                    _SchiffUndProjekt = value;
                    NotifyOfPropertyChange(() => SchiffUndProjekt);
                    isDirty = true;
                }
            }
        }

        private string _Lieferant;
        public string Lieferant
        {
            get { return _Lieferant; }
            set
            {
                if (value != _Lieferant)
                {
                    _Lieferant = value;
                    NotifyOfPropertyChange(() => Lieferant);
                    isDirty = true;
                }
            }
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                if (value != _Username)
                {
                    _Username = value;
                    NotifyOfPropertyChange(() => Username);
                    isDirty = true;
                }
            }
        }


        private string _Ziellager;
        public string Ziellager
        {
            get { return _Ziellager; }
            set
            {
                if (value != _Ziellager)
                {
                    _Ziellager = value;
                    NotifyOfPropertyChange(() => Ziellager);
                    isDirty = true;
                }
            }
        }

        private string _Quelllager;
        public string Quelllager
        {
            get { return _Quelllager; }
            set
            {
                if (value != _Quelllager)
                {
                    _Quelllager = value;
                    NotifyOfPropertyChange(() => Quelllager);
                    isDirty = true;
                }
            }
        }


        private string _Bemerkungbeleg;
        public string Bemerkungbeleg
        {
            get { return _Bemerkungbeleg; }
            set
            {
                if (value != _Bemerkungbeleg)
                {
                    _Bemerkungbeleg = value;
                    NotifyOfPropertyChange(() => Bemerkungbeleg);
                    isDirty = true;
                }
            }
        }

        private DateTime _Belegdatum;
        public DateTime Belegdatum
        {
            get { return _Belegdatum; }
            set
            {
                if (value != _Belegdatum)
                {
                    _Belegdatum = value;
                    NotifyOfPropertyChange(() => Belegdatum);
                    isDirty = true;
                }
            }
        }

        private int _BelegId;
        public int BelegId
        {
            get { return _BelegId; }
            set
            {
                if (value != _BelegId)
                {
                    _BelegId = value;
                    NotifyOfPropertyChange(() => BelegId);
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

        private Int32? _id_lagerliste;
        public Int32? id_lagerliste
        {
            get { return _id_lagerliste; }
            set
            {
                if (value != _id_lagerliste)
                {
                    _id_lagerliste = value;
                    NotifyOfPropertyChange(() => id_lagerliste);
                    isDirty = true;
                }
            }
        }

        private String _addtype;
        public String addtype
        {
            get { return _addtype; }
            set
            {
                if (value != _addtype)
                {
                    _addtype = value;
                    NotifyOfPropertyChange(() => addtype);
                    isDirty = true;
                }
            }
        }

        private String _projektnummer;
        public String projektnummer
        {
            get { return _projektnummer; }
            set
            {
                if (value != _projektnummer)
                {
                    _projektnummer = value;
                    NotifyOfPropertyChange(() => projektnummer);
                    isDirty = true;
                }
            }
        }

        private Int32? _anzahl;
        public Int32? anzahl
        {
            get { return _anzahl; }
            set
            {
                if (value != _anzahl)
                {
                    _anzahl = value;
                    NotifyOfPropertyChange(() => anzahl);
                    isDirty = true;
                }
            }
        }

        private String _bemerkung;
        public String bemerkung
        {
            get { return _bemerkung; }
            set
            {
                if (value != _bemerkung)
                {
                    _bemerkung = value;
                    NotifyOfPropertyChange(() => bemerkung);
                    isDirty = true;
                }
            }
        }

        private String _type;
        public String type
        {
            get { return _type; }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    NotifyOfPropertyChange(() => type);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_Quelllager;
        public Int32? id_Quelllager
        {
            get { return _id_Quelllager; }
            set
            {
                if (value != _id_Quelllager)
                {
                    _id_Quelllager = value;
                    NotifyOfPropertyChange(() => id_Quelllager);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_Ziellager;
        public Int32? id_Ziellager
        {
            get { return _id_Ziellager; }
            set
            {
                if (value != _id_Ziellager)
                {
                    _id_Ziellager = value;
                    NotifyOfPropertyChange(() => id_Ziellager);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_projekt;
        public Int32? id_projekt
        {
            get { return _id_projekt; }
            set
            {
                if (value != _id_projekt)
                {
                    _id_projekt = value;
                    NotifyOfPropertyChange(() => id_projekt);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_BelegPosition;
        public Int32? id_BelegPosition
        {
            get { return _id_BelegPosition; }
            set
            {
                if (value != _id_BelegPosition)
                {
                    _id_BelegPosition = value;
                    NotifyOfPropertyChange(() => id_BelegPosition);
                    isDirty = true;
                }
            }
        }

        private Int16? _isConfirmed;
        public Int16? isConfirmed
        {
            get { return _isConfirmed; }
            set
            {
                if (value != _isConfirmed)
                {
                    _isConfirmed = value;
                    NotifyOfPropertyChange(() => isConfirmed);
                    isDirty = true;
                }
            }
        }

        private Int32? _Quelllager_BestandAlt;
        public Int32? Quelllager_BestandAlt
        {
            get { return _Quelllager_BestandAlt; }
            set
            {
                if (value != _Quelllager_BestandAlt)
                {
                    _Quelllager_BestandAlt = value;
                    NotifyOfPropertyChange(() => Quelllager_BestandAlt);
                    isDirty = true;
                }
            }
        }

        private Int32? _Quelllager_BestandNeu;
        public Int32? Quelllager_BestandNeu
        {
            get { return _Quelllager_BestandNeu; }
            set
            {
                if (value != _Quelllager_BestandNeu)
                {
                    _Quelllager_BestandNeu = value;
                    NotifyOfPropertyChange(() => Quelllager_BestandNeu);
                    isDirty = true;
                }
            }
        }

        private Int32? _Ziellager_BestandNeu;
        public Int32? Ziellager_BestandNeu
        {
            get { return _Ziellager_BestandNeu; }
            set
            {
                if (value != _Ziellager_BestandNeu)
                {
                    _Ziellager_BestandNeu = value;
                    NotifyOfPropertyChange(() => Ziellager_BestandNeu);
                    isDirty = true;
                }
            }
        }

        private Int32? _Ziellager_BestandAlt;
        public Int32? Ziellager_BestandAlt
        {
            get { return _Ziellager_BestandAlt; }
            set
            {
                if (value != _Ziellager_BestandAlt)
                {
                    _Ziellager_BestandAlt = value;
                    NotifyOfPropertyChange(() => Ziellager_BestandAlt);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_Firma;
        public Int32? id_Firma
        {
            get { return _id_Firma; }
            set
            {
                if (value != _id_Firma)
                {
                    _id_Firma = value;
                    NotifyOfPropertyChange(() => id_Firma);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_user;
        public Int32? id_user
        {
            get { return _id_user; }
            set
            {
                if (value != _id_user)
                {
                    _id_user = value;
                    NotifyOfPropertyChange(() => id_user);
                    isDirty = true;
                }
            }
        }

        private string _BelegnummerLieferant;
        public string BelegnummerLieferant
        {
            get { return _BelegnummerLieferant; }
            set
            {
                if (value != _BelegnummerLieferant)
                {
                    _BelegnummerLieferant = value;
                    NotifyOfPropertyChange(() => BelegnummerLieferant);
                    isDirty = true;
                }
            }
        }



        #endregion


        #region "Constructors"

        public lagerliste_addremoveViewModel(int id)
        {

            lagerliste_addremove item;
            using (var db = new DAL.SteinbachEntities())
            {
                item = db.lagerliste_addremove.Where(n => n.id == id).SingleOrDefault();


                Mapper.CreateMap<lagerliste_addremove, ViewModels.lagerliste_addremoveViewModel>();
                Mapper.Map<lagerliste_addremove, ViewModels.lagerliste_addremoveViewModel>(item, this);

                var un = db.personen.Where(n => n.id == id_user).SingleOrDefault();
                if (un != null)
                {
                    Username = un.benutzername;
                }
                var p = db.projekte.Where(n => n.id == id_projekt).SingleOrDefault();
                if (p != null)
                {
                    SchiffUndProjekt = String.Format("{0} - {1}", p.projektnummer, p.projekt_schiff);
                }

                var ba = db.StammBewegungsarten.Where(n => n.id == addtype).SingleOrDefault();
                if (ba != null)
                {
                    Bewegungsart = ba.Bewegungsart;
                }

                var lief = db.firmen.Where(n => n.id == id_Firma).SingleOrDefault();
                if (lief != null)
                {
                    Lieferant = lief.name;
                }

                


                StammLagerorte lo;

                lo = db.StammLagerorte.Where(n => n.id == id_Ziellager).SingleOrDefault();
                if (lo != null)
                {
                    Ziellager = lo.Lagerortname;
                }

                lo = db.StammLagerorte.Where(n => n.id == id_Quelllager).SingleOrDefault();
                if (lo != null)
                {
                    Quelllager = lo.Lagerortname;
                }

                var bb = from pos in db.SI_BelegePositionen
                         join bel in db.SI_Belege on pos.id_Beleg equals bel.id
                         where pos.id  == id_BelegPosition
                         select bel;


                if (bb.Any() )
                {
                    try
                    {
                        var Beleg = bb.SingleOrDefault();
                        Bemerkungbeleg = Beleg.Bemerkung;
                        Belegdatum = (DateTime)Beleg.Belegdatum;
                        BelegId = Beleg.id;
                        BelegnummerLieferant = Beleg.BelegnummerLieferant;
                        Belegart = db.StammBelegarten.Where(n=>n.id==Beleg.Belegart).SingleOrDefault().Belegart;

                    }
                    catch (Exception)
                    {


                    }

                }




            }

        }

        #endregion

    }
}
