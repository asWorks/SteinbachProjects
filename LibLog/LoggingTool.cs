using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibLog
{
    public class LoggingTool
    {
        public enum LogState
        {
            high = 4,
            medium = 2,
            low =1,
            critical = 8
        }

        public static void LogExeption(Exception ex,string ClassName, string procedure)
        {
            // toDo
            // Logging installieren.

            //System.Text.StringBuilder sb = new StringBuilder();
            //sb.AppendLine(ex.Message);
            //if (ex.InnerException != null)
            //    sb.AppendLine(ex.InnerException.Message);

            //sb.AppendLine(ClassName);
            //sb.AppendLine(procedure);
            //sb.AppendLine(DateTime.Now.ToLongDateString());

            //System.Windows.MessageBox.Show(sb.ToString());

            LoggingTool.LogExeption(ex, ClassName, procedure, LogState.low);
                    
        
        }

        public static void LogExeption(Exception ex, string ClassName, string procedure,LogState state)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.AppendLine(ex.Message);
            if (ex.InnerException != null)
                sb.AppendLine(ex.InnerException.Message);

            sb.AppendLine(ClassName);
            sb.AppendLine(procedure);
            sb.AppendLine(DateTime.Now.ToLongDateString());

            System.Windows.MessageBox.Show(sb.ToString());

        }


    }
}
