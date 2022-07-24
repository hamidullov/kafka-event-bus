using System.Text.RegularExpressions;

namespace Example.EventBus.Kafka.Extensions;

public static class StringExtensions
{
    public static string ToKebabCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;                       
        return Regex.Replace(value, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", "-$1", RegexOptions.Compiled)
            .Trim()
            .ToLower();
    }
    
    public static string SanitizingLog(this string value, int length = 8)
    {
        if (string.IsNullOrEmpty(value))
            return value;           
        return Regex.Replace(value, "\"Password\":\"(.*?)\",", "\"Password\":\"" + new string('*',length) + "\",", RegexOptions.Compiled);
    }
}