﻿using System;
using Microsoft.Extensions.DependencyInjection;
using DocIT.Core.Repositories;
using DocIT.Core.Repositories.Implementations;
using DocIT.Core.Services;
using DocIT.Core.Services.Implementations;

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

        }

        private static void COnfigureServices(IServiceCollection service)
        {
            service.AddScoped<IGitConfigurationService, GitConfigurationService>();
            service.AddScoped<IUserAuthenticationService, AuthenticationService>();
        }
    }
}
