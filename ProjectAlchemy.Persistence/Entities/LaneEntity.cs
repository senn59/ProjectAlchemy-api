using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

public class LaneEntity
{
    [Required]
    public int Id { get; set; }
    [MaxLength(10)]
    public string Name { get; set; }
    public int ProjectId { get; set; }
    
    public ProjectEntity Project { get; set; }
}