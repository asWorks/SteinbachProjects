using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Text;
using C1.WPF.FlexGrid;

namespace ExcelGrid
{
    /// <summary>
    /// Extends the C1FlexGrid control to provide an Excel look.
    /// </summary>
    public class FlexExcel : C1FlexGrid
    {
        // ** fields
        ExcelMarquee _marquee;
        ColorScheme _colorScheme;

        // ** ctor
        public FlexExcel()
        {
            // initialize common properties
            AllowSorting = false;
            GridLinesVisibility = GridLinesVisibility.All;

            // Excel style marquee
            _marquee = new ExcelMarquee(this);

            // custom cell factory to provice top/left cell and row/col headers
            CellFactory = new ExcelCellFactory();

            // apply color scheme
            ColorScheme = ColorScheme.Blue;
        }

        // ** object model

        /// <summary>
        /// Gets or sets the color scheme used to render the grid
        /// </summary>
        public ColorScheme ColorScheme
        {
            get { return _colorScheme; }
            set
            {
                _colorScheme = value;
                var c = PaletteProvider.GetPalette(value);
                if (c != null)
                {
                    RowBackground =
                    AlternatingRowBackground =
                    CursorBackground =
                    EditorBackground = new SolidColorBrush(Colors.Transparent);
                    TopLeftCellBackground = new SolidColorBrush(c[0]);
                    RowHeaderBackground = new SolidColorBrush(c[1]);
                    RowHeaderSelectedBackground = new SolidColorBrush(c[2]);
                    ColumnHeaderBackground = CreateGradientBrush(c[3], c[4]);
                    ColumnHeaderSelectedBackground = CreateGradientBrush(c[5], c[6]);
                    GridLinesBrush = new SolidColorBrush(c[7]); ;
                    HeaderGridLinesBrush = new SolidColorBrush(c[8]);
                    SelectionBackground = new SolidColorBrush(c[9]);
                }
                _marquee.Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets a value that determines whether the grid shows the current 
        /// selection at all times or only when it has the focus.
        /// </summary>
        public ShowSelection ShowSelection
        {
            get { return _marquee.ShowMarquee; }
            set { _marquee.ShowMarquee = value; }
        }

        // ** implementation

        // create a linear gradient brush based on two colors 
        static LinearGradientBrush CreateGradientBrush(Color top, Color bot)
        {
            var lgb = new LinearGradientBrush();
            var gsc = lgb.GradientStops;
            gsc.Add(new GradientStop() { Color = top, Offset = 0 });
            gsc.Add(new GradientStop() { Color = bot, Offset = 1 });
            lgb.EndPoint = new Point(0, 1);
            return lgb;
        }
    }
}
