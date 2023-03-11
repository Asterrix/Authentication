using Application.Models;
using Application.Validators;

namespace Application.Tests.Validators;

public class UserValidatorTests
{
    private readonly UserValidator _validator;

    public UserValidatorTests()
    {
        _validator = new UserValidator();
    }

    #region Username

    [Fact]
    protected async Task UsernameShouldFailEmptyString()
    {
        #region Arrange

        var model = new User
        {
            Username = "",
            Email = "test@email.com",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);

        #endregion
    }

    [Fact]
    protected async Task UsernameMinimumLengthShouldLogErrorMessage()
    {
        #region Arrange

        var model = new User
        {
            Username = "ABC",
            Email = "test@email.com",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        Assert.Equal(
            $"The length of 'Username' must be at least 4 characters. You entered {model.Username.Length} characters.",
            result.Errors.First().ErrorMessage
        );

        #endregion
    }

    [Fact]
    protected async Task UsernameMaximumLengthShouldLogErrorMessage()
    {
        #region Arrange

        var model = new User
        {
            Username = "kdaspkdsapdspqewqexzZdas",
            Email = "test@email.com",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        Assert.Equal(
            $"The length of 'Username' must be 15 characters or fewer. You entered {model.Username.Length} characters.",
            result.Errors.First().ErrorMessage
        );

        #endregion
    }

    [Fact]
    protected async Task UsernameInvalidCharactersShouldLogErrorMessage()
    {
        #region Arrange

        var model = new User
        {
            Username = "tyt(ye%qwe#",
            Email = "test@email.com",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        Assert.Equal("Username cannot contain special characters.", result.Errors.First().ErrorMessage);

        #endregion
    }

    [Fact]
    protected async Task UsernameContainsMoreNumbersThanLettersShouldLogErrorMessage()
    {
        #region Arrange

        var model = new User
        {
            Username = "abc1234",
            Email = "test@email.com",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        Assert.Equal(
            "Username must contain more alphabetical letters than numerical characters.",
            result.Errors.First().ErrorMessage
        );

        #endregion
    }

    [Fact]
    protected async Task UsernameConsistsOfLettersAndNumbersShouldPass()
    {
        #region Arrange

        var model = new User
        {
            Username = "Abc1d2x",
            Email = "test@email.com",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.True(result.IsValid);

        #endregion
    }

    #endregion

    #region Email

    [Fact]
    protected async Task EmailEmptyStringShouldLogMessage()
    {
        #region Arrange

        var model = new User
        {
            Username = "Abc1d2x",
            Email = "",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        Assert.Equal("Email field cannot be empty", result.Errors.First().ErrorMessage);

        #endregion
    }

    [Fact]
    protected async Task EmailMinimumLengthShouldLogMessage()
    {
        #region Arrange

        var model = new User
        {
            Username = "Abc1d2x",
            Email = "test@g.cm",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        Assert.Equal(
            $"The length of 'Email' must be at least 12 characters. You entered {model.Email.Length} characters.",
            result.Errors.First().ErrorMessage
        );

        #endregion
    }

    [Fact]
    protected async Task EmailMaximumLengthShouldLogMessage()
    {
        #region Assert

        var model = new User
        {
            Username = "Abc1d2x",
            Email = "test@xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.com",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        Assert.Equal(
            $"The length of 'Email' must be 32 characters or fewer. You entered {model.Email.Length} characters.",
            result.Errors.First().ErrorMessage
        );

        #endregion
    }

    [Fact]
    protected async Task EmailValidEmailShouldPass()
    {
        #region Assert

        var model = new User
        {
            Username = "Abc1d2x",
            Email = "test@email.com",
            Password = "",
            DateBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            Role = Role.Admin,
            Active = Active.Online,
            TimeRegistration = DateTime.UnixEpoch,
            LastVisit = DateTime.UnixEpoch
        };

        #endregion

        #region Act

        var result = await _validator.ValidateAsync(model);

        #endregion

        #region Assert

        Assert.True(result.IsValid);

        #endregion
    }

    #endregion
}