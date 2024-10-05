using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class CreateWorkItemRequest
{
    public string Name { get; set; }

    public static WorkItem ToWorkItem(CreateWorkItemRequest request)
    {
        return new WorkItem(request.Name, "");
    }
}