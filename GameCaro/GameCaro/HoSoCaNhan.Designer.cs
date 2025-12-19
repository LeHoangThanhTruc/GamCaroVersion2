namespace GameCaro
{
    partial class HoSoCaNhan
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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxAvartar = new System.Windows.Forms.PictureBox();
            this.panelThongTinDaDangKy = new System.Windows.Forms.Panel();
            this.panelChienTich = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChooseAvartar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnQuayLaiGiaoDienChung = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvartar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ravie", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(156, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(468, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hồ Sơ Cá Nhân";
            // 
            // pictureBoxAvartar
            // 
            this.pictureBoxAvartar.Location = new System.Drawing.Point(31, 89);
            this.pictureBoxAvartar.Name = "pictureBoxAvartar";
            this.pictureBoxAvartar.Size = new System.Drawing.Size(85, 79);
            this.pictureBoxAvartar.TabIndex = 1;
            this.pictureBoxAvartar.TabStop = false;
            // 
            // panelThongTinDaDangKy
            // 
            this.panelThongTinDaDangKy.Location = new System.Drawing.Point(167, 127);
            this.panelThongTinDaDangKy.Name = "panelThongTinDaDangKy";
            this.panelThongTinDaDangKy.Size = new System.Drawing.Size(298, 311);
            this.panelThongTinDaDangKy.TabIndex = 2;
            // 
            // panelChienTich
            // 
            this.panelChienTich.Location = new System.Drawing.Point(471, 127);
            this.panelChienTich.Name = "panelChienTich";
            this.panelChienTich.Size = new System.Drawing.Size(314, 311);
            this.panelChienTich.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Avartar";
            // 
            // btnChooseAvartar
            // 
            this.btnChooseAvartar.Location = new System.Drawing.Point(31, 174);
            this.btnChooseAvartar.Name = "btnChooseAvartar";
            this.btnChooseAvartar.Size = new System.Drawing.Size(85, 32);
            this.btnChooseAvartar.TabIndex = 5;
            this.btnChooseAvartar.Text = "Choose Image";
            this.btnChooseAvartar.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(164, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Thông Tin Đã Đăng Ký";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(468, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Chiến Tích";
            // 
            // btnQuayLaiGiaoDienChung
            // 
            this.btnQuayLaiGiaoDienChung.Location = new System.Drawing.Point(700, 453);
            this.btnQuayLaiGiaoDienChung.Name = "btnQuayLaiGiaoDienChung";
            this.btnQuayLaiGiaoDienChung.Size = new System.Drawing.Size(85, 32);
            this.btnQuayLaiGiaoDienChung.TabIndex = 8;
            this.btnQuayLaiGiaoDienChung.Text = "Quay Lại";
            this.btnQuayLaiGiaoDienChung.UseVisualStyleBackColor = true;
            this.btnQuayLaiGiaoDienChung.Click += new System.EventHandler(this.btnQuayLaiGiaoDienChung_Click);
            // 
            // HoSoCaNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 491);
            this.Controls.Add(this.btnQuayLaiGiaoDienChung);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnChooseAvartar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelChienTich);
            this.Controls.Add(this.panelThongTinDaDangKy);
            this.Controls.Add(this.pictureBoxAvartar);
            this.Controls.Add(this.label1);
            this.Name = "HoSoCaNhan";
            this.Text = "Game Caro";
            this.Load += new System.EventHandler(this.HoSoCaNhan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvartar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxAvartar;
        private System.Windows.Forms.Panel panelThongTinDaDangKy;
        private System.Windows.Forms.Panel panelChienTich;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChooseAvartar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnQuayLaiGiaoDienChung;
    }
}