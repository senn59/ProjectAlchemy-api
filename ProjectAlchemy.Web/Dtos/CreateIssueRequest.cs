using System.Text.Json.Serialization;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class CreateIssueRequest
{
    public required string Name { get; set; }
    public required IssueType Type { get; set; }
    public required int laneId { get; set; }

    public static Issue ToIssue(CreateIssueRequest request, Lane lane)
    {
        return new Issue(request.Name, request.Type, lane);
    }
}