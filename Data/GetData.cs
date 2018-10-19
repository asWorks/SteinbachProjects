using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;


namespace ProjektDB.Data
{
    public class GetData
    {

        public SI_Projekt.Verlauf_PersonDataTable GetVerlauf(int PID)
        {
            using (var ta = new SI_ProjektTableAdapters.Verlauf_PersonTableAdapter())
            {
                var dt = new SI_Projekt.Verlauf_PersonDataTable();
                ta.Fill(dt,PID);

                return dt;

            
            }
  
        }

        public void UpdateVerlaufPerson(SI_Projekt.Verlauf_PersonDataTable dt)
        {

            var ta = new SI_ProjektTableAdapters.Verlauf_PersonTableAdapter();
            ta.Update(dt);

            ta.Dispose();
        }

    }
}
