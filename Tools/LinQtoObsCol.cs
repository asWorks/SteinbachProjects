using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ProjektDB
{
    /// <summary>
    /// Erweiterungsmethoden für LINQ
    /// </summary>
    public static class LinQtoObsCol
    {
        /// <summary>
        /// Konvertiert das Ergebnis einer LINQ-Abfrage in ein DataTable
        /// </summary>
        /// <typeparam name="T">
        /// generischer Typ
        /// </typeparam>
        /// <param name="list">
        /// Ergebnis der LINQ-Abfrage
        /// </param>
        /// <returns>
        /// DataTable
        /// </returns>
        /// <example>
        /// <code>
        /// var countries =
        ///		from c in db.Countries
        ///		select new
        ///		{
        ///			c.ID,
        ///			c.Name
        ///		};
        ///		
        ///	DataTable dt = countries.Linq2DataTable();
        /// </code>
        /// </example>
        public static List<string> Linq2DataTable<T>(this IEnumerable<T> list)
        {
            // DataTable mit Namen aus GUID erstellen:
            //DataTable dt = new DataTable(Guid.NewGuid().ToString());
            List<string> namensliste = new List<string>();
            // Spaltennamen:
            PropertyInfo[] cols = null;

            // Ist das LINQ-Ergebnis null wird ein leeres DataTable 
            // zurückgegeben:
            if (list == null)
                return null;

            // Alle Elemente der Liste durchlaufen (LINQ-Ergebnis):
            foreach (T item in list)
            {
                // Die Spaltennamen werden per Reflektion ermittelt.
                // Wird nur beim 1. Durchlauf ermittelt:
                if (cols == null)
                {
                    // Alle Spalten ermitteln:
                    cols = item.GetType().GetProperties();

                    // Spalten durchlaufen und im DataTable die Spalten erstellen:
                    foreach (PropertyInfo pi in cols)
                    {
                        // Spaltentyp:
                        Type colType = pi.PropertyType;

                        if (colType.IsGenericType &&
                            colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            colType = colType.GetGenericArguments()[0];

                        // Spalte der DataTable hinzufügen:
                        object xx;
                        foreach (PropertyInfo pix in cols)
                        {
                             xx = pix.GetValue(item, null) == null ? DBNull.Value : pi.GetValue
                            (item, null);
                        }

                       //namensliste.Add(xx.ToString());

                    }
                }

                // Zeile hinzufügen:
                //DataRow dr = dt.NewRow();
                //foreach (PropertyInfo pi in cols)
                //    dr[pi.Name] =
                //        pi.GetValue(item, null) ?? DBNull.Value;

                //dt.Rows.Add(dr);
            }

            return namensliste;
        }
    }
}

