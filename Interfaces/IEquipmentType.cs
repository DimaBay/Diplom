using inventory.Models;

namespace inventory.Interfaces
{
	public interface IEquipmentType
	{
		public IEnumerable<TypeEquipment> AllTypeEquipment { get; }

		public int Add(TypeEquipment typeEquipment);

		public int Edit(TypeEquipment typeEquipment);

		public int Delete(TypeEquipment typeEquipment);
	}
}
