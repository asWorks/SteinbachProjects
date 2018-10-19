using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using CommonTools.Tools;

namespace ProjektDB.Temp
{
    public class WriteFirmennummerToProjekt
    {

        public void DoWriteFirmannummer()
        {

            try
            {
                using (var db = new SteinbachEntities())
                {
                    var query = from f in db.firmen
                                where f.istFirma == 1
                                select new
                                {
                                    f.KdNr,
                                    f.name
                                };


                    foreach (var item in query)
                    {
                        var project = from p in db.projekte where p.firmenname == item.name select p;
                        foreach (var pro in project)
                        {
                            pro.FirmenNr = item.KdNr;
                        }
                    }

                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));
            }






        }
    }
}
