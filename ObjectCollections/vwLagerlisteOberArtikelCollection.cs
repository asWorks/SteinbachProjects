using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using DAL;
using DAL.Tools;
using ProjektDB.Tools;
using System.Windows.Data;




namespace ProjektDB.ObjectCollections
{
    public class vwLagerlisteOberartikelCollection : ObservableCollection<vwLagerListenOberartikel>
    {

        private SteinbachEntities _context;

        public SteinbachEntities Context
        {
            get { return _context; }
        }



        public vwLagerlisteOberartikelCollection(IEnumerable<vwLagerListenOberartikel> Lagerlistetable, SteinbachEntities context)
            : base(Lagerlistetable)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {

                LoggingTool.LogExeption(ex, "LagerlisteCollection", "Constructor");
                //System.Windows.MessageBox.Show("Beim Initialisieren von vwBrunvollAuftragsbestand ist ein Fehler aufgetreten.");
            }



        }

        protected override void InsertItem(int index, vwLagerListenOberartikel item)
        {

            // item.Logs.AssociationChanged += new System.ComponentModel.CollectionChangeEventHandler(Logs_AssociationChanged);
            this.Context.AddTovwLagerListenOberartikel(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {

            try
            {
                // vwLagerListenOberartikel project = this[index];
                using (var db = new SteinbachEntities())
                {
                    int id = this[index].id;
                    var artikel = db.lagerlisten.Where(s => s.id == id);
                    Console.WriteLine(artikel.FirstOrDefault().beschreibung);
                    db.DeleteObject(artikel.FirstOrDefault());
                    db.SaveChanges();
                }




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

