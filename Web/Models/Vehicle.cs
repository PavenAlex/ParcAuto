using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int ID_Vehicul { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public int An_Fabricatie { get; set; }
        public string Tip_Combustibil { get; set; }
        public string Stare { get; set; }
        public int Kilometraj { get; set; }
    }
}
