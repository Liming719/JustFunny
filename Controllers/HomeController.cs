using JustFunny.Database;
using JustFunny.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JustFunny.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IDataService<User> userService;

        public HomeController(ILogger<HomeController> logger, IDataService<User> userService)
        {
            _logger = logger;
            this.userService = userService;
        }

        public IActionResult Index()            
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        public IActionResult Teacher()
        {
            return View();
        }

        public IActionResult Student()
        {
            return View();
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("name", username);
            User? user = (await userService.GetAsync("GetByName", keyValuePairs)).DefaultIfEmpty(null).First();

            if (user == null)
            {
                TempData["Message"] = "User Not Found";
                return RedirectToAction("Index", "Home");
            }

            if (user.Password != password)
            {
                TempData["Message"] = "Incorrect Password";
                return RedirectToAction("Index", "Home");
            }                       

            if (user.Role == UserRole.Teacher)
            {
                return RedirectToAction("Teacher", "Home");
            }
            else if(user.Role == UserRole.Student)
            {
                return RedirectToAction("Student", "Home");
            }
            else if(user.Role == UserRole.Admin)
            {
                return RedirectToAction("Manage", "Account");
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}