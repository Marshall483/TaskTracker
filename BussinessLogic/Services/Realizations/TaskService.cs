using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Models;
using System.Threading.Tasks;
using Monads;
using ViewModels;

namespace Services 
{
    public class TaskService : IProjectService<Either<ProjectTask, ICollection<Error>>, CreateTaskViewModel>
    {
        public Task<Either<ProjectTask, ICollection<Error>>> Create(CreateTaskViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Either<ProjectTask, ICollection<Error>>> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<Either<ProjectTask, ICollection<Error>>> Edit()
        {
            throw new NotImplementedException();
        }

        public Task<Either<ProjectTask, ICollection<Error>>> View(string guid)
        {
            throw new NotImplementedException();
        }
    }
}
