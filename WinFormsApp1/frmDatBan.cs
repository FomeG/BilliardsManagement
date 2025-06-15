using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models.Models;
using Models.HandleData;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    public partial class frmDatBan : Form
    {
        private readonly Ban currentBan;
        private readonly TaiKhoan currentUser;
        private readonly Action onSuccess;
        private readonly DaDBContext dbContext;

        public frmDatBan(Ban ban, TaiKhoan user, Action onSuccessCallback)
        {
            InitializeComponent();
            currentBan = ban;
            currentUser = user;
            onSuccess = onSuccessCallback;
            dbContext = new DaDBContext();

            // Add event handler for form closed
            this.FormClosed += frmDatBan_FormClosed;

            SetupForm();
            LoadKhachHangData();
        }

        private void SetupForm()
        {
            lblTenBan.Text = $"Tên bàn: {currentBan.TenBan}";
            lblGiaBan.Text = $"Giá theo giờ: {currentBan.GiaTheoGio:N0} VNĐ";

            // Mặc định chọn khách hàng
            rdKhachHang.Checked = true;
            UpdateCustomerSelection();
        }

        private async void LoadKhachHangData()
        {
            try
            {
                var khachHangs = await dbContext.KhachHangs
                    .Where(k => k.SoTienConLai > 0) // Chỉ hiển thị khách hàng có tiền
                    .OrderBy(k => k.HoTen)
                    .ToListAsync();

                cmbKhachHang.DisplayMember = "HoTen";
                cmbKhachHang.ValueMember = "ID";
                cmbKhachHang.DataSource = khachHangs;

                if (khachHangs.Count == 0)
                {
                    // Nếu không có khách hàng nào có tiền, chuyển sang khách vãng lai
                    rdKhachVangLai.Checked = true;
                    UpdateCustomerSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khách hàng: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RdKhachHang_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCustomerSelection();
        }

        private void RdKhachVangLai_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCustomerSelection();
        }

        private void UpdateCustomerSelection()
        {
            if (rdKhachHang.Checked)
            {
                cmbKhachHang.Visible = true;
                txtTenKhachVangLai.Visible = false;
                lblTenKhachVangLai.Visible = false;
            }
            else
            {
                cmbKhachHang.Visible = false;
                txtTenKhachVangLai.Visible = true;
                lblTenKhachVangLai.Visible = true;
            }
        }

        private void TxtSoTienTra_TextChanged(object sender, EventArgs e)
        {
            CalculatePlayTime();
        }

        private void CalculatePlayTime()
        {
            if (decimal.TryParse(txtSoTienTra.Text, out decimal soTienTra) && soTienTra > 0)
            {
                double soGioChoi = (double)(soTienTra / currentBan.GiaTheoGio);
                lblSoGioChoiValue.Text = $"{soGioChoi:F2} giờ";
            }
            else
            {
                lblSoGioChoiValue.Text = "0 giờ";
            }
        }

        private async void BtnDatBan_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                    return;

                // Lấy user hiện tại (ưu tiên currentUser, nếu null thì dùng UserSession)
                var activeUser = currentUser ?? UserSession.CurrentUser;

                if (activeUser == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng hiện tại. Vui lòng đăng nhập lại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Thông tin debug (có thể xóa sau khi test xong)
                // MessageBox.Show($"Debug Info: User {activeUser.HoTen}, Bàn {currentBan?.TenBan}", "Debug");

                decimal soTienTra = decimal.Parse(txtSoTienTra.Text);
                double soGioChoi = (double)(soTienTra / currentBan.GiaTheoGio);

                // Lấy thời gian hiện tại theo múi giờ Việt Nam (UTC+7)
                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var thoiGianBatDau = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
                var thoiGianKetThuc = thoiGianBatDau.AddHours(soGioChoi);

                // Tạo phiên chơi mới
                var phienChoi = new PhienChoi
                {
                    BanID = currentBan.ID,
                    NhanVienID = activeUser.ID,
                    ThoiGianBatDau = thoiGianBatDau,
                    ThoiGianKetThuc = thoiGianKetThuc,
                    TongThoiGian = soGioChoi,
                    TienBan = soTienTra,
                    TrangThai = 1, // Đang chơi
                    TenKhachVangLai = "" // Khởi tạo với chuỗi rỗng thay vì null
                };

                if (rdKhachHang.Checked)
                {
                    var selectedKhachHang = (KhachHang?)cmbKhachHang.SelectedItem;
                    if (selectedKhachHang != null)
                    {
                        phienChoi.KhachHangID = selectedKhachHang.ID;
                        phienChoi.TenKhachVangLai = ""; // Để trống cho khách hàng thành viên

                        // MessageBox.Show($"Selected Customer: {selectedKhachHang.HoTen}", "Debug");

                        // Trừ tiền khách hàng
                        selectedKhachHang.SoTienConLai -= soTienTra;
                        dbContext.KhachHangs.Update(selectedKhachHang);
                    }
                }
                else
                {
                    phienChoi.TenKhachVangLai = txtTenKhachVangLai.Text.Trim();
                    phienChoi.KhachHangID = null; // Không có khách hàng thành viên
                    // MessageBox.Show($"Khách vãng lai: {phienChoi.TenKhachVangLai}", "Debug");
                }

                // Cập nhật trạng thái bàn
                currentBan.TrangThai = 0; // Đang có người
                dbContext.Bans.Update(currentBan);

                // Thêm phiên chơi
                dbContext.PhienChois.Add(phienChoi);

                // MessageBox.Show("Chuẩn bị lưu vào database...", "Debug");

                await dbContext.SaveChangesAsync();

                MessageBox.Show($"Đặt bàn {currentBan.TenBan} thành công!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Gọi callback để refresh dữ liệu
                onSuccess?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi chi tiết khi đặt bàn:\n" +
                    $"Message: {ex.Message}\n" +
                    $"Inner Exception: {ex.InnerException?.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtSoTienTra.Text) || !decimal.TryParse(txtSoTienTra.Text, out decimal soTien) || soTien <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienTra.Focus();
                return false;
            }

            if (rdKhachHang.Checked)
            {
                if (cmbKhachHang.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var selectedKhachHang = (KhachHang?)cmbKhachHang.SelectedItem;
                if (selectedKhachHang != null && selectedKhachHang.SoTienConLai < soTien)
                {
                    MessageBox.Show($"Khách hàng chỉ còn {selectedKhachHang.SoTienConLai:N0} VNĐ, không đủ để thanh toán!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtTenKhachVangLai.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập tên khách vãng lai!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenKhachVangLai.Focus();
                    return false;
                }
            }

            return true;
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            onSuccess?.Invoke(); // Gọi callback để xóa form
        }

        private void DisposeResources()
        {
            dbContext?.Dispose();
        }

        private void frmDatBan_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeResources();
        }
    }
}
