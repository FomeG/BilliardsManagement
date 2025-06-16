namespace WinFormsApp1
{
    partial class frmThongKe
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

            panelTop = new Panel();
            lblTuNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            lblDenNgay = new Label();
            dtpDenNgay = new DateTimePicker();
            btnLoc = new Button();
            btnHomNay = new Button();
            btnThangNay = new Button();
            btnNamNay = new Button();

            panelSummary = new Panel();
            lblTongDoanhThu = new Label();
            lblSoHoaDon = new Label();
            lblDoanhThuTrungBinh = new Label();

            chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();

            panelTop.SuspendLayout();
            panelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartDoanhThu).BeginInit();
            SuspendLayout();

            //
            // panelTop
            //
            panelTop.Controls.Add(lblTuNgay);
            panelTop.Controls.Add(dtpTuNgay);
            panelTop.Controls.Add(lblDenNgay);
            panelTop.Controls.Add(dtpDenNgay);
            panelTop.Controls.Add(btnLoc);
            panelTop.Controls.Add(btnHomNay);
            panelTop.Controls.Add(btnThangNay);
            panelTop.Controls.Add(btnNamNay);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1200, 60);
            panelTop.TabIndex = 0;
            panelTop.BackColor = Color.LightGray;

            //
            // lblTuNgay
            //
            lblTuNgay.AutoSize = true;
            lblTuNgay.Location = new Point(20, 20);
            lblTuNgay.Name = "lblTuNgay";
            lblTuNgay.Size = new Size(55, 15);
            lblTuNgay.TabIndex = 0;
            lblTuNgay.Text = "Từ ngày:";

            //
            // dtpTuNgay
            //
            dtpTuNgay.Format = DateTimePickerFormat.Short;
            dtpTuNgay.Location = new Point(80, 17);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(120, 23);
            dtpTuNgay.TabIndex = 1;

            //
            // lblDenNgay
            //
            lblDenNgay.AutoSize = true;
            lblDenNgay.Location = new Point(220, 20);
            lblDenNgay.Name = "lblDenNgay";
            lblDenNgay.Size = new Size(63, 15);
            lblDenNgay.TabIndex = 2;
            lblDenNgay.Text = "Đến ngày:";

            //
            // dtpDenNgay
            //
            dtpDenNgay.Format = DateTimePickerFormat.Short;
            dtpDenNgay.Location = new Point(290, 17);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(120, 23);
            dtpDenNgay.TabIndex = 3;

            //
            // btnLoc
            //
            btnLoc.Location = new Point(430, 16);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(75, 25);
            btnLoc.TabIndex = 4;
            btnLoc.Text = "Lọc";
            btnLoc.UseVisualStyleBackColor = true;
            btnLoc.Click += btnLoc_Click;

            //
            // btnHomNay
            //
            btnHomNay.Location = new Point(520, 16);
            btnHomNay.Name = "btnHomNay";
            btnHomNay.Size = new Size(75, 25);
            btnHomNay.TabIndex = 5;
            btnHomNay.Text = "Hôm nay";
            btnHomNay.UseVisualStyleBackColor = true;
            btnHomNay.Click += btnHomNay_Click;

            //
            // btnThangNay
            //
            btnThangNay.Location = new Point(610, 16);
            btnThangNay.Name = "btnThangNay";
            btnThangNay.Size = new Size(80, 25);
            btnThangNay.TabIndex = 6;
            btnThangNay.Text = "Tháng này";
            btnThangNay.UseVisualStyleBackColor = true;
            btnThangNay.Click += btnThangNay_Click;

            //
            // btnNamNay
            //
            btnNamNay.Location = new Point(700, 16);
            btnNamNay.Name = "btnNamNay";
            btnNamNay.Size = new Size(75, 25);
            btnNamNay.TabIndex = 7;
            btnNamNay.Text = "Năm này";
            btnNamNay.UseVisualStyleBackColor = true;
            btnNamNay.Click += btnNamNay_Click;

            //
            // panelSummary
            //
            panelSummary.Controls.Add(lblTongDoanhThu);
            panelSummary.Controls.Add(lblSoHoaDon);
            panelSummary.Controls.Add(lblDoanhThuTrungBinh);
            panelSummary.Dock = DockStyle.Top;
            panelSummary.Location = new Point(0, 60);
            panelSummary.Name = "panelSummary";
            panelSummary.Size = new Size(1200, 80);
            panelSummary.TabIndex = 1;
            panelSummary.BackColor = Color.WhiteSmoke;

            //
            // lblTongDoanhThu
            //
            lblTongDoanhThu.AutoSize = true;
            lblTongDoanhThu.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblTongDoanhThu.ForeColor = Color.Green;
            lblTongDoanhThu.Location = new Point(20, 15);
            lblTongDoanhThu.Name = "lblTongDoanhThu";
            lblTongDoanhThu.Size = new Size(150, 20);
            lblTongDoanhThu.TabIndex = 0;
            lblTongDoanhThu.Text = "Tổng Doanh Thu: 0 VNĐ";

            //
            // lblSoHoaDon
            //
            lblSoHoaDon.AutoSize = true;
            lblSoHoaDon.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblSoHoaDon.ForeColor = Color.Blue;
            lblSoHoaDon.Location = new Point(20, 45);
            lblSoHoaDon.Name = "lblSoHoaDon";
            lblSoHoaDon.Size = new Size(120, 20);
            lblSoHoaDon.TabIndex = 1;
            lblSoHoaDon.Text = "Số Hóa Đơn: 0";

            //
            // lblDoanhThuTrungBinh
            //
            lblDoanhThuTrungBinh.AutoSize = true;
            lblDoanhThuTrungBinh.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblDoanhThuTrungBinh.ForeColor = Color.Orange;
            lblDoanhThuTrungBinh.Location = new Point(400, 15);
            lblDoanhThuTrungBinh.Name = "lblDoanhThuTrungBinh";
            lblDoanhThuTrungBinh.Size = new Size(200, 20);
            lblDoanhThuTrungBinh.TabIndex = 2;
            lblDoanhThuTrungBinh.Text = "Doanh Thu Trung Bình: 0 VNĐ";

            //
            // chartDoanhThu
            //
            chartArea1.Name = "ChartArea1";
            chartDoanhThu.ChartAreas.Add(chartArea1);
            chartDoanhThu.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            chartDoanhThu.Legends.Add(legend1);
            chartDoanhThu.Location = new Point(0, 140);
            chartDoanhThu.Name = "chartDoanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartDoanhThu.Series.Add(series1);
            chartDoanhThu.Size = new Size(1200, 460);
            chartDoanhThu.TabIndex = 2;
            chartDoanhThu.Text = "chart1";

            //
            // frmThongKe
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 600);
            Controls.Add(chartDoanhThu);
            Controls.Add(panelSummary);
            Controls.Add(panelTop);
            Name = "frmThongKe";
            Text = "Thống Kê Doanh Thu";
            WindowState = FormWindowState.Maximized;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelSummary.ResumeLayout(false);
            panelSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartDoanhThu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label lblDenNgay;
        private DateTimePicker dtpDenNgay;
        private Button btnLoc;
        private Button btnHomNay;
        private Button btnThangNay;
        private Button btnNamNay;
        private Panel panelSummary;
        private Label lblTongDoanhThu;
        private Label lblSoHoaDon;
        private Label lblDoanhThuTrungBinh;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
    }
}