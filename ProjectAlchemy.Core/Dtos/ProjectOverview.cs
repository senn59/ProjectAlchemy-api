using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class ProjectOverview
{
    public required Guid ProjectId { get; init; }
    public required string ProjectName { get; init; }
    public required MemberType MemberType { get; init; }
}