using System;
using System.IO;

namespace ACI.Infrastructure.CrossCutting.Logging
{
    public static class Logger
    {
        private static string GetFileName()
        {
            return "log_" + (
                DateTime.Now.Year + "" +
                DateTime.Now.Month + "" +
                DateTime.Now.Day) + ".log";
        }

        public static void WriteLog(string message, string stackTrace)
        {

            string path = Environment.CurrentDirectory + @"\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string content = "";
            content += DateTime.Now + "_" + "Message:" + message +
                                        " StackTrace:" + stackTrace.Trim() +
                                        Environment.NewLine;
            using (StreamWriter streamWriter = new StreamWriter(path + "/" + GetFileName(), true))
            {
                streamWriter.Write(content);
                streamWriter.Close();
            }
        }
    }
}
