using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class IssuePartial
{
    public required int Key { get; set; }
    public required string Name { get; set; }
    public required IssueType Type { get; set; }
    public required Lane Lane { get; set; }
}
