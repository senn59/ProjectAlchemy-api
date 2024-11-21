using System.ComponentModel.DataAnnotations;

namespace ProjectAlchemy.Core.Domain;

public class Issue
{
    public const int MaxNameLength = 30;
    public const int MaxDescriptionLength = 30;
    
    public int Id { get; private set; }
    [Required]
    [StringLength(MaxNameLength, MinimumLength = 1)]
    public string Name { get; private set; } = null!;
    [StringLength(MaxDescriptionLength)]
    public string Description { get; private set; } = "";
    [Required]
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
        Name = name;
    }
    
    public void SetDescription(string description)
    {
        description = description.Trim();
        Description = description;
    }

    public void SetType(IssueType type)
    {
        Type = type;
    }
}