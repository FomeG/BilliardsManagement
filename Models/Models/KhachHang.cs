namespace Models.Models
{
    public class KhachHang
    {
        public int ID { get; set; }
        public string HoTen { get; set; }
        public bool GioiTinh {get;set;}
        public DateTime NgaySinh {get ;set;}
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayDangKy { get; set; }
        public decimal SoTienConLai { get; set; }

        // các trường quan hệ _
        public virtual ICollection<PhienChoi> PhienChois { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<NapGio> NapGios { get; set; }
    }
}
