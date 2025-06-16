# 📊 Hướng Dẫn Sử Dụng Form Thống Kê (frmThongKe)

## 🎯 Tổng Quan
Form thống kê được tạo để hiển thị doanh thu của quán bida theo yêu cầu khách hàng:
- **Chỉ tính hóa đơn có trạng thái = 1** (đã hoàn thành)
- **Biểu đồ đường** hiển thị doanh thu theo ngày
- **Bộ lọc theo thời gian** với mặc định là tháng hiện tại
- **Thông tin tổng quan** về doanh thu

## 🚀 Cách Truy Cập
1. Đăng nhập vào hệ thống
2. Trên menu strip ở trang chủ, click vào **"Thống Kê"**
3. Form thống kê sẽ mở ra với dữ liệu mặc định của tháng hiện tại

## 📈 Tính Năng Chính

### 1. Bộ Lọc Thời Gian
- **Từ ngày / Đến ngày**: Chọn khoảng thời gian cụ thể
- **Nút Lọc**: Áp dụng bộ lọc đã chọn
- **Nút Hôm nay**: Xem thống kê ngày hôm nay
- **Nút Tháng này**: Xem thống kê tháng hiện tại (mặc định)
- **Nút Năm này**: Xem thống kê cả năm

### 2. Thông Tin Tổng Quan
- **Tổng Doanh Thu**: Tổng tiền của tất cả hóa đơn đã hoàn thành
- **Số Hóa Đơn**: Tổng số hóa đơn đã hoàn thành
- **Doanh Thu Trung Bình**: Doanh thu trung bình mỗi hóa đơn

### 3. Biểu Đồ Đường
- Hiển thị doanh thu theo từng ngày
- Trục X: Ngày (định dạng dd/MM)
- Trục Y: Doanh thu (VNĐ)
- Có điểm đánh dấu cho mỗi ngày có doanh thu

## 🔧 Cấu Hình Kỹ Thuật

### Dependencies Đã Thêm
```xml
<PackageReference Include="System.Windows.Forms.DataVisualization" Version="1.0.0-prerelease.20110.1" />
```

### Cấu Trúc Code
- **frmThongKe.cs**: Logic xử lý và tương tác với database
- **frmThongKe.Designer.cs**: Giao diện form
- **frmTrangChu.cs**: Đã thêm menu navigation cho thống kê

### Database Query
Form sử dụng Entity Framework để truy vấn:
```csharp
var hoaDons = await dbContext.HoaDons
    .Where(h => h.TrangThai == 1 && h.NgayLap >= tuNgay && h.NgayLap <= denNgay)
    .Select(h => new { h.NgayLap, h.TongTien })
    .ToListAsync();
```

## 📊 Dữ Liệu Test
Đã tạo sẵn dữ liệu test với các hóa đơn từ ngày 11/06 đến 16/06/2025:
- Tổng cộng 7 hóa đơn đã hoàn thành
- Tổng doanh thu: 6,510,000 VNĐ
- Doanh thu trung bình: ~930,000 VNĐ/hóa đơn

## 🎨 Giao Diện
- **Panel trên**: Bộ lọc thời gian và các nút chức năng
- **Panel giữa**: Thông tin tổng quan với màu sắc phân biệt
- **Panel dưới**: Biểu đồ đường chiếm toàn bộ không gian còn lại

## 🔐 Phân Quyền
- **Staff**: Có thể xem thống kê cơ bản
- **Admin**: Có thể xem tất cả thống kê và dữ liệu chi tiết

## 🐛 Lưu Ý
1. Form chỉ hiển thị hóa đơn có `TrangThai = 1` (đã hoàn thành)
2. Mặc định hiển thị dữ liệu tháng hiện tại khi mở form
3. Biểu đồ sẽ trống nếu không có dữ liệu trong khoảng thời gian đã chọn
4. Định dạng tiền tệ sử dụng `N0` (không có số thập phân, có dấu phẩy ngăn cách)

## 🔄 Cập Nhật Tương Lai
Có thể mở rộng thêm:
- Thống kê theo sản phẩm
- Thống kê theo nhân viên
- Xuất báo cáo Excel/PDF
- Biểu đồ cột, tròn
- Thống kê theo giờ trong ngày
