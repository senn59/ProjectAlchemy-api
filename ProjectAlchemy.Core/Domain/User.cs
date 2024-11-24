namespace ProjectAlchemy.Core.Domain;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public List<Issue> Issues { get; set; }
}