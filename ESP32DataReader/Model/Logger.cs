using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP32DataReader.Model
{
    static public class Logger
    {/// <summary>
     /// Log message to file and status section
     /// </summary>
     /// <param name="message">Message to wrtie</param>
     /// <param name="type">0 - Success, 1 - Info, 2 - Warning, 3 - Error</param>
     /// <param name="sendMessage">false - dont send message to MainViewModel, true - send</param>
     /// <param name="logToFile">false - dont send message to MainViewModel, true - send</param>
     /// <returns></returns>
        static public void LogMessage(string message, int type = 1)
        {
            string output = string.Empty;

            try
            {
                if (type < 0 || type >= TextUtilities.msgTypes.Count)
                    type = 1;

                string logFormat = TextUtilities.GetLogPrefix(type, false);

                output = logFormat + message;

                LogToFile(output);
            }
            catch (Exception ex)
            { }
        }

        static public void LogEmpty()
        {
            LogToFile(string.Empty);
        }

        private static void LogToFile(string message)
        {
            StreamWriter sw = null;

            string pathName = @"C:\Users\Public\ESP32Reader\Logs\";
            string filename = "ESP32Reader_";
            string errorDate = DateTime.Now.ToString("yyyy-MM-dd");
            string fullPath = pathName + filename + errorDate + ".txt";

            if (!Directory.Exists(pathName))
                Directory.CreateDirectory(pathName);

            sw = new StreamWriter(fullPath, true);

            sw.WriteLine(message);
            sw.Flush();

            if (sw != null)
            {
                sw.Dispose();
                sw.Close();
            }
        }
    }
}
