using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class ProjectView
{
    public string Id { get; init; }
    public string Name { get; init; }
    public IEnumerable<LaneView> Lanes { get; init; }
    public IEnumerable<PartialIssue> Issues { get; init; }
    
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