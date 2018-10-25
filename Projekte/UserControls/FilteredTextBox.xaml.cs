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
using System.Windows.Controls.Primitives;
using System.Collections;

namespace ProjektDB.UserControls
{
    /// <summary>
    /// Interaction logic for FilteredComboBox.xaml
    /// </summary>
    public partial class FilteredTextBox : UserControl
    {
        public delegate void FiteredBoxChanged(object sender, FilteredComboBoxChangedEventArgs e);
        public delegate void FilteredBoxSelectionChanged(object sender, SelectionChangedEventArgs e);
        public event FiteredBoxChanged onfcbChanged;
        public event FilteredBoxSelectionChanged OnFcb_SelectionChanged;

        CollectionViewSource ViewSource;

        IEnumerable ComboBoxDataSource;
        //   System.Collections.IEnumerable ComboBoxDataSource;
        bool bUser = false;

        public FilteredTextBox()
        {
            InitializeComponent();

        }

        public DataTemplate cBoxItemTemplate
        {
            get
            {
               return cBox.ItemTemplate;
               
            }

            set
            {
                this.cBox.ItemTemplate = value;
            }
        }
        public Thickness ControlsMargin
        {
            get
            {
                return cBox.Margin;
               
            }
            set
            {
                cBox.Margin = value;
                
            }
        }

        public double ControlsHeight
        {
            get
            {
                return cBox.Height;
                           }
            set
            {
                cBox.Height = value;
                tBox.Height = value;
            }
        }

        public int MaxTextLength
        {
            get { return tBox.MaxLength; }
            set { tBox.MaxLength = value; }
        }




        public IEnumerable ComboBoxViewSource
        {
            get { return ComboBoxDataSource; }
            set
            {
                ComboBoxDataSource = value;
                if (ViewSource != null)
                {
                    SetUser(false);
                     ViewSource.Source = ComboBoxDataSource;
                    cBox.ItemsSource = value;
                }

            }
        }

        public double ControlsWidth
        {
            get 
            {
                return cBox.Width;
                            }
            set
            {
                cBox.Width = value;
                tBox.Width = value;

            }
        }

        public Brush ControlsBackground
        {
            get
            { 
                return cBox.Background; 
                
            }
            set
            {
                cBox.Background = value;
                tBox.Background = value;

            }
        }

        public Binding This_Text
        {
            get { return null; }
            set
            {
                tBox.SetBinding(TextBox.TextProperty, value);


            }
        }

        public void SetSimpleBinding(string vText, string vSelectedValuePath, string vDisplayedMemberPath, BindingMode vMode, UpdateSourceTrigger vTrigger)
        {
            Binding b = new Binding(vText);
            b.Mode = vMode;
            b.UpdateSourceTrigger = vTrigger;
            cBox.SelectedValuePath = vSelectedValuePath;
            cBox.DisplayMemberPath = vDisplayedMemberPath;
            tBox.SetBinding(TextBox.TextProperty, b);


        }

        public void SetSimpleBinding(string vText, string vSelectedValuePath, DataTemplate vTemplate, BindingMode vMode, UpdateSourceTrigger vTrigger)
        {
            Binding b = new Binding(vText);
            b.Mode = vMode;
            b.UpdateSourceTrigger = vTrigger;
            cBox.SelectedValuePath = vSelectedValuePath;
            cBox.ItemTemplate = vTemplate;
            tBox.SetBinding(TextBox.TextProperty, b);

        }

        public void SetBinding(string path, BindingMode vMode, UpdateSourceTrigger vTrigger)
        {
            Binding b = new Binding(path);
            b.UpdateSourceTrigger = vTrigger;
            b.Mode = vMode;
            tBox.SetBinding(TextBox.TextProperty, b);

            
        }


        public string ComboBoxText
        {
            get
            {
                return cBox.Text;
                
            }

            set { ;}
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cBoxViewSource")));
            if (ComboBoxDataSource != null)
            {
                SetUser(false);
                ViewSource.Source = ComboBoxDataSource;
            }
        
        }

        private void SetUser(bool State)
        {
            bUser = State;

            //if (State)
            //{
            //    tBox.Background = Brushes.Yellow;
            //    tBox.Foreground = Brushes.Red;
            //}
            //else
            //{
            //    tBox.Background = Brushes.White;
            //    tBox.Foreground = Brushes.Black;  
            //}
        }

        private void cBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SetUser(true);

            //if (e.Text != "\r")
            //{

            //    tBox.Visibility = System.Windows.Visibility.Visible;
            //    cBox.Visibility = Visibility.Collapsed;

            //    tBox.SelectionStart = tBox.Text.Length;
            //    tBox.Focus();
            //}
        }



        private void tBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                SetUser(true);
                tBox.Visibility = System.Windows.Visibility.Collapsed;
                cBox.Visibility = Visibility.Visible;

                cBox.IsDropDownOpen = true;

                if (onfcbChanged != null)
                {
                    onfcbChanged(this, new FilteredComboBoxChangedEventArgs(tBox.Text));
                }


               // tBox.Text = "";
                cBox.Focus();
                cBox.IsDropDownOpen = true;
            }
        }

        private void cBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cBox.SelectedValue != null)
                {
                    tBox.Text = cBox.SelectedValue.ToString();
                    tBox.Visibility = System.Windows.Visibility.Visible;
                    cBox.Visibility = System.Windows.Visibility.Collapsed;
                }

            }
            catch (Exception)
            {


            }
            

            if (OnFcb_SelectionChanged != null)
            {
                if (bUser)
                {

                   OnFcb_SelectionChanged(this.cBox, e);
                }

            }

        }

        private void cBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetUser(true);
        }

        private void cBox_KeyDown(object sender, KeyEventArgs e)
        {
            SetUser(true);
            if (e.Key == Key.Escape)
            {
                cBox.Visibility = System.Windows.Visibility.Collapsed;
                tBox.Visibility = System.Windows.Visibility.Visible;
                tBox.SelectionStart = tBox.Text.Length;
                tBox.Focus();
            }

            //if (e.Key != Key.Return)
            //{

            //    tBox.Visibility = System.Windows.Visibility.Visible;
            //    cBox.Visibility = Visibility.Collapsed;

            //    tBox.SelectionStart = tBox.Text.Length;
            //    tBox.Focus();
            //}
        }

        private void cBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //cBox.Visibility = System.Windows.Visibility.Collapsed;
            //tBox.Visibility = System.Windows.Visibility.Visible;
            //tBox.SelectionStart = tBox.Text.Length;
        }

        private void cBox_DropDownClosed(object sender, EventArgs e)
        {
            cBox.Visibility = System.Windows.Visibility.Collapsed;
            tBox.Visibility = System.Windows.Visibility.Visible;
            tBox.SelectionStart = tBox.Text.Length;
        }

    



    }
}
