using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibLog
{

    public static class Output
    {
        public static void WriteMessage(string message)
        {
            try
            {
                

                string pfad = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

                using (var sr = new System.IO.StreamWriter(pfad, true))
                {
                    sr.WriteLine(message);
                    sr.Flush();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }







}
