 using Moq;
using TodoApp.Api.Models;
using TodoApp.API.DTOs;
using TodoApp.API.Notifications;
using TodoApp.API.Repositories;
using TodoApp.API.UseCases;

namespace TodoApp.API.Test.UseCases;

public class UserServiceTests
{
    private readonly Mock<ITokenService> _tokenService = new()
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly DomainNotifier _notifier = new();
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(_userRepoMock.Object, _notifier, _tokenService);
    }

    [Fact]
    public async Task RegisterAsync_ShouldAddNotification_WhenUserNameExists()
    {
        //Arrange
        _userRepoMock.Setup(r => r.GetUserByUserName("alice")).ReturnsAsync(new User());

        var userDto = new UserCreateDto()
        {
            UserName = "alice",
            Password = "123456789"
        };

        //Act
        var result = await _userService.RegisterAsync(userDto);

        //Assert
        Assert.Null(result);
        Assert.True(_notifier.HasNotifications);
        Assert.Contains(_notifier.Notifications, n => n.Key == "UserNameExists");
    }
}