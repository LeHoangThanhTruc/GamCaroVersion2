namespace GameCaro
{
    partial class DangNhap
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
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenTaiKhoan = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.lnkQuenMatKhau = new System.Windows.Forms.LinkLabel();
            this.lnkDangKy = new System.Windows.Forms.LinkLabel();
            this.ckHienMatKhau = new System.Windows.Forms.CheckBox();
            this.ckNhoMatKhau = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BackColor = System.Drawing.Color.Khaki;
            this.btnDangNhap.Font = new System.Drawing.Font("UTM Cookies", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.ForeColor = System.Drawing.Color.Violet;
            this.btnDangNhap.Location = new System.Drawing.Point(371, 352);
            this.btnDangNhap.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(211, 54);
            this.btnDangNhap.TabIndex = 0;
            this.btnDangNhap.Text = "ĐĂNG NHẬP";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(204, 223);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên tài khoản";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(239, 282);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mật khẩu";
            // 
            // txtTenTaiKhoan
            // 
            this.txtTenTaiKhoan.Location = new System.Drawing.Point(324, 217);
            this.txtTenTaiKhoan.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtTenTaiKhoan.Name = "txtTenTaiKhoan";
            this.txtTenTaiKhoan.Size = new System.Drawing.Size(301, 24);
            this.txtTenTaiKhoan.TabIndex = 3;
            this.txtTenTaiKhoan.TextChanged += new System.EventHandler(this.txtTenTaiKhoan_TextChanged);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(324, 276);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '●';
            this.txtMatKhau.Size = new System.Drawing.Size(301, 24);
            this.txtMatKhau.TabIndex = 4;
            // 
            // lnkQuenMatKhau
            // 
            this.lnkQuenMatKhau.AutoSize = true;
            this.lnkQuenMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.lnkQuenMatKhau.Location = new System.Drawing.Point(268, 316);
            this.lnkQuenMatKhau.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lnkQuenMatKhau.Name = "lnkQuenMatKhau";
            this.lnkQuenMatKhau.Size = new System.Drawing.Size(122, 18);
            this.lnkQuenMatKhau.TabIndex = 6;
            this.lnkQuenMatKhau.TabStop = true;
            this.lnkQuenMatKhau.Text = "Quên mật khẩu";
            this.lnkQuenMatKhau.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkQuenMatKhau_LinkClicked);
            // 
            // lnkDangKy
            // 
            this.lnkDangKy.AutoSize = true;
            this.lnkDangKy.BackColor = System.Drawing.Color.Transparent;
            this.lnkDangKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDangKy.Location = new System.Drawing.Point(336, 545);
            this.lnkDangKy.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lnkDangKy.Name = "lnkDangKy";
            this.lnkDangKy.Size = new System.Drawing.Size(263, 20);
            this.lnkDangKy.TabIndex = 7;
            this.lnkDangKy.TabStop = true;
            this.lnkDangKy.Text = "Chưa có tài khoản ? ĐĂNG KÝ";
            this.lnkDangKy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDangKy_LinkClicked);
            // 
            // ckHienMatKhau
            // 
            this.ckHienMatKhau.AutoSize = true;
            this.ckHienMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.ckHienMatKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckHienMatKhau.Location = new System.Drawing.Point(634, 278);
            this.ckHienMatKhau.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ckHienMatKhau.Name = "ckHienMatKhau";
            this.ckHienMatKhau.Size = new System.Drawing.Size(66, 20);
            this.ckHienMatKhau.TabIndex = 8;
            this.ckHienMatKhau.Text = "Show";
            this.ckHienMatKhau.UseVisualStyleBackColor = false;
            this.ckHienMatKhau.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ckNhoMatKhau
            // 
            this.ckNhoMatKhau.AutoSize = true;
            this.ckNhoMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.ckNhoMatKhau.Checked = true;
            this.ckNhoMatKhau.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckNhoMatKhau.Location = new System.Drawing.Point(161, 364);
            this.ckNhoMatKhau.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ckNhoMatKhau.Name = "ckNhoMatKhau";
            this.ckNhoMatKhau.Size = new System.Drawing.Size(143, 22);
            this.ckNhoMatKhau.TabIndex = 8;
            this.ckNhoMatKhau.Text = "Nhớ đăng nhập";
            this.ckNhoMatKhau.UseVisualStyleBackColor = false;
            // 
            // DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GameCaro.Properties.Resources.DANGNHAP_01;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(917, 613);
            this.Controls.Add(this.ckNhoMatKhau);
            this.Controls.Add(this.ckHienMatKhau);
            this.Controls.Add(this.lnkDangKy);
            this.Controls.Add(this.lnkQuenMatKhau);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.txtTenTaiKhoan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDangNhap);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "DangNhap";
            this.Text = "Game Caro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DangNhap_FormClosing);
            this.Load += new System.EventHandler(this.DangNhap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenTaiKhoan;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.LinkLabel lnkQuenMatKhau;
        private System.Windows.Forms.LinkLabel lnkDangKy;
        private System.Windows.Forms.CheckBox ckHienMatKhau;
        private System.Windows.Forms.CheckBox ckNhoMatKhau;
    }
}