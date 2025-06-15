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
    public partial class frmThongTinDichVu_ThemDV : Form
    {
        private readonly PhienChoi currentPhienChoi;
        private readonly TaiKhoan currentUser;
        private readonly Action onSuccess;
        private readonly Action onCancel;
        private readonly DaDBContext dbContext;
        private SanPham? selectedSanPham;

        public frmThongTinDichVu_ThemDV(PhienChoi phienChoi, TaiKhoan user, Action onSuccessCallback, Action? onCancelCallback = null)
        {
            InitializeComponent();
            currentPhienChoi = phienChoi;
            currentUser = user;
            onSuccess = onSuccessCallback;
            onCancel = onCancelCallback ?? onSuccessCallback; // Nếu không có callback riêng cho hủy, dùng success callback
            dbContext = new DaDBContext();

            // Add event handler for form closed
            this.FormClosed += frmThongTinDichVu_ThemDV_FormClosed;

            SetupForm();
            LoadSanPhamData();
        }

        private void SetupForm()
        {
            SetupDataGridView();
            UpdateTongTien();
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("TenSanPham", "Tên sản phẩm");
            dataGridView1.Columns.Add("DonGia", "Giá");
            dataGridView1.Columns.Add("LoaiSanPham", "Loại");

            // Ẩn cột ID
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void LoadSanPhamData()
        {
            try
            {
                var sanPhams = await dbContext.SanPhams
                    .Where(sp => sp.ConHang == true) // Chỉ lấy sản phẩm còn hàng
                    .OrderBy(sp => sp.LoaiSanPham)
                    .ThenBy(sp => sp.TenSanPham)
                    .ToListAsync();

                dataGridView1.Rows.Clear();
                foreach (var sp in sanPhams)
                {
                    dataGridView1.Rows.Add(
                        sp.ID,
                        sp.TenSanPham,
                        sp.DonGia.ToString("N0"),
                        sp.LoaiSanPham
                    );
                }

                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int sanPhamId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                selectedSanPham = dbContext.SanPhams.FirstOrDefault(sp => sp.ID == sanPhamId);
                UpdateTongTien();
            }
        }

        private void NumSoLuong_ValueChanged(object sender, EventArgs e)
        {
            UpdateTongTien();
        }

        private void UpdateTongTien()
        {
            if (selectedSanPham != null)
            {
                decimal tongTien = selectedSanPham.DonGia * numSoLuong.Value;
                lblTongTien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";
            }
            else
            {
                lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            }
        }

        private async void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedSanPham == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tìm hoặc tạo hóa đơn cho phiên chơi này
                var hoaDon = await dbContext.HoaDons
                    .Include(h => h.ChiTietHoaDons)
                    .FirstOrDefaultAsync(h => h.PhienChoiID == currentPhienChoi.ID && h.TrangThai == 0);

                if (hoaDon == null)
                {
                    // Tạo hóa đơn mới
                    // Lấy user hiện tại (ưu tiên currentUser, nếu null thì dùng UserSession)
                    var activeUser = currentUser ?? UserSession.CurrentUser;

                    if (activeUser == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin người dùng hiện tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    hoaDon = new HoaDon
                    {
                        PhienChoiID = currentPhienChoi.ID,
                        KhachHangID = currentPhienChoi.KhachHangID,
                        TenKhachVangLai = currentPhienChoi.TenKhachVangLai,
                        NhanVienID = activeUser.ID,
                        NgayLap = DateTime.Now,
                        TrangThai = 0 // Chưa thanh toán
                    };
                    dbContext.HoaDons.Add(hoaDon);
                    await dbContext.SaveChangesAsync(); // Save để có ID
                }

                // Kiểm tra xem sản phẩm đã có trong hóa đơn chưa
                var existingChiTiet = hoaDon.ChiTietHoaDons?.FirstOrDefault(ct => ct.SanPhamID == selectedSanPham.ID);

                if (existingChiTiet != null)
                {
                    // Cập nhật số lượng
                    existingChiTiet.SoLuong += (int)numSoLuong.Value;
                    existingChiTiet.ThanhTien = existingChiTiet.SoLuong * existingChiTiet.DonGia;
                    dbContext.ChiTietHoaDons.Update(existingChiTiet);
                }
                else
                {
                    // Thêm chi tiết hóa đơn mới
                    var chiTietHoaDon = new ChiTietHoaDon
                    {
                        HoaDonID = hoaDon.ID,
                        SanPhamID = selectedSanPham.ID,
                        SoLuong = (int)numSoLuong.Value,
                        DonGia = selectedSanPham.DonGia,
                        ThanhTien = selectedSanPham.DonGia * numSoLuong.Value
                    };
                    dbContext.ChiTietHoaDons.Add(chiTietHoaDon);
                }

                // Cập nhật tổng tiền hóa đơn
                await dbContext.SaveChangesAsync();

                // Tính lại tổng tiền hóa đơn
                var tongTienHoaDon = await dbContext.ChiTietHoaDons
                    .Where(ct => ct.HoaDonID == hoaDon.ID)
                    .SumAsync(ct => ct.ThanhTien);

                hoaDon.TongTien = tongTienHoaDon;
                dbContext.HoaDons.Update(hoaDon);
                await dbContext.SaveChangesAsync();

                MessageBox.Show($"Đã thêm {numSoLuong.Value} {selectedSanPham.TenSanPham} vào hóa đơn!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form
                numSoLuong.Value = 1;

                // Gọi callback để quay lại form trước
                onSuccess?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            onCancel?.Invoke(); // Gọi callback hủy để quay lại form trước
        }

        private void DisposeResources()
        {
            dbContext?.Dispose();
        }

        private void frmThongTinDichVu_ThemDV_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeResources();
        }
    }
}
