using inventory.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory.Interfaces
{
	public interface IInventory
	{
		public IEnumerable<Inventory> AllInventory { get; }

		public int Add(Inventory inventories);

		public int Edit(Inventory inventories);

		public int Delete(Inventory inventories);
	}
}
