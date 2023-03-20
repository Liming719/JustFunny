using JustFunny.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JustFunny.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private DbSet<Users> Users { get; set; }
        private DbContext DbContext { get; set; }

        public HomeController(ILogger<HomeController> logger, DbSet<Users> users, DbContext dbContext)
        {
            _logger = logger;
            Users = users;
            DbContext = dbContext;
        }

        public IActionResult Index()
        {
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

        public IActionResult Login(string username, string password)
        {
            Users user = this.Users.Where(x => x.Name.Equals(username)).First();

            if (user.Role == UserRole.Teacher)
            {
                return RedirectToAction("Teacher", "Home");
            }
            else if(user.Role == UserRole.Student)
            {
                return RedirectToAction("Student", "Home");
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