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
        private System.Windows.Forms.Timer? searchTimer;
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
            LoadDefaultContent();

            // Thiết lập timer cho check phiên chơi hết hạn
            SetupPhienChoiCheckTimer();

            // Thiết lập menu navigation
            SetupMenuNavigation();

            // Đảm bảo hiển thị trang chủ ban đầu
            ShowHomePageControls();
        }

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
                EnableAdminFeatures();
            }
            else
            {
                lblUserRole.Text = "Vai trò: Nhân viên";
                // Tắt các chức năng admin
                DisableAdminFeatures();
            }

            // Cập nhật thời gian
            UpdateDateTime();

            // Setup responsive layout
            SetupResponsiveLayout();
        }

        private void SetupResponsiveLayout()
        {
            // Thêm event handler cho form resize
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

        private void EnableAdminFeatures()
        {
            // Bật các chức năng dành cho admin
            // Ví dụ: quản lý tài khoản, báo cáo tổng hợp, etc.
        }

        private void DisableAdminFeatures()
        {
            // Tắt các chức năng admin
            // Ví dụ: ẩn menu quản trị, etc.
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

        private async void LoadDefaultContent()
        {
            // Load danh sách bàn vào TableLayoutPanel
            await LoadBanData();

            // Load danh sách thành viên vào DataGridView
            await LoadThanhVienData();

            // Setup TabControl event để handle resize
            SetupTabControlEvents();

            // Setup search functionality
            SetupSearchFunctionality();
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

        private async Task LoadBanData()
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
                    break;
                case 1: // Bàn trống - hiển thị nút đặt bàn và bảo trì
                    ShowBanTrongOptions(ban);
                    break;
                case 2: // Bàn bảo trì - có thể thêm xử lý sau
                    MessageBox.Show("Bàn đang trong trạng thái bảo trì!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public async Task RefreshBanData()
        {
            // Phương thức để refresh lại dữ liệu bàn
            // Sử dụng context riêng để tránh conflict với UI thread
            await LoadBanData();
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
                await RefreshBanData();
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
                await RefreshBanData();
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
                    ban.TrangThai = 2; // Bảo trì
                    dbContext.Bans.Update(ban);
                    await dbContext.SaveChangesAsync();

                    MessageBox.Show($"{ban.TenBan} đã được chuyển sang trạng thái bảo trì!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh dữ liệu và xóa panel
                    await RefreshBanData();
                    pnChiTiet.Controls.Clear();
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
                await CheckExpiredPhienChoi();
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không hiển thị MessageBox để tránh spam user
                Console.WriteLine($"Lỗi khi check phiên chơi hết hạn: {ex.Message}");
            }
        }

        private async Task CheckExpiredPhienChoi()
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
                            _ = Task.Run(async () => await RefreshBanData());
                        }));
                    }
                    else
                    {
                        await RefreshBanData();
                    }

                    // Thông báo cho user (tùy chọn)
                    var expiredBanNames = string.Join(", ", expiredPhienChois.Select(p => p.Ban?.TenBan ?? "N/A"));
                    Console.WriteLine($"Đã tự động kết thúc phiên chơi cho các bàn: {expiredBanNames}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong CheckExpiredPhienChoi: {ex.Message}");
            }
        }

        #endregion

        #region Search Functionality

        private void SetupSearchFunctionality()
        {
            // Setup search timer for debouncing
            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300; // 300ms delay
            searchTimer.Tick += SearchTimer_Tick;

            // Add event handler for search textbox
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            txtTimKiem.PlaceholderText = "Tìm kiếm theo tên, địa chỉ, số điện thoại...";
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Stop previous timer and start new one (debouncing)
            searchTimer.Stop();
            searchTimer.Start();
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            // Stop timer and perform search
            searchTimer.Stop();
            await PerformSearch(txtTimKiem.Text.Trim());
        }

        private async Task PerformSearch(string searchText)
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
                        // Tìm kiếm bằng tên, sdt, địa chỉ
                        khachHangs = await searchContext.KhachHangs
                            .Where(k => k.HoTen.Contains(searchText) ||
                                       k.DiaChi.Contains(searchText) ||
                                       k.SoDienThoai.Contains(searchText))
                            .OrderBy(k => k.ID)
                            .ToListAsync();
                    }
                }

                // Update DataGridView with search results
                UpdateDataGridViewWithResults(khachHangs);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDataGridViewWithResults(List<KhachHang> khachHangs)
        {
            // Clear existing data
            dataGridView1.Rows.Clear();

            // Add search results
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

        private async Task LoadThanhVienData()
        {
            try
            {
                // Setup DataGridView columns first
                SetupDataGridViewColumns();

                // Load all data initially using separate context
                using (var loadContext = new DaDBContext())
                {
                    var khachHangs = await loadContext.KhachHangs.OrderBy(k => k.ID).ToListAsync();

                    // Update DataGridView with data
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
                // Clear any existing columns and data
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                // Disable auto-generate columns
                dataGridView1.AutoGenerateColumns = false;

                // Add columns manually
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

                // cài đặt các thuộc tính của datagridview
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.ReadOnly = true;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.ScrollBars = ScrollBars.Both;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

  
            }
        }

  

        private async void LoadThanhVienToForm(DataGridViewRow row)
        {
            try
            {
                if (row.Cells["ID"].Value != null)
                {
                    int khachHangId = Convert.ToInt32(row.Cells["ID"].Value);
                    var khachHang = await dbContext.KhachHangs.FindAsync(khachHangId);

                    if (khachHang != null)
                    {
                        textBox1.Text = khachHang.ID.ToString();
                        txtTenThanhVien.Text = khachHang.HoTen;
                        rdNam.Checked = khachHang.GioiTinh;
                        rdNu.Checked = !khachHang.GioiTinh;
                        dateTimePicker1.Value = khachHang.NgaySinh;
                        txtSDT.Text = khachHang.SoDienThoai;
                        txtDiaChi.Text = khachHang.DiaChi ?? string.Empty;
                        txtGioConLai.Text = khachHang.SoTienConLai.ToString();
                        lblNgayDangKy.Text = khachHang.NgayDangKy.ToString("dd/MM/yyyy HH:mm");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thông tin thành viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Tab Ban Event Handlers

        private async void btnSaveBan_Click(object sender, EventArgs e)
        {
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
                    await RefreshBanData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDeleteBan_Click(object sender, EventArgs e)
        {
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
                        ClearBanForm();
                        await RefreshBanData();
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
                    TrangThai = 1 // Mặc định là trống (bàn mới tạo thì cứ để trống nhưu vậy, ai dặt thì đặt)
                };

                dbContext.Bans.Add(newBan);
                await dbContext.SaveChangesAsync();

                MessageBox.Show("Thêm bàn mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // xóa dữ liệu trên ô txtbox và tải lại dữ liệu 
                ClearBanForm();
                await RefreshBanData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearBanForm()
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
            // Xóa hết thông tin trên form bàn (bỏ chọn)
            ClearBanForm();

            // Thông báo cho người dùng (tùy chọn)
            // MessageBox.Show("Đã bỏ chọn bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Tab Thanh Vien Event Handlers

        private async void btnThemTV_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(txtTenThanhVien.Text) || string.IsNullOrEmpty(txtSDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ họ tên và số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                await RefreshThanhVienData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm thành viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSuaTV_Click(object sender, EventArgs e)
        {
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
                    await RefreshThanhVienData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thành viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoaTV_Click(object sender, EventArgs e)
        {
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
                        await RefreshThanhVienData();
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

        private async Task RefreshThanhVienData()
        {
            // Refresh data based on current search text
            string currentSearch = txtTimKiem.Text.Trim();
            await PerformSearch(currentSearch);
        }

        #endregion

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Giải phóng resources khi form đóng
            searchTimer?.Stop();
            searchTimer?.Dispose();

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
                var selectedRow = dataGridView1.SelectedRows[0];
                LoadThanhVienToForm(selectedRow);

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
                            await RefreshThanhVienData();
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
            LoadDefaultContent();
        }

        #region Menu Navigation

        private void SetupMenuNavigation()
        {
            // Thêm event handlers cho các menu items
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
            ShowSanPhamForm();
        }

        private void ThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowThongKeForm();
        }

        private void QuảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowQuanLyTaiKhoanForm();
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
    }
}
