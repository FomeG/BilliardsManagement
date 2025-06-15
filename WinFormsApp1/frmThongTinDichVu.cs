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
    public partial class frmThongTinDichVu : Form
    {
        private readonly Ban currentBan;
        private readonly TaiKhoan currentUser;
        private readonly Action onClose;
        private readonly DaDBContext dbContext;
        private PhienChoi? currentPhienChoi;
        private System.Windows.Forms.Timer? updateTimer;

        public frmThongTinDichVu(Ban ban, TaiKhoan user, Action onCloseCallback)
        {
            InitializeComponent();
            currentBan = ban;
            currentUser = user;
            onClose = onCloseCallback;
            dbContext = new DaDBContext();

            // Add event handler for form closed
            this.FormClosed += frmThongTinDichVu_FormClosed;

            SetupForm();
            LoadPhienChoiData();
            SetupTimer();
        }

        private void SetupForm()
        {
            lblTenBan.Text = $"Tên bàn: {currentBan.TenBan}";
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("TenSanPham", "Tên dịch vụ");
            dataGridView1.Columns.Add("SoLuong", "Số lượng");
            dataGridView1.Columns.Add("DonGia", "Đơn giá");
            dataGridView1.Columns.Add("ThanhTien", "Thành tiền");

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void SetupTimer()
        {
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000; // Cập nhật mỗi giây
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateThoiGianChoi();
        }

        private async void LoadPhienChoiData()
        {
            try
            {
                currentPhienChoi = await dbContext.PhienChois
                    .Include(p => p.KhachHang)
                    .Include(p => p.HoaDons)
                        .ThenInclude(h => h.ChiTietHoaDons)
                            .ThenInclude(ct => ct.SanPham)
                    .FirstOrDefaultAsync(p => p.BanID == currentBan.ID && p.TrangThai == 1);

                if (currentPhienChoi != null)
                {
                    UpdatePhienChoiInfo();
                    LoadDichVuData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phiên chơi cho bàn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    onClose?.Invoke();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin phiên chơi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePhienChoiInfo()
        {
            if (currentPhienChoi.KhachHang != null)
            {
                lblTenKhachHang.Text = $"Khách hàng: {currentPhienChoi.KhachHang.HoTen}";
            }
            else
            {
                lblTenKhachHang.Text = $"Khách vãng lai: {currentPhienChoi.TenKhachVangLai}";
            }

            lblThoiGianBatDau.Text = $"Bắt đầu: {currentPhienChoi.ThoiGianBatDau:HH:mm:ss dd/MM/yyyy}";
            lblThoiGianKetThuc.Text = $"Bắt đầu: {currentPhienChoi.ThoiGianKetThuc:HH:mm:ss dd/MM/yyyy}";
            UpdateThoiGianChoi();
            UpdateTongTien();
        }

        private void UpdateThoiGianChoi()
        {
            if (currentPhienChoi != null)
            {
                var thoiGianChoi = DateTime.Now - currentPhienChoi.ThoiGianBatDau;
                lblThoiGianChoi.Text = $"Đã chơi: {thoiGianChoi.Hours:D2}:{thoiGianChoi.Minutes:D2}:{thoiGianChoi.Seconds:D2}";
            }
        }

        private void LoadDichVuData()
        {
            dataGridView1.Rows.Clear();

            if (currentPhienChoi.HoaDons != null)
            {
                foreach (var hoaDon in currentPhienChoi.HoaDons)
                {
                    foreach (var chiTiet in hoaDon.ChiTietHoaDons)
                    {
                        dataGridView1.Rows.Add(
                            chiTiet.SanPham?.TenSanPham ?? "N/A",
                            chiTiet.SoLuong,
                            chiTiet.DonGia.ToString("N0"),
                            chiTiet.ThanhTien.ToString("N0")
                        );
                    }
                }
            }
        }

        private void UpdateTongTien()
        {
            decimal tongTienDichVu = 0;
            if (currentPhienChoi.HoaDons != null)
            {
                tongTienDichVu = currentPhienChoi.HoaDons.Sum(h => h.ChiTietHoaDons.Sum(ct => ct.ThanhTien));
            }

            decimal tongTien = (currentPhienChoi.TienBan ?? 0) + tongTienDichVu;
            lblTongTien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";
        }

        private void BtnThemDichVu_Click(object sender, EventArgs e)
        {
            ShowThemDichVuForm();
        }

        private void ShowThemDichVuForm()
        {
            try
            {
                // Tạm dừng timer
                updateTimer?.Stop();

                // Kiểm tra dữ liệu cần thiết
                if (currentPhienChoi == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin phiên chơi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    updateTimer?.Start(); // Khởi động lại timer
                    return;
                }

                // Kiểm tra parent container
                if (this.Parent == null)
                {
                    MessageBox.Show("Không thể mở form thêm dịch vụ. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    updateTimer?.Start(); // Khởi động lại timer
                    return;
                }

                // Tạo form thêm dịch vụ
                var frmThemDV = new frmThongTinDichVu_ThemDV(currentPhienChoi, currentUser, OnThemDichVuSuccess, OnThemDichVuCancel)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };

                // Lưu reference đến parent container
                var parentContainer = this.Parent;

                // Xóa nội dung hiện tại và thêm form mới
                parentContainer.Controls.Clear();
                parentContainer.Controls.Add(frmThemDV);
                frmThemDV.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form thêm dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                updateTimer?.Start(); // Khởi động lại timer nếu có lỗi
            }
        }

        private async void BtnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn thanh toán và kết thúc phiên chơi?",
                    "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Cập nhật phiên chơi
                    currentPhienChoi.ThoiGianKetThuc = DateTime.Now;
                    currentPhienChoi.TongThoiGian = (DateTime.Now - currentPhienChoi.ThoiGianBatDau).TotalHours;
                    currentPhienChoi.TrangThai = 0; // Kết thúc

                    // Cập nhật trạng thái bàn
                    currentBan.TrangThai = 1; // Trống
                    dbContext.Bans.Update(currentBan);

                    // Cập nhật hóa đơn thành đã thanh toán
                    if (currentPhienChoi.HoaDons != null)
                    {
                        foreach (var hoaDon in currentPhienChoi.HoaDons)
                        {
                            hoaDon.TrangThai = 1; // Đã thanh toán
                        }
                    }

                    dbContext.PhienChois.Update(currentPhienChoi);
                    await dbContext.SaveChangesAsync();

                    MessageBox.Show("Thanh toán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Đóng form và refresh dữ liệu
                    updateTimer?.Stop();
                    onClose?.Invoke();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnHuyBan_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn hủy bàn? Thao tác này không thể hoàn tác!",
                    "Xác nhận hủy bàn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Xóa phiên chơi và các hóa đơn liên quan
                    if (currentPhienChoi.HoaDons != null)
                    {
                        foreach (var hoaDon in currentPhienChoi.HoaDons)
                        {
                            dbContext.ChiTietHoaDons.RemoveRange(hoaDon.ChiTietHoaDons);
                            dbContext.HoaDons.Remove(hoaDon);
                        }
                    }

                    dbContext.PhienChois.Remove(currentPhienChoi);

                    // Cập nhật trạng thái bàn
                    currentBan.TrangThai = 1; // Trống
                    dbContext.Bans.Update(currentBan);

                    // Hoàn tiền cho khách hàng nếu có
                    if (currentPhienChoi.KhachHang != null && currentPhienChoi.TienBan.HasValue)
                    {
                        currentPhienChoi.KhachHang.SoTienConLai += currentPhienChoi.TienBan.Value;
                        dbContext.KhachHangs.Update(currentPhienChoi.KhachHang);
                    }

                    await dbContext.SaveChangesAsync();

                    MessageBox.Show("Hủy bàn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Đóng form và refresh dữ liệu
                    updateTimer?.Stop();
                    onClose?.Invoke();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hủy bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnThemDichVuSuccess()
        {
            try
            {
                // Đảm bảo chạy trên UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(OnThemDichVuSuccess));
                    return;
                }

                // Kiểm tra parent container
                if (this.Parent != null)
                {
                    // Lưu reference đến parent container
                    var parentContainer = this.Parent;

                    // Xóa form thêm dịch vụ và quay lại form thông tin dịch vụ
                    parentContainer.Controls.Clear();

                    // Tạo lại form thông tin dịch vụ với dữ liệu mới
                    var newThongTinForm = new frmThongTinDichVu(currentBan, currentUser, onClose)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill
                    };

                    parentContainer.Controls.Add(newThongTinForm);
                    newThongTinForm.Show();
                }
                else
                {
                    // Fallback: chỉ refresh dữ liệu nếu không có parent
                    LoadPhienChoiData(); // Refresh dữ liệu
                    updateTimer?.Start(); // Khởi động lại timer
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi refresh dữ liệu dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Khởi động lại timer trong trường hợp lỗi
                updateTimer?.Start();
            }
        }

        private void OnThemDichVuCancel()
        {
            try
            {
                // Đảm bảo chạy trên UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(OnThemDichVuCancel));
                    return;
                }

                // Kiểm tra parent container
                if (this.Parent != null)
                {
                    // Lưu reference đến parent container
                    var parentContainer = this.Parent;

                    // Xóa form thêm dịch vụ và quay lại form thông tin dịch vụ hiện tại (không tạo mới)
                    parentContainer.Controls.Clear();
                    parentContainer.Controls.Add(this);

                    // Khởi động lại timer
                    updateTimer?.Start();
                    this.Show();
                }
                else
                {
                    // Fallback: chỉ khởi động lại timer nếu không có parent
                    updateTimer?.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quay lại form dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Khởi động lại timer trong trường hợp lỗi
                updateTimer?.Start();
            }
        }

        private void DisposeResources()
        {
            updateTimer?.Stop();
            updateTimer?.Dispose();
            dbContext?.Dispose();
        }

        private void frmThongTinDichVu_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeResources();
        }
    }
}
