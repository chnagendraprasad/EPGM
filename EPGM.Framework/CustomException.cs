using System;
using System.IO;
using System.Text;
using System.Web;

namespace EPGM.Framework
{
    /// <summary>
    /// This class facilitates wrapping exceptions into custom exceptions
    /// </summary>
    public class CustomException : Exception
    {
        public CustomException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public CustomException(Exception exc, string Msg)
            : base(Msg)
        {
        }

        public CustomException(Exception exc)
        {
            // Include enterprise logic for logging exceptions 
            // Get the absolute path to the log file 
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Open the log file for append and write the log
            StringBuilder sw = new StringBuilder();
            sw.Append("********** {0} **********" + fileName);
            if (exc.InnerException != null)
            {
                sw.Append("Inner Exception Type: ");
                sw.AppendLine(exc.InnerException.GetType().ToString());
                sw.Append("Inner Exception: ");
                sw.AppendLine(exc.InnerException.Message);
                sw.Append("Inner Source: ");
                sw.AppendLine(exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                {
                    sw.AppendLine("Inner Stack Trace: ");
                    sw.AppendLine(exc.InnerException.StackTrace);
                }
            }
            sw.Append("Exception Type: ");
            sw.AppendLine(exc.GetType().ToString());
            sw.AppendLine("Exception: " + exc.Message);
            sw.AppendLine("Stack Trace: ");
            if (exc.StackTrace != null)
            {
                sw.AppendLine(exc.StackTrace);
                sw.AppendLine();
            }
            File.WriteAllText("E:\\ErrorLog_" + fileName + ".txt", sw.ToString());
        }
    }
}
