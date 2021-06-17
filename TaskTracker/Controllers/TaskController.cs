using Abstractions;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Monads;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using ViewModels;

namespace TaskTracker.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly INotificator<bool> _notify;
        private readonly IConstructor<CreateTaskViewModel, ProjectTask> _creator;
        private readonly IConstructor<EditTaskViewModel, ProjectTask> _editor;
        private readonly IFieldService<Either<TaskFields, ICollection<Error>>> _fieldService;
        private readonly IProjectService<Either<ProjectTask, ICollection<Error>>, CreateTaskViewModel, EditTaskViewModel> _taskService;
        public TaskController(INotificator<bool> notificator,
            IProjectService<Either<ProjectTask, ICollection<Error>>, CreateTaskViewModel, EditTaskViewModel> taskService,
            IConstructor<CreateTaskViewModel, ProjectTask> creator,
            IConstructor<EditTaskViewModel, ProjectTask> editor,
            IFieldService<Either<TaskFields, ICollection<Error>>> fieldService)
        {
            _notify = notificator;
            _taskService = taskService;
            _creator = creator;
            _editor = editor;
            _fieldService = fieldService;
        }

        [HttpGet]
        public async Task<IActionResult> Task(string taskGuid)
        {
            var res = await _taskService.View(taskGuid);

            if (res.Succeeded)
                return View(res.GetResult);
            else
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            return View(new ProjectTask());
        }

        [HttpGet]
        public IActionResult Create(string projectGuid)
        {
            var model = new CreateTaskViewModel();
            model.ProjectGuid = Guid.Parse(projectGuid);
            model.State = new SelectList(new[] { "ToDo", "In Progress", "Done" });

            return View("NewTaskPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskViewModel taskModel)
        {
            if (ModelState.IsValid)
            {
                var res = await _taskService.Create(taskModel);

                if (res.Succeeded)
                    return RedirectToAction("Task", new { taskGuid = res.GetResult.Id });
                else
                    foreach (var error in res.GetFail)
                        ModelState.AddModelError(string.Empty, (string)error);
            }
            return View("NewTaskPartial", taskModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string taskGuid)
        {
            var res = await _taskService.View(taskGuid);

            if (res.Succeeded)
                return View(_editor.ConsructView(res.GetResult));
            else
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            return View(new EditTaskViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> EditTask(EditTaskViewModel taskModel)
        {
            var res = await _taskService.Edit(taskModel);

            if (res.Succeeded)
                return RedirectToAction("Task", new { taskGuid = taskModel.TaskGuid});
            else
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            return View(taskModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddField(AddTaskFieldViewModel fieldModel)
        {
            if (ModelState.IsValid)
            {
                var res = await _fieldService.AddField(fieldModel);

                if(res.Succeeded)
                    return RedirectToAction("Task", new { taskGuid = fieldModel.TaskGuid });
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            }
            return View("NewFieldPartial", fieldModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditField(EditFieldViewModel fieldModel)
        {
            if (ModelState.IsValid)
            {
                var res = await _fieldService.EditField(fieldModel);

                if (res.Succeeded)
                    return RedirectToAction("Task", new { taskGuid = fieldModel.TaskGuid });
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            }
            return View("NewFieldPartial", fieldModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteField(string fieldGuid)
        {
            var res = await _fieldService.DeleteField(fieldGuid);

            if (res.Succeeded)
                return RedirectToAction("Task", new { taskGuid = res.GetResult.TaskId });
            foreach (var error in res.GetFail)
                ModelState.AddModelError(string.Empty, (string)error);

            return RedirectToAction("Index", "Project");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string taskGuid)
        {
            var res = await _taskService.Delete(taskGuid);

            if (res.Succeeded)
                return RedirectToAction("Index", "Project");
            else
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            return RedirectToAction("Index", "Project");
        }
    }
}
