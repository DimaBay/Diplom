using inventory.Models;

namespace inventory.Interfaces
{
	public interface IProfile
	{
		public IEnumerable<Profile> AllProfile { get; }

		public int Add(Profile profile);

		
	}
}
