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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace ProjektDB.UserControls
{
    /// <summary>
    /// Interaction logic for BetragWaehrung.xaml
    /// </summary>
    public partial class LabelAndDatepicker : UserControl
    {
        public LabelAndDatepicker()
        {
            InitializeComponent();
           
        }

       
    

        public DateTime? DatePickerDateTime
        {
            get { return (DateTime?)GetValue(DatePickerDateTimeProperty); }
            set { SetValue(DatePickerDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DatePickerDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DatePickerDateTimeProperty =
            DependencyProperty.Register("DatePickerDateTime", typeof(DateTime?), typeof(LabelAndDatepicker), null);

        


        public string TextBlockText
        {
            get { return (string)GetValue(TextBlockTextProperty); }
            set { SetValue(TextBlockTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  TextBlockText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextProperty =
            DependencyProperty.Register("TextBlockText", typeof(string), typeof(LabelAndDatepicker), null);



        public double DatePickerWidth
        {
            get { return (double)GetValue(DatePickerWidthProperty); }
            set { SetValue(DatePickerWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DatePickerWidthProperty =
            DependencyProperty.Register("DatePickerWidth", typeof(double), typeof(LabelAndDatepicker), null);




        public Orientation StackPanelOrientation
        {
            get { return (Orientation)GetValue(StackPanelOrientationProperty); }
            set { SetValue(StackPanelOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StackPanelOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StackPanelOrientationProperty =
            DependencyProperty.Register("StackPanelOrientation", typeof(Orientation), typeof(LabelAndDatepicker), null);




        public FontFamily TextBlockFontFamily
        {
            get { return (FontFamily)GetValue(TextBlockFontFamilyProperty); }
            set { SetValue(TextBlockFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontFamilyProperty =
            DependencyProperty.Register("TextBlockFontFamily", typeof(FontFamily), typeof(LabelAndDatepicker), null);



        public double? TextBlockFontSize
        {
            get { return (double?)GetValue(TextBlockFontSizeProperty); }
            set { SetValue(TextBlockFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontSizeProperty =
            DependencyProperty.Register("TextBlockFontSize", typeof(double?), typeof(LabelAndDatepicker), null);



        public Thickness? TextBlockMargin
        {
            get { return (Thickness?)GetValue(TextBlockMarginProperty); }
            set { SetValue(TextBlockMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockMarginProperty =
            DependencyProperty.Register("TextBlockMargin", typeof(Thickness?), typeof(LabelAndDatepicker), null);




        public Thickness? DatePickerMargin
        {
            get { return (Thickness?)GetValue(DatePickerMarginProperty); }
            set { SetValue(DatePickerMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DatePickerMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DatePickerMarginProperty =
            DependencyProperty.Register("DatePickerMargin", typeof(Thickness?), typeof(LabelAndDatepicker), null);






        public FontFamily DatePickerFontFamily
        {
            get { return (FontFamily)GetValue(DatePickerFontFamilyProperty); }
            set { SetValue(DatePickerFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DatePickerFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DatePickerFontFamilyProperty =
            DependencyProperty.Register("DatePickerFontFamily", typeof(FontFamily), typeof(LabelAndDatepicker), null);



        public double? DatePickerFontSize
        {
            get { return (double?)GetValue(DatePickerFontSizeProperty); }
            set { SetValue(DatePickerFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DatePickerFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DatePickerFontSizeProperty =
            DependencyProperty.Register("DatePickerFontSize", typeof(double?), typeof(LabelAndDatepicker), null);



        public TextAlignment? TextBlockTextAlignment
        {
            get { return (TextAlignment?)GetValue(TextBlockTextAlignmentProperty); }
            set { SetValue(TextBlockTextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockTextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextAlignmentProperty =
            DependencyProperty.Register("TextBlockTextAlignment", typeof(TextAlignment?), typeof(LabelAndDatepicker), null);




    }



}

