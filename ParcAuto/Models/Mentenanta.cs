using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ParcAuto.Models
{
    public class Mentenanta
    {
        [Key]
        public int ID_Mentenanta { get; set; }

        // Cheia străină
        [ForeignKey("Vehicul")]
        public int ID_Vehicul { get; set; }

        // Proprietatea de navigare
        [JsonIgnore]
        public Vehicle? Vehicul { get; set; }

        public string Tip_Interventie { get; set; }
        public DateTime Data_Programare { get; set; }
        public decimal Cost_Estimativ { get; set; }
    }
}