using inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace inventory.Pages
{
	public class profileEditModel : PageModel
	{
		private readonly MySqlConnection _dbContext;
		public DBProfile bProfile = new DBProfile();
		public List<Profile> profiles = new List<Profile>();
		public Profile newProfile = new Profile();
		public string UserRole { get; set; }
		public string FirstName { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public profileEditModel()
		{
			// Adjust the connection string accordingly
			string connectionString = "server=127.0.0.1;port=3302;database=DataBase;uid=root;pwd=;";
			_dbContext = new MySqlConnection(connectionString);
		}

		[BindProperty]
		public IFormFile ImageFile { get; set; }

		public void OnGet()
		{
			Login = HttpContext.Session.GetString("Login");
			profiles = bProfile.AllProfile(Login).ToList();
			UserRole = HttpContext.Session.GetString("UserRole");
			FirstName = HttpContext.Session.GetString("FirstName");
			Email = HttpContext.Session.GetString("Email");
		}

		public IActionResult OnPostAdd()
		{
			

			newProfile.LastName = Request.Form["LastName"];
			newProfile.FirstName = Request.Form["FirstName"];
			newProfile.Patronymic = Request.Form["Patronymic"];
			newProfile.Branch = Request.Form["Branch"];
			newProfile.Email = Request.Form["Email"];
			newProfile.PhoneNumber = Request.Form["PhoneNumber"];

			bProfile.Add(newProfile);
			
			profiles = bProfile.AllProfile(Login).ToList();

			try
			{
				_dbContext.Open();

				if (ImageFile != null && ImageFile.Length > 0)
				{
					using (var stream = new MemoryStream())
					{
						ImageFile.CopyToAsync(stream);
						newProfile.ImageData = stream.ToArray();
					}
					Console.WriteLine($"ImageData Length: {newProfile.ImageData.Length}");

					string updateQuery = $"UPDATE Profile SET ImageData = @ImageData WHERE Id = {newProfile.Id}";
					using (var command = new MySqlCommand(updateQuery, _dbContext))
					{
						command.Parameters.Add("@ImageData", MySqlDbType.Blob).Value = newProfile.ImageData;

						// Execute the query
						command.ExecuteNonQuery();
						Console.WriteLine("Data updated successfully.");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Exception: {ex.Message}");
				// Handle exceptions, log them, or display an error message
				return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
			}
			finally
			{
				Connection.MySqlClose(_dbContext);
			}

			return RedirectToPage("/profile");
		}
	}
}
