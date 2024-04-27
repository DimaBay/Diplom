using inventory.Models;

namespace inventory.Interfaces
{
	public interface IEquipment
	{
		public IEnumerable<Equipment> AllEquipment { get; }

		public int Add(Equipment equipment);

		public int Edit(Equipment equipment);

		public int Delete(Equipment equipment);

	}
}