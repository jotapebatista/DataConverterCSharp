namespace DataConverterApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DataConverterApp <inputFilePath> <outputFilePath>");
                return;
            }

            string inputFilePath = args[0];
            string outputFilePath = args[1];

            bool isXmlToJson = IsXmlToJsonConversion(inputFilePath, outputFilePath);

            try
            {
                // If it's XML to JSON conversion
                if (isXmlToJson)
                {
                    IDataReader<DataModel> xmlReader = new XmlDataReader<DataModel>();
                    IDataWriter<DataModel> jsonWriter = new JsonDataWriter<DataModel>();
                    DataProcessor processor = new DataProcessor(xmlReader, jsonWriter);
                    bool success = processor.ProcessData(inputFilePath, outputFilePath);
                    if (success)
                    {
                        Console.WriteLine("XML data converted to JSON.");
                    }
                }
                // If it's JSON to XML conversion
                else
                {
                    IDataReader<DataModel> jsonReader = new JsonDataReader<DataModel>();
                    IDataWriter<DataModel> xmlWriter = new XmlDataWriter<DataModel>();
                    DataProcessor processor = new DataProcessor(jsonReader, xmlWriter);
                    bool success = processor.ProcessData(inputFilePath, outputFilePath);
                    if (success)
                    {
                        Console.WriteLine("XML data converted to JSON.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static bool IsXmlToJsonConversion(string inputFilePath, string outputFilePath)
        {
            // Check if the input file has an XML extension and the output file has a JSON extension
            return inputFilePath.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) &&
                   outputFilePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase);
        }
    }
}
