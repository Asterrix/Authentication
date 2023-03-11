using Application.Contracts;
using Application.Models;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthenticationDatabaseContext _context;

    public UserRepository(AuthenticationDatabaseContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> UserExist(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> UsernameAvailable(string username, CancellationToken cancellationToken)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        return result == null;
    }

    public string HashUserPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool ValidatePassword(string requestPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(requestPassword, hashedPassword);
    }
}