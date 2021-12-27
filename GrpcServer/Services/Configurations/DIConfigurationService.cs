using GrpcServer.Database;
using GrpcServer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using TestGrpc.Contracts;
using TestGrpc.Models;

namespace GrpcServer.Services.Configurations
{
    public static class DIConfigurationService
    {
        public static IServiceCollection DIConfiguration(this IServiceCollection services)
        {
            //This helps to keep the startup.cs file clean.
            services.AddTransient<IBaseService<Todo>, TodoService>();
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<IBaseRepository<Todo>, TodoRepository>();

            return services;
        }
    }
}