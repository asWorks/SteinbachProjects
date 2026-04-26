using System.Linq;
using System.Windows;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms.Integration;
using DAL;
using System;
using System.IO;
using System.Windows.Forms;

namespace ProjektDB.Reports
{


    /// <summary>
    /// Interaction logic for RepViewer.xaml
    /// </summary>
    public partial class RepViewer : Window
    {
        WindowsFormsHost host;
        Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        SteinbachEntities db;
        string pNr = string.Empty;
        int pID = 0;
        bool bSI = false;
        bool bAuftrag = false;
        string Firma = string.Empty;
        string Type = string.Empty;
        int Firmennummer = 0;

        public RepViewer()
        {
            InitializeComponent();
        }

        public RepViewer(string projektNr, int ProjektID)
        {
            pNr = projektNr;
            pID = ProjektID;
            InitializeComponent();
        }

        /// <summary>
        /// Angepaßter Konstruktor mit Initialisierungsdaten
        /// </summary>
        /// <param name="projektNr"></param>
        /// <param name="ProjektID"></param>
        /// <param name="Firma"></param>
        /// <param name="Auftrag"></param>
        /// <param name="SI"></param>
        public RepViewer(string projektNr, int ProjektID, string Firma, bool Auftrag, bool SI)
        {
            pNr = projektNr;
            pID = ProjektID;
            this.Firma = Firma;
            bAuftrag = Auftrag;
            bSI = SI;


            InitializeComponent();
        }


        public RepViewer(string projektNr, int ProjektID, string Firma, bool Auftrag, bool SI, string type)
        {
            pNr = projektNr;
            pID = ProjektID;
            this.Firma = Firma;
            bAuftrag = Auftrag;
            bSI = SI;
            Type = type;


            InitializeComponent();
        }

        public RepViewer(string projektNr, int ProjektID, int FirmenNr, bool Auftrag, bool SI, string type)
        {
            pNr = projektNr;
            pID = ProjektID;
            this.Firmennummer = FirmenNr;
            bAuftrag = Auftrag;
            bSI = SI;
            Type = type;


            InitializeComponent();
        }

