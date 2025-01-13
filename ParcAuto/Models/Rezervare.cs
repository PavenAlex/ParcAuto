using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ParcAuto.Models
{
    public class Rezervare
    {
        [Key]
        public int ID_Rezervare { get; set; }
        [ForeignKey("Client")]
        public int ID_Client { get; set; }
        [JsonIgnore]
        public Client? Client { get; set; }
        [ForeignKey("Vehicle")]
        public int ID_Vehicul { get; set; }
        [JsonIgnore]
        public Vehicle? Vehicle { get; set; }
        public DateTime Data_Start { get; set; }
        public DateTime Data_Sfarsit { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public Factura? Factura { get; set; }
    }
}
