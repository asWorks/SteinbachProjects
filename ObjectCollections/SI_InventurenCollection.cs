using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using DAL;
using DAL.Tools;




namespace ProjektDB.ObjectCollections
{
    public class SI_InventurenCollection : ObservableCollection<SI_Inventuren>
    {

        

        private SteinbachEntities _context;
      
        public SteinbachEntities Context
        {
            get { return _context; }
        }



        public SI_InventurenCollection(IEnumerable<SI_Inventuren> Artikeltable, SteinbachEntities context)
            :base(Artikeltable)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {

                LoggingTool.LogExeption(ex, "SI_InventurenCollection", "Constructor");
                //System.Windows.MessageBox.Show("Beim Initialisieren von vwBrunvollAuftragsbestand ist ein Fehler aufgetreten.");
            }



        }


        protected override void InsertItem(int index, SI_Inventuren item)
        {

            // item.Logs.AssociationChanged += new System.ComponentModel.CollectionChangeEventHandler(Logs_AssociationChanged);
            this.Context.AddToSI_Inventuren(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {

            try
            {
                SI_Inventuren project = this[index];

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

