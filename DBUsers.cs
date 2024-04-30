using inventory.Models;
using MySql.Data.MySqlClient;


using inventory.Interfaces;

using System.Collections.Generic;
using System.Linq;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace inventory
{
    public class DBUsers : IUsers
	{
		public IEnumerable<Equipment> Equipment = new DBEquipment().AllEquipment;
		public IEnumerable<Filial> Filial = new DBFilial().AllFilial;

		public IEnumerable<Users> AllUsers
        {
            get
            {
				List<Users> users = new List<Users>();



				MySqlDataReader UsersData = Connection.SqlConnection("SELECT * FROM DataBase.Profile");

				while (UsersData.Read())
				{

					users.Add(new Users()
					{

						id = UsersData.GetInt32(0),
						LastName = UsersData.GetString(1),
						FirstName = UsersData.GetString(2),
						Patronymic = UsersData.GetString(3),
						Branch = UsersData.GetString(4),
						Email = UsersData.GetString(5),
						PhoneNumber = UsersData.GetString(6),
						Login = UsersData.GetString(7),
						Password = UsersData.GetString(8),
						Role = UsersData.GetString(9)
					});


				}
				UsersData.Close();

				return users;
				
			}
			
		}
		public int Add(Users user) 
		{

			MySqlDataReader UsersData = Connection.SqlConnection(
				$"insert into Profile (LastName, FirstName, Patronymic, Branch, Email, PhoneNumber, Login, Password, Role) values ('{user.LastName}','{user.FirstName}','{user.Patronymic}','{user.Branch}','{user.Email}','{user.PhoneNumber}','{user.Login}','{user.Password}','{user.Role}');");

			int IdUser = -1;

			UsersData.Close();
			return IdUser;
		}
		public int Edit(Users user)
		{
			MySqlDataReader UsersData = Connection.SqlConnection(
				$"update Profile set LastName = '{user.LastName}', FirstName = '{user.FirstName}', Patronymic = '{user.Patronymic}', Branch = '{user.Branch}', Email = '{user.Email}', PhoneNumber = '{user.PhoneNumber}', Login = '{user.Login}', Password = '{user.Password}', Role = '{user.Role}' where id = {user.id};");

			int IdUser = -1;
			UsersData.Close();
			return IdUser;
		}

		public int Delete(Users user)
		{

			MySqlDataReader UsersData = Connection.SqlConnection(
				$"Delete from Profile where id = {user.id}");
			UsersData.Close();
			return -1;
		}
	}
}
