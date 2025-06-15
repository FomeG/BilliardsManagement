namespace Models.Models
{
    public class SanPham
    {
        public int ID { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public decimal DonGia { get; set; }
        public bool ConHang { get; set; }
        public byte[] HinhAnh { get; set; }

        // các trường quan hệ _
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
