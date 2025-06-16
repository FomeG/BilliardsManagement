using Microsoft.EntityFrameworkCore;
using Models.HandleData;
using Models.Models;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinFormsApp1
{
    public partial class frmThongKe : Form
    {
        private readonly DaDBContext dbContext;
        private readonly TaiKhoan currentUser;

        public frmThongKe(DaDBContext context, TaiKhoan user)
        {
            InitializeComponent();
            dbContext = context;
            currentUser = user;

            SetupForm();
            LoadDefaultData();

            // Đăng ký sự kiện resize để xử lý responsive
            this.Resize += FrmThongKe_Resize;
            this.Load += FrmThongKe_Load;
        }

        private void SetupForm()
        {
            this.Text = "Thống Kê Doanh Thu";
            this.WindowState = FormWindowState.Maximized;

            // Thiết lập DateTimePicker mặc định cho tháng hiện tại
            var now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1); // Ngày đầu tháng
            dtpDenNgay.Value = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)); // Ngày cuối tháng

            SetupChart();
        }

        private void SetupChart()
        {
            // Cấu hình biểu đồ
            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas.Clear();
            chartDoanhThu.Legends.Clear();

            // Tạo ChartArea
            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "Ngày";
            chartArea.AxisY.Title = "Doanh Thu (VNĐ)";
            chartArea.AxisX.MajorGrid.Enabled = true;
            chartArea.AxisY.MajorGrid.Enabled = true;
            chartArea.AxisX.LabelStyle.Format = "dd/MM";
            chartArea.AxisY.LabelStyle.Format = "N0";
            chartDoanhThu.ChartAreas.Add(chartArea);

            // Tạo Series cho biểu đồ đường
            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.Color = Color.Blue;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 6;
            series.MarkerColor = Color.Red;
            chartDoanhThu.Series.Add(series);

            // Cấu hình Legend
            Legend legend = new Legend("Legend1");
            legend.Docking = Docking.Top;
            chartDoanhThu.Legends.Add(legend);
        }

        private async void LoadDefaultData()
        {
            await LoadThongKeData();
        }

        private async Task LoadThongKeData()
        {
            try
            {
                var tuNgay = dtpTuNgay.Value.Date;
                var denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1); // Cuối ngày

                // Lấy dữ liệu hóa đơn đã hoàn thành trong khoảng thời gian
                var hoaDons = await dbContext.HoaDons
                    .Where(h => h.TrangThai == 1 && h.NgayLap >= tuNgay && h.NgayLap <= denNgay)
                    .Select(h => new { h.NgayLap, h.TongTien })
                    .ToListAsync();

                // Nhóm theo ngày và tính tổng doanh thu
                var doanhThuTheoNgay = hoaDons
                    .GroupBy(h => h.NgayLap.Date)
                    .Select(g => new
                    {
                        Ngay = g.Key,
                        TongDoanhThu = g.Sum(h => h.TongTien)
                    })
                    .OrderBy(x => x.Ngay)
                    .ToList();

                // Cập nhật biểu đồ
                UpdateChart(doanhThuTheoNgay.Cast<object>().ToList());

                // Cập nhật thông tin tổng quan
                UpdateSummaryInfo(hoaDons.Cast<object>().ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu thống kê: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateChart(List<object> data)
        {
            chartDoanhThu.Series["Doanh Thu"].Points.Clear();

            foreach (dynamic item in data)
            {
                var ngay = (DateTime)item.Ngay;
                var doanhThu = (decimal)item.TongDoanhThu;

                chartDoanhThu.Series["Doanh Thu"].Points.AddXY(ngay, doanhThu);
            }

            // Cập nhật tiêu đề biểu đồ
            chartDoanhThu.Titles.Clear();
            chartDoanhThu.Titles.Add($"Biểu Đồ Doanh Thu Từ {dtpTuNgay.Value:dd/MM/yyyy} Đến {dtpDenNgay.Value:dd/MM/yyyy}");
        }

        private void UpdateSummaryInfo(List<object> hoaDons)
        {
            var tongDoanhThu = hoaDons.Sum(h => (decimal)((dynamic)h).TongTien);
            var soHoaDon = hoaDons.Count;
            var doanhThuTrungBinh = soHoaDon > 0 ? tongDoanhThu / soHoaDon : 0;

            lblTongDoanhThu.Text = $"Tổng Doanh Thu: {tongDoanhThu:N0} VNĐ";
            lblSoHoaDon.Text = $"Số Hóa Đơn: {soHoaDon}";
            lblDoanhThuTrungBinh.Text = $"Doanh Thu Trung Bình: {doanhThuTrungBinh:N0} VNĐ";
        }

        private async void btnLoc_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value > dtpDenNgay.Value)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await LoadThongKeData();
        }

        private void btnHomNay_Click(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            dtpTuNgay.Value = today;
            dtpDenNgay.Value = today;
        }

        private void btnThangNay_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
        }

        private void btnNamNay_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, 1, 1);
            dtpDenNgay.Value = new DateTime(now.Year, 12, 31);
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            AdjustLayoutForCurrentSize();
        }

        private void FrmThongKe_Resize(object sender, EventArgs e)
        {
            AdjustLayoutForCurrentSize();
        }

        private void AdjustLayoutForCurrentSize()
        {
            if (this.WindowState == FormWindowState.Minimized)
                return;

            // Điều chỉnh chiều cao của các panel dựa trên kích thước form
            AdjustPanelHeights();

            // Điều chỉnh font size cho responsive
            AdjustFontSizes();

            // Điều chỉnh layout của summary panel cho màn hình nhỏ
            AdjustSummaryLayout();

            // Điều chỉnh layout của top panel
            AdjustTopPanelLayout();
        }

        private void AdjustPanelHeights()
        {
            // Tính toán chiều cao tối ưu cho các panel
            int formHeight = this.ClientSize.Height;

            // Panel top: tối thiểu 60px, tối đa 100px
            int topPanelHeight = Math.Max(60, Math.Min(100, formHeight / 10));
            panelTop.Height = topPanelHeight;

            // Panel summary: tối thiểu 80px, tối đa 120px
            int summaryPanelHeight = Math.Max(80, Math.Min(120, formHeight / 8));
            panelSummary.Height = summaryPanelHeight;
        }

        private void AdjustFontSizes()
        {
            // Điều chỉnh font size dựa trên kích thước form
            int formWidth = this.ClientSize.Width;

            float baseFontSize = 12F;
            if (formWidth < 1000)
                baseFontSize = 10F;
            else if (formWidth > 1400)
                baseFontSize = 14F;

            // Cập nhật font cho các label summary
            Font summaryFont = new Font("Microsoft Sans Serif", baseFontSize, FontStyle.Bold);
            lblTongDoanhThu.Font = summaryFont;
            lblSoHoaDon.Font = summaryFont;
            lblDoanhThuTrungBinh.Font = summaryFont;
        }

        private void AdjustSummaryLayout()
        {
            // Thay đổi layout của summary panel dựa trên kích thước màn hình
            int formWidth = this.ClientSize.Width;

            if (formWidth < 900)
            {
                // Màn hình nhỏ: chuyển sang layout dọc
                tableLayoutPanelSummary.RowCount = 3;
                tableLayoutPanelSummary.ColumnCount = 1;

                tableLayoutPanelSummary.RowStyles.Clear();
                tableLayoutPanelSummary.ColumnStyles.Clear();

                tableLayoutPanelSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                tableLayoutPanelSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                tableLayoutPanelSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
                tableLayoutPanelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                // Đặt lại vị trí controls
                tableLayoutPanelSummary.SetRow(lblTongDoanhThu, 0);
                tableLayoutPanelSummary.SetColumn(lblTongDoanhThu, 0);
                tableLayoutPanelSummary.SetRow(lblDoanhThuTrungBinh, 1);
                tableLayoutPanelSummary.SetColumn(lblDoanhThuTrungBinh, 0);
                tableLayoutPanelSummary.SetRow(lblSoHoaDon, 2);
                tableLayoutPanelSummary.SetColumn(lblSoHoaDon, 0);

                // Tăng chiều cao panel summary cho layout dọc
                panelSummary.Height = Math.Max(120, this.ClientSize.Height / 6);
            }
            else
            {
                // Màn hình lớn: layout ngang
                tableLayoutPanelSummary.RowCount = 1;
                tableLayoutPanelSummary.ColumnCount = 3;

                tableLayoutPanelSummary.RowStyles.Clear();
                tableLayoutPanelSummary.ColumnStyles.Clear();

                tableLayoutPanelSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                tableLayoutPanelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                tableLayoutPanelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                tableLayoutPanelSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));

                // Đặt lại vị trí controls
                tableLayoutPanelSummary.SetRow(lblTongDoanhThu, 0);
                tableLayoutPanelSummary.SetColumn(lblTongDoanhThu, 0);
                tableLayoutPanelSummary.SetRow(lblDoanhThuTrungBinh, 0);
                tableLayoutPanelSummary.SetColumn(lblDoanhThuTrungBinh, 1);
                tableLayoutPanelSummary.SetRow(lblSoHoaDon, 0);
                tableLayoutPanelSummary.SetColumn(lblSoHoaDon, 2);
            }
        }

        private void AdjustTopPanelLayout()
        {
            // Điều chỉnh kích thước controls trong top panel dựa trên kích thước form
            int formWidth = this.ClientSize.Width;

            if (formWidth < 800)
            {
                // Màn hình nhỏ: giảm kích thước buttons và controls
                foreach (Control control in flowLayoutPanelTop.Controls)
                {
                    if (control is Button btn)
                    {
                        btn.MinimumSize = new Size(60, 23);
                        btn.Size = new Size(60, 23);
                        btn.Margin = new Padding(2);
                    }
                    else if (control is DateTimePicker dtp)
                    {
                        dtp.MinimumSize = new Size(100, 23);
                        dtp.Size = new Size(100, 23);
                        dtp.Margin = new Padding(2, 3, 8, 3);
                    }
                    else if (control is Label lbl)
                    {
                        lbl.Margin = new Padding(2, 6, 2, 0);
                    }
                }

                // Tăng chiều cao panel để chứa wrapped controls
                panelTop.Height = Math.Max(80, this.ClientSize.Height / 8);
            }
            else
            {
                // Màn hình lớn: kích thước bình thường
                foreach (Control control in flowLayoutPanelTop.Controls)
                {
                    if (control is Button btn)
                    {
                        if (btn.Name == "btnThangNay")
                        {
                            btn.MinimumSize = new Size(80, 25);
                            btn.Size = new Size(80, 25);
                        }
                        else
                        {
                            btn.MinimumSize = new Size(75, 25);
                            btn.Size = new Size(75, 25);
                        }
                        btn.Margin = new Padding(3, 2, 5, 3);
                    }
                    else if (control is DateTimePicker dtp)
                    {
                        dtp.MinimumSize = new Size(120, 23);
                        dtp.Size = new Size(120, 23);
                        dtp.Margin = new Padding(3, 3, 10, 3);
                    }
                    else if (control is Label lbl)
                    {
                        lbl.Margin = new Padding(3, 8, 3, 0);
                    }
                }
            }
        }
    }
}
