using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ParcAuto.Models
{
    public class Vehicle
    {
        [Key]
        public int ID_Vehicul { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        [DisplayName("An fabricatie")]

        public int An_Fabricatie { get; set; }
        [DisplayName("Tip combustibil")]
        public string Tip_Combustibil { get; set; }
        public string Stare { get; set; } 
        public int Kilometraj { get; set; }
        [JsonIgnore]
        public ICollection<Mentenanta>? Mentenante { get; set; }
        [JsonIgnore]
        public ICollection<Rezervare>? Rezervari { get; set; }

    }
}
