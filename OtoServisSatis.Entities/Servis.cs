using System.ComponentModel.DataAnnotations;

namespace OtoServisSatis.Entities
{
    public class Servis : IEntity
    {
        public int Id { get; set; }
        public DateTime ServisGelisTarihi { get; set; }    
        public string AracSorunu { get; set; }    
        public decimal ServisUcreti { get; set; }    
        public DateTime ServistenCikisTarihi { get; set; }
        public string? YapilanIslemler { get; set; }
        public bool GarantiKapsamindaMi { get; set; }
        [StringLength(15)]
        public string AracPlaka { get; set; }
        [StringLength(30)]
        public string Marka { get; set; }
        [StringLength(30)]
        public string? Model { get; set; }
        [StringLength(30)]
        public string? KasaTipi { get; set; }
        [StringLength(50)]
        public string? SaseNo { get; set; }
        public string Notlar { get; set; }
    }
}
