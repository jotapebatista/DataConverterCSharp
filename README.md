# Data Converter Application

The Data Converter Application is a C# program that allows you to convert data between XML and JSON formats. You can use this tool to:

- Convert XML data to JSON.
- Convert JSON data to XML.

## Table of Contents

- [Getting Started](#getting-started)
- [Usage](#usage)
- [File Extensions](#file-extensions)
- [Validation](#validation)
- [Project Structure](#project-structure)

## Getting Started

To get started, follow these instructions:

1. Clone this repository to your local machine.

2. Build the project using a C# development environment such as Visual Studio or Visual Studio Code.

3. Open the command prompt or terminal in the project directory.

4. Run the application with the appropriate arguments to convert your data. See [Usage](#usage) for more details.

## Usage

The Data Converter Application accepts two arguments: `inputFilePath` and `outputFilePath`. It will detect whether you are converting from XML to JSON or from JSON to XML based on the file extensions provided in the arguments.

### Examples

**Convert XML to JSON:**

```bash
DataConverterApp input.xml output.json
```

**Convert JSON to XML:**

```bash
DataConverterApp input.json output.xml
```

## Validation

The application validates the input XML data against a predefined XML Schema Definition (XSD) to ensure that it conforms to the expected structure. If validation fails, an error message will be displayed.

## Project Structure

- `DataConverterApp` contains the main application code.

- `schema.xsd` is the XML Schema Definition used for validation.
