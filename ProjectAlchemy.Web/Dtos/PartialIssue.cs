using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class PartialIssue
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required IssueType Type { get; set; }
    public required string Lane { get; set; }

    public static PartialIssue FromIssue(Issue issue)
    {
        return new PartialIssue()
        {
            Id = issue.Id,
            Name = issue.Name,
            Type = issue.Type,
            Lane = issue.Lane.Name
        };
    }
}