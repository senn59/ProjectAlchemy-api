namespace ProjectAlchemy.Core.Domain;

public class Issue
{
    public const int MaxNameLength = 30;
    public const int MaxDescriptionLength = 30;
    
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int Key { get; set; }
    public IssueType Type { get; init; }
    
    public Issue(int id, string name, IssueType type, int key, string? description = null)
    {
        Id = id;
        Name = name;
        Description = description;
        Key = key;
        Type = type;
    }
    
    public Issue(int id, string name, IssueType type, string? description = null)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
    }
    
    public Issue(string name, IssueType type, string? description = null)
    {
        Name = name;
        Description = description;
        Type = type;
    }

    public void SetName(string name)
    {
        Name = name;
    }
    
    public void SetDescription(string description)
    {
        Description = description;
    }
}