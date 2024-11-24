using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

public class ProjectEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(Project.MaxNameLength, MinimumLength = 1)]
    public string Name { get; set; }
    public ICollection<IssueEntity> Issues { get; set; } = new List<IssueEntity>();
    public ICollection<LaneEntity> Lanes { get; set; } = new List<LaneEntity>();
    public ICollection<MemberEntity> Members { get; set; } = new List<MemberEntity>();
}