using System;
using System.Collections.Generic;

using System.Collections.ObjectModel ;
using DAL;
using DAL.Tools;


namespace ProjektDB.ObjectCollections
{
    class VerlaufCollection: ObservableCollection<projekt_verlauf>
    {
     
            private readonly SteinbachEntities _context;
            public SteinbachEntities Context
            {
                get { return _context; }
            }
            public VerlaufCollection(IEnumerable<projekt_verlauf> p_verlauf, SteinbachEntities context)
                : base(p_verlauf)
            {
               try 
	            {	        
		             _context = context;
	            }
	            catch (Exception ex)
	            {
		
                    LoggingTool.LogExeption (ex,"VerlaufCollection","Constructor");
                    System.Windows.MessageBox.Show ("Beim Initialisieren von Projekte_Verlauf ist ein Fehler aufgetreten.");  
	            }
               

               
            }

            


            protected override void InsertItem(int index, projekt_verlauf item)
            {
                try
                {
                this.Context.AddToprojekt_verlaufe(item);
                base.InsertItem(index, item);

                 }
               
                catch (Exception ex)
                {

                    LoggingTool.LogExeption (ex,"VerlaufCollection","InsertItem");
                    System.Windows.MessageBox.Show ("Beim Einfügen eines Eintrages in Projekte_Verlauf ist ein Fehler aufgetreten." );  
                   
                   
                }
            }

            protected override void RemoveItem(int index)
            {

                try
                {
                   
                    this.Context.DeleteObject(this[index]);
                    base.RemoveItem(index);
                }
                catch (Exception ex)
                {

                    LoggingTool.LogExeption (ex,"VerlaufCollection","RemoveItem");
                    System.Windows.MessageBox.Show ("Beim Löschen eines Eintrages in Projekte_Verlauf ist ein Fehler aufgetreten." );  
                    
                   
                   
                }

            }



        }
    }

