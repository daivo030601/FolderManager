using FolderManager.Application.Common.Interfaces;
using FolderManager.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FolderManager.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureShared(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
