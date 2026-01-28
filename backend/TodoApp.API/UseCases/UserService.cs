using TodoApp.Api.Models;
using TodoApp.API.DTOs;
using TodoApp.API.Notifications;
using TodoApp.API.Repositories;

namespace TodoApp.API.UseCases
{
    public class UserService(IUserRepository repository, DomainNotifier notifier) : IUserService
    {
        public IReadOnlyCollection<Notification> Notifications => notifier.Notifications;

        public async Task<UserReadDto?> LoginAsync(UserLoginDto userDto)
        {
            User? user = await repository.GetUserByUserName(userDto.UserName);

            if (user == null || user.PasswordHash != userDto.Password)
            {
                notifier.AddNotification(new Notification("InvalidCrediatials", $"UserName ou Password estão incorretos"));
                return null;
            }

            if(!string.IsNullOrEmpty(user.UserName))
                return new UserReadDto(user.Id, user.UserName, user.CreatedAt);

            return null;
        }

        public async Task<UserReadDto?> RegisterAsync(UserCreateDto userDto)
        {
            User? userExists = await repository.GetUserByUserName(userDto.UserName);


            if (userExists != null)
            {
                notifier.AddNotification(new Notification("UserNameExsit", $"Já existe um usuário com esse {userDto.UserName}"));
                return null;
            }


            var user = new User()
            {
                UserName = userDto.UserName,
                PasswordHash = userDto.Password
            };

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();

            return new UserReadDto(user.Id, user.UserName, user.CreatedAt);
        }
    }
}
