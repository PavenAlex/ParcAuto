using System.ComponentModel.DataAnnotations;

namespace ParcAuto.Models
{
    public class Rezervare
    {
        [Key]
        public int ID_Rezervare { get; set; }
        public int ID_Client { get; set; }
        public int ID_Vehicul { get; set; }
        public DateTime Data_Start { get; set; }
        public DateTime Data_Sfarsit { get; set; }
        public string Status { get; set; }
    }
}
