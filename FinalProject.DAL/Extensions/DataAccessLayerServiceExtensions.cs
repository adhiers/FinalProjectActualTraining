using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.DAL.Extensions
{
    public static class DataAccessLayerServiceExtensions
    {
        public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with connection string from configuration
            services.AddDbContext<FinalProjectDBContext>(options =>
                options.UseSqlServer(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("FinalProjectConnectionString")));

            services.AddScoped<ICar, CarDAL>();
            services.AddScoped<IDealer, DealerDAL>();

            return services;
        }
    }
}
