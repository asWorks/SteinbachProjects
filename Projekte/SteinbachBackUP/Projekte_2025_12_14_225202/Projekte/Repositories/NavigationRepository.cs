using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using ProjektDB.ObjectCollections;


namespace ProjektDB.Repositories
{
    public class NavigationRepository
    {

             public int RecordCount { get; private set; }
            private SteinbachEntities db;

            public NavigationRepository(SteinbachEntities  dbContext)
            {
                db = dbContext;

            }


            public bvAuftragsBestand GetBrunvollViewData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<vwBrunvollAuftragsbestand> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.vwBrunvollAuftragsbestand
                          .OrderBy("it.created desc");
                }
                else
                {
                    p = db.vwBrunvollAuftragsbestand
                         .Where(filter)
                        .OrderBy("it.created desc");
                }

                RecordCount = p.Count();

                var res = new bvAuftragsBestand(p.Skip(toSkip).Take(toTake), db);
                return res;
            }


          

            public ProjektCollection GetProjektData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<projekt> p;
              

                if (filter == string.Empty || filter == null)
                {
                    p = db.projekte
                          .OrderBy("it.created desc");
                }
                else
                {
                    
                    p = db.projekte
                        .Where(filter)
                        .OrderBy("it.created desc");
                }

                RecordCount = p.Count();

                var res = new ProjektCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }

            public ProjektMitVerlaufCollection GetProjektMitVerlaufData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<vwProjektMitVerlauf> p;


                if (filter == string.Empty || filter == null)
                {
                    p = db.vwProjektMitVerlauf
                          .OrderBy("it.created desc");
                }
                else
                {

                    p = db.vwProjektMitVerlauf
                        .Where(filter)
                        .OrderBy("it.created desc");
                }

                RecordCount = p.Count();

                var res = new ProjektMitVerlaufCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }
            public viewProjektAnlagentyp GetProjektAnlagentypData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<vwProjektAnlagentyp> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.vwProjektAnlagentyp
                          .OrderBy("it.created desc");
                }
                else
                {
                    p = db.vwProjektAnlagentyp
                         .Where(filter)
                        .OrderBy("it.created desc");
                }

                RecordCount = p.Count();

                var res = new viewProjektAnlagentyp(p.Skip(toSkip).Take(toTake), db);
                return res;
            }

            public viewProjektRechnungen GetProjektRechnungenData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<vwProjekt_ProjektRechnungen> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.vwProjekt_ProjektRechnungen
                          .OrderBy("it.created desc");
                }
                else
                {
                    p = db.vwProjekt_ProjektRechnungen
                         .Where(filter)
                        .OrderBy("it.created desc");
                }

                RecordCount = p.Count();

                var res = new viewProjektRechnungen(p.Skip(toSkip).Take(toTake), db);
                return res;
            }


            //public viewProjektRechnungenKommissionen GetProjektRechnungenKommissionData(string filter, int toSkip, int toTake)
            //{
            //    System.Data.Objects.ObjectQuery<Vw_Projekt_Rechnungen_Kommissionen> p;

            //    if (filter == string.Empty || filter == null)
            //    {
            //        p = db.Vw_Projekt_Rechnungen_Kommissionen
            //              .OrderBy("it.created desc");
            //    }
            //    else
            //    {
            //        p = db.Vw_Projekt_Rechnungen_Kommissionen
            //             .Where(filter)
            //            .OrderBy("it.created desc");
            //    }

            //    RecordCount = p.Count();

            //    var res = new viewProjektRechnungenKommissionen(p.Skip(toSkip).Take(toTake), db);
            //    return res;
            //}


            public KalkulationCollection GetKalkulationenData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<kalkulationstabelle> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.kalkulationstabellen
                          .OrderBy("it.created desc");
                }
                else
                {
                    p = db.kalkulationstabellen
                         .Where(filter)
                        .OrderBy("it.created desc");
                }

                RecordCount = p.Count();

                var res = new KalkulationCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }

            public SchiffCollection GetSchiffeData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<schiff> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.schiffe
                          .OrderBy("it.name");
                }
                else
                {
                    p = db.schiffe
                         .Where(filter)
                        .OrderBy("it.name");
                }

                RecordCount = p.Count();

                var res = new SchiffCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }

            public FirmaCollection GetFirmaData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<firma> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.firmen
                          .OrderBy("it.name");
                }
                else
                {
                    p = db.firmen
                         .Where(filter)
                        .OrderBy("it.name");
                }

                RecordCount = p.Count();

                var res = new FirmaCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }

            public PersonCollection GetPersonData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<person> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.personen
                          .OrderBy("it.nachname");
                }
                else
                {
                    p = db.personen
                         .Where(filter)
                        .OrderBy("it.nachname");
                }

                RecordCount = p.Count();

                var res = new PersonCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }




            public SI_BelegeCollection GetSI_BelegeData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<SI_Belege> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.SI_Belege
                          .OrderBy("it.Belegdatum desc");
                }
                else
                {
                    p = db.SI_Belege
                         .Where(filter)
                        .OrderBy("it.Belegdatum desc");
                }

                RecordCount = p.Count();

                var res = new SI_BelegeCollection (p.Skip(toSkip).Take(toTake), db);
                return res;
            }


            public LagerorteCollection GetLagerorteData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<StammLagerorte> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.StammLagerorte
                          .OrderBy("it.Lagerortname desc");
                }
                else
                {
                    p = db.StammLagerorte
                         .Where(filter)
                        .OrderBy("it.Lagerortname");
                }

                RecordCount = p.Count();

                var res = new LagerorteCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }


            public TextbausteinCollection GetTextbausteineData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<StammTextbaustein> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.StammTextbausteine
                          .OrderBy("it.Caption");
                }
                else
                {
                    p = db.StammTextbausteine
                         .Where(filter)
                        .OrderBy("it.Caption");
                }

                RecordCount = p.Count();

                var res = new TextbausteinCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }


            public SI_InventurenCollection GetSI_InventurenData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<SI_Inventuren> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.SI_Inventuren
                          .OrderBy("it.Inventurdatum desc");
                }
                else
                {
                    p = db.SI_Inventuren
                         .Where(filter)
                        .OrderBy("it.Inventurdatum desc");
                }

                RecordCount = p.Count();

                var res = new SI_InventurenCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }





            public ZahlungsbedingungCollection GetStammZahlungsbedingungData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<StammZahlungsbedingungen> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.StammZahlungsbedingungen
                          .OrderBy("it.Zahlungsbedingung");
                }
                else
                {
                    p = db.StammZahlungsbedingungen
                         .Where(filter)
                        .OrderBy("it.Zahlungsbedingung");
                }

                RecordCount = p.Count();

                var res = new ZahlungsbedingungCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }

            public AggregatCollection GetAggregatData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<Stamm_Aggregate> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.Stamm_Aggregate
                          .OrderBy("it.aggregat");
                }
                else
                {
                    p = db.Stamm_Aggregate
                         .Where(filter)
                        .OrderBy("it.aggregat");
                }

                RecordCount = p.Count();

                var res = new AggregatCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }

            //public vwLagerlisteOberartikelCollection GetLagerlisteData(string filter, int toSkip, int toTake)
            //{
            //    System.Data.Objects.ObjectQuery<vwLagerListenOberartikel> p;

            //    if (filter == string.Empty || filter == null)
            //    {
            //        p = db.vwLagerListenOberartikel
            //              .OrderBy("it.bezeichnung");
            //    }
            //    else
            //    {
            //        p = db.vwLagerListenOberartikel
            //             .Where(filter)
            //            .OrderBy("it.bezeichnung");
            //    }

            //    RecordCount = p.Count();


            //    var res = new vwLagerlisteOberartikelCollection(p.Skip(toSkip).Take(toTake), db);
            //    return res;
            //}

            public LagerlisteCollection GetLagerlisteData(string filter, int toSkip, int toTake)
            {
                System.Data.Objects.ObjectQuery<lagerliste> p;

               // filter = Tools.EditFilterSettings.LagetlisteLieferant(filter);   funktioniert noch nicht !!

                if (filter == string.Empty || filter == null)
                {
                    p = db.lagerlisten
                          .OrderBy("it.bezeichnung");
                }
                else
                {
                    p = db.lagerlisten
                         .Where(filter)
                        .OrderBy("it.bezeichnung");
                }

                RecordCount = p.Count();


                var res = new LagerlisteCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }

            public vwLagerlisteOberartikelCollection GetLagerlisteData()
            {
                System.Data.Objects.ObjectQuery<vwLagerListenOberartikel> p = db.vwLagerListenOberartikel
                          .OrderBy("it.bezeichnung");

                RecordCount = p.Count();

                var res = new vwLagerlisteOberartikelCollection(p, db);
                return res;
            }



            public vwProjektSiRgKundenCollection GetOffeneEingaengeData(string filter, int toSkip, int toTake)
            {

                //var q = from it in db.vw_Projekte_si_rgKunde
                //        where it.datum >= new DateTime(2011, 01, 01) && it.datum <= new DateTime(2012, 12, 31) && ((it.rechnungfaellig != null) || it.rechnungan != null) && (it.rechnungvom == null)
                //        select it;


                //int n = q.Count();

                
                System.Data.Objects.ObjectQuery<vw_Projekte_si_rgKunde> p;

                if (filter == string.Empty || filter == null)
                {
                    p = db.vw_Projekte_si_rgKunde
                          .OrderBy("it.created desc");
                }
                else
                {
                    p = db.vw_Projekte_si_rgKunde
                         .Where(filter)
                         .OrderBy("it.created desc");
                }

                RecordCount = p.Count();

                var res = new vwProjektSiRgKundenCollection(p.Skip(toSkip).Take(toTake), db);
                return res;
            }
            

    }
}
