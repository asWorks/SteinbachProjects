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


namespace ProjektDB.Reports
{
    /// <summary>
    /// Interaction logic for CalcRepViewer.xaml
    /// </summary>
    public partial class CalcRepViewer : Window
    {

        WindowsFormsHost host;
        Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        SteinbachEntities db;
        int CalcID = 0;

        public CalcRepViewer()
        {
            InitializeComponent();
        }

        public CalcRepViewer(int calcID)
        {
            CalcID = calcID;

            InitializeComponent();
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string ReportName = "ProjektDB.Reports.CalcMain.rdlc";
            db = new SteinbachEntities();
            host = new WindowsFormsHost();
            var Calc = from p in db.kalkulationstabellen
                        where p.id == CalcID
                        select p;

            var detail = from d in db.kalkulationstabelle_details
                         where d.id_kalkulationstabelle == CalcID
                         select d;
            var obj = new CalcData(CalcID);

                         

            reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            reportViewer.LocalReport.ReportEmbeddedResource = ReportName;       //"ProjektDB.Reports.Jets_Auftrag_SI.rdlc";
            ReportDataSource ds = new ReportDataSource("dsCalkProjekt", Calc);
            ReportDataSource dsDetail = new ReportDataSource("dsCalcDetails", detail);
            ReportDataSource dsObj = new ReportDataSource("dsObject", obj.Positionen );

            reportViewer.LocalReport.DataSources.Add(ds);
            reportViewer.LocalReport.DataSources.Add(dsDetail);
            reportViewer.LocalReport.DataSources.Add(dsObj);
            
            

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
