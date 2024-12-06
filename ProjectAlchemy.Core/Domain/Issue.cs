using ProjectAlchemy.Core.Helpers;

namespace ProjectAlchemy.Core.Domain;

public class Issue
{
    public const int MaxNameLength = 30;
    public const int MaxDescriptionLength = 200;
    
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; } = "";
    public IssueType Type { get; private set; }
    public Lane Lane { get; private set; }
    
    public Issue(int id, string name, IssueType type, string description, Lane lane)
    {
        Id = id;
        Name = name;
        Lane = lane;
        SetName(name);
        SetDescription(description);
        SetType(type);
        SetLane(lane);
    }
    
    public Issue(string name, IssueType type, Lane lane)
    {
        Name = name;
        Lane = lane;
        SetName(name);
        SetType(type);
        SetLane(lane);
    }

    public void SetName(string name)
    {
        name = name.Trim();
        Guard.AgainstNullOrEmpty(name, nameof(name));
        Guard.AgainstLength(name, nameof(name), MaxNameLength);
        Name = name;
    }
    
    public void SetDescription(string description)
    {
        description = description.Trim();
        Guard.AgainstLength(description, nameof(description), MaxDescriptionLength);
        Description = description;
    }

    public void SetType(IssueType type)
    {
        Type = type;
    }

    public void SetLane(Lane lane)
    {
        Lane = lane;
    }
}