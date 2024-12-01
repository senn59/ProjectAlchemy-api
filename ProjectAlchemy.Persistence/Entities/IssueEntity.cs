using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

[Table("Issues")]
public class IssueEntity
{
    [Required]
    public int Id { get; set; }
    [MaxLength(Issue.MaxNameLength)]
    public required string Name { get; set; }
    [MaxLength(Issue.MaxDescriptionLength)]
    public required string Description { get; set; } = "";
    public IssueType Type { get; set; }
    public string ProjectId { get; set; }
    public int LaneId { get; set; }
    
    public ProjectEntity Project { get; set; }

    public static Issue ToIssue(IssueEntity entity, Lane lane)
    {
        return new Issue(entity.Id, entity.Name, entity.Type, entity.Description, lane);
    }

    public static IssueEntity FromIssue(Issue issue)
    {
        return new IssueEntity()
        {
            Id = issue.Id,
            Name = issue.Name,
            Description = issue.Description,
            Type = issue.Type,
            LaneId = issue.Lane.Id
        };
    }
}