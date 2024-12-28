using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos;

public class IssuePatch
{
    [MaxLength(IssueService.MaxNameLength), MinLength(1)]
    public required string Name { get; init; }
    [MaxLength(IssueService.MaxDescriptionLength)]
    public required string Description { get; init; }
    public required IssueType Type { get; init; }
    public required Lane Lane { get; init; }
}