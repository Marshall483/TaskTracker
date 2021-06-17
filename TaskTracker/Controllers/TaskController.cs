using Abstractions;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Monads;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IProjectService<Either<ProjectTask, ICollection<Error>>, CreateTaskViewModel, EditTaskViewModel> _taskService;
        public TaskController(INotificator<bool> notificator,
            IProjectService<Either<ProjectTask, ICollection<Error>>, CreateTaskViewModel, EditTaskViewModel> taskService,
            IConstructor<CreateTaskViewModel, ProjectTask> creator,
            IConstructor<EditTaskViewModel, ProjectTask> editor )
        {
            _notify = notificator;
            _taskService = taskService;
            _creator = creator;
            _editor = editor;
        }

        public async Task<IActionResult> Index(string taskGuid)
        {
            var res = await _taskService.View(taskGuid);

            if (res.Succeeded)
                return View(_creator.ConsructView(res.GetResult));
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

            return View("NewTaskPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskViewModel taskModel)
        {
            if (ModelState.IsValid)
            {
                var res = await _taskService.Create(taskModel);

                if (res.Succeeded)
                    return View(_creator.ConsructView(res.GetResult));
                else
                    foreach (var error in res.GetFail)
                        ModelState.AddModelError(string.Empty, (string)error);
            }
            return View("NewTaskPartial", taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTaskViewModel taskModel)
        {
            var res = await _taskService.Edit(taskModel);

            //Here problem with TaskGuid in EditTaskViewModel

            if (res.Succeeded)
                return RedirectToAction("Index", new { taskGuid = taskModel.TaskGuid});
            else
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            return View(taskModel);
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
