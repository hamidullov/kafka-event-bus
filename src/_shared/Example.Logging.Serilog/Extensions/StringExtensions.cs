using System.Text.RegularExpressions;

namespace Example.Logging.Serilog.Extensions;

static class StringExtensions
{
    public static string SanitizingLog(this string value, int length = 8)
    {
        if (string.IsNullOrEmpty(value))
            return value;           
        return Regex.Replace(value, "\"Password\":\"(.*?)\",", "\"Password\":\"" + new string('*',length) + "\",", RegexOptions.Compiled);
    }
        
    public static string SanitizingXmlLog(this string value, int length = 8)
    {
        if (string.IsNullOrEmpty(value))
            return value;           
        return Regex.Replace(value, "\"password\">(.*?)</", "\"password\">" + new string('*',length) + "</", RegexOptions.Compiled);
    }
        
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength); 
    }
}