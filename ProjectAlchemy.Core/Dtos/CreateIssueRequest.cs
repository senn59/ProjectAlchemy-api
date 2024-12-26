using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class CreateIssueRequest
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(Issue.MaxNameLength)]
    public required string Name { get; set; }
    public required IssueType Type { get; set; }
    public required int LaneId { get; set; }

    public static Issue ToIssue(CreateIssueRequest request, Lane lane)
    {
        return new Issue(request.Name, request.Type, lane);
    }
}