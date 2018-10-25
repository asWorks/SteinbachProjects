using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using C1.WPF;

namespace ProjektDB.Tools
{
    public class GetC1DatagridFilter
    {
        public Dictionary<string, string> Filter { get; set; }
        public string filterString { get; set; }

        public GetC1DatagridFilter()
        {

            Filter = new Dictionary<string, string>();


        }




        public string ResetFilter(C1.WPF.DataGrid.C1DataGrid c1Grid, bool bIncludeLike)
        {
            ReadColumns(c1Grid);
            return ReadDictionary(bIncludeLike);
        }

        public string ResetFilter(string prefilter, C1.WPF.DataGrid.C1DataGrid c1Grid, bool bIncludeLike)
        {
            ReadColumns(c1Grid);
            return ReadDictionary(bIncludeLike, prefilter);
        }



        private void ReadColumns(C1.WPF.DataGrid.C1DataGrid Grid)
        {
            
            Filter.Clear();
            filterString = string.Empty;
            //C1.WPF.DataGrid.DataGridColumn[] dc = Grid.FilteredColumns;
            //foreach (var d in dc)
            //{
            //    var fs = d.FilterState;
            //    Filter.Add(d.Name, fs.Tag.ToString());

            //}


            var filterrow = Grid.TopRows[0];
            foreach (var col in Grid.Columns)
            {
                try
                {
                    if (filterrow.Presenter != null)
                    {
                        if ((filterrow.Presenter[col].Content as C1TextBoxBase).C1Text != "")
                        {
                            Filter.Add(col.Name, (filterrow.Presenter[col].Content as C1TextBoxBase).C1Text);

                        }
                    }
                }
                catch (Exception)
                {

                   
                }

            }

        }





        //private void Test(C1.WPF.DataGrid.C1DataGrid Grid, Dictionary<string, string> filter)
        //{

        //     C1.WPF.DataGrid.DataGridColumn[]  cc = Grid.FilteredColumns;
        //    string defOut = "";
        //    foreach (var item in cc)
        //    {

        //        if (filter.ContainsKey(item.Name))
        //        {
        //            bool buf = filter.TryGetValue(item.Name, out defOut);
        //            var fs = new C1.WPF.DataGrid.DataGridFilterState();
        //            fs.Tag = defOut;
        //            item.FilterState.Tag = defOut;
        //      C1.WPF.DataGrid.C1DataGridFilterHelper.BuildFilterPredicate
        //        }                


        //    }

        //}



        private string ReadDictionary()
        {
            return ReadDictionary(false);
        }

        private string ReadDictionary(bool bInludeLike)
        {
            return ReadDictionary(bInludeLike, string.Empty);
        }


        private string ReadDictionary(bool bIncludeLike, string preFilter)
        {
            string f = string.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (var e in Filter)
            {
                enumDataType buf;
                bool r = StaticData.DataTypeList.TryGetValue(e.Key, out buf);
                if (r)
                {



                    if (buf == enumDataType.Date)
                    {
                        sb.Append("it.");
                        sb.Append(e.Key);
                        sb.Append(" >= ");
                        sb.Append(String.Format("DATETIME'{0} 00:00:00'", GetDateFormatted(e.Value)));
                        sb.Append(" and ");
                        sb.Append("it.");
                        sb.Append(e.Key);
                        sb.Append(" <= ");
                        sb.Append(String.Format("DATETIME'{0} 23:59:59'", GetDateFormatted(e.Value)));



                    }
                    else if (buf == enumDataType.Numeric)
                    {
                        sb.Append("it.");
                        sb.Append(e.Key);
                        sb.Append(" = ");
                        sb.Append(e.Value);
                        sb.Append(" and ");
                    }
                    else
                    {
                        sb.Append("it.");
                        sb.Append(e.Key);
                        sb.Append(" like ");
                        sb.Append("'");
                        if (bIncludeLike)
                            sb.Append("%");
                        sb.Append(e.Value);
                        if (bIncludeLike)
                            sb.Append("%");
                        sb.Append("'");
                        sb.Append(" and ");
                    }
                }
            }

            f = sb.ToString();
            if (f.EndsWith(" and "))
                f = f.Remove(f.LastIndexOf(" and "), 5);

            filterString = f;

            if (preFilter != string.Empty && f != string.Empty)
            {
                f = String.Format("{0} {1} {2}", preFilter, " and ", f);
                filterString = f;
            }
            else if (preFilter != string.Empty && f == string.Empty)
            {
                f = preFilter;
                filterString = preFilter;
            }
            return f;

        }

        string GetQuery()
        {
            return null;
        }

        string GetDateFormatted(string date)
        {
            int yy = DateTime.Now.Year;
            string y = yy.ToString().Substring(0, 2);
            if (date.Length == 8)
            {
                return String.Format("{4}{0}-{1}-{2}", date.Substring(6, 2), date.Substring(3, 2), date.Substring(0, 2), y);
            }
            else if (date.Length == 10)
            {
                return String.Format("{0}-{1}-{2}", date.Substring(6, 4), date.Substring(3, 2), date.Substring(0, 2));
            }
            else
            {
                return "";
            }


        }




    }
}
