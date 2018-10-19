using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using DAL;
using DAL.Tools;
using ProjektDB.Tools;




namespace ProjektDB.ObjectCollections
{
    public class LagerlisteAddRemoveCollection : ObservableCollection<lagerliste_addremove>
    {

        private SteinbachEntities _context;
      
        public SteinbachEntities Context
        {
            get { return _context; }
        }



        public LagerlisteAddRemoveCollection(IEnumerable<lagerliste_addremove> Lagerlistetable, SteinbachEntities context)
            : base(Lagerlistetable)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {

                LoggingTool.LogExeption(ex, "LagerlisteAddRemoveCollection", "Constructor");
                //System.Windows.MessageBox.Show("Beim Initialisieren von vwBrunvollAuftragsbestand ist ein Fehler aufgetreten.");
            }



        }

        protected override void InsertItem(int index, lagerliste_addremove item)
        {

            // item.Logs.AssociationChanged += new System.ComponentModel.CollectionChangeEventHandler(Logs_AssociationChanged);
            this.Context.AddTolagerliste_addremove(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {

            try
            {
                lagerliste_addremove project = this[index];

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

