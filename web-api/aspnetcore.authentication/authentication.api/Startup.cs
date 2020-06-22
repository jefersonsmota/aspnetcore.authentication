using authentication.api.Filters;
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
using authentication.api.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using authentication.api.Events;
using authentication.domain.Notifications;
using authentication.application.Common;
using authentication.domain.Constants;
using System.Collections.Generic;

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
            });

            services.AddControllers()
                .AddJsonOptions(opts => { opts.JsonSerializerOptions.IgnoreNullValues = true; })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        IEnumerable<Notification> errors = null;

                        if (context.ModelState.Any())
                            errors = context.ModelState.Select(x => new Notification(x.Key, x.Value.Errors.First().ErrorMessage));

                        return new BadRequestObjectResult(new CommandResponse(400, Messages.INVALID_FIELDS, null, false) { Notifications = errors });
                    };
                });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            services.AddSingleton(appSettings);
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
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

                options.Events = new JwtBearerCustomEvents();
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
            services.AddScoped<NotificationContext>();
            services.AddSingleton(Configuration);
            services.AddApplication();
        }
    }
}
