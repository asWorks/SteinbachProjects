using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using DAL;
using DAL.Tools;




namespace ProjektDB.ObjectCollections
{
    public class KalkulationCollection : ObservableCollection<kalkulationstabelle>
    {

        private SteinbachEntities _context;
      
        public SteinbachEntities Context
        {
            get { return _context; }
        }



        public KalkulationCollection(IEnumerable<kalkulationstabelle> Calktable, SteinbachEntities context)
            : base(Calktable)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {

                LoggingTool.LogExeption(ex, "KalkulationCollection", "Constructor");
                //System.Windows.MessageBox.Show("Beim Initialisieren von vwBrunvollAuftragsbestand ist ein Fehler aufgetreten.");
            }



        }

        protected override void InsertItem(int index, kalkulationstabelle item)
        {

            // item.Logs.AssociationChanged += new System.ComponentModel.CollectionChangeEventHandler(Logs_AssociationChanged);
            this.Context.AddTokalkulationstabellen(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {

            try
            {
                kalkulationstabelle project = this[index];

                this.Context.DeleteObject(this[index]);
                base.RemoveItem(index);
            }
            catch (Exception ex)
            {

                string Message;
                Message = ex.Message;
                if (ex.InnerException != null)
                {
                    Message += '\n';
                    Message += ex.InnerException.Message;

                }
                throw new Exception(Message);
            }

        }


    }
}

