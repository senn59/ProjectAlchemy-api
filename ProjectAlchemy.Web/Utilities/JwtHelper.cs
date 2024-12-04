using System.Security.Claims;

namespace ProjectAlchemy.Web.Utilities;

public class JwtHelper
{
    public static string GetId(ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return id ?? throw new ArgumentNullException(nameof(id), "user id not found");
    }
}