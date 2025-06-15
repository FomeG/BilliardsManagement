using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class DoiSoGioThanhSoTien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoGioNap",
                table: "NapGio");

            migrationBuilder.DropColumn(
                name: "SoGioConLai",
                table: "KhachHang");

            migrationBuilder.AddColumn<decimal>(
                name: "SoTienConLai",
                table: "KhachHang",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0.00m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoTienConLai",
                table: "KhachHang");

            migrationBuilder.AddColumn<double>(
                name: "SoGioNap",
                table: "NapGio",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SoGioConLai",
                table: "KhachHang",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
