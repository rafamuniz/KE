using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarmicEnergy.Service
{
    public static class Logger
    {
        public static void WriteLog(String message)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(String.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "LogFile.txt"), true);
                sw.WriteLine(String.Format("{0}: {1}", DateTime.Now, message));
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }
    }
}
