using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ParcAuto.Models
{
    public class Factura
    {
        [Key]
        public int ID_Factura { get; set; }
        [ForeignKey("Rezervare")]
        public int ID_Rezervare { get; set; }
        [JsonIgnore]
        public Rezervare? Rezervare { get; set; }
        public DateTime Data_Emitere { get; set; }
        public decimal Suma_Totala { get; set; }
        public string Status_Plata { get; set; }
    }
}
