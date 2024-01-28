using Microsoft.EntityFrameworkCore;
using OtoServisSatis.Entities;

namespace OtoServisSatis.Data
{
    public class DataBaseContext : DbContext // bunun sayesinde entity framework core içerisindeki yapıları kullanabiliyoruz
    {
        public DbSet<Arac> Araclar { get; set; } //araclar tablo adı olarak veritabanına gönderiliyor migration oluşturarak entity framework orm'i ile
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Satis> Satislar { get; set; }
        public DbSet<Servis> Servisler { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB ; database=OtoServisSatisNetCore ; integrated security=True ; MultipleActiveResultSets=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API kullanımı
            modelBuilder.Entity<Marka>().Property(m=>m.Adi).IsRequired().HasColumnType("varchar(50)");// Buradan entity katmanına erişerek zorunlu alan oluşturuyoruz buradan data annotation yapıyoruz
            modelBuilder.Entity<Rol>().Property(r => r.Adi).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Entity<Rol>().HasData(new Rol()
            {
                Id = 1,
                Adi = "admin"
            });
            modelBuilder.Entity<Kullanici>().HasData(new Kullanici()
            {
                Id = 1,
                Adi = "Admin",
                AktifMi = true,
                EklenmeTarihi = DateTime.Now,
                Email = "admin@otoservissatis.tc",
                KullaniciAdi = "admin",
                Sifre = "123456",
                RolId = 1,
                Soyadi = "admin",
                Telefon = "0850",

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
