using System.ComponentModel.DataAnnotations;

namespace ParcAuto.Models
{
    public class Factura
    {
        [Key]
        public int ID_Factura { get; set; }
        public int ID_Rezervare { get; set; }
        public DateTime Data_Emitere { get; set; }
        public decimal Suma_Totala { get; set; }
        public string Status_Plata { get; set; }
    }
}
