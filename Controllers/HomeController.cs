using inventory.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using inventory.Interfaces;
using Org.BouncyCastle.Utilities;
using inventory.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace inventory.Controllers
{
    public class HomeController : Controller
    {

		private IUsers IAllUsers;
		private IEquipment IAllEquipment;
		private IFilial IAllFilial;
		VMUsers vmusers = new VMUsers();
		VMEquipment vmequipment = new VMEquipment();

		private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
		/*private readonly ILogger<HomeController> _logger;*/

		public HomeController(IUsers iAllUsers, IEquipment iAllEquipment, IFilial iAllFilial, /*ILogger<HomeController> logger*/ Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            IAllUsers = iAllUsers;
			IAllEquipment = iAllEquipment;
			IAllFilial = iAllFilial;
			hostingEnvironment = environment;
			/*_logger = logger;*/

		}
		public ViewResult user()
		{
			

			vmusers.Users = IAllUsers.AllUsers;
			vmusers.Equipment = IAllEquipment.AllEquipment;
			vmusers.Filial = IAllFilial.AllFilial;

			return View(vmusers);
		}
		[HttpGet]
		public ViewResult Add()
		{
			
			IEnumerable<Users> users = IAllUsers.AllUsers;

			return View(users);
		}
		[HttpPost]
		public RedirectResult Add(string FIO, string Filial, string Email)
		{
		

			Users newUsers = new Users();
			newUsers.FIO = FIO;
			newUsers.Filial = Filial;
			newUsers.Email = Email;

			int id = IAllUsers.Add(newUsers);

			return Redirect("/user");

		}









		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
