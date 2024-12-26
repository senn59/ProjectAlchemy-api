using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos.Issue;

public class PartialIssue
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required IssueType Type { get; set; }
    public required LaneView Lane { get; set; }
}
