using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos;

public class Lane
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    [Required(AllowEmptyStrings = false)]
    [StringLength(LaneService.MaxNameLength)]
    public required string Name { get; init; }
}