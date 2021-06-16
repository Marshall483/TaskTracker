using DataAccess;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace TaskTracker.Controllers
{
    public class RegLogController : Controller
    {
        private readonly INotificator<bool> _notify;
        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _signIn;
        private readonly Database _db;

        public RegLogController(INotificator<bool> notificator, UserManager<User> userManager,
            SignInManager<User> signInManager, Database db)
        {
            _notify = notificator;
            _user = userManager;
            _signIn = signInManager;
            _db = db;
        }

        [HttpGet]
        public IActionResult Index() =>
            View(new Tuple<LoginViewModel, RegisterViewModel>(
                    new LoginViewModel(), new RegisterViewModel()));

        [HttpPost]
        public async Task<IActionResult> Reg(RegisterViewModel regVM)
        {
            if (ModelState.IsValid)
            {

                var user = await _user.FindByNameAsync(regVM.Email);

                if (user != null)
                {
                    ModelState.AddModelError(string.Empty, "User already exist");
                    return View(new Tuple<LoginViewModel, RegisterViewModel>(null, regVM));
                }

                await _user.CreateAsync(CreateUser(regVM.Email), regVM.Password);
                await _notify.AboutRegistrationAsync(RegistrationReason.Succeeded, regVM.Email);

                return await Log(new LoginViewModel { Email = regVM.Email, Password = regVM.Password });

            }
            return View("Index", new Tuple<LoginViewModel, RegisterViewModel>(null, regVM));
        }

        [HttpPost]
        public async Task<IActionResult> Log(LoginViewModel logVM)
        {
            if (ModelState.IsValid)
            {

                var user = await _user.FindByEmailAsync(logVM.Email);

                if (user == null)
                    return RedirectToAction("Reg", "RegLog");

                var res = await SignIn(user.UserName, logVM.Password, logVM.Remember);

                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError(string.Empty, "Incorrect login or password.");

            }
            return View("Index", new Tuple<LoginViewModel, RegisterViewModel>(logVM, null));
        }

        [HttpGet]
        public IActionResult GetRegPartial() =>
             PartialView("RegPartial", new RegisterViewModel());
        
        [HttpGet]
        public IActionResult GetLogPartial() =>
            PartialView("LogPartial", new LoginViewModel());
        
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signIn.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task<Microsoft.AspNetCore.Identity.SignInResult> SignIn(string userName,
           string password, bool remember) =>
               await _signIn.PasswordSignInAsync(userName, password, remember, false);

        private User CreateUser(string email) => new User { Email = email, UserName = email };
    }
}
