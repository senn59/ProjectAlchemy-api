using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Dtos.Issue;

public class CreateIssue
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(IssueService.MaxNameLength)]
    public required string Name { get; set; }
    public required IssueType Type { get; set; }
    public required int LaneId { get; set; }
}
