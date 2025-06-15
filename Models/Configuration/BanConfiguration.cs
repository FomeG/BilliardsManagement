using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;

namespace Models.Configuration
{
    public class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.ToTable("Ban");
            
            builder.HasKey(b => b.ID);
            
            builder.Property(b => b.ID)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
            
            builder.Property(b => b.TenBan)
                .HasColumnName("TenBan")
                .HasMaxLength(50)
                .IsRequired();
            
            builder.HasIndex(b => b.TenBan)
                .IsUnique();
            
            builder.Property(b => b.LoaiBan)
                .HasColumnName("LoaiBan")
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(b => b.GiaTheoGio)
                .HasColumnName("GiaTheoGio")
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            
            builder.Property(b => b.TrangThai)
                .HasColumnName("TrangThai")
                .HasColumnType("int")
                .HasDefaultValue(1); // 0: Đang có người, 1: Trống

            // Configure relationships
            builder.HasMany(b => b.PhienChois)
                .WithOne(p => p.Ban)
                .HasForeignKey(p => p.BanID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
