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

				

                MySqlDataReader UsersData = Connection.SqlConnection("SELECT * FROM DataBase.Users");

                while (UsersData.Read())
                {
					
					users.Add(new Users()
					{
						id = UsersData.GetInt32(0),
						FIO = UsersData.GetString(1),
						Filial = UsersData.GetString(2),
						Email = UsersData.GetString(3),
					});
					

				}
				UsersData.Close();

				return users;
				
			}
			
		}
		public int Add(Users user) 
		{

			MySqlDataReader UsersData = Connection.SqlConnection(
				$"insert into Users (FIO, Filial, Email) values ('{user.FIO}','{user.Filial}','{user.Email}');");

			int IdUser = -1;

			UsersData.Close();
			return IdUser;
		}
		public int Edit(Users user)
		{
			MySqlDataReader UsersData = Connection.SqlConnection(
				$"update Users set FIO = '{user.FIO}', Filial = '{user.Filial}', Email = '{user.Email}' where id = {user.id};");

			int IdUser = -1;
			UsersData.Close();
			return IdUser;
		}

		public int Delete(Users user)
		{

			MySqlDataReader UsersData = Connection.SqlConnection(
				$"Delete from Users where id = {user.id}");
			UsersData.Close();
			return -1;
		}
	}
}
