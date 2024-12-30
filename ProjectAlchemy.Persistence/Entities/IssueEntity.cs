using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Persistence.Entities;

[Table("Issues")]
public class IssueEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int Key { get; set; }
    [Required]
    [StringLength(IssueService.MaxNameLength, MinimumLength = 1)]
    public required string Name { get; init; }
    [Required]
    [MaxLength(IssueService.MaxDescriptionLength)]
    public string Description { get; init; } = "";
    [Required]
    public required IssueType Type { get; init; }
    [Required]
    public bool Deleted { get; set; }
        
    public int LaneId { get; init; }
    [MaxLength(36)]
    public string? ProjectId { get; set; }
    public ProjectEntity? Project { get; init; }

    public static Issue ToIssue(IssueEntity entity, Lane lane)
    {
        return new Issue
        {
            Key = entity.Key,
            Description = entity.Description,
            Lane = lane,
            Name = entity.Name,
            Type = entity.Type
        };
    }

    public static IssueEntity FromIssue(Issue issue)
    {
        return new IssueEntity
        {
            Key = issue.Key,
            Name = issue.Name,
            Description = issue.Description,
            Type = issue.Type,
            LaneId = issue.Lane.Id
        };
    }
    
    public static IssueEntity FromIssueCreate(IssueCreate issue)
    {
        return new IssueEntity
        {
            Name = issue.Name,
            Type = issue.Type,
            LaneId = issue.LaneId
        };
    }
    
    public static IssuePartial ToPartial(IssueEntity entity, Lane lane)
    {
        return new IssuePartial
        {
            Key = entity.Key,
            Lane = lane,
            Name = entity.Name,
            Type = entity.Type
        };
    }
    
    public static IssueEntity FromPartial(IssuePartial partial)
    {
        return new IssueEntity
        {
            Key = partial.Key,
            Name = partial.Name,
            LaneId = partial.Lane.Id,
            Type = partial.Type
        };
    }
}