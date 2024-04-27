using inventory.Models;

namespace inventory.Interfaces
{
	public interface IFilial
	{
		public IEnumerable<Filial> AllFilial { get; }

		public int Add(Filial filial);

		public int Edit(Filial filial);

		public int Delete(Filial filial);
	}
}
