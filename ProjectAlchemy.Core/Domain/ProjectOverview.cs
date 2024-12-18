using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Domain;

public class ProjectOverview
{
    public required string ProjectId { get; init; }
    public required string ProjectName { get; init; }
    public required MemberType MemberType { get; init; }
}