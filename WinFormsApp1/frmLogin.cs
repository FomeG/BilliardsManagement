using Microsoft.EntityFrameworkCore;
using Models.HandleData;

namespace WinFormsApp1
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            SetupForm();
            NhoDangNhap();
        }

        #region Code giao diện

        private void SetupForm()
        {
            this.Text = "Đăng nhập hệ thống";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Thiết lập mật khẩu ẩn
            matkhau.PasswordChar = '*';

            // Cho phép nhấn Enter để đăng nhập
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            // Thiết lập hiệu ứng cho form
            SetupUIEffects();

            // Force set màu trắng cho panel login và disable visual styles
            Application.EnableVisualStyles();
            panelLogin.BackColor = Color.White;
            panelUsername.BackColor = Color.White;
            panelPassword.BackColor = Color.White;
        }

        private void SetupUIEffects()
        {
            // Thiết lập rounded corners cho panel login
            SetupRoundedCorners();

            // Thiết lập custom paint cho panel login
            panelLogin.Paint += PanelLogin_Paint;

            // Thiết lập border cho textbox
            SetupTextBoxBorders();

            // Thiết lập hover effect cho button
            SetupButtonHoverEffect();

            // Thiết lập placeholder text
            SetupPlaceholderText();

            // Force refresh
            panelLogin.Invalidate();
        }

        private void SetupRoundedCorners()
        {
            // Tạo rounded corners cho panel login
            panelLogin.Region = CreateRoundedRegion(panelLogin.Size, 15);

            // Tạo rounded corners cho button
            button1.Region = CreateRoundedRegion(button1.Size, 8);
        }

        private Region CreateRoundedRegion(Size size, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(size.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(size.Width - radius, size.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, size.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return new Region(path);
        }

        private void PanelLogin_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is Panel panel)
            {
                // Vẽ background trắng trước
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(whiteBrush, 0, 0, panel.Width, panel.Height);
                }

                // Vẽ shadow cho panel login (optional)
                // Rectangle shadowRect = new Rectangle(5, 5, panel.Width, panel.Height);
                // using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                // {
                //     e.Graphics.FillRectangle(shadowBrush, shadowRect);
                // }
            }
        }

        private void SetupTextBoxBorders()
        {
            // Thiết lập border cho username panel
            panelUsername.Paint += (sender, e) =>
            {
                if (sender is Panel panel)
                {
                    using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 2))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 25, panel.Width - 1, 30);
                    }
                }
            };

            // Thiết lập border cho password panel
            panelPassword.Paint += (sender, e) =>
            {
                if (sender is Panel panel)
                {
                    using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 2))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 25, panel.Width - 1, 30);
                    }
                }
            };

            // Focus events cho textboxes
            taikhoan.Enter += (sender, e) => panelUsername.Invalidate();
            taikhoan.Leave += (sender, e) => panelUsername.Invalidate();
            matkhau.Enter += (sender, e) => panelPassword.Invalidate();
            matkhau.Leave += (sender, e) => panelPassword.Invalidate();
        }

        private void SetupButtonHoverEffect()
        {
            Color originalColor = Color.FromArgb(41, 128, 185);
            Color hoverColor = Color.FromArgb(52, 152, 219);

            button1.MouseEnter += (sender, e) =>
            {
                button1.BackColor = hoverColor;
                button1.Cursor = Cursors.Hand;
            };

            button1.MouseLeave += (sender, e) =>
            {
                button1.BackColor = originalColor;
                button1.Cursor = Cursors.Default;
            };
        }

        private void SetupPlaceholderText()
        {
            if (string.IsNullOrEmpty(taikhoan.Text))
            {
                taikhoan.ForeColor = Color.Gray;
                taikhoan.Text = "Nhập tên đăng nhập...";
            }

            taikhoan.Enter += (sender, e) =>
            {
                if (taikhoan.Text == "Nhập tên đăng nhập...")
                {
                    taikhoan.Text = string.Empty;
                    taikhoan.ForeColor = Color.Black;
                }
            };

            taikhoan.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(taikhoan.Text))
                {
                    taikhoan.Text = "Nhập tên đăng nhập...";
                    taikhoan.ForeColor = Color.Gray;
                }
            };

            matkhau.Enter += (sender, e) =>
            {
                if (matkhau.Text == string.Empty)
                {
                    matkhau.ForeColor = Color.Black;
                }
            };
        }

        #endregion

        #region Event Handlers

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click_1(sender ?? this, e);
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            string username = taikhoan.Text.Trim();
            string password = matkhau.Text.Trim();

            if (username == "Nhập tên đăng nhập..." || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị loading
            button1.Enabled = false;
            button1.Text = "Đang đăng nhập...";

            try
            {
                using (var context = new DaDBContext())
                {
                    // Tìm tài khoản trong database
                    var user = await context.TaiKhoans
                        .FirstOrDefaultAsync(t => t.TenDangNhap == username && t.MatKhau == password && t.TrangThai == true);

                    if (user != null)
                    {
                        // Lưu thông tin đăng nhập nếu được chọn Remember Me
                        SaveCredentialsIfRemembered(username);

                        // Đăng nhập thành công - lưu vào session
                        UserSession.Login(user);
                        MessageBox.Show($"Đăng nhập thành công! Xin chào {user.HoTen}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Mở form chính và ẩn form đăng nhập
                        frmTrangChu mainForm = new frmTrangChu(user);

                        // Đăng ký sự kiện khi MainForm đóng để hiển thị lại Form1
                        mainForm.FormClosed += (s, args) =>
                        {
                            this.Show();
                            this.WindowState = FormWindowState.Normal;
                            this.BringToFront();
                            // Reset form đăng nhập
                            matkhau.Clear();
                            // Chỉ xóa username nếu không ghi nhớ
                            if (!IsRememberMeChecked())
                            {
                                ClearUsernameField();
                            }
                            taikhoan.Focus();
                        };

                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Đăng nhập thất bại
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        matkhau.Clear();
                        taikhoan.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Khôi phục trạng thái nút
                button1.Enabled = true;
                button1.Text = "Đăng nhập";
            }
        }

        private void LinkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên để được hỗ trợ đặt lại mật khẩu.",
                           "Quên mật khẩu",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
        }

        #endregion

        #region Chức năng nhớ tài khoản đăng nhập

        private void NhoDangNhap()
        {
            try
            {
                // Đọc thông tin đã lưu từ Settings
                string savedUsername = Properties.Settings.Default.RememberedUsername;
                bool rememberMe = Properties.Settings.Default.RememberMe;

                if (rememberMe && !string.IsNullOrEmpty(savedUsername))
                {
                    // Đặt username đã lưu
                    taikhoan.Text = savedUsername;
                    taikhoan.ForeColor = Color.Black;

                    // Đặt checkbox Remember Me (nếu có)
                    SetRememberMeChecked(true);

                    // Focus vào password field
                    matkhau.Focus();
                }
                else
                {
                    // Focus vào username field
                    taikhoan.Focus();
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, chỉ focus vào username
                taikhoan.Focus();
            }
        }

        private void SaveCredentialsIfRemembered(string username)
        {
            try
            {
                if (IsRememberMeChecked())
                {
                    // Lưu thông tin đăng nhập
                    Properties.Settings.Default.RememberedUsername = username;
                    Properties.Settings.Default.RememberMe = true;
                }
                else
                {
                    // Xóa thông tin đã lưu
                    Properties.Settings.Default.RememberedUsername = string.Empty;
                    Properties.Settings.Default.RememberMe = false;
                }

                // Lưu settings
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                // Nếu có lỗi khi lưu, không làm gì cả
            }
        }

        private bool IsRememberMeChecked()
        {
            return chkRememberMe.Checked;
        }

        private void SetRememberMeChecked(bool isChecked)
        {
            chkRememberMe.Checked = isChecked;
        }

        private void ClearUsernameField()
        {
            taikhoan.Text = "Nhập tên đăng nhập...";
            taikhoan.ForeColor = Color.Gray;
        }

        #endregion
    }
}
