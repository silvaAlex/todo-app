using TodoApp.Api.Models;

namespace TodoApp.API.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
