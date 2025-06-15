namespace WinFormsApp1
{
    partial class frmSanPham
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
            if (disposing)
            {
                // Dispose image trong PictureBox trước khi dispose components
                if (picHinhAnh?.Image != null)
                {
                    picHinhAnh.Image.Dispose();
                    picHinhAnh.Image = null;
                }

                if (components != null)
                {
                    components.Dispose();
                }
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
            dgvSanPham = new DataGridView();
            groupBox1 = new GroupBox();
            btnChonHinh = new Button();
            picHinhAnh = new PictureBox();
            chkConHang = new CheckBox();
            txtDonGia = new TextBox();
            txtLoaiSanPham = new TextBox();
            txtTenSanPham = new TextBox();
            txtID = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            btnHuy = new Button();
            btnLuu = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnThem = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            //
            // dgvSanPham
            //
            dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSanPham.Location = new Point(12, 12);
            dgvSanPham.Name = "dgvSanPham";
            dgvSanPham.Size = new Size(776, 250);
            dgvSanPham.TabIndex = 0;
            //
            // groupBox1
            //
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnChonHinh);
            groupBox1.Controls.Add(picHinhAnh);
            groupBox1.Controls.Add(chkConHang);
            groupBox1.Controls.Add(txtDonGia);
            groupBox1.Controls.Add(txtLoaiSanPham);
            groupBox1.Controls.Add(txtTenSanPham);
            groupBox1.Controls.Add(txtID);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 280);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(580, 200);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin sản phẩm";
            //
            // btnChonHinh
            //
            btnChonHinh.Location = new Point(450, 160);
            btnChonHinh.Name = "btnChonHinh";
            btnChonHinh.Size = new Size(100, 30);
            btnChonHinh.TabIndex = 11;
            btnChonHinh.Text = "Chọn hình";
            btnChonHinh.UseVisualStyleBackColor = true;
            btnChonHinh.Click += btnChonHinh_Click;
            //
            // picHinhAnh
            //
            picHinhAnh.BorderStyle = BorderStyle.FixedSingle;
            picHinhAnh.Location = new Point(450, 30);
            picHinhAnh.Name = "picHinhAnh";
            picHinhAnh.Size = new Size(120, 120);
            picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picHinhAnh.TabIndex = 10;
            picHinhAnh.TabStop = false;
            //
            // chkConHang
            //
            chkConHang.AutoSize = true;
            chkConHang.Location = new Point(120, 160);
            chkConHang.Name = "chkConHang";
            chkConHang.Size = new Size(80, 19);
            chkConHang.TabIndex = 9;
            chkConHang.Text = "Còn hàng";
            chkConHang.UseVisualStyleBackColor = true;
            //
            // txtDonGia
            //
            txtDonGia.Location = new Point(120, 120);
            txtDonGia.Name = "txtDonGia";
            txtDonGia.Size = new Size(200, 23);
            txtDonGia.TabIndex = 8;
            //
            // txtLoaiSanPham
            //
            txtLoaiSanPham.Location = new Point(120, 90);
            txtLoaiSanPham.Name = "txtLoaiSanPham";
            txtLoaiSanPham.Size = new Size(200, 23);
            txtLoaiSanPham.TabIndex = 7;
            //
            // txtTenSanPham
            //
            txtTenSanPham.Location = new Point(120, 60);
            txtTenSanPham.Name = "txtTenSanPham";
            txtTenSanPham.Size = new Size(300, 23);
            txtTenSanPham.TabIndex = 6;
            //
            // txtID
            //
            txtID.Location = new Point(120, 30);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(100, 23);
            txtID.TabIndex = 5;
            //
            // label5
            //
            label5.AutoSize = true;
            label5.Location = new Point(20, 123);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 4;
            label5.Text = "Đơn giá:";
            //
            // label4
            //
            label4.AutoSize = true;
            label4.Location = new Point(20, 93);
            label4.Name = "label4";
            label4.Size = new Size(88, 15);
            label4.TabIndex = 3;
            label4.Text = "Loại sản phẩm:";
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Location = new Point(20, 63);
            label3.Name = "label3";
            label3.Size = new Size(84, 15);
            label3.TabIndex = 2;
            label3.Text = "Tên sản phẩm:";
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new Point(450, 12);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 1;
            label2.Text = "Hình ảnh:";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new Point(20, 33);
            label1.Name = "label1";
            label1.Size = new Size(21, 15);
            label1.TabIndex = 0;
            label1.Text = "ID:";
            //
            // groupBox2
            //
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox2.Controls.Add(btnHuy);
            groupBox2.Controls.Add(btnLuu);
            groupBox2.Controls.Add(btnXoa);
            groupBox2.Controls.Add(btnSua);
            groupBox2.Controls.Add(btnThem);
            groupBox2.Location = new Point(610, 280);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(178, 200);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Chức năng";
            //
            // btnHuy
            //
            btnHuy.Location = new Point(20, 160);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(140, 30);
            btnHuy.TabIndex = 4;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            //
            // btnLuu
            //
            btnLuu.Location = new Point(20, 125);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(140, 30);
            btnLuu.TabIndex = 3;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            //
            // btnXoa
            //
            btnXoa.Location = new Point(20, 90);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(140, 30);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            //
            // btnSua
            //
            btnSua.Location = new Point(20, 55);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(140, 30);
            btnSua.TabIndex = 1;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            //
            // btnThem
            //
            btnThem.Location = new Point(20, 20);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(140, 30);
            btnThem.TabIndex = 0;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            //
            // frmSanPham
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 500);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(dgvSanPham);
            MinimumSize = new Size(800, 500);
            Name = "frmSanPham";
            Text = "Quản lý Sản phẩm";
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvSanPham;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox txtID;
        private TextBox txtTenSanPham;
        private TextBox txtLoaiSanPham;
        private TextBox txtDonGia;
        private CheckBox chkConHang;
        private PictureBox picHinhAnh;
        private Button btnChonHinh;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLuu;
        private Button btnHuy;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}