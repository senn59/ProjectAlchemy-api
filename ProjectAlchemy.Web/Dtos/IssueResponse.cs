using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class IssueResponse
{
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public required int Key { get; set; }
    public required IssueType Type { get; set; }
    public static IssueResponse FromIssue(Issue issue)
    {
        return new IssueResponse()
        {
            Name = issue.Name,
            Description = issue.Description,
            Key = issue.Key,
            Type = issue.Type
        };
    }
}