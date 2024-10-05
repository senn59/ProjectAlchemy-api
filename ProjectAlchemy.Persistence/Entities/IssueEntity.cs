using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

[Table("Issues")]
public class IssueEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(Issue.MaxNameLength)]
    public string Name { get; set; }
    [MaxLength(Issue.MaxDescriptionLength)]
    public string? Description { get; set; }
    [Required]
    public int Key { get; set; }
    public IssueType Type { get; set; }

    public static Issue ToIssue(IssueEntity entity)
    {
        return new Issue(entity.Id, entity.Name, entity.Type, entity.Key, entity.Description);
    }

    public static IssueEntity FromIssue(Issue issue)
    {
        return new IssueEntity()
        {
            Id = issue.Id,
            Name = issue.Name,
            Description = issue.Description,
            Type = issue.Type
        };
    }
}