using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;

namespace ProjektDB.Tools
{
    public static class StaticMethods
    {
        public enum GetMessageResult
        {
            yes,
            no,
            cancel
        }

        private static int ProjektID;

        public static string GetConfigEntry(string key)
        {
            using (var db = new DAL.SteinbachEntities())
            {

                string res = db.config.Where(s => s.mkey == key).FirstOrDefault().value;
                return res;

            }

        }

        public static string GetPath(string filename)
        {
            //string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //int pos = path.IndexOf(@"\bin");
            //path = path.Substring(0, pos) + "\\Belege\\" + filename;
            //return path;

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo fi = new FileInfo(path);
            path = fi.FullName;
            path = path.Replace(fi.Name, filename);

            return path;

        }

        public static int GetSingleProjektID(string Projektnummer)
        {

            using (var db = new DAL.SteinbachEntities())
            {
                var res = db.projekte.Where(n => n.projektnummer == Projektnummer);
                if (res.Count() == 1)
                {

                    return res.SingleOrDefault().id;
                }
                else
                {
                    views.SelectProjekt sp = new views.SelectProjekt(Projektnummer);
                    sp.PushProjektID += new Action<int>(sp_PushProjektID);
                    sp.ShowDialog();
                    return ProjektID;
                }

            }


        }

        static void sp_PushProjektID(int obj)
        {
            ProjektID = obj;
        }


    }
}
