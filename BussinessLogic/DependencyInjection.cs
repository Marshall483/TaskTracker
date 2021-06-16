using Abstractions;
using Constructors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Monads;
using Services;
using System.Collections.Generic;
using ViewModels;

namespace BussinessLogic
{
    public static class Extension
    {
        public static IServiceCollection ConfigureBussinessLogic(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IConstructor<CreateProjectViewModel, Project>, CreateProjectConstructor>();
            services.AddSingleton<IConstructor<EditProjectViewModel, Project>, EditProjectConstructor>();
            services.AddSingleton<IProjectService<Either<Project, ICollection<Error>>>, ProjectService>();

            return services;
        }
    }
}
