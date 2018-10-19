using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace WaWi.Lagerbuchungen.Lagerbuchungen
{
    public class Lagerbuchungen : IDisposable
    {


        public enum LagerbuchungResult
        {
            BelegartNichtBuchbar,
            PositionsbuchungFehlgeschlagen,
            BelegUnvollständig,
            PositionIstHandelsware,
            BuchungOK
        }

        SteinbachEntities db;


        public Lagerbuchungen(SteinbachEntities context)
        {
            db = context;

        }

        ~Lagerbuchungen()
        {
            Dispose();
        }


        public static void TransferLagerbestaende()
        {
            using (var db = new SteinbachEntities())
            {
                var ll = db.lagerlisten;
                foreach (var item in ll)
                {
                    var lb = new Lagerbestaende();
                    lb.id_Lagerliste = item.id;
                    lb.id_Lagerort = 1;
                    lb.Lagerbestand = item.anzahlauflager;
                    db.AddToLagerbestaende(lb);
                }

                db.SaveChanges();

            }
        }


        public bool Lagerbuchung(int Quelllager, int Ziellager, int WirkungQuelllager, int WirkungZiellager, int Bewegungsmenge, int id_Artikel,
                                 string BewegungsArt, int id_Projekt, SI_BelegePositionen BelegPosition)
        {


            try
            {

                bool resQ = false;
                bool resZ = false;

                var lb = new lagerliste_addremove();

                lb.isConfirmed = 0;
                lb.created = DateTime.Now;
                lb.id_Quelllager = Quelllager;
                lb.id_Ziellager = Ziellager;
                lb.id_lagerliste = id_Artikel;
                lb.anzahl = Bewegungsmenge;
                lb.addtype = BewegungsArt;
                lb.id_projekt = id_Projekt;
                lb.SI_BelegePositionen = BelegPosition;
                lb.Quelllager_BestandAlt = GetBestandLagerort(id_Artikel, Quelllager);
                lb.Ziellager_BestandAlt = GetBestandLagerort(id_Artikel, Ziellager);
                db.AddTolagerliste_addremove(lb);
                // db.SaveChanges();
                if (WirkungQuelllager != 0)
                {
                    resQ = Lagerbuchung(Quelllager, id_Artikel, Bewegungsmenge * WirkungQuelllager);

                }
                else
                {
                    resQ = true;
                }

                resZ = Lagerbuchung(Ziellager, id_Artikel, Bewegungsmenge * WirkungZiellager);

                if (resQ == true && resZ == true)
                {
                    lb.isConfirmed = -1;
                    // db.SaveChanges();
                }

                lb.Quelllager_BestandNeu = GetBestandLagerort(id_Artikel, Quelllager);
                lb.Ziellager_BestandNeu = GetBestandLagerort(id_Artikel, Ziellager);

                // db.SaveChanges();
                return true;


            }

            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Lagerbuchung  fehlgeschlagen.");

                return false;
            }

        }




        public bool Lagerbuchung(int Quelllager, int Ziellager, int WirkungQuelllager, int WirkungZiellager, int Bewegungsmenge, int id_Artikel,
                                    string BewegungsArt, int id_Projekt, int id_BelegPosition)
        {


            try
            {

                bool resQ = false;
                bool resZ = false;

                var lb = new lagerliste_addremove();

                lb.isConfirmed = 0;
                lb.created = DateTime.Now;
                lb.id_Quelllager = Quelllager;
                lb.id_Ziellager = Ziellager;
                lb.id_lagerliste = id_Artikel;
                lb.anzahl = Bewegungsmenge;
                lb.addtype = BewegungsArt;
                lb.id_projekt = id_Projekt;
                lb.id_BelegPosition = id_BelegPosition;
                lb.Quelllager_BestandAlt = GetBestandLagerort(id_Artikel, Quelllager);
                lb.Ziellager_BestandAlt = GetBestandLagerort(id_Artikel, Ziellager);
                db.AddTolagerliste_addremove(lb);
                // db.SaveChanges();
                if (WirkungQuelllager != 0)
                {
                    resQ = Lagerbuchung(Quelllager, id_Artikel, Bewegungsmenge * WirkungQuelllager);

                }
                else
                {
                    resQ = true;
                }

                resZ = Lagerbuchung(Ziellager, id_Artikel, Bewegungsmenge * WirkungZiellager);

                if (resQ == true && resZ == true)
                {
                    lb.isConfirmed = -1;
                    // db.SaveChanges();
                }

                lb.Quelllager_BestandNeu = GetBestandLagerort(id_Artikel, Quelllager);
                lb.Ziellager_BestandNeu = GetBestandLagerort(id_Artikel, Ziellager);

                // db.SaveChanges();
                return true;


            }

            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Lagerbuchung  fehlgeschlagen.");

                return false;
            }

        }


        public bool Lagerbuchung(int Quelllager, int Ziellager, int WirkungQuelllager, int WirkungZiellager, int Bewegungsmenge, int id_Artikel,
                                string BewegungsArt, int id_Projekt, int id_BelegPosition, int id_Firma)
        {


            try
            {

                bool resQ = false;
                bool resZ = false;

                var lb = new lagerliste_addremove();

                lb.isConfirmed = 0;
                lb.created = DateTime.Now;
                lb.id_Quelllager = Quelllager;
                lb.id_Ziellager = Ziellager;
                lb.id_lagerliste = id_Artikel;
                lb.anzahl = Bewegungsmenge;
                lb.addtype = BewegungsArt;
                lb.id_projekt = id_Projekt;
                lb.id_BelegPosition = id_BelegPosition;
                lb.id_Firma = id_Firma;
                lb.Quelllager_BestandAlt = GetBestandLagerort(id_Artikel, Quelllager);
                lb.Ziellager_BestandAlt = GetBestandLagerort(id_Artikel, Ziellager);
                db.AddTolagerliste_addremove(lb);
                // db.SaveChanges();
                if (WirkungQuelllager != 0)
                {
                    resQ = Lagerbuchung(Quelllager, id_Artikel, Bewegungsmenge * WirkungQuelllager);

                }
                else
                {
                    resQ = true;
                }

                resZ = Lagerbuchung(Ziellager, id_Artikel, Bewegungsmenge * WirkungZiellager);

                if (resQ == true && resZ == true)
                {
                    lb.isConfirmed = -1;
                    // db.SaveChanges();
                }

                lb.Quelllager_BestandNeu = GetBestandLagerort(id_Artikel, Quelllager);
                lb.Ziellager_BestandNeu = GetBestandLagerort(id_Artikel, Ziellager);

                // db.SaveChanges();
                return true;


            }

            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Lagerbuchung  fehlgeschlagen.");

                return false;
            }

        }

        public LagerbuchungResult Lagerbuchung(SI_Belege Beleg)
        {
            bool posError = false;
            var bw = (StammBewegungsarten)Beleg.StammBelegarten.StammBewegungsarten;
            if (bw.Lagerbuchung == 0)
            {
                return LagerbuchungResult.BelegartNichtBuchbar;
            }



            foreach (var item in Beleg.SI_BelegePositionen)
            {
                try
                {

                    if ((int)item.Handelsware != 1)
                    {
                        bool resQ = false;
                        bool resZ = false;
                        var belegart = db.StammBelegarten.Where(n => n.id == Beleg.Belegart).SingleOrDefault();
                        var lb = new lagerliste_addremove();

                        lb.id_user = Session.User.id;

                        lb.isConfirmed = 0;
                        lb.created = DateTime.Now;
                        lb.id_Quelllager = Beleg.id_Quelllager;
                        lb.id_Ziellager = Beleg.id_Ziellager;
                        lb.id_lagerliste = item.id_Artikel;
                        // lb.bemerkung = item.SI_Belege.Bemerkung;
                        lb.anzahl = (int)item.Menge;
                        lb.addtype = bw.id;
                        lb.id_projekt = Beleg.id_Projekt ?? 0;
                        lb.id_BelegPosition = item.id;
                        lb.id_Firma = Beleg.id_Firma;
                        lb.Quelllager_BestandAlt = GetBestandLagerort((int)item.id_Artikel, (int)Beleg.id_Quelllager);
                        lb.Ziellager_BestandAlt = GetBestandLagerort((int)item.id_Artikel, (int)Beleg.id_Ziellager);
                        db.AddTolagerliste_addremove(lb);
                        if (bw.WirkungQuelllager != 0)
                        {
                            resQ = Lagerbuchung((int)Beleg.id_Quelllager, (int)item.id_Artikel, (int)item.Menge * (int)bw.WirkungQuelllager);

                        }
                        else
                        {
                            resQ = true;
                        }
                        // Änderung Wirkung Belegart
                        resZ = Lagerbuchung((int)Beleg.id_Ziellager, (int)item.id_Artikel, (int)item.Menge * (int)bw.WirkungZiellager);

                        if (resQ == true && resZ == true)
                        {
                            lb.isConfirmed = -1;
                            item.istGebucht = 1;
                            // db.SaveChanges();
                        }

                        lb.Quelllager_BestandNeu = GetBestandLagerort((int)item.id_Artikel, (int)Beleg.id_Quelllager);
                        lb.Ziellager_BestandNeu = GetBestandLagerort((int)item.id_Artikel, (int)Beleg.id_Ziellager);

                        // db.SaveChanges();

                    }
                }


                catch (Exception ex)
                {
                    CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Lagerbuchung  fehlgeschlagen.");
                    posError = true;
                }
            }
            if (posError)
            {
                return LagerbuchungResult.PositionsbuchungFehlgeschlagen;
            }
            else
            {
                return LagerbuchungResult.BuchungOK;
            }
        }

        public void ConsolidateLagerbestaende()
        {

            try
            {

                Console.WriteLine("ConsolidateLagerbestaende");
                var duplicates = (from item in db.Lagerbestaende
                                  group item by new { Lagerliste = item.id_Lagerliste, Lagerort = item.id_Lagerort } into g

                                  where g.Count() > 1
                                  select g);



                foreach (var item in duplicates)
                {
                    Console.WriteLine(item.Key.Lagerliste);
                    var bst = new Lagerbestaende();
                    db.AddToLagerbestaende(bst);
                    bst.Lagerbestand = 0;
                    foreach (Lagerbestaende lb in item.ToList())
                    {
                        bst.id_Lagerliste = lb.id_Lagerliste;
                        bst.id_Lagerort = lb.id_Lagerort;
                        bst.Lagerbestand += lb.Lagerbestand;
                        bst.letzteBuchung = lb.letzteBuchung;
                        bst.Mindestbestand = lb.Mindestbestand;
                        bst.Sollbestand = lb.Sollbestand;
                        db.DeleteObject(lb);

                    }

                }

                db.SaveChanges();

                Console.WriteLine("Ende - ConsolidateLagerbestaende");

            }
            catch (Exception)
            {

                throw;
            }
        }




        public bool Lagerbuchung(int id_Lagerort, int id_Artikel, int BewegungsMenge)
        {
            try
            {
                using (var dbAddNew = new SteinbachEntities())
                {
                    var lbAdd = db.Lagerbestaende.Where(n => n.id_Lagerliste == id_Artikel && n.id_Lagerort == id_Lagerort).SingleOrDefault();
                    if (lbAdd == null)
                    {
                        lbAdd = new Lagerbestaende();
                        lbAdd.id_Lagerliste = id_Artikel;
                        lbAdd.id_Lagerort = id_Lagerort;
                        lbAdd.Lagerbestand = 0;
                        dbAddNew.AddToLagerbestaende(lbAdd);
                        dbAddNew.SaveChanges();

                    }
                }



                var lb = db.Lagerbestaende.Where(n => n.id_Lagerliste == id_Artikel && n.id_Lagerort == id_Lagerort).SingleOrDefault();
                if (lb == null)
                {
                    lb = new Lagerbestaende();
                    lb.id_Lagerliste = id_Artikel;
                    lb.id_Lagerort = id_Lagerort;
                    lb.Lagerbestand = 0;
                    db.AddToLagerbestaende(lb);
                }

                if (!lb.Lagerbestand.HasValue)
                {
                    lb.Lagerbestand = 0;
                }

                lb.Lagerbestand += BewegungsMenge;
                lb.letzteBuchung = DateTime.Now;
                // db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Lagerbuchung fehlgeschlagen.");

                return false;
                // throw;

            }





        }


        public int GetBestandLagerort(string Artikelnummer, int id_Lagerort)
        {

            if (Artikelnummer == null)
            {
                throw new ArgumentNullException("Artikelnummer kann nicht null sein");
            }

            var Art = db.lagerlisten.Where(n => n.artikelnr == Artikelnummer);

            if (!Art.Any())
            {
                throw new KeyNotFoundException("Artikelnummer ist nicht vorhanden");
            }
            if (Art.Count() > 1)
            {
                throw new ArgumentOutOfRangeException("Artikelnummer kann nicht mehrfach vorhanden sein", Artikelnummer);
            }


            return Lagerbuchungen.GetLagerbestand(Art.SingleOrDefault().id, id_Lagerort);
        }


        public int GetBestandLagerort(int id_Artikel, int id_Lagerort)
        {
            return Lagerbuchungen.GetLagerbestand(id_Artikel, id_Lagerort);
        }

        public static int GetLagerbestand(int ArtikelID, int LagerortID)
        {
            if (ArtikelID != null && LagerortID != null)
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var lo = db.Lagerbestaende.Where(n => n.id_Lagerliste == ArtikelID && n.id_Lagerort == LagerortID).SingleOrDefault();
                        if (lo != null)
                        {
                            if (lo.Lagerbestand.HasValue)
                            {
                                return (int)lo.Lagerbestand;
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Fehler in Lagerbuchungen.GetLagerbestand");
                    sb.AppendLine("ArtikelID : " + ArtikelID);
                    sb.AppendLine("LagerortID : " + LagerortID);


                    CommonTools.Tools.ErrorMethods.HandleStandardError(ex, sb.ToString());
                }



            }

            return 0;

        }


        public void Dispose()
        {
            db = null;
        }
    }
}
