using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CommonTools.Tools;
using Microsoft.Win32;


namespace ProjektDB.Tools
{
    class CallRechnung
    {

        //F:\VERKAUF\BR\Aufträge\2012\E-Teile\120158-S, Tender Donau\Auftrag\Kommerziell

        //BR 100031-S-E R1
        //100031-S-E Eilbek
      


        public CallRechnung(string RNr, DateTime Datum, string SchiffName, string Firma, string Projektnummer, string LandSchiff = "")
        {
            try
            {
                string kurzname = string.Empty;
                string pfad = string.Empty;
                string pfadDetail = string.Empty;
                string year = Datum.ToString("yyyy");
                string Folder1 = string.Empty;
                DirectoryInfo foRechnungen; 

                using (DAL.SteinbachEntities db = new DAL.SteinbachEntities())
                {
                    kurzname = db.firmen.Where(n => n.name == Firma).Single().kurzname;
                    if (LandSchiff != "")
                    {
                        pfad = db.config.Where(i => i.mkey == "PfadSIRechnungenJets").Single().value;
                        Folder1 = string.Format(@pfad, kurzname, year, LandSchiff);
                    }
                    else
                    {
                        pfad = db.config.Where(i => i.mkey == "PfadSIRechnungen").Single().value;
                        Folder1 = string.Format(@pfad, kurzname, year);
                    }
                    
                    pfadDetail = db.config.Where(i => i.mkey == "PfadSIRechnungenDetail").Single().value;

                }



                DirectoryInfo fo1 = new DirectoryInfo(Folder1);

                DirectoryInfo[] fo2 = fo1.GetDirectories(string.Format("*{0}*", Projektnummer));

                if (fo2.Count() == 1)
                {
                    var foProjekt = (DirectoryInfo)fo2[0];
                    //var foRechnungen = new DirectoryInfo(string.Format("{0}\\Auftrag\\Kommerziell", foProjekt.FullName));
                    foRechnungen = new DirectoryInfo(string.Format(@pfadDetail, foProjekt.FullName));

                    FileInfo[] files = foRechnungen.GetFiles(string.Format("{0}.*", RNr));
                    if (files.Count() == 1)
                    {
                        FileInfo fi = (FileInfo)files[0];
                        OpenRechnung(fi.FullName);

                    }
                    else if (files.Count() > 1)
                    {
                        FileInfo fi = (FileInfo)files[0];
                        ShowFiles(fi.FullName);

                    }
                    else if (files.Count() == 0)
                    {
                        ShowFiles(foRechnungen.FullName);
                    }
                }
                else
                {
                    ShowFiles(fo1.FullName);
                }
              
            }
            catch (Exception ex)
            {

                throw new Exception(ErrorMethods.GetExceptionMessageInfo(ex));
            }
        }

        private void ShowFiles(string filename)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = filename;
            dlg.DefaultExt = ".*";
            dlg.Filter = "Alle Dateien(.*)|*.*";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename1 = dlg.FileName;
                OpenRechnung(filename1);

            }
        }


        void OpenRechnung(string FName)
        {

            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            try
            {

                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = FName;
                proc.Start();

            }
            catch (FileNotFoundException)
            {

                throw new FileNotFoundException(string.Format("Datei {0} konnte nicht gefunden werden.", FName));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Fehler beim Öffnen der Datei {0} {1}.", FName, ex.Message));
            }
            finally
            {
                proc.Dispose();
                proc.Close();
            }




        }

    }
}
