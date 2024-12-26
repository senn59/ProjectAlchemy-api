using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class IssuePatch
{
    [MaxLength(Issue.MaxNameLength), MinLength(1)]
    public required string Name { get; init; }
    [MaxLength(Issue.MaxDescriptionLength)]
    public required string Description { get; init; }
    public required IssueType Type { get; init; }
    public required Lane Lane { get; init; }
}