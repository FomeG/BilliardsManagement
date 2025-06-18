using Microsoft.EntityFrameworkCore;
using Models.HandleData;
using Models.Models;
using System.Data;

namespace WinFormsApp1
{
    public partial class frmTrangChu : Form
    {
        private TaiKhoan currentUser;
        private DaDBContext dbContext;
        private Ban? currentSelectedBan; // Lưu thông tin bàn hiện tại được chọn
        private System.Windows.Forms.Timer? phienChoiCheckTimer; // Timer để check phiên chơi hết hạn

        // Lưu trữ nội dung trang chủ để restore lại
        private Form? currentChildForm; // Form con hiện tại đang hiển thị

        public frmTrangChu(TaiKhoan user)
        {
            InitializeComponent();
            currentUser = user;
            dbContext = new DaDBContext();
            SetupUserInterface();


            TaiDulieuTrangChu();

            // Thiết lập timer cho check phiên chơi hết hạn
            SetupPhienChoiCheckTimer();

            // Thiết lập menu navigation
            SetupMenuNavigation();

            ShowHomePageControls();
        }


        // setup thông tin người dùng trên giao diện
        private void SetupUserInterface()
        {
            btnSaveBan.Enabled = false; // Tắt nút Cập nhật
            btnDeleteBan.Enabled = false; // Tắt nút Xóa bàn
            // Thiết lập tiêu đề form
            this.Text = "Hệ Thống Quản Lý Quán Bida";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Hiển thị thông tin người dùng trong StatusBar
            lblUserName.Text = $"Người dùng: {currentUser.HoTen}";

            // Thiết lập quyền truy cập dựa trên vai trò
            if (currentUser.VaiTro == "Admin")
            {
                lblUserRole.Text = "Vai trò: Quản trị viên";
                // Bật các chức năng admin
                BatChucNangAdmin();
            }
            else
            {
                lblUserRole.Text = "Vai trò: Nhân viên";
                // Tắt các chức năng admin
                TatChucNangAdmin();
            }

            // Cập nhật thời gian
            UpdateDateTime();

            // Setup responsive layout
            SetupResponsiveLayout();
        }

        private void SetupResponsiveLayout()
        {
            this.Resize += Form2_Resize;
            this.Load += Form2_Load;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Thiết lập layout ban đầu khi form load
            HandleTabChanged();
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            // Cập nhật layout khi form resize
            if (tabControl1 != null && tabControl1.SelectedTab != null)
            {
                int currentPanelHeight = panel1.Height;
                UpdateResponsiveLayout(currentPanelHeight);
            }
        }

        private void BatChucNangAdmin()
        {
            // Bật các chức năng dành cho admin
            // Menu items
            đồĂnThứcUốngToolStripMenuItem.Visible = true;  // Quản lý sản phẩm
            thốngKêToolStripMenuItem.Visible = true;       // Thống kê doanh thu
            quảnLýTàiKhoảnToolStripMenuItem.Visible = true; // Quản lý tài khoản

            // Buttons CRUD cho bàn (trong tab Bàn)
            btnSaveBan.Visible = true;
            btnDeleteBan.Visible = true;
            btnNewBan.Visible = true;

            // Buttons CRUD cho thành viên (trong tab Thành viên)
            btnThemTV.Visible = true;
            btnXoaTV.Visible = true;
            btnSuaTV.Visible = true;

            // Cho phép chỉnh sửa thông tin bàn và thành viên
            txtBanID.ReadOnly = false;
            txtTenBan.ReadOnly = false;
            txtLoaiBan.ReadOnly = false;
            txtGiaTheoGio.ReadOnly = false;
        }

        private void TatChucNangAdmin()
        {
            // Tắt các chức năng admin - chỉ để lại tính năng cơ bản cho staff
            // Ẩn menu items dành cho admin
            đồĂnThứcUốngToolStripMenuItem.Visible = false;  // Không được quản lý sản phẩm
            thốngKêToolStripMenuItem.Visible = false;       // Không được xem thống kê
            quảnLýTàiKhoảnToolStripMenuItem.Visible = false; // Không được quản lý tài khoản

            // Ẩn buttons CRUD cho bàn
            btnSaveBan.Visible = false;
            btnDeleteBan.Visible = false;
            btnNewBan.Visible = false;

            // Ẩn buttons CRUD cho thành viên (chỉ để lại thêm thành viên)
            btnXoaTV.Visible = false;  // Không được xóa thành viên
            btnSuaTV.Visible = false;  // Không được sửa thành viên
            btnThemTV.Visible = true;  // Được phép thêm thành viên mới

            // Không cho phép chỉnh sửa thông tin bàn
            txtBanID.ReadOnly = true;
            txtTenBan.ReadOnly = true;
            txtLoaiBan.ReadOnly = true;
            txtGiaTheoGio.ReadOnly = true;

            // Staff vẫn có thể:
            // - Đăng ký thành viên mới (btnThemTV)
            // - Nạp tiền cho khách (button nạp tiền)
            // - Đặt bàn cho khách (btnDatBan)
            // - Xem bàn trống/đang sử dụng (tableLayoutPanel1)
            // - Tính tiền dịch vụ (thông qua form dịch vụ)
        }

