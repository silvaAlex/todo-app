using TodoApp.Api.Models;
using TodoApp.API.DTOs;
using TodoApp.API.Notifications;
using TodoApp.API.Repositories;
using TodoApp.API.Services;

namespace TodoApp.API.UseCases
{
    public class UserService(IUserRepository repository, DomainNotifier notifier, ITokenService tokenService) : IUserService
    {
        public IReadOnlyCollection<Notification> Notifications => notifier.Notifications;

        public async Task<TokenUserDto?> LoginAsync(UserLoginDto userDto)
        {
            User? user = await repository.GetUserByUserName(userDto.UserName);

            if (user == null || !string.IsNullOrEmpty(user.PasswordHash) && !PasswordHasher.VerifyPassword(userDto.Password, user.PasswordHash))
            {
                notifier.AddNotification(new Notification("InvalidCrediatials", $"UserName ou Password estão incorretos"));
                return null;
            }

            if (!string.IsNullOrEmpty(user.UserName))
            {
                var token = tokenService.GenerateToken(user);

                return new TokenUserDto(token, new UserReadDto(user.Id, user.UserName, user.CreatedAt));
            }

            return null;
        }

        public async Task<UserReadDto?> RegisterAsync(UserCreateDto userDto)
        {
            User? userExists = await repository.GetUserByUserName(userDto.UserName);


            if (userExists != null)
            {
                notifier.AddNotification(new Notification("UserNameExists", $"Já existe um usuário com esse {userDto.UserName}"));
                return null;
            }


            var user = new User()
            {
                UserName = userDto.UserName,
                PasswordHash = PasswordHasher.HashPassword(userDto.Password)
            };

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();

            return new UserReadDto(user.Id, user.UserName, user.CreatedAt);
        }
    }
}
