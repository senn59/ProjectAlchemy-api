using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class CreateIssueRequest
{
    public required string Name { get; set; }

    public static Issue ToIssue(CreateIssueRequest request)
    {
        return new Issue(request.Name, "");
    }
}