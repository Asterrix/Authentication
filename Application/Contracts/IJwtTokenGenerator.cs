using Application.Models;

namespace Application.Contracts;
public interface IJwtTokenGenerator
{
    string GenerateJwtToken(User user);
}
