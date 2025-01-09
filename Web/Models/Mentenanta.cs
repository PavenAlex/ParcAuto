using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Table("Mentenantas")]
    public class Mentenanta
    {
        public int ID_Mentenanta { get; set; }
        public int ID_Vehicul { get; set; }
        public string Tip_Interventie { get; set; }
        public DateTime Data_Programare { get; set; }
        public decimal Cost_Estimativ { get; set; }
    }
}
