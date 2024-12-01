using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Dto;

public class ProjectOverview
{
    public string ProjectId { get; init; }
    public string ProjectName { get; init; }
    public MemberType MemberType { get; init; }
}