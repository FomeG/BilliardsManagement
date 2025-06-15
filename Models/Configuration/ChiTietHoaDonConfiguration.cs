using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;

namespace Models.Configuration
{
    public class ChiTietHoaDonConfiguration : IEntityTypeConfiguration<ChiTietHoaDon>
    {
        public void Configure(EntityTypeBuilder<ChiTietHoaDon> builder)
        {
            builder.ToTable("ChiTietHoaDon");
            
            builder.HasKey(c => c.ID);
            
            builder.Property(c => c.ID)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            
            builder.Property(c => c.HoaDonID)
                .HasColumnName("HoaDonID")
                .IsRequired();
            
            builder.Property(c => c.SanPhamID)
                .HasColumnName("SanPhamID")
                .IsRequired();
            
            builder.Property(c => c.SoLuong)
                .HasColumnName("SoLuong")
                .IsRequired();
            
            builder.Property(c => c.DonGia)
                .HasColumnName("DonGia")
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            
            builder.Property(c => c.ThanhTien)
                .HasColumnName("ThanhTien")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            // Foreign key relationships
            builder.HasOne(c => c.HoaDon)
                .WithMany(h => h.ChiTietHoaDons)
                .HasForeignKey(c => c.HoaDonID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.SanPham)
                .WithMany(s => s.ChiTietHoaDons)
                .HasForeignKey(c => c.SanPhamID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
