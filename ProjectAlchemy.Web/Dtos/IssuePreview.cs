using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class IssuePreview
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required IssueType Type { get; set; }

    public static IssuePreview FromIssue(Issue issue)
    {
        return new IssuePreview()
        {
            Id = issue.Id,
            Name = issue.Name,
            Type = issue.Type
        };
    }
}