# 🎱 Dự Án Quản Lý Quán Bida - WinForms Application

Ứng dụng WinForms để quản lý quán bida, bao gồm quản lý bàn chơi, khách hàng, phiên chơi, hóa đơn và các sản phẩm.

## 📋 Yêu Cầu Hệ Thống

- **.NET 8.0 SDK**
- **SQL Server Express** hoặc **LocalDB**
- **Windows OS** (cho WinForms)

## 📁 Cấu Trúc Dự Án

```
├── WinFormsApp1/          # Main Application
├── Models/                # Entity Framework Models
│   ├── Models/           # Entity Classes
│   ├── Configuration/    # Entity Configurations
│   └── HandleData/       # DbContext
├── ClassLibrary1/         # DBHelper (Legacy)
├── setup.bat              # Auto Setup Script (Batch)
├── setup.ps1              # Auto Setup Script (PowerShell)
└── README.md              # Hướng dẫn này
```

## 🔧 Cấu Hình

**Connection String mặc định:**
```
Server=MAY-02\SQLEXPRESS;Database=BilliardsManagement_DB;Trusted_Connection=True;TrustServerCertificate=True
```

Nên sửa lại connection string khi sử dụng

## 🎯 Tính Năng Chính

- **Quản lý Bàn Bida**: Theo dõi trạng thái và loại bàn
- **Quản lý Khách Hàng**: Thông tin khách hàng và nạp giờ chơi
- **Quản lý Sản Phẩm**: Đồ uống, thức ăn và các dịch vụ khác
- **Quản lý Tài Khoản**: Phân quyền nhân viên và quản lý

## 📊 Cơ Sở Dữ Liệu

### Các Model Chính:
- **TaiKhoan**: Quản lý tài khoản nhân viên
- **KhachHang**: Thông tin khách hàng
- **Ban**: Thông tin bàn bida
- **PhienChoi**: Phiên chơi của khách hàng
- **HoaDon**: Hóa đơn thanh toán
- **ChiTietHoaDon**: Chi tiết sản phẩm trong hóa đơn
- **SanPham**: Danh mục sản phẩm
- **NapGio**: Lịch sử nạp giờ của khách hàng

## 🛠️ Công Nghệ Sử Dụng

- **Framework**: .NET 8.0 WinForms
- **ORM**: Entity Framework Core
- **Database**: SQL Server Express
- **Architecture**: Code-First với Fluent API Configuration

## 📞 Hỗ Trợ

Nếu gặp vấn đề trong quá trình setup, hãy kiểm tra:

1. **.NET 8.0 SDK** đã được cài đặt
2. **SQL Server Express** đang chạy
3. **Connection string** trong DaDBContext.cs đúng với cấu hình SQL Server của bạn
4. **Firewall** không chặn kết nối database

---

*Dự án được phát triển với cấu trúc tương tự như Hệ Thống Quản Lý Đề Tài Khoa Học*