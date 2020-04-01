using Gestor.Core.Application;
using Gestor.Core.Domain.Contracts.Services;
using Gestor.Core.Infra.Repository.Migrations;
using Gestor.Tools.Contracts.Repository;
using Gestor.Core.Infra.Providers;
using Gestor.Tools.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Gestor.Tools.Logging;
using Gestor.Api.Security;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gestor
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var paramsValidation = options.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfigurations.Key));
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddSingleton(tokenConfigurations);

            services.AddControllersCustom();

            services.AddSingleton<NHContext>();

            services.AddScoped<IUnitOfWork, NHUnitOfWork>();

            services.AddInvalidModelStateResponse();

            services.AddAPIResult();

            AddOneTransactionPerHttpCall(services);

            services.AddMessageNotifier(Configuration);

            services.AddServiceMappingsFromAssemblies<BaseApplication, IBaseService, GestorDbMigrator>();

            GestorDbMigrator.RunMigrationUp(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyMethod()
                                          .AllowAnyOrigin()
                                          .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<MessageLoggingMiddleware>(Configuration["Logging:Name"]);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddOneTransactionPerHttpCall(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkTransaction>((serviceProvider) =>
            {
                var wow = serviceProvider.GetService<IUnitOfWork>();

                wow.Open();

                return wow.BeginTransaction();
            });

            services.AddScoped<UnitOfWorkFilter>();
            services.Configure<MvcOptions>(o => o.Filters.AddService<UnitOfWorkFilter>(2));
        }
    }
}
