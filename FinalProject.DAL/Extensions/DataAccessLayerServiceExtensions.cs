using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.BO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.DAL.Extensions
{
    public static class DataAccessLayerServiceExtensions
    {
        public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services)
        {
            // Register DbContext with connection string from configuration
            services.AddDbContext<FinalProjectDBContext>(options =>
               options.UseSqlServer(services.BuildServiceProvider()
                   .GetRequiredService<IConfiguration>()
                   .GetConnectionString("FinalProjectConnectionString")));


            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<FinalProjectDBContext>();

            services.AddScoped<ICar, CarDAL>();
            services.AddScoped<IDealer, DealerDAL>();
            services.AddScoped<IDealerCar, DealerCarDAL>();
            services.AddScoped<IUsMan, UsManDAL>();
            services.AddScoped<ISalesPerson, SalesPersonDAL>();
            services.AddScoped<IScheduling, SchedulingDAL>();
            services.AddScoped<IConsultation, ConsultationDAL>();
            services.AddScoped<IGuest, GuestDAL>();

            return services;
        }
    }
}
