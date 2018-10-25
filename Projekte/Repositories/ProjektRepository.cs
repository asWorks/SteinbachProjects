using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

using ProjektDB.ObjectCollections;
using System.Windows.Data;
using System.Windows.Media;

namespace ProjektDB.Repositories
{
    class ProjektRepository
    {

        public int RecordCount { get; private set; }
        private SteinbachEntities db;

        public ProjektRepository(SteinbachEntities dbContext)
        {
            db = dbContext;

        }



        public ProjektCollection GetAlleProjekte()
        {
            var result = from p in db.projekte.Include("projekt_verlauf")
                         select p;
            var pc = new ProjektCollection(result, this.db);

            return pc;
        }

        public ProjektCollection GetProjekteListe()
        {
            var result = from p in db.projekte
                         where p.datum >= new DateTime(2011, 05, 1)
                         orderby p.projektnummer descending
                         select p;

            var pc = new ProjektCollection(result, this.db);
            return pc;
        }

        public ProjektCollection GetProjekteListe(int page, int rowsPerPage)
        {

            int toSkip = (page - 1) * rowsPerPage;

            var result = from p in db.projekte
                         where p.datum >= new DateTime(2004, 01, 1)
                         orderby p.projektnummer descending
                         select p;

            var pc = new ProjektCollection(result.Skip(toSkip).Take(rowsPerPage), this.db);
            return pc;
        }



        public ProjektCollection GetUntergeordneteProjekte(int ProjektID)
        {

            string pNumber = string.Empty;
            var buf = from n in db.projekte
                      where n.id == ProjektID
                      select n.ursprungsprojekt;

            pNumber = buf.FirstOrDefault();

            // pNumber = db.projekte.Where(p => p.id == ProjektID).FirstOrDefault().ursprungsprojekt;  // wirft Exception wenn ProjektId = 0;

            if (pNumber == string.Empty || pNumber == null)
            {
                return null;
            }



            var ur = from up in db.projekte
                     where up.ursprungsprojekt == pNumber
                     select up;

            var pc = new ProjektCollection(ur, this.db);
            return pc;

        }

        public ProjektCollection GetReklamationen(int pID)
        {
            //string pNumber = db.projekte.Where(p => p.id == pID).SingleOrDefault().projektnummer;

            var buf = from n in db.projekte
                      where n.id == pID
                      select n.ursprungsprojekt;

            string pNumber = buf.FirstOrDefault();

            var res = from r in db.projekte
                      where r.ursprungsprojekt == pNumber && r.type == "Reklamation"
                      select r;

            return new ProjektCollection(res, this.db);
        }




        public ProjektCollection GetProjekteByID(int ProjektID)
        {
            var result = from p in db.projekte          //.Include("projekt_verlauf")
                         where p.id == ProjektID
                         select p;

            var pc = new ProjektCollection(result, this.db);

            return pc;
        }

        public KalkulationCollection GetKalkukationByID(int KalkID)
        {
            var result = from k in db.kalkulationstabellen
                         where k.id == KalkID
                         select k;
            return new KalkulationCollection(result, this.db);
        }

        public ProjektCollection GetLeerprojekt()
        {
            var result = from p in db.projekte
                         where p.id == 0
                         select p;
            var pc = new ProjektCollection(result, this.db);

            return pc;
        }

        public string GetProjektNumberFromID(int ID)
        {
            if (ID == 0)
            {
                return string.Empty;
            }

            var query = from i in db.projekte
                        where i.id == ID
                        select i.projektnummer;

            return query.First();

        }
        public int GetProjektIDFromNumber(string PNumber)
        {

            var query = from i in db.projekte
                        where i.projektnummer == PNumber
                        select i.id;

            return query.First();
        }

        public IEnumerable<kalkulationstabelle> GetCalculation(int ProjektID)
        {
            if (ProjektID == 0)
            {
                return null;
            }

            string pNumber = this.GetProjektNumberFromID(ProjektID);
            var query = from k in db.kalkulationstabellen
                        where k.projektnummer == pNumber
                        select k;
            return query;

        }

        public KalkulationCollection GetCalculationType(int ProjektID)
        {
            if (ProjektID == 0)
            {
                return null;
            }

            string pNumber = this.GetProjektNumberFromID(ProjektID);
            var query = from k in db.kalkulationstabellen
                        where k.projektnummer == pNumber
                        select k;
            return new KalkulationCollection(query, this.db);

        }


        //var query = (from p in dc.GetTable<Person>()
        //             join pa in dc.GetTable<PersonAddress>() on p.Id equals pa.PersonId into tempAddresses
        //             from addresses in tempAddresses.DefaultIfEmpty()
        //             select new { p.FirstName, p.LastName, addresses.State });


