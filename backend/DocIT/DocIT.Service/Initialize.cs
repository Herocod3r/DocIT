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
            ConfigureServices(service);
        }


        private static void ConfigureRepositories(IServiceCollection service)
        {
            service.AddTransient<IGitConfigurationRepository, GitConfigurationRepository>();
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IProjectRepository, ProjectRepository>();
            service.AddTransient<IProjectInviteRepository, InviteRepository>();
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            service.AddTransient<IFileUploader, FileUploader>();
            service.AddTransient<IGitConfigurationService, GitConfigurationService>();
            service.AddTransient<Authentication.IAuthenticationService, Authentication.CoreAuthentication>();
            service.AddTransient<Authentication.IUserAuthTokenService, Authentication.TokenService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IProjectService, ProjectService>();
            service.AddTransient<IInviteService, InviteService>();
        }
    }
}
