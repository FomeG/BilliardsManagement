using Microsoft.EntityFrameworkCore;
using Models.Configuration;
using Models.Models;

namespace Models.HandleData
{
    public class DaDBContext : DbContext
    {
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<PhienChoi> PhienChois { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<NapGio> NapGios { get; set; }

        public DaDBContext() : base()
        {
        }

        public DaDBContext(DbContextOptions<DaDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=NGHIA\\SQLEXPRESS;Database=BilliardsManagement_DB;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaiKhoanConfiguration());
            modelBuilder.ApplyConfiguration(new KhachHangConfiguration());
            modelBuilder.ApplyConfiguration(new BanConfiguration());
            modelBuilder.ApplyConfiguration(new SanPhamConfiguration());
            modelBuilder.ApplyConfiguration(new PhienChoiConfiguration());
            modelBuilder.ApplyConfiguration(new HoaDonConfiguration());
            modelBuilder.ApplyConfiguration(new ChiTietHoaDonConfiguration());
            modelBuilder.ApplyConfiguration(new NapGioConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
