using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP32DataReader.Model
{
    public static class TextUtilities
    {
        public static List<string> msgTypes = new List<string>()
        {
            "Success", "Info", "Warning", "Error"
        };

        public static List<string> msgTypesPL = new List<string>()
        {
            "Sukces", "Informacja", "Ostrzeżenie", "Błąd"
        };

        /// <summary>
        /// Get log prefix from
        /// </summary>
        /// <param name="code">0 - Success, 1 - Info, 2 - Warning, 3 - Error</param>
        /// <param name="shortDateTime">true - shot date time</param>
        /// <param name="language">0 - English, 1 - Polish</param>
        /// <returns></returns>
        public static string GetLogPrefix(int code, bool shortDateTime, int language = 0)
        {
            return DateTime.Now.ToString((!shortDateTime ? "yyyy-MM-dd " : "") + "HH:mm" + (!shortDateTime ? ":ss.fff" : ""))
                    + " [" + (language == 0 ? msgTypes.ElementAt(code) : msgTypesPL.ElementAt(code)) + "] ";
        }
    }
}
