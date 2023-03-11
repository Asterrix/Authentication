using Application.Contracts;
using Application.Mapper;
using Application.Models;
using Application.ViewModels;
using MediatR;

namespace Application.Services.Query;

public record LoginQuery(string Email, string Password) : IRequest<AuthenticationResult>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.UserExist(request.Email.Trim(), cancellationToken);
        if (user == null)
        {
            throw new ArgumentException("Check your credentials for potential mistakes and try again.");
        }

        bool passwordIsValid = _userRepository.ValidatePassword(request.Password, user.Password);
        if (!passwordIsValid)
        {
            throw new ArgumentException("Check your credentials for potential mistakes and try again.");
        }

        string token = _jwtTokenGenerator.GenerateJwtToken(user);

        ModelMapper.MapToUserViewModel(ref user, out var model);

        return new AuthenticationResult(model, token);
    }
}