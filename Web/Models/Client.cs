using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Table("Clients")]
    public class Client
    {
        public int ID_Client { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string CNP { get; set; }
        public DateTime Data_Inregistrarii { get; set; }

    }
}
