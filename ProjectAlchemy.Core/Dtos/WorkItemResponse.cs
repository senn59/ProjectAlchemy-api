using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Dtos;

public class WorkItemResponse
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    // public required string Key { get; set; }
    public static WorkItemResponse FromWorkItem(WorkItem item)
    {
        return new WorkItemResponse()
        {
            Name = item.Name,
            Description = item.Description,
        };
    }
}