using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB
{
    public class PagerHelper
    {  
        public string RecordInfo { get; set; }
        public int recordsPP { get; set; }
        private int CurrentPage { get; set; }
        private int RecordsCount { get; set; }
        private int _toSkip;
        public int toSkip 
     
        { 
            get
            {
             return _toSkip;
            }

            set
            {
              if (value < 0)
                  _toSkip = 0;
              else
                  _toSkip = value;
            }
        
        }
        public int toTake { get; set; }

        public void Init(int Page, int RecordCount,int RecordsPerPage)
        {
           CurrentPage = Page;
           RecordsCount = RecordCount;
           recordsPP = RecordsPerPage;
           toTake = recordsPP;
           toSkip = 0;
           RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage, CurrentPage+recordsPP, RecordsCount);


            
        }

        public void goLast()
        {
            toSkip = RecordsCount - recordsPP;
            CurrentPage = RecordsCount - recordsPP;
            RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage, CurrentPage + recordsPP, RecordsCount);
            
        }
        public void goFirst()
        {
            toSkip = 0;
            CurrentPage = 1;
            RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage, CurrentPage + recordsPP, RecordsCount);

        }

        public void next()
        {
            if (CurrentPage + recordsPP < RecordsCount)
            {
                CurrentPage += recordsPP;
                toSkip = CurrentPage - recordsPP;
            }
            RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage, CurrentPage + recordsPP, RecordsCount);



        }

        public void previous()
        {
            if (CurrentPage - recordsPP < 0)
                goFirst();
            else
            {
                CurrentPage -= recordsPP;
                toSkip = CurrentPage - recordsPP;
            }

            RecordInfo = String.Format("Datensatz {0} bis {1} von {2}", CurrentPage, CurrentPage + recordsPP, RecordsCount);
        
        }




        



    }
}
