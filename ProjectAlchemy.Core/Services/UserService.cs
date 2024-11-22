using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class UserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
}