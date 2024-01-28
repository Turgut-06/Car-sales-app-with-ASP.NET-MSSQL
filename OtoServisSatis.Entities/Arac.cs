using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class Arac : IEntity
    {
        
        public int Id { get ; set ; }
        public int MarkaId { get; set; }
        [StringLength(50)]
        public string Renk { get; set; }
        public decimal Fiyati { get; set; }
        [StringLength(50)]
        public string Modeli { get; set; }
        [StringLength(50)]   // string alanlara karakter sınırı koyuyoruz
        public string KasaTipi { get; set; }
        public int ModelYili { get; set; }
        public int Km { get; set; }
        public bool SatistaMi { get; set; }
        [Display(Name = "Anasayfada gözüksün mü")]
        public bool Anasayfa { get; set; }
        public string Notlar { get; set; }

        [StringLength(100)]
        public string? Resim1 { get; set; }
        [StringLength(100)]
        public string? Resim2 { get; set; }
        [StringLength(100)]
        public string? Resim3 { get; set; }

        public virtual Marka? marka { get; set; } //Araç sınıfa ile Marka sınıfı arasında bağlantı kuruyorum
                                                  // Nullable(?) olarak işaretledik her seferinde zorunlu kılmasın diye nullabla=boş geçilebilir
    }
}
