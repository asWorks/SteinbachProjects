using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using DAL;
using AutoMapper;
using System.Windows;
using System.Windows.Controls;
using WaWi.Lagerbuchungen.Lagerbuchungen;
using System.IO;


namespace ProjektDB.ViewModels
{
    public class SI_InventurenViewModel : Screen
    {
        int Counter = 0;
        SteinbachEntities db;
        SI_Inventuren Inventur = null;

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
                    isBeendetEnabled = !value;
                    NotifyOfPropertyChange(() => isDirty);

                }
            }
        }


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

        private DateTime? _Inventurdatum;
        public DateTime? Inventurdatum
        {
            get { return _Inventurdatum; }
            set
            {
                if (value != _Inventurdatum)
                {
                    _Inventurdatum = value;
                    NotifyOfPropertyChange(() => Inventurdatum);
                    isDirty = true;
                }
            }
        }

        private String _Inventurart;
        public String Inventurart
        {
            get { return _Inventurart; }
            set
            {
                if (value != _Inventurart)
                {
                    _Inventurart = value;
                    NotifyOfPropertyChange(() => Inventurart);
                    isDirty = true;
                }
            }
        }

        private Int32? _Verantwortlicher;
        public Int32? Verantwortlicher
        {
            get { return _Verantwortlicher; }
            set
            {
                if (value != _Verantwortlicher)
                {
                    _Verantwortlicher = value;
                    if (Verantwortlicher.HasValue && Verantwortlicher != 0)
                    {
                        SelectedVerantwortlich = db.personen.Where(n => n.id == Verantwortlicher).SingleOrDefault();
                        SelectedSI_Person = db.personen.Where(n => n.id == Verantwortlicher).SingleOrDefault();
                    }

                    Console.WriteLine("Verantwortlicher : " + Verantwortlicher.ToString() + "  --  id : " + id.ToString() + " -- Counter : " + ++Counter);
                    NotifyOfPropertyChange(() => Verantwortlicher);
                    isDirty = true;
                }
            }
        }

        private String _Notiz;
        public String Notiz
        {
            get { return _Notiz; }
            set
            {
                if (value != _Notiz)
                {
                    _Notiz = value;
                    NotifyOfPropertyChange(() => Notiz);
                    isDirty = true;
                }
            }
        }

        private Int16? _Inventurbeendet;
        public Int16? Inventurbeendet
        {
            get { return _Inventurbeendet; }
            set
            {
                if (value != _Inventurbeendet)
                {
                    _Inventurbeendet = value;
                    NotifyOfPropertyChange(() => Inventurbeendet);
                    if (value.HasValue)
                    {
                        isEnabled = value == 0 ? true : false;
                        isDirty = true;
                    }


                }
            }
        }

        private Int16? _Inventurgebucht;
        public Int16? Inventurgebucht
        {
            get { return _Inventurgebucht; }
            set
            {
                if (value != _Inventurgebucht)
                {
                    _Inventurgebucht = value;
                    NotifyOfPropertyChange(() => Inventurgebucht);
                    isDirty = true;
                }
            }
        }


        private int? _BelegID;
        public int? BelegID
        {
            get { return _BelegID; }
            set
            {
                if (value != _BelegID)
                {
                    _BelegID = value;
                    NotifyOfPropertyChange(() => BelegID);
                    isDirty = true;
                }
            }
        }

        private bool _isEnabled;
        public bool isEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    NotifyOfPropertyChange(() => isEnabled);
                    isDirty = true;

                }
            }
        }

        private bool _isGebuchtEnabled;
        public bool isGebuchtEnabled
        {
            get { return _isGebuchtEnabled; }
            set
            {
                if (value != _isGebuchtEnabled)
                {
                    _isGebuchtEnabled = value;
                    NotifyOfPropertyChange(() => isGebuchtEnabled);
                    isDirty = true;
                }
            }
        }

        private bool _isBeendetEnabled;
        public bool isBeendetEnabled
        {
            get { return _isBeendetEnabled; }
            set
            {
                //if (value != _isBeendetEnabled)
                //{
                _isBeendetEnabled = value;
                NotifyOfPropertyChange(() => isBeendetEnabled);

            }
            //}
        }






        #endregion

        #region  "ObservableCollection"
        private StammInventuren _SelectedInventurstamm;
        public StammInventuren SelectedInventurstamm
        {
            get { return _SelectedInventurstamm; }
            set
            {
                if (value != _SelectedInventurstamm)
                {
                    _SelectedInventurstamm = value;
                    Inventurart = value.id;
                    NotifyOfPropertyChange(() => SelectedInventurstamm);
                    isDirty = true;
                }
            }
        }

        private ObservableCollection<StammInventuren> _Inventurstamm;
        public ObservableCollection<StammInventuren> Inventurstamm
        {
            get { return _Inventurstamm; }
            set
            {
                if (value != _Inventurstamm)
                {
                    _Inventurstamm = value;
                    NotifyOfPropertyChange(() => Inventurstamm);
                    isDirty = true;
                }
            }
        }



        private ObservableCollection<person> _Verantwortlich;
        public ObservableCollection<person> Verantwortlich
        {
            get { return _Verantwortlich; }
            set
            {
                if (value != _Verantwortlich)
                {
                    _Verantwortlich = value;
                    NotifyOfPropertyChange(() => Verantwortlich);
                    isDirty = true;
                }
            }
        }

        private person _SelectedVerantwortlich;
        public person SelectedVerantwortlich
        {
            get { return _SelectedVerantwortlich; }
            set
            {
                if (value != _SelectedVerantwortlich)
                {
                    _SelectedVerantwortlich = value;
                    Verantwortlicher = value.id;
                    NotifyOfPropertyChange(() => SelectedVerantwortlich);
                    isDirty = true;
                }
            }

        }

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
                    isDirty = true;
                }
            }
        }

        private lagerliste _SelectedArtikelLookoutFull;
        public lagerliste SelectedArtikelLookoutFull
        {
            get { return _SelectedArtikelLookoutFull; }
            set
            {
                if (value != null)
                {


                    if (value != _SelectedArtikelLookoutFull)
                    {
                        _SelectedArtikelLookoutFull = value;
                        var pos = GetPos(value.id);

                        if (invPositionen == null)
                        {
                            addPosition(pos);
                        }
                        else
                        {
                            if (!invPositionen.Contains(pos, new CompareSI_InventurenPositionen()))
                            {
                                addPosition(pos);
                            }
                        }




                        NotifyOfPropertyChange(() => SelectedArtikelLookoutFull);
                        isDirty = true;
                    }
                }
            }
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
                    isDirty = true;
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
                    isDirty = true;

                }
            }
        }



        private ObservableCollection<person> _SI_Person;
        public ObservableCollection<person> SI_Person
        {
            get { return _SI_Person; }
            set
            {
                if (value != _SI_Person)
                {
                    _SI_Person = value;
                    NotifyOfPropertyChange(() => SI_Person);
                    isDirty = true;

                }
            }
        }

        private person _SelectedSI_Person;
        public person SelectedSI_Person
        {
            get { return _SelectedSI_Person; }
            set
            {
                if (value != _SelectedSI_Person)
                {
                    _SelectedSI_Person = value;
                    Verantwortlicher = value.id;
                    NotifyOfPropertyChange(() => SelectedSI_Person);
                    isDirty = true;

                }
            }
        }
        #endregion


        #region "PropertiesPositionen"

        private ObservableCollection<SI_InventurenPositionen> _invPositionen;
        public ObservableCollection<SI_InventurenPositionen> invPositionen
        {
            get { return _invPositionen; }
            set
            {
                if (value != _invPositionen)
                {
                    _invPositionen = value;
                    NotifyOfPropertyChange(() => invPositionen);
                    isDirty = true;
                }
            }
        }

        private SI_InventurenPositionen _SelectedinvPositionen;
        public SI_InventurenPositionen SelectedinvPositionen
        {
            get { return _SelectedinvPositionen; }
            set
            {
                if (value != _SelectedinvPositionen)
                {
                    _SelectedinvPositionen = value;
                    NotifyOfPropertyChange(() => SelectedinvPositionen);
                    isDirty = true;
                }
            }
        }
        #endregion

        #region "HandlePositiones"

        private SI_InventurenPositionen GetPos(int ArtikelID)
        {
            var pos = new SI_InventurenPositionen();
            pos.SI_Inventuren = Inventur;
            pos.id_artikel = ArtikelID;
            return pos;

        }


        public void addPosition(int ArtikelID)
        {

            addPosition(GetPos(ArtikelID));

        }

        public void addPosition(SI_InventurenPositionen pos)
        {
            if (invPositionen == null)
            {
                invPositionen = new ObservableCollection<SI_InventurenPositionen>();
            }

            //db.AddToSI_InventurenPositionen(pos);
            invPositionen.Add(pos);
        }


        public void GeneratePositions()
        {
            var query = db.lagerlisten;
            foreach (var item in query)
            {
                addPosition(item.id);
            }

        }


        //public void LoadPositions()
        //{
        //    if (id != null && id != 0)
        //    {

        //        invPositionen = new ObservableCollection<SI_InventurenPositionen>(db.SI_InventurenPositionen.Where(i => i.id_inventur == id));


        //    }

        //}


        //public void LoadPositions(SteinbachEntities db)
        //{
        //    if (id != null && id != 0)
        //    {

        //        this.db = db;
        //        invPositionen = new ObservableCollection<SI_InventurenPositionen>(db.SI_InventurenPositionen.Where(i => i.id_inventur == id));

        //    }

        //}
        #endregion


        #region "Constructors"

        public SI_InventurenViewModel()
        {
            //Mapper.CreateMap<Firmen_Kundenbesuch, KundenbesuchViewModel>()
            //          .ForMember(dest => dest.SI_Firma, opt => opt.Ignore())
            //          .ForMember(dest => dest.SI_Person, opt => opt.Ignore());

            ////Mapper.AssertConfigurationIsValid(); 
            //Mapper.Map<Firmen_Kundenbesuch, KundenbesuchViewModel>(besuch, this);

        }

        public SI_InventurenViewModel(int id)
        {

            db = new SteinbachEntities();
            if (id == 0)
            {
                Inventur = new SI_Inventuren();
                db.AddToSI_Inventuren(Inventur);
                Inventur.Inventurdatum = DateTime.Now;
            }
            else
            {
                Inventur = db.SI_Inventuren.Where(n => n.id == id).SingleOrDefault();

            }

            Mapper.CreateMap<SI_Inventuren, SI_InventurenViewModel>();
            //.ForMember(dest => dest.SI_Firma, opt => opt.Ignore())
            //.ForMember(dest => dest.SI_Person, opt => opt.Ignore());

            ////Mapper.AssertConfigurationIsValid(); 
            Mapper.Map<SI_Inventuren, SI_InventurenViewModel>(Inventur, this);
            Init();
            isDirty = false;
            isBeendetEnabled = false;
            isGebuchtEnabled = false;

        }

           
        #endregion

        #region "Functions"
        private void Init()
        {
            Inventurstamm = new ObservableCollection<StammInventuren>(db.StammInventuren);
            Verantwortlich = new ObservableCollection<person>(db.personen);
            ArtikelLookoutFull = new ObservableCollection<lagerliste>(db.lagerlisten.Where(n => n.id == 0));
            ArtikelLookoutFiltered = new ObservableCollection<lagerliste>(db.lagerlisten.Where(n => n.id == 0));
            SelectedVerantwortlich = db.personen.Where(n => n.id == Verantwortlicher).SingleOrDefault();
            SelectedInventurstamm = db.StammInventuren.Where(n => n.id == Inventurart).SingleOrDefault();
            isEnabled = true;

            if (Inventur.id != null && Inventur.id != 0)
            {
                invPositionen = new ObservableCollection<SI_InventurenPositionen>(db.SI_InventurenPositionen.Where(i => i.id_inventur == Inventur.id));
            }

            LockPositions();

        }

        private void LockPositions()
        {
            if (invPositionen != null)
            {
                foreach (var item in invPositionen)
                {
                    if (item.IstGebucht == 1)
                    {
                        item.AfterInit = true;
                    }
                }
            }

        }


        private bool DoSaveChanges()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Inventur konnte nicht gespeichert werden.");
            sb.AppendLine("Folgende Korrekturen sind nötig");
            sb.AppendLine();

            bool res = true;
            if (Inventurart == null)
            {
                sb.AppendLine("Inventurart kann nicht leer sein.");
                res = false;
            }

            if (!Verantwortlicher.HasValue)
            {
                sb.AppendLine("Verantwortlicher kann nicht leer sein.");
                res = false;
            }


            if (res == true)
            {

                Mapper.CreateMap<SI_InventurenViewModel, SI_Inventuren>();
                //.ForMember(dest => dest.SI_Firma, opt => opt.Ignore())
                //.ForMember(dest => dest.SI_Person, opt => opt.Ignore());

                ////Mapper.AssertConfigurationIsValid(); 
                Mapper.Map<SI_InventurenViewModel, SI_Inventuren>(this, Inventur);

                db.SaveChanges();
                id = Inventur.id;
                isDirty = false;

            }
            else
            {
                MessageBox.Show(sb.ToString());
            }

            LockPositions();
            // MakeBuchungsbeleg();

            //CommonTools.Tools.LockProcess.enumResultLockProcess Buchungen = CommonTools.Tools.LockProcess.DoLockProcess(SaveBuchungen, "Inventurbuchung_Gesperrt");
            //Console.WriteLine(Buchungen.ToString());

            return res;

        }

        private bool MakeBuchungsbeleg()
        {
            bool result = false;
            try
            {

                int count = invPositionen.Where(n => (n.Differenz != 0 && n.Differenz != null) && (n.IstGebucht == 0 || n.IstGebucht == null)).Count();
                if (count > 0)
                {
                    SI_Belege b = null;
                    if (!BelegID.HasValue || BelegID == 0)
                    {
                        b = new SI_Belege();
                        db.AddToSI_Belege(b);

                        b.created = DateTime.Now;
                        b.Belegart = "inv";
                        b.id_Ziellager = db.StammLagerorte.Where(n => n.istStandardLagerort == 1).SingleOrDefault().id;
                        b.id_Quelllager = 4;
                        b.Belegdatum = this.Inventurdatum;
                        b.Bemerkung = "Inventur : " + id;
                        b.Belegnummer = "Inventur : " + id;
                        b.id_user = Session.User.id;
                        b.id_Sprache = 1;
                        b.istGebucht = 0;

                    }
                    else
                    {
                        b = db.SI_Belege.Where(id => id.id == BelegID).SingleOrDefault();
                    }


                    if (invPositionen != null)
                    {

                        foreach (var item in invPositionen.Where(n => (n.Differenz != 0 && n.Differenz != null) && (n.IstGebucht == 0 || n.IstGebucht == null)))
                        {
                            var pos = (SI_InventurenPositionen)item;
                            var posBel = new SI_BelegePositionen();
                            posBel.SI_Belege = b;
                            posBel.Menge = pos.Differenz;
                            posBel.id_Artikel = pos.id_artikel;

                            db.AddToSI_BelegePositionen(posBel);


                            if (pos.IstGebucht == 0 || pos.IstGebucht == null)
                            {

                                if (pos.id_artikel != null && pos.Differenz != null && pos.Differenz != 0)
                                {
                                    int ProId = pos.id_inventur;


                                    pos.IstGebucht = 1;
                                }



                            }
                        }

                    }

                    db.SaveChanges();
                    if (!BelegID.HasValue || BelegID == 0)
                    {
                        BelegID = b.id;
                    }
                    Inventurgebucht = 1;
                    isGebuchtEnabled = false;
                    isDirty = false;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }


        }




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


        private void AddScannerPosition(string Artikelnummer, string menge)
        {

            try
            {

                int? id = GetArtikelID(Artikelnummer);
                if (id.HasValue)
                {
                    int res = 0;
                    var pos = GetPos((int)id);
                    bool Success = int.TryParse(menge, out res);
                    if (Success)
                    {
                        pos.Zaehlmenge = res;

                    }

                    addPosition(pos);

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        private int? GetArtikelID(string Artikelnummer)
        {

            var res = db.lagerlisten.Where(i => i.artikelnr == Artikelnummer).FirstOrDefault();
            if (res != null)
            {
                int? id = res.id;

                if (id.HasValue)
                {
                    return (int)id;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        //private bool SaveBuchungen()
        //{

        //    bool result = false;
        //    try
        //    {


        //        var b = new SI_Belege();
        //        db.AddToSI_Belege(b);

        //        b.Belegart = "inv";
        //        b.id_Ziellager = db.StammLagerorte.Where(n => n.istStandardLagerort == 1).SingleOrDefault().id;
        //        b.id_Quelllager = 4;
        //        b.Belegdatum = this.Inventurdatum;
        //        b.Bemerkung = "Inventur : " + id;

        //        var bw = (StammBewegungsarten)b.StammBelegarten.StammBewegungsarten;




        //        if (bw.Lagerbuchung == 1)
        //        {
        //            if (invPositionen != null)
        //            {

        //                var lb = new Lagerbuchungen(db);

        //                foreach (var item in invPositionen.Where(n => n.Differenz != 0 && n.Differenz != null))
        //                {
        //                    var pos = (SI_InventurenPositionen)item;
        //                    var posBel = new SI_BelegePositionen();
        //                    posBel.SI_Belege = b;
        //                    posBel.Menge = pos.Differenz;
        //                    posBel.id_Artikel = pos.id_artikel;

        //                    db.AddToSI_BelegePositionen(posBel);


        //                    if (pos.IstGebucht == 0 || pos.IstGebucht == null)
        //                    {

        //                        if (pos.id_artikel != null && pos.Differenz != null && pos.Differenz != 0)
        //                        {
        //                            int ProId = pos.id_inventur;

        //                            lb.Lagerbuchung((int)b.id_Quelllager, (int)b.id_Ziellager, (int)bw.WirkungQuelllager, (int)bw.WirkungZiellager, (int)pos.Differenz,
        //                                                                        (int)pos.id_artikel, bw.id, ProId, posBel);
        //                            pos.IstGebucht = 1;
        //                        }



        //                    }
        //                }

        //            }
        //        }


        //        b.istGebucht = 1;

        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }



        //}
        #endregion


        #region "Commands"
        public void btnSave()
        {

            DoSaveChanges();
        }

        public void btnInventurBuchen()
        {
            // SaveBuchungen();

            //decimal p = 512.65m;
            //decimal r = CommonTools.ParseFormula.GetCalculation(p, 1);
            //Console.WriteLine(r.ToString());


            int count = invPositionen.Where(n => !n.Zaehlmenge.HasValue).Count();
            if (count > 0)
            {
                MessageBox.Show("Es sind nicht für alle Inventurpositionen Zählbestände eingegeben worden. Buchen nicht möglich.");
            }
            else
            {
                MakeBuchungsbeleg();
            }


        }
        #endregion
        public void PrintList()
        {

            //var x = new views.ViewerInventurliste(invPositionen,);
            //x.ShowDialog();

        }

        public void GetListeSource(Window w)
        {
            try
            {
                var x = (views.Inventur)w;
                var grid = x.c1GridDetais;
                var dt = x.InvDatum.DateTime;

                // IEnumerable<SI_InventurenPositionen> liste = grid.ItemsSource.OfType<SI_InventurenPositionen>();

                var buf = GetFilteredGridRows(grid);

                var x1 = new views.ViewerInventurliste(buf, dt, views.ViewerInventurliste.InventurlistenArt.Zaehliste);
                x1.ShowDialog();
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }


        }

        private static IEnumerable<SI_InventurenPositionen> GetFilteredGridRows(C1.WPF.DataGrid.C1DataGrid grid)
        {
            var res = grid.Rows;
            var li = new List<SI_InventurenPositionen>();
            foreach (var item in res)
            {
                if (item.DataItem != null)
                {
                    li.Add((SI_InventurenPositionen)item.DataItem);
                }

            }

            var buf = (IEnumerable<SI_InventurenPositionen>)li;
            return buf;
        }



        public void GetDiffListeSource(Window w)
        {
            try
            {
                var x = (views.Inventur)w;
                var grid = x.c1GridDetais;
                var dt = x.InvDatum.DateTime;


                var buf = GetFilteredGridRows(grid);
                var x1 = new views.ViewerInventurliste(buf, dt, views.ViewerInventurliste.InventurlistenArt.Differenzliste);
                x1.ShowDialog();
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex);
            }


        }





        //System.Data.Objects.ObjectQuery<projekt> p;


        //      if (filter == string.Empty || filter == null)
        //      {
        //          p = db.projekte
        //                .OrderBy("it.created desc");
        //      }
        //      else
        //      {

        //          p = db.projekte
        //              .Where(filter)
        //              .OrderBy("it.created desc");
        //      }

        //      RecordCount = p.Count();

        //      var res = new ProjektCollection(p.Skip(toSkip).Take(toTake), db);
        //      return res;







        #region "EventCalls"
        public void DeletePosition(views.Inventur window)
        {

            var grid = window.c1GridDetais;
            var pos = (SI_InventurenPositionen)grid.SelectedItem;

            if (pos.IstGebucht == 1)
            {
                MessageBox.Show(string.Format("Position {0} ist schon gebucht. Löschen nicht möglich.", pos.Artikelname));

            }
            else
            {
                if (MessageBox.Show(string.Format("Position {0} wirklich löschen?", pos.Artikelname), "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    db.DeleteObject(pos);
                    invPositionen.Remove(pos);
                }


            }




        }
        public void CancelClosing(System.ComponentModel.CancelEventArgs e)
        {
            //var om = db.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified | System.Data.EntityState.Added | System.Data.EntityState.Deleted);
            //if (om.Count() > 0)
            //{

            //    var res = MessageBox.Show("Änderungen speichern ?", "", MessageBoxButton.YesNoCancel, System.Windows.MessageBoxImage.Warning);
            //    if (res == System.Windows.MessageBoxResult.Yes)
            //    {


            //        e.Cancel = !DoSaveChanges();

            //    }

            //    else if (res == MessageBoxResult.No)
            //    {
            //        e.Cancel = false;


            //    }

            //    else if (res == MessageBoxResult.Cancel)
            //    {
            //        e.Cancel = true;
            //    }
            //    else
            //    {
            //        e.Cancel = false;
            //    }

            //}


            //else
            //{

            //    e.Cancel = false;
            //}



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
                    ArtikelLookoutFiltered = new ObservableCollection<lagerliste>(db.lagerlisten.Where(n => n.bezeichnung.Contains(e.filter)));
                }
            }
        }
        public void ImportScanner()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.InitialDirectory = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("ImportFolder_ScannerData", @"E:\test\Import");
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents (.txt)|*.txt";

                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    string filename = dlg.FileName;
                    string res = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("ScannerImportColumns", "2,3", "Spalten der Exportdatei die eingelesen werden.");
                    string[] cols = res.Split(',');

                    importScannerData(filename, int.Parse(cols[0]), int.Parse(cols[1]));

                    // importScannerData(filename, 1, 2);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        #endregion


    }
}
