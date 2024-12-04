using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

public class LaneEntity
{
    [Required]
    public required int Id { get; init; }
    [MaxLength(10)]
    public required string Name { get; init; }
    
    [MaxLength(200)]
    public string? ProjectId { get; init; }
    public ProjectEntity? Project { get; init; }

    public static Lane ToLane(LaneEntity entity)
    {
        return new Lane(entity.Id, entity.Name);
    }

    public static LaneEntity FromLane(Lane lane)
    {
        return new LaneEntity()
        {
            Id = lane.Id,
            Name = lane.Name
        };
    }
}