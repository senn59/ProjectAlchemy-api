using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Persistence.Entities;

public class LaneEntity
{
    [Required]
    [MaxLength(36)]
    public required string Id { get; init; }
    [MaxLength(LaneService.MaxNameLength)]
    public required string Name { get; init; }
    
    [MaxLength(200)]
    public string? ProjectId { get; init; }
    public ProjectEntity? Project { get; init; }

    public static Lane ToLane(LaneEntity entity)
    {
        return new Lane
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public static LaneEntity FromLane(Lane lane)
    {
        return new LaneEntity
        {
            Id = lane.Id,
            Name = lane.Name
        };
    }
}