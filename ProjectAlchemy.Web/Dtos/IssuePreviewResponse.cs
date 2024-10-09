using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class IssuePreviewResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int Key { get; set; }
    public required IssueType Type { get; set; }

    public static IssuePreviewResponse FromIssue(Issue issue)
    {
        return new IssuePreviewResponse()
        {
            Id = issue.Id,
            Name = issue.Name,
            Key = issue.Key,
            Type = issue.Type
        };
    }
}