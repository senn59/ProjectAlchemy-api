namespace ProjectAlchemy.Core.Dtos;

public class UpdateWorkItemRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}