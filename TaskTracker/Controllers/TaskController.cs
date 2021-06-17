using Infrastructure.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public TaskController(INotificator<bool> notificator)
        {
            _notify = notificator;
        }

        public IActionResult Index() =>
            View();

        [HttpGet]
        public IActionResult Create(string projectGuid) =>
            View();

        [HttpPost]
        public IActionResult CreateTask(CreateTaskViewModel viewModel) =>
            View();

        [HttpPost]
        public IActionResult Edit(string taskGuid) =>
            View();

        [HttpPost]
        public IActionResult Delete(string taskGuid) =>
            View();
    }
}
