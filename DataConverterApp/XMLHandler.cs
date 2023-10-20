using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DataConverterApp
{
    public class XmlDataReader<T> : IDataReader<T>
    {
        public T Read(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
    }

    public class XmlDataWriter<T> : IDataWriter<T>
    {
        public void Write(string filePath, T data)
        {
            using (var writer = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, data);
            }
        }
    }
}
