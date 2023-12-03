using System;
using System.Text.Json;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        string json = "{\"name\": \"John\", \"age\": 30, \"city\": \"New York\"}";

        try
        {
            string xml = ConvertJsonToXml(json);
            Console.WriteLine(xml);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static string ConvertJsonToXml(string json)
    {
        using (JsonDocument document = JsonDocument.Parse(json))
        {
            JsonElement root = document.RootElement;

            XElement xmlRoot = new XElement("root");
            ConvertJsonElementToXml(root, xmlRoot);

            return xmlRoot.ToString();
        }
    }

    static void ConvertJsonElementToXml(JsonElement jsonElement, XElement parentElement)
    {
        foreach (JsonProperty property in jsonElement.EnumerateObject())
        {
            if (property.Value.ValueKind == JsonValueKind.Object)
            {
                XElement childElement = new XElement(property.Name);
                ConvertJsonElementToXml(property.Value, childElement);
                parentElement.Add(childElement);
            }
            else if (property.Value.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement arrayElement in property.Value.EnumerateArray())
                {
                    XElement childElement = new XElement(property.Name);
                    ConvertJsonElementToXml(arrayElement, childElement);
                    parentElement.Add(childElement);
                }
            }
            else
            {
                XElement childElement = new XElement(property.Name, property.Value.ToString());
                parentElement.Add(childElement);
            }
        }
    }
}

