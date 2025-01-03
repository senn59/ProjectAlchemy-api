using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Persistence.Entities;

public class ProjectEntity
{
    [Required]
    [Key]
    public required Guid Id { get; init; }
    [Required]
    [StringLength(ProjectService.MaxNameLength, MinimumLength = 1)]
    public required string Name { get; init; }
    public ICollection<IssueEntity> Issues { get; init; } = new List<IssueEntity>();
    public ICollection<LaneEntity> Lanes { get; init; } = new List<LaneEntity>();
    public ICollection<MemberEntity> Members { get; init; } = new List<MemberEntity>();
    public ICollection<InvitationEntity> Invitations { get; init; } = new List<InvitationEntity>();

    public static Project ToProject(ProjectEntity entity)
    {
        var lanes = entity.Lanes.Select(LaneEntity.ToLane).ToList();
        var project = new Project
        {
            Id = entity.Id,
            Name = entity.Name,
            Lanes = lanes,
            Issues = entity.Issues.Select(i => IssueEntity.ToPartial(i, lanes.First(l => l.Id == i.LaneId))),
            Members = entity.Members.Select(MemberEntity.ToMember)
        };
        return project;
    }
    
    public static ProjectEntity FromProject(Project project)
    {
        return new ProjectEntity()
        {
            Id = project.Id,
            Name = project.Name,
            Issues = project.Issues.Select(IssueEntity.FromPartial).ToList(),
            Lanes = project.Lanes.Select(LaneEntity.FromLane).ToList(),
            Members = project.Members.Select(MemberEntity.FromMember).ToList()
        };
    }
}