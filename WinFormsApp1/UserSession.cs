using Models.Models;

namespace WinFormsApp1
{
    /// <summary>
    /// Class quản lý thông tin phiên đăng nhập của người dùng (đăng nhập xong dữ liệu sẽ lưu vào các biến ở bên dưới)
    /// </summary>
    public static class UserSession
    {
        /// <summary>
        /// Thông tin tài khoản hiện tại đang đăng nhập
        /// </summary>
        public static TaiKhoan? CurrentUser { get; set; }

        /// <summary>
        /// ID của người dùng hiện tại
        /// </summary>
        public static int? CurrentUserId => CurrentUser?.ID;

        /// <summary>
        /// Tên của người dùng hiện tại
        /// </summary>
        public static string? CurrentUserName => CurrentUser?.HoTen;

        /// <summary>
        /// Vai trò của người dùng hiện tại
        /// </summary>
        public static string? CurrentUserRole => CurrentUser?.VaiTro;

        /// <summary>
        /// Kiểm tra xem có người dùng nào đang đăng nhập không
        /// </summary>
        public static bool IsLoggedIn => CurrentUser != null;

        /// <summary>
        /// Đăng xuất - xóa thông tin phiên đăng nhập
        /// </summary>
        public static void Logout()
        {
            CurrentUser = null;
        }

        /// <summary>
        /// Đăng nhập - lưu thông tin người dùng
        /// </summary>
        /// <param name="user">Thông tin tài khoản</param>
        public static void Login(TaiKhoan user)
        {
            CurrentUser = user;
        }
    }
}
