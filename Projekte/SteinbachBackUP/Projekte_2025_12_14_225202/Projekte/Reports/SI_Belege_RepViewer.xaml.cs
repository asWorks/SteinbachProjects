using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms.Integration;
using DAL;
using ProjektDB.Temp;


namespace ProjektDB.Reports
{
    /// <summary>
    /// Interaction logic for CalcRepViewer.xaml
    /// </summary>
    public partial class SI_Belege_RepViewer : Window
    {

        WindowsFormsHost host;
        Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        SteinbachEntities db;
     

        public SI_Belege_RepViewer()
        {
            InitializeComponent();
        }

    




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string ReportName = "ProjektDB.Reports.SteinbachRechnung.rdlc";
            db = new SteinbachEntities();
            host = new WindowsFormsHost();
            StringBuilder Kundenadresse = new StringBuilder();
            Kundenadresse.AppendLine("Arpad Stöver");
            Kundenadresse.AppendLine("Wasserkrüger Weg 109");
            Kundenadresse.AppendLine("23879 Mölln");

            var Absender = new StringBuilder();
            Absender.AppendLine("Steinbach Ingenieurtechnik");

           

            
            var data = new DTO.dtoBelege(Kundenadresse.ToString(),Absender.ToString(),DateTime.Now.ToShortDateString());
            var ds = new DTO.dtoBelege_DataSource(data);
           
            for (int i = 0; i < 45; i++)
            {
                ds.AddPos(i.ToString(), "Artikel", i + 1, i + 1 * 1.235m);
            }
            


                         

            reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            reportViewer.LocalReport.ReportEmbeddedResource = ReportName;       //"ProjektDB.Reports.Jets_Auftrag_SI.rdlc";
            //ReportDataSource ds = new ReportDataSource("dsCalkProjekt", Calc);
            //ReportDataSource dsDetail = new ReportDataSource("dsCalcDetails", detail);
            ReportDataSource dsObj = new ReportDataSource("AdressData", ds.ListBelege );
            ReportDataSource dsPos = new ReportDataSource("PosData", ds.ListPositionen);
            

            //reportViewer.LocalReport.DataSources.Add(ds);
            //reportViewer.LocalReport.DataSources.Add(dsDetail);
            reportViewer.LocalReport.DataSources.Add(dsObj);
            reportViewer.LocalReport.DataSources.Add(dsPos);
            

            reportViewer.RefreshReport();

            host.Child = reportViewer;
            this.GridDisplay.Children.Add(host);
        }

        //void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
