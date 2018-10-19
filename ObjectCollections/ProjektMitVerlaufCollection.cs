using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using DAL;

namespace ProjektDB.ObjectCollections
{
    

        public class ProjektMitVerlaufCollection : ObservableCollection<vwProjektMitVerlauf>
        {

            private readonly SteinbachEntities  _context;
            public SteinbachEntities Context
            {
                get { return _context; }
            }
            public ProjektMitVerlaufCollection(IEnumerable<vwProjektMitVerlauf> Projekte, SteinbachEntities context)
                : base(Projekte)
            {
                _context = context;

               
            }

         

                       protected override void InsertItem(int index, vwProjektMitVerlauf item)
            {

               
                this.Context.AddTovwProjektMitVerlauf(item);
                base.InsertItem(index, item);
            }

            protected override void RemoveItem(int index)
            {

                try
                {
                    vwProjektMitVerlauf project = this[index];
                   
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

