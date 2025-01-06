using System.Security.Claims;

namespace ProjectAlchemy.Web.Utilities;

public static class JwtHelper
{
    public static Guid GetId(ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id), "user id not found");
        }

        return new Guid(id);
    }
    public static string GetEmail(ClaimsPrincipal user)
    {
        var email = user.FindFirst(ClaimTypes.Email)?.Value;
        return email ?? throw new ArgumentNullException(nameof(email), "user id not found");
    }
}