using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ProjektDB.Temp
{
    public class GenderList:ObservableCollection<GenderType>
    {
     
        public GenderList()
        {
            this.Add(new GenderType("Herr",1));
            this.Add(new GenderType("Frau",0));
        }
    }

    public class GenderType
    {
        public string Gender { get; set; }
        public short GenderID { get; set; }
        public GenderType(string sGender,short sID)
        {
            Gender = sGender;
            GenderID = sID;
        }
    }

}
