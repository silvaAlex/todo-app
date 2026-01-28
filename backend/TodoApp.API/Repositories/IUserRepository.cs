using TodoApp.Api.Models;

namespace TodoApp.API.Repositories;

public interface IUserRepository: IRepository<User>
{
    Task<User?> GetUserByUserName(string userName);
}
