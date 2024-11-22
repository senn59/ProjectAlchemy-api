using System.ComponentModel.DataAnnotations;
using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Persistence.Entities;

public class UserEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    public List<IssueEntity> Issues { get; set; }

    public static User ToUser(UserEntity userEntity)
    {
        return new User()
        {
            Id = userEntity.Id,
            Username = userEntity.Username
        };
    }
}