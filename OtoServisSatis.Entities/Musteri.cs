using System.ComponentModel.DataAnnotations;

namespace OtoServisSatis.Entities
{
    public class Musteri : IEntity
    {
        public int Id { get; set; }
        public int AracId { get; set; } //Entityframeworkcore bunun sayesinde arac classıyla bağlantıyı kuracak
        [StringLength(50)]
        public string Adi { get; set; }
        [StringLength(50)]
        public string Soyadi { get; set; }
        [StringLength(11)]
        public string? TcNo { get; set; } //bu alanlar zorunlu olmasın diyoruz nullabla geçilebiliyor
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(500)]
        public string? Adres { get; set; } // zorunlu olup olmadığı ve data annotation ı DataBaseContext sınıfında Fluent API ile yapabiliyorum
        [StringLength(15)]
        public string? Telefon { get; set; }
        public string? Notlar { get; set; } //önceki sürümde soru işareti koymasan da nullable geçebiliyordun
        public virtual Arac? arac { get; set; }
    }
}
