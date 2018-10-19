using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ProjektDB.Tools;
using ProjektDB.views;
using DAL;

namespace ProjektDB.Format
{

    public class FormatProjektliste
    {

        public static void ShowKommissionFilter(views.Projektliste pl,bool showFilter)
        {
            if (showFilter)
            {
                pl.cboKommissionBezahlt.Visibility = Visibility.Visible;
            }
            else
            {
                pl.cboKommissionBezahlt.Visibility = Visibility.Hidden;

            }
        }

        public static void ShowAddNewButton(Projektliste pl,ListeTypeEnum type)
        {
            if (type == ListeTypeEnum.Person)
            {
                if (Session.User.rights != "admin")
                    pl.btnNewHigh.Visibility = Visibility.Hidden;

            }

        }

        public static void ShowDatumFilter(Projektliste pl, bool showFilter)
        {
            if (showFilter == false)
            {
                pl.spJahrBis.Visibility = Visibility.Hidden;
                pl.spJahrVon.Visibility = Visibility.Hidden;
            }
            else
            {
                pl.spJahrBis.Visibility = Visibility.Visible;
                pl.spJahrVon.Visibility = Visibility.Visible;
            }
        }

        public static void ShowKundenFilter(Projektliste pl, ListeTypeEnum type)
        {
            if (type == ListeTypeEnum.Firma)
            {
                pl.chkAlleFirmenAnzeigen.Visibility = Visibility.Visible;
                pl.tbAlleFirmenAnzeigen.Visibility = Visibility.Visible;
                            }
            else
            {
                pl.chkAlleFirmenAnzeigen.Visibility = Visibility.Collapsed;
                pl.tbAlleFirmenAnzeigen.Visibility = Visibility.Collapsed;
                pl.chkAlleFirmenAnzeigen.IsChecked = false;
            }
        }

        public static void ShowPrintButton(Projektliste pl, ListeTypeEnum type)
        {
            if (type == ListeTypeEnum.Artikel)
            {
                pl.btnPrint.Visibility = Visibility.Visible;
               // pl.tbAlleFirmenAnzeigen.Visibility = Visibility.Visible;
            }
            else
            {
                pl.btnPrint.Visibility = Visibility.Collapsed;
                //pl.tbAlleFirmenAnzeigen.Visibility = Visibility.Collapsed;
                //pl.chkAlleFirmenAnzeigen.IsChecked = false;
            }
        }


        

        public static void ConfigureView(views.Projektliste pl, Tools.ListeTypeEnum type)
        {
            switch (type)
            {
                case Tools.ListeTypeEnum.Firma :
                    {
                    
                    break;
                    }

                case Tools.ListeTypeEnum.Artikel:
                    break;
                case Tools.ListeTypeEnum.Person:
                    break;
                case Tools.ListeTypeEnum.Schiff:
                    break;
                case Tools.ListeTypeEnum.Projekt:
                    break;
                case Tools.ListeTypeEnum.Kalkulationliste:
                    break;
                case Tools.ListeTypeEnum.Aggregat:
                    break;
                case Tools.ListeTypeEnum.Zahlungsbedingung:
                    break;
                case Tools.ListeTypeEnum.Standard:
                    break;
                default:
                    break;
            }

        }

    }
}
