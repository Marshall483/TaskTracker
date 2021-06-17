using BussinessLogic.Extensions;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Abstractions;
using System;
using Monads;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskTracker.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly UserManager<User> _user;
        private readonly INotificator<bool> _notify;
        private readonly IConstructor<CreateProjectViewModel, Project> _creator;
        private readonly IConstructor<EditProjectViewModel, Project> _editor;
        private readonly IProjectService<Either<Project, ICollection<Error>>, CreateProjectViewModel, EditProjectViewModel> _projectService;

        public ProjectController(INotificator<bool> notificator, UserManager<User> userManager, 
            IProjectService<Either<Project, ICollection<Error>>, CreateProjectViewModel, EditProjectViewModel> projectService,
            IConstructor<CreateProjectViewModel, Project> createProject, 
            IConstructor<EditProjectViewModel, Project> editProject )
        {
            _notify = notificator;
            _user = userManager;
            _projectService = projectService;
            _creator = createProject;
            _editor = editProject;
        }

        [HttpGet]
        public IActionResult Index() 
        { 
            var user = _user.AndProjects(User.Identity.Name);

            ViewBag.UserGuid = user.Id.ToString();

            return View(user.Projects);
        }

        [HttpGet]
        public IActionResult CreateProject(string userGuid)
        {
            ViewBag.UserGuid = userGuid;

            return View("Create", _creator.ConsructView(new Project()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                ViewBag.UserGuid = project.UserGuid;

                var res = await _projectService.Create(project);

                if (res.Succeeded)
                    return RedirectToAction("Index");
                else
                    foreach (var error in res.GetFail)
                        ModelState.AddModelError(string.Empty, (string)error);
            }

            project.StateSelect = new SelectList(new[] { "Not started", "Active", "Completed" });
            project.PrioritySelect = new SelectList(new[] { "Low", "Normal", "High" });

            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Project(string projectGuid)
        {
            var res = await _projectService.View(projectGuid);

            if (res.Succeeded)
                return View(res.GetResult);
            else
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            return View(new Project());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string projectGuid)
        {
            var res = await _projectService.View(projectGuid);

            if (res.Succeeded)
                return View(_editor.ConsructView(res.GetResult));
            else
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditProject(EditProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _projectService.Edit(model);

                if (res.Succeeded)
                    return View("Project", res.GetResult);
                else
                    foreach (var error in res.GetFail)
                        ModelState.AddModelError(string.Empty, (string)error);
            }

            model.StateSelect = new SelectList(new[] { "Not started", "Active", "Completed" });
            model.PrioritySelect = new SelectList(new[] { "Low", "Normal", "High" });

            return View("Edit", model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string projectGuid)
        {
            var res = await _projectService.Delete(projectGuid);

            if (res.Succeeded)
                return RedirectToAction("Index");
            else
                foreach (var error in res.GetFail)
                    ModelState.AddModelError(string.Empty, (string)error);

            return RedirectToAction("Project", routeValues: new { projectGuid });
        }
        
    }
}
