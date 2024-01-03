using System.Dynamic;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace PathfinderFxGateway;

public static class Utils
{
    public static string JsonToCsv(string jsonContent, string delimiter)
    {
        var expands = JsonConvert.DeserializeObject<ExpandoObject[]>(jsonContent);

        using var writer = new StringWriter();
        using (var csv = new CsvWriter(writer, new CsvConfiguration(new System.Globalization.CultureInfo("en-US"))
        {
            HasHeaderRecord = true,
            IgnoreBlankLines = true,
            Delimiter = delimiter
        }))
        {
            csv.WriteRecords(expands as IEnumerable<dynamic>);
        }

        return writer.ToString();
    }
}