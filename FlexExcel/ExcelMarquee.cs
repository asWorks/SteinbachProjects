using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Text;
using C1.WPF.FlexGrid;

namespace ExcelGrid
{
    /// <summary>
    /// Specifies a value the determines when the marquee will be displayed.
    /// </summary>
    public enum ShowSelection
    {
        /// <summary>
        /// Always show the marquee.
        /// </summary>
        Always,
        /// <summary>
        /// Show the marquee only when the control has the focus.
        /// </summary>
        WithFocus
    }

    /// <summary>
    /// Excel-style marquee to highlight the current grid selection.
    /// </summary>
    internal class ExcelMarquee
    {
        // ** fields
        FlexExcel _grid;
        Canvas _canvas;
        Rectangle _marquee;
        ShowSelection _visibility = ShowSelection.Always;
        bool _focus, _dirty;
        Brush _brSel, _brColHdrSel, _brRowHdrSel;

        // ** ctor
        public ExcelMarquee(FlexExcel grid)
        {
            // save parameters
            _grid = grid;
            SaveHeaderBrushes();

            // connect event handlers
            grid.GotFocus += grid_GotFocus;
            grid.LostFocus += grid_LostFocus;
            grid.SelectionChanged += grid_MarqueePositionChanged;
            grid.ScrollPositionChanged += grid_MarqueePositionChanged;
            grid.Rows.CollectionChanged += grid_MarqueePositionChanged;
            grid.Columns.CollectionChanged += grid_MarqueePositionChanged;
            grid.LayoutUpdated += grid_LayoutUpdated;

            // create marquee
            _marquee = new Rectangle();
            _marquee.Stroke = new SolidColorBrush(Color.FromArgb(0xd0, 0, 0, 0));
            _marquee.StrokeThickness = 3;

            // create canvas to hold marquee
            _canvas = new Canvas();
            _canvas.Background = null;
            _canvas.SetValue(Canvas.ZIndexProperty, 10);
            _canvas.Children.Add(_marquee);
            grid.Cells.Children.Add(_canvas);

            // initialize marquee
            _dirty = true;
            Update();
        }

        // ** object model
        public ShowSelection ShowMarquee
        {
            get { return _visibility; }
            set
            {
                if (value != _visibility)
                {
                    _visibility = value;
                    Update();
                }
            }
        }
        public void Invalidate()
        {
            SaveHeaderBrushes();
            Update();
        }

        // ** event handlers
        void grid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!_focus)
            {
                _focus = true;
                if (ShowMarquee == ShowSelection.WithFocus)
                {
                    Update();
                }
            }
        }
        void grid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_focus && !ContainsFocus(_grid))
            {
                _focus = false;
                if (ShowMarquee == ShowSelection.WithFocus)
                {
                    Update();
                }
            }
        }
        void grid_MarqueePositionChanged(object sender, EventArgs e)
        {
            if (MarqueeVisible)
            {
                _dirty = true;
            }
        }
        void grid_LayoutUpdated(object sender, EventArgs e)
        {
            if (_dirty && MarqueeVisible)
            {
                Update();
            }
        }

        // ** implementation
        bool MarqueeVisible
        {
            get
            {
                if (_visibility == ShowSelection.WithFocus && !_focus)
                {
                    return false;
                }
                return true;
            }
        }
        void Update()
        {
            _dirty = false;
            var sel = _grid.Selection;
            var vr = _grid.ViewRange;

            // set marquee visibility/background
            if (MarqueeVisible)
            {
                // update visibility
                if (_grid.Rows.Count == 0 || _grid.Columns.Count == 0 || !sel.IsValid)
                {
                    _marquee.Visibility = System.Windows.Visibility.Collapsed;
                    return;
                }
                _marquee.Visibility = System.Windows.Visibility.Visible;

                // update position
                var rc = _grid.Cells.GetCellRect(sel);
                var w = _marquee.StrokeThickness;
                rc = new Rect(rc.X - w / 2 - 0.5, rc.Y - w / 2 - 0.5, rc.Width + w, rc.Height + w);
                _marquee.SetValue(Canvas.LeftProperty, rc.X);
                _marquee.SetValue(Canvas.TopProperty, rc.Y);
                _marquee.Width = rc.Width;
                _marquee.Height = rc.Height;
                _marquee.Arrange(rc);
                _grid.SelectionBackground = _brSel;
                _grid.ColumnHeaderSelectedBackground = _brColHdrSel;
                _grid.RowHeaderSelectedBackground = _brRowHdrSel;
            }
            else
            {
                // hide marquee
                _marquee.Visibility = System.Windows.Visibility.Collapsed;
                _grid.SelectionBackground = null;
                _grid.ColumnHeaderSelectedBackground = null;
                _grid.RowHeaderSelectedBackground = null;
            }
        }
        bool ContainsFocus(FrameworkElement e)
        {
            var f = Keyboard.FocusedElement as FrameworkElement;
            while (f != null && f != e)
            {
                f = VisualTreeHelper.GetParent(f) as FrameworkElement;
            }
            return f == e;
        }
        void SaveHeaderBrushes()
        {
            _brSel = _grid.SelectionBackground;
            _brColHdrSel = _grid.ColumnHeaderSelectedBackground;
            _brRowHdrSel = _grid.RowHeaderSelectedBackground;
        }
    }
}
