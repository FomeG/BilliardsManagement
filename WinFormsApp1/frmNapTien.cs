using Models.HandleData;
using Models.Models;

namespace WinFormsApp1
{
    public partial class frmNapTien : Form
    {
        private readonly KhachHang? selectedKhachHang;
        private readonly TaiKhoan currentUser;
        private readonly Action? onSuccess;
        private readonly DaDBContext dbContext;

        public frmNapTien()
        {
            InitializeComponent();
            // Constructor mặc định - không nên sử dụng
            MessageBox.Show("Vui lòng chọn khách hàng trước khi nạp tiền!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
        }

        public frmNapTien(KhachHang khachHang, TaiKhoan user, Action? onSuccessCallback = null)
        {
            InitializeComponent();
            selectedKhachHang = khachHang;
            currentUser = user;
            onSuccess = onSuccessCallback;
            dbContext = new DaDBContext();

            SetupForm();
            SetupEvents();
        }

        private void SetupForm()
        {
            if (selectedKhachHang != null)
            {
                this.Text = "Nạp tiền cho khách hàng";
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                // Hiển thị thông tin khách hàng
                lblKhachHang.Text = $"Khách hàng: {selectedKhachHang.HoTen}";
                lblSoTienHienTai.Text = $"Số tiền hiện tại: {selectedKhachHang.SoTienConLai:N0} VNĐ";

                // Focus vào textbox số tiền
                txtSoTienNap.Focus();
            }
        }

        private void SetupEvents()
        {
            // Thêm event cho nút nạp tiền
            btnNapTien.Click += BtnNapTien_Click;

            // Thêm event cho Enter key
            txtSoTienNap.KeyPress += TxtSoTienNap_KeyPress;

            // Chỉ cho phép nhập số
            txtSoTienNap.KeyPress += (sender, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
        }

        private void TxtSoTienNap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnNapTien_Click(sender, e);
            }
        }

        private async void BtnNapTien_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtSoTienNap.Text))
                {
                    MessageBox.Show("Vui lòng nhập số tiền cần nạp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTienNap.Focus();
                    return;
                }

                if (!decimal.TryParse(txtSoTienNap.Text, out decimal soTienNap) || soTienNap <= 0)
                {
                    MessageBox.Show("Số tiền nạp phải là số dương!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTienNap.Focus();
                    return;
                }

                if (soTienNap > 10000000) // Giới hạn 10 triệu
                {
                    MessageBox.Show("Số tiền nạp không được vượt quá 10,000,000 VNĐ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTienNap.Focus();
                    return;
                }

                // Xác nhận nạp tiền
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn nạp {soTienNap:N0} VNĐ cho khách hàng {selectedKhachHang?.HoTen}?",
                    "Xác nhận nạp tiền",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await ProcessNapTien(soTienNap);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nạp tiền: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ProcessNapTien(decimal soTienNap)
        {
            try
            {
                if (selectedKhachHang == null) return;

                // Disable nút để tránh click nhiều lần
                btnNapTien.Enabled = false;
                btnNapTien.Text = "Đang xử lý...";

                // Lấy khách hàng từ database để đảm bảo dữ liệu mới nhất
                var khachHang = await dbContext.KhachHangs.FindAsync(selectedKhachHang.ID);
                if (khachHang == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin khách hàng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cập nhật số tiền còn lại
                khachHang.SoTienConLai += soTienNap;

                // Tạo bản ghi nạp tiền
                var napGio = new NapGio
                {
                    KhachHangID = khachHang.ID,
                    NhanVienID = currentUser.ID,
                    SoTienNap = soTienNap,
                    NgayNap = DateTime.Now
                };

                // Lưu vào database
                dbContext.KhachHangs.Update(khachHang);
                dbContext.NapGios.Add(napGio);
                await dbContext.SaveChangesAsync();

                MessageBox.Show(
                    $"Nạp tiền thành công!\n" +
                    $"Số tiền nạp: {soTienNap:N0} VNĐ\n" +
                    $"Số tiền còn lại: {khachHang.SoTienConLai:N0} VNĐ",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Gọi callback để refresh dữ liệu
                onSuccess?.Invoke();

                // Đóng form
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý nạp tiền: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Khôi phục nút
                btnNapTien.Enabled = true;
                btnNapTien.Text = "Nạp";
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            dbContext?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
