using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Table("Rezervares")]
    public class Rezervare
    {
        public int ID_Rezervare { get; set; }
        public int ID_Client { get; set; }
        public int ID_Vehicul { get; set; }
        public DateTime Data_Start { get; set; }
        public DateTime Data_Sfarsit { get; set; }
        public string Status { get; set; }
    }
}
