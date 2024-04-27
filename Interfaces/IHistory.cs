using inventory.Models;

namespace inventory.Interfaces
{
	public interface IHistory
	{
		public IEnumerable<History> AllHistory { get; }
		public int Success(History history);
		public int Danger(History history);
	}
}
