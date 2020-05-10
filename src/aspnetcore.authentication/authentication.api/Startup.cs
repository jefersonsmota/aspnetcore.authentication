using authentication.api.Filters;
using authentication.api.ViewModels;
using authentication.application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;
using System.Linq;
using System.Text;
using authentication.api.Services;
using Microsoft.AspNetCore.Authorization;
using authentication.api.Configurations;

namespace authentication.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            ConfigureIoC(services);

            services.AddCors();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ApiExceptionFilterAttribute());
                options.Filters.Add(new JwtAuthorizationFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            });

            services.AddControllers()
                .AddJsonOptions(opts => { opts.JsonSerializerOptions.IgnoreNullValues = true; })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState.Values.SelectMany(x => x.Errors);

                        var errorMessage = errors.FirstOrDefault()?.ErrorMessage;

                        return new BadRequestObjectResult(new DataResponse(errorMessage, 400));
                    };
                });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            services.AddSingleton(appSettings);
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "MyBearer";
                options.DefaultChallengeScheme = "MyBearer";

                options.AddScheme<CustomAuthenticationHandler>("MyBearer", "MyBearer");
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    LifetimeValidator = TokenLifetimeValidator.Validate
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Carregamento de injeções de dependencia.
        /// </summary>
        /// <param name="services">Service Collection</param>
        private void ConfigureIoC(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddApplication();
        }
    }
}
