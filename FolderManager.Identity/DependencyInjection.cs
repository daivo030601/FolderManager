using FolderManager.Application.Common.Interfaces;
using FolderManager.Identity.Helpers;
using FolderManager.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIdentity (this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AuthSettings>(config.GetSection(nameof(AuthSettings)));
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
