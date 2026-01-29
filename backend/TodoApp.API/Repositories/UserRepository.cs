using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models;
using TodoApp.API.Data;
using TodoApp.API.Notifications;

namespace TodoApp.API.Repositories
{
    public class UserRepository(TodoAppDbContext context, DomainNotifier notifier) : BaseRepository<User>(context, notifier), IUserRepository
    {
        private readonly DomainNotifier _notifier = notifier;

        public async Task<User?> GetUserByUserName(string userName)
        {
            User? entity = await _dbSet.FirstOrDefaultAsync(x => x.UserName == userName);
            return entity;
        }
    }
}
