using inventory.Interfaces;
using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace inventory.Pages
{
    public class userModel : PageModel
    {
		public DBFilial bFilial = new DBFilial();
		public DBUsers bUsers = new DBUsers();
		public List<Users> users = new List<Users>();
		public List<Filial> filials = new List<Filial>();
		public Users newUser = new Users();
		public string UserRole { get; set; }
		public DBProfile bProfile = new DBProfile();

		public List<Profile> profiles = new List<Profile>();

		public Profile newProfile = new Profile();
		public string FirstName { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }

		public void OnGet()
		{
			Login = HttpContext.Session.GetString("Login");
			profiles = bProfile.AllProfile(Login).ToList();
			filials = bFilial.AllFilial.ToList();
			users = bUsers.AllUsers.ToList();
			UserRole = HttpContext.Session.GetString("UserRole");
			FirstName = HttpContext.Session.GetString("FirstName");
			Email = HttpContext.Session.GetString("Email");
		}

		public IActionResult OnPost()
		{



			
				newUser.FIO = Request.Form["FIO"];
				newUser.Email = Request.Form["Email"];
				newUser.Filial = Request.Form["Filial"];
				bUsers.Add(newUser);
				filials = bFilial.AllFilial.ToList();
				users = bUsers.AllUsers.ToList();



				return RedirectToPage(); // Перенаправляем пользователя обратно на эту же страницу
			

			return Page();

		}
		[BindProperty]
		public Users User { get; set; }

		public IActionResult OnPostDelete()
		{
			// Вызываем метод Delete из экземпляра dbUsers
			int result = bUsers.Delete(User);

			// Перенаправляем пользователя обратно на страницу, где отображаются данные
			return RedirectToPage();
		}
		public IActionResult OnPostEdit()
		{


			int userId = int.Parse(Request.Form["id"]);
			string newFIO = Request.Form["FIO"];
			string newEmail = Request.Form["Email"];
			string newFilial = Request.Form["Filial"];

			Users userToUpdate = new Users()
			{
				id = userId,
				FIO = newFIO,
				Email = newEmail,
				Filial = newFilial
			};

			int result = bUsers.Edit(userToUpdate);






			return RedirectToPage();
		}
	}
}
