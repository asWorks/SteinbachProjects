using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB
{
    public interface IPerson
    {
        int Nachname
        {
            get;
            set;
        }

        void save();
    }
}
