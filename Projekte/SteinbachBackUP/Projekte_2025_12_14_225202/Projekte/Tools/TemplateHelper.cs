using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

using System.Windows;
using System.IO;
using System.Xml;


namespace ProjektDB.Tools
{
    class TemplateHelper
    {



        public static void AddColumns(C1.WPF.DataGrid.C1DataGrid dg, List<ColumnDescription> cList)
        {


            StaticData.DataTypeList.Clear();
            foreach (var cd in cList)
            {

                string buf = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                if (buf.ToLower() != "id")
                {
                    StaticData.DataTypeList.Add(buf, cd.DataType);
                }



                if (cd.TypeEnum == enumType.Date)
                {
                    C1.WPF.DataGrid.DataGridTextColumn col =
                        cd.oBinding != null ? TemplateHelper.Getc1TextColumn(cd.header, cd.oBinding)
                            : TemplateHelper.Getc1TextColumn(cd.header, cd.binding);
                    if (cd.WidthAuto)
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width, C1.WPF.DataGrid.DataGridUnitType.Auto);
                    else
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width);

                    if (cd.SetFilterPath)
                        col.FilterMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    if (cd.SetSortPath)
                        col.SortMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    col.Name = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    col.Format = "dd.MM.yyyy";
                    col.HorizontalAlignment = HorizontalAlignment.Right;
                    col.VerticalAlignment = VerticalAlignment.Top;
                    dg.Columns.Add(col);

                }

                else if (cd.TypeEnum == enumType.Currency)
                {
                    C1.WPF.DataGrid.DataGridTextColumn col =
                        cd.oBinding != null ? TemplateHelper.Getc1TextColumn(cd.header, cd.oBinding)
                            : TemplateHelper.Getc1TextColumn(cd.header, cd.binding);
                    if (cd.WidthAuto)
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width, C1.WPF.DataGrid.DataGridUnitType.Auto);
                    else
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width);

