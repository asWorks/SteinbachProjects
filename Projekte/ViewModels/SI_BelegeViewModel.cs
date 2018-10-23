using AutoMapper;
using Caliburn.Micro;
using CommonTools.Tools;
using DAL;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using WaWi.Lagerbuchungen.Lagerbuchungen;





namespace ProjektDB.ViewModels
{
    public class SI_BelegeViewModel : Screen
    {

        public event System.Action RingArtikelFullViewSourceChanged;

        private void OnRingArtikelFullViewSourceChanged()
        {
            if (RingArtikelFullViewSourceChanged != null)
            {
                RingArtikelFullViewSourceChanged();
            }
        }



        public const string ArtikelnummerNichtVorhandenMessage = "Die Artikelnummer existiert nicht";
        public const string ArtikelnummerMehrfachVorhandenMessage = "Die Artikelnummer existiert mehrfach";
        public const string BelegartKannNichtNullSeinMessage = "Die Belegart kann nicht null sein";
        public const string ArgumentKannNichtNullSeinMessage = "Das Argument kann nicht null sein";
        private const string KalkulationstabelleOderArtikelnummerNichtGefundenMessage = "Die Kalkulationstabelle oder die Artikelnummer wurden nicht gefunden";

        //Change for GIT

        #region "Declarations"
        SteinbachEntities db;
        public SI_Belege Beleg = null;
        Dispatcher ThisDispatcher = null;
        System.Windows.Input.Cursor ThisCursor = null;
        DispatcherTimer ti;
        bool DoSortZusatzzeilen = false;
        bool IstBelegVerarbeitet = false;


        private ViewModels.CheckListBoxes.ListboxBelegeTextbausteineViewModel _ListboxBelegeTextbausteineVM;
        public ViewModels.CheckListBoxes.ListboxBelegeTextbausteineViewModel ListboxBelegeTextbausteineVM
        {
            get
            {
                return _ListboxBelegeTextbausteineVM;
            }
            set
            {
                if (value != _ListboxBelegeTextbausteineVM)
                {
                    _ListboxBelegeTextbausteineVM = value;
                    NotifyOfPropertyChange(() => ListboxBelegeTextbausteineVM);
                    isDirty = true;
                }
            }
        }

        public enum enumZusatzzeileRefType
        {
            Zwischensumme,
            Ust,
            NeuePosition
        }



        #endregion

        #region "PosUI"

        public bool IstVorgangBeleg
        {
            get; set;
        }
        SI_Belege SourceBeleg = null;
        int SourceBelegID = 0;

        public string ImportFileName
        {
            get; set;
        }
        public string kundenname
        {
            get; set;
        }
        public string firmenname
        {
            get; set;
        }
        public string type
        {
            get; set;
        }
        public string projekt_schiff
        {
            get; set;
        }
        public string BelegartName
        {
            get; set;
        }
        public string BeziehungPartner
        {
            get; set;
        }
        private bool _isFirmaEnabled;
        public bool isProjektVisible
        {
            get; set;
        }
        public bool isFirmaVisible
        {
            get; set;
        }
        public bool isQuellLagerEnabled
        {
            get; set;
        }
        public bool isZielLagerEnabled
        {
            get; set;
        }
        private bool _isAdressenVisible;
        public bool isAdressenVisible
        {
            get
            {
                return _isAdressenVisible;
            }
            set
            {
                if (value != _isAdressenVisible)
                {
                    _isAdressenVisible = value;
                    NotifyOfPropertyChange(() => isAdressenVisible);
                    isDirty = true;
                }
            }
        }
        private bool _isQuelllagerVisible;
        public bool isPreiseAnzeigen
        {
            get; set;
        }

        private bool _isVorgaengeEnabled;
        public bool isVorgaengeEnabled
        {
            get
            {
                return _isVorgaengeEnabled;
            }
            set
            {
                if (value != _isVorgaengeEnabled)
                {
                    _isVorgaengeEnabled = value;
                    NotifyOfPropertyChange(() => isVorgaengeEnabled);
                    isDirty = true;
                }
            }
        }

        private bool _isUmsatzsteuerChoiseVisible;
        public bool isUmsatzsteuerChoiseVisible
        {
            get
            {
                return _isUmsatzsteuerChoiseVisible;
            }
            set
            {
                if (value != _isUmsatzsteuerChoiseVisible)
                {
                    _isUmsatzsteuerChoiseVisible = value;
                    NotifyOfPropertyChange(() => isUmsatzsteuerChoiseVisible);
                    isDirty = true;
                }
            }
        }

        private Visibility _isButtonTestVisible;
        public Visibility isButtonTestVisible
        {
            get
            {
                return _isButtonTestVisible;
            }
            set
            {
                if (value != _isButtonTestVisible)
                {
                    _isButtonTestVisible = value;
                    NotifyOfPropertyChange(() => isButtonTestVisible);
                    isDirty = true;
                }
            }
        }



        private Visibility _ZusatzzeilenVisible;

        public Visibility ZusatzzeilenVisible
        {
            get
            {
                return _ZusatzzeilenVisible;
            }
            set
            {
                _ZusatzzeilenVisible = value;
                NotifyOfPropertyChange(() => ZusatzzeilenVisible);
            }
        }

        private double _BelegePositionenHeight = 367;
        public double BelegePositionenHeight
        {
            get
            {
                return _BelegePositionenHeight;
            }
            set
            {
                if (value != _BelegePositionenHeight)
                {
                    _BelegePositionenHeight = value;
                    NotifyOfPropertyChange(() => BelegePositionenHeight);
                    isDirty = true;
                }
            }
        }



        public bool isQuelllagerVisible
        {
            get
            {
                return _isQuelllagerVisible;
            }
            set
            {
                _isQuelllagerVisible = value;
                NotifyOfPropertyChange(() => isQuelllagerVisible);
            }
        }

        private string _ContentZiellager;

        public string ContentZiellager
        {
            get
            {
                return _ContentZiellager;
            }
            set
            {
                _ContentZiellager = value;
                NotifyOfPropertyChange(() => ContentZiellager);
            }
        }

        public bool isFirmaEnabled
        {
            get
            {
                return _isFirmaEnabled;
            }
            set
            {
                _isFirmaEnabled = value;
                NotifyOfPropertyChange(() => isFirmaEnabled);

            }
        }


        private SolidColorBrush _BackgroundState;
        public SolidColorBrush BackgroundState
        {
            get
            {
                return _BackgroundState;
            }
            set
            {
                if (value != _BackgroundState)
                {
                    _BackgroundState = value;
                    NotifyOfPropertyChange(() => BackgroundState);
                    isDirty = true;
                }
            }
        }


        private bool _isButtonAddPositionEnabled;

        public bool isButtonAddPositionEnabled
        {
            get
            {
                return _isButtonAddPositionEnabled;
            }
            set
            {
                _isButtonAddPositionEnabled = value;
                NotifyOfPropertyChange(() => isButtonAddPositionEnabled);
            }
        }


        private bool _isProjektEnabled;
        public bool isProjektEnabled
        {
            get
            {
                return _isProjektEnabled;
            }
            set
            {
                _isProjektEnabled = value;
                NotifyOfPropertyChange(() => isProjektEnabled);

            }
        }

        private string _EKVK;
        public string EKVK
        {
            get
            {
                return _EKVK;
            }
            set
            {
                _EKVK = value;
                NotifyOfPropertyChange(() => EKVK);
                VisibilityFirmaProjekt();
            }
        }

