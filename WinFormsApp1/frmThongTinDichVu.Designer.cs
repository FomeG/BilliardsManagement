namespace WinFormsApp1
{
    partial class frmThongTinDichVu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTenBan = new Label();
            lblTenKhachHang = new Label();
            lblThoiGianBatDau = new Label();
            lblThoiGianChoi = new Label();
            lblTongTien = new Label();
            dataGridView1 = new DataGridView();
            btnThemDichVu = new Button();
            btnThanhToan = new Button();
            btnHuyBan = new Button();
            lblThoiGianKetThuc = new Label();
            btnXoaDichVu = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblTenBan
            // 
            lblTenBan.AutoSize = true;
            lblTenBan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTenBan.Location = new Point(12, 9);
            lblTenBan.Name = "lblTenBan";
            lblTenBan.Size = new Size(74, 21);
            lblTenBan.TabIndex = 0;
            lblTenBan.Text = "Tên bàn:";
            // 
            // lblTenKhachHang
            // 
            lblTenKhachHang.AutoSize = true;
            lblTenKhachHang.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTenKhachHang.Location = new Point(12, 35);
            lblTenKhachHang.Name = "lblTenKhachHang";
            lblTenKhachHang.Size = new Size(90, 19);
            lblTenKhachHang.TabIndex = 1;
            lblTenKhachHang.Text = "Khách hàng:";
            // 
            // lblThoiGianBatDau
            // 
            lblThoiGianBatDau.AutoSize = true;
            lblThoiGianBatDau.Location = new Point(12, 63);
            lblThoiGianBatDau.Name = "lblThoiGianBatDau";
            lblThoiGianBatDau.Size = new Size(102, 15);
            lblThoiGianBatDau.TabIndex = 2;
            lblThoiGianBatDau.Text = "Thời gian bắt đầu:";
            // 
            // lblThoiGianChoi
            // 
            lblThoiGianChoi.AutoSize = true;
            lblThoiGianChoi.Location = new Point(12, 114);
            lblThoiGianChoi.Name = "lblThoiGianChoi";
            lblThoiGianChoi.Size = new Size(85, 15);
            lblThoiGianChoi.TabIndex = 3;
            lblThoiGianChoi.Text = "Thời gian chơi:";
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Red;
            lblTongTien.Location = new Point(12, 138);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(76, 19);
            lblTongTien.TabIndex = 4;
            lblTongTien.Text = "Tổng tiền:";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 196);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Size = new Size(252, 286);
            dataGridView1.TabIndex = 5;
            // 
            // btnThemDichVu
            // 
            btnThemDichVu.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnThemDichVu.BackColor = Color.FromArgb(40, 167, 69);
            btnThemDichVu.FlatAppearance.BorderColor = Color.Black;
            btnThemDichVu.FlatStyle = FlatStyle.Flat;
            btnThemDichVu.ForeColor = Color.White;
            btnThemDichVu.Location = new Point(12, 492);
            btnThemDichVu.Name = "btnThemDichVu";
            btnThemDichVu.Size = new Size(80, 30);
            btnThemDichVu.TabIndex = 6;
            btnThemDichVu.Text = "Thêm DV";
            btnThemDichVu.UseVisualStyleBackColor = false;
            btnThemDichVu.Click += BtnThemDichVu_Click;
            // 
            // btnThanhToan
            // 
            btnThanhToan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnThanhToan.BackColor = Color.FromArgb(0, 123, 255);
            btnThanhToan.FlatAppearance.BorderColor = Color.Black;
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(98, 492);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(80, 30);
            btnThanhToan.TabIndex = 7;
            btnThanhToan.Text = "Thanh toán";
            btnThanhToan.UseVisualStyleBackColor = false;
            btnThanhToan.Click += BtnThanhToan_Click;
            // 
            // btnHuyBan
            // 
            btnHuyBan.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnHuyBan.BackColor = Color.FromArgb(220, 53, 69);
            btnHuyBan.FlatAppearance.BorderColor = Color.Black;
            btnHuyBan.FlatStyle = FlatStyle.Flat;
            btnHuyBan.ForeColor = Color.White;
            btnHuyBan.Location = new Point(184, 492);
            btnHuyBan.Name = "btnHuyBan";
            btnHuyBan.Size = new Size(80, 30);
            btnHuyBan.TabIndex = 8;
            btnHuyBan.Text = "Hủy bàn";
            btnHuyBan.UseVisualStyleBackColor = false;
            btnHuyBan.Click += BtnHuyBan_Click;
            // 
            // lblThoiGianKetThuc
            // 
            lblThoiGianKetThuc.AutoSize = true;
            lblThoiGianKetThuc.Location = new Point(12, 89);
            lblThoiGianKetThuc.Name = "lblThoiGianKetThuc";
            lblThoiGianKetThuc.Size = new Size(105, 15);
            lblThoiGianKetThuc.TabIndex = 2;
            lblThoiGianKetThuc.Text = "Thời gian kết thúc:";
            // 
            // btnXoaDichVu
            // 
            btnXoaDichVu.BackColor = Color.FromArgb(220, 53, 69);
            btnXoaDichVu.FlatAppearance.BorderColor = Color.Black;
            btnXoaDichVu.FlatStyle = FlatStyle.Flat;
            btnXoaDichVu.ForeColor = Color.White;
            btnXoaDichVu.Location = new Point(12, 160);
            btnXoaDichVu.Name = "btnXoaDichVu";
            btnXoaDichVu.Size = new Size(74, 30);
            btnXoaDichVu.TabIndex = 9;
            btnXoaDichVu.Text = "Xóa DV";
            btnXoaDichVu.UseVisualStyleBackColor = false;
            btnXoaDichVu.Click += BtnXoaDichVu_Click;
            // 
            // frmThongTinDichVu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(276, 537);
            Controls.Add(btnXoaDichVu);
            Controls.Add(btnHuyBan);
            Controls.Add(btnThanhToan);
            Controls.Add(btnThemDichVu);
            Controls.Add(dataGridView1);
            Controls.Add(lblTongTien);
            Controls.Add(lblThoiGianChoi);
            Controls.Add(lblThoiGianKetThuc);
            Controls.Add(lblThoiGianBatDau);
            Controls.Add(lblTenKhachHang);
            Controls.Add(lblTenBan);
            Name = "frmThongTinDichVu";
            Text = "Thông tin dịch vụ";
            Load += frmThongTinDichVu_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTenBan;
        private Label lblTenKhachHang;
        private Label lblThoiGianBatDau;
        private Label lblThoiGianChoi;
        private Label lblTongTien;
        private DataGridView dataGridView1;
        private Button btnThemDichVu;
        private Button btnThanhToan;
        private Button btnHuyBan;
        private Label lblThoiGianKetThuc;
        private Button btnXoaDichVu;
    }
}