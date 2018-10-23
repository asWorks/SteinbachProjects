using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DAL;
using System.Globalization;

namespace ProjektDB.Converter.ItemConverters
{
    public class FirmenAdressenConverter: TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var u = (Firmen_Adressen)value;
            return u.ZusatzInfo;
        }
    }


}
