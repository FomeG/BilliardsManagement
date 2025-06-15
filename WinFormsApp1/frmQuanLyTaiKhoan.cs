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
    public partial class frmQuanLyTaiKhoan : Form
    {
        private DaDBContext dbContext;
        private bool isEditing = false;

        public frmQuanLyTaiKhoan(DaDBContext context)
        {
            InitializeComponent();
            dbContext = context;
            SetupForm();
            _ = LoadTaiKhoanData();
        }

        private void SetupForm()
        {
            this.Text = "Quản lý Tài khoản";
            
            // Setup DataGridView
            SetupDataGridView();
            
            // Setup ComboBox
            SetupComboBox();
            
            // Setup initial button states
            SetButtonStates(false);
            
            // Clear form initially
            ClearForm();
        }

        #region UI Setup

        private void SetupDataGridView()
        {
            if (dgvTaiKhoan.Columns.Count == 0)
            {
                dgvTaiKhoan.Columns.Clear();
                dgvTaiKhoan.AutoGenerateColumns = false;

                dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ID",
                    HeaderText = "ID",
                    DataPropertyName = "ID",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    ReadOnly = true
                });

                dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TenDangNhap",
                    HeaderText = "Tên đăng nhập",
                    DataPropertyName = "TenDangNhap",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 25
                });

                dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "HoTen",
                    HeaderText = "Họ tên",
                    DataPropertyName = "HoTen",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 30
                });

                dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "VaiTro",
                    HeaderText = "Vai trò",
                    DataPropertyName = "VaiTro",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 20
                });

                dgvTaiKhoan.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "TrangThai",
                    HeaderText = "Trạng thái",
                    DataPropertyName = "TrangThai",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                });

                dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTaiKhoan.MultiSelect = false;
                dgvTaiKhoan.AllowUserToAddRows = false;
                dgvTaiKhoan.AllowUserToDeleteRows = false;
                dgvTaiKhoan.ReadOnly = true;

                dgvTaiKhoan.SelectionChanged += DgvTaiKhoan_SelectionChanged;
            }
        }

        private void SetupComboBox()
        {
            cmbVaiTro.Items.Clear();
            cmbVaiTro.Items.AddRange(new string[] { "Admin", "NhanVien" });
            cmbVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SetButtonStates(bool editing)
        {
            isEditing = editing;
            btnThem.Enabled = !editing;
            btnSua.Enabled = !editing && dgvTaiKhoan.SelectedRows.Count > 0;
            btnXoa.Enabled = !editing && dgvTaiKhoan.SelectedRows.Count > 0;
            btnLuu.Enabled = editing;
            btnHuy.Enabled = editing;
            
            // Enable/disable input controls
            txtTenDangNhap.Enabled = editing;
            txtMatKhau.Enabled = editing;
            txtHoTen.Enabled = editing;
            cmbVaiTro.Enabled = editing;
            chkTrangThai.Enabled = editing;
        }

        #endregion

        #region Data Operations

        private async Task LoadTaiKhoanData()
        {
            try
            {
                var taiKhoans = await dbContext.TaiKhoans.OrderBy(t => t.ID).ToListAsync();
                dgvTaiKhoan.DataSource = taiKhoans;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu tài khoản: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTaiKhoan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0 && !isEditing)
            {
                LoadTaiKhoanToForm(dgvTaiKhoan.SelectedRows[0]);
                SetButtonStates(false);
            }
        }

        private void LoadTaiKhoanToForm(DataGridViewRow row)
        {
            try
            {
                if (row.DataBoundItem is TaiKhoan taiKhoan)
                {
                    txtID.Text = taiKhoan.ID.ToString();
                    txtTenDangNhap.Text = taiKhoan.TenDangNhap;
                    txtMatKhau.Text = taiKhoan.MatKhau;
                    txtHoTen.Text = taiKhoan.HoTen;
                    cmbVaiTro.Text = taiKhoan.VaiTro;
                    chkTrangThai.Checked = taiKhoan.TrangThai;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thông tin tài khoản: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();
            cmbVaiTro.SelectedIndex = -1;
            chkTrangThai.Checked = true;
            dgvTaiKhoan.ClearSelection();
        }

        #endregion

        #region Validation

        private bool ValidateInput()
        {
            // Validate Tên đăng nhập (required, max 50 chars, unique)
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (txtTenDangNhap.Text.Trim().Length > 50)
            {
                MessageBox.Show("Tên đăng nhập không được vượt quá 50 ký tự!", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            // Validate Mật khẩu (required, max 50 chars)
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            if (txtMatKhau.Text.Trim().Length > 50)
            {
                MessageBox.Show("Mật khẩu không được vượt quá 50 ký tự!", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            // Validate Họ tên (required, max 100 chars)
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (txtHoTen.Text.Trim().Length > 100)
            {
                MessageBox.Show("Họ tên không được vượt quá 100 ký tự!", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            // Validate Vai trò (required, max 20 chars)
            if (cmbVaiTro.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vai trò!", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbVaiTro.Focus();
                return false;
            }

            return true;
        }

        private async Task<bool> CheckTenDangNhapExists(string tenDangNhap, int? excludeId = null)
        {
            try
            {
                var query = dbContext.TaiKhoans.Where(t => t.TenDangNhap == tenDangNhap);
                if (excludeId.HasValue)
                {
                    query = query.Where(t => t.ID != excludeId.Value);
                }
                return await query.AnyAsync();
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Event Handlers

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetButtonStates(true);
            txtTenDangNhap.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetButtonStates(true);
            txtTenDangNhap.Focus();
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (string.IsNullOrEmpty(txtID.Text)) // Thêm mới
                {
                    // Check if username already exists
                    bool exists = await CheckTenDangNhapExists(txtTenDangNhap.Text.Trim());
                    if (exists)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại!", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenDangNhap.Focus();
                        return;
                    }

                    var newTaiKhoan = new TaiKhoan
                    {
                        TenDangNhap = txtTenDangNhap.Text.Trim(),
                        MatKhau = txtMatKhau.Text.Trim(),
                        HoTen = txtHoTen.Text.Trim(),
                        VaiTro = cmbVaiTro.Text,
                        TrangThai = chkTrangThai.Checked
                    };

                    dbContext.TaiKhoans.Add(newTaiKhoan);
                    await dbContext.SaveChangesAsync();

                    MessageBox.Show("Thêm tài khoản thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Cập nhật
                {
                    int id = int.Parse(txtID.Text);

                    // Check if username already exists (exclude current record)
                    bool exists = await CheckTenDangNhapExists(txtTenDangNhap.Text.Trim(), id);
                    if (exists)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại!", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenDangNhap.Focus();
                        return;
                    }

                    var taiKhoan = await dbContext.TaiKhoans.FindAsync(id);

                    if (taiKhoan != null)
                    {
                        taiKhoan.TenDangNhap = txtTenDangNhap.Text.Trim();
                        taiKhoan.MatKhau = txtMatKhau.Text.Trim();
                        taiKhoan.HoTen = txtHoTen.Text.Trim();
                        taiKhoan.VaiTro = cmbVaiTro.Text;
                        taiKhoan.TrangThai = chkTrangThai.Checked;

                        await dbContext.SaveChangesAsync();

                        MessageBox.Show("Cập nhật tài khoản thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                _ = LoadTaiKhoanData();
                SetButtonStates(false);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu tài khoản: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtID.Text);
                    var taiKhoan = await dbContext.TaiKhoans.FindAsync(id);

                    if (taiKhoan != null)
                    {
                        dbContext.TaiKhoans.Remove(taiKhoan);
                        await dbContext.SaveChangesAsync();

                        MessageBox.Show("Xóa tài khoản thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _ = LoadTaiKhoanData();
                        ClearForm();
                        SetButtonStates(false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa tài khoản: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetButtonStates(false);
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                LoadTaiKhoanToForm(dgvTaiKhoan.SelectedRows[0]);
            }
            else
            {
                ClearForm();
            }
        }

        #endregion
    }
}
