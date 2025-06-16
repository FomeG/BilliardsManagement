namespace WinFormsApp1
{
    partial class frmNapTien
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
            lblKhachHang = new Label();
            lblSoTienHienTai = new Label();
            label1 = new Label();
            txtSoTienNap = new TextBox();
            btnNapTien = new Button();
            SuspendLayout();
            //
            // lblKhachHang
            //
            lblKhachHang.AutoSize = true;
            lblKhachHang.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblKhachHang.Location = new Point(12, 9);
            lblKhachHang.Name = "lblKhachHang";
            lblKhachHang.Size = new Size(75, 15);
            lblKhachHang.TabIndex = 0;
            lblKhachHang.Text = "Khách hàng:";
            //
            // lblSoTienHienTai
            //
            lblSoTienHienTai.AutoSize = true;
            lblSoTienHienTai.ForeColor = Color.Blue;
            lblSoTienHienTai.Location = new Point(12, 30);
            lblSoTienHienTai.Name = "lblSoTienHienTai";
            lblSoTienHienTai.Size = new Size(95, 15);
            lblSoTienHienTai.TabIndex = 1;
            lblSoTienHienTai.Text = "Số tiền hiện tại:";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new Point(12, 55);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 2;
            label1.Text = "Nhập số tiền cần nạp";
            //
            // txtSoTienNap
            //
            txtSoTienNap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtSoTienNap.Location = new Point(12, 73);
            txtSoTienNap.Name = "txtSoTienNap";
            txtSoTienNap.PlaceholderText = "Nhập số tiền...";
            txtSoTienNap.Size = new Size(260, 23);
            txtSoTienNap.TabIndex = 3;
            txtSoTienNap.TextAlign = HorizontalAlignment.Right;
            //
            // btnNapTien
            //
            btnNapTien.BackColor = Color.FromArgb(46, 125, 50);
            btnNapTien.FlatStyle = FlatStyle.Flat;
            btnNapTien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNapTien.ForeColor = Color.White;
            btnNapTien.Location = new Point(12, 110);
            btnNapTien.Name = "btnNapTien";
            btnNapTien.Size = new Size(260, 35);
            btnNapTien.TabIndex = 4;
            btnNapTien.Text = "Nạp";
            btnNapTien.UseVisualStyleBackColor = false;
            //
            // frmNapTien
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 161);
            Controls.Add(btnNapTien);
            Controls.Add(txtSoTienNap);
            Controls.Add(label1);
            Controls.Add(lblSoTienHienTai);
            Controls.Add(lblKhachHang);
            Name = "frmNapTien";
            Text = "Nạp tiền";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblKhachHang;
        private Label lblSoTienHienTai;
        private Label label1;
        private TextBox txtSoTienNap;
        private Button btnNapTien;
    }
}