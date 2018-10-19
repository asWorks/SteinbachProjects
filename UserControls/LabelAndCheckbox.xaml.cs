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
    public partial class LabelAndCheckbox : UserControl
    {
        public LabelAndCheckbox()
        {
            InitializeComponent();
            
           
        }

       
     



        public bool? CheckBoxChecked
        {
            get { return (bool?)GetValue(CheckBoxCheckedProperty); }
            set { SetValue(CheckBoxCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheckBoxChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckBoxCheckedProperty =
            DependencyProperty.Register("CheckBoxChecked", typeof(bool?), typeof(LabelAndCheckbox), null);



        public string TextBlockText
        {
            get { return (string)GetValue(TextBlockTextProperty); }
            set { SetValue(TextBlockTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  TextBlockText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextProperty =
            DependencyProperty.Register("TextBlockText", typeof(string), typeof(LabelAndCheckbox), null);



        public double TextBoxWidth
        {
            get { return (double)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register("TextBoxWidth", typeof(double), typeof(LabelAndCheckbox), null);




        public Orientation StackPanelOrientation
        {
            get { return (Orientation)GetValue(StackPanelOrientationProperty); }
            set { SetValue(StackPanelOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StackPanelOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StackPanelOrientationProperty =
            DependencyProperty.Register("StackPanelOrientation", typeof(Orientation), typeof(LabelAndCheckbox), null);



        public Thickness CheckBoxMargin
        {
            get { return (Thickness)GetValue(CheckBoxMarginProperty); }
            set { SetValue(CheckBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheckBoxMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckBoxMarginProperty =
            DependencyProperty.Register("CheckBoxMargin", typeof(Thickness), typeof(LabelAndCheckbox), null);


        public FontFamily TextBlockFontFamily
        {
            get { return (FontFamily)GetValue(TextBlockFontFamilyProperty); }
            set { SetValue(TextBlockFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontFamilyProperty =
            DependencyProperty.Register("TextBlockFontFamily", typeof(FontFamily), typeof(LabelAndCheckbox), null);



        public double? TextBlockFontSize
        {
            get { return (double?)GetValue(TextBlockFontSizeProperty); }
            set { SetValue(TextBlockFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontSizeProperty =
            DependencyProperty.Register("TextBlockFontSize", typeof(double?), typeof(LabelAndCheckbox), null);



        public Thickness? TextBlockMargin
        {
            get { return (Thickness?)GetValue(TextBlockMarginProperty); }
            set { SetValue(TextBlockMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockMarginProperty =
            DependencyProperty.Register("TextBlockMargin", typeof(Thickness?), typeof(LabelAndCheckbox), null);



        public TextAlignment? TextBlockTextAlignment
        {
            get { return (TextAlignment?)GetValue(TextBlockTextAlignmentProperty); }
            set { SetValue(TextBlockTextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockTextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextAlignmentProperty =
            DependencyProperty.Register("TextBlockTextAlignment", typeof(TextAlignment?), typeof(LabelAndCheckbox), null);

    }



}

