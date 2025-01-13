using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace OrderService.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }

        public static T GetEnumFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null)!;
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null)!;
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
        }

        public static string GetHashAndName(this Enum value) => $"({value.GetHashCode()}) {value}";

        public static string GetHashAndNameAndDescription(this Enum value) => $"({value.GetHashCode()}) {value} -> {value.Description()}";

        public static Dictionary<string, string> GetNameAndDescription(this Enum value)
        {
            var list = new Dictionary<string, string>
            {
                { value.ToString(), value.Description() }
            };
            return list;
        }

        public static IList<KeyValuePair<T, string>> GetValuesFromEnumWithDescriptionAttribute<T>(this Type type, bool addKeyToValue = false)
        {
            var result = new List<KeyValuePair<T, string>>();

            foreach (FieldInfo field in type.GetFields().Where(x => x.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() != null))
            {
                var attribute = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault()!;
                if (attribute != null)
                {
                    if (typeof(T).Equals(typeof(string)))
                    {
                        if (addKeyToValue)
                            result.Add(new KeyValuePair<T, string>((T)(field.GetValue(field)!.ToString()! as object), $"{attribute.Description} ({(T)(field.GetValue(field)!.ToString()! as object)})"));
                        else
                            result.Add(new KeyValuePair<T, string>((T)(field.GetValue(field)!.ToString()! as object), $"{attribute.Description}"));
                    }

                    if (typeof(T).Equals(typeof(int)))
                    {
                        if (addKeyToValue)
                            result.Add(new KeyValuePair<T, string>((T)field.GetValue(field)!, $"{attribute.Description} ({(T)field.GetValue(field)!})"));
                        else
                            result.Add(new KeyValuePair<T, string>((T)field.GetValue(field)!, $"{attribute.Description}"));
                    }
                }
            }

            return result;
        }

        public static string GetValueStringFromEnumBased(this Type type, object code)
        {
            foreach (FieldInfo field in type.GetFields().Where(x => x.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() != null))
            {
                if (code.GetType().Equals(typeof(string)))
                {
                    if (field.GetValue(field)!.ToString() == (string)code)
                    {
                        var attribute = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault()!;

                        if (attribute != null)
                            return attribute.Description;

                        return string.Empty;
                    }
                }

                if (code.GetType().Equals(typeof(int)))
                {
                    if ((int)field.GetValue(field)! == (int)code)
                    {
                        var attribute = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault()!;

                        if (attribute != null)
                            return attribute.Description;

                        return string.Empty;
                    }
                }
            }

            return string.Empty;
        }

        public static T ToEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);
    }
}