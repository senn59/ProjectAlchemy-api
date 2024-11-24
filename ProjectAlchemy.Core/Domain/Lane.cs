using System.ComponentModel.DataAnnotations;

namespace ProjectAlchemy.Core.Domain;

public class Lane
{
    public const int MaxLength = 10;
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    [StringLength(MaxLength)]
    public string Name { get; set; }

    public Lane(int id, string name)
    {
        Id = id;
        Name = name.Trim();
    }
    
    public Lane(string name)
    {
        Name = name.Trim();
    }
}