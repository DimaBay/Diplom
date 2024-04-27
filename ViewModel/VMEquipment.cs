using inventory.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory.ViewModel
{
	public class VMEquipment : PageModel
	{
		public IEnumerable<Equipment> Equipment = new DBEquipment().AllEquipment;
	}
}
