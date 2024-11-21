using System.ComponentModel.DataAnnotations;

namespace ProjectAlchemy.Persistence.Entities;

public class UserEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}