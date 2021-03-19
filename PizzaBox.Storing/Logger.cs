using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PizzaBox.Storing
{
    public class Logger
    {
        private readonly string _errorLogPath = "ErrorLog.txt";
        private readonly string _logPath = "log.txt";
        private static Logger _logger = new Logger();
        private StreamWriter errorWriter;
        private StreamWriter logWriter;

        private Logger()
        {
            errorWriter = new StreamWriter(_errorLogPath);
            logWriter = new StreamWriter(_logPath);
        }

        public static Logger Instance
        {
            get
            {
                return _logger;
            }
        }

        public void Log(string message)
        {
            logWriter.WriteLine(DateTime.Now.ToString() + ": " + message);
        }

        public void LogError(string message)
        {
            errorWriter.WriteLine(DateTime.Now.ToString() + ": " + message);
        }

        public void WriteToXml<T>(List<T> data, string path) where T : class
        {
            using (var writer = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(writer, data);
            }
        }

    }
}