        private bool _isEnabled;
        public bool isEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                isVorgaengeEnabled = !value;
                NotifyOfPropertyChange(() => isEnabled);
                VisibilityFirmaProjekt();
                SetEditingEnabled(value);
            }
        }

        private bool _isZusatzinfoVisible;
        public bool isZusatzinfoVisible
        {
            get
            {
                return _isZusatzinfoVisible;
            }
            set
            {
                if (value != _isZusatzinfoVisible)
                {
                    _isZusatzinfoVisible = value;
                    NotifyOfPropertyChange(() => isZusatzinfoVisible);
                    isDirty = true;
                }
            }
        }

        private bool _isVisible;
        public bool isVisible
        {
            get
            {
                return _isVisible;
            }
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

        private bool _isAdresseExpanded;
        public bool isAdresseExpanded
        {
            get
            {
                return _isAdresseExpanded;
            }
            set
            {
                if (value != _isAdresseExpanded)
                {
                    _isAdresseExpanded = value;
                    NotifyOfPropertyChange(() => isAdresseExpanded);
                    // isDirty = true;
                }
            }
        }

        private bool _isEkVisible;
        public bool isEkVisible
        {
            get
            {
                return _isEkVisible;
            }
            set
            {
                if (value != _isEkVisible)
                {
                    _isEkVisible = value;
                    NotifyOfPropertyChange(() => isEkVisible);
                    isDirty = true;
                }
            }
        }




        #endregion

        #region "ObservableColections"
        //private ObservableCollection<SI_Belege> _ViewSource;
        //public ObservableCollection<SI_Belege> ViewSource
        //{
        //    get { return _ViewSource; }
        //    set
        //    {
        //        if (value != _ViewSource)
        //        {
        //            _ViewSource = value;
        //            NotifyOfPropertyChange(() => ViewSource);
        //            isDirty = true;
        //        }
        //    }
        //}

        //private SI_Belege _SelectedViewSource;
        //public SI_Belege SelectedViewSource
        //{
        //    get { return _SelectedViewSource; }
        //    set
        //    {
        //        if (value != _SelectedViewSource)
        //        {
        //            _SelectedViewSource = value;
        //            NotifyOfPropertyChange(() => SelectedViewSource);
        //            isDirty = true;
        //        }
        //    }
        //}


        //private ObservableCollection<lagerliste> _LagerlisteFullViewSource;
        //public ObservableCollection<lagerliste> LagerlisteFullViewSource
        //{
        //    get { return _LagerlisteFullViewSource; }
        //    set
        //    {
        //        if (value != _LagerlisteFullViewSource)
        //        {
        //            _LagerlisteFullViewSource = value;
        //            NotifyOfPropertyChange(() => LagerlisteFullViewSource);

        //        }
        //    }
        //}



        private ObservableCollection<SI_BelegePositionen> _BelegePositionen;
        public ObservableCollection<SI_BelegePositionen> BelegePositionen
        {
            get
            {
                return _BelegePositionen;
            }
            set
            {
                if (value != _BelegePositionen)
                {
                    _BelegePositionen = value;
                    SetEditingEnabled(isEnabled);
                    NotifyOfPropertyChange(() => BelegePositionen);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<SI_BelegeZusatzzeilen> _Zusatzzeilen;
        public ObservableCollection<SI_BelegeZusatzzeilen> Zusatzzeilen
        {
            get
            {
                return _Zusatzzeilen;
            }
            set
            {
                if (value != _Zusatzzeilen)
                {
                    _Zusatzzeilen = value;
                    NotifyOfPropertyChange(() => Zusatzzeilen);
                    isDirty = true;
                }
            }
        }



        //private SI_BelegePositionen _SelectedBelegePositionen;
        //public SI_BelegePositionen SelectedBelegePositionen
        //{
        //    get { return _SelectedBelegePositionen; }
        //    set
        //    {
        //        if (value != _SelectedBelegePositionen)
        //        {
        //            _SelectedBelegePositionen = value;
        //            NotifyOfPropertyChange(() => SelectedBelegePositionen);
        //            isDirty = true;
        //        }
        //    }
        //}

        private ObservableCollection<StammLagerorte> _Lagerorte;
        public ObservableCollection<StammLagerorte> Lagerorte
        {
            get
            {
                return _Lagerorte;
            }
            set
            {
                if (value != _Lagerorte)
                {
                    _Lagerorte = value;
                    NotifyOfPropertyChange(() => Lagerorte);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<StammLagerorte> _LagerorteZiel;
        public ObservableCollection<StammLagerorte> LagerorteZiel
        {
            get
            {
                return _LagerorteZiel;
            }
            set
            {
                if (value != _LagerorteZiel)
                {
                    _LagerorteZiel = value;
                    NotifyOfPropertyChange(() => LagerorteZiel);
                    isDirty = true;
                }
            }
        }



        private StammLagerorte _SelectedZiellager;
        public StammLagerorte SelectedZiellager
        {
            get
            {
                return _SelectedZiellager;
            }
            set
            {
                if (value != _SelectedZiellager)
                {
                    _SelectedZiellager = value;
                    id_Ziellager = value == null ? 0 : value.id;
                    NotifyOfPropertyChange(() => SelectedZiellager);
                    isDirty = true;
                }
            }
        }

        private StammLagerorte _SelectedQuellLager;
        public StammLagerorte SelectedQuellLager
        {
            get
            {
                return _SelectedQuellLager;
            }
            set
            {
                if (value != _SelectedQuellLager)
                {
                    _SelectedQuellLager = value;
                    id_Quelllager = value == null ? 0 : value.id;
                    NotifyOfPropertyChange(() => SelectedQuellLager);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<StammLagerorte> _Sets;
        public ObservableCollection<StammLagerorte> Sets
        {
            get
            {
                return _Sets;
            }
            set
            {
                if (value != _Sets)
                {
                    _Sets = value;
                    NotifyOfPropertyChange(() => Sets);
                    isDirty = true;
                }
            }
        }

        private StammLagerorte _SelectedSets;
        public StammLagerorte SelectedSets
        {
            get
            {
                return _SelectedSets;
            }
            set
            {
                if (value != _SelectedSets)
                {
                    _SelectedSets = value;

                    AddSetPositionen(value);

                    NotifyOfPropertyChange(() => SelectedSets);
                    // isDirty = true;
                }
            }
        }



        private ObservableCollection<StammBelegarten> _Belegarten;
        public ObservableCollection<StammBelegarten> Belegarten
        {
            get
            {
                return _Belegarten;
            }
            set
            {
                if (value != _Belegarten)
                {
                    _Belegarten = value;
                    NotifyOfPropertyChange(() => Belegarten);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<view_BelegartenBelegUebernahme> _BelegartenUebernahme;
        public ObservableCollection<view_BelegartenBelegUebernahme> BelegartenUebernahme
        {
            get
            {
                return _BelegartenUebernahme;
            }
            set
            {
                if (value != _BelegartenUebernahme)
                {
                    _BelegartenUebernahme = value;
                    NotifyOfPropertyChange(() => BelegartenUebernahme);
                    isDirty = true;
                }
            }
        }




        private view_BelegartenBelegUebernahme _SelectedNeueBelegart;
        public view_BelegartenBelegUebernahme SelectedNeueBelegart
        {
            get
            {
                return _SelectedNeueBelegart;
            }
            set
            {
                if (value != null)
                {
                    if (value != _SelectedNeueBelegart)
                    {
                        _SelectedNeueBelegart = value;

                        StammBelegarten sba = db.StammBelegarten.Where(n => n.id == value.UebernahmeBelgart).SingleOrDefault();
                        views.EditBelege bel = new views.EditBelege(id, sba);
                        bel.ShowDialog();

                        NotifyOfPropertyChange(() => SelectedNeueBelegart);
                        //isDirty = true;
                    }
                }

            }
        }




        private ObservableCollection<Models.ProjekteSource> _Projekte;
        public ObservableCollection<Models.ProjekteSource> Projekte
        {
            get
            {
                return _Projekte;
            }
            set
            {
                if (value != _Projekte)
                {
                    _Projekte = value;
                    NotifyOfPropertyChange(() => Projekte);
                    isDirty = true;
                }
            }
        }

        private Models.ProjekteSource _SelectedProjekte;
        public Models.ProjekteSource SelectedProjekte
        {
            get
            {
                return _SelectedProjekte;
            }
            set
            {
                if (value != _SelectedProjekte)
                {
                    if (value == null)
                    {
                        _id_Projekt = null;
                    }
                    else
                    {
                        this._id_Projekt = value.id;
                    }

                    _SelectedProjekte = value;

                    if (id_Projekt != null)
                    {


                        SetKundeFirmaFromProject();
                        // Belegnummer = CommonTools.Tools.Belegnummern.GetBelegnummerProjekt(Belegart, (int)id_Projekt);
                        NotifyOfPropertyChange(() => kundenname);
                        NotifyOfPropertyChange(() => firmenname);
                        NotifyOfPropertyChange(() => type);
                        NotifyOfPropertyChange(() => projekt_schiff);
                        LoadLieferadresse();
                        LoadReference();


                    }

                    NotifyOfPropertyChange(() => SelectedProjekte);
                    isDirty = true;
                }
            }
        }

        //private ObservableCollection<lagerliste> _Lagerlisten;
        //public ObservableCollection<lagerliste> Lagerlisten
        //{
        //    get { return _Lagerlisten; }
        //    set
        //    {
        //        if (value != _Lagerlisten)
        //        {
        //            _Lagerlisten = value;
        //            NotifyOfPropertyChange(() => Lagerlisten);
        //            isDirty = true;
        //        }
        //    }
        //}

        //private lagerliste _SelectedLagerlisten;
        //public lagerliste SelectedLagerlisten
        //{
        //    get { return _SelectedLagerlisten; }
        //    set
        //    {
        //        if (value != _SelectedLagerlisten)
        //        {
        //            _SelectedLagerlisten = value;
        //            NotifyOfPropertyChange(() => SelectedLagerlisten);
        //            isDirty = true;
        //        }
        //    }
        //}

        //private ObservableCollection<lagerliste> _LagerlistenFull;
        //public ObservableCollection<lagerliste> LagerlistenFull
        //{
        //    get { return _LagerlistenFull; }
        //    set
        //    {
        //        if (value != _LagerlistenFull)
        //        {
        //            _LagerlistenFull = value;
        //            NotifyOfPropertyChange(() => LagerlistenFull);
        //            isDirty = true;
        //        }
        //    }
        //}

        //private lagerliste _SelectedLagerlistenFull;
        //public lagerliste SelectedLagerlistenFull
        //{
        //    get { return _SelectedLagerlistenFull; }
        //    set
        //    {
        //        if (value != _SelectedLagerlistenFull)
        //        {
        //            _SelectedLagerlistenFull = value;
        //            NotifyOfPropertyChange(() => SelectedLagerlistenFull);
        //            isDirty = true;
        //        }
        //    }
        //}

        private ObservableCollection<AuswahlEintraege> _Sprachen;
        public ObservableCollection<AuswahlEintraege> Sprachen
        {
            get
            {
                return _Sprachen;
            }
            set
            {
                if (value != _Sprachen)
                {
                    _Sprachen = value;
                    NotifyOfPropertyChange(() => Sprachen);
                    isDirty = true;
                }
            }
        }

        private AuswahlEintraege _SelectedSprachen;
        public AuswahlEintraege SelectedSprachen
        {
            get
            {
                return _SelectedSprachen;
            }
            set
            {
                if (value != _SelectedSprachen)
                {
                    _SelectedSprachen = value;
                    id_Sprache = value == null ? 0 : value.ai_int;
                    if (isLoaded)
                    {
                        ListboxBelegeTextbausteineVM.UpdateAvailableNames();
                        ti.Start();
                        LoadSignatures();
                    }
                    NotifyOfPropertyChange(() => SelectedSprachen);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<Models.FirmenSource> _Firmen;
        public ObservableCollection<Models.FirmenSource> Firmen
        {
            get
            {
                return _Firmen;
            }
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



        private ObservableCollection<StammBewegungsarten> _Bewegungsarten;
        public ObservableCollection<StammBewegungsarten> Bewegungsarten
        {
            get
            {
                return _Bewegungsarten;
            }
            set
            {
                if (value != _Bewegungsarten)
                {
                    _Bewegungsarten = value;
                    NotifyOfPropertyChange(() => Bewegungsarten);
                    isDirty = true;
                }
            }
        }

        private StammBewegungsarten _SelectedBewegungsarten;
        public StammBewegungsarten SelectedBewegungsarten
        {
            get
            {
                return _SelectedBewegungsarten;
            }
            set
            {
                if (value != _SelectedBewegungsarten)
                {
                    _SelectedBewegungsarten = value;
                    NotifyOfPropertyChange(() => SelectedBewegungsarten);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<kalkulationstabelle> _Kalkulation;
        public ObservableCollection<kalkulationstabelle> Kalkulation
        {
            get
            {
                return _Kalkulation;
            }
            set
            {
                if (value != _Kalkulation)
                {
                    _Kalkulation = value;
                    NotifyOfPropertyChange(() => Kalkulation);
                    isDirty = true;
                }
            }
        }

        private kalkulationstabelle _SelectedKalkulation;
        public kalkulationstabelle SelectedKalkulation
        {
            get
            {
                return _SelectedKalkulation;
            }
            set
            {
                if (value != _SelectedKalkulation)
                {
                    _SelectedKalkulation = value;
                    SetSateAddPosition();
                    if (value != null)
                    {
                        //ImportKalkulation(value);
                    }

                    NotifyOfPropertyChange(() => SelectedKalkulation);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<Firmen_Adressen> _FirmenAdressen;
        public ObservableCollection<Firmen_Adressen> FirmenAdressen
        {
            get
            {
                return _FirmenAdressen;
            }
            set
            {
                if (value != _FirmenAdressen)
                {
                    _FirmenAdressen = value;
                    NotifyOfPropertyChange(() => FirmenAdressen);
                    isDirty = true;
                }
            }
        }

        private Firmen_Adressen _SelectedFirmenAdressen;
        public Firmen_Adressen SelectedFirmenAdressen
        {
            get
            {
                return _SelectedFirmenAdressen;
            }
            set
            {
                if (value != _SelectedFirmenAdressen)
                {
                    _SelectedFirmenAdressen = value;
                    if (SelectedFirmen != null)
                    {
                        var fa = SelectedFirmen.name;
                        FillAdresse(value, fa);
                    }


                    NotifyOfPropertyChange(() => SelectedFirmenAdressen);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<C_Localizing> _Signaturen;
        public ObservableCollection<C_Localizing> Signaturen
        {
            get
            {
                return _Signaturen;
            }
            set
            {
                if (value != _Signaturen)
                {
                    _Signaturen = value;
                    NotifyOfPropertyChange(() => Signaturen);
                    isDirty = true;
                }
            }
        }


        private C_Localizing _SelectedSignaturen1;
        public C_Localizing SelectedSignaturen1
        {
            get
            {
                return _SelectedSignaturen1;
            }
            set
            {
                if (value != null)
                {
                    if (value != _SelectedSignaturen1)
                    {
                        _SelectedSignaturen1 = value;
                        Signatur1 = value.Wert;
                        NotifyOfPropertyChange(() => SelectedSignaturen1);
                        isDirty = true;
                    }
                }

            }
        }

        private C_Localizing _SelectedSignaturen2;
        public C_Localizing SelectedSignaturen2
        {
            get
            {
                return _SelectedSignaturen2;
            }
            set
            {
                if (value != null)
                {
                    if (value != _SelectedSignaturen2)
                    {
                        _SelectedSignaturen2 = value;
                        Signatur2 = value.Wert;
                        NotifyOfPropertyChange(() => SelectedSignaturen2);
                        isDirty = true;
                    }

                }

            }
        }





        #endregion

        #region "Properties"
        public bool isLoaded
        {
            get; set;
        }
        public bool isDirty
        {
            get; set;
        }
        private Int32 _id;
        public Int32 id
        {
            get
            {
                return _id;
            }
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

        private bool _isFatalError;
        public bool isFatalError
        {
            get
            {
                return _isFatalError;
            }
            set
            {
                if (value != _isFatalError)
                {
                    _isFatalError = value;

                    BackgroundState = value == true ? Brushes.LightSalmon : Brushes.Transparent;

                    NotifyOfPropertyChange(() => isFatalError);
                    isDirty = true;
                }
            }
        }



        private DateTime? _created;
        public DateTime? created
        {
            get
            {
                return _created;
            }
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

        private String _Belegart;
        public String Belegart
        {
            get
            {
                return _Belegart;
            }
            set
            {
                if (value != _Belegart)
                {
                    _Belegart = value;

                    // InitBelegart();
                    if (value != null)
                    {
                        SelectedBelegarten = db.StammBelegarten.Where(n => n.id == value).SingleOrDefault();
                    }

                    NotifyOfPropertyChange(() => Belegart);
                    isDirty = true;
                    // LoadLieferadresse();
                }
            }
        }


        private StammBelegarten _SelectedBelegarten;
        public StammBelegarten SelectedBelegarten
        {
            get
            {
                return _SelectedBelegarten;
            }
            set
            {
                if (value != _SelectedBelegarten)
                {
                    _SelectedBelegarten = value;
                    _Belegart = value == null ? string.Empty : value.id;
                    if (isLoaded && value != null)
                    {
                        ListboxBelegeTextbausteineVM.UpdateAvailableNames();
                        ti.Start();
                    }
                    InitBelegart();

                    NotifyOfPropertyChange(() => SelectedBelegarten);
                    isDirty = true;
                    LoadLieferadresse();
                }
            }
        }

        private String _Belegnummer;
        public String Belegnummer
        {
            get
            {
                return _Belegnummer;
            }
            set
            {
                if (value != _Belegnummer)
                {
                    _Belegnummer = value;
                    NotifyOfPropertyChange(() => Belegnummer);
                    isDirty = true;
                }
            }
        }

        private DateTime? _Belegdatum;
        public DateTime? Belegdatum
        {
            get
            {
                return _Belegdatum;
            }
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

        private Int32? _id_Ziellager;
        public Int32? id_Ziellager
        {
            get
            {
                return _id_Ziellager;
            }
            set
            {
                if (value != _id_Ziellager)
                {
                    _id_Ziellager = value;
                    SelectedZiellager = db.StammLagerorte.Where(n => n.id == value).SingleOrDefault();
                    NotifyOfPropertyChange(() => id_Ziellager);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_Quelllager;
        public Int32? id_Quelllager
        {
            get
            {
                return _id_Quelllager;
            }
            set
            {
                if (value != _id_Quelllager)
                {
                    _id_Quelllager = value;
                    SelectedQuellLager = db.StammLagerorte.Where(n => n.id == id_Quelllager).SingleOrDefault();
                    NotifyOfPropertyChange(() => id_Quelllager);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_Projekt;
        public Int32? id_Projekt
        {
            get
            {
                return _id_Projekt;
            }
            set
            {
                if (value != _id_Projekt)
                {
                    _id_Projekt = value;

                    //if (_id_Projekt != null)
                    //{


                    //    SetKundeFirmaFromProject();
                    //   // Belegnummer = CommonTools.Tools.Belegnummern.GetBelegnummerProjekt(Belegart, (int)id_Projekt);
                    //    NotifyOfPropertyChange(() => kundenname);
                    //    NotifyOfPropertyChange(() => firmenname);
                    //    NotifyOfPropertyChange(() => type);
                    //    NotifyOfPropertyChange(() => projekt_schiff);
                    //    LoadLieferadresse();
                    //    LoadReference();


                    //}

                    var query = from p in db.projekte
                                where p.id == value
                                select new Models.ProjekteSource
                                {
                                    projektnummer = p.projektnummer,
                                    type = p.type,
                                    firmenname = p.firmenname,
                                    kundenname = p.kundenname,
                                    projekt_schiff = p.projekt_schiff,
                                    id = p.id

                                };

                    SelectedProjekte = query.SingleOrDefault();
                    NotifyOfPropertyChange(() => id_Projekt);

                    isDirty = true;
                }
            }
        }

        private String _Projektnummer;
        public String Projektnummer
        {
            get
            {
                return _Projektnummer;
            }
            set
            {
                if (value != _Projektnummer)
                {
                    _Projektnummer = value;
                    NotifyOfPropertyChange(() => Projektnummer);
                    isDirty = true;
                }
            }
        }

        private Int32? _QuellBeleg;
        public Int32? QuellBeleg
        {
            get
            {
                return _QuellBeleg;
            }
            set
            {
                if (value != _QuellBeleg)
                {
                    _QuellBeleg = value;
                    NotifyOfPropertyChange(() => QuellBeleg);
                    isDirty = true;
                }
            }
        }

        private Int16? _istGebucht;
        public Int16? istGebucht
        {
            get
            {
                return _istGebucht;
            }
            set
            {
                //if (value != _istGebucht)
                //{
                _istGebucht = value;
                isEnabled = istGebucht == 1 ? false : true;
                SetEditingEnabled(isEnabled);
                NotifyOfPropertyChange(() => istGebucht);
                isDirty = true;
                // }
            }
        }

        private String _Bemerkung;
        public String Bemerkung
        {
            get
            {
                return _Bemerkung;
            }
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

        private Int32? _id_Sprache;
        public Int32? id_Sprache
        {
            get
            {
                return _id_Sprache;
            }
            set
            {
                if (value != _id_Sprache)
                {
                    _id_Sprache = value;
                    SelectedSprachen = db.AuswahlEintraege.Where(n => n.Gruppe == "TypSprache" && n.ai_int == value).SingleOrDefault();
                    HandleMWST_Change();
                    LoadZahlungsbedingungen();
                    LoadReference();
                    NotifyOfPropertyChange(() => id_Sprache);
                    isDirty = true;
                }
            }
        }

        private Int32? _id_Firma;
        public Int32? id_Firma
        {
            get
            {
                return _id_Firma;
            }
            set
            {
                if (value != _id_Firma)
                {
                    _id_Firma = value;
                    SelectedFirmen = GetFirmensourceByID((int)value); //db.firmen.Where(n => n.id == value).SingleOrDefault();
                    NotifyOfPropertyChange(() => id_Firma);
                    isDirty = true;

                }
            }
        }



        private Models.FirmenSource _SelectedFirmen;
        public Models.FirmenSource SelectedFirmen
        {
            get
            {
                return _SelectedFirmen;
            }
            set
            {
                if (value != _SelectedFirmen)
                {

                    _SelectedFirmen = value;
                    if (value == null)
                    {
                        _id_Firma = null;
                    }
                    else
                    {
                        _id_Firma = value.id;
                        if (firmenname == null || firmenname == string.Empty)
                        {
                            firmenname = value.name;
                        }


                    }


                    InitBelegart();
                    LoadZahlungsbedingungen();
                    LoadLieferadresse();



                    NotifyOfPropertyChange(() => SelectedFirmen);
                    isDirty = true;
                }
            }
        }


        private int _id_Stammadresse;
        public int id_Stammadresse
        {
            get
            {
                return _id_Stammadresse;
            }
            set
            {
                if (value != _id_Stammadresse)
                {
                    _id_Stammadresse = value;
                    NotifyOfPropertyChange(() => id_Stammadresse);
                    isDirty = true;
                }
            }
        }

        private string _Straße;
        public string Straße
        {
            get
            {
                return _Straße;
            }
            set
            {
                if (value != _Straße)
                {
                    _Straße = value;
                    NotifyOfPropertyChange(() => Straße);
                    isDirty = true;
                }
            }
        }



        private string _PLZ;
        public string PLZ
        {
            get
            {
                return _PLZ;
            }
            set
            {
                if (value != _PLZ)
                {
                    _PLZ = value;
                    NotifyOfPropertyChange(() => PLZ);
                    isDirty = true;
                }
            }
        }

        private string _Ort;
        public string Ort
        {
            get
            {
                return _Ort;
            }
            set
            {
                if (value != _Ort)
                {
                    _Ort = value;
                    NotifyOfPropertyChange(() => Ort);
                    isDirty = true;
                }
            }
        }

        private string _ZusatzInfo;
        public string ZusatzInfo
        {
            get
            {
                return _ZusatzInfo;
            }
            set
            {
                if (value != _ZusatzInfo)
                {
                    _ZusatzInfo = value;
                    NotifyOfPropertyChange(() => ZusatzInfo);
                    isDirty = true;
                }
            }
        }


        private string _ZusatzInfo2;
        public string ZusatzInfo2
        {
            get
            {
                return _ZusatzInfo2;
            }
            set
            {
                if (value != _ZusatzInfo2)
                {
                    _ZusatzInfo2 = value;
                    NotifyOfPropertyChange(() => ZusatzInfo2);
                    isDirty = true;
                }
            }
        }



        private string _ZusatzInfo3;
        public string ZusatzInfo3
        {
            get
            {
                return _ZusatzInfo3;
            }
            set
            {
                if (value != _ZusatzInfo3)
                {
                    _ZusatzInfo3 = value;
                    NotifyOfPropertyChange(() => ZusatzInfo3);
                    isDirty = true;
                }
            }
        }


        private string _Land;
        public string Land
        {
            get
            {
                return _Land;
            }
            set
            {
                if (value != _Land)
                {
                    _Land = value;
                    NotifyOfPropertyChange(() => Land);
                    isDirty = true;
                }
            }
        }

        private string _VAT;
        public string VAT
        {
            get
            {
                return _VAT;
            }
            set
            {
                if (value != _VAT)
                {
                    _VAT = value;
                    NotifyOfPropertyChange(() => VAT);
                    isDirty = true;
                }
            }
        }

        private int _id_user;
        public int id_user
        {
            get
            {
                return _id_user;
            }
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
            get
            {
                return _BelegnummerLieferant;
            }
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



        private string _Belegtext;
        public string Belegtext
        {
            get
            {
                return _Belegtext;
            }
            set
            {
                if (value != _Belegtext)
                {
                    _Belegtext = value;
                    NotifyOfPropertyChange(() => Belegtext);
                    isDirty = true;
                }
            }
        }

        private Decimal _SummeBelegPositionen;
        public Decimal SummeBelegPositionen
        {
            get
            {
                return _SummeBelegPositionen;
            }
            set
            {
                if (value != _SummeBelegPositionen)
                {
                    _SummeBelegPositionen = value;
                    NotifyOfPropertyChange(() => SummeBelegPositionen);
                    isDirty = true;
                }
            }
        }

        private Int16? _IstUmsatzsteuer;
        public Int16? IstUmsatzsteuer
        {
            get
            {
                return _IstUmsatzsteuer;
            }
            set
            {
                if (value != _IstUmsatzsteuer)
                {
                    _IstUmsatzsteuer = value;
                    HandleMWST_Change();
                    NotifyOfPropertyChange(() => IstUmsatzsteuer);
                    isDirty = true;
                }
            }
        }

        private Decimal _Umsatzsteuersatz;
        public Decimal Umsatzsteuersatz
        {
            get
            {
                return _Umsatzsteuersatz;
            }
            set
            {
                if (value != _Umsatzsteuersatz)
                {
                    _Umsatzsteuersatz = value;
                    NotifyOfPropertyChange(() => Umsatzsteuersatz);
                    isDirty = true;
                }
            }
        }

        private Decimal _Belegsumme;
        public Decimal Belegsumme
        {
            get
            {
                return _Belegsumme;
            }
            set
            {
                if (value != _Belegsumme)
                {
                    _Belegsumme = value;
                    NotifyOfPropertyChange(() => Belegsumme);
                    isDirty = true;
                }
            }
        }

        private Decimal _BelegsummeFix;
        public Decimal BelegsummeFix
        {
            get
            {
                return _BelegsummeFix;
            }
            set
            {
                if (value != _BelegsummeFix)
                {
                    _BelegsummeFix = value;
                    NotifyOfPropertyChange(() => BelegsummeFix);
                    isDirty = true;
                }
            }
        }

        private string _Signatur1;
        public string Signatur1
        {
            get
            {
                return _Signatur1;
            }
            set
            {
                if (value != _Signatur1)
                {
                    _Signatur1 = value;
                    SelectedSignaturen1 = db.C_Localizing.Where(n => n.Objektname == 3 && n.Wert == value).SingleOrDefault();
                    NotifyOfPropertyChange(() => Signatur1);
                    isDirty = true;
                }
            }
        }


        private string _Signatur2;
        public string Signatur2
        {
            get
            {
                return _Signatur2;
            }
            set
            {
                if (value != _Signatur2)
                {
                    _Signatur2 = value;

                    SelectedSignaturen2 = db.C_Localizing.Where(n => n.Objektname == 3 && n.Wert == value).SingleOrDefault();
                    NotifyOfPropertyChange(() => Signatur2);
                    isDirty = true;
                }
            }
        }



        private int? _id_Quellbeleg;
        public int? id_Quellbeleg
        {
            get
            {
                return _id_Quellbeleg;
            }
            set
            {
                if (value != _id_Quellbeleg)
                {
                    _id_Quellbeleg = value;
                    NotifyOfPropertyChange(() => id_Quellbeleg);
                    isDirty = true;
                }
            }
        }

        private int? _id_Vorgang;
        public int? id_Vorgang
        {
            get
            {
                return _id_Vorgang;
            }
            set
            {
                if (value != _id_Vorgang)
                {
                    _id_Vorgang = value;
                    NotifyOfPropertyChange(() => id_Vorgang);
                    isDirty = true;
                }
            }
        }


        private Int16? _istInitialBeleg;
        public Int16? istInitialBeleg
        {
            get
            {
                return _istInitialBeleg;
            }
            set
            {
                if (value != _istInitialBeleg)
                {
                    _istInitialBeleg = value;
                    NotifyOfPropertyChange(() => istInitialBeleg);
                    isDirty = true;
                }
            }
        }

        private string _ZahlungsbedingungText;
        public string ZahlungsbedingungText
        {
            get
            {
                return _ZahlungsbedingungText;
            }
            set
            {
                if (value != _ZahlungsbedingungText)
                {
                    _ZahlungsbedingungText = value;
                    NotifyOfPropertyChange(() => ZahlungsbedingungText);
                    isDirty = true;
                }
            }
        }

        private int? _id_Zahlungsbedingung;
        public int? id_Zahlungsbedingung
        {
            get
            {
                return _id_Zahlungsbedingung;
            }
            set
            {
                if (value != _id_Zahlungsbedingung)
                {
                    _id_Zahlungsbedingung = value;
                    NotifyOfPropertyChange(() => id_Zahlungsbedingung);
                    isDirty = true;
                }
            }
        }




        #endregion

        #region "Constructors"
        public SI_BelegeViewModel()
        {

        }
        //public SI_BelegeViewModel(int id)
        //{


        //    InitModel(id);
        //}

        //public SI_BelegeViewModel(int id, System.Windows.Input.Cursor cursor)
        //{

        //    ThisCursor = cursor;
        //    InitModel(id);
        //}

        //public SI_BelegeViewModel(int id, Dispatcher dispatcher)
        //{
        //    // Obsolet
        //    ThisDispatcher = dispatcher;

        //    InitModel(id);
        //}

        public SI_BelegeViewModel(int id, Dispatcher dispatcher, System.Windows.Input.Cursor cursor)
        {
            // Start neuer Beleg oder Öffnen vorhandenen Beleg.
            isButtonTestVisible = CommonTools.Tools.ConfigEntry<int>.GetConfigEntry("isButtonTestVisible", "1", "Button Test ein und ausblenden.") == 0 ? Visibility.Hidden : Visibility.Visible;
            IstBelegVerarbeitet = id > 0 ? true : false;

            ThisCursor = cursor;
            ThisDispatcher = dispatcher;
            isProjektVisible = true;
            IstVorgangBeleg = false;

            InitModel(id);
        }

        public SI_BelegeViewModel(int id, Dispatcher dispatcher, System.Windows.Input.Cursor cursor, int SourceBelegID, StammBelegarten neueBelegart)
        {
            isButtonTestVisible = CommonTools.Tools.ConfigEntry<int>.GetConfigEntry("isButtonTestVisible", "1", "Button Test ein und ausblenden.") == 0 ? Visibility.Hidden : Visibility.Visible;
            IstBelegVerarbeitet = id > 0 ? true : false;
            //Übernahme Beleg in neue Belegart.
            ThisCursor = cursor;
            ThisDispatcher = dispatcher;
            this.SourceBelegID = SourceBelegID;

            InitModel(id);

            IstVorgangBeleg = true;

            SI_Belege b = db.SI_Belege.Where(n => n.id == SourceBelegID).SingleOrDefault();
            SourceBeleg = b;

            if (b != null)
            {
                SelectedBelegarten = neueBelegart;
                if (b.id_Projekt.HasValue)
                {

                    // projekt po = db.projekte.Where(n => n.id == b.id_Projekt).SingleOrDefault();

                    var query = from p in db.projekte
                                where p.id == b.id_Projekt
                                select new Models.ProjekteSource
                                {
                                    projektnummer = p.projektnummer,
                                    type = p.type,
                                    firmenname = p.firmenname,
                                    kundenname = p.kundenname,
                                    projekt_schiff = p.projekt_schiff,
                                    id = p.id

                                };

                    // Rückgängig machen für Projekte
                    // Projekte = new ObservableCollection<projekt>(db.projekte.Where(n => n.projektnummer.StartsWith(po.projektnummer)));

                    SelectedProjekte = query.SingleOrDefault();

                    Projekte = new ObservableCollection<Models.ProjekteSource>(query);

                    if (Projekte == null)
                    {
                        Console.WriteLine("Oh Yeah");
                    }

                }
                else
                {
                    //SelectedFirmen = db.firmen.Where(n => n.id == b.id_Firma).SingleOrDefault();
                    SelectedFirmen = GetFirmensourceByID((int)b.id_Firma);
                }


                BelegePositionen = new ObservableCollection<SI_BelegePositionen>();
                foreach (var item in b.SI_BelegePositionen)
                {

                    var newPos = new SI_BelegePositionen();
                    newPos.id_Artikel = item.id_Artikel;
                    newPos.Menge = item.Menge;
                    newPos.created = DateTime.Now;
                    newPos.PreisVorRabatt = item.PreisVorRabatt.HasValue ? item.PreisVorRabatt : newPos.PreisVorRabatt;
                    newPos.Rabatt = item.Rabatt ?? newPos.Rabatt;
                    newPos.Preis = item.Preis ?? newPos.Preis;

                    newPos.Endpreis = item.Endpreis ?? newPos.Endpreis;
                    newPos.istGebucht = 0;

                    newPos.Einheit = item.Einheit ?? newPos.Einheit;
                    newPos.Endpreis = item.Endpreis ?? newPos.Endpreis;


                    BelegePositionen.Add(newPos);
                }


            }

            isDirty = true;

        }

        #endregion

        #region "Commands"
        public void btnSaveA()
        {
            DoSaveChanges();

        }

        public void btnAddPosition(Window w)
        {
            var pos = CreateBelegePosition();
            pos.Belegart = SelectedBelegarten;
            pos.Kalkulationstabelle = SelectedKalkulation;
            pos.SI_Belege = Beleg;
            pos.istGebucht = 0;
            pos.created = DateTime.Now;

            addPosition(pos);

            var view = (views.EditBelege)w;

            view.SI_BelegePositionen_ListView.ScrollIntoView(pos);


        }

        public void btnImport()
        {
            string dir = string.Empty;
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dir = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("ImportFolder_ScannerData", @"E:\test\Import");
                dlg.InitialDirectory = dir;
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents (.txt)|*.txt";

                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    string filename = dlg.FileName;
                    ImportFileName = filename;
                    string res = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("ScannerImportColumns", "2,3", "Spalten der Exportdatei die eingelesen werden.");
                    string[] cols = res.Split(',');

                    importScannerData(filename, int.Parse(cols[0]), int.Parse(cols[1]));

                }
            }
            catch (Exception ex)
            {
                ErrorMethods.HandleStandardError(ex, dir);
                // MessageBox.Show(ex.Message);
            }

        }

        public void btnPrint()
        {
            //if (istGebucht == 1)
            //{

            //}

            OpenBelegDruck(id);
        }

        public void btnClose(views.EditBelege window)
        {
            window.Close();
        }

        public void btnTest()
        {
            isVisible = !isVisible;
            ZusatzInfo += "X";
            Straße += "S";
        }

        public void cmdNew()
        {

            //if (isDirty)
            //{
            //    var res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel, System.Windows.MessageBoxImage.Warning);
            //    if (res == System.Windows.MessageBoxResult.Yes)
            //    {



            //    }
            //}

            //db.Dispose();
            //db = null;
            //Beleg = null;

            //InitModel(0);




        }

        public void btnUebernahme()
        {
            BelegUebernamhe("re");
        }

        public void AddZusatzzeile()
        {
            AddZusatzzeile(enumZusatzzeileRefType.NeuePosition);
        }

        public void AddZusatzzeileZwischensumme()
        {
            AddZusatzzeile(enumZusatzzeileRefType.Zwischensumme);
            DoSortZusatzzeilen = false;
            RecalcBeleg();
        }


        public void AddZusatzzeile(enumZusatzzeileRefType RefType = enumZusatzzeileRefType.NeuePosition)
        {
            var zz = CreateZusatzzeile();
            zz.created = DateTime.Now;
            zz.Pos = Zusatzzeilen.Count + 1;
            zz.idx = zz.Pos * 10;
            zz.Wert = 0;
            zz.Text = RefType.ToString();
            zz.ReferenzTyp = RefType.ToString();


            Zusatzzeilen.Add(zz);

            Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>(Zusatzzeilen.OrderBy(n => n.idx));

        }

        public void CmdImportKakulation()
        {
            if (SelectedKalkulation != null)
            {
                ImportKalkulation(SelectedKalkulation);
            }

        }

        public void CmdOpenKakulation()
        {
            if (SelectedKalkulation != null)
            {
                views.Kalkulation k = new views.Kalkulation(SelectedKalkulation.id);
                k.ShowDialog();
            }

        }


        public void RecalcThis()
        {
            RecalcBeleg();
        }

        public void RunTest()
        {


            //Console.WriteLine("Start Measuring . . . . . . . . ");
            //System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
            //w.Start();
            //var ba = db.StammBelegarten;
            //Console.WriteLine(w.ElapsedMilliseconds.ToString());

            //w.Reset();
            //w.Start();
            //var bas = from b in db.StammBelegarten
            //          select new Models.BelegartenSource
            //          {
            //              id = b.id,
            //              Belegart = b.Belegart
            //          };

            //Console.WriteLine(w.ElapsedMilliseconds.ToString());


            // DataTransform.SetBelegePositionenEndpreis();

            //DataTransform.CreateVorgaengeForOldBelege();


            //if (id_Projekt.HasValue && id_Projekt != 0)
            //{
            //    projekt po = db.projekte.Where(n => n.id == id_Projekt).SingleOrDefault();
            //  //  Projekte = new ObservableCollection<projekt>(db.projekte.Where(n => n.projektnummer.StartsWith(po.projektnummer)));

            //    SelectedProjekte = po;
            //}

            // Belegnummer = CommonTools.Tools.Belegnummern.GetBelegnummerProjekt(Belegart, (int)id_Projekt);





            //var disp = System.Windows.Threading.Dispatcher.CurrentDispatcher;
            //var Cur = System.Windows.Input.Cursors.Wait;
            //var BelegeVM = new ProjektDB.ViewModels.SI_BelegeViewModel(0, disp, Cur);
            //BelegeVM.SelectedBelegarten = db.StammBelegarten.Where(n => n.id == "re").SingleOrDefault();
            //var p = db.projekte.Where(n => n.projektnummer == "140589").SingleOrDefault();
            ////var query = from p in db.projekte
            ////            where p.projektnummer == "140589"
            ////            select p;

            //var queryX = from po in db.projekte
            //             where po.projektnummer == "140589"
            //             select new ProjektDB.Models.ProjekteSource
            //             {
            //                 projektnummer = p.projektnummer,
            //                 type = p.type,
            //                 firmenname = p.firmenname,
            //                 kundenname = p.kundenname,
            //                 projekt_schiff = p.projekt_schiff,
            //                 id = p.id

            //             };

            //BelegeVM.SelectedProjekte = queryX.SingleOrDefault();
            //BelegeVM.SelectedKalkulation = db.kalkulationstabellen.Where(n => n.inhalt == "Test140589-29.07.2014").SingleOrDefault();
            //BelegeVM.ImportKalkulation(BelegeVM.SelectedKalkulation);
            //BelegeVM.SelectedFirmenAdressen = BelegeVM.FirmenAdressen[0];

            //BelegeVM.RecalcThis();
            //var bPos = BelegeVM.BelegePositionen[0];
            //var zz = BelegeVM.Zusatzzeilen[0];

            //Mapper.CreateMap<SI_BelegeViewModel, SI_Belege>()
            //           .ForMember(dest => dest.SI_BelegePositionen, opt => opt.Ignore())
            //           .ForMember(dest => dest.SI_BelegeZusatzzeilen, opt => opt.Ignore())
            //           .ForMember(dest => dest.SI_BelegeTextbausteine, opt => opt.Ignore())
            //           .ForSourceMember(dest => dest.ListboxBelegeTextbausteineVM, opt => opt.Ignore())
            //           .ForSourceMember(dest => dest.isFatalError, opt => opt.Ignore())
            //           .ForSourceMember(dest => dest.BackgroundState, opt => opt.Ignore())
            //           ;
            //var b = Mapper.Map<SI_BelegeViewModel, SI_Belege>(BelegeVM);
            //Console.WriteLine(b.Belegart);

            //return;


            //ReloadLagerListenFullViewsource();
            //return;


            //decimal v = 12.1255m;
            //var v1 = Math.Round(v, 2);
            //Console.WriteLine(v1);
            //v = 12.1212m;
            //v1 = Math.Round(v, 2);
            //Console.WriteLine(v1);


            //StringBuilder sb = new StringBuilder();

            //try
            //{
            //    // addKalulationsTabellePos("000217", 5, 51.26);
            //    //addKalulationsTabellePos("016272", 4, 26.11);
            //    addKalulationsTabellePos("0837", 5, 51.26, "", "", 0);
            //    //addKalulationsTabellePos("003150", 3, 151.19);




            //}
            //catch (ArgumentOutOfRangeException ex)
            //{

            //    sb.AppendLine("DieBelegposition konnte nicht hinzugefügt werden.");
            //    sb.AppendLine("Ursache ist : ");
            //    sb.AppendLine(ex.Message);
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            //if (sb.ToString() != string.Empty)
            //{
            //    CommonTools.Tools.UserMessage.NotifyUser(sb.ToString());
            //}


            //return;

            //Console.WriteLine("Start Measuring . . . . . . . . ");
            //System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
            //w.Start();

            //var q2 = from p in db.projekte
            //         select new
            //         {
            //             x = p.projektnummer
            //             //a=p.projekt_verlauf,
            //             //b=p.projektAggregat,
            //             //c=p.ProjektAuftrag,
            //             //d=p.projektService,
            //             //e=p.projekt_reklamation_rechnung,
            //             //f=p.projekt_rechnunglieferant

            //         };

            //var xy = q2.ToList();

            //Console.WriteLine("Projected Short " + w.ElapsedMilliseconds.ToString());
            //w.Restart();

            //var query = from p in db.projekte
            //            select new Models.ProjekteSource
            //            {
            //                projektnummer = p.projektnummer,
            //                type = p.type,
            //                firmenname = p.firmenname,
            //                kundenname = p.kundenname,
            //                projekt_schiff = p.projekt_schiff,
            //                id = p.id

            //            };
            //var res1 = query.ToList();

            //Console.WriteLine("Projected " + w.ElapsedMilliseconds.ToString());


            //w.Restart();

            //var q1 = from p in db.projekte
            //         select p;
            //var res = q1.ToList();
            //Console.WriteLine("Entire " + w.ElapsedMilliseconds.ToString());





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



        public void ProjektefcbFilterChanged(DataTypes.FilteredComboBoxChangedEventArgs e)
        {
            // this.Projekte = new ObservableCollection<projekt>(db.projekte.Where(n => n.projektnummer.StartsWith(e.filter)));
        }

        public void FirmenfcbFilterChanged(DataTypes.FilteredComboBoxChangedEventArgs e)
        {

            var Query = from f in db.firmen
                        where f.name.Contains(e.filter) && f.IstKunde == 1
                        select new Models.FirmenSource
                        {
                            name = f.name
                        };
            this.Firmen = new ObservableCollection<Models.FirmenSource>(Query);

        }

        public void DeletePosition(views.EditBelege window)
        {
            var grid = window.SI_BelegePositionen_ListView;
            var pos = (SI_BelegePositionen)grid.SelectedItem;

            if (pos != null)
            {
                if (pos.istGebucht == 1 || istGebucht == 1)
                {
                    MessageBox.Show(string.Format("Position {0} ist schon gebucht. Löschen nicht möglich.", pos.Artikelnummer));

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


                        BelegePositionen.Remove(pos);

                        RenumberPositions();
                    }


                }
            }


        }


        public void MoveZusatzzeileUp(views.EditBelege window)
        {
            var grid = (C1.WPF.DataGrid.C1DataGrid)window.GridZusatzzeilen;
            var pos = (SI_BelegeZusatzzeilen)grid.SelectedItem;

            if (pos.Pos > 1)
            {
                var pos1 = Zusatzzeilen.Where(n => n.Pos == pos.Pos - 1).SingleOrDefault();
                int p = (int)pos.Pos;
                pos.Pos = pos1.Pos;
                pos1.Pos = p;
                Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>(Zusatzzeilen.OrderBy(n => n.Pos));
                DoSortZusatzzeilen = false;
                RecalcBeleg();

            }
        }

        public void MoveZusatzzeileDown(views.EditBelege window)
        {
            var grid = (C1.WPF.DataGrid.C1DataGrid)window.GridZusatzzeilen;
            var pos = (SI_BelegeZusatzzeilen)grid.SelectedItem;

            if (pos.Pos < Zusatzzeilen.Count)
            {
                var pos1 = Zusatzzeilen.Where(n => n.Pos == pos.Pos + 1).SingleOrDefault();
                int p = (int)pos.Pos;
                pos.Pos = pos1.Pos;
                pos1.Pos = p;
                Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>(Zusatzzeilen.OrderBy(n => n.Pos));
                DoSortZusatzzeilen = false;
                RecalcBeleg();
            }
        }


        public void DeleteZusatzZeile(views.EditBelege window)
        {
            var grid = (C1.WPF.DataGrid.C1DataGrid)window.GridZusatzzeilen;
            var pos = (SI_BelegeZusatzzeilen)grid.SelectedItem;

            if (MessageBox.Show("Zeile wirlich löschen?", "", MessageBoxButton.YesNo) != MessageBoxResult.No)
            {
                Zusatzzeilen.Remove(pos);
            }
        }

        #endregion

        #region "Functions"

        private void LoadZahlungsbedingungen()
        {
            if (istGebucht == 1)
            {
                return;
            }

            ZahlungsbedingungText = string.Empty;
            id_Zahlungsbedingung = null;


            if (SelectedBelegarten != null && SelectedBelegarten.ZahlungsbedingungenSetzen == 1)
            {

                if (SelectedFirmen != null)
                {
                    id_Zahlungsbedingung = SelectedFirmen.Zahlungsbedingungen;
                    if (id_Zahlungsbedingung.HasValue && id_Zahlungsbedingung != 0 && id_Sprache.HasValue && id_Sprache != 0)
                    {
                        var zbs = db.SI_Zahlungsbedingungen_Sprache.Where(n => n.id_Sprache == id_Sprache && n.id_Zahlungsbedingung == id_Zahlungsbedingung);
                        if (zbs.Any())
                        {
                            ZahlungsbedingungText = zbs.SingleOrDefault().Text;
                        }
                    }
                }

            }

            //else
            //{
            //    ZahlungsbedingungText = string.Empty;
            //    id_Zahlungsbedingung = null;
            //}
        }

        /// <summary>
        /// Aufruf aus id-Projekt Set
        /// Setzen der abhängigen Daten nach wechsel der ProjektID
        /// </summary>
        private void SetKundeFirmaFromProject()
        {
            //if (IstBelegVerarbeitet)
            //{
            //    return;
            //}

            try
            {
                var project = db.projekte.Where(n => n.id == id_Projekt).SingleOrDefault();
                if (EKVK.ToLower() == "ek")
                {

                    kundenname = project.kundenname;
                    firmenname = project.firmenname;
                    //id_Firma = db.firmen.Where(n => n.KdNr == project.FirmenNr && n.IstKunde == 1).SingleOrDefault().id;
                    //SelectedFirmen = db.firmen.Where(n => n.KdNr == project.FirmenNr && n.IstKunde == 1).SingleOrDefault();
                    SelectedFirmen = GetFirmensourceByKdNr(project.FirmenNr);

                }
                else
                {
                    kundenname = project.kundenname;
                    firmenname = project.firmenname;

                    //id_Firma = db.firmen.Where(n => n.KdNr == project.KdNr && n.IstKunde == 1).SingleOrDefault().id;
                    //SelectedFirmen = db.firmen.Where(n => n.KdNr == project.KdNr && n.IstKunde == 1).SingleOrDefault();
                    SelectedFirmen = GetFirmensourceByKdNr(project.KdNr);

                }

                Projektnummer = project.projektnummer;

                type = project.type;
                //Belegnummer = CommonTools.Tools.Belegnummern.GetBelegnummerProjekt(Belegart, (int)id_Projekt);
                Belegnummer = CommonTools.Tools.Belegnummern.GetBelegnummerProjekt(SelectedBelegarten.id, (int)id_Projekt);
                projekt_schiff = project.projekt_schiff;
                ManageUmsatzSteuer();
                isFirmaEnabled = false;



            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler SetKundeFirmaFromProject - SI_BelegeViewModel", true);
                isFatalError = true;
            }






            try
            {
                if (Projektnummer != null)
                {
                    Kalkulation = new ObservableCollection<kalkulationstabelle>(db.kalkulationstabellen.Where(n => n.projektnummer == this.Projektnummer));
                }

            }
            catch (Exception)
            {


            }
        }

        private void ManageUmsatzSteuer()
        {

            if (!id_Projekt.HasValue || id_Projekt == 0)
            {
                return;
            }


            try
            {
                var project = db.projekte.Where(n => n.id == id_Projekt).SingleOrDefault();
                isUmsatzsteuerChoiseVisible = project.istUmsatzsteuerbefreit == 1 ? false : true;

                id_Sprache = project.id_Sprache ?? 1;
                IstUmsatzsteuer = project.istUmsatzsteuerbefreit == 1 ? (short)0 : (short)1;



            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler ManageUmsatzSteuer - SI_BelegeViewModel", true);
            }

        }

        private void InitBelegart()
        {
            //if (Belegart != null)
            if (SelectedBelegarten != null && SelectedBelegarten.id != null)
            {
                if (BelegePositionen != null && BelegePositionen.Count > 0)
                {
                    if (MessageBox.Show("Bei Belegartänderung werden alle vorhandenen Positionen gelöscht. Belegart dennoch ändern?", "Bitte bestätigen.", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        BelegePositionen.Clear();
                    }
                }
                //var ba = db.StammBelegarten.Where(id => id.id == Belegart).SingleOrDefault();
                var ba = db.StammBelegarten.Where(id => id.id == SelectedBelegarten.id).SingleOrDefault();
                if (ba != null)
                {
                    BelegartName = ba.Belegart;
                    BelegartenUebernahme = new ObservableCollection<view_BelegartenBelegUebernahme>(db.view_BelegartenBelegUebernahme.Where(n => n.id_BelegArt == ba.id));
                    isPreiseAnzeigen = ba.PreiseAnzeigen == 1 ? true : false;
                    ZusatzzeilenVisible = isPreiseAnzeigen ? Visibility.Visible : Visibility.Collapsed;
                    BelegePositionenHeight = isPreiseAnzeigen ? 367 : 480;
                    BeziehungPartner = ba.BeziehungPartner;
                    EKVK = ba.EKVK;
                    // NotifyOfPropertyChange(() => Belegart);
                    NotifyOfPropertyChange(() => SelectedBelegarten);
                    NotifyOfPropertyChange(() => BeziehungPartner);

                    SetSateAddPosition();


                }

                LoadZahlungsbedingungen();
            }
        }
        private void HandleMWST_Change()
        {

            if (id > 0) return;


            if (Zusatzzeilen == null)
            {
                Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>();
                Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>(Zusatzzeilen.OrderBy(n => n.idx));
            }

            if (IstUmsatzsteuer == 1)
            {

                AddZusatzzeilenBelegart("MWST");
            }
            else
            {

                AddZusatzzeilenBelegart("Ustbefreit");
                //ClearZusatzzeilen();

                //var query = from b in db.SI_BelegartenZusatzzeilen
                //            join p in db.StammZusatzzeilen on b.id_Zusartzeile equals p.id
                //            where p.Typ == "Ustbefreit" && b.id_Sprache == id_Sprache && b.id_Belegart == Belegart
                //            select new
                //            {
                //                typ = p.Typ,
                //                Text = p.Text,
                //                pro = p.Wert


                //            };

                //if (query.Any())
                //{
                //    foreach (var item in query)
                //    {
                //        var buf = new SI_BelegeZusatzzeilen();
                //      //  buf.SI_Belege = Beleg;
                //        buf.Text = item.Text;
                //        buf.Prozent = item.pro;
                //        buf.idx = 99;
                //        buf.Typ = item.typ;
                //        buf.created = DateTime.Now;

                //        Zusatzzeilen.Add(buf);

                //    }

                //    Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>(Zusatzzeilen.OrderBy(n => n.idx));

                //}
            }



        }

        private void AddZusatzzeilenBelegart(string Type)
        {
            ClearZusatzzeilen();

            //var xx = Belegart;
            //Console.WriteLine(xx);

            var query = from b in db.SI_BelegartenZusatzzeilen
                        join p in db.StammZusatzzeilen on b.id_Zusartzeile equals p.id
                        where p.Typ == Type && b.id_Sprache == id_Sprache && b.id_Belegart == Belegart
                        select new
                        {
                            typ = p.Typ,
                            Text = p.Text,
                            pro = p.Wert


                        };

            if (query.Any())
            {
                foreach (var item in query)
                {
                    var buf = CreateZusatzzeile();
                    //  buf.SI_Belege = Beleg;
                    buf.Text = item.Text;
                    buf.Prozent = item.pro;
                    buf.idx = 990;
                    buf.ReferenzTyp = enumZusatzzeileRefType.Ust.ToString();
                    buf.Typ = item.typ;
                    buf.created = DateTime.Now;

                    Zusatzzeilen.Add(buf);

                }

                Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>(Zusatzzeilen.OrderBy(n => n.idx));
                RecalcBeleg();
            }
        }

        private void VisibilityFirmaProjekt()
        {
            if (EKVK != null)
            {
                switch (EKVK.ToLower().Trim())
                {
                    case "vk":
                        {
                            isProjektVisible = true;
                            isProjektEnabled = isEnabled;
                            isButtonAddPositionEnabled = isEnabled;
                            isFirmaVisible = true;
                            isFirmaEnabled = isEnabled;
                            id_Quelllager = 4;
                            isQuellLagerEnabled = false;
                            isQuelllagerVisible = false;
                            //  id_Ziellager = 1;
                            isZielLagerEnabled = isEnabled;
                            ContentZiellager = "Lager :";
                            isAdressenVisible = false;
                            isEkVisible = false;
                            break;
                        }
                    case "ek":
                        {
                            isProjektVisible = true;
                            isProjektEnabled = isEnabled;
                            isButtonAddPositionEnabled = isEnabled;
                            isFirmaVisible = true;
                            isFirmaEnabled = isEnabled;
                            //isProjektEnabled = false;
                            //isProjektVisible = false;
                            // id_Projekt = null;
                            id_Quelllager = 4;
                            isQuellLagerEnabled = false;
                            isQuelllagerVisible = false;
                            //   id_Ziellager = 1;
                            isZielLagerEnabled = isEnabled;
                            ContentZiellager = "Lager :";
                            isAdressenVisible = false;
                            isEkVisible = true;
                            break;
                        }
                    case "rewe":
                        {
                            isFirmaEnabled = isEnabled;
                            isFirmaVisible = true;
                            isProjektEnabled = isEnabled;
                            isButtonAddPositionEnabled = isEnabled;
                            isProjektVisible = true;
                            //id_Projekt = null;
                            id_Quelllager = 4;
                            isQuellLagerEnabled = false;
                            isQuelllagerVisible = false;
                            //  id_Ziellager = 1;
                            isZielLagerEnabled = false;
                            ContentZiellager = "Lager :";
                            isAdressenVisible = true;
                            isEkVisible = false;
                            break;
                        }
                    case "intern":
                        {
                            isFirmaEnabled = false;
                            isFirmaVisible = false;
                            isProjektEnabled = false;
                            isButtonAddPositionEnabled = isEnabled;
                            isProjektVisible = false;
                            id_Projekt = null;
                            // id_Firma = null;
                            SelectedFirmen = null;
                            id_Quelllager = 4;
                            isQuellLagerEnabled = true;
                            isQuelllagerVisible = true;
                            //   id_Ziellager = 1;
                            isZielLagerEnabled = isEnabled;
                            ContentZiellager = "Ziellager :";
                            isEkVisible = false;

                            break;
                        }

                    case "inv":
                        {
                            isFirmaEnabled = false;
                            isFirmaVisible = false;
                            isProjektEnabled = false;
                            isProjektVisible = false;
                            id_Projekt = null;
                            // id_Firma = null;
                            SelectedFirmen = null;
                            isQuellLagerEnabled = false;
                            isQuelllagerVisible = false;
                            id_Quelllager = 4;
                            id_Ziellager = 1;
                            isZielLagerEnabled = isEnabled;
                            ContentZiellager = "Lager :";
                            isEkVisible = false;

                            break;
                        }


                    default:
                        break;
                }

                NotifyOfPropertyChange(() => isFirmaVisible);
                NotifyOfPropertyChange(() => isProjektVisible);
                NotifyOfPropertyChange(() => isQuellLagerEnabled);
                NotifyOfPropertyChange(() => isZielLagerEnabled);
            }

        }


        void BelegUebernamhe(string Belegart)
        {
            //if (id != 0 && id != null)
            //{
            //    if (!isDirty)
            //    {
            //        db.Dispose();
            //        db = new SteinbachEntities();
            //        int BelID = id;
            //        Beleg = new SI_Belege();
            //        db.AddToSI_Belege(Beleg);

            //        ListboxBelegeTextbausteineVM = new CheckListBoxes.ListboxBelegeTextbausteineViewModel(db, this);
            //        ListboxBelegeTextbausteineVM.DataChanged += ListboxBelegeTextbausteineVM_DataChanged;
            //        ti.Start();

            //        isLoaded = false;
            //        isVisible = true;

            //        //SI_Belege bel = (SI_Belege)Beleg.Clone();
            //        //bel.Bemerkung = "Clone von " + Beleg.id;

            //        Mapper.CreateMap<SI_Belege, SI_BelegeViewModel>();
            //        Mapper.Map<SI_Belege, SI_BelegeViewModel>(Beleg, this);

            //        Belegnummer = "Neu";

            //        created = DateTime.Now;
            //        Belegdatum = DateTime.Now;
            //        id_Ziellager = 1;
            //        id_Quelllager = 4;
            //        istGebucht = 0;
            //        id_Sprache = 1;
            //        id_user = DAL.Session.User.id;

            //        Lagerorte = new ObservableCollection<StammLagerorte>(db.StammLagerorte.Where(n => n.aktiv == 1));
            //        LagerorteZiel = new ObservableCollection<StammLagerorte>(Lagerorte.Where(n => n.ShowInZiellager == 1));

            //        Bewegungsarten = new ObservableCollection<StammBewegungsarten>(db.StammBewegungsarten);
            //        Belegarten = new ObservableCollection<StammBelegarten>(db.StammBelegarten);
            //        string buf = DateTime.Now.Year.ToString().Substring(2);
            //        Projekte = new ObservableCollection<projekt>(db.projekte.Where(n => n.projektnummer.StartsWith(buf)));
            //        Lagerlisten = new ObservableCollection<lagerliste>(db.lagerlisten);    //db.lagerlisten.Where(n => n.artikelnr == "0"
            //        LagerlistenFull = new ObservableCollection<lagerliste>(db.lagerlisten);
            //        Sprachen = new ObservableCollection<AuswahlEintraege>(db.AuswahlEintraege.Where(n => n.Gruppe == "TypSprache"));
            //        Firmen = new ObservableCollection<firma>(db.firmen.Where(n => n.IstKunde == 1));


            //        //this.Belegart = Belegart;
            //        //this.Belegnummer += " Clone";
            //        //id = 0;
            //        //istGebucht = 0;

            //        BelegePositionen.Clear();
            //        var bel = db.SI_Belege.Where(n => n.id == BelID).SingleOrDefault();
            //        BelegePositionen = new ObservableCollection<SI_BelegePositionen>();
            //        foreach (var item in bel.SI_BelegePositionen)
            //        {
            //            var pos = new SI_BelegePositionen();
            //            pos.id_Artikel = item.id_Artikel;
            //            pos.Menge = item.Menge;
            //            BelegePositionen.Add(pos);

            //        }

            //        //bel.SI_BelegePositionen.Clear();
            //        //Beleg = bel;
            //        //bel.Belegart = Belegart;

            //        //Beleg = new SI_Belege();
            //        //db.AddToSI_Belege(bel);

            //        InitBelegart();
            //        isLoaded = true;

            //    }
            //    else
            //    {
            //        UserMessage.NotifyUser("Es sind Belegänderungen vorhanden.");
            //    }
            //}




        }

        public void InitModel(int id)
        {


            BelegePositionenHeight = 367;
            isFatalError = true;
            SetCursor(false);
            ti = new DispatcherTimer();
            ti.Tick += ti_Tick;
            ti.Interval = new TimeSpan(0, 0, 0, 0, 500);


            System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
            w.Start();
            isFatalError = false;
            isLoaded = false;
            isVisible = true;
            db = new SteinbachEntities();
            if (id == 0)
            {
                InitModelNewBeleg();
            }
            else
            {
                InitModelLoadBeleg(id);


            }

            Trace.WriteLine("InitBeleg : " + w.ElapsedMilliseconds);



            MapBelegToThis();

            Fill_ItemsSourceColllections(w);


            Trace.WriteLine("Nach InitCollections : " + w.ElapsedMilliseconds);
            SetCursor(true);



            if (id == 0)
            {
                Belegnummer = "Neu";
                //Bemerkung = "Keine";
                created = DateTime.Now;
                Belegdatum = DateTime.Now;
                id_Ziellager = 1;
                id_Quelllager = 4;
                istGebucht = 0;
                id_Sprache = 1;

                if (DAL.Session.User != null)   // Unit Test
                {
                    id_user = DAL.Session.User.id;
                }


                ContentZiellager = "Lager : ";

                Trace.WriteLine("Nach Laden Neubeleg : " + w.ElapsedMilliseconds);



            }
            else
            {

                var task1 = System.Threading.Tasks.Task.Factory.StartNew(() =>
                 {

                     ThisDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (new System.Action(() =>
                     {

                         BelegePositionen = new ObservableCollection<SI_BelegePositionen>(db.SI_BelegePositionen.Where(n => n.id_Beleg == id));
                         Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>(db.SI_BelegeZusatzzeilen.Where(n => n.id_Beleg == id).OrderBy(n => n.idx));
                         isDirty = false;

                     })));

                 });

                Trace.WriteLine("Nach Laden AltBeleg : " + w.ElapsedMilliseconds);

            }

            isDirty = false;
            InitBelegart();
            isLoaded = true;

            w.Stop();
            w = null;

            long res = CommonTools.GlobalStopwatch.GetEllapsedMilliseconds();
            CommonTools.GlobalStopwatch.StopWatch();
            DAL.Tools.LoggingTool.addDatabaseLogging("0", 0, DAL.Tools.LoggingTool.LogState.medium, "Ladedauer SI Beleg", res.ToString());
            isButtonAddPositionEnabled = false;
            // MessageBox.Show(res.ToString());

        }

        private void Fill_ItemsSourceColllections(System.Diagnostics.Stopwatch w)
        {
            Trace.WriteLine("Nach Automapper : " + w.ElapsedMilliseconds);

            var task = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {

                ThisDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (new System.Action(() =>
                {

                    Lagerorte = new ObservableCollection<StammLagerorte>(db.StammLagerorte.Where(n => n.aktiv == 1));
                    LagerorteZiel = new ObservableCollection<StammLagerorte>(Lagerorte.Where(n => n.ShowInZiellager == 1));
                    Sets = new ObservableCollection<StammLagerorte>(db.StammLagerorte.Where(n => n.istSet == 1));
                    // LagerlisteFullViewSource = new ObservableCollection<lagerliste>(db.lagerlisten);

                    Bewegungsarten = new ObservableCollection<StammBewegungsarten>(db.StammBewegungsarten);
                    Belegarten = new ObservableCollection<StammBelegarten>(db.StammBelegarten);
                    string buf = DateTime.Now.Year.ToString().Substring(2);
                    //if (Projekte == null)
                    //{
                    //    Projekte = new ObservableCollection<projekt>(db.projekte.Where(n => n.id == Beleg.id_Projekt));
                    //}

                    if (Projekte == null)
                    {
                        var query = from p in db.projekte
                                    select new Models.ProjekteSource
                                    {
                                        projektnummer = p.projektnummer,
                                        type = p.type,
                                        firmenname = p.firmenname,
                                        kundenname = p.kundenname,
                                        projekt_schiff = p.projekt_schiff,
                                        id = p.id

                                    };

                        Projekte = new ObservableCollection<Models.ProjekteSource>(query);
                    }




                    //Lagerlisten = new ObservableCollection<lagerliste>(db.lagerlisten.Where(n => n.artikelnr == "0"));    //db.lagerlisten.Where(n => n.artikelnr == "0"
                    //LagerlistenFull = new ObservableCollection<lagerliste>(db.lagerlisten);
                    Sprachen = new ObservableCollection<AuswahlEintraege>(db.AuswahlEintraege.Where(n => n.Gruppe == "TypSprache"));

                    var Query = from f in db.firmen
                                where f.IstKunde == 1
                                select new Models.FirmenSource
                                {
                                    id = f.id,
                                    name = f.name,
                                    Zahlungsbedingungen = f.Zahlungsbedingungen

                                };

                    Firmen = new ObservableCollection<Models.FirmenSource>(Query);
                    //Firmen = new ObservableCollection<firma>(db.firmen.Where(n => n.IstKunde == 1));

                    LoadSignatures();

                    isDirty = false;

                })));



            });
        }

        private void MapBelegToThis()
        {
            Mapper.CreateMap<SI_Belege, SI_BelegeViewModel>();
            Mapper.Map<SI_Belege, SI_BelegeViewModel>(Beleg, this);
        }

        private void InitModelLoadBeleg(int id)
        {
            Beleg = db.SI_Belege.Where(n => n.id == id).SingleOrDefault();
        }

        private void InitModelNewBeleg()
        {
            Beleg = new SI_Belege();
            db.AddToSI_Belege(Beleg);
            ListboxBelegeTextbausteineVM = new CheckListBoxes.ListboxBelegeTextbausteineViewModel(db, this);
            ListboxBelegeTextbausteineVM.DataChanged += ListboxBelegeTextbausteineVM_DataChanged;
            ti.Start();
        }

        private void LoadSignatures()
        {
            Signaturen = new ObservableCollection<C_Localizing>(db.C_Localizing.Where(n => n.id_Sprache == id_Sprache && n.Objektname == 3));

        }

        void ListboxBelegeTextbausteineVM_DataChanged(bool obj)
        {
            isDirty = true;
        }

        void ti_Tick(object sender, EventArgs e)
        {
            ListboxBelegeTextbausteineVM.AddSelectedNames();
            ti.Stop();
        }

        private void SetCursor(bool Value)
        {
            if (ThisCursor != null)
            {
                if (Value)
                {
                    ThisCursor = System.Windows.Input.Cursors.Arrow;
                }
                else
                {
                    ThisCursor = System.Windows.Input.Cursors.Wait;
                }

            }
        }



        public bool DoSaveChanges()
        {
            //if (isDirty || Belegart == "inv")
            if (isDirty || SelectedBelegarten.id == "inv")
            {
                RecalcBeleg();
                StringBuilder sb = new StringBuilder();
                bool bPlausible = true;

                sb.AppendLine("Belegdaten sind nicht vollständig.");
                sb.AppendLine();

                //var ba = db.StammBelegarten.Where(n=>n.id == Belegart).SingleOrDefault();
                //var bw = db.StammBewegungsarten.Where(j => j.id == ba.Bewegungsart).SingleOrDefault();
                //Console.WriteLine(bw.ToString());

                //if (Belegart == null)
                if (SelectedBelegarten.id == null)
                {
                    sb.AppendLine("Es muß eine Belegart ausgewählt werden.");
                    bPlausible = false;
                }

                if (EKVK != "intern" && EKVK != "inv")
                {
                    //if (id_Firma == null)
                    //{
                    //    sb.AppendLine("Es muß eine Firma ausgewählt werden.");
                    //    bPlausible = false;
                    //}

                    if (SelectedFirmen == null)
                    {
                        sb.AppendLine("Es muß eine Firma ausgewählt werden.");
                        bPlausible = false;
                    }

                }

                if (BelegePositionen == null)
                {
                    sb.AppendLine("Es sind keine Belegpositionen vorhanden.");
                    bPlausible = false;
                }
                else
                {
                    if (BelegePositionen.Count == 0)
                    {
                        sb.AppendLine("Es sind keine Belegpositionen vorhanden.");
                        bPlausible = false;
                    }
                    foreach (var item in BelegePositionen)
                    {
                        if (!item.Menge.HasValue || item.Menge == 0)
                        {
                            sb.AppendLine("Menge in Belegposition kann nicht leer oder 0 sein.");
                            bPlausible = false;
                        }

                        if (!item.id_Artikel.HasValue || item.id_Artikel == 0)
                        {
                            sb.AppendLine("Artikelnummer in Belegposition kann nicht leer oder 0 sein.");
                            bPlausible = false;
                        }
                    }

                }


                if (id_Ziellager == id_Quelllager)
                {
                    sb.AppendLine("QuellLager und ZielLager können nicht identisch sein.");
                    bPlausible = false;
                }

                if (EKVK == "intern" && (id_Quelllager == 4 || id_Ziellager == 4))
                {
                    sb.AppendLine("Umbuchung kann nicht von oder zu keinem Lager erfolgen.");
                    bPlausible = false;
                }

                if (SelectedBelegarten.Adressenpflicht == 1)
                {
                    if (ZusatzInfo == null || ZusatzInfo.Trim() == string.Empty)
                    {
                        sb.AppendLine("Es ist keine Firmenbezeichnung ausgewählt.");
                        bPlausible = false;
                    }

                    if (CommonTools.Tools.ConfigEntry<int>.GetConfigEntry("Erweiterte_Adresspflicht_SI_Belegen", "1", "") == 1)
                    {
                        if (Ort == null || Ort.Trim() == string.Empty)
                        {
                            sb.AppendLine("Es ist kein Ort ausgewählt.");
                            bPlausible = false;
                        }
                        if (Straße == null || Straße.Trim() == string.Empty)
                        {
                            sb.AppendLine("Es ist kein Straße ausgewählt.");
                            bPlausible = false;
                        }
                        if (PLZ == null || PLZ.Trim() == string.Empty)
                        {
                            sb.AppendLine("Es ist kein PLZ ausgewählt.");
                            bPlausible = false;
                        }
                    }




                }


                if (isFatalError)
                {
                    sb.AppendLine("Es ist ein schwerwiegender Fehler aufgetreten.");
                    sb.AppendLine("Der Beleg kann nicht gespeichert werden");
                    sb.AppendLine("Bitte brechen Sie die Belegverarbeitung ab.");
                    bPlausible = false;
                }


                if (bPlausible == true)
                {
                    Mapper.CreateMap<SI_BelegeViewModel, SI_Belege>()
                        .ForMember(dest => dest.SI_BelegePositionen, opt => opt.Ignore())
                        .ForMember(dest => dest.SI_BelegeZusatzzeilen, opt => opt.Ignore())
                        .ForMember(dest => dest.SI_BelegeTextbausteine, opt => opt.Ignore())
                        .ForSourceMember(dest => dest.ListboxBelegeTextbausteineVM, opt => opt.Ignore())
                        .ForSourceMember(dest => dest.isFatalError, opt => opt.Ignore())
                        .ForSourceMember(dest => dest.BackgroundState, opt => opt.Ignore())
                        ;


                    this.Belegnummer = string.Empty;

                    ////   Mapper.AssertConfigurationIsValid();

                    Mapper.Map<SI_BelegeViewModel, SI_Belege>(this, Beleg);
                    // SaveLieferadresse();
                    foreach (var item in BelegePositionen)
                    {
                        Beleg.SI_BelegePositionen.Add(item);
                    }

                    if (Zusatzzeilen != null)
                    {
                        foreach (var item in Zusatzzeilen)
                        {
                            Beleg.SI_BelegeZusatzzeilen.Add(item);
                        }
                    }



                    db.SaveChanges();
                    id = Beleg.id;
                    isDirty = false;
                    using (var lb = new WaWi.Lagerbuchungen.Lagerbuchungen.Lagerbuchungen(db))
                    {
                        var res = lb.Lagerbuchung(Beleg);
                        if (res == Lagerbuchungen.LagerbuchungResult.BuchungOK || res == Lagerbuchungen.LagerbuchungResult.BelegartNichtBuchbar)
                        {
                            Beleg.istGebucht = 1;
                            istGebucht = 1;
                            db.SaveChanges();
                            isDirty = false;

                        }
                    }
                    if (!id_Projekt.HasValue || id_Projekt == 0)
                    {
                        // Belegnummer = CommonTools.Tools.Belegnummern.GetBelegnummerOhneProjekt(Belegart);
                        Belegnummer = CommonTools.Tools.Belegnummern.GetBelegnummerOhneProjekt(SelectedBelegarten.id);
                    }
                    else
                    {
                        //Belegnummer = CommonTools.Tools.Belegnummern.GetBelegnummerProjekt(Belegart, (int)id_Projekt);
                        Belegnummer = CommonTools.Tools.Belegnummern.GetBelegnummerProjekt(SelectedBelegarten.id, (int)id_Projekt);
                    }
                    Beleg.Belegnummer = Belegnummer;
                    db.SaveChanges();
                    isDirty = false;

                    var wawi = new WaWi.Vorgaenge.Vorgaenge(db);

                    if (SelectedBelegarten != null && SelectedBelegarten.Vorgang != 0)
                    {
                        if (IstVorgangBeleg)
                        {
                            if (SourceBeleg != null && Beleg != null)
                            {
                                var res = wawi.ProcessVorgang(SourceBeleg, Beleg);
                                if (res)
                                {
                                    db.SaveChanges();
                                }

                            }

                        }
                        else
                        {
                            var vorgang = wawi.CreateNewVorgang(Beleg.firma);
                            var res = wawi.AddToVorgang(vorgang, Beleg);
                            if (res)
                            {
                                db.SaveChanges();
                            }

                        }
                    }


                }
                else
                {
                    UserMessage.NotifyUser(sb.ToString());
                    return false;
                }
            }

            return true;
        }

        private int? GetArtikelID(string Artikelnummer)
        {


            var res = db.lagerlisten.Where(i => i.artikelnr == Artikelnummer);
            if (!res.Any())
            {
                //throw new ArgumentOutOfRangeException("Artikelnummer", Artikelnummer, ArtikelnummerNichtVorhandenMessage);
                return 0;
            }
            else if (res.Count() > 1)
            {
                throw new ArgumentOutOfRangeException("Artikelnummer", Artikelnummer, ArtikelnummerMehrfachVorhandenMessage);
            }
            else
            {
                int? id = res.SingleOrDefault().id;
                return (int)id;
            }

            //var res = db.lagerlisten.Where(i => i.artikelnr == Artikelnummer).FirstOrDefault();
            //if (res != null)
            //{
            //    int? id = res.id;

            //    if (id.HasValue)
            //    {
            //        return (int)id;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //else
            //{
            //    return null;
            //}
        }

        public void ImportKalkulation(kalkulationstabelle kalkulationstabelle, bool ShowUserMessage = true)
        {
            StringBuilder sb = new StringBuilder();


            foreach (var item in kalkulationstabelle.kalkulationstabelle_detail)
            {
                try
                {
                    if (item.ausgeben != 0)
                    {
                        addKalulationsTabellePos(item.artikelnr, item.menge, item.einzelpreis, item.beschreibung, item.einheit, item.id_Lieferant);
                    }
                    else
                    {
                        sb.Append("Kalkulationstabellenposition ");
                        sb.Append(item.idx + 1);
                        sb.Append(" Artikel :");
                        sb.Append(item.beschreibung);
                        sb.AppendLine(" wurde nicht hinzugefügt da als 'nicht ausgeben' gekennzeichnet.");
                    }


                }
                catch (ArgumentOutOfRangeException ex)
                {
                    sb.AppendLine("Die Belegposition konnte nicht hinzugefügt werden");
                    sb.AppendLine(ex.Message);
                    sb.AppendLine();
                }
                catch (ArgumentNullException ex)
                {
                    sb.AppendLine("Die Belegposition konnte nicht hinzugefügt werden");
                    sb.AppendLine(ex.Message);
                    sb.AppendLine();
                }
                catch (Exception)
                {

                    throw;
                }


            }

            RecalcBeleg();

            if (sb.ToString() != string.Empty)
            {
                if (ShowUserMessage)
                {
                    CommonTools.Tools.UserMessage.NotifyUser(sb.ToString());
                }

            }


        }

        //private decimal GetSelectedCalculationArticlePrice(kalkulationstabelle SelectedKalkulation,string ArtikelNr)
        //{

        //    try
        //    {
        //        if (SelectedKalkulation == null)
        //        {
        //            throw new ArgumentOutOfRangeException("Es wurde keine Kalulationstabelle ausgewählt.", KalkulationstabelleOderArtikelnummerNichtGefundenMessage);
        //        }

        //        if (SelectedKalkulation.kalkulationstabelle_detail == null)
        //        {
        //            throw new ArgumentOutOfRangeException("Die ausgewählte Kalulationstabelle enthält keine Positionen.", KalkulationstabelleOderArtikelnummerNichtGefundenMessage);
        //        }

        //        var ktp = SelectedKalkulation.kalkulationstabelle_detail.Where(n => n.artikelnr == ArtikelNr).SingleOrDefault();
        //        if (ktp == null)
        //        {
        //            throw new ArgumentOutOfRangeException(ArtikelNr, "Der Artikel ist nicht in der ausgewählten Kalulationstabelle enthalten.", KalkulationstabelleOderArtikelnummerNichtGefundenMessage);
        //        }

        //        if (ktp.zuschlagpreis.HasValue)
        //        {
        //            return Math.Round((decimal)ktp.zuschlagpreis, 2);
        //        }
        //        else
        //        {
        //            return 0m;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }



        //}






        private decimal GetKalkulationPreis(string ArtikelNr)
        {

            if (id_Projekt.HasValue)
            {
                var pn = db.projekte.Where(id => id.id == id_Projekt).SingleOrDefault().projektnummer;
                var kt = db.kalkulationstabellen.Where(p => String.Compare(p.projektnummer, pn, false) == 0);
                if (kt.Count() == 1)
                {
                    int kid = kt.SingleOrDefault().id;
                    var ktp = db.kalkulationstabelle_details.Where(id => id.id_kalkulationstabelle == kid && id.artikelnr == ArtikelNr).SingleOrDefault();
                    if (ktp != null)
                    {
                        if (ktp.zuschlagpreis.HasValue)
                        {
                            return (decimal)ktp.zuschlagpreis;
                        }

                    }

                }
            }
            return 0;

        }

        private void OpenBelegDruck(int BelegId)
        {
            var b = new BelegViewer(this);
            b.ShowDialog();


        }


        private void LoadLieferadresse()
        {

            if (!isLoaded)      // nur wenn Beleg vollständig geladen.
                return;


            if (istGebucht.HasValue == false || istGebucht == 0)
            {

                switch (EKVK)
                {
                    case "VK":
                        {
                            if (id_Projekt.HasValue && id_Projekt != 0)
                            {
                                var la = db.projekte.Where(n => n.id == id_Projekt).SingleOrDefault();
                                if (la != null)
                                {

                                    Ort = la.Ort;
                                    PLZ = la.PLZ;
                                    Straße = la.Straße;
                                    ZusatzInfo = la.ZusatzInfo;
                                    ZusatzInfo2 = la.ZusatzInfo2;
                                    ZusatzInfo3 = la.ZusatzInfo3;
                                    VAT = la.VAT;
                                    Land = la.Land;
                                    isAdresseExpanded = true;
                                }


                            }
                            break;
                        }

                    case "EK":
                        {
                            //if (id_Firma.HasValue && id_Firma != 0)
                            if (SelectedFirmen != null)
                            {

                                Firmen_Adressen la = db.Firmen_Adressen.Where(n => n.id_Firma == SelectedFirmen.id && n.Hauptadresse == 1).SingleOrDefault();
                                FillAdresse(la, firmenname);


                            }



                            break;
                        }
                    case "ReWe":
                        {
                            // if (id_Firma.HasValue && id_Firma != 0)
                            if (SelectedFirmen != null)
                            {
                                FirmenAdressen = new ObservableCollection<Firmen_Adressen>(db.Firmen_Adressen.Where(n => n.id_Firma == SelectedFirmen.id && n.Typ == 5));

                            }



                            break;
                        }


                    default:
                        break;

                }
            }
            else
            {



            }



        }

        private void FillAdresse(Firmen_Adressen la, string FirmaName)
        {
            if (la != null)
            {
                isAdresseExpanded = true;
                Ort = la.Ort;
                PLZ = la.PLZ;
                Straße = la.Straße;
                ZusatzInfo = FirmaName;
                ZusatzInfo2 = la.ZusatzInfo2;
                ZusatzInfo3 = la.ZusatzInfo3;
                VAT = la.VAT;
                Land = la.Land;
            }
            else
            {
                ZusatzInfo = "Es konnte keine Standardadresse gefunden werden.";
            }
        }

        public void SetEditingEnabled(bool value)
        {
            if (BelegePositionen != null)
            {
                foreach (var item in BelegePositionen)
                {
                    item.EditingEnabled = value;
                }
            }

        }

        //private void SaveLieferadresse()
        //{
        //    if (Adresse != null)
        //    {

        //        if (Adresse.isDirty)
        //        {
        //            Firmen_Adressen la = new Firmen_Adressen();
        //            db.AddToFirmen_Adressen(la);
        //            Mapper.CreateMap<CommonTools.ViewModels.AdressenViewModel, Firmen_Adressen>().ForMember(dest => dest.id, opt => opt.Ignore());
        //            Mapper.Map<CommonTools.ViewModels.AdressenViewModel, Firmen_Adressen>(Adresse, la);
        //            Beleg.Firmen_Adressen = la;
        //            Adresse.isDirty = false;
        //        }
        //    }


        //}

        private void RecalcBeleg()
        {
            if (!isPreiseAnzeigen) return;


            decimal buf = 0;
            if (BelegePositionen != null)
            {
                foreach (var item in BelegePositionen)
                {
                    if (item.Endpreis.HasValue)
                    {
                        buf += (decimal)item.Endpreis;
                    }

                }

                SummeBelegPositionen = buf;
                Belegsumme = buf;
                buf = 0;
            }


            if (Zusatzzeilen != null)
            {
                // Sortierung erneuern
                if (DoSortZusatzzeilen)
                {
                    Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>(Zusatzzeilen.OrderBy(n => n.idx));
                    DoSortZusatzzeilen = false;
                }


                int pos = 0;
                foreach (var item in Zusatzzeilen)
                {
                    item.Pos = ++pos;
                    if (item.ReferenzTyp == enumZusatzzeileRefType.Zwischensumme.ToString())
                    {

                        item.Zeilensaldo = Belegsumme;
                        item.Zeilenwert = Belegsumme;

                    }
                    else
                    {
                        if (!item.Wert.HasValue || item.Wert == 0)
                        {
                            if (item.Prozent.HasValue && item.Prozent != 0)
                            {
                                buf = Belegsumme * ((decimal)item.Prozent / 100);
                                item.Zeilenwert = buf;

                                Belegsumme += buf;
                                item.Zeilensaldo = Belegsumme;
                            }
                        }

                        if (!item.Prozent.HasValue || item.Prozent == 0)
                        {
                            if (item.Wert.HasValue && item.Wert != 0)
                            {


                                Belegsumme += (decimal)item.Wert;
                                item.Zeilenwert = (decimal)item.Wert;
                                item.Zeilensaldo = Belegsumme;
                            }
                        }
                    }


                }
            }

        }


        private void RenumberPositions()
        {
            BelegePositionen = new ObservableCollection<SI_BelegePositionen>(BelegePositionen.OrderBy(n => n.idx));
            int Pos = 0;
            foreach (var item in BelegePositionen)
            {
                item.Pos = ++Pos;
            }

        }




        //-------------------------------------------------------------------------------------------------------------------
        // A. Stoever (11.08.2014) ToBeDeleted 
        //-------------------------------------------------------------------------------------------------------------------

        //private Nullable<double> CalculateMenge(StammBelegarten Belegart, double? menge)
        //{
        //    if (Belegart == null)
        //    {
        //        throw new ArgumentNullException("Belegart", BelegartKannNichtNullSeinMessage);
        //    }

        //    if (!Belegart.Wirkung.HasValue)
        //    {
        //        throw new ArgumentNullException("Belegart Wirkung", ArgumentKannNichtNullSeinMessage);
        //    }

        //    if (!menge.HasValue)
        //    {
        //        throw new ArgumentNullException("Belegart Wirkung", ArgumentKannNichtNullSeinMessage);
        //    }

        //    return menge * Belegart.Wirkung;
        //}
        #endregion

        #region "AddPosition"

        private void addKalulationsTabellePos(string ArtNr, double? menge, double? preis, string Bezeichnung, string Einheit, int? Hersteller)
        {
            try
            {
                var det = CreateBelegePosition();
                det.Belegart = SelectedBelegarten;
                det.Kalkulationstabelle = SelectedKalkulation;
                det.Kennzeichen = "calc";

                det.istGebucht = 0;
                det.created = DateTime.Now;
                int? ArtId = GetArtikelID(ArtNr);
                if (ArtId == 0)
                {
                    ArtId = DAL.WaWiTools.SI_BelegeHelper.CreateArtikel(ArtNr, preis, Bezeichnung, Einheit, Hersteller, true);
                    OnRingArtikelFullViewSourceChanged();
                }
                det.id_Artikel = ArtId;
                det.Menge = menge == null ? 0 : menge;
                decimal rPreis = preis == null ? 0 : Math.Round((decimal)preis, 2);

                SetPriceBackgroundBrush(preis, det);

                det.PreisVorRabatt = rPreis; // (decimal)preis;
                det.Preis = rPreis; // (decimal)preis;
                det.Endpreis = rPreis * (decimal)det.Menge;  // (decimal)preis * (decimal)det.Menge;

                addPosition(det);


            }
            catch (ArgumentOutOfRangeException ex)
            {

                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                CommonTools.Tools.UserMessage.NotifyUser(ex.Message);
            }

        }

        public void AddSetPositionen(StammLagerorte value)
        {
            foreach (var item in value.Lagerbestaende)
            {
                AddScannerPosition(item.Artikelnummer, item.Sollbestand.ToString());
            }
        }


        public void AddScannerPosition(string Artikelnummer, string menge)
        {

            try
            {
                var det = CreateBelegePosition();
                det.Belegart = SelectedBelegarten;
                det.Kalkulationstabelle = SelectedKalkulation;
                det.istGebucht = 0;
                det.created = DateTime.Now;
                det.id_Artikel = GetArtikelID(Artikelnummer);
                int res = 0;
                if (int.TryParse(menge, out res))
                {

                    det.Menge = res;

                    //det.Menge = res * SelectedBelegarten.Wirkung ;
                }
                var preis = DAL.WaWiTools.SI_BelegeHelper.GetArticlePrice(SelectedKalkulation, SelectedBelegarten, Artikelnummer);
                if (preis != 0)
                {
                    det.PreisVorRabatt = preis;
                    det.Preis = preis;
                    det.Endpreis = preis * (decimal)det.Menge;
                }


                addPosition(det);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                CommonTools.Tools.UserMessage.NotifyUser(ex.Message);

            }

            catch (ArgumentNullException ex)
            {
                CommonTools.Tools.UserMessage.NotifyUser(ex.Message);

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }



        public void addPosition(SI_BelegePositionen pos)
        {
            if (BelegePositionen == null)
            {
                BelegePositionen = new ObservableCollection<SI_BelegePositionen>();
            }

            pos.idx = BelegePositionen.Count + 1;
            pos.Pos = pos.idx;
            BelegePositionen.Add(pos);
            isDirty = true;
        }

        #endregion


        #region "Helperfunctions"

        private void SetPriceBackgroundBrush(double? preis, SI_BelegePositionen det)
        {
            if (preis == null)
            {
                det.BackgroundX = Brushes.Red;
            }
            else if (preis == 0)
            {
                det.BackgroundX = Brushes.Yellow;
            }
            else
            {
                det.BackgroundX = Brushes.Lime;
            }
        }

        private Models.FirmenSource GetFirmensourceByKdNr(int? KdNr)
        {
            var Q = from f in db.firmen
                    where f.KdNr == KdNr && f.IstKunde == 1
                    select new Models.FirmenSource
                    {
                        id = f.id,
                        name = f.name,
                        Zahlungsbedingungen = f.Zahlungsbedingungen
                    };
            return Q.SingleOrDefault();
        }

        private Models.FirmenSource GetFirmensourceByID(int Id)
        {
            var Q = from f in db.firmen
                    where f.id == Id
                    select new Models.FirmenSource
                    {
                        id = f.id,
                        name = f.name,
                        Zahlungsbedingungen = f.Zahlungsbedingungen
                    };
            return Q.SingleOrDefault();
        }

        private SI_BelegeZusatzzeilen CreateZusatzzeile()
        {
            if (Zusatzzeilen == null)
            {
                Zusatzzeilen = new ObservableCollection<SI_BelegeZusatzzeilen>();
            }

            var zz = new SI_BelegeZusatzzeilen();
            // zz.DataChanged += zz_DataChanged;
            // zz.DataChanged +=()=>RecalcBeleg();
            // zz.DataChanged +=  RecalcBeleg();
            zz.DataChanged += zz_DataChanged;
            return zz;
        }

        void zz_DataChanged(bool obj)
        {
            DoSortZusatzzeilen = obj;
            RecalcBeleg();


        }


        private SI_BelegePositionen CreateBelegePosition()
        {
            var det = new SI_BelegePositionen();
            det.DataChanged += det_DataChanged;
            det.PositionChanged += det_PositionChanged;
            return det;
        }

        void det_PositionChanged(DAL.DataTypes.SI_BelegePositionChangedEventArgs arg1, SI_BelegePositionen.ChangeType arg2)
        {

            switch (arg2)
            {
                case SI_BelegePositionen.ChangeType.id_Artikel:
                    {
                        //arg1.CurrentPosition.Beschreibung = "Unsinn";
                        //arg1.CurrentPosition.Bezeichnung = "Unsinn";
                        break;
                    }
                case SI_BelegePositionen.ChangeType.Preis:
                    break;
                case SI_BelegePositionen.ChangeType.Rabatt:
                    break;
                default:
                    break;
            }


        }




        private void det_DataChanged()
        {
            RecalcBeleg();
        }

        private void SetSateAddPosition()
        {
            var sba = SelectedBelegarten;
            var ska = SelectedKalkulation;
            if (sba != null)
            {
                if (sba.Kalkulationstabellenpflicht != null && sba.Kalkulationstabellenpflicht != 0)
                {
                    if (ska != null)
                    {
                        isButtonAddPositionEnabled = true;
                    }
                    else
                    {
                        isButtonAddPositionEnabled = false;
                    }

                }
                else
                {
                    isButtonAddPositionEnabled = true;
                }

            }
            else
            {
                isButtonAddPositionEnabled = false;
            }
        }


        private void LoadReference()
        {
            try
            {
                if (id_Projekt.HasValue && SelectedSprachen != null)
                {
                    string Ref = db.projekte.Where(n => n.id == id_Projekt).SingleOrDefault().referenzkdbestellnummer;
                    string buf = db.C_Localizing.Where(n => n.Begriffname == "Header_ProjectReference" && n.id_Sprache == SelectedSprachen.id_This_int).SingleOrDefault().Wert;
                    Bemerkung = buf ?? "" + Ref ?? "";
                }
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler bei der Übernahme der Projektreferenz");
            }

        }

        private void ClearZusatzzeilen()
        {
            var query = Zusatzzeilen.Where(n => n.Typ == "MWST" || n.Typ == "Ustbefreit");
            if (query.Any())
            {
                foreach (var item in query.ToList())
                {
                    Zusatzzeilen.Remove(item);
                }
            }
        }

        private void ReloadLagerListenFullViewsource()
        {
            // LagerlisteFullViewSource = new ObservableCollection<lagerliste>(db.lagerlisten);
        }
        #endregion

        #region "Scanner"
        private void importScannerData(string filename, int ArtikelNr, int Menge)
        {

            try
            {
                StreamReader f = new StreamReader(filename);
                string[] cols;
                string line = string.Empty;

                while (!f.EndOfStream)
                {
                    line = f.ReadLine();
                    cols = line.Split(';');
                    AddScannerPosition(cols[ArtikelNr], cols[Menge]);


                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Importdatei konnte nicht gefunden werden.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region "Buchen"

        //private bool SaveBuchungen()
        //{
        //    bool res = true;
        //    try
        //    {
        //        var b = Beleg;
        //        var bw = (StammBewegungsarten)b.StammBelegarten.StammBewegungsarten;


        //        if (bw.Lagerbuchung == 1)
        //        {
        //            if (BelegePositionen != null)
        //            {

        //                using (var lb = new Lagerbuchungen(db))
        //                {
        //                    foreach (var item in BelegePositionen)
        //                    {
        //                        var pos = (SI_BelegePositionen)item;
        //                        if (pos.istGebucht == 0 || pos.istGebucht == null)
        //                            if (pos.id_Artikel != null && pos.Menge != null && pos.Menge != 0)
        //                            {
        //                                int ProId = b.id_Projekt.HasValue ? (int)b.id_Projekt : 0;
        //                                int FirmaID = b.id_Firma.HasValue ? (int)b.id_Firma : 0;
        //                                lb.Lagerbuchung((int)b.id_Quelllager, (int)b.id_Ziellager, (int)bw.WirkungQuelllager, (int)bw.WirkungZiellager, (int)pos.Menge, (int)pos.id_Artikel, bw.id, ProId, (int)pos.id, FirmaID);
        //                                pos.istGebucht = 1;
        //                            }
        //                    }
        //                }

        //            }
        //        }


        //        b.istGebucht = 1;

        //        db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }

        //    return res;

        //}


        #endregion






    }
}
