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
        <Either<Project, ICollection<Error>>, 
        CreateProjectViewModel,
        EditProjectViewModel>
    {
        private readonly IConstructor<CreateProjectViewModel, Project> _creator;
        private readonly IConstructor<EditProjectViewModel, Project> _editor;
        private readonly Database _db;

        public ProjectService(IConstructor<CreateProjectViewModel, Project> constructor,
            Database db)
        {
            _creator = constructor;
            _db = db;
        }
        public async Task<Either<Project, ICollection<Error>>> Create(CreateProjectViewModel projectModel)
        {
            if(!CorrectDates(projectModel))
                return Either<Project, ICollection<Error>>.
                   WithError(new Error[] { "Competition date must be greather than start date." });

            var project = _creator.ConstructModel(projectModel);

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
            if(!ValidGuid(guid))
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Provided guid null or empty" });

            var project = _db.Projects.Find(Guid.Parse(guid));

            if(project != null)
                return Either<Project, ICollection<Error>>.
                    WithSuccess(project);
            else
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Project not found or not exist." });
        }

        public async Task<Either<Project, ICollection<Error>>> Edit(EditProjectViewModel model)
        {
            if (!ValidGuid(model.ProjectGuid.ToString()))
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Provided guid null or empty" });

            if(!CorrectDates(model))
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Competition date must be greather than start date." });

            var project = _db.Projects.Find(model.ProjectGuid);

            if (project == null)
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Project not found or not exist." });

            var edited = _editor.ConstructModel(model);
            edited.Id = model.ProjectGuid;

            _db.Projects.Update(edited);
            var inserted = await _db.SaveChangesAsync();

            if (inserted > 0)
                return Either<Project, ICollection<Error>>.
                    WithSuccess(edited);
            else
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Project not changed." });
        }

        public async Task<Either<Project, ICollection<Error>>> Delete(string guid)
        {
            if (!ValidGuid(guid))
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Provided guid null or empty" });

            var project = _db.Projects.Find(Guid.Parse(guid));

            if (project == null)
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Project not found or not exist." });

            _db.Projects.Remove(project);
            var deleted = await _db.SaveChangesAsync();

            if (deleted > 0)
                return Either<Project, ICollection<Error>>.
                    WithSuccess(new Project()); 
            else
                return Either<Project, ICollection<Error>>.
                    WithError(new Error[] { "Project not deletd." });

        }

        private bool CorrectDates(EditProjectViewModel model) =>
            model.CompetitionDate > model.StartDate;

        private bool CorrectDates(CreateProjectViewModel model) =>
            model.CompetitionDate > model.StartDate;

        private bool ValidGuid(string guid) =>
            guid != null &&
            guid != "" &&
            guid != Guid.Empty.ToString();
    }
}
