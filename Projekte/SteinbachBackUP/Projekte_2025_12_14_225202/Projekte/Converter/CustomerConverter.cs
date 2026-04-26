using System;
using System.Linq;
using DAL;
using System.ComponentModel;
using System.Globalization;

namespace ProjektDB
{
    public class CustomerConverter
           : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null) return string.Empty;

            return ((firma)value).name;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            using (SteinbachEntities db = new SteinbachEntities())
            {
                string text = (string)value;
                var item = from c in db.firmen where c.name == text select c;
                if (item.Count() != 0)
                   // return item.FirstOrDefault();
                    return db.firmen.First<firma>(new Func<firma, bool>(c =>
                    {
                        return c.name == text;
                    }));

                else
                    return new firma { name = text };
            }
        }

    }
}
