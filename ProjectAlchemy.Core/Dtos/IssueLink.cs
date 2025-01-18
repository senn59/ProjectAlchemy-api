using ProjectAlchemy.Core.Enums;

namespace ProjectAlchemy.Core.Dtos;

public class IssueLink
{
    public int Key { get; set; }
    public string Name { get; set; }
    public IssueType Type { get; set; }
}