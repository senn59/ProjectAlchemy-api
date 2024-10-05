namespace ProjectAlchemy.Web.Dtos;

public class UpdateWorkItemRequest
{
    public required string Name { get; set; }
    public required string? Description { get; set; }
}