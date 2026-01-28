using TodoApp.API.Notifications;
using TodoApp.API.Repositories;
using TodoApp.API.UseCases;

namespace TodoApp.API.Configuration;
public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<DomainNotifier>();

        services.AddScoped<IUserRepository,  UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}
