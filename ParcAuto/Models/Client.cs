using System.ComponentModel.DataAnnotations;

namespace ParcAuto.Models
{
    public class Client
    {
        [Key]
        public int ID_Client { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string CNP { get; set; }
        public DateTime Data_Inregistrarii { get; set; }

    }
}
