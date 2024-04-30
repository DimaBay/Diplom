using inventory.Interfaces;
using inventory.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace inventory
{
    public class DBProfile
	{

       
        public DbSet<Profile> Profiles { get; set; }

        public IEnumerable<Profile> AllProfile(string login)
		{
            
            
                List<Profile> profiles = new List<Profile>();

            MySqlDataReader ProfileData = Connection.SqlConnection($"SELECT * FROM DataBase.Profile WHERE Login = '{login}'");
                
                    while (ProfileData.Read())
                    {
                        
                profiles.Add(new Profile()
                        {
                            Id = ProfileData.GetInt32(0),
                            LastName = ProfileData.GetString(1),
                            FirstName = ProfileData.GetString(2),
                            Patronymic = ProfileData.GetString(3),
                            Branch = ProfileData.GetString(4),
                            Email = ProfileData.GetString(5),
                            PhoneNumber = ProfileData.GetString(6),
                            Login = ProfileData.GetString(7),
                            Password = ProfileData.GetString(8),
                            Role = ProfileData.GetString(9),
                            ImageData = ProfileData.GetFieldValue<byte[]>(10),
                        });
                    }

			ProfileData.Close();

				return profiles;
            
        }

      
        public int Add(Profile profile)
        {
            MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3302;database=DataBase;uid=root;pwd=;");
            
                connection.Open();

                string updateQuery = @"UPDATE Profile SET LastName = @LastName, FirstName = @FirstName, Patronymic = @Patronymic, Branch = @Branch, Email = @Email, PhoneNumber = @PhoneNumber, ImageData = @ImageData  WHERE id = @Id";

            MySqlCommand command = new MySqlCommand(updateQuery, connection);
                
                    command.Parameters.AddWithValue("@LastName", profile.LastName);
                    command.Parameters.AddWithValue("@FirstName", profile.FirstName);
                    command.Parameters.AddWithValue("@Patronymic", profile.Patronymic);
                    command.Parameters.AddWithValue("@Branch", profile.Branch);
                    command.Parameters.AddWithValue("@Email", profile.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", profile.PhoneNumber);
                    command.Parameters.AddWithValue("@ImageData", profile.ImageData);
                    command.Parameters.AddWithValue("@Id", profile.Id);

                    int affectedRows = command.ExecuteNonQuery();
					
			        connection.Close();
			        return affectedRows;

			

			/*
						List<MySqlParameter> parameters = new List<MySqlParameter>
				{
					new MySqlParameter("@ImageData", MySqlDbType.Blob) { Value = profile.ImageData },
					// Add other parameters as needed
				};

						Connection.SqlConnection($"UPDATE Profile SET LastName = '{profile.LastName}', FirstName = '{profile.FirstName}', Patronymic = '{profile.Patronymic}', Branch = '{profile.Branch}', Email = '{profile.Email}', PhoneNumber = '{profile.PhoneNumber}', ImageData = @ImageData WHERE id = {profile.Id};", parameters);

						int IdUser = -1;
						return IdUser;
					}*/
		}
    }
}
