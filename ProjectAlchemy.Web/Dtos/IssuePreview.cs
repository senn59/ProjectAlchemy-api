using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class IssuePreview
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public required int Key { get; set; }
    public required IssueType Type { get; set; }
    public static IssuePreview FromIssue(Issue issue)
    {
        return new IssuePreview()
        {
            Id = issue.Id,
            Name = issue.Name,
            Description = issue.Description,
            Key = issue.Key,
            Type = issue.Type
        };
    }
}