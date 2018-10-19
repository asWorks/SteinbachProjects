using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProjektDB.Format
{
    public class FormatEditProjekt
    {
        /// <summary>
        /// Anpassen der Formularoptik anhand der Parameter firma und type
        /// Sichbarmachen oder ausblenden von Feldern, umbenennen von Labels
        /// </summary>
        /// <param name="firma">Ausführende Firma</param>
        /// <param name="type">Projekttyp</param>
        /// <param name="mw">Referenz auf Window</param>
        public static void SetFirma(int firma, EditProjekt mw)
        {

            if (firma  == 10014) // Brunvoll As
            {
                mw.Aggregate_ListView.Visibility = Visibility.Visible ;
                mw.lblUnitNr.Content = "Brunvoll Projekt Nr.";
            }
            else
            {
                mw.Aggregate_ListView.Visibility = Visibility.Hidden;
                mw.lblUnitNr.Content = "Unit Nr.";
            }
    
        }
        /// <summary>
        /// Setzen der Sichtbarkeit für Si und Auftragsteil anhand der im Formular
        /// oder der Neuanlage gesetzen Werte.
        /// </summary>
        /// <param name="bSI">Teil SI sichtbar Ja/Nein</param>
        /// <param name="bAuftrag">Teil Auftrag sichtbar Ja/Nein</param>
        /// <param name="mw">Referenz auf Window</param>
        public static void SetSI(bool bSI, bool bAuftrag, EditProjekt mw)
        {
            if (bSI)
            {
                mw.brdSI.Visibility = Visibility.Visible; 
            }
            else
            {
                mw.brdSI.Visibility = Visibility.Collapsed; 
            }

            if (bAuftrag)
            {
                mw.brdAuftrag.Visibility = Visibility.Visible;
            }
            else
            {
                mw.brdAuftrag.Visibility = Visibility.Collapsed;
            }

        
        }

        public static void SetType(string type, EditProjekt mw)
        {
            if (type.ToLower() == "service")
                mw.brdService.Visibility = Visibility.Visible;
            else
                mw.brdService.Visibility = Visibility.Collapsed;
        
        }
    }
}