        public IQueryable GetLagerListenGroupBezeichnung(string filter, int ID)
        {
            if (filter.Equals(string.Empty))
            {
                var query = from l in db.lagerlisten
                            where l.id != ID
                            orderby l.bezeichnung
                            select l;

                //var gr = query.GroupBy(p => p.bezeichnung);
                return query;
            }
            else
            {
                var query = from l in db.lagerlisten
                            where l.id != ID && l.bezeichnung.Contains(filter)
                            orderby l.bezeichnung
                            select l;

                //var gr = query.GroupBy(p => p.bezeichnung);
                return query;

            }

        }




        public IQueryable GetLagerListenMitOberArtikel(string filter, int ID)
        {
            if (filter.Equals(string.Empty))
            {
                var query = from l in db.lagerlisten
                            join a in db.lagerlisten
                            on l.id_parent equals a.id
                            into tempListen
                            from listens in tempListen.DefaultIfEmpty()
                            where l.id != ID
                            orderby l.bezeichnung
                            select new Tools.LagerListeOberArtikel
                            {
                                bezeichnung = l.bezeichnung,
                                beschreibung = l.beschreibung,
                                oberartikel = listens.bezeichnung,
                                id = l.id,
                                id_parent = l.id_parent == null ? 0 : (int)l.id_parent,
                                artikelnr = l.artikelnr


                            };
                return query;
            }
            else
            {
                var query = from l in db.lagerlisten
                            join a in db.lagerlisten
                            on l.id_parent equals a.id
                            into tempListen
                            from listens in tempListen.DefaultIfEmpty()
                            where l.bezeichnung.Contains(filter) && l.id != ID
                            orderby l.bezeichnung
                            select new Tools.LagerListeOberArtikel
                            {
                                bezeichnung = l.bezeichnung,
                                beschreibung = l.beschreibung,
                                oberartikel = listens.bezeichnung,
                                id = l.id,
                                id_parent = l.id_parent == null ? 0 : (int)l.id_parent,
                                artikelnr = l.artikelnr


                            };
                return query;
            }


        }

        //    public System.Collections.IEnumerable GetLagerlistenBewegung(string ProjektNr)
        public System.Linq.IQueryable GetLagerlistenBewegung(string ProjektNr)
        {
            if (ProjektNr == string.Empty)
            {
                return null;
            }

            var query = from c in db.lagerlisten
                        join o in db.lagerliste_addremove on c.id equals o.id_lagerliste
                        where o.projektnummer == ProjektNr
                        select new { c.id, c.bezeichnung, c.artikelnr, o.addtype, o.anzahl, o.created };

            if (query.Count() == 0)
            {
                return null;
            }

            return (System.Linq.IQueryable)query;
        }


        /// <summary>
        /// Gibt eine Liste der ausführenden Firmen ohne Kunden zurück.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<firma> GetKunden()
        {
            var query = from f in db.firmen
                        where (f.istFirma == 0 || f.istFirma == 2 || f.istFirma == 3) && f.IstKunde == 1
                        orderby f.name
                        select f;
            return query;

        }

        /// <summary>
        /// Gibt eine Liste der Kunden ohne die ausführenden Firmen zurück.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<firma> GetFirmen()
        {
            var query = from f in db.firmen
                        where f.istFirma == 1 || f.istFirma == 10
                        orderby f.sortierung_Integer
                        select f;
            return query;

        }

        public System.Collections.IEnumerable GetSchiff()
        {
            var boat = from s in db.schiffe
                       orderby s.name
                       where s.name != string.Empty
                       select new { s.name };

            return boat.ToList();


        }

        public IEnumerable<StammProjektTyp> GetProjektTyp()
        {
            var type = from t in db.StammProjektTypen
                       orderby t.Projekttyp
                       select t;

            return type;


        }

        public string GetNewProjektnummer()
        {
            var max = from p in db.projekte
                      select p.id;
            int num = max.Max();

            var number = from n in db.projekte
                         where n.id == num
                         select n.projektnummer;


            int newnum = int.Parse(number.First()) + 1;

            return newnum.ToString();

        }

        public string GetNewProjektnummerWithYear()
        {
            try
            {
                string year = DateTime.Now.ToString("yy");
                var number = from n in db.projekte
                             where n.projektnummer.Substring(0, 2) == (year.Trim())
                             select n.projektnummer;
                //                var x = number.Distinct();

                string num = number.Max();

                if (num != null)
                {
                    int newnum = int.Parse(num.Substring(2)) + 1;
                    //int newnum = x.Count() + 1;
                    string pNum = year.Trim() + newnum.ToString().PadLeft(4, '0');
                    return pNum;
                }
                else
                {
                    return year.Trim() + "0001";
                }
            }
            catch (Exception ex)
            {

                throw new Exception(CommonTools.Tools.ErrorMethods.GetExceptionMessageInfo(ex));
            }



        }

        public ProjektCollection GetLikeTest()
        {
            var test = from p in db.projekte
                       where p.projektnummer.Contains("111")
                       select p;
            var res = new ProjektCollection(test, db);
            return res;

        }