        private string GetTypesString(string ProjektNummer)
        {
            var sb = new System.Text.StringBuilder();
            var query = from p in db.projekte
                        where p.projektnummer == ProjektNummer
                        select p;
            foreach (var item in query)
            {
                sb.Append(" - ");
                sb.Append(item.type.Substring(0, 1));

            }

            return sb.ToString();

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            string ReportName = string.Empty;
            db = new SteinbachEntities();
            host = new WindowsFormsHost();


            var query = from p in db.projekte
                        where p.id == pID
                        select p;
            var Merge = new Temp.MergeData_DataSorce();

            var q = query.FirstOrDefault();

            //Merge.SetValue(String.Format("{0} - {1}", q.projektnummer, q.type.Substring(0, 1)), 0);
            Merge.SetValue(String.Format("{0} {1}", q.projektnummer, GetTypesString(q.projektnummer)), 0);

            if (q.lager == 0 || q.lager == null)
            {
                if (q.ersatzteilebestellt.HasValue)
                {
                    DateTime s1 = (DateTime)q.ersatzteilebestellt;
                    Merge.SetValue(s1.ToString("d"), 1);
                }

                if (q.ersatzteileerhalten.HasValue)
                {
                    DateTime s2 = (DateTime)q.ersatzteileerhalten;
                    Merge.SetValue(s2.ToString("d"), 2);
                }

            }
            else
            {
                Merge.SetValue("Lager", 1);
                Merge.SetValue("Lager", 2);
            }

            reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            switch (Firmennummer)
            {
                case 10177:
                    {
                        if (Type == "Ersatzteile" && bSI)
                        {
                            ReportName = "ProjektDB.Reports.Jets.Jets_ETeile_SI.rdlc";
                            break;
                        }
                        if (bSI)
                        {
                            ReportName = "ProjektDB.Reports.Jets.Jets_Auftrag_SI.rdlc";
                            break;
                        }
                        if (bAuftrag)
                        {
                            ReportName = "ProjektDB.Reports.Jets.Jets_Auftrag.rdlc";
                            break;
                        }

                        ReportName = "ProjektDB.Reports.Jets.Jets_Angebot.rdlc";

                        break;
                    }
                case 10014:
                    {
                        //if (Type == "Service")
                        //{
                        //     ReportName = "ProjektDB.Reports.Alle_Angebot.rdlc";
                        //     break;
                        //}

                        if (bSI)
                        {
                            ReportName = "ProjektDB.Reports.Alle_Auftrag_SI.rdlc";
                            break;
                        }

                        if (bAuftrag)
                        {
                            ReportName = "ProjektDB.Reports.Brunvoll.Brunvoll_Auftrag.rdlc";
                            break;
                        }


                        ReportName = "ProjektDB.Reports.Alle_Angebot.rdlc";

                        break;
                    }

                default:
                    {
                        if (bSI)
                        {
                            ReportName = "ProjektDB.Reports.Alle_Auftrag_SI.rdlc";
                            break;
                        }
                        if (bAuftrag)
                        {
                            ReportName = "ProjektDB.Reports.Alle_Angebot.rdlc";
                            break;
                        }

                        ReportName = "ProjektDB.Reports.Alle_Angebot.rdlc";

                        break;

                    }




            }
            reportViewer.LocalReport.ReportEmbeddedResource = ReportName;       //"ProjektDB.Reports.Jets_Auftrag_SI.rdlc";
            ReportDataSource ds = new ReportDataSource("dsDeckblattTest", query);
            ReportDataSource MeD = new ReportDataSource("MergeData", Merge.MergedData);
            reportViewer.LocalReport.DataSources.Add(ds);
            reportViewer.LocalReport.DataSources.Add(MeD);




            reportViewer.RefreshReport();


            host.Child = reportViewer;
            this.GridDisplay.Children.Add(host);



        }

        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            switch (e.ReportPath)
            {
                case "SI_Rg_Kunde":
                    {
                        var query = from s in db.projekt_si_rgkunden where s.id_projekt == pID select s;
                        e.DataSources.Add(new ReportDataSource("dsRechnungen", query));
                        break;
                    }
                case "SI_Rg_Lief":
                    {
                        var query = from s in db.projekt_si_rglieferanten where s.id_projekt == pID select s;
                        e.DataSources.Add(new ReportDataSource("dsRechnungLieferant", query));
                        break;
                    }

                case "SI_Gutschrift_Kunde":
                    {
                        var query = from s in db.projekt_si_gutschriftkunden where s.id_projekt == pID select s;
                        e.DataSources.Add(new ReportDataSource("ds_Gutschrift_Kunde", query));
                        break;
                    }

                case "SI_Gutschrift_Lief":
                    {
                        var query = from s in db.projekt_si_gutschriftlieferanten where s.id_projekt == pID select s;
                        e.DataSources.Add(new ReportDataSource("ds_Gutschrift_Lieferant", query));
                        break;
                    }


                case "aggregate":
                    {
                        var query = from s in db.vwAggregate where s.id_projekt == pID select s;
                        e.DataSources.Add(new ReportDataSource("dsAggregate", query));
                        break;
                    }
                case "CompanyListBV":
                    {

                        var SubMerge = new Temp.MergeData_DataSorce();
                        SubMerge.SetValue("Jets", 1);
                        SubMerge.SetValue("Sperre", 2);

                        e.DataSources.Add(new ReportDataSource("MergeData", SubMerge.MergedData));
                        break;
                    }

                case "CompanyListJE":
                    {

                        var SubMerge = new Temp.MergeData_DataSorce();
                        SubMerge.SetValue("Jets", 1);
                        SubMerge.SetValue("Sperre", 2);

                        e.DataSources.Add(new ReportDataSource("MergeData", SubMerge.MergedData));
                        break;
                    }


                default:
                    break;
            }

            //   RenderingExtension[] re = reportViewer.LocalReport.ListRenderingExtensions();
            //   string deviceInfo =
            //       "<DeviceInfo>" +
            //"  <OutputFormat>EMF</OutputFormat>" +
            //"  <PageWidth>8.5in</PageWidth>" +
            //"  <PageHeight>11in</PageHeight>" +
            //"  <MarginTop>0.25in</MarginTop>" +
            //"  <MarginLeft>0.25in</MarginLeft>" +
            //"  <MarginRight>0.25in</MarginRight>" +
            //"  <MarginBottom>0.25in</MarginBottom>" +
            //"</DeviceInfo>";

            //   reportViewer.ExportDialog(re[3], deviceInfo, "Test.doc");

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            host.Dispose();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //private string ExportReport()

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] bytes = reportViewer.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                 out streamids, out warnings);

            string filename = Path.Combine(Path.GetTempPath(), "report.pdf");
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }


            string _sSuggestedName = pNr;

            


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "*PDF files (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("InitDirectoryRechnungenPDF", @"E:\test\Dokumente");
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = _sSuggestedName;
            saveFileDialog1.ShowDialog(); 
            
                FileStream newFile = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                newFile.Write(bytes, 0, bytes.Length);
                newFile.Close();
            


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] bytes = reportViewer.LocalReport.Render(
                "WORD", null, out mimeType, out encoding, out filenameExtension,
                 out streamids, out warnings);

            string filename = Path.Combine(Path.GetTempPath(), "report.pdf");
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }


            string _sSuggestedName = pNr;




            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "*Word files (*.doc)|*.doc";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("InitDirectoryRechnungenWord", @"E:\test\Dokumente");
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = _sSuggestedName;
            saveFileDialog1.ShowDialog();

            FileStream newFile = new FileStream(saveFileDialog1.FileName, FileMode.Create);
            newFile.Write(bytes, 0, bytes.Length);
            newFile.Close();

        }
    }

}
