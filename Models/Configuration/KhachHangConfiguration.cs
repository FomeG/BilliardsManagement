using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;

namespace Models.Configuration
{
    public class KhachHangConfiguration : IEntityTypeConfiguration<KhachHang>
    {
        public void Configure(EntityTypeBuilder<KhachHang> builder)
        {
            builder.ToTable("KhachHang");
            
            builder.HasKey(k => k.ID);
            
            builder.Property(k => k.ID)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            
            builder.Property(k => k.HoTen)
                .HasColumnName("HoTen")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(k => k.GioiTinh)
                .HasColumnName("GioiTinh")
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue(true); // true = Nam, false = Nữ

            builder.Property(k => k.NgaySinh)
                .HasColumnName("NgaySinh")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(k => k.SoDienThoai)
                .HasColumnName("SoDienThoai")
                .HasMaxLength(15)
                .IsRequired();

            builder.HasIndex(k => k.SoDienThoai)
                .IsUnique();

            builder.Property(k => k.DiaChi)
                .HasColumnName("DiaChi")
                .HasMaxLength(200);

            builder.Property(k => k.NgayDangKy)
                .HasColumnName("NgayDangKy")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(k => k.SoTienConLai)
                .HasColumnName("SoTienConLai")
                .HasDefaultValue(0.0);
        }
    }
}
