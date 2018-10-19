using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace Lagerwirtschaft.Buchungen
{
    public class Lagerbuchungen : IDisposable
    {

        SteinbachEntities db;


        public Lagerbuchungen(SteinbachEntities context)
        {
            db = context;
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


        public bool Lagerbuchung(int Quelllager, int Ziellager, int WirkungQuelllager, int WirkungZiellager, int Bewegungsmenge, int id_Artikel, string BewegungsArt, int id_Projekt, int id_Beleg)
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
                    lb.id_Beleg = id_Beleg;
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

        public bool Lagerbuchung(int id_Lagerort, int id_Artikel, int BewegungsMenge)
        {
            try
            {
               
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



        public int GetBestandLagerort(int id_Artikel, int id_Lagerort)
        {
            return Lagerbuchungen.GetLagerbestand(id_Artikel, id_Lagerort);
        }

        public static int GetLagerbestand(int ArtikelID, int LagerortID)
        {
            if (ArtikelID != null && LagerortID != null)
            {
                using (var db = new SteinbachEntities())
                {
                    var lo = db.Lagerbestaende.Where(n => n.id_Lagerliste == ArtikelID && n.id_Lagerort == LagerortID).SingleOrDefault();
                    if (lo != null)
                    {

                        return (int)lo.Lagerbestand;
                    }

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
