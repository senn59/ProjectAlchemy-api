using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

[Table("Issues")]
public class IssueEntity
{
    [Required]
    public required int Id { get; init; }
    [Required]
    [StringLength(Issue.MaxNameLength, MinimumLength = 1)]
    public required string Name { get; init; }
    [Required]
    [MaxLength(Issue.MaxDescriptionLength)]
    public required string Description { get; init; } = "";
    public IssueType Type { get; init; }
    public int LaneId { get; init; }
    
    [MaxLength(200)]
    public string? ProjectId { get; set; }
    public ProjectEntity? Project { get; init; }

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