using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DAL;

namespace ProjektDB.Models
{
    public class CheckListBoxColumns : ObservableCollection<ZMetaArtikellistColumn>
    {

        public CheckListBoxColumns(IEnumerable<ZMetaArtikellistColumn> Query)
            :base(Query)
        {
            //this.InsertItem(0, new ArtikellistColumn { ColumnIndex = 1, ColumnName = "Kategorie" });
            //this.InsertItem(1, new ArtikellistColumn { ColumnIndex = 2, ColumnName = "Lieferant" });
            //this.InsertItem(2, new ArtikellistColumn { ColumnIndex = 4, ColumnName = "Bezeichnung" });
            //this.InsertItem(3, new ArtikellistColumn { ColumnIndex = 5, ColumnName = "Beschreibung"});
            //this.InsertItem(4, new ArtikellistColumn { ColumnIndex = 8, ColumnName = "Kommissionsbestand"});
            //this.InsertItem(5, new ArtikellistColumn { ColumnIndex = 9, ColumnName = "Regal" });
            //this.InsertItem(6, new ArtikellistColumn { ColumnIndex = 10, ColumnName = "Box" });
            //this.InsertItem(7, new ArtikellistColumn { ColumnIndex = 11, ColumnName = "Warentarifnummer"});

            //this.InsertItem(8, new ArtikellistColumn { ColumnIndex = 14, ColumnName = "Sonderpreis" });
            //this.InsertItem(9, new ArtikellistColumn { ColumnIndex = 15, ColumnName = "Handelsware" });
            //this.InsertItem(10, new ArtikellistColumn { ColumnIndex = 16, ColumnName = "WKZ" });
            //this.InsertItem(11, new ArtikellistColumn { ColumnIndex = 17, ColumnName = "Preis vom:" });
            //this.InsertItem(12, new ArtikellistColumn { ColumnIndex = 18, ColumnName = "Mitarbeiter" });
           



        }
    }
}
