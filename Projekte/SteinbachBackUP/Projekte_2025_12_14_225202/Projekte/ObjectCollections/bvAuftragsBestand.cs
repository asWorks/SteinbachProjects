using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Collections;
using DAL;




namespace ProjektDB
{
    public class bvAuftragsBestand : ObservableCollection<vwBrunvollAuftragsbestand>
    {

        private readonly SteinbachEntities _context;
     
        public SteinbachEntities Context
        {
            get { return _context; }
        }

       


       


        public bvAuftragsBestand(IEnumerable<vwBrunvollAuftragsbestand> bvAutraege, SteinbachEntities context)
            : base(bvAutraege)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {

                //LoggingTool.LogExeption(ex, "TemplateCollection", "Constructor");
                System.Windows.MessageBox.Show("Beim Initialisieren von vwBrunvollAuftragsbestand ist ein Fehler aufgetreten.");
            }



        }


    }
}

