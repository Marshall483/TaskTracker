using Infrastructure.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.Controllers
{
    public class TaskController : Controller
    {
        private readonly INotificator<bool> _notify;
        public TaskController(INotificator<bool> notificator)
        {
            _notify = notificator;
        }

        public IActionResult Index() =>
            View();

        public IActionResult Create(Guid forProject) => 
            View();    

        public IActionResult Edit(Guid guid) =>
            View();

        public IActionResult Delete(Guid guid) =>
            View();
    }
}
