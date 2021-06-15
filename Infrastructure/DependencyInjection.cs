using Infrastructure.EmailNotifications;
using Infrastructure.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class Extenstion
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services,
                    IConfiguration configuration)
        {
            services.AddSingleton<EmailSender>();
            services.AddSingleton<INotificator<bool>, EmailNotificator>();

            return services;
        }
    }
}