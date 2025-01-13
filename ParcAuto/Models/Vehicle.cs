using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<Mentenanta> Mentenante { get; set; }

    }
}
