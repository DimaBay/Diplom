using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace inventory.Pages
{
    public class profileModel : PageModel
    {
		public DBProfile bProfile = new DBProfile();

		public List<Profile> profiles = new List<Profile>();
		
		public Profile newProfile = new Profile();
		public string UserRole { get; set; }
		public string FirstName { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }

		public void OnGet()
		{

			Login = HttpContext.Session.GetString("Login");

			profiles = bProfile.AllProfile(Login).ToList(); //вывод в основной профиль
			UserRole = HttpContext.Session.GetString("UserRole");
			FirstName = HttpContext.Session.GetString("FirstName");
			Email = HttpContext.Session.GetString("Email");

		}

	
	}
}
