using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence.Repositories;

public class UserRepository: IUserRepository
{
    private AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Exists(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username) != null;
    }
    
    public async Task<User> Create(string username, string password)
    {
        var user = await _context.Users.AddAsync(new UserEntity()
        {
            Username = username,
            Password = password
        });
        return UserEntity.ToUser(user.Entity);
    }

    public Task<User?> MatchAgainstPassword(string username, string password)
    {
        throw new NotImplementedException();
    }
}