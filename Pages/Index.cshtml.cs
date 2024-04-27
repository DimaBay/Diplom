using inventory.Interfaces;
using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory.Pages
{
    public class IndexModel : PageModel
    {
		[TempData]
		public string UserRole { get; set; }
		public string FirstName { get; set; }
		public DBFilial bFilial = new DBFilial();
		public DBUsers bUsers = new DBUsers();
		public DBEquipment bEquipment = new DBEquipment();
		public List<Filial> filials = new List<Filial>();
		public List<Users> users = new List<Users>();
		public List<Equipment> equipment = new List<Equipment>();
		public Equipment newEquipment = new Equipment();
		
		public DBProfile bProfile = new DBProfile();

		public List<Profile> profiles = new List<Profile>();

		public Profile newProfile = new Profile();
		public string Email { get; set; }
		public string Login { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		

		public void OnGet()
		{
			Login = HttpContext.Session.GetString("Login");
			profiles = bProfile.AllProfile(Login).ToList();


			users = bUsers.AllUsers.ToList();
			filials = bFilial.AllFilial.ToList();
			equipment = bEquipment.AllEquipment.ToList();

			UserRole = HttpContext.Session.GetString("UserRole");
			FirstName = HttpContext.Session.GetString("FirstName");
		
			Email = HttpContext.Session.GetString("Email");
			


		}
		public IActionResult OnPost()
		{



				newEquipment.name = Request.Form["name"];
				newEquipment.invnum = Request.Form["invnum"];
				newEquipment.mol = Request.Form["mol"];

				newEquipment.personalNum = Request.Form["personalNum"];
				newEquipment.type = Request.Form["type"];
				newEquipment.date = Request.Form["date"];
				newEquipment.price = Request.Form["price"];
				
				newEquipment.filial = Request.Form["filial"];

				bEquipment.Add(newEquipment);
				
				
				equipment = bEquipment.AllEquipment.ToList();
				filials = bFilial.AllFilial.ToList();
				users = bUsers.AllUsers.ToList();


				return RedirectToPage(); // Перенаправляем пользователя обратно на эту же страницу
			

			return Page();

		}
		[BindProperty]
		public Equipment equipments { get; set; }

		public IActionResult OnPostDelete()
		{
			// Вызываем метод Delete из экземпляра dbUsers
			int result = bEquipment.Delete(equipments);

			// Перенаправляем пользователя обратно на страницу, где отображаются данные
			return RedirectToPage();
		}
		public IActionResult OnPostEdit()
		{


			int EquipmentId = int.Parse(Request.Form["Id"]); 
			string newpersonalNum = Request.Form["personalNum"];
			string newname = Request.Form["name"];
			string newtype = Request.Form["type"];
			string newinvnum = Request.Form["invnum"];
			string newdate = Request.Form["date"];
			string newprice = Request.Form["price"];
			string newQR = Request.Form["QR"];
			string newfilial = Request.Form["filial"];
			string newmol = Request.Form["mol"];

			Equipment EquipmentToUpdate = new Equipment()
			{
				Id = EquipmentId,
				personalNum = newpersonalNum,
				name = newname,
				type = newtype,
				invnum = newinvnum,
				date = newdate,
				price = newprice,
				QR = newQR,
				filial = newfilial,
				mol = newmol
			};

			int result = bEquipment.Edit(EquipmentToUpdate);






			return RedirectToPage();
		}
	
		public IActionResult OnPostMoving()
		{
			

			int count = 0;
			
			LastName = HttpContext.Session.GetString("LastName");
			FirstName = HttpContext.Session.GetString("FirstName");
			Patronymic = HttpContext.Session.GetString("Patronymic");
			Email = HttpContext.Session.GetString("Email");



			int EquipmentId = int.Parse(Request.Form["Id"]);
			string newpersonalNum = Request.Form["personalNum"];
			string newname = Request.Form["name"];
			string newtype = Request.Form["type"];
			string newinvnum = Request.Form["invnum"];
			string newdate = Request.Form["date"];
			string newprice = Request.Form["price"];
			string newQR = Request.Form["QR"];
			string newfilial = Request.Form["filial"];
			string newmol = Request.Form["mol"];
			string newmol1 = Request.Form["mol1"];

			
			Equipment EquipmentToUpdate = new Equipment()
			{

				
				


			Id = EquipmentId,
				mol = newmol,
				mol1 = newmol1,
				filial = newfilial,
				name = newname,
				invnum = newinvnum
			};
			

			bEquipment.Moving(EquipmentToUpdate, FirstName, LastName, Patronymic, count, Login, UserRole, Email);




			if (bEquipment.error != null)
			{
				TempData["ErrorMessage"] = $"{bEquipment.error}";
				
			}
			if (bEquipment.success != null)
			{
				TempData["ErrorMessage"] = $"{bEquipment.success}";
				
			}
			return RedirectToPage();
		}
		
		

		
	}
}