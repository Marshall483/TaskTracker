using Infrastructure.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly INotificator<bool> _notify;

        public ProjectsController(INotificator<bool> notificator)
        {
            _notify = notificator;
        }


        public IActionResult Index() =>
            View();

        public IActionResult Create(Guid forUser) =>
            View();

        public IActionResult View(Guid guid) =>
            View();

        [HttpGet]
        public IActionResult Edit(Guid guid) =>
            View();

        [HttpPost]
        public IActionResult Edit(Guid guid) =>
            View();

        public IActionResult Delete(Guid guid) =>
            View();
        
    }
}
