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
    public partial class LagerListenViewer : Window
    {

        WindowsFormsHost host;
        Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        SteinbachEntities db;
        int CalcID = 0;

        public LagerListenViewer()
        {
            InitializeComponent();
        }

    




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string ReportName = "ProjektDB.Reports.LagerListe.rdlc";
            db = new SteinbachEntities();
            host = new WindowsFormsHost();


            var temp = new Repositories.NavigationRepository(this.db);
            var query = temp.GetLagerlisteData();

            var merge = new MergeData_DataSorce (DateTime.Now.ToShortDateString(),"","","","","","","","");

           

                         

            reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            reportViewer.LocalReport.ReportEmbeddedResource = ReportName;       //"ProjektDB.Reports.Jets_Auftrag_SI.rdlc";
            //ReportDataSource ds = new ReportDataSource("dsCalkProjekt", Calc);
            //ReportDataSource dsDetail = new ReportDataSource("dsCalcDetails", detail);
             ReportDataSource dsLL = new ReportDataSource("dsLagerListenOA", query );
             ReportDataSource Merger = new ReportDataSource("dsMerge", merge.MergedData);

            //reportViewer.LocalReport.DataSources.Add(ds);
            //reportViewer.LocalReport.DataSources.Add(dsDetail);
            reportViewer.LocalReport.DataSources.Add(dsLL);
            reportViewer.LocalReport.DataSources.Add(Merger);


           // RenderingExtension[] re = reportViewer.LocalReport.ListRenderingExtensions();
            reportViewer.RefreshReport();
           // reportViewer.ExportDialog(re[0], "", "Test.xml");

            host.Child = reportViewer;
            this.GridDisplay.Children.Add(host);
        }

        //void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
