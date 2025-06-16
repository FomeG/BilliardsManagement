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
            flowLayoutPanelTop = new FlowLayoutPanel();
            lblTuNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            lblDenNgay = new Label();
            dtpDenNgay = new DateTimePicker();
            btnLoc = new Button();
            btnHomNay = new Button();
            btnThangNay = new Button();
            btnNamNay = new Button();
            panelSummary = new Panel();
            tableLayoutPanelSummary = new TableLayoutPanel();
            lblTongDoanhThu = new Label();
            lblSoHoaDon = new Label();
            lblDoanhThuTrungBinh = new Label();
            chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();

            panelTop.SuspendLayout();
            flowLayoutPanelTop.SuspendLayout();
            panelSummary.SuspendLayout();
            tableLayoutPanelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartDoanhThu).BeginInit();
            SuspendLayout();
            //
            // panelTop
            //
            panelTop.BackColor = Color.LightGray;
            panelTop.Controls.Add(flowLayoutPanelTop);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(10);
            panelTop.Size = new Size(1200, 80);
            panelTop.TabIndex = 0;
            //
            // flowLayoutPanelTop
            //
            flowLayoutPanelTop.AutoSize = true;
            flowLayoutPanelTop.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanelTop.Controls.Add(lblTuNgay);
            flowLayoutPanelTop.Controls.Add(dtpTuNgay);
            flowLayoutPanelTop.Controls.Add(lblDenNgay);
            flowLayoutPanelTop.Controls.Add(dtpDenNgay);
            flowLayoutPanelTop.Controls.Add(btnLoc);
            flowLayoutPanelTop.Controls.Add(btnHomNay);
            flowLayoutPanelTop.Controls.Add(btnThangNay);
            flowLayoutPanelTop.Controls.Add(btnNamNay);
            flowLayoutPanelTop.Dock = DockStyle.Fill;
            flowLayoutPanelTop.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelTop.Location = new Point(10, 10);
            flowLayoutPanelTop.Name = "flowLayoutPanelTop";
            flowLayoutPanelTop.Size = new Size(1180, 60);
            flowLayoutPanelTop.TabIndex = 0;
            flowLayoutPanelTop.WrapContents = true;
            //
            // lblTuNgay
            //
            lblTuNgay.Anchor = AnchorStyles.Left;
            lblTuNgay.AutoSize = true;
            lblTuNgay.Location = new Point(3, 8);
            lblTuNgay.Margin = new Padding(3, 8, 3, 0);
            lblTuNgay.Name = "lblTuNgay";
            lblTuNgay.Size = new Size(52, 15);
            lblTuNgay.TabIndex = 0;
            lblTuNgay.Text = "Từ ngày:";
            //
            // dtpTuNgay
            //
            dtpTuNgay.Anchor = AnchorStyles.Left;
            dtpTuNgay.Format = DateTimePickerFormat.Short;
            dtpTuNgay.Location = new Point(61, 3);
            dtpTuNgay.Margin = new Padding(3, 3, 10, 3);
            dtpTuNgay.MinimumSize = new Size(120, 23);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(120, 23);
            dtpTuNgay.TabIndex = 1;
            //
            // lblDenNgay
            //
            lblDenNgay.Anchor = AnchorStyles.Left;
            lblDenNgay.AutoSize = true;
            lblDenNgay.Location = new Point(194, 8);
            lblDenNgay.Margin = new Padding(3, 8, 3, 0);
            lblDenNgay.Name = "lblDenNgay";
            lblDenNgay.Size = new Size(60, 15);
            lblDenNgay.TabIndex = 2;
            lblDenNgay.Text = "Đến ngày:";
            //
            // dtpDenNgay
            //
            dtpDenNgay.Anchor = AnchorStyles.Left;
            dtpDenNgay.Format = DateTimePickerFormat.Short;
            dtpDenNgay.Location = new Point(260, 3);
            dtpDenNgay.Margin = new Padding(3, 3, 10, 3);
            dtpDenNgay.MinimumSize = new Size(120, 23);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(120, 23);
            dtpDenNgay.TabIndex = 3;
            //
            // btnLoc
            //
            btnLoc.Anchor = AnchorStyles.Left;
            btnLoc.Location = new Point(393, 2);
            btnLoc.Margin = new Padding(3, 2, 5, 3);
            btnLoc.MinimumSize = new Size(75, 25);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(75, 25);
            btnLoc.TabIndex = 4;
            btnLoc.Text = "Lọc";
            btnLoc.UseVisualStyleBackColor = true;
            btnLoc.Click += btnLoc_Click;
            //
            // btnHomNay
            //
            btnHomNay.Anchor = AnchorStyles.Left;
            btnHomNay.Location = new Point(476, 2);
            btnHomNay.Margin = new Padding(3, 2, 5, 3);
            btnHomNay.MinimumSize = new Size(75, 25);
            btnHomNay.Name = "btnHomNay";
            btnHomNay.Size = new Size(75, 25);
            btnHomNay.TabIndex = 5;
            btnHomNay.Text = "Hôm nay";
            btnHomNay.UseVisualStyleBackColor = true;
            btnHomNay.Click += btnHomNay_Click;
            //
            // btnThangNay
            //
            btnThangNay.Anchor = AnchorStyles.Left;
            btnThangNay.Location = new Point(559, 2);
            btnThangNay.Margin = new Padding(3, 2, 5, 3);
            btnThangNay.MinimumSize = new Size(80, 25);
            btnThangNay.Name = "btnThangNay";
            btnThangNay.Size = new Size(80, 25);
            btnThangNay.TabIndex = 6;
            btnThangNay.Text = "Tháng này";
            btnThangNay.UseVisualStyleBackColor = true;
            btnThangNay.Click += btnThangNay_Click;
            //
            // btnNamNay
            //
            btnNamNay.Anchor = AnchorStyles.Left;
            btnNamNay.Location = new Point(647, 2);
            btnNamNay.Margin = new Padding(3, 2, 5, 3);
            btnNamNay.MinimumSize = new Size(75, 25);
            btnNamNay.Name = "btnNamNay";
            btnNamNay.Size = new Size(75, 25);
            btnNamNay.TabIndex = 7;
            btnNamNay.Text = "Năm này";
            btnNamNay.UseVisualStyleBackColor = true;
            btnNamNay.Click += btnNamNay_Click;
            //
            // panelSummary
            //
            panelSummary.BackColor = Color.WhiteSmoke;
            panelSummary.Controls.Add(tableLayoutPanelSummary);
            panelSummary.Dock = DockStyle.Top;
            panelSummary.Location = new Point(0, 80);
            panelSummary.Name = "panelSummary";
            panelSummary.Padding = new Padding(10);
            panelSummary.Size = new Size(1200, 100);
            panelSummary.TabIndex = 1;
            //
            // tableLayoutPanelSummary
            //
            tableLayoutPanelSummary.AutoSize = true;
            tableLayoutPanelSummary.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelSummary.ColumnCount = 3;
            tableLayoutPanelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tableLayoutPanelSummary.Controls.Add(lblTongDoanhThu, 0, 0);
            tableLayoutPanelSummary.Controls.Add(lblDoanhThuTrungBinh, 1, 0);
            tableLayoutPanelSummary.Controls.Add(lblSoHoaDon, 2, 0);
            tableLayoutPanelSummary.Dock = DockStyle.Fill;
            tableLayoutPanelSummary.Location = new Point(10, 10);
            tableLayoutPanelSummary.Name = "tableLayoutPanelSummary";
            tableLayoutPanelSummary.RowCount = 1;
            tableLayoutPanelSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelSummary.Size = new Size(1180, 80);
            tableLayoutPanelSummary.TabIndex = 0;
            //
            // lblTongDoanhThu
            //
            lblTongDoanhThu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblTongDoanhThu.AutoSize = true;
            lblTongDoanhThu.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblTongDoanhThu.ForeColor = Color.Green;
            lblTongDoanhThu.Location = new Point(3, 30);
            lblTongDoanhThu.Name = "lblTongDoanhThu";
            lblTongDoanhThu.Size = new Size(387, 20);
            lblTongDoanhThu.TabIndex = 0;
            lblTongDoanhThu.Text = "Tổng Doanh Thu: 0 VNĐ";
            lblTongDoanhThu.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblDoanhThuTrungBinh
            //
            lblDoanhThuTrungBinh.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblDoanhThuTrungBinh.AutoSize = true;
            lblDoanhThuTrungBinh.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblDoanhThuTrungBinh.ForeColor = Color.Orange;
            lblDoanhThuTrungBinh.Location = new Point(396, 30);
            lblDoanhThuTrungBinh.Name = "lblDoanhThuTrungBinh";
            lblDoanhThuTrungBinh.Size = new Size(387, 20);
            lblDoanhThuTrungBinh.TabIndex = 2;
            lblDoanhThuTrungBinh.Text = "Doanh Thu Trung Bình: 0 VNĐ";
            lblDoanhThuTrungBinh.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblSoHoaDon
            //
            lblSoHoaDon.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblSoHoaDon.AutoSize = true;
            lblSoHoaDon.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblSoHoaDon.ForeColor = Color.Blue;
            lblSoHoaDon.Location = new Point(789, 30);
            lblSoHoaDon.Name = "lblSoHoaDon";
            lblSoHoaDon.Size = new Size(388, 20);
            lblSoHoaDon.TabIndex = 1;
            lblSoHoaDon.Text = "Số Hóa Đơn: 0";
            lblSoHoaDon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // chartDoanhThu
            //
            chartArea1.Name = "ChartArea1";
            chartDoanhThu.ChartAreas.Add(chartArea1);
            chartDoanhThu.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            chartDoanhThu.Legends.Add(legend1);
            chartDoanhThu.Location = new Point(0, 180);
            chartDoanhThu.Name = "chartDoanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartDoanhThu.Series.Add(series1);
            chartDoanhThu.Size = new Size(1200, 420);
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
            MinimumSize = new Size(800, 600);
            Name = "frmThongKe";
            Text = "Thống Kê Doanh Thu";
            WindowState = FormWindowState.Maximized;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            flowLayoutPanelTop.ResumeLayout(false);
            flowLayoutPanelTop.PerformLayout();
            panelSummary.ResumeLayout(false);
            panelSummary.PerformLayout();
            tableLayoutPanelSummary.ResumeLayout(false);
            tableLayoutPanelSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartDoanhThu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private FlowLayoutPanel flowLayoutPanelTop;
        private Label lblTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label lblDenNgay;
        private DateTimePicker dtpDenNgay;
        private Button btnLoc;
        private Button btnHomNay;
        private Button btnThangNay;
        private Button btnNamNay;
        private Panel panelSummary;
        private TableLayoutPanel tableLayoutPanelSummary;
        private Label lblTongDoanhThu;
        private Label lblSoHoaDon;
        private Label lblDoanhThuTrungBinh;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
    }
}