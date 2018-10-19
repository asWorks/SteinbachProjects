using System;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System.Linq;
using Caliburn.Micro;


namespace UnitTestProjectDB
{
    [TestClass]
    public class SI_BelegeTest
    {
        [TestMethod]
        public void SIBelegeViewModel_Rechnung_ReturnsValidRechnung_()
        {

            using (var db = new SteinbachEntities())
            {

                //string guid =  Guid.NewGuid().ToString();
                //Console.WriteLine(guid);


                var disp = System.Windows.Threading.Dispatcher.CurrentDispatcher;
                var Cur = System.Windows.Input.Cursors.Wait;


                var BelegeVM = new ProjektDB.ViewModels.SI_BelegeViewModel(0, disp, Cur);
                BelegeVM.InitModel(0);
                BelegeVM.SelectedBelegarten = db.StammBelegarten.Where(n => n.id == "re").SingleOrDefault();
                var p = db.projekte.Where(n => n.projektnummer == "140589").SingleOrDefault();
               

                var query = from po in db.projekte
                            where po.projektnummer == "140589"
                            select new ProjektDB.Models.ProjekteSource
                            {
                                projektnummer = p.projektnummer,
                                type = p.type,
                                firmenname = p.firmenname,
                                kundenname = p.kundenname,
                                projekt_schiff = p.projekt_schiff,
                                id = p.id

                            };

                BelegeVM.SelectedProjekte = query.SingleOrDefault();
                BelegeVM.SelectedKalkulation = db.kalkulationstabellen.Where(n => n.inhalt == "Test140589-29.07.2014").SingleOrDefault();
                BelegeVM.ImportKalkulation(BelegeVM.SelectedKalkulation,false);
                BelegeVM.SelectedFirmenAdressen = BelegeVM.FirmenAdressen[0];
               
                BelegeVM.RecalcThis();
                var bPos = BelegeVM.BelegePositionen[0];
                var zz = BelegeVM.Zusatzzeilen[0];

                Assert.AreEqual(BelegeVM.SelectedFirmenAdressen.ZusatzInfo.Substring(0, 3), "Lür");  
                Assert.AreEqual(BelegeVM.SelectedFirmenAdressen.ZusatzInfo2,"z. Hd. Herrn Meyer");
                Assert.AreEqual(bPos.PreisVorRabatt,82.00m);
                Assert.AreEqual(bPos.Menge, 2);
                Assert.AreEqual(bPos.Endpreis, 164.00m);

                Assert.AreEqual(BelegeVM.BelegePositionen.Count, 4);
                Assert.AreEqual(BelegeVM.Zusatzzeilen.Count, 1);

                Assert.AreEqual(zz.Prozent, 19);
                Assert.AreEqual(Math.Round((decimal)zz.Zeilenwert, 2), 38.33m);
                Assert.AreEqual(Math.Round((decimal)zz.Zeilensaldo, 2), 240.07m);

                Assert.AreEqual(Math.Round((decimal)BelegeVM.Belegsumme, 2), 240.07m);
                Assert.AreEqual(Math.Round((decimal)BelegeVM.SummeBelegPositionen, 2), 201.74m);

                Assert.IsNotNull(BelegeVM.ZahlungsbedingungText);
               // Assert.IsNotNull(BelegeVM.Belegtext);
                


            }



        }

        [TestMethod]
        public void WaWi_AddArtikelAndCreateValidRecord ()
        {
            
            
            using (var db = new SteinbachEntities())
            {

                Session.Login("J. Steinbach", "jörg");

                string guid =  Guid.NewGuid().ToString();
                guid = guid.Replace("-", "");
               

                var ll = new DAL.lagerliste();
                ll.artikelnr = guid;
                ll.beschreibung = DateTime.Now.ToString();
                ll.id_lieferant = 14;

                ll.einheit = "Pcs";
                ll.preiseuro = 10.00m;
                ll.preisbrutto = 11.90m;

                db.AddTolagerlisten(ll);
                db.SaveChanges();

                var bu = new WaWi.Lagerbuchungen.Lagerbuchungen.Lagerbuchungen(db);
               
               

                var disp = System.Windows.Threading.Dispatcher.CurrentDispatcher;
                var Cur = System.Windows.Input.Cursors.Wait;


                var BelegeVM = new ProjektDB.ViewModels.SI_BelegeViewModel(0, disp, Cur);
                BelegeVM.InitModel(0);
                BelegeVM.SelectedBelegarten = db.StammBelegarten.Where(n => n.id == "we").SingleOrDefault();
                BelegeVM.SelectedFirmen = GetFirmensourceByID(14, db);


                BelegeVM.AddScannerPosition(guid, "5");
                BelegeVM.AddScannerPosition(guid, "7");
                BelegeVM.id_user = Session.User.id;

                BelegeVM.DoSaveChanges();
                int bestand = bu.GetBestandLagerort(guid, 1);
                Assert.AreEqual(bestand, 12);

                db.SaveChanges();
              
 
            
            }
        }


        private ProjektDB.Models.FirmenSource GetFirmensourceByKdNr(int? KdNr,SteinbachEntities db)
        {
            var Q = from f in db.firmen
                    where f.KdNr == KdNr && f.IstKunde == 1
                    select new ProjektDB.Models.FirmenSource
                    {
                        id = f.id,
                        name = f.name,
                        Zahlungsbedingungen = f.Zahlungsbedingungen
                    };
            return Q.SingleOrDefault();
        }

        private ProjektDB.Models.FirmenSource GetFirmensourceByID(int Id,SteinbachEntities db)
        {
            var Q = from f in db.firmen
                    where f.id == Id
                    select new ProjektDB.Models.FirmenSource
                    {
                        id = f.id,
                        name = f.name,
                        Zahlungsbedingungen = f.Zahlungsbedingungen
                    };
            return Q.SingleOrDefault();
        }


    }
}
