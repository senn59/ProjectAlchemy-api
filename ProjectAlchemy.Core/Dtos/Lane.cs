using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos;

public class Lane
{
    public Guid Id { get; init; }
    [Required(AllowEmptyStrings = false)]
    [StringLength(LaneService.MaxNameLength)]
    public required string Name { get; init; }
}