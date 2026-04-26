using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DAL;
using System.Globalization;

namespace ProjektDB.Converter.ItemConverters
{
    public class ProjekteConverter: TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            try
            {
                if (value.GetType() != typeof(Models.ProjekteSource))
                {
                    return "";
                }

             var u = (Models.ProjekteSource)value;
             return u.projektnummer;
            }
            catch (Exception ex)
            {
              
                return value.GetType().ToString(); 
            }
           
        }
    }


}
