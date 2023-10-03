using inventory.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace inventory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }
       



        public IActionResult Equipment()
        {
            return View();
        }
        public IActionResult EditTypeEquipment()
        {
            return View();
        }
        public IActionResult EditEquipment()
        {
            return View();
        }
        public IActionResult ChangePoliz()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult AddTypeEquipment()
        {
            return View();

        }
        public IActionResult AddEquipment()
        {
            return View();

        }
        public IActionResult AddHistory()
        {
            return View();

        }
        public IActionResult AddPoliz()
        {
            return View();

        }
        public IActionResult MainPage()
        {

            return View();

        }
        public IActionResult PersonalAccount()
        {
            return View();

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
