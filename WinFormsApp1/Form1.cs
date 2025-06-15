using System.Drawing.Drawing2D;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Đăng nhập hệ thống";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Thiết lập mật khẩu ẩn
            matkhau.PasswordChar = '*';

            // Gán sự kiện cho nút đăng nhập
            button1.Click += Button1_Click;

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
            // Placeholder cho username
            if (string.IsNullOrEmpty(taikhoan.Text))
            {
                taikhoan.ForeColor = Color.Gray;
                taikhoan.Text = "Nhập tên đăng nhập...";
            }

            taikhoan.Enter += (sender, e) =>
            {
                if (taikhoan.Text == "Nhập tên đăng nhập...")
                {
                    taikhoan.Text = "";
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

            // Placeholder cho password
            matkhau.Enter += (sender, e) =>
            {
                if (matkhau.Text == "")
                {
                    matkhau.ForeColor = Color.Black;
                }
            };
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(sender ?? this, e);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string username = taikhoan.Text.Trim();
            string password = matkhau.Text.Trim();

            // Kiểm tra placeholder text
            if (username == "Nhập tên đăng nhập..." || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Thêm logic xử lý đăng nhập ở đây
            MessageBox.Show("Chức năng đăng nhập chưa được triển khai!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void LoadRememberedCredentials()
        {
            // TODO: Thêm logic load thông tin đăng nhập đã lưu ở đây
            taikhoan.Focus();
        }

        private void SaveCredentialsIfRemembered(string username)
        {
            // TODO: Thêm logic lưu thông tin đăng nhập ở đây
        }

        private void LinkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Test",
                           "Quên mật khẩu",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
        }
    }
}
