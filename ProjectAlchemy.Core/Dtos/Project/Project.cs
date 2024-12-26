using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dtos.Issue;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos.Project;

public class Project
{
    public required string Id { get; init; }
    [Required(AllowEmptyStrings = false)]
    [MaxLength(ProjectService.MaxNameLength)]
    public required string Name { get; set; }
    public required IReadOnlyList<PartialIssue> Issues { get; init; }
    public required IReadOnlyList<Lane> Lanes { get; init; }
    public required IReadOnlyList<Member> Members { get; init; }
}