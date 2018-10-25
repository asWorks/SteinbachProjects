using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

using DAL;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows;


namespace ProjektDB
{
    [ValueConversion(typeof(int), typeof(System.Drawing.Image))]
    class ImageStatusConverter : IValueConverter
    {
        bool gelb;
        bool blau;
        bool gruen;
        bool rot;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            rot = false;
            blau = false;
            gelb = false;
            gruen = false;

            using (var db = new SteinbachEntities())
            {

                if (value == null || (int)value == 0)
                {
                    return null;
                }

                int v = (int)value;
                var finanz = from f in db.projekte
                             where f.id == v
                             select f;
                var fRes = finanz.FirstOrDefault();



                blau = (fRes.creditinfinanzplanung != null && (fRes.creditcommissiondatum == null
                            || fRes.creditinbetriebnahmesummedatum == null || fRes.creditnotenummerdatum == null || fRes.creditverkaufsprovisiondatum == null));

                gelb = (bool)(fRes.service ==1) ; 

                gruen =(bool)(fRes.claim == 1);


                var query = from p in db.projekt_si_rgkunden
                            where p.id_projekt == v
                            select p;
                foreach (var res in query)
                {
                    if (res.rechnungvom != null)
                    {
                        gelb = false;
                    }
                    if (((res.rechnungfaellig != null) || res.rechnungsdatum != null) && (res.rechnungvom == null))
                    {
                        rot = true;
                       

                        //blau Finanzplanung wenn aufgenommen in Finanzplanug eingetragen und nicht alle Einträge getätigt.
                        //gelb Feld Service angehakt - Rechnung an kunden bezahlt löscht gelb und rot
                        //grün Claim angehakt

                    }

                }

                var ausRech = from r in db.projekt_ausgang_rechnungen
                              where
                                  r.id_projekt == v && r.faelligam != null && r.bezahltam == null
                              select r;
                if (ausRech.Count() > 0)
                {
                    rot = true; 
                }


                var anfrRech = from r in db.projekt_rechnungenkunde
                              where
                                  r.id_projekt == v && r.rechnungfaellig != null && r.rechnungbezahlt == null
                              select r;
                if (anfrRech.Count() > 0)
                {
                    rot = true;
                }



                return GetImage(new bool[] { rot, gelb, gruen, blau });
                //return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        private RenderTargetBitmap GetImage(bool[] farbe)
        {
                        RenderTargetBitmap rtb = new RenderTargetBitmap(50, 20, 96, 96, PixelFormats.Pbgra32);


                        double s, w = 0;
                        Rectangle cir = new Rectangle();

                        int count = 0;
                        double Anteil = 0;
                        foreach (bool c in farbe)
                        {
                            if (c)
                                count++; 
                        }

                        if (count == 0)
                        {
                            return null;
                        }

                        Anteil = (double)1 / count;
                        if (rot)
                        {
                            s = 15;                      
                            w = 50;
                            cir.Height = s;
                            cir.Width = w;
                            cir.Stroke = new SolidColorBrush(Colors.Red); // Brushes.Black;
                            cir.StrokeThickness = 10.0;
                            cir.Arrange(new Rect(new Size(w, s)));
                            rtb.Render(cir);
                        }
                        if (blau)
                        {
                            s = 15;
                            w = 38;  //50*(Anteil * (count-1));
                            cir.Height = s;
                            cir.Width = w;
                            cir.Stroke = new SolidColorBrush(Colors.Blue); // Brushes.Black;
                            cir.StrokeThickness = 10.0;
                            cir.Arrange(new Rect(new Size(w, s)));
                            rtb.Render(cir);
                        }
                        if (gelb)
                        {
                            s = 15;
                            w = 24;//50* (Anteil * (count - 2));
                            cir.Height = s;
                            cir.Width = w;
                            cir.Stroke = new SolidColorBrush(Colors.Yellow); // Brushes.Black;
                            cir.StrokeThickness = 10.0;
                            cir.Arrange(new Rect(new Size(w, s)));
                            rtb.Render(cir);
                        }

                        if (gruen)
                        {
                            s = 15;
                            w = 12;//50*(Anteil * (count - 3));
                            cir.Height = s;
                            cir.Width = w;
                            cir.Stroke = new SolidColorBrush(Colors.Green); // Brushes.Black;
                            cir.StrokeThickness = 10.0;
                            cir.Arrange(new Rect(new Size(w, s)));
                            rtb.Render(cir);
                        }
                        PngBitmapEncoder png = new PngBitmapEncoder();
                        png.Frames.Add(BitmapFrame.Create(rtb));

                        return rtb;
         
 



        }
    }
}
