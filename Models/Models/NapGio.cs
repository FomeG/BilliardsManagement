namespace Models.Models
{
    public class NapGio
    {
        public int ID { get; set; }
        public int KhachHangID { get; set; }
        public int NhanVienID { get; set; }
        public decimal SoTienNap { get; set; }
        public DateTime NgayNap { get; set; }

        // các trường quan hệ _
        public virtual KhachHang KhachHang { get; set; }
        public virtual TaiKhoan NhanVien { get; set; }
    }
}
