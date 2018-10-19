using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using WaWi.Vorgaenge;

namespace UnitTestProjectDB
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateNewVorgangFromFirma()
        {

            using (var db = new SteinbachEntities())
            {
                var Fa = db.firmen.Where(X => X.id == 14).SingleOrDefault();

                Vorgaenge vg = new Vorgaenge(db);
                SI_Vorgaenge vorgang1 = vg.CreateNewVorgang(Fa);

                SI_Vorgaenge vorgang2 = vg.CreateNewVorgang(Fa.id);

                Assert.AreEqual(vorgang1.firma, vorgang2.firma);
                Assert.AreEqual(vorgang1.firma.id, 14);
                

            }
        }
    }
}
