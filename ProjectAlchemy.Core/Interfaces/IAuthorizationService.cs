namespace ProjectAlchemy.Core.Interfaces;

public interface IAuthorizationService
{
    Task AuthorizeProjectAccess(string userId, string projectId);
    Task AuthorizeIssueDeletion(string userId, string projectId, int issueId);
    Task AuthorizeIssueAccess(string userId, string projectId, int issueId);
    Task AuthorizeIssueUpdate(string userId, string projectId, int issueId);
}