using Newtonsoft.Json;

namespace DataConverterApp
{
    public class JsonDataReader<T> : IDataReader<T>
    {
        public T Read(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }

    public class JsonDataWriter<T> : IDataWriter<T>
    {
        public void Write(string filePath, T data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }
}
