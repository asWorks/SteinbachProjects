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
using DAL;

namespace ProjektDB.Temp
{
    /// <summary>
    /// Interaction logic for EditKundentabelle1.xaml
    /// </summary>
    public partial class EditKundentabelle1 : Window
    {
        SteinbachEntities db;
        private ListCollectionView EditkundenView;
        private CollectionViewSource EditkundenViewSource;
        int KundenNr = 0;
        string kdNr = string.Empty;
        int toSkip;
        public EditKundentabelle1()
        {
            InitializeComponent();
            db = new SteinbachEntities();
            FillList();
            toSkip = 0;
        }

        private void FillList()
        {

            db.SaveChanges();
            db = new SteinbachEntities();
            var kto = from k in db.firmen
                      where k.istVerarbeitet == null || k.istVerarbeitet == 0
                      orderby k.name
                      select k;

            EditkundenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("EditKunden_ViewSource")));
            EditkundenViewSource.Source = new ObjectCollections.FirmaCollection(kto.Skip(toSkip), db);
            EditkundenView = (ListCollectionView)EditkundenViewSource.View;
        }



        private void DoFilter(bool bName)
        {
            db.SaveChanges();
            int kn = 0;
            if (!bName)
            {
                 kn = int.Parse(txtFilter.Text);
            }
            

            IOrderedQueryable<firma> kto1;
            if (bName)
            {
                 kto1 = from k in db.firmen
                       where k.name.Contains(txtFilter.Text)
                       orderby k.name
                       select k;
            }
            else
            {
                kto1 = from k in db.firmen
                       where k.KdNr == kn
                       orderby k.name
                       select k;
            }
            

            EditkundenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("EditKunden_ViewSource")));
            EditkundenViewSource.Source = new ObjectCollections.FirmaCollection(kto1, db);
            EditkundenView = (ListCollectionView)EditkundenViewSource.View;

        }






        private void ProcessData()
        {
            db.SaveChanges();
            using (var dbCon = new SteinbachEntities())
            {

                int c = db.firmen.Where(n => n.IstKunde == 1 && (n.istVerarbeitet == 0 || n.istVerarbeitet == null)).Count();

                if (c == 0)
                {
                    MessageBox.Show("Es muss ein Eintrag im Felde IstKunde ausgewählt werden.");
                    return;
                }

                if (c > 1)
                {
                    MessageBox.Show("Es darf nur ein ein Eintrag im Felde IstKunde ausgewählt werden.");
                    return;
                }

                int g = db.firmen.Where(n => n.IstGruppe == 1 && (n.istVerarbeitet == 0 || n.istVerarbeitet == null)).Count();

                if (g == 0)
                {
                    MessageBox.Show("Es muss mindestens ein Eintrag im Felde IstGruppe ausgewählt werden.");
                    return;
                }

                var kto = from k in dbCon.firmen
                          where (k.istVerarbeitet == null || k.istVerarbeitet == 0)
                          && k.IstGruppe == 1
                          orderby k.name
                          select k;

                int kn = db.firmen.Where(n => n.IstKunde == 1 && (n.istVerarbeitet == 0 || n.istVerarbeitet == null)).Single().id;
                //KundenNr = GetMaxKdNr() + 1;
                KundenNr = 10000 + kn;
                ProcessProjects(dbCon, kto, KundenNr);

                dbCon.SaveChanges();

            }

            db.SaveChanges();

        }

        private void ProcessAllData()
        {
            db.SaveChanges();
            using (var dbCon = new SteinbachEntities())
            {
                var kto = from k in dbCon.firmen
                          where (k.istVerarbeitet == null || k.istVerarbeitet == 0)
                          orderby k.name
                          select k;


                ProcessProjects(dbCon, kto, 0, true);

                dbCon.SaveChanges();
            }

            db.SaveChanges();

        }






        private void ProcessProjects(SteinbachEntities dbCon, IOrderedQueryable<firma> kto, int KdNr, bool SetIstKunde = false)
        {

            int KN = 0;

            foreach (var item in kto)
            {
                if (KdNr == 0)
                {
                    KN = 10000 + item.id;
                }
                else
                {
                    KN = KdNr;
                }



                var project = from p in dbCon.projekte where p.kundenname == item.name select p;
                foreach (var po in project)
                {
                    if (KdNr == 0)
                    {
                        //KN = GetMaxKdNr() + 1;
                       // KN = 10000 + item.id;
                        po.KdNr = KN;
                    }
                    else
                    {
                       // KN = KdNr;
                        po.KdNr = KN;
                    }

                }

                if (SetIstKunde)
                {
                    item.IstKunde = 1;
                }

                item.istVerarbeitet = 1;
                item.KdNr = KN;

            }

            dbCon.SaveChanges();
        }


        int GetMaxKdNr()
        {
            var query = from f in db.firmen select f.KdNr;
            int res = (int)query.Max();
            return res;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessData();
            FillList();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wirklich alle Kundendaten verarbeiten?", "Sicherheitsabfrage", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ProcessAllData();
                FillList();
            }


        }

        private void btnDoFilter_Click(object sender, RoutedEventArgs e)
        {
            DoFilter(true);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //PrintDialog printDlg = new PrintDialog();
            //printDlg.PrintVisual(this.c1EditKundentabelle, "Grid Printing.");

            var report = new Reports.KundenRepViewer();
            report.ShowDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = string.Empty;
            FillList();
        }

        private void c1EditKundentabelle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtSkip.Text = c1EditKundentabelle.CurrentRow.Index.ToString();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            toSkip += int.Parse(txtSkip.Text);
            FillList();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            toSkip = 0;
        }

        private void cmdFilterKdNr_Click(object sender, RoutedEventArgs e)
        {
            DoFilter(false);
        }






    }


}
