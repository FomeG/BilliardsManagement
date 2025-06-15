using Microsoft.EntityFrameworkCore;
using Models.HandleData;
using Models.Models;
using System.Data;

namespace WinFormsApp1
{
    public partial class frmSanPham : Form
    {
        private DaDBContext dbContext;
        private bool isEditing = false;

        public frmSanPham(DaDBContext context)
        {
            InitializeComponent();
            dbContext = context;
            SetupForm();
            _ = LoadSanPhamData();
        }

        private void SetupForm()
        {
            this.Text = "Quản lý Sản phẩm - Đồ ăn / Thức uống";

            // Setup DataGridView
            SetupDataGridView();

            // Setup initial button states
            SetButtonStates(false);

            // Clear form initially
            ClearForm();
        }

        #region UI Setup

        private void SetupDataGridView()
        {
            if (dgvSanPham.Columns.Count == 0)
            {
                dgvSanPham.Columns.Clear();
                dgvSanPham.AutoGenerateColumns = false;

                dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ID",
                    HeaderText = "ID",
                    DataPropertyName = "ID",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    ReadOnly = true
                });

                dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TenSanPham",
                    HeaderText = "Tên sản phẩm",
                    DataPropertyName = "TenSanPham",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 30
                });

                dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "LoaiSanPham",
                    HeaderText = "Loại sản phẩm",
                    DataPropertyName = "LoaiSanPham",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 20
                });

                dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "DonGia",
                    HeaderText = "Đơn giá",
                    DataPropertyName = "DonGia",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 15,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
                });

                dgvSanPham.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "ConHang",
                    HeaderText = "Còn hàng",
                    DataPropertyName = "ConHang",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                });

                dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvSanPham.MultiSelect = false;
                dgvSanPham.AllowUserToAddRows = false;
                dgvSanPham.AllowUserToDeleteRows = false;
                dgvSanPham.ReadOnly = true;

                dgvSanPham.SelectionChanged += DgvSanPham_SelectionChanged;
            }
        }

        private void SetButtonStates(bool editing)
        {
            isEditing = editing;
            btnThem.Enabled = !editing;
            btnSua.Enabled = !editing && dgvSanPham.SelectedRows.Count > 0;
            btnXoa.Enabled = !editing && dgvSanPham.SelectedRows.Count > 0;
            btnLuu.Enabled = editing;
            btnHuy.Enabled = editing;

            // Enable/disable input controls
            txtTenSanPham.Enabled = editing;
            txtLoaiSanPham.Enabled = editing;
            txtDonGia.Enabled = editing;
            chkConHang.Enabled = editing;
            btnChonHinh.Enabled = editing;
        }

        #endregion

        #region Data Operations

        private async Task LoadSanPhamData()
        {
            try
            {
                var sanPhams = await dbContext.SanPhams.OrderBy(s => s.ID).ToListAsync();
                dgvSanPham.DataSource = sanPhams;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu sản phẩm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvSanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count > 0 && !isEditing)
            {
                LoadSanPhamToForm(dgvSanPham.SelectedRows[0]);
                SetButtonStates(false);
            }
        }

        private void LoadSanPhamToForm(DataGridViewRow row)
        {
            try
            {
                if (row.DataBoundItem is SanPham sanPham)
                {
                    txtID.Text = sanPham.ID.ToString();
                    txtTenSanPham.Text = sanPham.TenSanPham;
                    txtLoaiSanPham.Text = sanPham.LoaiSanPham;
                    txtDonGia.Text = sanPham.DonGia.ToString();
                    chkConHang.Checked = sanPham.ConHang;

                    // Dispose image cũ trước khi gán image mới
                    if (picHinhAnh.Image != null)
                    {
                        picHinhAnh.Image.Dispose();
                        picHinhAnh.Image = null;
                    }

                    // Load hình ảnh nếu có
                    if (sanPham.HinhAnh != null && sanPham.HinhAnh.Length > 0)
                    {
                        using (var ms = new MemoryStream(sanPham.HinhAnh))
                        {
                            // Tạo copy của image để tránh lỗi khi stream bị dispose
                            var originalImage = Image.FromStream(ms);
                            picHinhAnh.Image = new Bitmap(originalImage);
                            originalImage.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thông tin sản phẩm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtTenSanPham.Clear();
            txtLoaiSanPham.Clear();
            txtDonGia.Clear();
            chkConHang.Checked = true;

            // Dispose image cũ trước khi clear
            if (picHinhAnh.Image != null)
            {
                picHinhAnh.Image.Dispose();
                picHinhAnh.Image = null;
            }

            dgvSanPham.ClearSelection();
        }

        #endregion

        #region Validation

        private bool ValidateInput()
        {
            // Validate Tên sản phẩm (required, max 100 chars)
            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return false;
            }

            if (txtTenSanPham.Text.Trim().Length > 100)
            {
                MessageBox.Show("Tên sản phẩm không được vượt quá 100 ký tự!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return false;
            }

            // Validate Loại sản phẩm (required, max 50 chars)
            if (string.IsNullOrWhiteSpace(txtLoaiSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập loại sản phẩm!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoaiSanPham.Focus();
                return false;
            }

            if (txtLoaiSanPham.Text.Trim().Length > 50)
            {
                MessageBox.Show("Loại sản phẩm không được vượt quá 50 ký tự!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoaiSanPham.Focus();
                return false;
            }

            // Validate Đơn giá (required, decimal, >= 0)
            if (string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn giá!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là số và không được âm!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Event Handlers

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetButtonStates(true);
            txtTenSanPham.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetButtonStates(true);
            txtTenSanPham.Focus();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                byte[]? hinhAnhData = null;
                if (picHinhAnh.Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        // Tạo copy của image để tránh lỗi GDI khi save
                        using (var imageCopy = new Bitmap(picHinhAnh.Image))
                        {
                            imageCopy.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            hinhAnhData = ms.ToArray();
                        }
                    }
                }

                if (string.IsNullOrEmpty(txtID.Text)) // Thêm mới
                {
                    var newSanPham = new SanPham
                    {
                        TenSanPham = txtTenSanPham.Text.Trim(),
                        LoaiSanPham = txtLoaiSanPham.Text.Trim(),
                        DonGia = decimal.Parse(txtDonGia.Text),
                        ConHang = chkConHang.Checked,
                        HinhAnh = hinhAnhData ?? new byte[0] // Default empty array if no image
                    };

                    dbContext.SanPhams.Add(newSanPham);
                    await dbContext.SaveChangesAsync();

                    MessageBox.Show("Thêm sản phẩm thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Cập nhật
                {
                    int id = int.Parse(txtID.Text);
                    var sanPham = await dbContext.SanPhams.FindAsync(id);

                    if (sanPham != null)
                    {
                        sanPham.TenSanPham = txtTenSanPham.Text.Trim();
                        sanPham.LoaiSanPham = txtLoaiSanPham.Text.Trim();
                        sanPham.DonGia = decimal.Parse(txtDonGia.Text);
                        sanPham.ConHang = chkConHang.Checked;

                        if (hinhAnhData != null)
                        {
                            sanPham.HinhAnh = hinhAnhData;
                        }

                        await dbContext.SaveChangesAsync();

                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                _ = LoadSanPhamData();
                SetButtonStates(false);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu sản phẩm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtID.Text);
                    var sanPham = await dbContext.SanPhams.FindAsync(id);

                    if (sanPham != null)
                    {
                        dbContext.SanPhams.Remove(sanPham);
                        await dbContext.SaveChangesAsync();

                        MessageBox.Show("Xóa sản phẩm thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _ = LoadSanPhamData();
                        ClearForm();
                        SetButtonStates(false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa sản phẩm: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetButtonStates(false);
            if (dgvSanPham.SelectedRows.Count > 0)
            {
                LoadSanPhamToForm(dgvSanPham.SelectedRows[0]);
            }
            else
            {
                ClearForm();
            }
        }

        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Chọn hình ảnh sản phẩm";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Dispose image cũ trước khi gán image mới
                        if (picHinhAnh.Image != null)
                        {
                            picHinhAnh.Image.Dispose();
                            picHinhAnh.Image = null;
                        }

                        // Load image từ file và tạo copy để tránh file lock
                        using (var originalImage = Image.FromFile(openFileDialog.FileName))
                        {
                            picHinhAnh.Image = new Bitmap(originalImage);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi tải hình ảnh: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion
    }
}
