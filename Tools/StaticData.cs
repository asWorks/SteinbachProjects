using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProjektDB.Tools
{
    public static  class StaticData
    {
        private static Dictionary<string, enumDataType> _DataTypeList;

        public static Dictionary<string, enumDataType> DataTypeList
        {
            get
            {
                if (_DataTypeList == null)
                {
                    _DataTypeList = new Dictionary<string, enumDataType>();
                }
                return _DataTypeList;
            }
            set
            {
                if (_DataTypeList == null)
                {
                    _DataTypeList = new Dictionary<string, enumDataType>();
                }
                _DataTypeList = value;
            }
        }

    }
}
