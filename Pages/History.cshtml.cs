using inventory.Interfaces;
using inventory.Models;
using inventory.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory.Pages
{
    public class historyModel : PageModel
    {
		public DBHistory bHistory = new DBHistory();

		public List<History> histories = new List<History>();

		public History newHistory = new History();

		public DBProfile bProfile = new DBProfile();

		public List<Profile> profiles = new List<Profile>();

		public Profile newProfile = new Profile();

		public DBEquipment bEquipment = new DBEquipment();
		public List<Equipment> equipment = new List<Equipment>();
		public Equipment newEquipment = new Equipment();

		public string UserRole { get; set; }
		public string FirstName { get; set; }
		public string Login { get; set; }
		public string patronymic { get; set; }
		public string lastName { get; set; }
		public string Email { get; set; }

		public void OnGet()
        {
			Login = HttpContext.Session.GetString("Login");
			profiles = bProfile.AllProfile(Login).ToList();
			patronymic = HttpContext.Session.GetString("Patronymic");
			lastName = HttpContext.Session.GetString("LastName");
			FirstName = HttpContext.Session.GetString("FirstName");
			UserRole = HttpContext.Session.GetString("UserRole");
			histories = bHistory.AllHistory(FirstName, lastName, patronymic, UserRole).ToList();
			equipment = bEquipment.AllEquipment.ToList();
						UserRole = HttpContext.Session.GetString("UserRole");
			
			Email = HttpContext.Session.GetString("Email");
		}

		[BindProperty]
		public History history { get; set; }
		public IActionResult OnPostSuccess()
		{
			int EquipmentId = int.Parse(Request.Form["Id"]);
			string newmol1 = Request.Form["mol1"];
			string newFilial = Request.Form["Filial"];
			string newInvNum = Request.Form["InvNum"];


			bHistory.Success(history);






			return RedirectToPage();


		}
		public IActionResult OnPostDanger()
		{

			bHistory.Danger(history);






			return RedirectToPage();

		}
	}
}
