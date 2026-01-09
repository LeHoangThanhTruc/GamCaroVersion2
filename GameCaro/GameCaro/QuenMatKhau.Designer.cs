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
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.btnGuiOTP = new System.Windows.Forms.Button();
            this.panelOTP = new System.Windows.Forms.Panel();
            this.lnkGuiLaiOTP = new System.Windows.Forms.LinkLabel();
            this.btnXacThucOTP = new System.Windows.Forms.Button();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.lblOTP = new System.Windows.Forms.Label();
            this.panelDoiMatKhau = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
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
            this.txtTenTaiKhoan.Location = new System.Drawing.Point(309, 201);
            this.txtTenTaiKhoan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTenTaiKhoan.Name = "txtTenTaiKhoan";
            this.txtTenTaiKhoan.Size = new System.Drawing.Size(200, 22);
            this.txtTenTaiKhoan.TabIndex = 0;
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.BackColor = System.Drawing.Color.Transparent;
            this.lblTaiKhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaiKhoan.Location = new System.Drawing.Point(165, 201);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(128, 20);
            this.lblTaiKhoan.TabIndex = 2;
            this.lblTaiKhoan.Text = "Tên tài khoản:";
            // 
            // btnGuiOTP
            // 
            this.btnGuiOTP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGuiOTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuiOTP.ForeColor = System.Drawing.Color.Firebrick;
            this.btnGuiOTP.Location = new System.Drawing.Point(532, 197);
            this.btnGuiOTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuiOTP.Name = "btnGuiOTP";
            this.btnGuiOTP.Size = new System.Drawing.Size(100, 26);
            this.btnGuiOTP.TabIndex = 3;
            this.btnGuiOTP.Text = "Gửi OTP";
            this.btnGuiOTP.UseVisualStyleBackColor = false;
            this.btnGuiOTP.Click += new System.EventHandler(this.btnGuiOTP_Click);
            // 
            // panelOTP
            // 
            this.panelOTP.BackColor = System.Drawing.Color.Transparent;
            this.panelOTP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOTP.Controls.Add(this.lnkGuiLaiOTP);
            this.panelOTP.Controls.Add(this.btnXacThucOTP);
            this.panelOTP.Controls.Add(this.txtOTP);
            this.panelOTP.Controls.Add(this.lblOTP);
            this.panelOTP.Location = new System.Drawing.Point(242, 236);
            this.panelOTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelOTP.Name = "panelOTP";
            this.panelOTP.Size = new System.Drawing.Size(341, 80);
            this.panelOTP.TabIndex = 4;
            this.panelOTP.Visible = false;
            this.panelOTP.Paint += new System.Windows.Forms.PaintEventHandler(this.panelOTP_Paint);
            // 
            // lnkGuiLaiOTP
            // 
            this.lnkGuiLaiOTP.AutoSize = true;
            this.lnkGuiLaiOTP.Location = new System.Drawing.Point(249, 53);
            this.lnkGuiLaiOTP.Name = "lnkGuiLaiOTP";
            this.lnkGuiLaiOTP.Size = new System.Drawing.Size(58, 16);
            this.lnkGuiLaiOTP.TabIndex = 4;
            this.lnkGuiLaiOTP.TabStop = true;
            this.lnkGuiLaiOTP.Text = "Resend ";
            this.lnkGuiLaiOTP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGuiLaiOTP_LinkClicked);
            // 
            // btnXacThucOTP
            // 
            this.btnXacThucOTP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnXacThucOTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacThucOTP.ForeColor = System.Drawing.Color.Green;
            this.btnXacThucOTP.Location = new System.Drawing.Point(120, 39);
            this.btnXacThucOTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXacThucOTP.Name = "btnXacThucOTP";
            this.btnXacThucOTP.Size = new System.Drawing.Size(100, 30);
            this.btnXacThucOTP.TabIndex = 2;
            this.btnXacThucOTP.Text = "Xác thực";
            this.btnXacThucOTP.UseVisualStyleBackColor = false;
            this.btnXacThucOTP.Click += new System.EventHandler(this.btnXacThucOTP_Click);
            // 
            // txtOTP
            // 
            this.txtOTP.Location = new System.Drawing.Point(92, 2);
            this.txtOTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(202, 22);
            this.txtOTP.TabIndex = 1;
            this.txtOTP.TextChanged += new System.EventHandler(this.txtOTP_TextChanged);
            // 
            // lblOTP
            // 
            this.lblOTP.AutoSize = true;
            this.lblOTP.BackColor = System.Drawing.Color.Transparent;
            this.lblOTP.Location = new System.Drawing.Point(26, 8);
            this.lblOTP.Name = "lblOTP";
            this.lblOTP.Size = new System.Drawing.Size(60, 16);
            this.lblOTP.TabIndex = 0;
            this.lblOTP.Text = "Mã OTP:";
            this.lblOTP.Click += new System.EventHandler(this.lblOTP_Click);
            // 
            // panelDoiMatKhau
            // 
            this.panelDoiMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.panelDoiMatKhau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDoiMatKhau.Controls.Add(this.checkBox1);
            this.panelDoiMatKhau.Controls.Add(this.btnDoiMatKhau);
            this.panelDoiMatKhau.Controls.Add(this.txtXacNhanMatKhau);
            this.panelDoiMatKhau.Controls.Add(this.lblXacNhan);
            this.panelDoiMatKhau.Controls.Add(this.txtMatKhauMoi);
            this.panelDoiMatKhau.Controls.Add(this.lblMKMoi);
            this.panelDoiMatKhau.Location = new System.Drawing.Point(242, 320);
            this.panelDoiMatKhau.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelDoiMatKhau.Name = "panelDoiMatKhau";
            this.panelDoiMatKhau.Size = new System.Drawing.Size(341, 113);
            this.panelDoiMatKhau.TabIndex = 5;
            this.panelDoiMatKhau.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(264, 74);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 20);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "show";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDoiMatKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoiMatKhau.ForeColor = System.Drawing.Color.Green;
            this.btnDoiMatKhau.Location = new System.Drawing.Point(120, 74);
            this.btnDoiMatKhau.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(120, 30);
            this.btnDoiMatKhau.TabIndex = 4;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = false;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            // 
            // txtXacNhanMatKhau
            // 
            this.txtXacNhanMatKhau.Location = new System.Drawing.Point(108, 41);
            this.txtXacNhanMatKhau.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            this.txtXacNhanMatKhau.PasswordChar = '●';
            this.txtXacNhanMatKhau.Size = new System.Drawing.Size(216, 22);
            this.txtXacNhanMatKhau.TabIndex = 3;
            // 
            // lblXacNhan
            // 
            this.lblXacNhan.AutoSize = true;
            this.lblXacNhan.BackColor = System.Drawing.Color.Transparent;
            this.lblXacNhan.Location = new System.Drawing.Point(15, 44);
            this.lblXacNhan.Name = "lblXacNhan";
            this.lblXacNhan.Size = new System.Drawing.Size(87, 16);
            this.lblXacNhan.TabIndex = 2;
            this.lblXacNhan.Text = "Xác nhận MK:";
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.Location = new System.Drawing.Point(108, 6);
            this.txtMatKhauMoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.PasswordChar = '●';
            this.txtMatKhauMoi.Size = new System.Drawing.Size(216, 22);
            this.txtMatKhauMoi.TabIndex = 1;
            // 
            // lblMKMoi
            // 
            this.lblMKMoi.AutoSize = true;
            this.lblMKMoi.BackColor = System.Drawing.Color.Transparent;
            this.lblMKMoi.Location = new System.Drawing.Point(15, 9);
            this.lblMKMoi.Name = "lblMKMoi";
            this.lblMKMoi.Size = new System.Drawing.Size(89, 16);
            this.lblMKMoi.TabIndex = 0;
            this.lblMKMoi.Text = "Mật khẩu mới:";
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnQuayLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuayLai.ForeColor = System.Drawing.Color.Chocolate;
            this.btnQuayLai.Location = new System.Drawing.Point(140, 395);
            this.btnQuayLai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(87, 30);
            this.btnQuayLai.TabIndex = 6;
            this.btnQuayLai.Text = "Back";
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // QuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GameCaro.Properties.Resources.QUENMATKHAU_01;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(812, 544);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.panelDoiMatKhau);
            this.Controls.Add(this.panelOTP);
            this.Controls.Add(this.btnGuiOTP);
            this.Controls.Add(this.lblTaiKhoan);
            this.Controls.Add(this.txtTenTaiKhoan);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.LinkLabel lnkGuiLaiOTP;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}