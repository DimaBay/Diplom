namespace inventory.Models
{
    public class Users 
    {

        public int id { get; set; }

        public string FIO { get; set; }

        public string Filial { get; set; }

        public string Email { get; set; }



		public string LastName { get; set; }


		public string FirstName { get; set; }


		public string Patronymic { get; set; }


		public string Branch { get; set; }


		


		public string PhoneNumber { get; set; }

		public string? Login { get; set; }
		public string? Password { get; set; }
		public string? Role { get; set; }
		public byte[]? ImageData { get; set; }

		public static List<Profile> ProfileItems = new List<Profile>();






		public List<Filial> filial { get; set; }
    }
}
