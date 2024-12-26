using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos;

public class Project
{
    public required string Id { get; init; }
    [Required(AllowEmptyStrings = false)]
    [MaxLength(ProjectService.MaxNameLength)]
    public required string Name { get; set; }
    public required IReadOnlyList<IssuePartial> Issues { get; init; }
    public required IReadOnlyList<Lane> Lanes { get; init; }
    public required IReadOnlyList<Member> Members { get; init; }
}