        private void UpdateDateTime()
        {
            // Cập nhật thời gian hiện tại
            lblDateTime.Text = $"Ngày giờ: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private async void TaiDulieuTrangChu()
        {
            // Load danh sách bàn vào TableLayoutPanel
            await TaiDulieuBan();

            // Load danh sách thành viên vào DataGridView
            await TaiDulieuKH();

            // Setup TabControl event để handle resize
            SetupTabControlEvents();

        }

        private void SetupTabControlEvents()
        {
            // Thêm event handler cho TabControl khi chuyển tab
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý thay đổi kích thước panel1 và responsive layout
            HandleTabChanged();
        }

        private void HandleTabChanged()
        {
            int newPanelHeight;

            // Xác định chiều cao mới cho panel1 dựa trên tab được chọn
            if (tabControl1.SelectedTab == tabPageBan)
            {
                // Tab Bàn - kích thước nhỏ
                newPanelHeight = 110;
            }
            else
            {
                // Tab Thành viên hoặc Tài khoản - kích thước lớn
                newPanelHeight = 300;
            }

            // Thay đổi kích thước panel1
            ResizePanel1(newPanelHeight);

            // Cập nhật layout responsive cho các controls khác
            UpdateResponsiveLayout(newPanelHeight);
        }

        private void ResizePanel1(int newHeight)
        {
            // Thay đổi kích thước panel1
            panel1.Height = newHeight;
        }



        // phần này liên quan đến thay đổi kích thước của table layout
        private void UpdateResponsiveLayout(int panelHeight)
        {
            // Tính toán vị trí và kích thước mới cho TableLayoutPanel và GroupBox
            int spacing = 10; // Khoảng cách giữa các controls
            int newTopPosition = panel1.Bottom + spacing;

            int availableHeight = this.ClientSize.Height - newTopPosition - statusStrip.Height - spacing;

            if (tableLayoutPanel1 != null)
            {
                tableLayoutPanel1.Top = newTopPosition;
                tableLayoutPanel1.Height = availableHeight;
            }


            if (groupBox1 != null)
            {
                groupBox1.Top = newTopPosition;
                groupBox1.Height = availableHeight;
            }
        }



        // đăng xuất
        private void LogOut()
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dbContext?.Dispose(); // Giải phóng DbContext
                this.Close(); // Đóng MainForm, sẽ trigger FormClosed event để hiển thị lại form dang nhajap
            }
        }

        #region Ban Management

