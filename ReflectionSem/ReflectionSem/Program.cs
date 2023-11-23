using System;
using System.Reflection;
using System.Linq;

namespace ReflectionSem
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CustomNameAttribute : Attribute
    {
        public string CustomFieldName { get; }
        public CustomNameAttribute(string customFieldName)
        {
            CustomFieldName = customFieldName;
        }
    }
    public static class SerializationExtensions
    {
        public static string ObjectToString(this object obj)
        {
            var properties = obj.GetType().GetFields()
                .ToDictionary(
                field => GetCustomFieldName(field) ?? field.Name,
                field => field.GetValue(obj)?.ToString() ?? "null"
                );
            return string.Join(", ", properties.Select(kv => $"{kv.Key}:{kv.Value}"));
        }
        public static void StringToObject(this object obj, string data)
        {
            var properties = obj.GetType().GetFields()
                .ToDictionary(field => GetCustomFieldName(field) ?? field.Name);

            var keyValuePairs = data.Split(',').Select(kv => kv.Trim().Split(':'));
            foreach (var pair in keyValuePairs)
            {
                var propertyName = pair[0];
                var propertyValue = pair.Length > 1 ? pair[1] : null;
                if (properties.TryGetValue(propertyName, out var fieldInfo))
                {
                    var fieldValue = Convert.ChangeType(propertyValue, fieldInfo.FieldType);
                    fieldInfo.SetValue(obj, fieldValue);
                }
            }
        }
        private static string GetCustomFieldName(FieldInfo fieldInfo)
        {
            var attribute = fieldInfo.GetCustomAttribute<CustomNameAttribute>();
            return attribute?.CustomFieldName;
        }
    }
    class MyClass
    {
        [CustomName("CustomFieldName")]
        public int I = 0;
    }
    class Program
    {
        static void Main()
        {
            var obj = new MyClass();
            var serialized = obj.ObjectToString();
            Console.WriteLine(serialized);

            var newObj = new MyClass();
            newObj.StringToObject(serialized);
            Console.WriteLine(newObj.I);
        }
    }
}