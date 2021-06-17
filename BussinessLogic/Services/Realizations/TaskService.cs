using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Models;
using System.Threading.Tasks;
using Monads;
using ViewModels;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Services 
{
    public class TaskService : IProjectService
        <Either<ProjectTask, ICollection<Error>>, 
        CreateTaskViewModel, 
        EditTaskViewModel>
    {
        private readonly Database _db;
        private readonly IConstructor<CreateTaskViewModel, ProjectTask> _creator;
        private readonly IConstructor<EditTaskViewModel, ProjectTask> _editor;

        public TaskService(Database db,
            IConstructor<CreateTaskViewModel, ProjectTask> creator,
            IConstructor<EditTaskViewModel, ProjectTask> editor)
        {
            _db = db;
            _creator = creator;
            _editor = editor;
        }

        public async Task<Either<ProjectTask, ICollection<Error>>> Create(CreateTaskViewModel model)
        {
            if (!ValidGuid(model.ProjectGuid.ToString()))
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Provided guid null or empty" });

            var task = _creator.ConstructModel(model);

            _db.ProjectTasks.Add(task);
            var inserted = await _db.SaveChangesAsync();

            if (inserted > 0)
                return Either<ProjectTask, ICollection<Error>>.
                    WithSuccess(task);
            else
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Project not created." });
        }

        public async Task<Either<ProjectTask, ICollection<Error>>> Delete(string taskGuid)
        {
            if (!ValidGuid(taskGuid))
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Provided guid null or empty" });

            var task = _db.ProjectTasks.Find(Guid.Parse(taskGuid));

            if (task == null)
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Task not found or not exist." });

            _db.ProjectTasks.Remove(task);
            var deleted = await _db.SaveChangesAsync();

            if (deleted > 0)
                return Either<ProjectTask, ICollection<Error>>.
                    WithSuccess(new ProjectTask());
            else
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Task not deletd." });
        }

        public async Task<Either<ProjectTask, ICollection<Error>>> Edit(EditTaskViewModel model)
        {
            if (!ValidGuid(model.ProjectGuid.ToString()))
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Provided guid null or empty" });

            var task = _db.ProjectTasks.Find(model.TaskGuid);

            if (task == null)
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Task not found or not exist." });

            task.TaskName = model.Name;
            task.State = _taskStateByStringMap[model.TaskState];

            _db.ProjectTasks.Update(task);
            var updated = await _db.SaveChangesAsync();

            if (updated > 0)
                return Either<ProjectTask, ICollection<Error>>.
                    WithSuccess(task);
            else
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Task not updated." });
        }

        public async Task<Either<ProjectTask, ICollection<Error>>> View(string taskGuid)
        {
            if (!ValidGuid(taskGuid))
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Provided guid null or empty" });

            var task = _db.ProjectTasks
                .Select(t => t) // To avoid "IvalidOperationException"
                .Where(t => t.Id.Equals(Guid.Parse(taskGuid)))
                .Include(f => f.Fields)
                .Single();

            if (task != null)
                return Either<ProjectTask, ICollection<Error>>.
                    WithSuccess(task);
            else
                return Either<ProjectTask, ICollection<Error>>.
                    WithError(new Error[] { "Task not found or not exist." });
        }

        private bool ValidGuid(string guid) =>
            guid != null &&
            guid != "" &&
            guid != Guid.Empty.ToString();

        public readonly Dictionary<string, TaskState> _taskStateByStringMap =
            new Dictionary<string, TaskState>
            {
                { "ToDo", TaskState.ToDo },
                { "In Progress", TaskState.InProgress },
                { "Done", TaskState.Done },
            };
    }
}
