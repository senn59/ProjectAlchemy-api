using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Interfaces;

public interface IUserRepository
{
    public User Register(string username, string password);
}