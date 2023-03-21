using JustFunny.Database;
using JustFunny.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustFunny.Controllers
{
    public class AccountController : Controller
    {
        IDataService<User> userService;
        public AccountController(IDataService<User> userService)
        {
            this.userService = userService;
        }
        public IActionResult Index()
        {
            ViewBag.Action = TempData["action"];
            return View();
        }

        public IActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register()
        {
            TempData["action"] = "Add";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(string username, string email, string password)
        {
            User user = new User
            {
                ID = Guid.NewGuid(),
                Name = username,
                Email = email,
                Password = password,
                Role = UserRole.Student,
                ModifyTime = DateTime.Now,
                CreateTime = DateTime.Now,
                Enabled = true
            };
            await userService.Insert(user);
            return RedirectToAction("Login", "Home", new { username, password });
        }

        [HttpPost]
        public async Task<IActionResult> Update(string username, string email, string password)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("name", username);
            User? user = (await userService.GetAsync("GetByName", keyValuePairs)).DefaultIfEmpty(null).First();
            if(user == null)
                return RedirectToAction("Login", "Home", new { username, password });

            user.Email = email;
            user.Password = password;

            await userService.Update(user);
            return RedirectToAction("Index");
        }
    }
}
