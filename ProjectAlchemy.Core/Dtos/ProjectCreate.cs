using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos;

public class ProjectCreate
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(ProjectService.MaxNameLength)]
    public required string Name { get; init; }
}