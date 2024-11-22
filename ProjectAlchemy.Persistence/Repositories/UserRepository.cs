using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Persistence.Repositories;

public class UserRepository: IUserRepository
{
    private AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public User Register(string username, string password)
    {
        throw new NotImplementedException();
    }
}