using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class CreateProjectRequest
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(Project.MaxNameLength)]
    public required string Name { get; init; }
}