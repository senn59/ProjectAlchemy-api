using ProjectAlchemy.Core.Helpers;

namespace ProjectAlchemy.Core.Domain;

public class Issue
{
    public const int MaxNameLength = 30;
    public const int MaxDescriptionLength = 30;
    
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = "";
    public IssueType Type { get; private set; }
    
    public Issue(int id, string name, IssueType type, string description)
    {
        Id = id;
        SetName(name);
        SetDescription(description);
        SetType(type);
    }
    
    public Issue(string name, IssueType type)
    {
        SetName(name);
        SetType(type);
    }

    public void SetName(string name)
    {
        name = name.Trim();
        Guard.StringAgainstNullOrEmpty(name, nameof(name));
        Guard.StringAgainstLength(name, nameof(name), MaxNameLength);
        Name = name;
    }
    
    public void SetDescription(string description)
    {
        description = description.Trim();
        Guard.StringAgainstLength(description, nameof(description), MaxNameLength);
        Description = description;
    }

    public void SetType(IssueType type)
    {
        Type = type;
    }
}