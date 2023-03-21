using ClosedXML.Excel;
using JustFunny.Database;
using JustFunny.Models;
using JustFunny.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JustFunny.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IDataService<User> userService;
        IDataService<Question> questionService;

        public HomeController(ILogger<HomeController> logger, IDataService<User> userService, IDataService<Question> questionService)
        {
            _logger = logger;
            this.userService = userService;
            this.questionService = questionService;
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
        public async Task<IActionResult> Quiz()
        {
            IEnumerable<Question> questions = await questionService.GetAsync("GetAll", null);
            return View(questions);
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
            else if (user.Role == UserRole.Student)
            {
                return RedirectToAction("Student", "Home");
            }
            else if (user.Role == UserRole.Admin)
            {
                return RedirectToAction("Manage", "Account");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ImportQuestions(IFormFile file)
        {
            // Open the Excel file using ClosedXML
            var workbook = new XLWorkbook(file.OpenReadStream());
            
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RowsUsed().Skip(1); // Skip header row

            // Map each row to an ExamQuestion object
            IEnumerable<Question> questions = rows.Select(row => new Question
            {
                ID = Guid.NewGuid(),
                Type = row.Cell(1).Value.ToString(),
                Content = row.Cell(2).Value.ToString(),
                OptionA = row.Cell(3).Value.ToString(),
                OptionB = row.Cell(4).Value.ToString(),
                OptionC = row.Cell(5).Value.ToString(),
                OptionD = row.Cell(6).Value.ToString(),
                Answer = row.Cell(7).Value.ToString()
            });
            foreach(Question question in questions)
            {
                await questionService.Insert(question);
            } 
            // Pass the list of questions to the view
            return View("Teacher", questions);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}