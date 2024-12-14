using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class IssueResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required IssueType Type { get; set; }
    public required LaneView Lane { get; set; }
    public static IssueResponse FromIssue(Issue issue)
    {
        return new IssueResponse()
        {
            Id = issue.Id,
            Name = issue.Name,
            Description = issue.Description,
            Type = issue.Type,
            Lane = LaneView.FromLane(issue.Lane)
        };
    }
}