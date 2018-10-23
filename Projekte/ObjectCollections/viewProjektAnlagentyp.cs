using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using DAL;




namespace ProjektDB.ObjectCollections
{
    public class viewProjektAnlagentyp : ObservableCollection<vwProjektAnlagentyp>
    {

        private readonly SteinbachEntities _context;
        //private GridView   TestGridView;
        public SteinbachEntities Context
        {
            get { return _context; }
        }

        public viewProjektAnlagentyp(IEnumerable<vwProjektAnlagentyp> viewProAblagentyp, SteinbachEntities context)
            : base(viewProAblagentyp)
        {
            try
            {
                _context = context;
            }
            catch (Exception )             
                
            {

            }



        }


    }
}

