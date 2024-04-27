using inventory.Interfaces;
using inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using inventory.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using Org.BouncyCastle.Crypto.Macs;
/*using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;*/

namespace inventory
{
	public class DBEquipment : IEquipment
	{

		List<Email1> email = new List<Email1>();
		public string error;
		public string success;
		public IEnumerable<Equipment> AllEquipment

		{
			get
			{




				List<Equipment> equipment = new List<Equipment>();


				MySqlDataReader EquipmentData = Connection.SqlConnection("SELECT * FROM DataBase.Equipment");


				while (EquipmentData.Read())
				{
					equipment.Add(new Equipment()
					{
						Id = EquipmentData.GetInt32(0),
						personalNum = EquipmentData.GetString(1),
						name = EquipmentData.GetString(2),
						type = EquipmentData.GetString(3),
						invnum = EquipmentData.GetString(4),
						date = EquipmentData.GetString(5),
						price = EquipmentData.GetString(6),
						QR = EquipmentData.GetString(7),
						filial = EquipmentData.GetString(8),
						mol = EquipmentData.GetString(9)
					});


				}
				EquipmentData.Close();
				return equipment;

			}
		}

		public int Add(Equipment equipment)
		{

			var random = new Random();
			string personalNum = string.Empty;
			for (int i = 0; i < 10; i++)
			{
				personalNum = String.Concat(personalNum, random.Next(10).ToString());
			}







			MySqlDataReader EquipmentData = Connection.SqlConnection(
				$"INSERT INTO Equipment (personalNum, name, type, invnum, date, price, QR, filial, mol) VALUES ('{personalNum}', '{equipment.name}', '{equipment.type}', '{equipment.invnum}', '{equipment.date}', '{equipment.price}', '{"QRcode"}', '{equipment.filial}', '{equipment.mol}');");

			int IdEquipment = -1;

			EquipmentData.Close();
			return IdEquipment;
		}


		public int Edit(Equipment equipment)
		{

			var random = new Random();
			string personalNum = string.Empty;
			for (int i = 0; i < 10; i++)
				personalNum = String.Concat(personalNum, random.Next(10).ToString());

			MySqlDataReader EquipmentData = Connection.SqlConnection(
				$"update Equipment set personalNum = '{personalNum}', name = '{equipment.name}', type = '{equipment.type}', invnum = '{equipment.invnum}', date = '{equipment.date}', price = '{equipment.price}', QR = 'QRcode', filial = '{equipment.filial}' , mol = '{equipment.mol}' where id = {equipment.Id};");

			int IdEquipment = -1;
			EquipmentData.Close();
			return IdEquipment;
		}

		public int Delete(Equipment equipment)
		{

			MySqlDataReader EquipmentData = Connection.SqlConnection(
				$"Delete from Equipment where id = {equipment.Id}");
			EquipmentData.Close();
			return -1;
		}

		public int Moving(Equipment equipment, string FirstName, string LastName, string Patronymic, int count, string Login, string Role, string Email)
		{
			MySqlDataReader EquipmentData = Connection.SqlConnection($"SELECT * FROM DataBase.History where InvNum = '{equipment.invnum}' AND Status = 'Ожидает'");



			count = 0;
			DateTime currentDate = DateTime.Now;


			List<Equipment> equipment1 = new List<Equipment>();

			string molColumnValue = equipment.mol;
			string[] molArray = molColumnValue.Split(' ', 3);

			string molColumnValue1 = equipment.mol1;
			string[] molArray1 = molColumnValue1.Split(' ', 3);

			MySqlDataReader EquipmentData1 = Connection.SqlConnection($"SELECT * FROM DataBase.Profile where LastName = '{molArray1[0]}' AND FirstName = '{molArray1[1]}' AND Patronymic = '{molArray1[2]}'");


			if (Role == "Администратор")
			{
				if (molArray1.Length == 3 &&
				molArray1[0] != LastName &&
				molArray1[1] != FirstName &&
				molArray1[2] != Patronymic)
				{
					if (EquipmentData.HasRows == false)
					{
						// Все 3 слова совпадают
						success = "Успешно перемещено!";







						Connection.SqlConnection(
						$"insert into History (Name,InvNum,mol,mol1,Filial,Date,Status) values ('{equipment.name}','{equipment.invnum}','{equipment.mol}','{equipment.mol1}','{equipment.filial}','{currentDate.ToString("d")}','Ожидает');");

						while (EquipmentData1.Read())
						{
							email.Add(new Email1()
							{
								email1 = EquipmentData1.GetString(5)
							});


						}
						/*SendEmail(email.ToString(), "Успешное перемещение", "Оборудование успешно перемещено!");*/



					}
					else
					{
						error = "Оборудование ожидает подтверждения!";
					}

				}
				else
				{
					error = "Нелья перемещать оборудование самому себе!";

				}
			}




			if (Role == "Пользователь")
			{
				if (molArray.Length == 3 &&
					molArray[0] == LastName &&
					molArray[1] == FirstName &&
					molArray[2] == Patronymic)
				{
					if (molArray1.Length == 3 &&
					molArray1[0] != LastName &&
					molArray1[1] != FirstName &&
					molArray1[2] != Patronymic)
					{
						if (EquipmentData.HasRows == false)
						{
							// Все 3 слова совпадают
							success = "Успешно перемещено!";

							Connection.SqlConnection(
							$"insert into History (Name,InvNum,mol,mol1,Filial,Date,Status) values ('{equipment.name}','{equipment.invnum}','{equipment.mol}','{equipment.mol1}','{equipment.filial}','{currentDate.ToString("d")}','Ожидает');");

							/*SendEmail("dironthebest@gmail.com", "Успешное перемещение", "Оборудование успешно перемещено!");*/

						}
						else
						{
							error = "Оборудование ожидает подтверждения!";
						}
					}
					else
					{
						error = "Нелья перемещать оборудование самому себе!";

					}
				}
				else
				{
					error = "Нелья перемещать чужое оборудование!";
				}
			}
			EquipmentData.Close();
			return -1;

		}
		/*public Task SendEmail(string toEmail, string subject, string body)
		{


			




*//*

			var client = new SmtpClient("smtp.yandex.ru", 465)
			{
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("email", "pas")
			};

			return client.SendMailAsync(
				new MailMessage(from: "email",
								to: "email",
								subject,
								body
								));
			}

*/










		/*using var emailMessage = new MimeMessage();

			emailMessage.From.Add(new MailboxAddress("Администрация сайта", "admin@mycompany.com"));
			emailMessage.To.Add(new MailboxAddress("", "email"));
			emailMessage.Subject = subject;
			emailMessage.Body = new TextPart(body);

			using (var client = new SmtpClient())
			{
				client.Connect("smtp.mail.ru", 465, false);
				client.Authenticate("email", "pas");
				client.Send(emailMessage);

				client.Disconnect(true);
			}*/






		/*string smtpServer = "smtp.gmail.com";
		int smtpPort = 587; // or your SMTP server port
		string smtpUsername = "email";
		string smtpPassword = "pas";

		using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
		{
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
			smtpClient.EnableSsl = true;

			using (MailMessage mailMessage = new MailMessage())
			{
				mailMessage.From = new MailAddress(smtpUsername);
				mailMessage.To.Add(toEmail);
				mailMessage.Subject = subject;
				mailMessage.Body = body;

				smtpClient.Send(mailMessage);
			}
		}*//*
	}*/
	}
}

