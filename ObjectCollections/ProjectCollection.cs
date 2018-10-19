using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using DAL;

namespace ProjektDB.ObjectCollections
{


    public class ProjektCollection : ObservableCollection<projekt>
    {

        private readonly SteinbachEntities _context;
        public SteinbachEntities Context
        {
            get { return _context; }
        }
        public ProjektCollection(IEnumerable<projekt> Projekte, SteinbachEntities context)
            : base(Projekte)
        {
            _context = context;

            //foreach (var p in Projekte)
            //{
            //    p.projekt_verlauf.AssociationChanged += new System.ComponentModel.CollectionChangeEventHandler(projekt_verlauf_AssociationChanged);
            //}
        }

        //void projekt_verlauf_AssociationChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        //{
        //    if (e.Action == System.ComponentModel.CollectionChangeAction.Remove)
        //        this._context.DeleteObject((projekt_verlauf)e.Element);

        //}

        protected override void InsertItem(int index, projekt item)
        {

           // item.projekt_verlauf.AssociationChanged += new System.ComponentModel.CollectionChangeEventHandler(projekt_verlauf_AssociationChanged);
            this.Context.AddToprojekte(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {

            try
            {
                projekt project = this[index];
              //  project.projekt_verlauf.AssociationChanged -= projekt_verlauf_AssociationChanged;

                //for (int i = project.projekt_verlauf.Count - 1; i >= 0; i--)
                //{
                //    this._context.DeleteObject(project.projekt_verlauf.ElementAt(i));
                //}

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

