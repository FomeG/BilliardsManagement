# 📖 Hướng Dẫn Setup Chi Tiết - Dự Án Quản Lý Quán Bida

## 🎯 Tổng Quan

Dự án **Quản Lý Quán Bida** được xây dựng với **WinForms** và **Entity Framework Core**, sử dụng **SQL Server Express** làm cơ sở dữ liệu.

---

## 🔧 Yêu Cầu Hệ Thống

### Phần Mềm Bắt Buộc:
1. **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **SQL Server Express** - [Download](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
3. **Visual Studio 2022** hoặc **VS Code** (tùy chọn)

### Kiểm Tra Cài Đặt:
```bash
# Kiểm tra .NET SDK
dotnet --version

# Kiểm tra SQL Server
sqlcmd -S MAY-02\SQLEXPRESS -E -Q "SELECT @@VERSION"
```

---

## 🚀 Cách Setup

### Phương Pháp 1: Script Tự Động (Khuyến Nghị)

#### Sử dụng Batch Script:
```bash
./setup.bat
```

#### Sử dụng PowerShell Script:
```powershell
./setup.ps1
```

### Phương Pháp 2: Setup Thủ Công

#### Bước 1: Clone/Download Project
```bash
# Nếu sử dụng Git
git clone <repository-url>
cd BilliardsManagement

# Hoặc download và giải nén
```

#### Bước 2: Restore Dependencies
```bash
dotnet restore WinFormsApp1.sln
```

#### Bước 3: Tạo Database
```bash
# Tạo database mới
sqlcmd -S MAY-02\SQLEXPRESS -E -Q "CREATE DATABASE BilliardsManagement_DB"

# Hoặc kiểm tra và tạo nếu chưa có
sqlcmd -S MAY-02\SQLEXPRESS -E -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BilliardsManagement_DB') CREATE DATABASE BilliardsManagement_DB"
```

#### Bước 4: Chạy Migration
```bash
cd Models
dotnet ef database update --startup-project ../WinFormsApp1
cd ..
```

#### Bước 5: Build Project
```bash
dotnet build WinFormsApp1.sln --configuration Debug
```

#### Bước 6: Chạy Ứng Dụng
```bash
dotnet run --project WinFormsApp1/WinFormsApp1.csproj
```

---

## 🔍 Xử Lý Lỗi Thường Gặp

### 1. Lỗi .NET SDK:
```bash
# Kiểm tra version
dotnet --list-sdks

# Cài đặt .NET 8.0 nếu chưa có
# Download từ: https://dotnet.microsoft.com/download/dotnet/8.0
```

### 2. Lỗi SQL Server:
```bash
# Kiểm tra SQL Server service
net start MSSQL$SQLEXPRESS

# Hoặc sử dụng SQL Server Configuration Manager
```

### 3. Lỗi Connection String:
Chỉnh sửa file `Models/HandleData/DaDBContext.cs`:
```csharp
// Thay đổi connection string phù hợp với cấu hình của bạn
optionsBuilder.UseSqlServer("Server=MAY-02\\SQLEXPRESS;Database=BilliardsManagement_DB;Trusted_Connection=True;TrustServerCertificate=True");
```

### 4. Lỗi Migration:
```bash
# Xóa migration cũ (nếu có)
cd Models
dotnet ef migrations remove

# Tạo migration mới
dotnet ef migrations add InitialCreate

# Cập nhật database
dotnet ef database update --startup-project ../WinFormsApp1
cd ..
```

### 5. Lỗi Build:
```bash
# Clean solution
dotnet clean WinFormsApp1.sln

# Restore lại
dotnet restore WinFormsApp1.sln

# Build lại
dotnet build WinFormsApp1.sln
```

---

## 📦 Cấu Trúc Dự Án

```
BilliardsManagement/
├── WinFormsApp1/              # Main WinForms Application
│   ├── Form1.cs              # Main Form
│   ├── Form2.cs              # Additional Forms
│   └── Program.cs            # Entry Point
├── Models/                    # Entity Framework Models & DbContext
│   ├── Models/               # Entity Classes
│   │   ├── TaiKhoan.cs
│   │   ├── KhachHang.cs
│   │   ├── Ban.cs
│   │   ├── PhienChoi.cs
│   │   ├── HoaDon.cs
│   │   ├── ChiTietHoaDon.cs
│   │   ├── SanPham.cs
│   │   └── NapGio.cs
│   ├── Configuration/        # Entity Configurations
│   │   ├── TaiKhoanConfiguration.cs
│   │   ├── KhachHangConfiguration.cs
│   │   ├── BanConfiguration.cs
│   │   ├── PhienChoiConfiguration.cs
│   │   ├── HoaDonConfiguration.cs
│   │   ├── ChiTietHoaDonConfiguration.cs
│   │   ├── SanPhamConfiguration.cs
│   │   └── NapGioConfiguration.cs
│   └── HandleData/           # DbContext
│       └── DaDBContext.cs
├── ClassLibrary1/            # DBHelper (Legacy - không sử dụng)
├── WinFormsApp1.sln          # Solution File
├── setup.bat                 # Auto Setup Script (Batch)
├── setup.ps1                 # Auto Setup Script (PowerShell)
├── README.md                 # Hướng dẫn tổng quan
└── huongDanSetUp.md          # File hướng dẫn này
```

---

## 🎯 Script Setup Tự Động

### Tạo file `setup.bat`:
```batch
@echo off
echo ========================================
echo    SETUP DU AN WINFORMS - QUAN LY QUAN BIDA
echo ========================================

echo.
echo [1/5] Kiem tra .NET SDK...
dotnet --version

echo.
echo [2/5] Restore dependencies...
dotnet restore WinFormsApp1.sln

echo.
echo [3/5] Tao database...
sqlcmd -S MAY-02\SQLEXPRESS -E -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BilliardsManagement_DB') CREATE DATABASE BilliardsManagement_DB"

echo.
echo [4/5] Chay migration...
cd Models
dotnet ef database update --startup-project ../WinFormsApp1
cd ..

echo.
echo [5/5] Build solution...
dotnet build WinFormsApp1.sln --configuration Debug

echo.
echo ========================================
echo    SETUP HOAN THANH!
echo ========================================
echo.
echo De chay ung dung, su dung lenh:
echo dotnet run --project WinFormsApp1/WinFormsApp1.csproj
echo.
pause
```

### Để sử dụng script:
```bash
# Tạo file setup.bat và chạy
./setup.bat
```

---

## 📞 Hỗ Trợ

Nếu gặp vấn đề trong quá trình setup, hãy kiểm tra:

1. **Phiên bản .NET**: Đảm bảo đã cài .NET 8.0 SDK
2. **SQL Server**: Kiểm tra service đang chạy
3. **Quyền truy cập**: Đảm bảo có quyền tạo database
4. **Firewall**: Không chặn kết nối SQL Server
5. **Connection String**: Phù hợp với cấu hình SQL Server

---

## 🔄 Cập Nhật Database Schema

Khi có thay đổi model:
```bash
cd Models
dotnet ef migrations add <TenMigration>
dotnet ef database update --startup-project ../WinFormsApp1
cd ..
```

---

*Dự án được thiết kế với cấu trúc tương tự như Hệ Thống Quản Lý Đề Tài Khoa Học để đảm bảo tính nhất quán và dễ bảo trì.*
