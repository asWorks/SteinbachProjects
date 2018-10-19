using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektDB.Temp
{

    public static class Output
    {
        public static void WriteMessage(string message)
        {
            try
            {
                using (var sr = new System.IO.StreamWriter(@"E:\test\output.txt", true))
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
