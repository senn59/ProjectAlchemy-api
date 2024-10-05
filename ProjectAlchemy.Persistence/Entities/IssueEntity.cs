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
    [MaxLength(30)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string? Description { get; set; }

    public static Issue ToIssue(IssueEntity entity)
    {
        return new Issue(entity.Id, entity.Name, entity.Description);
    }

    public static IssueEntity FromIssue(Issue item)
    {
        return new IssueEntity()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
        };
    }
}