                    if (cd.SetFilterPath)
                        col.FilterMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    if (cd.SetSortPath)
                        col.SortMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    col.Name = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    col.Format = "{0:f}";
                    col.HorizontalAlignment = HorizontalAlignment.Right;
                    col.VerticalAlignment = VerticalAlignment.Top;
                    dg.Columns.Add(col);

                }



                else if (cd.TypeEnum == enumType.Checkbox)
                {
                    C1.WPF.DataGrid.DataGridCheckBoxColumn col =
                        cd.oBinding != null ? TemplateHelper.Getc1CheckBoxColumn(cd.header, cd.oBinding)
                            : TemplateHelper.Getc1CheckBoxColumn(cd.header, cd.binding);

                    if (cd.WidthAuto)
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width, C1.WPF.DataGrid.DataGridUnitType.Auto);
                    else
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width);

                    if (cd.SetFilterPath)
                        col.FilterMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    if (cd.SetSortPath)
                        col.SortMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    col.Name = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    col.HorizontalAlignment = HorizontalAlignment.Right;

                    dg.Columns.Add(col);

                }

                else if (cd.TypeEnum == enumType.MultiLineText)
                {
                    DataGridMultiLineTextColumn col =
                         cd.oBinding != null ? TemplateHelper.Getc1MultiLineTextColumn(cd.header, cd.oBinding)
                             : TemplateHelper.Getc1MultiLineTextColumn(cd.header, cd.binding);

                    if (cd.WidthAuto)
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width, C1.WPF.DataGrid.DataGridUnitType.Auto);
                    else
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width);

                    if (cd.SetFilterPath)
                        col.FilterMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    if (cd.SetSortPath)
                        col.SortMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    //col.Name = cd.binding;

                    dg.Columns.Add(col);

                }






                else if (cd.TypeEnum == enumType.Hyperlink)
                {
                    C1.WPF.DataGrid.DataGridHyperlinkColumn col =
                         cd.oBinding != null ? TemplateHelper.Getc1HyperlinkColumn(cd.header, cd.oBinding)
                             : TemplateHelper.Getc1HyperlinkColumn(cd.header, cd.binding);





                    if (cd.WidthAuto)
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width, C1.WPF.DataGrid.DataGridUnitType.Auto);
                    else
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width);

                    if (cd.SetFilterPath)
                        col.FilterMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    if (cd.SetSortPath)
                        col.SortMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    //col.Name = cd.binding;

                    dg.Columns.Add(col);

                }



                else if (cd.TypeEnum == enumType.Image)
                {
                    C1.WPF.DataGrid.DataGridImageColumn col =
                        cd.oBinding != null ? TemplateHelper.Getc1ImageColumn(cd.header, cd.oBinding)
                            : TemplateHelper.Getc1ImageColumn(cd.header, cd.binding);

                    if (cd.WidthAuto)
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width, C1.WPF.DataGrid.DataGridUnitType.Auto);
                    else
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width);

                    if (cd.SetFilterPath)
                        col.FilterMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    if (cd.SetSortPath)
                        col.SortMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    col.Name = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    col.Stretch = System.Windows.Media.Stretch.None;


                    // col.HorizontalAlignment = HorizontalAlignment.Right;

                    dg.Columns.Add(col);

                }

                else if (cd.TypeEnum == enumType.Text)
                {
                    C1.WPF.DataGrid.DataGridTextColumn col =
                        cd.oBinding != null ? TemplateHelper.Getc1TextColumn(cd.header, cd.oBinding)
                            : TemplateHelper.Getc1TextColumn(cd.header, cd.binding);

                    if (cd.WidthAuto)
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width, C1.WPF.DataGrid.DataGridUnitType.Auto);
                    else
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width);



                    if (cd.SetFilterPath)
                    {
                        if (cd.oBinding != null)
                        {
                            if (cd.oBinding.Path.PathParameters.Count == 1)
                            {
                                col.FilterMemberPath = cd.oBinding.Path.PathParameters[0].ToString();
                            }
                            else
                            {
                                col.FilterMemberPath = cd.oBinding.Path.Path;
                            }
                        }
                        else
                        {
                            col.FilterMemberPath = cd.binding;
                        }
                        
                        
                    }



                    if (cd.SetSortPath)
                    {
                        if (cd.oBinding != null)
                        {
                            if (cd.oBinding.Path.PathParameters.Count == 1)
                            {
                                col.SortMemberPath = cd.oBinding.Path.PathParameters[0].ToString();
                            }
                            else
                            {
                                col.SortMemberPath = cd.oBinding.Path.Path;
                            }
                        }
                        else
                        {
                            col.SortMemberPath = cd.binding;
                        }

                    }


                    col.Name = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;




                    dg.Columns.Add(col);


                }
                else if (cd.TypeEnum == enumType.TemplateBackground)
                {

                    string binding = string.Empty;
                    if (cd.binding == null)
                    {
                        binding = cd.oBinding.Path.Path;
                    }
                    else
                    {
                        binding = cd.binding;
                    }

                    C1.WPF.DataGrid.DataGridTemplateColumn col = TemplateHelper.Getc1TextTemplate(cd.header, binding, cd.BackGroundBinding);


                    if (cd.WidthAuto)
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width, C1.WPF.DataGrid.DataGridUnitType.Auto);
                    else
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width);

                    if (cd.SetFilterPath)
                        col.FilterMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    if (cd.SetSortPath)
                        col.SortMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    col.Name = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    dg.Columns.Add(col);
                }



                else
                {
                    C1.WPF.DataGrid.DataGridTemplateColumn col = TemplateHelper.Getc1TextTemplate(cd.header, cd.binding);

                    if (cd.WidthAuto)
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width, C1.WPF.DataGrid.DataGridUnitType.Auto);
                    else
                        col.Width = new C1.WPF.DataGrid.DataGridLength(cd.width);

                    if (cd.SetFilterPath)
                        col.FilterMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;
                    if (cd.SetSortPath)
                        col.SortMemberPath = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    col.Name = cd.oBinding != null ? cd.oBinding.Path.Path : cd.binding;

                    dg.Columns.Add(col);
                }


            }

        }






        protected static DataGridMultiLineTextColumn Getc1MultiLineTextColumn(string header, Binding b)
        {
            var col = new DataGridMultiLineTextColumn { Header = header, Binding = b };
            return col;

        }
        protected static DataGridMultiLineTextColumn Getc1MultiLineTextColumn(string header, string binding)
        {
            var col = new DataGridMultiLineTextColumn { Header = header, Binding = new Binding(binding) };
            return col;

        }

        protected static C1.WPF.DataGrid.DataGridHyperlinkColumn Getc1HyperlinkColumn(string header, Binding b)
        {
            var col = new C1.WPF.DataGrid.DataGridHyperlinkColumn { Header = header, Binding = b };
            //col.NavigateUri = new Uri("MainWindow.xaml?Id=49", UriKind.Relative);
            col.TargetName = "MainWindow.xaml?Id=49";
            col.Click += col_Click;


            return col;

        }


        protected static C1.WPF.DataGrid.DataGridHyperlinkColumn Getc1HyperlinkColumn(string header, string binding)
        {
            var col = new C1.WPF.DataGrid.DataGridHyperlinkColumn { Header = header, Binding = new Binding(binding) };
            col.TargetName = "MainWindow.xaml?Id=49";
            col.Click += col_Click;
            return col;

        }

        static void col_Click(object sender, C1.WPF.DataGrid.DataGridRowEventArgs e)
        {


            var col = (C1.WPF.C1HyperlinkButton)sender;
            //col.Click += new RoutedEventHandler(col_ClickHyper);
            col.NavigateUri = new Uri("MainWindow.xaml?Id=49", UriKind.Relative);

            //MessageBox.Show(col.TargetName);
            //var item = (DAL.kalkulationstabelle)(e.Row.DataItem);
            //var rep = new Repositories.ProjektRepository(new DAL.SteinbachEntities());
            //int id = rep.GetProjektIDFromNumber(item.projektnummer);
            //if (id != 0)
            //{
            //    var p = new MainWindow(id);
            //    //System.Windows.Navigation.NavigationService.GetNavigationService();
            //    //ns.Navigate(p);
            //}


        }

        static void col_ClickHyper(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


        protected static C1.WPF.DataGrid.DataGridImageColumn Getc1ImageColumn(string header, string binding)
        {
            var col = new C1.WPF.DataGrid.DataGridImageColumn();

            col.Header = header;
            col.Binding = new Binding(binding);


            return col;
        }

        protected static C1.WPF.DataGrid.DataGridImageColumn Getc1ImageColumn(string header, Binding binding)
        {
            var col = new C1.WPF.DataGrid.DataGridImageColumn();

            col.Header = header;
            col.Binding = binding;

            return col;
        }

        protected static C1.WPF.DataGrid.DataGridTextColumn Getc1TextColumn(string header, Binding b)
        {
            var col = new C1.WPF.DataGrid.DataGridTextColumn { Header = header, Binding = b };


            return col;



        }
        protected static C1.WPF.DataGrid.DataGridTextColumn Getc1TextColumn(string header, string binding)
        {

            //var b = new Binding();
            //b.
            //var tb= new System.Windows.Controls.TextBlock();



            var col = new C1.WPF.DataGrid.DataGridTextColumn { Header = header, Binding = new Binding(binding) };
            return col;

        }

        protected static C1.WPF.DataGrid.DataGridCheckBoxColumn Getc1CheckBoxColumn(string header, Binding b)
        {

            var col = new C1.WPF.DataGrid.DataGridCheckBoxColumn { Header = header, Binding = b };
            return col;
        }

        protected static C1.WPF.DataGrid.DataGridCheckBoxColumn Getc1CheckBoxColumn(string header, string binding)
        {


            var col = new C1.WPF.DataGrid.DataGridCheckBoxColumn { Header = header, Binding = new Binding(binding) };
            return col;
        }




        protected static C1.WPF.DataGrid.DataGridTemplateColumn Getc1TextTemplate(string header, string binding, string BackgroundBinding)
        {


            C1.WPF.DataGrid.DataGridTemplateColumn templateColumn = new C1.WPF.DataGrid.DataGridTemplateColumn();

            templateColumn.Header = header;


            StringBuilder CellTemp = new StringBuilder();
            CellTemp.Append("<DataTemplate ");
            CellTemp.Append("xmlns='http://schemas.microsoft.com/winfx/");
            CellTemp.Append("2006/xaml/presentation' ");
            CellTemp.Append("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");

            //Be sure to replace "YourNamespace" and "YourAssembly" with your app's 
            //actual namespace and assembly here
            CellTemp.Append("xmlns:local = 'clr-namespace:ProjektDB");
            CellTemp.Append(";assembly=ProjektDB'>");


            //CellTemp.Append("xmlns:local = 'clr-namespace:ProjektDB");
            //CellTemp.Append(";assembly=ProjektDB'>");

            CellTemp.Append("<Grid>");
            CellTemp.Append("<Grid.Resources>");
            CellTemp.Append("<local:LagerlisteGesamtbestandConverter x:Key='LagerlisteBestandConverter'/>");
            CellTemp.Append("<local:BackgroundConverter x:Key='BackgroundColorConverter'/>");
            CellTemp.Append("<local:BackgroundColorMultiValueConverter x:Key='MultiBindBackgroundConverter'/>");

            CellTemp.Append("</Grid.Resources>");
            CellTemp.Append("<TextBlock Margin='4'>");
            CellTemp.Append("<TextBlock.Text>");
            //CellTemp.Append("Text = '{Binding " + binding + " }' ");
            //CellTemp.Append("<Binding Path = '" + binding + "'/>");
            CellTemp.Append("<Binding Path = '" + binding + "' ");
            CellTemp.Append("Converter='{StaticResource LagerlisteBestandConverter}'/> ");
            CellTemp.Append("</TextBlock.Text>");
            //CellTemp.Append("Background = '{Binding " + BackgroundBinding + " , ");
            //CellTemp.Append("Converter={StaticResource BackgroundColorConverter}}' ");


            CellTemp.Append("<TextBlock.Background>");
            CellTemp.Append("<MultiBinding Converter='{StaticResource MultiBindBackgroundConverter}'>");
            CellTemp.Append("<Binding Path= '" + binding + "'/>");
            CellTemp.Append("<Binding Path= '" + BackgroundBinding + "'/>");
            //CellTemp.Append("<Binding Path= 'anzahlauflager'/>");
            //CellTemp.Append("<Binding Path= 'anzahlmin'/>");

            CellTemp.Append("</MultiBinding>");
            CellTemp.Append("</TextBlock.Background>");


            CellTemp.Append("</TextBlock>");
            CellTemp.Append("</Grid>");

            CellTemp.Append("</DataTemplate>");


            //<TextBlock>
            //  <TextBlock.Text>
            //      <MultiBinding Converter="{StaticResource MultiBindConverter}">
            //            <Binding Path="addtype"/>
            //            <Binding Path="anzahl"/>
            //       </MultiBinding>
            //    </TextBlock.Text>
            // </TextBlock>

            StringBuilder CellETemp = new StringBuilder();
            CellETemp.Append("<DataTemplate ");
            CellETemp.Append("xmlns='http://schemas.microsoft.com/winfx/");
            CellETemp.Append("2006/xaml/presentation' ");
            CellETemp.Append("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            CellETemp.Append("xmlns:basics='clr-namespace:System.Windows.Controls;");
            CellETemp.Append("assembly=System.Windows.Controls' >");
            CellETemp.Append("<TextBox ");
            CellETemp.Append(String.Format("Text = '{{Binding {0}, Mode=TwoWay}}' ", binding));
            CellETemp.Append("Background = '{Binding " + BackgroundBinding + " , ");
            CellETemp.Append("Converter={StaticResource BackgroundColorConverter}}'/>");

            CellETemp.Append("</DataTemplate>");




            templateColumn.CellTemplate = (DataTemplate)XamlReader.Load(new XmlTextReader(new StringReader(CellTemp.ToString())));
            templateColumn.CellEditingTemplate = (DataTemplate)XamlReader.Load(new XmlTextReader(new StringReader(CellETemp.ToString())));



            return templateColumn;

        }


        protected static C1.WPF.DataGrid.DataGridTemplateColumn Getc1TextTemplate(string header, string binding)
        {


            C1.WPF.DataGrid.DataGridTemplateColumn templateColumn = new C1.WPF.DataGrid.DataGridTemplateColumn();

            templateColumn.Header = header;

            StringBuilder CellTemp = new StringBuilder();
            CellTemp.Append("<DataTemplate ");
            CellTemp.Append("xmlns='http://schemas.microsoft.com/winfx/");
            CellTemp.Append("2006/xaml/presentation' ");
            CellTemp.Append("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");

            //Be sure to replace "YourNamespace" and "YourAssembly" with your app's 
            //actual namespace and assembly here
            CellTemp.Append("xmlns:local = 'clr-namespace:ProjektDB");
            CellTemp.Append(";assembly=ProjektDB'>");

            CellTemp.Append("<Grid>");
            CellTemp.Append("<Grid.Resources>");
            // CellTemp.Append("<local:DateTimeConverter x:Key='DateConverter' />");
            CellTemp.Append("</Grid.Resources>");
            CellTemp.Append("<TextBlock ");
            CellTemp.Append(String.Format("Text = '{{Binding {0} }}' ", binding));
            // CellTemp.Append("Converter={StaticResource DateConverter}}' ");
            CellTemp.Append("Margin='4'/>");
            CellTemp.Append("</Grid>");
            CellTemp.Append("</DataTemplate>");

            StringBuilder CellETemp = new StringBuilder();
            CellETemp.Append("<DataTemplate ");
            CellETemp.Append("xmlns='http://schemas.microsoft.com/winfx/");
            CellETemp.Append("2006/xaml/presentation' ");
            CellETemp.Append("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            CellETemp.Append("xmlns:basics='clr-namespace:System.Windows.Controls;");
            CellETemp.Append("assembly=System.Windows.Controls' >");
            CellETemp.Append("<TextBox ");
            CellETemp.Append(String.Format("Text = '{{Binding {0}, Mode=TwoWay}}' />", binding));
            CellETemp.Append("</DataTemplate>");



            templateColumn.CellTemplate = (DataTemplate)XamlReader.Load(new XmlTextReader(new StringReader(CellTemp.ToString())));
            templateColumn.CellEditingTemplate = (DataTemplate)XamlReader.Load(new XmlTextReader(new StringReader(CellETemp.ToString())));



            return templateColumn;

        }
    }
}
