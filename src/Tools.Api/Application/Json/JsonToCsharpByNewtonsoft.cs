using System.Text;
using Newtonsoft.Json.Linq;

namespace Tools.Api.Application.Json;

public class JsonToCsharpByNewtonsoft
{
     public static string GenerateClassFromJson(string jsonString, string className)
    {
        JObject jsonObject = JObject.Parse(jsonString);
        var classDefinitions = new StringBuilder();
        ProcessJObject(jsonObject, className, classDefinitions);
        return classDefinitions.ToString();
    }

    private static void ProcessJObject(JObject jObject, string className, StringBuilder classDefinitions)
    {
        var classCode = new StringBuilder($"public class {className}\n{{\n");

        foreach (var property in jObject.Properties())
        {
            string propertyName = property.Name;
            string propertyType = GetCSharpType(property.Value, propertyName, classDefinitions);
            classCode.AppendLine($"\tpublic {propertyType} {propertyName} {{ get; set; }}");
        }

        classCode.AppendLine("}\n");
        classDefinitions.Append(classCode);
    }

    private static string GetCSharpType(JToken token, string propertyName, StringBuilder classDefinitions)
    {
        switch (token.Type)
        {
            case JTokenType.Integer:
                return "int";
            case JTokenType.Float:
                return "double";
            case JTokenType.String:
                return "string";
            case JTokenType.Boolean:
                return "bool";
            case JTokenType.Object:
                string nestedClassName = propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1);
                ProcessJObject((JObject)token, nestedClassName, classDefinitions);
                return nestedClassName;
            case JTokenType.Array:
                var firstElement = token.First;
                if (firstElement != null)
                {
                    string elementType = GetCSharpType(firstElement, propertyName, classDefinitions);
                    return $"List<{elementType}>";
                }
                return "List<object>";
            default:
                return "string";
        }
    }

}