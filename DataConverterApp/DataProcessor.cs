using System.Xml;
using System.Xml.Schema;

namespace DataConverterApp
{
    public class DataProcessor
    {
        private IDataReader<DataModel> dataReader;
        private IDataWriter<DataModel> dataWriter;

        public DataProcessor(IDataReader<DataModel> reader, IDataWriter<DataModel> writer)
        {
            dataReader = reader;
            dataWriter = writer;
        }

        public bool ProcessData(string inputFilePath, string outputFilePath)
        {
            if (!ValidateXmlAgainstSchema())
            {
                Console.WriteLine("XML validation against schema failed.");
                return false;
            }

            var dataModel = dataReader.Read(inputFilePath);

            if (dataModel != null)
            {
                UpdateDataModel(dataModel);
                dataWriter.Write(outputFilePath, dataModel);
                Console.WriteLine("Data updated and saved.");
                return true; // Success
            }

            return false; // Processing failed
        }

        private static void UpdateDataModel(DataModel dataModel)
        {
            if (dataModel == null || dataModel.Vehicles == null)
            {
                return;
            }

            foreach (var vehicle in dataModel.Vehicles)
            {
                UpdateVehicleID(vehicle);
                UpdateTruckMotor(vehicle);
                UpdateBicycleWheelPressure(vehicle);
                UpdateHybridCarColor(vehicle);
            }
        }

        private static void UpdateVehicleID(Vehicle vehicle)
        {
            // Generate a unique ID for the vehicle
            vehicle.ID = GenerateUniqueID().ToString();
        }

        private static void UpdateTruckMotor(Vehicle vehicle)
        {
            if (vehicle.Type == "truck" && (vehicle.Motor == null || string.IsNullOrEmpty(vehicle.Motor.Displacement)))
            {
                if (vehicle.Motor == null)
                {
                    vehicle.Motor = new Motor();
                }
                vehicle.Motor.Displacement = "3000";
            }
        }

        private static void UpdateBicycleWheelPressure(Vehicle vehicle)
        {
            if (vehicle.Type == "bicycle" && vehicle.Wheels != null)
            {
                foreach (var wheel in vehicle.Wheels.WheelList)
                {
                    if (wheel.Size == "14")
                    {
                        wheel.Pressure = "50";
                    }
                }
            }
        }

        private static void UpdateHybridCarColor(Vehicle vehicle)
        {
            if (vehicle.Type == "car" && vehicle.Motor != null && vehicle.Motor.Type == "hybrid")
            {
                vehicle.Color = "red";
            }
        }

        private static int GenerateUniqueID()
        {
            return Guid.NewGuid().GetHashCode();
        }


        private static bool ValidateXmlAgainstSchema()
        {
            try
            {
                // Define your schema
                string schemaContent = @"<xs:schema attributeFormDefault=""unqualified"" elementFormDefault=""qualified"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""vehicles"">
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""vehicle"" maxOccurs=""unbounded"" minOccurs=""0"">
          <xs:complexType>
            <xs:sequence>
              <xs:element name=""motor"" minOccurs=""0"">
                <xs:complexType mixed=""true"">
                  <xs:sequence>
                    <xs:element type=""xs:short"" name=""power"" minOccurs=""0""/>
                  </xs:sequence>
                  <xs:attribute type=""xs:string"" name=""type"" use=""optional""/>
                </xs:complexType>
              </xs:element>
              <xs:element name=""wheels"">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name=""wheel"" maxOccurs=""unbounded"" minOccurs=""0"">
                      <xs:complexType>
                        <xs:simpleContent>
                          <xs:extension base=""xs:string"">
                            <xs:attribute type=""xs:byte"" name=""size"" use=""optional""/>
                            <xs:attribute type=""xs:byte"" name=""pressure"" use=""optional""/>
                          </xs:extension>
                        </xs:simpleContent>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
            </xs:sequence>
            <xs:attribute type=""xs:string"" name=""type"" use=""optional""/>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";

                XmlSchemaSet schemaSet = new XmlSchemaSet();
                schemaSet.Add("", XmlReader.Create(new StringReader(schemaContent)));

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas = schemaSet;
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += (sender, args) =>
                {
                    Console.WriteLine($"Validation Error: {args.Exception.Message}");
                };

                using (XmlReader reader = XmlReader.Create(schemaContent, settings))
                {
                    while (reader.Read()) { }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML validation error: {ex.Message}");
                return false;
            }
        }
    }
}
