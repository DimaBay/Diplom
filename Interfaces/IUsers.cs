using inventory.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inventory.Interfaces
{
	public interface IUsers
	{
		public IEnumerable<Users> AllUsers { get; }

		public int Add(Users user);

		public int Edit(Users user);

		public int Delete(Users user);
	}
}
