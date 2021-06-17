using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abstractions;
using Models;
using Monads;

namespace Services
{
    public class ProjectService : IProjectService<Either<Project, ICollection<Error>>>
    {

        public Task<Either<Project, ICollection<Error>>> Create()
        {
            throw new NotImplementedException();
        }

        public Task<Either<Project, ICollection<Error>>> View()
        {
            throw new NotImplementedException();
        }

        public Task<Either<Project, ICollection<Error>>> Edit()
        {
            throw new NotImplementedException();
        }

        public Task<Either<Project, ICollection<Error>>> Delete()
        {
            throw new NotImplementedException();
        }
    }
}
