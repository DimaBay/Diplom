namespace inventory.Models
{
    public class Users 
    {

        public int id { get; set; }

        public string FIO { get; set; }

        public string Filial { get; set; }

        public string Email { get; set; }

        public List<Filial> filial { get; set; }
    }
}
