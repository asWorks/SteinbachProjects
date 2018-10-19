using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

using ProjektDB.ObjectCollections;


namespace ProjektDB.Repositories
{
    class VerlaufRepository
    {


        private SteinbachEntities db;

        public VerlaufRepository(SteinbachEntities dbContext)
        {
            db = dbContext;

        }

        public VerlaufCollection GetVerlaeufe()
        {
            var result = from p in db.projekt_verlaufe
                         select p;
            var pc = new VerlaufCollection(result, this.db);

            return pc;
        }


        public VerlaufCollection GetVerlaeufeByProjektID(int ProjektID)
        {
            var result = from p in db.projekt_verlaufe
                         where p.id_projekt  == ProjektID
                         select p;
            var pc = new VerlaufCollection(result, this.db);

            return pc;
        }








    }

}

