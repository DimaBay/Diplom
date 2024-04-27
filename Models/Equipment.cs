namespace inventory.Models
{
	public class Equipment
	{
		public int Id { get; set; }

		public string personalNum { get; set; }

		public string name { get; set; }


		public string type { get; set; }

		public string invnum { get; set; }

		public string date { get; set; }

		public string price { get; set; }
		public string QR { get; set; }

		public string mol { get; set; }
		public string filial { get; set; }
		public string? mol1 { get; set; }	
		public List<Users> Users { get; set; }
	}
}
