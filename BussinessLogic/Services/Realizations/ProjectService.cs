using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
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

        public ProjectService(Database db,
            IConstructor<EditProjectViewModel, Project> editor,
            IConstructor<CreateProjectViewModel, Project> creator)
        {
            _editor = editor; 
            _creator = creator;
            _db = db;
        }
        public async Task<Either<Project, ICollection<Error>>> Create(CreateProjectViewModel projectModel)
        {
            if (!CorrectDates(projectModel))
                return Error("Competition date must be greather than start date.");

            var project = _creator.ConstructModel(projectModel);

            _db.Projects.Add(project);
            var inserted = await _db.SaveChangesAsync();

            if (inserted > 0)
                return Success(project);
            else
                return Error("Project not created.");
        }

        public async Task<Either<Project, ICollection<Error>>> View(string guid)
        {
            if (!ValidGuid(guid))
                return Error("Provided guid null or empty");

            var project = _db.Projects
                .Select(p => p) // To avoid "IvalidOperationException"
                .Where(p => p.Id == Guid.Parse(guid))
                .Include(t => t.Tasks)
                .Single();

            if (project != null)
                return Success(project);
            else
                return Error("Project not found or not exist.");

        }

        public async Task<Either<Project, ICollection<Error>>> Edit(EditProjectViewModel model)
        {
            if (!ValidGuid(model.ProjectGuid.ToString()))
                return Error("Provided guid null or empty");

            if (!CorrectDates(model))
                return Error("Competition date must be greather than start date.");

            var project = _db.Projects.Find(model.ProjectGuid);

            if (project == null)
                return Error("Project not found or not exist.");

            var edited = _editor.ConstructModel(model);

            edited.Id = project.Id;
            edited.UserId = project.UserId;

            _db.Projects.Remove(project);
            _db.Projects.Add(edited);

            var inserted = await _db.SaveChangesAsync();

            if (inserted > 0)
                return Success(edited);
            else
                return Error("Project not changed.");
        }

        

        public async Task<Either<Project, ICollection<Error>>> Delete(string guid)
        {
            if (!ValidGuid(guid))
                return Error("Provided guid null or empty");

            var project = _db.Projects.Find(Guid.Parse(guid));

            if (project == null)
                return Error("Project not found or not exist.");

            _db.Projects.Remove(project);
            var deleted = await _db.SaveChangesAsync();

            if (deleted > 0)
                return Success(new Project());
            else
                return Error("Project not deletd.");
        }

        private bool CorrectDates(EditProjectViewModel model) =>
            model.CompetitionDate > model.StartDate;

        private bool CorrectDates(CreateProjectViewModel model) =>
            model.CompetitionDate > model.StartDate;

        private static Either<Project, ICollection<Error>> Error(string error) =>
            Either<Project, ICollection<Error>>.
                WithError(new Error[] { error });

        private static Either<Project, ICollection<Error>> Success(Project proj) =>
            Either<Project, ICollection<Error>>
                .WithSuccess(proj);
                
        private bool ValidGuid(string guid) =>
            guid != null &&
            guid != "" &&
            guid != Guid.Empty.ToString();
    }
}
