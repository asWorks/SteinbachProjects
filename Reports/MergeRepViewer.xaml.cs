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
    public partial class MergeRepViewer : Window
    {

        WindowsFormsHost host;
        Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        SteinbachEntities db;
        int CalcID = 0;

        public MergeRepViewer()
        {
            InitializeComponent();
        }

    




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string ReportName = "ProjektDB.Reports.TestAs.rdlc";
            db = new SteinbachEntities();
            host = new WindowsFormsHost();
            var Calc = from p in db.kalkulationstabellen
                       where p.id == 155
                       select p.projektnummer ;


            var detail = from d in db.kalkulationstabelle_details
                         where d.id_kalkulationstabelle == 155
                         select d.beschreibung ;


            var obj = new MergeData_DataSorce(String.Format("{0} - {1}", Calc.FirstOrDefault(), detail.FirstOrDefault()), "Zwei", "Drei", "Ein beliebiger Text","","","","","");
            


                         

            reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            reportViewer.LocalReport.ReportEmbeddedResource = ReportName;       //"ProjektDB.Reports.Jets_Auftrag_SI.rdlc";
            //ReportDataSource ds = new ReportDataSource("dsCalkProjekt", Calc);
            //ReportDataSource dsDetail = new ReportDataSource("dsCalcDetails", detail);
            ReportDataSource dsObj = new ReportDataSource("dsMergedData", obj.MergedData );
            

            //reportViewer.LocalReport.DataSources.Add(ds);
            //reportViewer.LocalReport.DataSources.Add(dsDetail);
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
