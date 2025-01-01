using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Interfaces;

public interface IAuthorizationService
{
    Task Authorize(Permission permission, string userId, string projectId);
    Task Authorize(Permission permission, string userId, string projectId, int issueKey);
}