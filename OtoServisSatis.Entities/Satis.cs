namespace OtoServisSatis.Entities
{
    public class Satis : IEntity // veri tabanı işlemleri için işaretliyoruz
    {
        public int Id { get; set; }
        public int AracId { get; set; }
        public int MusteriId { get; set; }
        public decimal SatisFiyati { get; set; }
        public DateTime SatisTarihi { get; set; }
        public virtual Arac? arac { get; set; }
        public virtual Musteri? musteri { get; set; } //Bu bir class yapısı
    }
}