        private async Task TaiDulieuBan()
        {
            try
            {
                List<Ban> bans;

                // Sử dụng context riêng để tránh threading issues
                using (var loadContext = new DaDBContext())
                {
                    // Lấy danh sách bàn từ database (chỉ lấy bàn chưa bị xóa)
                    bans = await loadContext.Bans
                        .Where(b => !b.IsDeleted)
                        .OrderBy(b => b.ID)
                        .ToListAsync();
                }

                // Đảm bảo UI operations chạy trên UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(() => UpdateBanUI(bans));
                }
                else
                {
                    UpdateBanUI(bans);
                }
            }
            catch (Exception ex)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(() => MessageBox.Show($"Lỗi khi tải dữ liệu bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error));
                }
                else
                {
                    MessageBox.Show($"Lỗi khi tải dữ liệu bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateBanUI(List<Ban> bans)
        {
            // Xóa các control cũ trong TableLayoutPanel
            tableLayoutPanel1.Controls.Clear();

            // Tạo nút cho mỗi bàn
            foreach (var ban in bans)
            {
                CreateBanButton(ban);
            }
        }

        private void CreateBanButton(Ban ban)
        {
            // Tạo button cho bàn
            Button btnBan = new Button();
            btnBan.Text = ban.TenBan;
            btnBan.Tag = ban; // Lưu thông tin bàn vào Tag
            btnBan.Dock = DockStyle.Fill;
            btnBan.Font = new Font("Arial", 10, FontStyle.Bold);
            btnBan.FlatStyle = FlatStyle.Flat;
            btnBan.FlatAppearance.BorderSize = 2;
            btnBan.FlatAppearance.BorderColor = Color.Black;

            // Đặt màu theo trạng thái
            SetBanButtonColor(btnBan, ban.TrangThai);

            // Thêm sự kiện click cho tất cả các bàn
            btnBan.Click += BtnBan_Click;

            // Thêm vào TableLayoutPanel
            tableLayoutPanel1.Controls.Add(btnBan);
        }

        private void SetBanButtonColor(Button button, int trangThai)
        {
            switch (trangThai)
            {
                case 0: // Đang có người
                    button.BackColor = Color.Red;
                    button.ForeColor = Color.White;
                    break;
                case 1: // Trống
                    button.BackColor = Color.Green;
                    button.ForeColor = Color.White;
                    break;
                case 2: // Bảo trì
                    button.BackColor = Color.Yellow;
                    button.ForeColor = Color.Black;
                    break;
                default:
                    button.BackColor = Color.Gray;
                    button.ForeColor = Color.White;
                    break;
            }
        }

        private void BtnBan_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is Ban ban)
            {
                // Xử lý khi click vào bàn
                HandleBanClick(ban);
            }
        }

        private void HandleBanClick(Ban ban)
        {


            // Xử lý theo trạng thái bàn
            switch (ban.TrangThai)
            {
                case 0: // Bàn đang có người - hiển thị thông tin dịch vụ
                    ShowThongTinDichVu(ban);


                    btnDeleteBan.Enabled = false; // Bật nút Xóa bàn
                    btnSaveBan.Enabled = false; // Bật nút Cập nhật thông tin bàn
                    btnNewBan.Enabled = false; // Tắt nút Thêm mới khi đã chọn bàn

                    button2.Enabled = false; // tắt nút Bảo trì
                    button2.Visible = false; // ẩn nút Bảo trì

                    txtTenBan.Text = ban.TenBan;
                    txtBanID.Text = ban.ID.ToString();
                    txtLoaiBan.Text = ban.LoaiBan;
                    txtGiaTheoGio.Text = ban.GiaTheoGio.ToString();
                    break;
                case 1: // Bàn trống - hiển thị nút đặt bàn và bảo trì
                    ShowBanTrongOptions(ban);


                    btnDeleteBan.Enabled = true; // Bật nút Xóa bàn
                    btnSaveBan.Enabled = true; // Bật nút Cập nhật thông tin bàn
                    btnNewBan.Enabled = false; // Tắt nút Thêm mới khi đã chọn bàn


                    button2.Enabled = false; // tắt nút Bảo trì
                    button2.Visible = false; // ẩn nút Bảo trì


                    txtTenBan.Text = ban.TenBan;
                    txtBanID.Text = ban.ID.ToString();
                    txtLoaiBan.Text = ban.LoaiBan;
                    txtGiaTheoGio.Text = ban.GiaTheoGio.ToString();
                    break;
                case 2: // Bàn bảo trì - có thể thêm xử lý sau

                    ShowBanTrongOptions(ban);



                    btnDeleteBan.Enabled = true; // Bật nút Xóa bàn
                    btnSaveBan.Enabled = true; // Bật nút Cập nhật thông tin bàn
                    btnNewBan.Enabled = false; // Tắt nút Thêm mới khi đã chọn bàn


                    button2.Enabled = true; // Bật nút Bảo trì
                    button2.Visible = true; // Hiển thị nút Bảo trì


                    currentSelectedBan = ban; // Lưu thông tin bàn hiện tại
                    txtTenBan.Text = ban.TenBan;
                    txtBanID.Text = ban.ID.ToString();
                    txtLoaiBan.Text = ban.LoaiBan;
                    txtGiaTheoGio.Text = ban.GiaTheoGio.ToString();
                    break;
                default:
                    // Chỉ bàn có trạng thái "không xác định" mới cho phép chỉnh sửa thông tin
                    LoadBanToForm(ban);
                    tabControl1.SelectedTab = tabPageBan;
                    break;
            }
        }

        private void ShowBanTrongOptions(Ban ban)
        {

            if (ban.TrangThai != 2)
            {
                // Lưu thông tin bàn hiện tại
                currentSelectedBan = ban;

                // Xóa nội dung hiện tại trong pnChiTiet
                pnChiTiet.Controls.Clear();

                // Hiển thị và bật nút đặt bàn và bảo trì
                btnDatBan.Visible = true;
                btnDatBan.Enabled = true;
                btnDatBan.Text = $"Đặt bàn {ban.TenBan}";

                btnBaoTri.Visible = true;
                btnBaoTri.Enabled = true;
                btnBaoTri.Text = $"Bảo trì {ban.TenBan}";


                // Thêm lại các nút vào panel
                pnChiTiet.Controls.Add(btnDatBan);
                pnChiTiet.Controls.Add(btnBaoTri);
            }
            else
            {
                // Lưu thông tin bàn hiện tại
                currentSelectedBan = ban;

                // Xóa nội dung hiện tại trong pnChiTiet
                pnChiTiet.Controls.Clear();

                // Hiển thị và bật nút đặt bàn và bảo trì
                btnDatBan.Visible = true;
                btnDatBan.Enabled = false;
                btnDatBan.Text = $"Bàn này đang được bảo trì";

                btnBaoTri.Visible = false;
                btnBaoTri.Enabled = false;
                btnBaoTri.Text = $"Bảo trì {ban.TenBan}";


                // Thêm lại các nút vào panel
                pnChiTiet.Controls.Add(btnDatBan);
                pnChiTiet.Controls.Add(btnBaoTri);
            }
        }

        private void ShowThongTinDichVu(Ban ban)
        {
            // Lưu thông tin bàn hiện tại
            currentSelectedBan = ban;

            // Xóa nội dung hiện tại trong pnChiTiet
            pnChiTiet.Controls.Clear();

            // Tạo và hiển thị form thông tin dịch vụ
            var frmThongTin = new frmThongTinDichVu(ban, currentUser, OnThongTinDichVuClose)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnChiTiet.Controls.Add(frmThongTin);
            frmThongTin.Show();
        }

        private void LoadBanToForm(Ban ban)
        {
            txtBanID.Text = ban.ID.ToString();
            txtTenBan.Text = ban.TenBan;
            txtLoaiBan.Text = ban.LoaiBan;
            txtGiaTheoGio.Text = ban.GiaTheoGio.ToString();
            // Bỏ combobox trạng thái - trạng thái sẽ được quản lý tự động
            btnNewBan.Enabled = false; // Tắt nút Thêm mới khi đã chọn bàn
            btnSaveBan.Enabled = true; // Bật nút Cập nhật
            btnDeleteBan.Enabled = true; // Bật nút Xóa bàn
        }

        private string GetTrangThaiText(int trangThai)
        {
            return trangThai switch
            {
                0 => "Đang có người",
                1 => "Trống",
                2 => "Bảo trì",
                _ => "Không xác định"
            };
        }

        public async Task ReloadDulieuBan()
        {
            // Phương thức để refresh lại dữ liệu bàn
            // Sử dụng context riêng để tránh conflict với UI thread
            await TaiDulieuBan();
        }

        private async void OnDatBanSuccess()
        {
            try
            {
                // Đảm bảo chạy trên UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(OnDatBanSuccess));
                    return;
                }

                // Xóa panel ngay lập tức
                pnChiTiet.Controls.Clear();

                // Refresh dữ liệu bàn trên UI thread
                await ReloadDulieuBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi refresh dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void OnThongTinDichVuClose()
        {
            try
            {
                // Đảm bảo chạy trên UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(OnThongTinDichVuClose));
                    return;
                }

                // Xóa panel ngay lập tức
                pnChiTiet.Controls.Clear();

                // Refresh dữ liệu bàn trên UI thread
                await ReloadDulieuBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi refresh dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ShowDatBanForm(Ban ban)
        {
            // Xóa nội dung hiện tại trong pnChiTiet
            pnChiTiet.Controls.Clear();

            // Tạo và hiển thị form đặt bàn
            var frmDatBan = new frmDatBan(ban, currentUser, OnDatBanSuccess)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnChiTiet.Controls.Add(frmDatBan);
            frmDatBan.Show();
        }

        private async Task SetBanBaoTri(Ban ban)
        {
            try
            {
                var result = MessageBox.Show($"Bạn có chắc chắn muốn chuyển {ban.TenBan} sang trạng thái bảo trì?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Tìm lại entity từ database để tránh tracking conflict
                    var banToUpdate = await dbContext.Bans.FirstOrDefaultAsync(b => b.ID == ban.ID && !b.IsDeleted);
                    if (banToUpdate != null)
                    {
                        banToUpdate.TrangThai = 2; // Bảo trì
                        await dbContext.SaveChangesAsync();

                        MessageBox.Show($"{banToUpdate.TenBan} đã được chuyển sang trạng thái bảo trì!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh dữ liệu và xóa panel
                        await ReloadDulieuBan();
                        pnChiTiet.Controls.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật trạng thái bàn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region PhienChoi Check Timer

        private void SetupPhienChoiCheckTimer()
        {
            // Thiết lập timer để check phiên chơi hết hạn mỗi 10 giây
            phienChoiCheckTimer = new System.Windows.Forms.Timer();
            phienChoiCheckTimer.Interval = 10000; // 10 giây = 10000ms
            phienChoiCheckTimer.Tick += PhienChoiCheckTimer_Tick;
            phienChoiCheckTimer.Start(); // Bắt đầu timer ngay
        }

        private async void PhienChoiCheckTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                await KiemTraPhienChoi();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi check phiên chơi hết hạn: {ex.Message}");
            }
        }

        private async Task KiemTraPhienChoi()
        {
            try
            {
                // Lấy thời gian hiện tại theo múi giờ Việt Nam (UTC+7)
                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var currentVietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

                // Tìm các phiên chơi đang hoạt động và đã hết hạn (chỉ bàn chưa bị xóa)
                var expiredPhienChois = await dbContext.PhienChois
                    .Include(p => p.Ban)
                    .Where(p => p.TrangThai == 1 && // Đang chơi
                               p.ThoiGianKetThuc.HasValue &&
                               p.ThoiGianKetThuc.Value <= currentVietnamTime &&
                               !p.Ban.IsDeleted) // Chỉ lấy phiên chơi của bàn chưa bị xóa
                    .ToListAsync();

                if (expiredPhienChois.Any())
                {
                    foreach (var phienChoi in expiredPhienChois)
                    {
                        // Cập nhật trạng thái phiên chơi thành kết thúc
                        phienChoi.TrangThai = 0; // Kết thúc
                        phienChoi.ThoiGianKetThuc = currentVietnamTime; // Cập nhật thời gian kết thúc thực tế

                        // Tính lại tổng thời gian thực tế
                        if (phienChoi.ThoiGianKetThuc.HasValue)
                        {
                            phienChoi.TongThoiGian = (phienChoi.ThoiGianKetThuc.Value - phienChoi.ThoiGianBatDau).TotalHours;
                        }

                        // Cập nhật trạng thái bàn thành trống
                        if (phienChoi.Ban != null)
                        {
                            phienChoi.Ban.TrangThai = 1; // Trống
                            dbContext.Bans.Update(phienChoi.Ban);
                        }

                        dbContext.PhienChois.Update(phienChoi);
                    }

                    // Lưu thay đổi vào database
                    await dbContext.SaveChangesAsync();

                    // Refresh UI trên main thread
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            _ = Task.Run(async () => await ReloadDulieuBan());
                        }));
                    }
                    else
                    {
                        await ReloadDulieuBan();
                    }

                    // Thông báo cho user (tùy chọn)
                    var expiredBanNames = string.Join(", ", expiredPhienChois.Select(p => p.Ban?.TenBan ?? "N/A"));
                    Console.WriteLine($"Đã tự động kết thúc phiên chơi cho các bàn: {expiredBanNames}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong KiemTraPhienChoi: {ex.Message}");
            }
        }

        #endregion

        #region Search Functionality

        private async Task TimKiemKH(string searchText)
        {
            try
            {
                List<KhachHang> khachHangs;

                using (var searchContext = new DaDBContext())
                {
                    if (string.IsNullOrEmpty(searchText))
                    {
                        khachHangs = await searchContext.KhachHangs.OrderBy(k => k.ID).ToListAsync();
                    }
                    else
                    {
                        khachHangs = await searchContext.KhachHangs
                            .Where(k => k.HoTen.Contains(searchText) ||
                                       k.DiaChi.Contains(searchText) ||
                                       k.SoDienThoai.Contains(searchText))
                            .OrderBy(k => k.ID)
                            .ToListAsync();
                    }
                }

                UpdateDataGridViewWithResults(khachHangs);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDataGridViewWithResults(List<KhachHang> khachHangs)
        {
            dataGridView1.Rows.Clear();

            foreach (var k in khachHangs)
            {
                dataGridView1.Rows.Add(
                    k.ID,
                    k.HoTen,
                    k.GioiTinh ? "Nam" : "Nữ",
                    k.NgaySinh.ToString("dd/MM/yyyy"),
                    k.SoDienThoai,
                    k.DiaChi ?? string.Empty,
                    k.NgayDangKy.ToString("dd/MM/yyyy"),
                    k.SoTienConLai.ToString("0.0")
                );
            }


            if (khachHangs.Count == 0 && !string.IsNullOrEmpty(txtTimKiem.Text.Trim()))
            {
                // Nếu dữ liệu trống (hiện chưa làm j)
            }
        }

        #endregion

        #region Thanh Vien Data Management

        private async Task TaiDulieuKH()
        {
            try
            {
                SetupDataGridViewColumns();

                using (var loadContext = new DaDBContext())
                {
                    var khachHangs = await loadContext.KhachHangs.OrderBy(k => k.ID).ToListAsync();

                    UpdateDataGridViewWithResults(khachHangs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu thành viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewColumns()
        {
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ID",
                    HeaderText = "ID",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    ReadOnly = true
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "HoTen",
                    HeaderText = "Họ tên",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 25
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "GioiTinh",
                    HeaderText = "Giới tính",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "NgaySinh",
                    HeaderText = "Ngày sinh",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "SoDienThoai",
                    HeaderText = "Số điện thoại",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 20
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "DiaChi",
                    HeaderText = "Địa chỉ",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 35
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "NgayDangKy",
                    HeaderText = "Ngày đăng ký",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "SoGioConLai",
                    HeaderText = "Tiền còn lại",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 15
                });




            }
        }



        #endregion

        #region Tab Ban Event Handlers

        private async void btnSaveBan_Click(object sender, EventArgs e)
        {
            if (!CheckQuyen("cập nhật thông tin bàn"))
                return;

            try
            {
                if (string.IsNullOrEmpty(txtBanID.Text))
                {
                    MessageBox.Show("Vui lòng chọn bàn để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int banId = int.Parse(txtBanID.Text);
                var ban = await dbContext.Bans.FirstOrDefaultAsync(b => b.ID == banId && !b.IsDeleted);

                if (ban != null)
                {
                    // Cập nhật thông tin (không cập nhật trạng thái - trạng thái được quản lý tự động)
                    ban.TenBan = txtTenBan.Text.Trim();
                    ban.LoaiBan = txtLoaiBan.Text.Trim();
                    ban.GiaTheoGio = decimal.Parse(txtGiaTheoGio.Text);
                    // Bỏ cập nhật trạng thái từ combobox

                    await dbContext.SaveChangesAsync();

                    MessageBox.Show("Cập nhật thông tin bàn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh lại danh sách bàn
                    await ReloadDulieuBan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDeleteBan_Click(object sender, EventArgs e)
        {
            if (!CheckQuyen("xóa bàn"))
                return;

            try
            {
                if (string.IsNullOrEmpty(txtBanID.Text))
                {
                    MessageBox.Show("Vui lòng chọn bàn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bàn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int banId = int.Parse(txtBanID.Text);
                    var ban = await dbContext.Bans.FirstOrDefaultAsync(b => b.ID == banId && !b.IsDeleted);

                    if (ban != null)
                    {
                        // Soft delete: đánh dấu IsDeleted = true thay vì xóa thật
                        ban.IsDeleted = true;
                        dbContext.Bans.Update(ban);
                        await dbContext.SaveChangesAsync();

                        MessageBox.Show("Xóa bàn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear form và refresh danh sách
                        XoaFormBan();
                        await ReloadDulieuBan();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnNewBan_Click(object sender, EventArgs e)
        {
            if (!CheckQuyen("thêm bàn mới"))
                return;

            try
            {
                // Validate input
                if (string.IsNullOrEmpty(txtTenBan.Text) || string.IsNullOrEmpty(txtLoaiBan.Text) || string.IsNullOrEmpty(txtGiaTheoGio.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newBan = new Ban
                {
                    TenBan = txtTenBan.Text.Trim(),
                    LoaiBan = txtLoaiBan.Text.Trim(),
                    GiaTheoGio = decimal.Parse(txtGiaTheoGio.Text),
                    TrangThai = 1
                };

                dbContext.Bans.Add(newBan);
                await dbContext.SaveChangesAsync();

                MessageBox.Show("Thêm bàn mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // xóa dữ liệu trên ô txtbox và tải lại dữ liệu 
                XoaFormBan();
                await ReloadDulieuBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaFormBan()
        {
            txtBanID.Clear();
            txtTenBan.Clear();
            txtLoaiBan.Clear();
            txtGiaTheoGio.Clear();
            btnNewBan.Enabled = true; // Bật nút Thêm mới
            btnSaveBan.Enabled = false; // Tắt nút Cập nhật
            btnDeleteBan.Enabled = false; // Tắt nút Xóa bàn
        }

        private void btnBoChon_Click(object sender, EventArgs e)
        {
            XoaFormBan();
            // MessageBox.Show("Đã bỏ chọn bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Tab Thanh Vien Event Handlers

        private bool ValidateSoDienThoai(string soDienThoai)
        {
            // Kiểm tra null hoặc empty
            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Loại bỏ khoảng trắng
            soDienThoai = soDienThoai.Trim();

            // Kiểm tra độ dài (10-11 số)
            if (soDienThoai.Length < 10 || soDienThoai.Length > 11)
            {
                MessageBox.Show("Số điện thoại phải có 10-11 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra chỉ chứa số
            if (!soDienThoai.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa các chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra đầu số hợp lệ (bắt đầu bằng 0)
            if (!soDienThoai.StartsWith("0"))
            {
                MessageBox.Show("Số điện thoại phải bắt đầu bằng số 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra các đầu số phổ biến tại Việt Nam
            string[] validPrefixes = { "032", "033", "034", "035", "036", "037", "038", "039", // Viettel
                                     "070", "079", "077", "076", "078", // Mobifone
                                     "083", "084", "085", "081", "082", // Vinaphone
                                     "056", "058", // Vietnamobile
                                     "059", // Gmobile
                                     "090", "093", "089" }; // Các mạng khác

            bool hasValidPrefix = validPrefixes.Any(prefix => soDienThoai.StartsWith(prefix));
            if (!hasValidPrefix)
            {
                MessageBox.Show("Đầu số điện thoại không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async void btnThemTV_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(txtTenThanhVien.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate số điện thoại
                if (!ValidateSoDienThoai(txtSDT.Text))
                {
                    return;
                }

                // Check if phone number already exists
                bool phoneExists = await dbContext.KhachHangs.AnyAsync(k => k.SoDienThoai == txtSDT.Text.Trim());
                if (phoneExists)
                {
                    MessageBox.Show("Số điện thoại đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newKhachHang = new KhachHang
                {
                    HoTen = txtTenThanhVien.Text.Trim(),
                    GioiTinh = rdNam.Checked,
                    NgaySinh = dateTimePicker1.Value.Date,
                    SoDienThoai = txtSDT.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    SoTienConLai = string.IsNullOrEmpty(txtGioConLai.Text) ? 0 : decimal.Parse(txtGioConLai.Text)
                };

                dbContext.KhachHangs.Add(newKhachHang);
                await dbContext.SaveChangesAsync();

                MessageBox.Show("Thêm thành viên mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear form và refresh danh sách
                ClearThanhVienForm();
                await RefreshDuLieuKH();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm thành viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSuaTV_Click(object sender, EventArgs e)
        {
            if (!CheckQuyen("cập nhật thông tin thành viên"))
                return;

            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng chọn thành viên để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int khachHangId = int.Parse(textBox1.Text);
                var khachHang = await dbContext.KhachHangs.FindAsync(khachHangId);

                if (khachHang != null)
                {
                    // Validate họ tên
                    if (string.IsNullOrEmpty(txtTenThanhVien.Text.Trim()))
                    {
                        MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validate số điện thoại
                    if (!ValidateSoDienThoai(txtSDT.Text))
                    {
                        return;
                    }

                    // Check if phone number already exists (exclude current record)
                    bool phoneExists = await dbContext.KhachHangs.AnyAsync(k => k.SoDienThoai == txtSDT.Text.Trim() && k.ID != khachHangId);
                    if (phoneExists)
                    {
                        MessageBox.Show("Số điện thoại đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cập nhật thông tin
                    khachHang.HoTen = txtTenThanhVien.Text.Trim();
                    khachHang.GioiTinh = rdNam.Checked;
                    khachHang.NgaySinh = dateTimePicker1.Value.Date;
                    khachHang.SoDienThoai = txtSDT.Text.Trim();
                    khachHang.DiaChi = txtDiaChi.Text.Trim();
                    khachHang.SoTienConLai = string.IsNullOrEmpty(txtGioConLai.Text) ? 0 : decimal.Parse(txtGioConLai.Text);

                    await dbContext.SaveChangesAsync();

                    MessageBox.Show("Cập nhật thông tin thành viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh lại danh sách
                    await RefreshDuLieuKH();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thành viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoaTV_Click(object sender, EventArgs e)
        {
            if (!CheckQuyen("xóa thành viên"))
                return;

            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng chọn thành viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thành viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int khachHangId = int.Parse(textBox1.Text);
                    var khachHang = await dbContext.KhachHangs.FindAsync(khachHangId);

                    if (khachHang != null)
                    {
                        dbContext.KhachHangs.Remove(khachHang);
                        await dbContext.SaveChangesAsync();

                        MessageBox.Show("Xóa thành viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear form và refresh danh sách
                        ClearThanhVienForm();
                        await RefreshDuLieuKH();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa thành viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoChontv_Click(object sender, EventArgs e)
        {
            // Xóa hết thông tin trên form thành viên (bỏ chọn)
            ClearThanhVienForm();

            btnXoaTV.Enabled = false; // Tắt nút Xóa thành viên
            btnSuaTV.Enabled = false; // Tắt nút Sửa thành viên
            btnThemTV.Enabled = true; // Bật nút Thêm thành viên
        }

        private void ClearThanhVienForm()
        {
            textBox1.Clear();
            txtTenThanhVien.Clear();
            rdNam.Checked = true;
            rdNu.Checked = false;
            dateTimePicker1.Value = DateTime.Now.AddYears(-18); // Default 18 tuổi
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtGioConLai.Text = "0";
            lblNgayDangKy.Text = string.Empty;

            // Clear selection in DataGridView
            dataGridView1.ClearSelection();
        }

        private async Task RefreshDuLieuKH()
        {
            string currentSearch = txtTimKiem.Text.Trim();
            await TimKiemKH(currentSearch);
        }

        #endregion

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            phienChoiCheckTimer?.Stop();
            phienChoiCheckTimer?.Dispose();

            dbContext?.Dispose();
            base.OnFormClosed(e);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var dong = dataGridView1.Rows[e.RowIndex];

                if (e.RowIndex >= 0)
                {

                    textBox1.Text = dong.Cells[0].Value.ToString();
                    txtTenThanhVien.Text = dong.Cells[1].Value.ToString();

                    if (dong.Cells[2].Value.ToString() == "Nam")
                    {
                        rdNam.Checked = true;
                    }
                    else
                    {
                        rdNu.Checked = true;
                    }


                    dateTimePicker1.Value = DateTime.ParseExact(dong.Cells[3].Value.ToString(), "dd/MM/yyyy", null);
                    txtSDT.Text = dong.Cells[4].Value.ToString();
                    txtDiaChi.Text = dong.Cells[5].Value.ToString() ?? string.Empty;
                    txtGioConLai.Text = dong.Cells[7].Value.ToString();
                    lblNgayDangKy.Text = dong.Cells[6].Value.ToString();



                }

                // Khi chọn dữ liệu: disable nút Thêm, enable nút Sửa và Xóa
                btnThemTV.Enabled = false;
                btnSuaTV.Enabled = true;
                btnXoaTV.Enabled = true;
            }
            else
            {
                // Khi không chọn dữ liệu: enable nút Thêm, disable nút Sửa và Xóa
                btnThemTV.Enabled = true;
                btnSuaTV.Enabled = false;
                btnXoaTV.Enabled = false;
            }
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có khách hàng nào được chọn không
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để nạp tiền!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy thông tin khách hàng được chọn
                var selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow.Cells["ID"].Value != null)
                {
                    int khachHangId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                    // Tìm khách hàng trong database
                    var khachHang = dbContext.KhachHangs.Find(khachHangId);
                    if (khachHang != null)
                    {
                        // Mở form nạp tiền với callback để refresh dữ liệu và reset form
                        var frmNap = new frmNapTien(khachHang, currentUser, async () =>
                        {
                            await RefreshDuLieuKH();
                            // Reset form thành viên để hiển thị dữ liệu mới nhất
                            ClearThanhVienForm();
                        });
                        frmNap.ShowDialog(this);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form nạp tiền: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TaiDulieuTrangChu();
        }

        #region Menu Navigation

        private void SetupMenuNavigation()
        {
            // gắn sự kiện click cho mấy cái menu ở trên dầu teien
            trangChủToolStripMenuItem.Click += TrangChủToolStripMenuItem_Click;
            đồĂnThứcUốngToolStripMenuItem.Click += ĐồĂnThứcUốngToolStripMenuItem_Click;
            thốngKêToolStripMenuItem.Click += ThốngKêToolStripMenuItem_Click;
            quảnLýTàiKhoảnToolStripMenuItem.Click += QuảnLýTàiKhoảnToolStripMenuItem_Click;
            đăngXuấtToolStripMenuItem.Click += ĐăngXuấtToolStripMenuItem_Click;
        }



        private void TrangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHomePage();
        }

        private void ĐồĂnThứcUốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckQuyen("quản lý sản phẩm"))
            {
                ShowSanPhamForm();
            }
        }

        private void ThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckQuyen("xem thống kê doanh thu"))
            {
                ShowThongKeForm();
            }
        }

        private void QuảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckQuyen("quản lý tài khoản"))
            {
                ShowQuanLyTaiKhoanForm();
            }
        }

        private void ĐăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogOut();
        }

        private void ShowHomePage()
        {
            // Đóng form con hiện tại nếu có
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
                currentChildForm = null;
            }

            // Hiện lại panel trang chủ và ẩn các controls khác
            ShowHomePageControls();
        }

        private void ShowSanPhamForm()
        {
            ShowChildForm(new frmSanPham(dbContext));
        }

        private void ShowThongKeForm()
        {
            ShowChildForm(new frmThongKe(dbContext, currentUser));
        }

        private void ShowQuanLyTaiKhoanForm()
        {
            ShowChildForm(new frmQuanLyTaiKhoan(dbContext));
        }

        private void ShowChildForm(Form childForm)
        {
            // Đóng form con hiện tại nếu có
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
            }

            // Ẩn các controls trang chủ
            HideHomePageControls();

            // Thiết lập form con
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Thêm form con vào panel
            pnHienThi.Controls.Add(childForm);
            childForm.Show();
            childForm.BringToFront();
        }

        private void ShowHomePageControls()
        {
            // Ẩn form con hiện tại nếu có
            if (currentChildForm != null)
            {
                currentChildForm.Visible = false;
            }

            // Hiện lại các controls trang chủ
            panel1.Visible = true;
            tableLayoutPanel1.Visible = true;
            groupBox1.Visible = true;

            panel1.BringToFront();
            tableLayoutPanel1.BringToFront();
            groupBox1.BringToFront();
        }

        private void HideHomePageControls()
        {
            // Ẩn các controls trang chủ
            panel1.Visible = false;
            tableLayoutPanel1.Visible = false;
            groupBox1.Visible = false;
        }

        private bool CheckQuyen(string feature)
        {
            if (currentUser.VaiTro != "Admin")
            {
                MessageBox.Show($"Bạn không có quyền {feature}!\nChỉ Admin mới có thể thực hiện chức năng này.",
                    "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        #endregion

        private async void btnDatBan_Click_1(object sender, EventArgs e)
        {
            if (currentSelectedBan != null)
            {
                ShowDatBanForm(currentSelectedBan);
            }
        }

        private async void btnBaoTri_Click_1(object sender, EventArgs e)
        {
            if (currentSelectedBan != null)
            {
                await SetBanBaoTri(currentSelectedBan);
            }
        }




        // nút hoàn tất bảo trì bàn
        private async void button2_Click(object sender, EventArgs e)
        {
            if (currentSelectedBan != null)
            {
                try
                {
                    var result = MessageBox.Show($"Bạn có chắc chắn hoàn tất bảo trì?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Tìm lại entity từ database để tránh tracking conflict
                        var banCanSua = await dbContext.Bans.FirstOrDefaultAsync(b => b.ID == currentSelectedBan.ID && !b.IsDeleted);
                        if (banCanSua != null)
                        {
                            banCanSua.TrangThai = 1; // hoàn tất bảo trì -> đổi trạng thái bàn sang trống người
                            await dbContext.SaveChangesAsync();

                            MessageBox.Show($"{banCanSua.TenBan} đã được hoàn tất bảo trì!",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh dữ liệu và xóa panel
                            await ReloadDulieuBan();
                            pnChiTiet.Controls.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật trạng thái bàn: {ex.Message}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void txtTimKiem_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                List<KhachHang> khachHangs;

                using (var searchContext = new DaDBContext())
                {
                    if (string.IsNullOrEmpty(txtTimKiem.Text))
                    {
                        khachHangs = await searchContext.KhachHangs.OrderBy(k => k.ID).ToListAsync();
                    }
                    else
                    {
                        khachHangs = await searchContext.KhachHangs
                            .Where(k => k.HoTen.Contains(txtTimKiem.Text) ||
                                       k.DiaChi.Contains(txtTimKiem.Text) ||
                                       k.SoDienThoai.Contains(txtTimKiem.Text))
                            .OrderBy(k => k.ID)
                            .ToListAsync();
                    }
                }

                UpdateDataGridViewWithResults(khachHangs);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
