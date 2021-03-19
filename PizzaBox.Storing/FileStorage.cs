using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PizzaBox.Storing
{
    public class FileStorage
    {
        private static FileStorage _fileStorage = new FileStorage();

        private FileStorage() { }

        public static FileStorage Instance
        {
            get
            {
                return _fileStorage;
            }
        }
        
        public void WriteToXml<T>(List<T> data, string path) where T : class
        {
            using (var writer = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(writer, data);
            }
        }

        public IEnumerable<T> ReadFromXml<T>(string path) where T : class
        {
            using (var reader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                return serializer.Deserialize(reader) as IEnumerable<T>;
            }
        }
    }
}