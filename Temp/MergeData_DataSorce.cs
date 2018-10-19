using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB.Temp
{
    public class MergeData_DataSorce
    {
        public MergeData_DataSorce()
        {
            MergedData = new List<MergeData>();
            MergeData md = new MergeData("", "", "", "", "", "", "", "", "");
            MergedData.Add(md);
        }

        public MergeData_DataSorce(string D1, string D2, string D3, string D4, string D5, string D6, string D7, string D8, string D9)
        {
            MergedData = new List<MergeData>();
            MergeData md = new MergeData(D1, D2, D3, D4, D5, D6, D7, D8, D9);
            MergedData.Add(md);

        }

        public void SetValue(string Value, int index)
        {
            MergeData md = MergedData.ToArray()[0];
            switch (index)
            {
                case 0:
                    {
                        md.Data = Value;
                        break;
                    }

                case 1:
                    {
                        md.Data1 = Value;
                        break;
                    }

                case 2:
                    {
                        md.Data2 = Value;
                        break;
                    }

                case 3:
                    {
                        md.Data3 = Value;
                        break;
                    }
                case 4:
                    {
                        md.Data4 = Value;
                        break;
                    }

                case 5:
                    {
                        md.Data5 = Value;
                        break;
                    }

                case 6:
                    {
                        md.Data6 = Value;
                        break;
                    }

                case 7:
                    {
                        md.Data7 = Value;
                        break;
                    }
                case 8:
                    {
                        md.Data8 = Value;
                        break;
                    }

                default:
                    break;
            }
        }

        public List<MergeData> MergedData { get; set; }
    }

    public class MergeData
    {
        public string Data { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public string Data4 { get; set; }
        public string Data5 { get; set; }
        public string Data6 { get; set; }
        public string Data7 { get; set; }
        public string Data8 { get; set; }

        public MergeData(string d1, string d2, string d3, string d4, string d5, string d6, string d7, string d8, string d9)
        {
            Data = d1;
            Data1 = d2;
            Data2 = d3;
            Data3 = d4;
            Data4 = d5;
            Data5 = d6;
            Data6 = d7;
            Data7 = d8;
            Data8 = d9;

        }
    }
}
