using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Persistence.Entities;

public class ProjectEntity
{
    [Required]
    public string Id { get; set; }
    [Required]
    [StringLength(Project.MaxNameLength, MinimumLength = 1)]
    public string Name { get; set; }
    public ICollection<IssueEntity> Issues { get; set; } = new List<IssueEntity>();
    public ICollection<LaneEntity> Lanes { get; set; } = new List<LaneEntity>();
    public ICollection<MemberEntity> Members { get; set; } = new List<MemberEntity>();

    public static Project ToProject(ProjectEntity entity)
    {
        var lanes = entity.Lanes.Select(LaneEntity.ToLane);
        var issues = entity.Issues
            .Select(i => IssueEntity.ToIssue(i, lanes.First(l => l.Id == i.LaneId)));
        var members = entity.Members.Select(MemberEntity.ToMember);
        return new Project(entity.Name, issues.ToList(), members.ToList(), lanes.ToList());
    }
    
    public static ProjectEntity FromProject(Project project)
    {
        return new ProjectEntity()
        {
            Id = project.Id,
            Name = project.Name,
            Issues = project.Issues.Select(IssueEntity.FromIssue).ToList(),
            Lanes = project.Lanes.Select(LaneEntity.FromLane).ToList(),
            Members = project.Members.Select(MemberEntity.FromMember).ToList(),
        };
    }
}