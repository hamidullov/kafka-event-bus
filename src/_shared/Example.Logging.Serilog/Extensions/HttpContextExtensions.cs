using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Example.Logging.Serilog.Extensions;

public static class HttpContextExtensions
{
    public static string GetClientName(this IHeaderDictionary headers)
    {
        return headers.FirstOrDefault(x => x.Key.ToUpper() == "X-CLIENT").Value.FirstOrDefault();
    }

    public static string GetClientName(this ClaimsPrincipal user, HttpRequest request)
    {
        return user.Claims.FirstOrDefault(x => x.Type == "client" && x.Value == request.Headers.GetClientName())?.Value;
    }
        
    public static string GetUserName(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value;
    }
        
    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
    }
       
}