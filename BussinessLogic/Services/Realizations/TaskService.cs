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
    public class TaskService : IProjectService
        <Either<ProjectTask, ICollection<Error>>, 
        CreateTaskViewModel, 
        EditTaskViewModel>
    {
        public Task<Either<ProjectTask, ICollection<Error>>> Create(CreateTaskViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Either<ProjectTask, ICollection<Error>>> Delete(string guid)
        {
            throw new NotImplementedException();
        }

        public Task<Either<ProjectTask, ICollection<Error>>> Edit(EditTaskViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Either<ProjectTask, ICollection<Error>>> View(string guid)
        {
            throw new NotImplementedException();
        }
    }
}
