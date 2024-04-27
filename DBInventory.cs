using inventory.Interfaces;
using inventory.Models;
using MySql.Data.MySqlClient;

namespace inventory
{
	public class DBInventory : IInventory
	{
		public IEnumerable<Equipment> Equipment = new DBEquipment().AllEquipment;
		public IEnumerable<Filial> Filial = new DBFilial().AllFilial;
		public IEnumerable<Users> User = new DBUsers().AllUsers;

		public IEnumerable<Inventory> AllInventory
		{
			get
			{
				List<Inventory> inventories = new List<Inventory>();



				MySqlDataReader inventoriesData = Connection.SqlConnection("SELECT * FROM DataBase.Inventory");

				while (inventoriesData.Read())
				{

					inventories.Add(new Inventory()
					{
						id = inventoriesData.GetInt32(0),
						mol = inventoriesData.GetString(1),
						Filial = inventoriesData.GetString(2),
						Equipment = inventoriesData.GetString(3),
					});


				}
				inventoriesData.Close();	
				return inventories;

			}


		}
		public int Add(Inventory inventories)
		{

			MySqlDataReader inventoriesData = Connection.SqlConnection(
				$"insert into Inventory (mol, Filial, Equipment) values ('{inventories.mol}','{inventories.Filial}','{inventories.Equipment}');");

			int inventoryId = -1;

			inventoriesData.Close();
			return inventoryId;
		}
		public int Edit(Inventory inventories)
		{
			MySqlDataReader inventoriesData = Connection.SqlConnection(
				$"update Inventory set mol = '{inventories.mol}', Filial = '{inventories.Filial}', Equipment = '{inventories.Equipment}' where id = {inventories.id};");

			int inventoryId = -1;
			inventoriesData.Close();
			return inventoryId;
		}

		public int Delete(Inventory inventories)
		{

			MySqlDataReader inventoriesData = Connection.SqlConnection(
				$"Delete from Inventory where id = {inventories.id}");
			inventoriesData.Close();
			return -1;
		}
	}
}
