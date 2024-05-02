using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory.Pages
{
    public class equipmentTypeModel : PageModel
    {
		public DBFilial bFilial = new DBFilial();
		public DBProfile bProfile = new DBProfile();
		public DBTypeEquipment bTypeEquipment = new DBTypeEquipment();

		public List<Profile> profiles = new List<Profile>();
		public List<Filial> filials = new List<Filial>();

		public List<TypeEquipment> equipmentTypeModels = new List<TypeEquipment>();

		public Profile newProfile = new Profile();
		public string UserRole { get; set; }
		public string FirstName { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
	
		

		public Filial newFilial = new Filial();
		
		
		public TypeEquipment newequipmentTypeModels = new TypeEquipment();
		public void OnGet()
        {

			Login = HttpContext.Session.GetString("Login");
			profiles = bProfile.AllProfile(Login).ToList();
			equipmentTypeModels = bTypeEquipment.AllTypeEquipment.ToList();
			UserRole = HttpContext.Session.GetString("UserRole");
			FirstName = HttpContext.Session.GetString("FirstName");
			Email = HttpContext.Session.GetString("Email");


		}
		public IActionResult OnPost()
		{




			newequipmentTypeModels.Type = Request.Form["LastName"];
			

			bTypeEquipment.Add(newequipmentTypeModels);
			equipmentTypeModels = bTypeEquipment.AllTypeEquipment.ToList();




			return RedirectToPage(); // Перенаправляем пользователя обратно на эту же страницу


			return Page();

		}
		[BindProperty]
		public TypeEquipment typeEquipment { get; set; }

		public IActionResult OnPostDelete()
		{

			int result = bTypeEquipment.Delete(typeEquipment);


			return RedirectToPage();
		}
		public IActionResult OnPostEdit()
		{


			int userId = int.Parse(Request.Form["id"]);
			string newType = Request.Form["LastName"];



			TypeEquipment typeEquipmentToUpdate = new TypeEquipment()
			{
				Id = userId,
				Type = newType
			};

			int result = bTypeEquipment.Edit(typeEquipmentToUpdate);






			return RedirectToPage();
		}
	}
}
