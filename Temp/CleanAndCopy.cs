using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;


namespace ProjektDB.Temp
{
    public class CleanAndCopy
    {

        public void CopyProjektAnlagen2SChiffAnlagen()
        {

            using (var db = new SteinbachEntities())
            {

                var query = from p in db.schiffe select p;
                foreach (var s in query)
                {
                    if (s.brplantnr != string.Empty )
                    {
                        AddSchiffAnlage(s, s.brplantnr, db);
                    }

                    if (s.brplantnr2 != string.Empty)
                    {
                        AddSchiffAnlage(s, s.brplantnr2, db);
                    }

                    if (s.brplantnr3 != string.Empty)
                    {
                        AddSchiffAnlage(s, s.brplantnr3, db);
                    }

                    if (s.brplantnr4 != string.Empty)
                    {
                        AddSchiffAnlage(s, s.brplantnr4, db);
                    }

                    
                }

                db.SaveChanges();

            }




        }


        private void AddSchiffAnlage(schiff s,string Kennzeichen,SteinbachEntities data)
        {
            var sa = new SchiffAnlage();
            sa.id_Schiff = s.id;
            sa.id_Anlage = 1;
            sa.Kennzeichen = Kennzeichen;
            data.AddToSchiffAnlage(sa);
            

        }




        public void DoAddCustomers()
        {



            using (SteinbachEntities db = new SteinbachEntities())
            {
                var projects = (from p in db.projekte select new { p.kundenname }).Distinct();
                
                foreach (var po in projects)
                {
                    var q = from k in db.firmen where k.name.Equals( po.kundenname,StringComparison.CurrentCulture) select k;
                    if (q.Count() == 0)
                    {
                        var f = new firma();
                        f.name = po.kundenname;
                        f.created = DateTime.Now;
                        f.istFirma = 3;
                        db.firmen.AddObject(f);

                    }

                }


                db.SaveChanges();

                Console.WriteLine("Done . . . . . .");
            }





        }

    }
}
