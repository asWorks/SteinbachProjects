using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using C1.WPF.FlexGrid;
using DAL;
using System.Reflection;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for KalkulationDetails.xaml
    /// </summary>
    public partial class KalkulationDetails : Page
    {
        SteinbachEntities db;

        public KalkulationDetails()
        {
            InitializeComponent();
            db = new SteinbachEntities();
            //_flex.CellFactory = new ExcelCellFactory();

            // start with blue color scheme
            // ColorSchemeManager.ApplyColorScheme(_flex, ColorScheme.Blue);

            // start in bound mode

            PopulateGrid();
        }

        void PopulateGrid()
        {
            int[] widths = { 20, 20, 30, 60, 80, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60 };
            string[] headers = { "Id", "Pos.", "Menge", "Einheit", "Beschreibung", "Artikel Nr.", "EP NOK", "Umrechn. €", "Summe Pos.", "Aufschlag", "Einzelpreis", "Gesamt alle Pos", "% von Gesamt", "Anteil Transport", "SI EInzel inl. Transp.", "Rundumgspreis", "Gesamt Angebot" };
            string[] surpress = { "kalkulationstabelle", "created", "kalkulationstabelle", "kalkulationstabelleReference", "SummePosition", "EntityState", "EntityKey", "id_kalkulationstabelle" };
            
            _flex.ItemsSource = null;
            _flex.Rows.Clear();
            _flex.Columns.Clear();
            var neu = new kalkulationstabelle_detail();
            var detailQuery = from d in db.kalkulationstabelle_details where d.id_kalkulationstabelle == 794 select d;
            int rows = detailQuery.Count();



            using (_flex.Rows.DeferNotifications())
            using (_flex.Columns.DeferNotifications())
            {
                while (_flex.Columns.Count <= headers.Count())
                {
                    _flex.Columns.Add(new Column());
                }
                while (_flex.Rows.Count < rows + 1)
                {
                    _flex.Rows.Add(new Row());
                }
            }
            int z = 1;
            int s = 0;
            bool bSurpress = false;
            object xx;



            for (int i = 0; i < headers.Count(); i++)
            {
                _flex[0, i] = headers[i];
                _flex.Columns[i].Width = new System.Windows.GridLength((double)widths[i]);
            }


            PropertyInfo[] cols = null;
            foreach (var c in detailQuery)
            {

                s = 0;
                cols = c.GetType().GetProperties();

                foreach (PropertyInfo pi in cols)
                {
                    bSurpress = false;
                    // Spaltentyp:
                    Type colType = pi.PropertyType;

                    if (colType.IsGenericType &&
                        colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        colType = colType.GetGenericArguments()[0];


                    foreach (var item in surpress)
                    {
                        if (pi.Name == item)
                        {
                            bSurpress = true;
                            break;
                        }
                    }
                    if (bSurpress == false)
                    {
                        xx = pi.GetValue(c, null) == null ? DBNull.Value : pi.GetValue(c, null);
                        _flex[z, s] = xx.ToString();
                        s++;
                        

                    }


                }
                s = 0;
                z++;
            }
        }

        private void _flex_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //var grid = (C1FlexGrid)sender;


            //var x = e;
            //if (x.Key == Key.Return)
            //{

            //    Point p = GetHit(grid);
            //    MessageBox.Show(GetString(p));
            //    ExecuteInput(p);
            //}
        }

        private void CreateCalk()
        {
            var query = from k in db.kalkulationstabellen where k.id == 794 select k;
            var calc = query.FirstOrDefault();

            kalkulationstabelle_detail det = db.CreateObject<kalkulationstabelle_detail>();
            det.created = DateTime.Now;
            det.artikelnr = "1140";
            det.beschreibung = "Keine";
            det.einheit = "Stück";
            det.einzelpreis = 6625;
            det.epnok = 0;
            det.idx = 4;
            det.menge = 13;
            det.id_kalkulationstabelle = 794;
            db.AddTokalkulationstabelle_details(det);
            db.SaveChanges();





        }

        private void AddDetail_Click(object sender, RoutedEventArgs e)
        {
            CreateCalk();

        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            _flex.Rows.Add(new Row());

            _flex[_flex.Rows.Count - 1, 1] = _flex.Rows.Count - 1;

        }

        private void _flex_Click(object sender, MouseButtonEventArgs e)
        {
            var grid = (C1FlexGrid)sender;


            var x = e;
            if (x.ChangedButton == MouseButton.Right)
            {
                //  MessageBox.Show("Hit enter"); 
                MessageBox.Show(String.Format("{0} - {1}", grid.Selection.Row, grid.Selection.Column));

            }
        }

        private void _flex_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var grid = (C1FlexGrid)sender;


            var x = e;

            var p = GetHit(grid);

            try
            {
                MessageBox.Show(GetString(p));
            }
            catch (Exception)
            {

                MessageBox.Show(String.Format("{0} - {1}", p.X, p.Y));
            }
            

        }

        private Point GetHit(C1.WPF.FlexGrid.C1FlexGrid grid)
        {
            Point p = new Point(grid.Selection.Column, grid.Selection.Row);
            return p;

        }

        private void ExecuteInput(Point p)
        {
            const int menge = 2;
            const int eonok = 6;
            const int umrechnungEuro = 7;
            const int summePos = 8;
            const int zuschlag = 9;
            const int einzelpreis = 10;
            const int gesamtPos = 11;
            const int prozGesamt = 12;
            const int transport = 13;
            const int sipreis = 14;
            const int rundung = 15;
            const int gesamtangebot = 16;

            int z = (int)p.Y;
            int s = (int)p.X;

            //  MessageBox.Show(GetString(p));
            switch (s)
            {
                case eonok:
                    {
                        double v = double.Parse(_flex[z, eonok].ToString());

                        _flex[z, umrechnungEuro] = v * .129;

                        break;
                    }

                default:
                    break;
            }





        }

        private string GetString(Point p)
        {
            try
            {
                return _flex[(int)p.Y, (int)p.X].ToString();
            }
            catch (Exception)
            {

                return "err - getstring";
            }
            
        }

        private double GetDouble(Point p)
        {
            return double.Parse(_flex[(int)p.Y, (int)p.X].ToString());
        }

        private void _flex_TextInput(object sender, TextCompositionEventArgs e)
        {
           
        }

        private void _flex_CellEditEnded(object sender, CellEditEventArgs e)
        {
            var grid = (C1FlexGrid)sender;


          
          

                Point p = GetHit(grid);
                MessageBox.Show(GetString(p));
                ExecuteInput(p);
           
        }
    }
}
