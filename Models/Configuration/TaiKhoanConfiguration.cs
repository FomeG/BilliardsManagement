using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;

namespace Models.Configuration
{
    public class TaiKhoanConfiguration : IEntityTypeConfiguration<TaiKhoan>
    {
        public void Configure(EntityTypeBuilder<TaiKhoan> builder)
        {
            builder.ToTable("TaiKhoan");
            
            builder.HasKey(t => t.ID);
            
            builder.Property(t => t.ID)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            
            builder.Property(t => t.TenDangNhap)
                .HasColumnName("TenDangNhap")
                .HasMaxLength(50)
                .IsRequired();
            
            builder.HasIndex(t => t.TenDangNhap)
                .IsUnique();
            
            builder.Property(t => t.MatKhau)
                .HasColumnName("MatKhau")
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(t => t.VaiTro)
                .HasColumnName("VaiTro")
                .HasMaxLength(20)
                .IsRequired();
            
            builder.Property(t => t.HoTen)
                .HasColumnName("HoTen")
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(t => t.TrangThai)
                .HasColumnName("TrangThai")
                .HasDefaultValue(true);
        }
    }
}
