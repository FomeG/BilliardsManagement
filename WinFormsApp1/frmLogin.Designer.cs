namespace WinFormsApp1
{
    partial class frmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            panelMain = new Panel();
            panelLogin = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            panelUsername = new Panel();
            lblUsername = new Label();
            taikhoan = new TextBox();
            panelPassword = new Panel();
            lblPassword = new Label();
            matkhau = new TextBox();
            chkRememberMe = new CheckBox();
            button1 = new Button();
            panelMain.SuspendLayout();
            panelLogin.SuspendLayout();
            panelUsername.SuspendLayout();
            panelPassword.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(41, 128, 185);
            panelMain.Controls.Add(panelLogin);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(395, 477);
            panelMain.TabIndex = 0;
            // 
            // panelLogin
            // 
            panelLogin.Anchor = AnchorStyles.None;
            panelLogin.BackColor = Color.White;
            panelLogin.Controls.Add(lblTitle);
            panelLogin.Controls.Add(lblSubtitle);
            panelLogin.Controls.Add(panelUsername);
            panelLogin.Controls.Add(panelPassword);
            panelLogin.Controls.Add(chkRememberMe);
            panelLogin.Controls.Add(button1);
            panelLogin.Location = new Point(-1, -1);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(400, 500);
            panelLogin.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitle.Location = new Point(0, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐĂNG NHẬP";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 11F);
            lblSubtitle.ForeColor = Color.FromArgb(149, 165, 166);
            lblSubtitle.Location = new Point(0, 90);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(400, 30);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Hệ thống quản lý quán Bida";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelUsername
            // 
            panelUsername.Controls.Add(lblUsername);
            panelUsername.Controls.Add(taikhoan);
            panelUsername.Location = new Point(50, 150);
            panelUsername.Name = "panelUsername";
            panelUsername.Size = new Size(300, 70);
            panelUsername.TabIndex = 2;
            // 
            // lblUsername
            // 
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(52, 73, 94);
            lblUsername.Location = new Point(0, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(300, 25);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "👤 Tên đăng nhập";
            // 
            // taikhoan
            // 
            taikhoan.BorderStyle = BorderStyle.None;
            taikhoan.Font = new Font("Segoe UI", 12F);
            taikhoan.Location = new Point(5, 30);
            taikhoan.Name = "taikhoan";
            taikhoan.Size = new Size(290, 22);
            taikhoan.TabIndex = 0;
            // 
            // panelPassword
            // 
            panelPassword.Controls.Add(lblPassword);
            panelPassword.Controls.Add(matkhau);
            panelPassword.Location = new Point(50, 230);
            panelPassword.Name = "panelPassword";
            panelPassword.Size = new Size(300, 70);
            panelPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(52, 73, 94);
            lblPassword.Location = new Point(0, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(300, 25);
            lblPassword.TabIndex = 0;
            lblPassword.Text = "🔒 Mật khẩu";
            // 
            // matkhau
            // 
            matkhau.BorderStyle = BorderStyle.None;
            matkhau.Font = new Font("Segoe UI", 12F);
            matkhau.Location = new Point(5, 30);
            matkhau.Name = "matkhau";
            matkhau.Size = new Size(290, 22);
            matkhau.TabIndex = 1;
            // 
            // chkRememberMe
            // 
            chkRememberMe.Font = new Font("Segoe UI", 9F);
            chkRememberMe.ForeColor = Color.FromArgb(127, 140, 141);
            chkRememberMe.Location = new Point(50, 320);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(150, 20);
            chkRememberMe.TabIndex = 2;
            chkRememberMe.Text = "Ghi nhớ đăng nhập";
            chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(41, 128, 185);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(50, 360);
            button1.Name = "button1";
            button1.Size = new Size(300, 45);
            button1.TabIndex = 3;
            button1.Text = "ĐĂNG NHẬP";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(41, 128, 185);
            ClientSize = new Size(395, 477);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập hệ thống";
            panelMain.ResumeLayout(false);
            panelLogin.ResumeLayout(false);
            panelUsername.ResumeLayout(false);
            panelUsername.PerformLayout();
            panelPassword.ResumeLayout(false);
            panelPassword.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Panel panelLogin;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelUsername;
        private Label lblUsername;
        private TextBox taikhoan;
        private Panel panelPassword;
        private Label lblPassword;
        private TextBox matkhau;
        private CheckBox chkRememberMe;
        private Button button1;
    }
}
