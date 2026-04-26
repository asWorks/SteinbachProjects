using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProjektDB.Format
{
    public class FormatMainWindow
    {
        /// <summary>
        /// Anpassen der Formularoptik anhand der Parameter firma und type
        /// Sichbarmachen oder ausblenden von Feldern, umbenennen von Labels
        /// </summary>
        /// <param name="firma">Ausführende Firma</param>
        /// <param name="type">Projekttyp</param>
        /// <param name="mw">Referenz auf Window</param>
        public static void SetFirma(int firma, MainWindow mw)
        {

            if (firma == 100) // Sonderprojekte
            {

                mw.SP3.Visibility = Visibility.Collapsed;
                mw.SP4.Visibility = Visibility.Collapsed;
                mw.SP5.Visibility = Visibility.Collapsed;
                mw.SP6.Visibility = Visibility.Collapsed;
                mw.SP9.Visibility = Visibility.Collapsed;
                mw.SP10.Visibility = Visibility.Collapsed;
                mw.SP11.Visibility = Visibility.Collapsed;
                mw.SP12.Visibility = Visibility.Collapsed;
                var height = new System.Windows.GridLength(0, GridUnitType.Pixel);

                mw.Anfrage.RowDefinitions[3].Height = height;
                mw.Anfrage.RowDefinitions[4].Height = height;
                mw.Anfrage.RowDefinitions[5].Height = height;
                mw.Anfrage.RowDefinitions[6].Height = height;
                mw.Anfrage.RowDefinitions[9].Height = height;
                mw.Anfrage.RowDefinitions[10].Height = height;
                mw.Anfrage.RowDefinitions[11].Height = height;
                mw.Anfrage.RowDefinitions[12].Height = height;

                mw.txtBrunvollprojektnummer.Visibility = Visibility.Collapsed;
                mw.lblUnitNr.Visibility = Visibility.Collapsed;
                mw.txtOffernummer.Visibility = Visibility.Collapsed;
                mw.lblOfferNr.Visibility = Visibility.Collapsed;
                mw.cboType.Visibility = Visibility.Collapsed;
                mw.lblType.Visibility = Visibility.Collapsed;
                mw.Aggregate_ListView.Visibility = Visibility.Hidden;
                mw.AddAggregate.Visibility = Visibility.Hidden;
                mw.tblSchiffUebernehmen.Visibility = Visibility.Hidden;
                mw.lblSchiffProjekt.Content = "Projektname :";
                mw.cboType.Text = "Anlage";

                mw.ExpAnlagenTyp.Visibility = Visibility.Collapsed;
                mw.expKalkulationstabellen.Visibility = Visibility.Collapsed;
                mw.brdLagerlisten.Visibility = Visibility.Collapsed;

                return;


            }
            else
            {
                mw.SP3.Visibility = Visibility.Visible;
                mw.SP4.Visibility = Visibility.Visible;
                mw.SP5.Visibility = Visibility.Visible;
                mw.SP6.Visibility = Visibility.Visible;
                mw.SP9.Visibility = Visibility.Visible;
                mw.SP10.Visibility = Visibility.Visible;
                mw.SP11.Visibility = Visibility.Visible;
                mw.SP12.Visibility = Visibility.Visible;

                var height35 = new System.Windows.GridLength(35, GridUnitType.Pixel);
                var height30 = new System.Windows.GridLength(30, GridUnitType.Pixel);
                var height25 = new System.Windows.GridLength(25, GridUnitType.Pixel);
                var heightAuto = new System.Windows.GridLength(0, GridUnitType.Auto);

                mw.Anfrage.RowDefinitions[3].Height = height30;
                mw.Anfrage.RowDefinitions[4].Height = height35;
                mw.Anfrage.RowDefinitions[5].Height = height30;
                mw.Anfrage.RowDefinitions[6].Height = heightAuto;
                mw.Anfrage.RowDefinitions[9].Height = heightAuto;
                mw.Anfrage.RowDefinitions[10].Height = height25;
                mw.Anfrage.RowDefinitions[11].Height = height30;
                mw.Anfrage.RowDefinitions[12].Height = height25;

                mw.txtBrunvollprojektnummer.Visibility = Visibility.Visible;
                mw.lblUnitNr.Visibility = Visibility.Visible;
                mw.txtOffernummer.Visibility = Visibility.Visible;
                mw.lblOfferNr.Visibility = Visibility.Visible;
                mw.cboType.Visibility = Visibility.Visible;
                mw.lblType.Visibility = Visibility.Visible;
                mw.tblSchiffUebernehmen.Visibility = Visibility.Visible;
                mw.lblSchiffProjekt.Content = "Schiff :";

                mw.ExpAnlagenTyp.Visibility = Visibility.Visible;
                mw.expKalkulationstabellen.Visibility = Visibility.Visible;
                mw.brdLagerlisten.Visibility = Visibility.Visible;

            }


            //if (firma.ToLower() == "brunvoll as")
            if (firma == 10014)
            {
                mw.Aggregate_ListView.Visibility = Visibility.Visible;
                mw.AddAggregate.Visibility = Visibility.Visible;
                mw.lblUnitNr.Content = "Brunvoll Projekt Nr.";
                mw.lblOfferNr.Visibility = Visibility.Hidden;
                mw.txtOffernummer.Visibility = Visibility.Hidden;
                return;


            }
            else
            {
                mw.lblOfferNr.Visibility = Visibility.Visible;
                mw.txtOffernummer.Visibility = Visibility.Visible;

                mw.Aggregate_ListView.Visibility = Visibility.Hidden;
                mw.AddAggregate.Visibility = Visibility.Hidden;
                mw.lblUnitNr.Content = "Unit Nr.";
            }

            //if (firma.ToLower() == "jets as")
            if (firma == 10177)
            {
                //mw.chkLand.Visibility = Visibility.Visible;
                //mw.chkSchiff.Visibility = Visibility.Visible;
                mw.rbLand.Visibility = Visibility.Visible;
                mw.rbSchiff.Visibility = Visibility.Visible;

                mw.lblLand.Visibility = Visibility.Visible;
                mw.lblSchiff.Visibility = Visibility.Visible;
                mw.txtInterneNummer.Visibility = Visibility.Visible;
                mw.lblInterneNummer.Visibility = Visibility.Visible;
                mw.lblUnitNr.Content = "Unit Nr.:";
                mw.lblOfferNr.Content = "Offer Nr.:";
            }
            else
            {
                //mw.chkLand.Visibility = Visibility.Hidden;
                //mw.chkSchiff.Visibility = Visibility.Hidden;
                mw.rbLand.Visibility = Visibility.Hidden;
                mw.rbSchiff.Visibility = Visibility.Hidden;
                mw.lblLand.Visibility = Visibility.Hidden;
                mw.lblSchiff.Visibility = Visibility.Hidden;
                mw.txtInterneNummer.Visibility = Visibility.Hidden;
                mw.lblInterneNummer.Visibility = Visibility.Hidden;
            }



            //  if (firma.ToLower() != "brunvoll as" && firma.ToLower() != "jets as")
            if (firma != 10014 && firma != 10177)
            {

                mw.lblUnitNr.Content = "Offer Nr.:";
                mw.lblOfferNr.Visibility = Visibility.Visible;
                mw.lblOfferNr.Content = "Order Nr.:";


            }

            //if (firma.ToLower() == "jets as" || firma.ToLower() == "tamrotor marine compressors as")
            if (firma == 10177 || firma == 10016)
            {
                mw.ExpErsatzteileGutschrift.Visibility = Visibility.Visible;
                mw.ExpProjektRechnung.Visibility = Visibility.Visible;
            }
            else
            {

                mw.ExpErsatzteileGutschrift.Visibility = Visibility.Collapsed;
                mw.ExpProjektRechnung.Visibility = Visibility.Collapsed;

            }


        }
        public static bool SetSI(bool bSI, bool bAuftrag, MainWindow mw, bool abfrage)
        {
            if (abfrage)
            {


                if (MessageBox.Show("Wirklich zwischen Auftrag und SI umschalten ?", "Daten werden entfernt", MessageBoxButton.YesNo) != MessageBoxResult.No)
                {

                    SetSI(bSI, bAuftrag, mw);
                    return true;

                }
                else
                {
                    return false;
                }

            }
            else
            {
                SetSI(bSI, bAuftrag, mw);
                return true;
            }
        }


        /// <summary>
        /// Setzen der Sichtbarkeit für Si und Auftragsteil anhand der im Formular
        /// oder der Neuanlage gesetzen Werte.
        /// </summary>
        /// <param name="bSI">Teil SI sichtbar Ja/Nein</param>
        /// <param name="bAuftrag">Teil Auftrag sichtbar Ja/Nein</param>
        /// <param name="mw">Referenz auf Window</param>
        private static void SetSI(bool bSI, bool bAuftrag, MainWindow mw)
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
                if (mw.cboType.Text.ToLower() != "service")
                {
                    mw.brdAuftrag.Visibility = Visibility.Visible;
                }

            }
            else
            {
                mw.brdAuftrag.Visibility = Visibility.Collapsed;
            }


        }

        public static void SetType(string type, MainWindow mw)
        {
            if (type.ToLower() == "service")
            {
                mw.brdService.Visibility = Visibility.Visible;
                mw.tbHostHyperlinkServive.Visibility = Visibility.Visible;
                mw.tbHypelinkServiceText.Text = "Ersatzteile anlegen";
                mw.brdAuftrag.Visibility = Visibility.Collapsed;
            }
            else if (type.ToLower() == "ersatzteile")
            {
                mw.brdService.Visibility = Visibility.Collapsed;
                mw.tbHostHyperlinkServive.Visibility = Visibility.Visible;
                mw.tbHypelinkServiceText.Text = "Service anlegen";

            }
            else
            {
                mw.brdService.Visibility = Visibility.Collapsed;
                mw.tbHostHyperlinkServive.Visibility = Visibility.Collapsed;
            }

        }

        public static void SetAngebot(DateTime datum, MainWindow mw)
        {
            if (datum.CompareTo(new DateTime(2009, 9, 24)) > 0)
            {
                mw.txtAngebot.Visibility = Visibility.Collapsed;
                mw.LblAngebot.Visibility = Visibility.Collapsed;
                mw.SP11.Visibility = Visibility.Collapsed;
                mw.SP12.Visibility = Visibility.Collapsed;
            }
            else
            {
                mw.txtAngebot.Visibility = Visibility.Visible;
                mw.LblAngebot.Visibility = Visibility.Visible;
                mw.SP11.Visibility = Visibility.Visible;
                mw.SP12.Visibility = Visibility.Visible; 

                
            }


        }

        public static void SetRechnungFactor(DAL.projekt_rechnung rechnung, MainWindow mw)
        {
            // if (mw.cboFirma.Text.ToLower() == "jets as")
            // int sv = (int)mw.cboFirma.SelectedValue;
            int sv = (int)mw.Firma_FCB_X.SelectedValue;
            if (sv == 10177)
            {
                if (mw.cboType.Text.ToLower() == "ersatzteile")
                {
                    rechnung.faktor = 15;
                }

            }

        }


        public static void SetRechnungInterneNo(DAL.projekt_rechnung rechnung, MainWindow mw)
        {
            if (mw.txtInterneNummer.Text != string.Empty)
            {

                rechnung.intno = mw.txtInterneNummer.Text;


            }

        }


        public static void SetAuftragErsatzteil(string type, MainWindow mw)
        {

            Visibility vi = new Visibility();
            if (type.ToLower() == "ersatzteile" && mw.chkAuftrag.IsChecked == true)
            {

                vi = Visibility.Collapsed;
            }
            else
            {
                vi = Visibility.Visible;
            }

            mw.spAuftragKopf1Data.Visibility = vi;
            mw.spAuftragKopf1Label.Visibility = vi;
            mw.spAuftragKopf2Data.Visibility = vi;
            mw.spAuftragKopf2Label.Visibility = vi;
            mw.ExpKomponenten.Visibility = vi;
            mw.ExpAusgangsGutschriften.Visibility = vi;
            mw.ExpAusgangsRechnungen.Visibility = vi;
            mw.ExpLieferantenGutschriften.Visibility = vi;
            mw.ExpLieferantenRechnungen.Visibility = vi;

        }



        public static void SetJetsLandanlagen(bool isLand, MainWindow mw)
        {

            if (mw.Firma_FCB_X.SelectionBoxItem != "")
            {


                var fi = (DAL.firma)mw.Firma_FCB_X.SelectionBoxItem;
                if (fi.KdNr == 10177)
                {


                    string buf = mw.cboType.Text;

                    Visibility vis = isLand == true ? Visibility.Collapsed : Visibility.Visible;

                    mw.txtWerftname.Visibility = vis;
                    mw.txtWerfnummer.Visibility = vis;
                    mw.lblWerft.Visibility = vis;
                    mw.lblNBNr.Visibility = vis;
                    mw.imonummer.Visibility = vis;
                    mw.lblImoNummer.Visibility = vis;

                    if (!isLand)
                    {
                        if (buf.ToLower() == "ersatzteile" && mw.chkAuftrag.IsChecked == true)
                        {
                            vis = Visibility.Collapsed;
                        }
                    }
                    mw.spAuftragKopf1Data.Visibility = vis;
                    mw.spAuftragKopf1Label.Visibility = vis;


                    mw.lblSchiffProjekt.Content = isLand == true ? "Projekt :" : "Schiff :";


                }
            }

        }




    }
}
