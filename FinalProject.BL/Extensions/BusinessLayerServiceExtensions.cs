using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL;
using FinalProject.BL.DTO;
using FinalProject.BL.Profiles;
using FinalProject.BL.Helpers;
using FinalProject.DAL;
using FinalProject.DAL.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace FinalProject.BL.Extensions
{
    public static class BusinessLayerServiceExtensions
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddDataAccessLayerServices();
            var appSettingsSection = services.BuildServiceProvider()
                .GetRequiredService<IConfiguration>()
                .GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            if (string.IsNullOrEmpty(appSettings?.Secret))
            {
                throw new ArgumentNullException(nameof(appSettings.Secret), "AppSettings Secret cannot be null or empty");
            }
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            if (key.Length == 0)
            {
                throw new ArgumentException("AppSettings Secret must be a valid non-empty string", nameof(appSettings.Secret));
            }
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Register the AutoMapper profile
            services.AddAutoMapper(cfg => cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg1NDU2MDAwIiwiaWF0IjoiMTc1Mzk1MjA3NyIsImFjY291bnRfaWQiOiIwMTk4NWZhZjMyMzM3MDk0ODE4MDg2ZDQzZmM0ZDg1OCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazFmdjFoY3k3d3lnaHdkaHZmeThqaDRqIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.v6WQtGCuYFa6Y3xZxb_irD2AXQv1J4J9XBQixeZXVeVDgntuWhLj6TYYZ4bs9TM8keJpEt0rOy2R2RC53ES-YsGxVCNidGicXXxax-jEyu567M4yx_bopvZKTDnhZYs04UEAvlL7oWM24CU_7hTVDzzdAT3jDou7Cf5-9g4yyQZfYPk7knPT2UH3UppnLdh_MhJhWUwVuvGfeM_7DSfps0BO_JjnFKyn5oAGinutJiMwaL5asJhFI6vxwUsxznLhgWV_np2TBhYSBUEOhXiLW2hXI3iHYgJzUkoN2YINKn-Unzc7Ekfu2jNnUxtZI8w_Av5273q4v-Gsgdaofz-A7Q",
                typeof(BusinessLayerServiceExtensions));

            services.AddScoped<ICarBL, CarBL>();
            services.AddScoped<IDealerBL, DealerBL>();
            services.AddScoped<IDealerCarBL, DealerCarBL>();
            services.AddScoped<ISalesPersonBL, SalesPersonBL>();
            services.AddScoped<IUsManBL, UsManBL>();
            services.AddScoped<IGuestBL, GuestBL>();
            services.AddScoped<ISchedulingBL, SchedulingBL>();

            return services;
        }
    }
}
