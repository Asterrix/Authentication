using Application.Contracts;
using Moq;

namespace Application.Tests;

public class HashUserPasswordTests
{
    private readonly Mock<IUserRepository> _mock;

    public HashUserPasswordTests()
    {
        _mock = new Mock<IUserRepository>();
    }

    [Fact]
    public void HashUserPasswordTestShouldPass()
    {
        #region Arrange

        const string password = "MyPassword123";
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        _mock.Setup(x => x.HashUserPassword(password)).Returns(hashedPassword);

        #endregion

        #region Act

        var hash = _mock.Object.HashUserPassword(password);

        #endregion


        #region Assert

        Assert.NotNull(hash);
        Assert.NotEmpty(hash);
        Assert.Equal(hashedPassword, hash);

        #endregion
    }
}