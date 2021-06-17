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
            services.AddTransient<IConstructor<CreateProjectViewModel, Project>, CreateProjectConstructor>();
            services.AddTransient<IConstructor<EditProjectViewModel, Project>, EditProjectConstructor>();
            services.AddTransient<IProjectService<Either<Project, ICollection<Error>>, CreateProjectViewModel, EditProjectViewModel>, ProjectService>();

            services.AddTransient<IConstructor<CreateTaskViewModel, ProjectTask>, CreateTaskConstructor>();
            services.AddTransient<IConstructor<EditTaskViewModel, ProjectTask>, EditTaskConstructor>();
            services.AddTransient<IProjectService<Either<ProjectTask, ICollection<Error>>, CreateTaskViewModel, EditTaskViewModel>, TaskService>();

            return services;
        }
    }
}
