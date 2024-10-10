using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class UpdateIssueRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required IssueType Type { get; set; }
}