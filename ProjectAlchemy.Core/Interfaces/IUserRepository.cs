using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Core.Interfaces;

public interface IUserRepository
{
    public Task<User> Create(string username, string password);
    public Task<bool> Exists(string username);
    public Task<User?> MatchAgainstPassword(string username, string password);
}