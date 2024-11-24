using System.ComponentModel.DataAnnotations;

namespace ProjectAlchemy.Core.Domain;

public class Project
{
    public const int MaxNameLength = 30;
    
    [Required]
    [StringLength(MaxNameLength, MinimumLength = 1)]
    public string Name { get; set; }
    public List<Issue> Issues { get; set; }
    public List<string> Lanes { get; set; } = ["To do", "In progress", "Done"];

    public Project()
    {
        
    }
}