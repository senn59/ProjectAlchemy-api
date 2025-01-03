using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Interfaces;

public interface IAuthorizationService
{
    Task Authorize(Permission permission, Guid userId, Guid projectId);
    Task Authorize(Permission permission, Guid userId, Guid projectId, int issueKey);
}