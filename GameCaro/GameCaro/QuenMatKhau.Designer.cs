namespace GameCaro
{
    partial class QuenMatKhau
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
            this.txtTenTaiKhoan = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.btnGuiOTP = new System.Windows.Forms.Button();
            this.panelOTP = new System.Windows.Forms.Panel();
            this.btnXacThucOTP = new System.Windows.Forms.Button();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.lblOTP = new System.Windows.Forms.Label();
            this.panelDoiMatKhau = new System.Windows.Forms.Panel();
            this.btnDoiMatKhau = new System.Windows.Forms.Button();
            this.txtXacNhanMatKhau = new System.Windows.Forms.TextBox();
            this.lblXacNhan = new System.Windows.Forms.Label();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.lblMKMoi = new System.Windows.Forms.Label();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.panelOTP.SuspendLayout();
            this.panelDoiMatKhau.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTenTaiKhoan
            // 
            this.txtTenTaiKhoan.Location = new System.Drawing.Point(112, 54);
            this.txtTenTaiKhoan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTenTaiKhoan.Name = "txtTenTaiKhoan";
            this.txtTenTaiKhoan.Size = new System.Drawing.Size(151, 20);
            this.txtTenTaiKhoan.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(75, 16);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(177, 26);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Quên Mật Khẩu";
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaiKhoan.Location = new System.Drawing.Point(9, 54);
            this.lblTaiKhoan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(99, 17);
            this.lblTaiKhoan.TabIndex = 2;
            this.lblTaiKhoan.Text = "Tên tài khoản:";
            // 
            // btnGuiOTP
            // 
            this.btnGuiOTP.Location = new System.Drawing.Point(112, 81);
            this.btnGuiOTP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGuiOTP.Name = "btnGuiOTP";
            this.btnGuiOTP.Size = new System.Drawing.Size(75, 24);
            this.btnGuiOTP.TabIndex = 3;
            this.btnGuiOTP.Text = "Gửi OTP";
            this.btnGuiOTP.UseVisualStyleBackColor = true;
            this.btnGuiOTP.Click += new System.EventHandler(this.btnGuiOTP_Click);
            // 
            // panelOTP
            // 
            this.panelOTP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOTP.Controls.Add(this.btnXacThucOTP);
            this.panelOTP.Controls.Add(this.txtOTP);
            this.panelOTP.Controls.Add(this.lblOTP);
            this.panelOTP.Location = new System.Drawing.Point(22, 114);
            this.panelOTP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelOTP.Name = "panelOTP";
            this.panelOTP.Size = new System.Drawing.Size(256, 65);
            this.panelOTP.TabIndex = 4;
            this.panelOTP.Visible = false;
            // 
            // btnXacThucOTP
            // 
            this.btnXacThucOTP.Location = new System.Drawing.Point(90, 37);
            this.btnXacThucOTP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXacThucOTP.Name = "btnXacThucOTP";
            this.btnXacThucOTP.Size = new System.Drawing.Size(75, 24);
            this.btnXacThucOTP.TabIndex = 2;
            this.btnXacThucOTP.Text = "Xác thực";
            this.btnXacThucOTP.UseVisualStyleBackColor = true;
            this.btnXacThucOTP.Click += new System.EventHandler(this.btnXacThucOTP_Click);
            // 
            // txtOTP
            // 
            this.txtOTP.Location = new System.Drawing.Point(90, 10);
            this.txtOTP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(114, 20);
            this.txtOTP.TabIndex = 1;
            // 
            // lblOTP
            // 
            this.lblOTP.AutoSize = true;
            this.lblOTP.Location = new System.Drawing.Point(8, 12);
            this.lblOTP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOTP.Name = "lblOTP";
            this.lblOTP.Size = new System.Drawing.Size(50, 13);
            this.lblOTP.TabIndex = 0;
            this.lblOTP.Text = "Mã OTP:";
            // 
            // panelDoiMatKhau
            // 
            this.panelDoiMatKhau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDoiMatKhau.Controls.Add(this.btnDoiMatKhau);
            this.panelDoiMatKhau.Controls.Add(this.txtXacNhanMatKhau);
            this.panelDoiMatKhau.Controls.Add(this.lblXacNhan);
            this.panelDoiMatKhau.Controls.Add(this.txtMatKhauMoi);
            this.panelDoiMatKhau.Controls.Add(this.lblMKMoi);
            this.panelDoiMatKhau.Location = new System.Drawing.Point(22, 187);
            this.panelDoiMatKhau.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelDoiMatKhau.Name = "panelDoiMatKhau";
            this.panelDoiMatKhau.Size = new System.Drawing.Size(256, 98);
            this.panelDoiMatKhau.TabIndex = 5;
            this.panelDoiMatKhau.Visible = false;
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.Location = new System.Drawing.Point(90, 65);
            this.btnDoiMatKhau.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(90, 24);
            this.btnDoiMatKhau.TabIndex = 4;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = true;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            // 
            // txtXacNhanMatKhau
            // 
            this.txtXacNhanMatKhau.Location = new System.Drawing.Point(90, 38);
            this.txtXacNhanMatKhau.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            this.txtXacNhanMatKhau.PasswordChar = '●';
            this.txtXacNhanMatKhau.Size = new System.Drawing.Size(151, 20);
            this.txtXacNhanMatKhau.TabIndex = 3;
            // 
            // lblXacNhan
            // 
            this.lblXacNhan.AutoSize = true;
            this.lblXacNhan.Location = new System.Drawing.Point(8, 41);
            this.lblXacNhan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblXacNhan.Name = "lblXacNhan";
            this.lblXacNhan.Size = new System.Drawing.Size(75, 13);
            this.lblXacNhan.TabIndex = 2;
            this.lblXacNhan.Text = "Xác nhận MK:";
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.Location = new System.Drawing.Point(90, 10);
            this.txtMatKhauMoi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.PasswordChar = '●';
            this.txtMatKhauMoi.Size = new System.Drawing.Size(151, 20);
            this.txtMatKhauMoi.TabIndex = 1;
            // 
            // lblMKMoi
            // 
            this.lblMKMoi.AutoSize = true;
            this.lblMKMoi.Location = new System.Drawing.Point(8, 12);
            this.lblMKMoi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMKMoi.Name = "lblMKMoi";
            this.lblMKMoi.Size = new System.Drawing.Size(74, 13);
            this.lblMKMoi.TabIndex = 0;
            this.lblMKMoi.Text = "Mật khẩu mới:";
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Location = new System.Drawing.Point(112, 301);
            this.btnQuayLai.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(75, 24);
            this.btnQuayLai.TabIndex = 6;
            this.btnQuayLai.Text = "Back";
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // QuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 327);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.panelDoiMatKhau);
            this.Controls.Add(this.panelOTP);
            this.Controls.Add(this.btnGuiOTP);
            this.Controls.Add(this.lblTaiKhoan);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtTenTaiKhoan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuenMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quên Mật Khẩu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuenMatKhau_FormClosing);
            this.Load += new System.EventHandler(this.QuenMatKhau_Load);
            this.panelOTP.ResumeLayout(false);
            this.panelOTP.PerformLayout();
            this.panelDoiMatKhau.ResumeLayout(false);
            this.panelDoiMatKhau.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTenTaiKhoan;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.Button btnGuiOTP;
        private System.Windows.Forms.Panel panelOTP;
        private System.Windows.Forms.Button btnXacThucOTP;
        private System.Windows.Forms.TextBox txtOTP;
        private System.Windows.Forms.Label lblOTP;
        private System.Windows.Forms.Panel panelDoiMatKhau;
        private System.Windows.Forms.Label lblMKMoi;
        private System.Windows.Forms.Button btnDoiMatKhau;
        private System.Windows.Forms.TextBox txtXacNhanMatKhau;
        private System.Windows.Forms.Label lblXacNhan;
        private System.Windows.Forms.TextBox txtMatKhauMoi;
        private System.Windows.Forms.Button btnQuayLai;
    }
}