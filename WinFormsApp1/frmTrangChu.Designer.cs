namespace WinFormsApp1
{
    partial class frmTrangChu
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            pnChiTiet = new Panel();
            btnBaoTri = new Button();
            btnDatBan = new Button();
            panel1 = new Panel();
            tabControl1 = new TabControl();
            tabPageBan = new TabPage();
            button1 = new Button();
            btnBoChon = new Button();
            btnNewBan = new Button();
            btnDeleteBan = new Button();
            btnSaveBan = new Button();
            cmbTrangThaiBan = new ComboBox();
            lblTrangThaiBan = new Label();
            txtGiaTheoGio = new TextBox();
            lblGiaTheoGio = new Label();
            txtLoaiBan = new TextBox();
            lblLoaiBan = new Label();
            txtTenBan = new TextBox();
            lblTenBan = new Label();
            txtBanID = new TextBox();
            lblBanID = new Label();
            tabPageThanhVien = new TabPage();
            btnNap = new Button();
            txtTimKiem = new TextBox();
            lblNgayDangKy = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            dateTimePicker1 = new DateTimePicker();
            rdNu = new RadioButton();
            rdNam = new RadioButton();
            label5 = new Label();
            textBox1 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtGioConLai = new TextBox();
            txtDiaChi = new TextBox();
            txtSDT = new TextBox();
            txtTenThanhVien = new TextBox();
            btnBoChontv = new Button();
            btnThemTV = new Button();
            btnXoaTV = new Button();
            btnSuaTV = new Button();
            dataGridView1 = new DataGridView();
            statusStrip = new StatusStrip();
            lblUserName = new ToolStripStatusLabel();
            lblUserRole = new ToolStripStatusLabel();
            lblDateTime = new ToolStripStatusLabel();
            contextMenuStrip1 = new ContextMenuStrip(components);
            testMenuStrip1ToolStripMenuItem = new ToolStripMenuItem();
            timer = new System.Windows.Forms.Timer(components);
            panel3 = new Panel();
            menuStrip1 = new MenuStrip();
            trangChủToolStripMenuItem = new ToolStripMenuItem();
            đồĂnThứcUốngToolStripMenuItem = new ToolStripMenuItem();
            thốngKêToolStripMenuItem = new ToolStripMenuItem();
            quảnLýTàiKhoảnToolStripMenuItem = new ToolStripMenuItem();
            càiĐặtToolStripMenuItem = new ToolStripMenuItem();
            đăngXuấtToolStripMenuItem = new ToolStripMenuItem();
            pnHienThi = new Panel();
            groupBox1.SuspendLayout();
            pnChiTiet.SuspendLayout();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageBan.SuspendLayout();
            tabPageThanhVien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            statusStrip.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            panel3.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 8;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.Location = new Point(12, 340);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(626, 321);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(pnChiTiet);
            groupBox1.Location = new Point(644, 340);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(351, 321);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chi tiết";
            // 
            // pnChiTiet
            // 
            pnChiTiet.BackColor = SystemColors.ButtonHighlight;
            pnChiTiet.Controls.Add(btnBaoTri);
            pnChiTiet.Controls.Add(btnDatBan);
            pnChiTiet.Dock = DockStyle.Fill;
            pnChiTiet.Location = new Point(3, 19);
            pnChiTiet.Name = "pnChiTiet";
            pnChiTiet.Size = new Size(345, 299);
            pnChiTiet.TabIndex = 0;
            // 
            // btnBaoTri
            // 
            btnBaoTri.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnBaoTri.Enabled = false;
            btnBaoTri.Location = new Point(3, 89);
            btnBaoTri.Name = "btnBaoTri";
            btnBaoTri.Size = new Size(339, 80);
            btnBaoTri.TabIndex = 1;
            btnBaoTri.Text = "Bảo trì";
            btnBaoTri.UseVisualStyleBackColor = true;
            btnBaoTri.Visible = false;
            btnBaoTri.Click += btnBaoTri_Click_1;
            // 
            // btnDatBan
            // 
            btnDatBan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnDatBan.Enabled = false;
            btnDatBan.Location = new Point(3, 3);
            btnDatBan.Name = "btnDatBan";
            btnDatBan.Size = new Size(339, 80);
            btnDatBan.TabIndex = 0;
            btnDatBan.Text = "Đặt bàn mới";
            btnDatBan.UseVisualStyleBackColor = true;
            btnDatBan.Visible = false;
            btnDatBan.Click += btnDatBan_Click_1;
            // 
            // panel1
            // 
            panel1.Controls.Add(tabControl1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(1007, 300);
            panel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageBan);
            tabControl1.Controls.Add(tabPageThanhVien);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 9F);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1007, 300);
            tabControl1.TabIndex = 0;
            // 
            // tabPageBan
            // 
            tabPageBan.BackColor = Color.PeachPuff;
            tabPageBan.Controls.Add(button1);
            tabPageBan.Controls.Add(btnBoChon);
            tabPageBan.Controls.Add(btnNewBan);
            tabPageBan.Controls.Add(btnDeleteBan);
            tabPageBan.Controls.Add(btnSaveBan);
            tabPageBan.Controls.Add(cmbTrangThaiBan);
            tabPageBan.Controls.Add(lblTrangThaiBan);
            tabPageBan.Controls.Add(txtGiaTheoGio);
            tabPageBan.Controls.Add(lblGiaTheoGio);
            tabPageBan.Controls.Add(txtLoaiBan);
            tabPageBan.Controls.Add(lblLoaiBan);
            tabPageBan.Controls.Add(txtTenBan);
            tabPageBan.Controls.Add(lblTenBan);
            tabPageBan.Controls.Add(txtBanID);
            tabPageBan.Controls.Add(lblBanID);
            tabPageBan.Location = new Point(4, 24);
            tabPageBan.Name = "tabPageBan";
            tabPageBan.Padding = new Padding(3);
            tabPageBan.Size = new Size(999, 272);
            tabPageBan.TabIndex = 0;
            tabPageBan.Text = "Thông tin bàn";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.BackColor = Color.Silver;
            button1.FlatAppearance.BorderColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.Black;
            button1.Location = new Point(913, 241);
            button1.Name = "button1";
            button1.Size = new Size(80, 25);
            button1.TabIndex = 13;
            button1.Text = "Tải lại";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnBoChon
            // 
            btnBoChon.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBoChon.BackColor = Color.Silver;
            btnBoChon.FlatAppearance.BorderColor = Color.White;
            btnBoChon.FlatStyle = FlatStyle.Flat;
            btnBoChon.ForeColor = Color.Black;
            btnBoChon.Location = new Point(263, 241);
            btnBoChon.Name = "btnBoChon";
            btnBoChon.Size = new Size(80, 25);
            btnBoChon.TabIndex = 13;
            btnBoChon.Text = "Bỏ chọn";
            btnBoChon.UseVisualStyleBackColor = false;
            btnBoChon.Click += btnBoChon_Click;
            // 
            // btnNewBan
            // 
            btnNewBan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNewBan.BackColor = Color.FromArgb(40, 167, 69);
            btnNewBan.FlatStyle = FlatStyle.Flat;
            btnNewBan.ForeColor = Color.White;
            btnNewBan.Location = new Point(5, 241);
            btnNewBan.Name = "btnNewBan";
            btnNewBan.Size = new Size(80, 25);
            btnNewBan.TabIndex = 12;
            btnNewBan.Text = "Thêm mới";
            btnNewBan.UseVisualStyleBackColor = false;
            btnNewBan.Click += btnNewBan_Click;
            // 
            // btnDeleteBan
            // 
            btnDeleteBan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDeleteBan.BackColor = Color.FromArgb(220, 53, 69);
            btnDeleteBan.FlatStyle = FlatStyle.Flat;
            btnDeleteBan.ForeColor = Color.White;
            btnDeleteBan.Location = new Point(177, 241);
            btnDeleteBan.Name = "btnDeleteBan";
            btnDeleteBan.Size = new Size(80, 25);
            btnDeleteBan.TabIndex = 11;
            btnDeleteBan.Text = "Xóa";
            btnDeleteBan.UseVisualStyleBackColor = false;
            btnDeleteBan.Click += btnDeleteBan_Click;
            // 
            // btnSaveBan
            // 
            btnSaveBan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveBan.BackColor = Color.FromArgb(0, 122, 204);
            btnSaveBan.FlatStyle = FlatStyle.Flat;
            btnSaveBan.ForeColor = Color.White;
            btnSaveBan.Location = new Point(91, 241);
            btnSaveBan.Name = "btnSaveBan";
            btnSaveBan.Size = new Size(80, 25);
            btnSaveBan.TabIndex = 10;
            btnSaveBan.Text = "Sửa";
            btnSaveBan.UseVisualStyleBackColor = false;
            btnSaveBan.Click += btnSaveBan_Click;
            // 
            // cmbTrangThaiBan
            // 
            cmbTrangThaiBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTrangThaiBan.FormattingEnabled = true;
            cmbTrangThaiBan.Items.AddRange(new object[] { "0 - Đang có người", "1 - Trống", "2 - Bảo trì" });
            cmbTrangThaiBan.Location = new Point(765, 12);
            cmbTrangThaiBan.Name = "cmbTrangThaiBan";
            cmbTrangThaiBan.Size = new Size(150, 23);
            cmbTrangThaiBan.TabIndex = 9;
            // 
            // lblTrangThaiBan
            // 
            lblTrangThaiBan.AutoSize = true;
            lblTrangThaiBan.Location = new Point(690, 15);
            lblTrangThaiBan.Name = "lblTrangThaiBan";
            lblTrangThaiBan.Size = new Size(62, 15);
            lblTrangThaiBan.TabIndex = 8;
            lblTrangThaiBan.Text = "Trạng thái:";
            // 
            // txtGiaTheoGio
            // 
            txtGiaTheoGio.Location = new Point(570, 12);
            txtGiaTheoGio.Name = "txtGiaTheoGio";
            txtGiaTheoGio.Size = new Size(100, 23);
            txtGiaTheoGio.TabIndex = 7;
            // 
            // lblGiaTheoGio
            // 
            lblGiaTheoGio.AutoSize = true;
            lblGiaTheoGio.Location = new Point(485, 15);
            lblGiaTheoGio.Name = "lblGiaTheoGio";
            lblGiaTheoGio.Size = new Size(74, 15);
            lblGiaTheoGio.TabIndex = 6;
            lblGiaTheoGio.Text = "Giá theo giờ:";
            // 
            // txtLoaiBan
            // 
            txtLoaiBan.Location = new Point(365, 12);
            txtLoaiBan.Name = "txtLoaiBan";
            txtLoaiBan.Size = new Size(100, 23);
            txtLoaiBan.TabIndex = 5;
            // 
            // lblLoaiBan
            // 
            lblLoaiBan.AutoSize = true;
            lblLoaiBan.Location = new Point(300, 15);
            lblLoaiBan.Name = "lblLoaiBan";
            lblLoaiBan.Size = new Size(55, 15);
            lblLoaiBan.TabIndex = 4;
            lblLoaiBan.Text = "Loại bàn:";
            // 
            // txtTenBan
            // 
            txtTenBan.Location = new Point(180, 12);
            txtTenBan.Name = "txtTenBan";
            txtTenBan.Size = new Size(100, 23);
            txtTenBan.TabIndex = 3;
            // 
            // lblTenBan
            // 
            lblTenBan.AutoSize = true;
            lblTenBan.Location = new Point(120, 15);
            lblTenBan.Name = "lblTenBan";
            lblTenBan.Size = new Size(51, 15);
            lblTenBan.TabIndex = 2;
            lblTenBan.Text = "Tên bàn:";
            // 
            // txtBanID
            // 
            txtBanID.Location = new Point(40, 12);
            txtBanID.Name = "txtBanID";
            txtBanID.ReadOnly = true;
            txtBanID.Size = new Size(60, 23);
            txtBanID.TabIndex = 1;
            // 
            // lblBanID
            // 
            lblBanID.AutoSize = true;
            lblBanID.Location = new Point(10, 15);
            lblBanID.Name = "lblBanID";
            lblBanID.Size = new Size(21, 15);
            lblBanID.TabIndex = 0;
            lblBanID.Text = "ID:";
            // 
            // tabPageThanhVien
            // 
            tabPageThanhVien.BackColor = Color.PeachPuff;
            tabPageThanhVien.Controls.Add(btnNap);
            tabPageThanhVien.Controls.Add(txtTimKiem);
            tabPageThanhVien.Controls.Add(lblNgayDangKy);
            tabPageThanhVien.Controls.Add(label8);
            tabPageThanhVien.Controls.Add(label7);
            tabPageThanhVien.Controls.Add(label6);
            tabPageThanhVien.Controls.Add(dateTimePicker1);
            tabPageThanhVien.Controls.Add(rdNu);
            tabPageThanhVien.Controls.Add(rdNam);
            tabPageThanhVien.Controls.Add(label5);
            tabPageThanhVien.Controls.Add(textBox1);
            tabPageThanhVien.Controls.Add(label4);
            tabPageThanhVien.Controls.Add(label3);
            tabPageThanhVien.Controls.Add(label2);
            tabPageThanhVien.Controls.Add(label1);
            tabPageThanhVien.Controls.Add(txtGioConLai);
            tabPageThanhVien.Controls.Add(txtDiaChi);
            tabPageThanhVien.Controls.Add(txtSDT);
            tabPageThanhVien.Controls.Add(txtTenThanhVien);
            tabPageThanhVien.Controls.Add(btnBoChontv);
            tabPageThanhVien.Controls.Add(btnThemTV);
            tabPageThanhVien.Controls.Add(btnXoaTV);
            tabPageThanhVien.Controls.Add(btnSuaTV);
            tabPageThanhVien.Controls.Add(dataGridView1);
            tabPageThanhVien.Location = new Point(4, 24);
            tabPageThanhVien.Name = "tabPageThanhVien";
            tabPageThanhVien.Padding = new Padding(3);
            tabPageThanhVien.Size = new Size(999, 272);
            tabPageThanhVien.TabIndex = 1;
            tabPageThanhVien.Text = "Thông tin thành viên";
            // 
            // btnNap
            // 
            btnNap.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNap.BackColor = Color.Yellow;
            btnNap.FlatStyle = FlatStyle.Flat;
            btnNap.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNap.ForeColor = Color.Black;
            btnNap.Location = new Point(290, 196);
            btnNap.Name = "btnNap";
            btnNap.Size = new Size(56, 23);
            btnNap.TabIndex = 36;
            btnNap.Text = "Nạp";
            btnNap.UseVisualStyleBackColor = false;
            btnNap.Click += btnNap_Click;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtTimKiem.Location = new Point(352, 24);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(636, 23);
            txtTimKiem.TabIndex = 35;
            // 
            // lblNgayDangKy
            // 
            lblNgayDangKy.AutoSize = true;
            lblNgayDangKy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNgayDangKy.Location = new Point(91, 223);
            lblNgayDangKy.Name = "lblNgayDangKy";
            lblNgayDangKy.Size = new Size(85, 15);
            lblNgayDangKy.TabIndex = 34;
            lblNgayDangKy.Text = "Ngày đăng ký:";
            lblNgayDangKy.Click += label8_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(8, 223);
            label8.Name = "label8";
            label8.Size = new Size(83, 15);
            label8.TabIndex = 34;
            label8.Text = "Ngày đăng ký:";
            label8.Click += label8_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(128, 50);
            label7.Name = "label7";
            label7.Size = new Size(63, 15);
            label7.TabIndex = 33;
            label7.Text = "Ngày sinh:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(8, 50);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 32;
            label6.Text = "Giới tính:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(128, 68);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(218, 23);
            dateTimePicker1.TabIndex = 31;
            // 
            // rdNu
            // 
            rdNu.AutoSize = true;
            rdNu.Location = new Point(65, 68);
            rdNu.Name = "rdNu";
            rdNu.Size = new Size(41, 19);
            rdNu.TabIndex = 30;
            rdNu.TabStop = true;
            rdNu.Text = "Nữ";
            rdNu.UseVisualStyleBackColor = true;
            // 
            // rdNam
            // 
            rdNam.AutoSize = true;
            rdNam.Location = new Point(8, 68);
            rdNam.Name = "rdNam";
            rdNam.Size = new Size(51, 19);
            rdNam.TabIndex = 29;
            rdNam.TabStop = true;
            rdNam.Text = "Nam";
            rdNam.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(352, 6);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 28;
            label5.Text = "Tìm kiếm:";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Enabled = false;
            textBox1.Location = new Point(112, 68);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(17, 23);
            textBox1.TabIndex = 27;
            textBox1.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 178);
            label4.Name = "label4";
            label4.Size = new Size(84, 15);
            label4.TabIndex = 26;
            label4.Text = "Số tiền còn lại:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 134);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 25;
            label3.Text = "Địa chỉ:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 90);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 24;
            label2.Text = "Số điện thoại:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 6);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 23;
            label1.Text = "Tên thành viên:";
            // 
            // txtGioConLai
            // 
            txtGioConLai.Location = new Point(8, 196);
            txtGioConLai.Name = "txtGioConLai";
            txtGioConLai.Size = new Size(276, 23);
            txtGioConLai.TabIndex = 22;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(8, 152);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(338, 23);
            txtDiaChi.TabIndex = 21;
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(8, 108);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(338, 23);
            txtSDT.TabIndex = 20;
            // 
            // txtTenThanhVien
            // 
            txtTenThanhVien.Location = new Point(8, 24);
            txtTenThanhVien.Name = "txtTenThanhVien";
            txtTenThanhVien.Size = new Size(338, 23);
            txtTenThanhVien.TabIndex = 19;
            // 
            // btnBoChontv
            // 
            btnBoChontv.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBoChontv.BackColor = Color.Silver;
            btnBoChontv.FlatAppearance.BorderColor = Color.White;
            btnBoChontv.FlatStyle = FlatStyle.Flat;
            btnBoChontv.ForeColor = Color.Black;
            btnBoChontv.Location = new Point(266, 241);
            btnBoChontv.Name = "btnBoChontv";
            btnBoChontv.Size = new Size(80, 25);
            btnBoChontv.TabIndex = 17;
            btnBoChontv.Text = "Bỏ chọn";
            btnBoChontv.UseVisualStyleBackColor = false;
            btnBoChontv.Click += btnBoChontv_Click;
            // 
            // btnThemTV
            // 
            btnThemTV.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnThemTV.BackColor = Color.FromArgb(40, 167, 69);
            btnThemTV.FlatStyle = FlatStyle.Flat;
            btnThemTV.ForeColor = Color.White;
            btnThemTV.Location = new Point(8, 241);
            btnThemTV.Name = "btnThemTV";
            btnThemTV.Size = new Size(80, 25);
            btnThemTV.TabIndex = 16;
            btnThemTV.Text = "Thêm mới";
            btnThemTV.UseVisualStyleBackColor = false;
            btnThemTV.Click += btnThemTV_Click;
            // 
            // btnXoaTV
            // 
            btnXoaTV.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaTV.BackColor = Color.FromArgb(220, 53, 69);
            btnXoaTV.Enabled = false;
            btnXoaTV.FlatStyle = FlatStyle.Flat;
            btnXoaTV.ForeColor = Color.White;
            btnXoaTV.Location = new Point(180, 241);
            btnXoaTV.Name = "btnXoaTV";
            btnXoaTV.Size = new Size(80, 25);
            btnXoaTV.TabIndex = 15;
            btnXoaTV.Text = "Xóa";
            btnXoaTV.UseVisualStyleBackColor = false;
            btnXoaTV.Click += btnXoaTV_Click;
            // 
            // btnSuaTV
            // 
            btnSuaTV.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSuaTV.BackColor = Color.FromArgb(0, 122, 204);
            btnSuaTV.Enabled = false;
            btnSuaTV.FlatStyle = FlatStyle.Flat;
            btnSuaTV.ForeColor = Color.White;
            btnSuaTV.Location = new Point(94, 241);
            btnSuaTV.Name = "btnSuaTV";
            btnSuaTV.Size = new Size(80, 25);
            btnSuaTV.TabIndex = 14;
            btnSuaTV.Text = "Sửa";
            btnSuaTV.UseVisualStyleBackColor = false;
            btnSuaTV.Click += btnSuaTV_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = Color.White;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.Location = new Point(352, 53);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dataGridView1.Size = new Size(636, 213);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(240, 248, 255);
            statusStrip.Font = new Font("Segoe UI", 9F);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblUserName, lblUserRole, lblDateTime });
            statusStrip.Location = new Point(0, 664);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1007, 24);
            statusStrip.TabIndex = 3;
            statusStrip.Text = "statusStrip1";
            // 
            // lblUserName
            // 
            lblUserName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(107, 19);
            lblUserName.Text = "Người dùng: [Tên]";
            // 
            // lblUserRole
            // 
            lblUserRole.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblUserRole.Font = new Font("Segoe UI", 9F);
            lblUserRole.Name = "lblUserRole";
            lblUserRole.Size = new Size(81, 19);
            lblUserRole.Text = "Vai trò: [Role]";
            // 
            // lblDateTime
            // 
            lblDateTime.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(804, 19);
            lblDateTime.Spring = true;
            lblDateTime.Text = "Ngày giờ: [DateTime]";
            lblDateTime.TextAlign = ContentAlignment.MiddleRight;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { testMenuStrip1ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(163, 26);
            // 
            // testMenuStrip1ToolStripMenuItem
            // 
            testMenuStrip1ToolStripMenuItem.Name = "testMenuStrip1ToolStripMenuItem";
            testMenuStrip1ToolStripMenuItem.Size = new Size(162, 22);
            testMenuStrip1ToolStripMenuItem.Text = "test menu strip 1";
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // panel3
            // 
            panel3.Controls.Add(menuStrip1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1007, 27);
            panel3.TabIndex = 4;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ActiveBorder;
            menuStrip1.Items.AddRange(new ToolStripItem[] { trangChủToolStripMenuItem, đồĂnThứcUốngToolStripMenuItem, thốngKêToolStripMenuItem, quảnLýTàiKhoảnToolStripMenuItem, càiĐặtToolStripMenuItem, đăngXuấtToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1007, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // trangChủToolStripMenuItem
            // 
            trangChủToolStripMenuItem.Name = "trangChủToolStripMenuItem";
            trangChủToolStripMenuItem.Size = new Size(73, 20);
            trangChủToolStripMenuItem.Text = "Trang Chủ";
            // 
            // đồĂnThứcUốngToolStripMenuItem
            // 
            đồĂnThứcUốngToolStripMenuItem.Name = "đồĂnThứcUốngToolStripMenuItem";
            đồĂnThứcUốngToolStripMenuItem.Size = new Size(118, 20);
            đồĂnThứcUốngToolStripMenuItem.Text = "Đồ ăn / Thức uống";
            // 
            // thốngKêToolStripMenuItem
            // 
            thốngKêToolStripMenuItem.Name = "thốngKêToolStripMenuItem";
            thốngKêToolStripMenuItem.Size = new Size(68, 20);
            thốngKêToolStripMenuItem.Text = "Thống kê";
            // 
            // quảnLýTàiKhoảnToolStripMenuItem
            // 
            quảnLýTàiKhoảnToolStripMenuItem.Name = "quảnLýTàiKhoảnToolStripMenuItem";
            quảnLýTàiKhoảnToolStripMenuItem.Size = new Size(112, 20);
            quảnLýTàiKhoảnToolStripMenuItem.Text = "Quản lý tài khoản";
            // 
            // càiĐặtToolStripMenuItem
            // 
            càiĐặtToolStripMenuItem.Name = "càiĐặtToolStripMenuItem";
            càiĐặtToolStripMenuItem.Size = new Size(56, 20);
            càiĐặtToolStripMenuItem.Text = "Cài đặt";
            // 
            // đăngXuấtToolStripMenuItem
            // 
            đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            đăngXuấtToolStripMenuItem.Size = new Size(73, 20);
            đăngXuấtToolStripMenuItem.Text = "Đăng xuất";
            // 
            // pnHienThi
            // 
            pnHienThi.BackColor = Color.IndianRed;
            pnHienThi.Dock = DockStyle.Fill;
            pnHienThi.Location = new Point(0, 0);
            pnHienThi.Name = "pnHienThi";
            pnHienThi.Size = new Size(1007, 688);
            pnHienThi.TabIndex = 5;
            // 
            // frmTrangChu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1007, 688);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(statusStrip);
            Controls.Add(groupBox1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(pnHienThi);
            MainMenuStrip = menuStrip1;
            Name = "frmTrangChu";
            Text = "Form2";
            WindowState = FormWindowState.Maximized;
            groupBox1.ResumeLayout(false);
            pnChiTiet.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPageBan.ResumeLayout(false);
            tabPageBan.PerformLayout();
            tabPageThanhVien.ResumeLayout(false);
            tabPageThanhVien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox1;
        private Panel panel1;
        private TabControl tabControl1;
        private TabPage tabPageBan;
        private TabPage tabPageThanhVien;
        private Label lblBanID;
        private TextBox txtBanID;
        private Label lblTenBan;
        private TextBox txtTenBan;
        private Label lblLoaiBan;
        private TextBox txtLoaiBan;
        private Label lblGiaTheoGio;
        private TextBox txtGiaTheoGio;
        private Label lblTrangThaiBan;
        private ComboBox cmbTrangThaiBan;
        private Button btnSaveBan;
        private Button btnDeleteBan;
        private Button btnNewBan;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblUserName;
        private ToolStripStatusLabel lblUserRole;
        private ToolStripStatusLabel lblDateTime;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem testMenuStrip1ToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private Panel pnChiTiet;
        private Panel panel3;
        private Button btnBoChon;
        private DataGridView dataGridView1;
        private Button btnBoChontv;
        private Button btnThemTV;
        private Button btnXoaTV;
        private Button btnSuaTV;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtGioConLai;
        private TextBox txtDiaChi;
        private TextBox txtSDT;
        private TextBox txtTenThanhVien;
        private Label label5;
        private TextBox textBox1;
        private RadioButton rdNu;
        private RadioButton rdNam;
        private Label label7;
        private Label label6;
        private DateTimePicker dateTimePicker1;
        private Label label8;
        private Label lblNgayDangKy;
        private TextBox txtTimKiem;
        private Button btnNap;
        private Panel pnHienThi;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem trangChủToolStripMenuItem;
        private ToolStripMenuItem đồĂnThứcUốngToolStripMenuItem;
        private ToolStripMenuItem thốngKêToolStripMenuItem;
        private ToolStripMenuItem quảnLýTàiKhoảnToolStripMenuItem;
        private ToolStripMenuItem càiĐặtToolStripMenuItem;
        private ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private Button btnDatBan;
        private Button btnBaoTri;
        private Button button1;
    }
}