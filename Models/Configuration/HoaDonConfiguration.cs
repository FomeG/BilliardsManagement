using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;

namespace Models.Configuration
{
    public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.ToTable("HoaDon");
            
            builder.HasKey(h => h.ID);
            
            builder.Property(h => h.ID)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            
            builder.Property(h => h.PhienChoiID)
                .HasColumnName("PhienChoiID");
            
            builder.Property(h => h.KhachHangID)
                .HasColumnName("KhachHangID");
            
            builder.Property(h => h.TenKhachVangLai)
                .HasColumnName("TenKhachVangLai")
                .HasMaxLength(100);
            
            builder.Property(h => h.NhanVienID)
                .HasColumnName("NhanVienID")
                .IsRequired();
            
            builder.Property(h => h.NgayLap)
                .HasColumnName("NgayLap")
                .HasDefaultValueSql("GETDATE()");
            
            builder.Property(h => h.TongTien)
                .HasColumnName("TongTien")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);
            
            builder.Property(h => h.TrangThai)
                .HasColumnName("TrangThai")
                .HasColumnType("int")
                .HasDefaultValue(0); // 0: Chưa thanh toán, 1: Đã thanh toán

            // Foreign key relationships
            builder.HasOne(h => h.PhienChoi)
                .WithMany(p => p.HoaDons)
                .HasForeignKey(h => h.PhienChoiID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(h => h.KhachHang)
                .WithMany(k => k.HoaDons)
                .HasForeignKey(h => h.KhachHangID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(h => h.NhanVien)
                .WithMany(t => t.HoaDons)
                .HasForeignKey(h => h.NhanVienID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
