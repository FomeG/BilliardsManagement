namespace Models.Models
{
    public class HoaDon
    {
        public int ID { get; set; }
        public int? PhienChoiID { get; set; }
        public int? KhachHangID { get; set; }
        public string TenKhachVangLai { get; set; }
        public int NhanVienID { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public int TrangThai { get; set; } // 0: Chưa thanh toán, 1: Đã thanh toán

        // các trường quan hệ _
        public virtual PhienChoi PhienChoi { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual TaiKhoan NhanVien { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
