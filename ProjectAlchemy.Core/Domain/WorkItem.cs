namespace ProjectAlchemy.Core.Domain;

public class WorkItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    // public int Key { get; set; }
    // public WorkItemType Type { get; init; }
    public WorkItem(string name, string description)
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