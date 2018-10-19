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
using System.Windows.Controls.Primitives;

namespace ProjektDB.UserControls
{
    /// <summary>
    /// Interaction logic for BetragWaehrung.xaml
    /// </summary>
    public partial class BetragWaehrung : UserControl
    {
        public BetragWaehrung()
        {
            InitializeComponent();
           
        }

        //static BetragWaehrung()
 
        //{
 
        //    //This OverrideMetadata call tells the system that this element wants 

        //    //to provide a style that is different than its base class.
 
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(BetragWaehrung), 

        //        new FrameworkPropertyMetadata(typeof(BetragWaehrung)));
 
        //}

        //public BetragWaehrung()

        //    : base()
        //{

        //}




        public string TextBoxText
        {
            get
            {
                return (string)GetValue(TextBoxTextProperty);
            }
            set
            {
                SetValue(TextBoxTextProperty, value);
            }
        }

        public static readonly DependencyProperty TextBoxTextProperty = DependencyProperty.Register("TextBoxText", typeof(string), typeof(BetragWaehrung), null);




        public string TextBlockText
        {
            get { return (string)GetValue(TextBlockTextProperty); }
            set { SetValue(TextBlockTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  TextBlockText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextProperty =
            DependencyProperty.Register("TextBlockText", typeof(string), typeof(BetragWaehrung), null);







        public IEnumerable CBoxItemssource
        {
            get { return (IEnumerable)GetValue(CBoxItemssourceProperty); }
            set { SetValue(CBoxItemssourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxItemssource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxItemssourceProperty =
            DependencyProperty.Register("CBoxItemssource", typeof(IEnumerable), typeof(BetragWaehrung),null);





        public string CBoxSelectedValue
        {
            get { return (string)GetValue(CBoxSelectedValueProperty); }
            set { SetValue(CBoxSelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CBoxSelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CBoxSelectedValueProperty =
            DependencyProperty.Register("CBoxSelectedValue", typeof(string), typeof(BetragWaehrung), null);

        

        

        public FontFamily TextBlockFontFamily
        {
            get { return (FontFamily)GetValue(TextBlockFontFamilyProperty); }
            set { SetValue(TextBlockFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontFamilyProperty =
            DependencyProperty.Register("TextBlockFontFamily", typeof(FontFamily), typeof(BetragWaehrung), null);



        public double? TextBlockFontSize
        {
            get { return (double?)GetValue(TextBlockFontSizeProperty); }
            set { SetValue(TextBlockFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockFontSizeProperty =
            DependencyProperty.Register("TextBlockFontSize", typeof(double?), typeof(BetragWaehrung), null);





        public FontFamily ValueFontFamily
        {
            get { return (FontFamily)GetValue(ValueFontFamilyProperty); }
            set { SetValue(ValueFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueFontFamilyProperty =
            DependencyProperty.Register("ValueFontFamily", typeof(FontFamily), typeof(BetragWaehrung), null);




        public double? ValueFontSize
        {
            get { return (double?)GetValue(ValueFontSizeProperty); }
            set { SetValue(ValueFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueFontSizeProperty =
            DependencyProperty.Register("ValueFontSize", typeof(double?), typeof(BetragWaehrung), null);

        public Thickness? TextBoxMargin
        {
            get { return (Thickness?)GetValue(TextBoxMarginProperty); }
            set { SetValue(TextBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxMarginProperty =
            DependencyProperty.Register("TextBoxMargin", typeof(Thickness?), typeof(BetragWaehrung), null);




        public Thickness? WaehrungBoxMargin
        {
            get { return (Thickness?)GetValue(WaehrungBoxMarginProperty); }
            set { SetValue(WaehrungBoxMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaehrungBoxMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaehrungBoxMarginProperty =
            DependencyProperty.Register("WaehrungBoxMargin", typeof(Thickness?), typeof(BetragWaehrung), null);

        public Orientation StackPanelOrientation
        {
            get { return (Orientation)GetValue(StackPanelOrientationProperty); }
            set { SetValue(StackPanelOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StackPanelOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StackPanelOrientationProperty =
            DependencyProperty.Register("StackPanelOrientation", typeof(Orientation), typeof(BetragWaehrung), null);


        public TextAlignment? TextBlockTextAlignment
        {
            get { return (TextAlignment?)GetValue(TextBlockTextAlignmentProperty); }
            set { SetValue(TextBlockTextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockTextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextAlignmentProperty =
            DependencyProperty.Register("TextBlockTextAlignment", typeof(TextAlignment?), typeof(BetragWaehrung), null);
    }



}

