using inventory.Interfaces;
using inventory.Models;
using inventory.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MySqlX.XDevAPI.Common;

namespace inventory.Pages
{
	public class LoginModel : PageModel
	{
		public static List<Profile> ProfileItems = new List<Profile>();
		[TempData]
		public string ErrorMessage { get; set; }

		public void OnGet()
		{

			


		}
		public IActionResult OnPost(string login, string password)
		{
			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
			{
				TempData["ErrorMessage"] = "Введите логин и пароль";
				return RedirectToPage();
			}

			string query = "SELECT Patronymic, LastName, Email, FirstName, Login, Role FROM Profile WHERE Login = @Login AND Password = @Password";
			List<MySqlParameter> parameters = new List<MySqlParameter>
			{
		new MySqlParameter("@Login", login),
		new MySqlParameter("@Password", password)
	};

			using (MySqlDataReader reader = Connection.SqlConnection(query, parameters))
			{
				if (reader.Read())
				{
					// Check the user's role
					string role = reader["Role"].ToString();
					string name = reader["FirstName"].ToString();
					string login1 = reader["Login"].ToString();
					string email = reader["Email"].ToString();
					string patronymic = reader["Patronymic"].ToString();
					string lastName = reader["LastName"].ToString();

					HttpContext.Session.SetString("UserRole", role);
					HttpContext.Session.SetString("FirstName", name);
					HttpContext.Session.SetString("Login", login1);
					HttpContext.Session.SetString("Email", email);
					HttpContext.Session.SetString("Patronymic", patronymic);
					HttpContext.Session.SetString("LastName", lastName);



					if (role == "Администратор")
					{
						// Redirect to admin page
						return RedirectToPage("Index");
					}
					else
					{
						// Redirect to a regular user page
						return RedirectToPage("Index");
					}
				}
				else
				{
					TempData["ErrorMessage"] = "Неправильный логин или пароль";
					Console.WriteLine(TempData["ErrorMessage"]);
					return RedirectToPage();
				}
			}

		}
	}
}