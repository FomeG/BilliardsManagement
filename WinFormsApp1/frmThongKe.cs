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
    }
}
