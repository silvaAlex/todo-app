using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models;
using TodoApp.API.Data;

namespace TodoApp.API.Repositories
{
    public class UserRepository(TodoAppDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetUserByUserName(string userName)
        {
            User? entity = await _dbSet.FirstOrDefaultAsync(x => x.UserName == userName);
            return entity;
        }
    }
}
