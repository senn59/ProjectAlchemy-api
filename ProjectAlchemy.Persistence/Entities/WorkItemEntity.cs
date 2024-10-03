using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

[Table("WorkItems")]
public class WorkItemEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }

    public static WorkItem ToWorkItem(WorkItemEntity entity)
    {
        return new WorkItem(entity.Name, entity.Description);
    }

    public static WorkItemEntity FromWorkitem(WorkItem item)
    {
        return new WorkItemEntity()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
        };
    }
}