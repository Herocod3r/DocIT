using System;
using Microsoft.Extensions.DependencyInjection;
using DocIT.Core.Repositories;
using DocIT.Core.Repositories.Implementations;
using DocIT.Core.Services;
using DocIT.Core.Services.Implementations;
using DocIT.Service.Services;

namespace DocIT.Service
{
    public static class Initialize
    {
        public static void ConfigureCoreApp(this IServiceCollection service)
        {
            ConfigureRepositories(service);
            COnfigureServices(service);
        }


        private static void ConfigureRepositories(IServiceCollection service)
        {
            service.AddScoped<IGitConfigurationRepository, GitConfigurationRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IProjectRepository, ProjectRepository>();
        }

        private static void COnfigureServices(IServiceCollection service)
        {
            service.AddScoped<IGitConfigurationService, GitConfigurationService>();
            service.AddScoped<Authentication.IAuthenticationService, Authentication.CoreAuthentication>();
            service.AddScoped<Authentication.IUserAuthTokenService, Authentication.TokenService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IInviteService, InviteService>();
        }
    }
}
