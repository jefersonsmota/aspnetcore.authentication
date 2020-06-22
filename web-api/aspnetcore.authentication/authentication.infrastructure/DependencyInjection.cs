using authentication.infrastructure.Context;
using authentication.infrastructure.Interfaces;
using authentication.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace authentication.infrastructure
{
    /// <summary>
    /// Injetor de dependências da camada de infraestrutura.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddDbContext<AuthDbContext>(opt => opt.UseInMemoryDatabase("AuthDatabase"));
            services.AddScoped<AuthDbContext, AuthDbContext>();
            services.AddScoped<IUserRespository, UserRepository>();

            return services;
        }
    }
}
