using inventory.Interfaces;
using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory.Pages
{
    public class filialsModel : PageModel
    {
		public DBFilial bFilial = new DBFilial();
		public DBProfile bProfile = new DBProfile();

		public List<Profile> profiles = new List<Profile>();

		public Profile newProfile = new Profile();
		public string UserRole { get; set; }
		public string FirstName { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public List<Filial> filials = new List<Filial>();
		
		public Filial newFilial = new Filial();
		

		public void OnGet()
		{
			Login = HttpContext.Session.GetString("Login");
			profiles = bProfile.AllProfile(Login).ToList();
			filials = bFilial.AllFilial.ToList();
			UserRole = HttpContext.Session.GetString("UserRole");
			FirstName = HttpContext.Session.GetString("FirstName");
			Email = HttpContext.Session.GetString("Email");
		}

		public IActionResult OnPost()
		{




			newFilial.Name = Request.Form["Name"];
			newFilial.Address = Request.Form["Address"];

			bFilial.Add(newFilial);
			filials = bFilial.AllFilial.ToList();
			



			return RedirectToPage(); // Перенаправляем пользователя обратно на эту же страницу


			return Page();

		}
		[BindProperty]
		public Filial filial { get; set; }

		public IActionResult OnPostDelete()
		{
			
			int result = bFilial.Delete(filial);

			
			return RedirectToPage();
		}
		public IActionResult OnPostEdit()
		{


			int userId = int.Parse(Request.Form["id"]);
			string newName = Request.Form["Name"];
			string newAddress = Request.Form["Address"];
			

			Filial filialToUpdate = new Filial()
			{
				id = userId,
				Name = newName,
				Address = newAddress
			};

			int result = bFilial.Edit(filialToUpdate);






			return RedirectToPage();
		}
	}
}
