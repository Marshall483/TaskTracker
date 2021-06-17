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

namespace TaskTracker.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly UserManager<User> _user;
        private readonly INotificator<bool> _notify;
        private readonly IConstructor<CreateProjectViewModel, Project> _createProject;
        private readonly IConstructor<EditProjectViewModel, Project> _editProject;
        private readonly IProjectService<Either<Project, ICollection<Error>>, CreateProjectViewModel> _projectService;

        public ProjectController(INotificator<bool> notificator, UserManager<User> userManager, 
            IProjectService<Either<Project, ICollection<Error>>, CreateProjectViewModel> projectService,
            IConstructor<CreateProjectViewModel, Project> createProject, 
            IConstructor<EditProjectViewModel, Project> editProject )
        {
            _notify = notificator;
            _user = userManager;
            _projectService = projectService;
            _createProject = createProject;
            _editProject = editProject;
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

            return View("Create", _createProject.ConsructView(new Project()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                ViewBag.UserGuid = project.UserGuid;

                var res = await _projectService.Create(project);

                if (res.Succeeded)
                    return await Project(res.GetResult.Id.ToString());
                else
                    foreach (var error in res.GetFail)
                        ModelState.AddModelError(string.Empty, (string)error);
            }

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
        public IActionResult GetEdit(Guid guid) =>
            View();

        [HttpPost]
        public IActionResult PostEdit(Guid guid) =>
            View();

        [HttpPost]
        public IActionResult PostDelete(Guid guid) =>
            View();
        
    }
}
