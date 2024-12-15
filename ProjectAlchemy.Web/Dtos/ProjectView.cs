using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class ProjectView
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required IEnumerable<LaneView> Lanes { get; init; }
    public required IEnumerable<PartialIssue> Issues { get; init; }
    
    public static ProjectView FromProject(Project p)
    {
        return new ProjectView()
        {
            Id = p.Id,
            Name = p.Name,
            Lanes = p.Lanes.Select(LaneView.FromLane),
            Issues = p.Issues.Select(PartialIssue.FromIssue),
        };
    }
}