using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using DAL;
using DAL.Tools;
using ProjektDB.Tools;





namespace ProjektDB.ObjectCollections
{
    public class vwProjektSiRgKundenCollection : ObservableCollection<vw_Projekte_si_rgKunde>
    {

        private SteinbachEntities _context;
      
        public SteinbachEntities Context
        {
            get { return _context; }
        }



        public vwProjektSiRgKundenCollection(IEnumerable<vw_Projekte_si_rgKunde> aggregatetable, SteinbachEntities context)
            :base(aggregatetable)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {

                LoggingTool.LogExeption(ex, "vwProjektSiRgKundenCollection", "Constructor");
                //System.Windows.MessageBox.Show("Beim Initialisieren von vwBrunvollAuftragsbestand ist ein Fehler aufgetreten.");
            }



        }


        protected override void InsertItem(int index, vw_Projekte_si_rgKunde item)
        {

            // item.Logs.AssociationChanged += new System.ComponentModel.CollectionChangeEventHandler(Logs_AssociationChanged);
            this.Context.AddTovw_Projekte_si_rgKunde(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {

            try
            {
                vw_Projekte_si_rgKunde project = this[index];

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

