using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abstractions;
using DataAccess;
using Models;
using Monads;
using ViewModels;

namespace Services
{
    public class ProjectService : IProjectService
        <Either<Project, ICollection<Error>>, CreateProjectViewModel>
    {
        private readonly IConstructor<CreateProjectViewModel, Project> _modelConstructor;
        private readonly Database _db;

        public ProjectService(IConstructor<CreateProjectViewModel, Project> constructor,
            Database db)
        {
            _modelConstructor = constructor;
            _db = db;
        }
        public async Task<Either<Project, ICollection<Error>>> Create(CreateProjectViewModel projectModel)
        {
            var project = _modelConstructor.ConstructModel(projectModel);

            _db.Projects.Add(project);
            var inserted = await _db.SaveChangesAsync();

            if (inserted > 0)
                return Either<Project, ICollection<Error>>.
                    WithSuccess(project);
            else
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Project not created." });
        }

        public async Task<Either<Project, ICollection<Error>>> View(string guid)
        {
            if(guid == null || guid == "" || guid == Guid.Empty.ToString())
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Provided guid null or empty" });

            var project = _db.Projects.Find(Guid.Parse(guid));

            if(project != null)
                return Either<Project, ICollection<Error>>.
                    WithSuccess(project);
            else
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Project not finded or not exist." });
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
