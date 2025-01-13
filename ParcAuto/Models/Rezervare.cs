using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Factura? Factura { get; set; }
    }
}
