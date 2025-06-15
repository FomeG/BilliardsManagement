namespace Models.Models
{
    public class PhienChoi
    {
        public int ID { get; set; }
        public int BanID { get; set; }
        public int? KhachHangID { get; set; }
        public string TenKhachVangLai { get; set; }
        public int NhanVienID { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public double? TongThoiGian { get; set; }
        public decimal? TienBan { get; set; }
        public int TrangThai { get; set; } // 0: Kết thúc, 1: Đang chơi

        // các trường quan hệ _
        public virtual Ban Ban { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual TaiKhoan NhanVien { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
