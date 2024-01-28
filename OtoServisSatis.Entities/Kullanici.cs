using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OtoServisSatis.Entities
{
    public class Kullanici : IEntity
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Display(Name = "Adı"),Required(ErrorMessage ="{0} boş bırakılamaz")] //bu şekilde uyarı mesajı yazabiliyoruz {0} yazarsak her seferinde adı soyadı diye belirtmemize gerek kalmaz
        public string Adi { get; set; }
        [StringLength(50), Display(Name = "Soyadı"), Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string Soyadi { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Telefon { get; set; }
        [StringLength(50), Display(Name = "Kullanıcı adı")]
        public string? KullaniciAdi { get; set; }
        [StringLength(50)]
        public string Sifre { get; set; }
        public bool AktifMi { get; set; }
        [Display(Name ="Eklenme Tarihi"),ScaffoldColumn(false)] //arayüzde görünen ismi buradan ayarlayabiliyoruz ,scaffoldcolumn false yapınca bu property için gerekli alanı oluşturmaz
        public DateTime? EklenmeTarihi { get; set; } = DateTime.Now;
        [Display(Name = "Kullanıcı Rolü"), Required(ErrorMessage = "{0} boş bırakılamaz")]
        public int RolId { get; set; }
        [Display(Name = "Kullanıcı Rolü"), Required(ErrorMessage = "{0} boş bırakılamaz")] //kullandığımız her şey yukarıda eklediğimiz DataAnnotation kütüphanesinden geliyor
        public virtual Rol? rol { get; set; }


    }
}
