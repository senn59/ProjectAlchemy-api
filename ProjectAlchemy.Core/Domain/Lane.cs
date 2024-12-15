using ProjectAlchemy.Core.Helpers;

namespace ProjectAlchemy.Core.Domain;

public class Lane
{
    public const int MaxNameLength = 20;
    public int Id { get; }
    public string Name { get; private set; }

    public Lane(int id, string name)
    {
        Id = id;
        Name = name;
        SetName(name);
    }
    
    public Lane(string name)
    {
        Name = name;
        SetName(name);
    }

    private void SetName(string name)
    {
        name = name.Trim();
        Guard.AgainstNullOrEmpty(name, nameof(name));
        Guard.AgainstLength(name, nameof(name), MaxNameLength);
        Name = name;
    }
}