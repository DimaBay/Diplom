using inventory.Interfaces;
using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory.Pages
{
    public class inventoryModel : PageModel
    {
		public DBFilial bFilial = new DBFilial();
		public DBUsers bUsers = new DBUsers();
		public DBEquipment bEquipment = new DBEquipment();
		public DBInventory bInventory = new DBInventory();
		public List<Users> users = new List<Users>();
		public List<Filial> filials = new List<Filial>();
		public List<Equipment> equipment = new List<Equipment>();
		public List<Inventory> inventory = new List<Inventory>();
		public Inventory newInventory = new Inventory();
		public DBProfile bProfile = new DBProfile();

		public List<Profile> profiles = new List<Profile>();

		public Profile newProfile = new Profile();
		public string Email { get; set; }
		public string Login { get; set; }
		public string UserRole { get; set; }
		public string FirstName { get; set; }

		public void OnGet()
		{
			Login = HttpContext.Session.GetString("Login");
			profiles = bProfile.AllProfile(Login).ToList();
			filials = bFilial.AllFilial.ToList();
			users = bUsers.AllUsers.ToList();
			equipment = bEquipment.AllEquipment.ToList();
			inventory = bInventory.AllInventory.ToList();
			UserRole = HttpContext.Session.GetString("UserRole");
			FirstName = HttpContext.Session.GetString("FirstName");
			Email = HttpContext.Session.GetString("Email");
		}

		public IActionResult OnPost()
		{




			newInventory.mol = Request.Form["mol"];
			newInventory.Filial = Request.Form["Filial"];
			newInventory.Equipment = Request.Form["Equipment"];
			bInventory.Add(newInventory);

			filials = bFilial.AllFilial.ToList();
			users = bUsers.AllUsers.ToList();
			equipment = bEquipment.AllEquipment.ToList();
			inventory = bInventory.AllInventory.ToList();



			return RedirectToPage(); // Перенаправляем пользователя обратно на эту же страницу


			return Page();

		}
		[BindProperty]
		public Inventory Inventorys { get; set; }

		public IActionResult OnPostDelete()
		{
			// Вызываем метод Delete из экземпляра dbUsers
			int result = bInventory.Delete(Inventorys);

			// Перенаправляем пользователя обратно на страницу, где отображаются данные
			return RedirectToPage();
		}
		public IActionResult OnPostEdit()
		{


			int inventoryId = int.Parse(Request.Form["id"]);
			string newmol = Request.Form["mol"];
			string newFilial = Request.Form["Filial"];
			string newEquipment = Request.Form["Equipment"];

			Inventory inventoryToUpdate = new Inventory()
			{
				id = inventoryId,
				mol = newmol,
				Filial = newFilial,
				Equipment = newEquipment
			};

			int result = bInventory.Edit(inventoryToUpdate);






			return RedirectToPage();
		}
	}
}
