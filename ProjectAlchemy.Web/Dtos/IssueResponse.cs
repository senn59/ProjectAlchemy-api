using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class IssueResponse
{
    public required string Name { get; set; }
    public required string? Description { get; set; }
    // public required string Key { get; set; }
    public static IssueResponse FromIssue(Issue item)
    {
        return new IssueResponse()
        {
            Name = item.Name,
            Description = item.Description,
        };
    }
}