using JustFunny.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustFunny.Controllers
{
    public class AccountController : Controller
    {
        private DbSet<Users> Users { get; set; }
        private DbContext DbContext { get; set; }
        public AccountController(DbSet<Users> users, DbContext dbContext)
        {
            Users = users;
            DbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(string username, string email, string password)
        {
            Users.Add(new Users
            {
                Name = username,
                Email = email,
                Password = password,
                Role = UserRole.Student,
                ModifyTime = DateTime.Now,
                CreateTime = DateTime.Now,
                Enabled = true
            });

            DbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
