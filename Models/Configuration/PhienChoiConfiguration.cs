using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;

namespace Models.Configuration
{
    public class PhienChoiConfiguration : IEntityTypeConfiguration<PhienChoi>
    {
        public void Configure(EntityTypeBuilder<PhienChoi> builder)
        {
            builder.ToTable("PhienChoi");
            
            builder.HasKey(p => p.ID);
            
            builder.Property(p => p.ID)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            
            builder.Property(p => p.BanID)
                .HasColumnName("BanID")
                .IsRequired();
            
            builder.Property(p => p.KhachHangID)
                .HasColumnName("KhachHangID");
            
            builder.Property(p => p.TenKhachVangLai)
                .HasColumnName("TenKhachVangLai")
                .HasMaxLength(100);
            
            builder.Property(p => p.NhanVienID)
                .HasColumnName("NhanVienID")
                .IsRequired();
            
            builder.Property(p => p.ThoiGianBatDau)
                .HasColumnName("ThoiGianBatDau")
                .HasDefaultValueSql("GETDATE()");
            
            builder.Property(p => p.ThoiGianKetThuc)
                .HasColumnName("ThoiGianKetThuc");
            
            builder.Property(p => p.TongThoiGian)
                .HasColumnName("TongThoiGian");
            
            builder.Property(p => p.TienBan)
                .HasColumnName("TienBan")
                .HasColumnType("decimal(10,2)");
            
            builder.Property(p => p.TrangThai)
                .HasColumnName("TrangThai")
                .HasColumnType("int")
                .HasDefaultValue(1); // 0: Kết thúc, 1: Đang chơi

            // Foreign key relationships
            builder.HasOne(p => p.Ban)
                .WithMany(b => b.PhienChois)
                .HasForeignKey(p => p.BanID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.KhachHang)
                .WithMany(k => k.PhienChois)
                .HasForeignKey(p => p.KhachHangID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.NhanVien)
                .WithMany(t => t.PhienChois)
                .HasForeignKey(p => p.NhanVienID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
