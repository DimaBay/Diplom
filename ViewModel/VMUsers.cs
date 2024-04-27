using inventory.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory.ViewModel
{
    public class VMUsers : PageModel
    {
		

		public IEnumerable<Users> Users = new DBUsers().AllUsers;

		public IEnumerable<Equipment> Equipment = new DBEquipment().AllEquipment;


		public IEnumerable<Filial> Filial = new DBFilial().AllFilial;



	}
}
