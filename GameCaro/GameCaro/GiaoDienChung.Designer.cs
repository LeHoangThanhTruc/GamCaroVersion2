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
            this.label1 = new System.Windows.Forms.Label();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnThongTinNPH = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPhongCho
            // 
            this.btnPhongCho.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhongCho.ForeColor = System.Drawing.Color.Red;
            this.btnPhongCho.Location = new System.Drawing.Point(324, 148);
            this.btnPhongCho.Margin = new System.Windows.Forms.Padding(4);
            this.btnPhongCho.Name = "btnPhongCho";
            this.btnPhongCho.Size = new System.Drawing.Size(209, 71);
            this.btnPhongCho.TabIndex = 0;
            this.btnPhongCho.Text = "Phòng chờ";
            this.btnPhongCho.UseVisualStyleBackColor = true;
            this.btnPhongCho.Click += new System.EventHandler(this.btnPhongCho_Click);
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCaiDat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaiDat.ForeColor = System.Drawing.Color.Red;
            this.btnCaiDat.Location = new System.Drawing.Point(324, 241);
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
            this.btnProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.ForeColor = System.Drawing.Color.Red;
            this.btnProfile.Location = new System.Drawing.Point(324, 331);
            this.btnProfile.Margin = new System.Windows.Forms.Padding(4);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(209, 71);
            this.btnProfile.TabIndex = 2;
            this.btnProfile.Text = "Hồ sơ cá nhân";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(115, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 91);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game Caro";
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Location = new System.Drawing.Point(367, 425);
            this.btnDangXuat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(124, 31);
            this.btnDangXuat.TabIndex = 4;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnThongTinNPH
            // 
            this.btnThongTinNPH.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnThongTinNPH.Location = new System.Drawing.Point(589, 50);
            this.btnThongTinNPH.Margin = new System.Windows.Forms.Padding(2);
            this.btnThongTinNPH.Name = "btnThongTinNPH";
            this.btnThongTinNPH.Size = new System.Drawing.Size(150, 25);
            this.btnThongTinNPH.TabIndex = 5;
            this.btnThongTinNPH.Text = "Thông tin NPH";
            this.btnThongTinNPH.UseVisualStyleBackColor = true;
            this.btnThongTinNPH.Click += new System.EventHandler(this.btnThongTinNPH_Click);
            // 
            // GiaoDienChung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GameCaro.Properties.Resources.GIAODIENCHUNG_01;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(904, 469);
            this.Controls.Add(this.btnThongTinNPH);
            this.Controls.Add(this.btnDangXuat);
            this.Controls.Add(this.label1);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPhongCho;
        private System.Windows.Forms.Button btnCaiDat;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnThongTinNPH;
    }
}