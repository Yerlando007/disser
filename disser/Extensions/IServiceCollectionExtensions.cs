using Microsoft.EntityFrameworkCore;
using disser.Interfaces;
using disser.Models.Base;
using disser.Services;

namespace disser.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                //options.UseLazyLoadingProxies();
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserService, UserService>()
                .AddScoped<IGOST, GOSTService>();
        }
    }
}
