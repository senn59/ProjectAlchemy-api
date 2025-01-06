using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos;

public class Project
{
    public required Guid Id { get; init; }
    [Required(AllowEmptyStrings = false)]
    [MaxLength(ProjectService.MaxNameLength)]
    public required string Name { get; set; }
    public required IEnumerable<IssuePartial> Issues { get; init; }
    public required IEnumerable<Lane> Lanes { get; init; }
    public required IEnumerable<Member> Members { get; init; }
}