namespace GameCaro
{
    partial class GiaoDienChung
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
            this.btnPhongCho = new System.Windows.Forms.Button();
            this.btnCaiDat = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPhongCho
            // 
            this.btnPhongCho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnPhongCho.Font = new System.Drawing.Font("UTM Cookies", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhongCho.ForeColor = System.Drawing.Color.BlueViolet;
            this.btnPhongCho.Location = new System.Drawing.Point(221, 300);
            this.btnPhongCho.Margin = new System.Windows.Forms.Padding(4);
            this.btnPhongCho.Name = "btnPhongCho";
            this.btnPhongCho.Size = new System.Drawing.Size(209, 71);
            this.btnPhongCho.TabIndex = 0;
            this.btnPhongCho.Text = "Phòng chờ";
            this.btnPhongCho.UseVisualStyleBackColor = false;
            this.btnPhongCho.Click += new System.EventHandler(this.btnPhongCho_Click);
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCaiDat.Font = new System.Drawing.Font("UTM Cookies", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaiDat.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnCaiDat.Location = new System.Drawing.Point(221, 192);
            this.btnCaiDat.Margin = new System.Windows.Forms.Padding(4);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Size = new System.Drawing.Size(209, 71);
            this.btnCaiDat.TabIndex = 1;
            this.btnCaiDat.Text = "Cài đặt";
            this.btnCaiDat.UseVisualStyleBackColor = false;
            this.btnCaiDat.Click += new System.EventHandler(this.btnCaiDat_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnProfile.Font = new System.Drawing.Font("UTM Cookies", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnProfile.Location = new System.Drawing.Point(498, 192);
            this.btnProfile.Margin = new System.Windows.Forms.Padding(4);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(209, 71);
            this.btnProfile.TabIndex = 2;
            this.btnProfile.Text = "Hồ sơ cá nhân";
            this.btnProfile.UseVisualStyleBackColor = false;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDangXuat.Font = new System.Drawing.Font("UTM Cookies", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangXuat.ForeColor = System.Drawing.Color.Red;
            this.btnDangXuat.Location = new System.Drawing.Point(386, 467);
            this.btnDangXuat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(145, 40);
            this.btnDangXuat.TabIndex = 4;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = false;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.Font = new System.Drawing.Font("UTM Cookies", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Goldenrod;
            this.button1.Location = new System.Drawing.Point(498, 300);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(209, 71);
            this.button1.TabIndex = 5;
            this.button1.Text = "LỊCH SỬ TRẬN ĐẤU";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // GiaoDienChung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GameCaro.Properties.Resources.GIAODIENCHUNG_01;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(917, 613);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDangXuat);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.btnCaiDat);
            this.Controls.Add(this.btnPhongCho);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GiaoDienChung";
            this.Text = "Game Caro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GiaoDienChung_FormClosing);
            this.Load += new System.EventHandler(this.GiaoDienChung_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPhongCho;
        private System.Windows.Forms.Button btnCaiDat;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button button1;
    }
}