        public ProjektCollection GetLikeTest2()
        {
            var p = db.projekte
                .Where("it.type = 'anlage' and it.firmenname = 'Jets As'")
                .OrderBy("it.projektnummer desc");

            var res = new ProjektCollection(p.Skip(4).Take(3), db);
            return res;
        }

        /// <summary>
        /// Löschen der Anlagendaten im Projekt und Einügen der Daten des aktuell ausgewählten Schiffes.
        /// </summary>
        /// <param name="s">Das Schiffsobjekt das ausgewählt wurde</param>
        /// <param name="blView">Die View in der die Anlagen angezeigt werden.</param>
        private void setSchiffAnlagen(schiff s, BindingListCollectionView blView)
        {


            int c = blView.Count - 1;

            for (int i = c; i >= 0; i--)
            {
                blView.RemoveAt(i);
            }


            var anl = from sa in db.SchiffAnlage
                      where sa.id_Schiff == s.id
                      select sa;
            foreach (var item in anl)
            {

                if (blView != null)
                {
                    var agg = (projektAggregat)(blView.AddNew());
                    agg.id_aggregat = item.id_Anlage;
                    agg.Kennzeichen = item.Kennzeichen;
                    agg.Bemerkung = item.Bemerkung;

                    blView.CommitNew();

                }


            }


        }
        public void insertShipData(MainWindow mw, BindingListCollectionView blView)
        {
            if (mw.ftbSchiff.ComboBoxText != null)
            {

                var query = from f in db.schiffe where f.name == mw.ftbSchiff.tBox.Text select f;


                if (query.Count() > 0)
                {


                    schiff ship = query.FirstOrDefault();
                    mw.imonummer.Text = ship.imonummer.ToString();
                    mw.txtWerftname.Text = ship.werftname;
                    mw.txtWerfnummer.Text = ship.werftnummer;
                    setSchiffAnlagen(ship, blView);

                    //switch (mw.cboFirma.SelectedValue == null ? 0 : (int)mw.cboFirma.SelectedValue)
                    switch (mw.Firma_FCB_X.SelectedValue == null ? 0 : (int)mw.Firma_FCB_X.SelectedValue)
                    {
                        case 10014: // "Brunvoll AS"
                            {
                                mw.txtUrsprungsprojekt.Text = ship.brursprungsprojekt;
                                mw.txtBrunvollprojektnummer.Text = ship.brpprojektnr;
                                break;
                            }
                        case 10016: //"Tamrotor Marine Compressors AS":
                            {
                                mw.txtUrsprungsprojekt.Text = ship.taursprungsprojekt;

                                break;
                            }

                        case 10019:  //"Sperre Industri AS":
                            {
                                mw.txtUrsprungsprojekt.Text = ship.spursprungsprojekt;
                                mw.txtBrunvollprojektnummer.Text = ship.spnr;
                                break;
                            }
                        case 10095: //"Finnoy Gear and Propeller AS":
                            {
                                mw.txtUrsprungsprojekt.Text = ship.foursprungsprojekt;

                                break;
                            }
                        case 10164: //"Nyborg AS":
                            {
                                mw.txtUrsprungsprojekt.Text = ship.nyursprungsprojekt;

                                break;
                            }
                        case 10179: //"Tecnicomar S.p.A.":
                            {
                                mw.txtUrsprungsprojekt.Text = ship.teursprungsprojekt;

                                break;
                            }
                        case 10177:  //"Jets AS":
                            {
                                mw.txtUrsprungsprojekt.Text = ship.jtursprungsprojekt;
                                mw.txtBrunvollprojektnummer.Text = ship.jtunitnr;
                                break;
                            }
                        case 11688:  //"Metizoft":
                            {
                                mw.txtUrsprungsprojekt.Text = ship.mzursprungsprojekt;
                                break;
                            }
                        case 12022:  //"MMC Green Technology AS":
                            {
                                mw.txtUrsprungsprojekt.Text = ship.mmursprungsprojekt;
                                break;
                            }

                        default:
                            break;
                    }

                }
            }

        }

        //public TimeSpan GetProjectDurationByDuration(int ProjectID)
        //{
        //    var Duration = new TimeSpan(0);
        //    var result = from p in db.Logs
        //                 where p.ProjectID == ProjectID
        //                 select p;
        //    foreach (var p in result)
        //    {

        //        if (p.StopTime != null && p.Starttime != null)
        //        {
        //            p.Duration = p.StopTime - p.Starttime;
        //            if (p.Duration != null)
        //            {
        //                var ts = (TimeSpan)(p.Duration);
        //                Duration += ts;
        //            }
        //        }
        //    }


        //    return Duration;
        //}



        //public string GetProjectDuration(int ProjectID)
        //{
        //    var ts1 = GetProjectDurationByDuration(ProjectID);
        //    int hours = (ts1.Days * 24) + ts1.Hours;
        //    int days = (int)(hours / 8);
        //    int hoursLeft = hours % 8;
        //    string result = string.Format("{0}Tg {1} St {2} Min", days, hoursLeft, ts1.Minutes);
        //    return result;


        //}
    }

}
