using System.Data;
using inventory.Interfaces;
using inventory.Models;
using inventory.ViewModel;
using MySql.Data.MySqlClient;

namespace inventory
{

	public class DBHistory
	{
		public IEnumerable<Equipment> Equipment = new DBEquipment().AllEquipment;
		public string UserRole { get; set; }
		public IEnumerable<History> AllHistory(string FirstName, string LastName, string Patronymic, string UserRole)
		{

				List<History> history = new List<History>();

				
				MySqlDataReader HistoryData = Connection.SqlConnection("SELECT * FROM DataBase.History");
			while (HistoryData.Read())
			{
				string molColumnValue = HistoryData.GetString(3);
				string[] molArray = molColumnValue.Split(' ', 3);




				if (UserRole == "Администратор")
				{

					history.Add(new History()
					{
						id = HistoryData.GetInt32(0),
						Name = HistoryData.GetString(1),
						InvNum = HistoryData.GetString(2),
						mol = HistoryData.GetString(3),
						mol1 = HistoryData.GetString(4),
						Filial = HistoryData.GetString(5),
						Date = HistoryData.GetString(6),
						Status = HistoryData.GetString(7),
					});

					




				}
				else
				{
					if (molArray.Length == 3 &&
						molArray[0] != LastName &&
						molArray[1] != FirstName &&
						molArray[2] != Patronymic)
					{
						history.Add(new History()
						{
							id = HistoryData.GetInt32(0),
							Name = HistoryData.GetString(1),
							InvNum = HistoryData.GetString(2),
							mol = HistoryData.GetString(3),
							mol1 = HistoryData.GetString(4),
							Filial = HistoryData.GetString(5),
							Date = HistoryData.GetString(6),
							Status = HistoryData.GetString(7),
						});

					}


				}
				
				


			}
			HistoryData.Close();
			return history;
		}
		public int Success(History history)
		{
			DateTime currentDate = DateTime.Now;


			MySqlDataReader HistoryData = Connection.SqlConnection(
				$"update History set  Date = '{currentDate.ToString("d")}', Status = 'Принято' where id = {history.id};");

			MySqlDataReader HistoryData1 = Connection.SqlConnection(
				$"update Equipment set  filial = '{history.Filial}', mol = '{history.mol1}' where invnum = '{history.InvNum}';");
			int IdUser = -1;
			HistoryData.Close();
			HistoryData1.Close();
			return IdUser;
		}
		public int Danger(History history)
		{
			DateTime currentDate = DateTime.Now;


			MySqlDataReader HistoryData = Connection.SqlConnection(
				$"update History set  Date = '{currentDate.ToString("d")}', Status = 'Отклонено' where id = {history.id};");
			int IdUser = -1;
			HistoryData.Close();
			return IdUser;
		}
	}
}
