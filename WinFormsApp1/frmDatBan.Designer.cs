namespace WinFormsApp1
{
    partial class frmDatBan
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
            lblGiaBan = new Label();
            lblKhachHang = new Label();
            cmbKhachHang = new ComboBox();
            txtTenKhachVangLai = new TextBox();
            lblTenKhachVangLai = new Label();
            lblSoTienTra = new Label();
            txtSoTienTra = new TextBox();
            lblSoGioChoi = new Label();
            lblSoGioChoiValue = new Label();
            btnDatBan = new Button();
            btnHuy = new Button();
            rdKhachHang = new RadioButton();
            rdKhachVangLai = new RadioButton();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
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
            // lblGiaBan
            // 
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(12, 40);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Size = new Size(77, 15);
            lblGiaBan.TabIndex = 1;
            lblGiaBan.Text = "Giá theo giờ: ";
            // 
            // lblKhachHang
            // 
            lblKhachHang.AutoSize = true;
            lblKhachHang.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKhachHang.Location = new Point(12, 70);
            lblKhachHang.Name = "lblKhachHang";
            lblKhachHang.Size = new Size(86, 19);
            lblKhachHang.TabIndex = 2;
            lblKhachHang.Text = "Khách hàng";
            // 
            // cmbKhachHang
            // 
            cmbKhachHang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbKhachHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKhachHang.FormattingEnabled = true;
            cmbKhachHang.Location = new Point(6, 62);
            cmbKhachHang.Name = "cmbKhachHang";
            cmbKhachHang.Size = new Size(240, 23);
            cmbKhachHang.TabIndex = 3;
            // 
            // txtTenKhachVangLai
            // 
            txtTenKhachVangLai.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTenKhachVangLai.Location = new Point(6, 62);
            txtTenKhachVangLai.Name = "txtTenKhachVangLai";
            txtTenKhachVangLai.PlaceholderText = "Nhập tên khách vãng lai";
            txtTenKhachVangLai.Size = new Size(240, 23);
            txtTenKhachVangLai.TabIndex = 4;
            txtTenKhachVangLai.Visible = false;
            // 
            // lblTenKhachVangLai
            // 
            lblTenKhachVangLai.AutoSize = true;
            lblTenKhachVangLai.Location = new Point(6, 44);
            lblTenKhachVangLai.Name = "lblTenKhachVangLai";
            lblTenKhachVangLai.Size = new Size(107, 15);
            lblTenKhachVangLai.TabIndex = 5;
            lblTenKhachVangLai.Text = "Tên khách vãng lai:";
            lblTenKhachVangLai.Visible = false;
            // 
            // lblSoTienTra
            // 
            lblSoTienTra.AutoSize = true;
            lblSoTienTra.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSoTienTra.Location = new Point(11, 189);
            lblSoTienTra.Name = "lblSoTienTra";
            lblSoTienTra.Size = new Size(59, 19);
            lblSoTienTra.TabIndex = 6;
            lblSoTienTra.Text = "Số tiền:";
            // 
            // txtSoTienTra
            // 
            txtSoTienTra.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSoTienTra.Font = new Font("Segoe UI", 10F);
            txtSoTienTra.Location = new Point(12, 211);
            txtSoTienTra.Name = "txtSoTienTra";
            txtSoTienTra.PlaceholderText = "Nhập số tiền khách trả";
            txtSoTienTra.Size = new Size(252, 25);
            txtSoTienTra.TabIndex = 7;
            txtSoTienTra.TextChanged += TxtSoTienTra_TextChanged;
            // 
            // lblSoGioChoi
            // 
            lblSoGioChoi.AutoSize = true;
            lblSoGioChoi.Location = new Point(12, 260);
            lblSoGioChoi.Name = "lblSoGioChoi";
            lblSoGioChoi.Size = new Size(69, 15);
            lblSoGioChoi.TabIndex = 8;
            lblSoGioChoi.Text = "Số giờ chơi:";
            // 
            // lblSoGioChoiValue
            // 
            lblSoGioChoiValue.AutoSize = true;
            lblSoGioChoiValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSoGioChoiValue.ForeColor = Color.Blue;
            lblSoGioChoiValue.Location = new Point(88, 258);
            lblSoGioChoiValue.Name = "lblSoGioChoiValue";
            lblSoGioChoiValue.Size = new Size(17, 19);
            lblSoGioChoiValue.TabIndex = 9;
            lblSoGioChoiValue.Text = "0";
            // 
            // btnDatBan
            // 
            btnDatBan.BackColor = Color.FromArgb(40, 167, 69);
            btnDatBan.FlatStyle = FlatStyle.Flat;
            btnDatBan.ForeColor = Color.White;
            btnDatBan.Location = new Point(12, 290);
            btnDatBan.Name = "btnDatBan";
            btnDatBan.Size = new Size(120, 35);
            btnDatBan.TabIndex = 10;
            btnDatBan.Text = "Đặt bàn";
            btnDatBan.UseVisualStyleBackColor = false;
            btnDatBan.Click += BtnDatBan_Click;
            // 
            // btnHuy
            // 
            btnHuy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHuy.BackColor = Color.FromArgb(220, 53, 69);
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(144, 290);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(120, 35);
            btnHuy.TabIndex = 11;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += BtnHuy_Click;
            // 
            // rdKhachHang
            // 
            rdKhachHang.AutoSize = true;
            rdKhachHang.Checked = true;
            rdKhachHang.Location = new Point(6, 22);
            rdKhachHang.Name = "rdKhachHang";
            rdKhachHang.Size = new Size(88, 19);
            rdKhachHang.TabIndex = 12;
            rdKhachHang.TabStop = true;
            rdKhachHang.Text = "Khách hàng";
            rdKhachHang.UseVisualStyleBackColor = true;
            rdKhachHang.CheckedChanged += RdKhachHang_CheckedChanged;
            // 
            // rdKhachVangLai
            // 
            rdKhachVangLai.AutoSize = true;
            rdKhachVangLai.Location = new Point(120, 22);
            rdKhachVangLai.Name = "rdKhachVangLai";
            rdKhachVangLai.Size = new Size(102, 19);
            rdKhachVangLai.TabIndex = 13;
            rdKhachVangLai.Text = "Khách vãng lai";
            rdKhachVangLai.UseVisualStyleBackColor = true;
            rdKhachVangLai.CheckedChanged += RdKhachVangLai_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(rdKhachHang);
            groupBox1.Controls.Add(rdKhachVangLai);
            groupBox1.Controls.Add(cmbKhachHang);
            groupBox1.Controls.Add(txtTenKhachVangLai);
            groupBox1.Controls.Add(lblTenKhachVangLai);
            groupBox1.Location = new Point(12, 92);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(252, 94);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            // 
            // frmDatBan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(276, 340);
            Controls.Add(groupBox1);
            Controls.Add(btnHuy);
            Controls.Add(btnDatBan);
            Controls.Add(lblSoGioChoiValue);
            Controls.Add(lblSoGioChoi);
            Controls.Add(txtSoTienTra);
            Controls.Add(lblSoTienTra);
            Controls.Add(lblKhachHang);
            Controls.Add(lblGiaBan);
            Controls.Add(lblTenBan);
            Name = "frmDatBan";
            Text = "Đặt bàn";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTenBan;
        private Label lblGiaBan;
        private Label lblKhachHang;
        private ComboBox cmbKhachHang;
        private TextBox txtTenKhachVangLai;
        private Label lblTenKhachVangLai;
        private Label lblSoTienTra;
        private TextBox txtSoTienTra;
        private Label lblSoGioChoi;
        private Label lblSoGioChoiValue;
        private Button btnDatBan;
        private Button btnHuy;
        private RadioButton rdKhachHang;
        private RadioButton rdKhachVangLai;
        private GroupBox groupBox1;
    }
}