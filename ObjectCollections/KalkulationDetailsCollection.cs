using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using DAL;




namespace ProjektDB.ObjectCollections
{
    public class KalkulationDetailsCollection : ObservableCollection<kalkulationstabelle_detail>
    {

        private readonly SteinbachEntities _context;
        //private GridView   TestGridView;
        public SteinbachEntities Context
        {
            get { return _context; }
        }



        public KalkulationDetailsCollection(IEnumerable<kalkulationstabelle_detail> CalkDetailtable, SteinbachEntities context)
            : base(CalkDetailtable)
        {
            try
            {
                _context = context;
            }
            catch (Exception )
            {

                //LoggingTool.LogExeption(ex, "TemplateCollection", "Constructor");
                //System.Windows.MessageBox.Show("Beim Initialisieren von vwBrunvollAuftragsbestand ist ein Fehler aufgetreten.");
            }



        }

        protected override void InsertItem(int index, kalkulationstabelle_detail item)
        {

            // item.Logs.AssociationChanged += new System.ComponentModel.CollectionChangeEventHandler(Logs_AssociationChanged);
            this.Context.AddTokalkulationstabelle_details(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {

            try
            {
                kalkulationstabelle_detail project = this[index];

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

