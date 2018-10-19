using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace ProjektDB.Belege
{
    /// <summary>
    /// Summary description for SectionReport2.
    /// </summary>
    public partial class SI_InvoiceStandard : GrapeCity.ActiveReports.SectionReport
    {
        private int PageCount;
        private SubZusatzZeilen SubRep;
        private ViewModels.SI_BelegeViewModel Model = null;
        string HeaderPicture;
        string FooterPicture;
        string AltFooterPicture;
        string AltHeaderPicture;


        public SI_InvoiceStandard()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        public SI_InvoiceStandard(ViewModels.SI_BelegeViewModel vModel, string HeaderPic, string FooterPic, string AltHeaderPic, string AltFooterPic)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            HeaderPicture = HeaderPic;
            FooterPicture = FooterPic;
            AltHeaderPicture = AltHeaderPic;
            AltFooterPicture = AltFooterPic;
            Model = vModel;
            PageCount = 1;
        }



        private void groupHeader1_Format(object sender, EventArgs e)
        {
            if (!Model.isPreiseAnzeigen)
            {
                HidePrices();

            }
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {

        }

        private void groupFooter1_BeforePrint(object sender, EventArgs e)
        {


        }

        private void SI_InvoiceStandard_ReportStart(object sender, EventArgs e)
        {
            if (HeaderPicture != "Blanko")
            {
                try
                {
                    System.Drawing.Image imgHeader = System.Drawing.Image.FromFile(HeaderPicture);
                    this.picture1.Image = imgHeader;
                }
                catch (Exception ex)
                {

                    CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Laden des Kopfbildes");
                }

            }

            if (FooterPicture != "Blanko")
            {
                try
                {
                    System.Drawing.Image imgFooter = System.Drawing.Image.FromFile(FooterPicture);
                    this.picture2.Image = imgFooter;
                }
                catch (Exception ex)
                {

                    CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Laden des Fuﬂbilds");
                }

            }

            try
            {
                SubRep = new SubZusatzZeilen();
                this.subZusatzzeilen.Report = SubRep;
                ObservableCollection<ViewModels.BelegeZusatzzeilenViewModel> Zusatzzeilen = new ObservableCollection<ViewModels.BelegeZusatzzeilenViewModel>();

                if (Model.Zusatzzeilen == null)
                {
                    subZusatzzeilen.Visible = false;
                    return;                                 //// get out of here
                }

                int Wirkung = Model.SelectedBelegarten.Wirkung != -1 ? 1 : -1;

                foreach (var item in Model.Zusatzzeilen)
                {
                    string sWert = string.Empty;
                    string sWert1 = string.Empty;
                    decimal wert = item.Wert.HasValue ? (decimal)item.Wert * Wirkung : (decimal)0;
                    decimal prozent = item.Prozent.HasValue ? (decimal)item.Prozent * Wirkung : (decimal)0;

                    if (wert == 0 && prozent != 0)
                    {
                        sWert = (prozent / 100).ToString("P");
                    }
                    else if (wert == 0 && prozent == 0)
                    {
                       // sWert = (prozent / 100).ToString("P");
                    }
                    else
                    {
                        sWert1 = wert.ToString("C");
                    }

                    decimal zeilenwert = item.Zeilenwert.HasValue ? (decimal)item.Zeilenwert * Wirkung : (decimal)0;
                    decimal zeilensaldo = item.Zeilensaldo.HasValue ? (decimal)item.Zeilensaldo * Wirkung : (decimal)0;


                    ViewModels.BelegeZusatzzeilenViewModel Zeile = new ViewModels.BelegeZusatzzeilenViewModel
                    {
                        Pos = item.Pos.ToString(),
                        Text = item.Text,
                        Typ = "",
                        Prozent = sWert,
                        Wert = sWert1,
                        Zeilenwert = zeilenwert.ToString("C"),
                        Zeilensaldo = zeilensaldo.ToString("C")
                    };

                    Zusatzzeilen.Add(Zeile);
                }

                SubRep.DataSource = Zusatzzeilen;
            }
            catch (Exception ex)
            {

                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler beim Initialisieren des Belegs");
            }


        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (!Model.isPreiseAnzeigen)
            {
                HidePrices();
            }


        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            if (!Model.isPreiseAnzeigen)
            {
                subZusatzzeilen.Visible = false;
                HidePrices();


            }
        }

        private void HidePrices()
        {
            //Footer2
            Header_SummeBeleg.Visible = false;
            Header_SummePositionen.Visible = false;
            txtSummeBeleg.Visible = false;
            txtSummePositionen.Visible = false;

            //Detail
            Gesamtpreis.Visible = false;
            //ZuAbschlag.Visible = false;
            Preis.Visible = false;

            //Header1
            Header_Preis.Visible = false;
            Header_Gesamtpreis.Visible = false;
            //Header_ZuAbschlag.Visible = false;
            LineBelowDetails.Visible = false;
            lineBelowSubreport.Visible = false;
            lineAboveDisclaimer.Visible = false;
            // LineAboveSubReport.Visible = false;



        }

        private void pageFooter_Format(object sender, EventArgs e)
        {
            if (Model.id_Sprache == 1)
            {
                this.reportInfo1.FormatString = "Seite {PageNumber} von {PageCount}";
            }
            else
            {
                this.reportInfo1.FormatString = "Page {PageNumber} of {PageCount}";
            }


        }

        private void SI_InvoiceStandard_PageEnd(object sender, EventArgs e)
        {
            PageCount += 1;
        }

        private void SI_InvoiceStandard_PageStart(object sender, EventArgs e)
        {


            if (PageCount > 1)
            {

                if (string.Compare(HeaderPicture, AltHeaderPicture) != 0)
                {
                    System.Drawing.Image imgHeader = System.Drawing.Image.FromFile(AltHeaderPicture);
                    this.picture1.Image = imgHeader;
                }

                if (string.Compare(FooterPicture, AltFooterPicture) != 0)
                {
                    System.Drawing.Image imgFooter = System.Drawing.Image.FromFile(AltFooterPicture);
                    this.picture2.Image = imgFooter;
                }





                this.txtAdresse.Visible = false;
                this.txtBearbeiter.Visible = false;
                this.txtPrefix_Bearbeiter.Visible = false;
                // picture1.Height = 1.0F;
            }

        }


    }
}
