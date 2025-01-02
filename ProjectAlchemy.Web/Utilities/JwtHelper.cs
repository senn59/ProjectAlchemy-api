using System.Security.Claims;

namespace ProjectAlchemy.Web.Utilities;

public static class JwtHelper
{
    public static string GetId(ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return id ?? throw new ArgumentNullException(nameof(id), "user id not found");
    }
    public static string GetEmail(ClaimsPrincipal user)
    {
        var email = user.FindFirst(ClaimTypes.Email)?.Value;
        return email ?? throw new ArgumentNullException(nameof(email), "user id not found");
    }
}