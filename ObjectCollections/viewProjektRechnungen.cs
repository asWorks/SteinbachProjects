using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using DAL;
using DAL.Tools;
using ProjektDB.Tools;





namespace ProjektDB.ObjectCollections
{
    public class viewProjektRechnungen : ObservableCollection<vwProjekt_ProjektRechnungen>
    {

        private readonly SteinbachEntities _context;
        //private GridView   TestGridView;
        public SteinbachEntities Context
        {
            get { return _context; }
        }

       


        public viewProjektRechnungen(IEnumerable<vwProjekt_ProjektRechnungen> viewProRech, SteinbachEntities context)
            : base(viewProRech)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {

                LoggingTool.LogExeption(ex, "viewProjektRechnungen", "Constructor");
                //System.Windows.MessageBox.Show("Beim Initialisieren von vwBrunvollAuftragsbestand ist ein Fehler aufgetreten.");
            }



        }


    }
}

