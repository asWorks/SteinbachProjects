using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB.Tools
{
    public class EditFilterSettings
    {
        public static string LagetlisteLieferant(string filter)
        {
            if (filter == string.Empty)
            {
                return string.Empty;
            }
           // filter = "it.id_lieferant = jets"
            int f = filter.IndexOf("=");
            string buf = filter.Substring(f + 1);
            

            buf = buf.Replace('"', ' ');
            buf = buf.Replace('%', ' ');
            buf = buf.Trim();

            using (var db = new DAL.SteinbachEntities() )
            {
                var res = db.firmen.Where(n => n.name.StartsWith(buf) && n.istFirma == 1);
                var x = res.DefaultIfEmpty(null);
                Console.WriteLine(x.ToString());
                if (res.Count() > 1)
                {
                    CommonTools.Tools.UserMessage.NotifyUser("Filter ergibt meherere Resultate - Filtern nicht möglich.");
                    return "";
                }
                else
	{
                    int id = res.SingleOrDefault().id;
                    filter = filter.Substring(0,f+1);
                    filter += id; 
                   // filter += '"';
	}
            }


            return filter;

        }
    }
}
