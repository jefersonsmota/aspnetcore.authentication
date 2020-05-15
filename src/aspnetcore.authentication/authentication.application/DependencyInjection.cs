using authentication.application.Handlers.Interfaces;
using authentication.application.Handlers.Services;
using authentication.infrastructure;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using authentication.application.Common.Mappers;

namespace authentication.application
{
    /// <summary>
    /// Injetor de dependências da camada de aplicação
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Injeta interfaces da camanda de aplicação.
        /// Carregar injetor da camada de infraestrutura
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Service Collection</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.ConfigureMappers();

            // Infra e Repositorios
            services.AddRepository();

            // Handlers de validação da camada de aplicação
            services.AddScoped<IUserValidationHandler, UserValidationHandler>();

            // Handlers de commands e querys da camada de aplicação
            services.AddScoped<IUserCommandHandler, UserCommandHandler>();
            services.AddScoped<IUserQueryHandler, UserQueryHandler>();

            return services;
        }

        /// <summary>
        /// Injeta configurações de mapeamento de objetos.
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <returns>Service Collection</returns>
        private static IServiceCollection ConfigureMappers(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
