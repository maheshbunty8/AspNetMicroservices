using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace CsvFileWriter
{
    public class ExportMapping
    {
        public string PropertyName { get; set; }
        public int ColumnIndex { get; set; }
        public string DataType { get; set; }
    }

    internal class CsvFileWriter
    {
        public static void ExportToCsv(List<Person> people, string filePath)
        {
            // Read the mapping JSON file
            string mappingJson = File.ReadAllText("mapping.json");
            var mappings = JsonSerializer.Deserialize<ExportMapping[]>(mappingJson);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Write the header row
                foreach (var mapping in mappings)
                {
                    csv.WriteField(mapping.PropertyName);
                }
                csv.NextRecord();

                // Write the data rows
                foreach (var person in people)
                {
                    foreach (var mapping in mappings)
                    {
                        var propertyValue = person.GetType().GetProperty(mapping.PropertyName).GetValue(person);
                        csv.WriteField(propertyValue);
                    }
                    csv.NextRecord();
                }
            }
        }
    }
}
