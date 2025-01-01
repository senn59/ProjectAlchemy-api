using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class Invitation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectName { get; set; }
}