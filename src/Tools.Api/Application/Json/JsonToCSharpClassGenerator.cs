using System.Text;
using System.Text.Json;

namespace Tools.Api.Application.Json;

public class JsonToCSharpClassGenerator
{
    public static string GenerateClassFromJson(string jsonString, string className)
    {
        var jsonDocument = JsonDocument.Parse(jsonString);
        var rootElement = jsonDocument.RootElement;

        var classDefinitions = new StringBuilder();
        ProcessElement(rootElement, className, classDefinitions);
        return classDefinitions.ToString();
    }

    private static void ProcessElement(JsonElement element, string className, StringBuilder classDefinitions)
    {
        var classCode = new StringBuilder($"public class {className}\n{{\n");

        foreach (var property in element.EnumerateObject())
        {
            string propertyName = property.Name;
            string propertyType = GetCSharpType(property.Value, propertyName, classDefinitions);
            classCode.AppendLine($"\tpublic {propertyType} {propertyName} {{ get; set; }}");
        }

        classCode.AppendLine("}\n");
        classDefinitions.Append(classCode);
    }

    private static string GetCSharpType(JsonElement element, string propertyName, StringBuilder classDefinitions)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Number:
                return element.TryGetInt32(out _) ? "int" : "double";
            case JsonValueKind.String:
                return "string";
            case JsonValueKind.True:
            case JsonValueKind.False:
                return "bool";
            case JsonValueKind.Object:
                string nestedClassName = propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1);
                ProcessElement(element, nestedClassName, classDefinitions);
                return nestedClassName;
            case JsonValueKind.Array:
                if (element.GetArrayLength() > 0)
                {
                    var firstElement = element[0];
                    string elementType = GetCSharpType(firstElement, propertyName, classDefinitions);
                    return $"List<{elementType}>";
                }
                return "List<object>";
            default:
                return "string";
        }
    }
}