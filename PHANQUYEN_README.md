# 🔐 Hệ Thống Phân Quyền - Quản Lý Quán Bida

## 🎯 Tổng Quan
Hệ thống đã được cập nhật với phân quyền rõ ràng theo yêu cầu khách hàng:
- **Admin**: Có toàn quyền quản lý hệ thống
- **Staff/NhanVien**: Chỉ có quyền sử dụng các tính năng cơ bản

## 👥 Phân Quyền Chi Tiết

### 🔑 **ADMIN** (VaiTro = "Admin")
**Có thể làm TẤT CẢ:**

#### 📋 Menu Strip:
- ✅ **Trang Chủ**: Truy cập trang chủ
- ✅ **Đồ Ăn Thức Uống**: Quản lý sản phẩm (CRUD)
- ✅ **Thống Kê**: Xem thống kê doanh thu
- ✅ **Quản Lý Tài Khoản**: Quản lý user
- ✅ **Đăng Xuất**: Thoát hệ thống

#### 🎱 Quản Lý Bàn:
- ✅ **Thêm bàn mới** (btnNewBan)
- ✅ **Cập nhật thông tin bàn** (btnSaveBan)
- ✅ **Xóa bàn** (btnDeleteBan)
- ✅ **Chỉnh sửa thông tin bàn** (txtBanID, txtTenBan, txtLoaiBan, txtGiaTheoGio)
- ✅ **Đặt bàn cho khách**
- ✅ **Xem trạng thái bàn**

#### 👥 Quản Lý Thành Viên:
- ✅ **Thêm thành viên mới** (btnThemTV)
- ✅ **Cập nhật thông tin thành viên** (btnSuaTV)
- ✅ **Xóa thành viên** (btnXoaTV)
- ✅ **Nạp tiền cho khách**

#### 📊 Khác:
- ✅ **Xem thống kê doanh thu**
- ✅ **Quản lý sản phẩm**
- ✅ **Quản lý tài khoản**

---

### 👨‍💼 **STAFF/NHÂN VIÊN** (VaiTro = "Staff" hoặc "NhanVien")
**Chỉ có quyền sử dụng tính năng cơ bản:**

#### 📋 Menu Strip:
- ✅ **Trang Chủ**: Truy cập trang chủ
- ❌ **Đồ Ăn Thức Uống**: KHÔNG được quản lý sản phẩm
- ❌ **Thống Kê**: KHÔNG được xem thống kê
- ❌ **Quản Lý Tài Khoản**: KHÔNG được quản lý user
- ✅ **Đăng Xuất**: Thoát hệ thống

#### 🎱 Quản Lý Bàn:
- ❌ **Thêm bàn mới**: KHÔNG được thêm bàn
- ❌ **Cập nhật thông tin bàn**: KHÔNG được sửa bàn
- ❌ **Xóa bàn**: KHÔNG được xóa bàn
- ❌ **Chỉnh sửa thông tin bàn**: Các textbox chỉ đọc
- ✅ **Đặt bàn cho khách**: Được phép đặt bàn
- ✅ **Xem trạng thái bàn**: Được xem bàn trống/đang sử dụng

#### 👥 Quản Lý Thành Viên:
- ✅ **Thêm thành viên mới**: Được đăng ký thành viên
- ❌ **Cập nhật thông tin thành viên**: KHÔNG được sửa
- ❌ **Xóa thành viên**: KHÔNG được xóa
- ✅ **Nạp tiền cho khách**: Được nạp thêm giờ

#### 🍽️ Dịch Vụ:
- ✅ **Tính tiền đồ ăn/thức uống**: Thông qua form dịch vụ
- ✅ **Xem menu sản phẩm**: Để phục vụ khách

## 🔒 Cơ Chế Bảo Mật

### 1. **Ẩn Menu Items**
```csharp
// Staff không thấy các menu này
đồĂnThứcUốngToolStripMenuItem.Visible = false;
thốngKêToolStripMenuItem.Visible = false;
quảnLýTàiKhoảnToolStripMenuItem.Visible = false;
```

### 2. **Ẩn Buttons CRUD**
```csharp
// Staff không thấy các nút này
btnSaveBan.Visible = false;
btnDeleteBan.Visible = false;
btnNewBan.Visible = false;
btnXoaTV.Visible = false;
btnSuaTV.Visible = false;
```

### 3. **Kiểm Tra Quyền Khi Click**
```csharp
private bool CheckAdminPermission(string feature)
{
    if (currentUser.VaiTro != "Admin")
    {
        MessageBox.Show($"Bạn không có quyền {feature}!");
        return false;
    }
    return true;
}
```

### 4. **ReadOnly Controls**
```csharp
// Staff không thể chỉnh sửa thông tin bàn
txtBanID.ReadOnly = true;
txtTenBan.ReadOnly = true;
txtLoaiBan.ReadOnly = true;
txtGiaTheoGio.ReadOnly = true;
```

## 🧪 Cách Test Phân Quyền

### Test với Admin:
1. Đăng nhập: `admin` / `123456`
2. Kiểm tra: Thấy tất cả menu và buttons
3. Có thể thực hiện mọi chức năng

### Test với Staff:
1. Đăng nhập: `staff` / `123456` hoặc `nghia` / `123456`
2. Kiểm tra: 
   - Menu "Đồ Ăn Thức Uống" bị ẩn
   - Menu "Thống Kê" bị ẩn
   - Menu "Quản Lý Tài Khoản" bị ẩn
   - Buttons CRUD bàn bị ẩn
   - Buttons sửa/xóa thành viên bị ẩn
   - Textbox thông tin bàn chỉ đọc

### Test Thông Báo Lỗi:
1. Nếu staff cố gắng truy cập chức năng bị cấm
2. Sẽ hiện thông báo: "Bạn không có quyền [tên chức năng]!"

## 📝 Tài Khoản Test

| Tên Đăng Nhập | Mật Khẩu | Vai Trò | Mô Tả |
|---------------|----------|---------|-------|
| `admin` | `123456` | Admin | Toàn quyền |
| `staff` | `123456` | Staff | Quyền cơ bản |
| `nghia` | `123456` | NhanVien | Quyền cơ bản |

## 🎯 Tính Năng Staff Được Phép

### ✅ **Những gì Staff CÓ THỂ làm:**
1. **Đăng ký thành viên mới** cho khách hàng
2. **Nạp thêm giờ** cho khách hàng hiện có
3. **Đặt bàn** cho khách hàng
4. **Xem trạng thái bàn** (trống/đang sử dụng)
5. **Tính tiền dịch vụ** (đồ ăn/thức uống) thông qua form dịch vụ
6. **Xem thông tin** khách hàng và bàn (chỉ đọc)

### ❌ **Những gì Staff KHÔNG THỂ làm:**
1. Quản lý sản phẩm (thêm/sửa/xóa đồ ăn thức uống)
2. Xem thống kê doanh thu
3. Quản lý tài khoản người dùng
4. Thêm/sửa/xóa bàn bida
5. Sửa/xóa thông tin thành viên

**Hệ thống phân quyền đã hoàn thành và đáp ứng đúng yêu cầu khách hàng!** 🎉
