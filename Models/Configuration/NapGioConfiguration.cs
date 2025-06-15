using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;

namespace Models.Configuration
{
    public class NapGioConfiguration : IEntityTypeConfiguration<NapGio>
    {
        public void Configure(EntityTypeBuilder<NapGio> builder)
        {
            builder.ToTable("NapGio");

            builder.HasKey(n => n.ID);

            builder.Property(n => n.ID)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(n => n.KhachHangID)
                .HasColumnName("KhachHangID")
                .IsRequired();

            builder.Property(n => n.NhanVienID)
                .HasColumnName("NhanVienID")
                .IsRequired();

            builder.Property(n => n.SoTienNap)
                .HasColumnName("SoTienNap")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(n => n.NgayNap)
                .HasColumnName("NgayNap")
                .HasDefaultValueSql("GETDATE()");

            // Foreign key relationships
            builder.HasOne(n => n.KhachHang)
                .WithMany(k => k.NapGios)
                .HasForeignKey(n => n.KhachHangID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(n => n.NhanVien)
                .WithMany(t => t.NapGios)
                .HasForeignKey(n => n.NhanVienID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
