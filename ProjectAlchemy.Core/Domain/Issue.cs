namespace ProjectAlchemy.Core.Domain;

public class Issue
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int Key { get; set; }
    // public IssueType Type { get; init; }
    
    public Issue(int id, string name, string? description = null)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    
    public Issue(string name, string? description = null)
    {
        Name = name;
        Description = description;
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