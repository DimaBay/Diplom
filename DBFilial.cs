using inventory.Interfaces;
using inventory.Models;
using inventory.ViewModel;
using MySql.Data.MySqlClient;

namespace inventory
{
	public class DBFilial : IFilial
	{
		public IEnumerable<Filial> AllFilial
		{
			get
			{

				List<Filial> filial = new List<Filial>();


				MySqlDataReader FilialData = Connection.SqlConnection("SELECT * FROM DataBase.Filial");

				while (FilialData.Read())
				{
					filial.Add(new Filial()

					{
						id = FilialData.GetInt32(0),
						Name = FilialData.GetString(1),
						Address = FilialData.GetString(2),

					});

				}
				FilialData.Close();
				return filial;
			}
		}
		public int Add(Filial filial)
		{

			MySqlDataReader FilialData = Connection.SqlConnection(
				$"insert into Filial (Name, Address) values ('{filial.Name}','{filial.Address}');");

			int IdUser = -1;

			FilialData.Close();
			return IdUser;
		}
		public int Edit(Filial filial)
		{
			MySqlDataReader FilialData = Connection.SqlConnection(
				$"update Filial set Name = '{filial.Name}', Address = '{filial.Address}' where id = {filial.id};");

			int IdUser = -1;
			FilialData.Close();
			return IdUser;
		}

		public int Delete(Filial filial)
		{

			MySqlDataReader FilialData = Connection.SqlConnection(
				$"Delete from Filial where id = {filial.id}");
			FilialData.Close();
			return -1;
		}
	}
}
