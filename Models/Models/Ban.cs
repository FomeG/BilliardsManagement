namespace Models.Models
{
    public class Ban
    {
        public int ID { get; set; }
        public string TenBan { get; set; }
        public string LoaiBan { get; set; }
        public decimal GiaTheoGio { get; set; }
        public int TrangThai { get; set; } // 0: Đang có người, 1: Trống, 2: Bảo trì

        // các trường quan hệ _
        public virtual ICollection<PhienChoi> PhienChois { get; set; }
    }
}
