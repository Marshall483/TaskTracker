using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Models;
using System.Threading.Tasks;
using Monads;

namespace Services 
{
    public class TaskService : IProjectService<Either<ProjectTask, ICollection<Error>>>
    {
        public Task<Either<ProjectTask, ICollection<Error>>> Create()
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

        public Task<Either<ProjectTask, ICollection<Error>>> View()
        {
            throw new NotImplementedException();
        }
    }
}
