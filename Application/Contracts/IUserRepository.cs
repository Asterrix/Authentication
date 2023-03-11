using Application.Models;

namespace Application.Contracts;
public interface IUserRepository
{
    Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<User?> UserExist(string email, CancellationToken cancellationToken);
    Task<bool> UsernameAvailable(string username, CancellationToken cancellationToken);
    string HashUserPassword(string password);
    bool ValidatePassword(string requestPassword, string hashedPassword);
}
