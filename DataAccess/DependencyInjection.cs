using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;


namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDataAccess(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<Database>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ITISBet")));

            services.AddIdentity<User, IdentityRole<Guid>>(opts => {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
              .AddEntityFrameworkStores<Database>()
              .AddDefaultTokenProviders();

            return services;
        }
    }
}
