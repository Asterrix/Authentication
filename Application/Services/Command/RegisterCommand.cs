using Application.Contracts;
using Application.Mapper;
using Application.Models;
using Application.ViewModels;
using FluentValidation;
using MediatR;
using System.Text;

namespace Application.Services.Command;

public record RegisterCommand(
    string Email,
    string Username,
    string Password,
    string Gender,
    DateTime DateBirth) : IRequest<UserViewModel>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _validator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RegisterCommandHandler(IUserRepository userRepository, IValidator<User> validator, IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _validator = validator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<UserViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        #region Validation

        var userExists = await _userRepository.UserExist(request.Email.Trim(), cancellationToken);
        if (userExists is not null)
        {
            throw new ArgumentException("Email address is already in use. Try to log in.");
        }

        var usernameIsAvailable = await _userRepository.UsernameAvailable(request.Username.Trim(), cancellationToken);
        if (!usernameIsAvailable)
        {
            throw new ArgumentException("Username is already in use. Try another one.");
        }

        var genderIsDefined = Enum.GetNames(typeof(Gender)).Any(x => x.ToLower() == request.Gender.ToLower().Trim());
        if (!genderIsDefined)
        {
            throw new ArgumentException("Select a valid gender.");
        }

        // Convert the gender input to match the Enum definition
        var normalizeGenderInput = request.Gender.ToLower().Trim();

        // Capitalize the first letter
        StringBuilder builder = new StringBuilder();
        builder.Append(normalizeGenderInput);
        builder[0] = char.ToUpper(normalizeGenderInput[0]);

        var entity = new User
        {
            Username = request.Username.Trim(),
            Email = request.Email.Trim(),
            Password = _userRepository.HashUserPassword(request.Password.Trim()),
            DateBirth = request.DateBirth,
            Gender = (Gender)Enum.Parse(typeof(Gender), builder.ToString()),
            Role = Role.ContentCreator,
            Active = Active.Offline,
            TimeRegistration = _dateTimeProvider.UtcNow,
            LastVisit = DateTime.UnixEpoch
        };


        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        #endregion

        var createdUser = await _userRepository.CreateUserAsync(entity, cancellationToken);

        ModelMapper.MapToUserViewModel(ref createdUser, out UserViewModel model);

        return model;
    }
}