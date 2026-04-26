using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;


namespace ProjektDB
{
    class AddKunden
    {
        public void DoAddCustomers()
        {

            // Korrekturlauf nach Datenübernahme aud MySql

            //using (SteinbachEntities db = new SteinbachEntities())
            //{
            //    var projects = (from p in db.projekte select new { p.kundenname }).Distinct();

            //    foreach (var po in projects)
            //    {
            //        var q = from k in db.firmen where k.name.Equals( po.kundenname,StringComparison.CurrentCulture) select k;
            //        if (q.Count() == 0)
            //        {
            //            var f = new firma();
            //            f.name = po.kundenname;
            //            f.created = DateTime.Now;
            //            f.istFirma = 3;
            //            db.firmen.AddObject(f);

            //        }

            //    }


            //    db.SaveChanges();

            //    Console.WriteLine("Done . . . . . .");
            }





        //}

    }
}
