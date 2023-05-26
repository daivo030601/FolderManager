using FolderManager.Application.Common.Interfaces;
using FolderManager.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureData(this IServiceCollection services)
        {
            services.AddScoped<IFolderManagerDbContext>(provider => provider.GetService<FolderManagerDbContext>());

            return services;
        }
    }
}
