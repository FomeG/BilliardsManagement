using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;

namespace Models.Configuration
{
    public class SanPhamConfiguration : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.ToTable("SanPham");
            
            builder.HasKey(s => s.ID);
            
            builder.Property(s => s.ID)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            
            builder.Property(s => s.TenSanPham)
                .HasColumnName("TenSanPham")
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(s => s.LoaiSanPham)
                .HasColumnName("LoaiSanPham")
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(s => s.DonGia)
                .HasColumnName("DonGia")
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            
            builder.Property(s => s.ConHang)
                .HasColumnName("ConHang")
                .HasDefaultValue(true);
            
            builder.Property(s => s.HinhAnh)
                .HasColumnName("HinhAnh")
                .HasColumnType("VARBINARY(MAX)");
        }
    }
}
