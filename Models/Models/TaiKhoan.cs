namespace Models.Models
{
    public class TaiKhoan
    {
        public int ID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }
        public string HoTen { get; set; }
        public bool TrangThai { get; set; }

        // các trường quan hệ _
        public virtual ICollection<PhienChoi> PhienChois { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<NapGio> NapGios { get; set; }
    }
}
