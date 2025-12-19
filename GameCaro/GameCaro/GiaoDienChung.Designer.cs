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
            this.SuspendLayout();
            // 
            // btnPhongCho
            // 
            this.btnPhongCho.Font = new System.Drawing.Font("Ravie", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhongCho.ForeColor = System.Drawing.Color.Red;
            this.btnPhongCho.Location = new System.Drawing.Point(243, 120);
            this.btnPhongCho.Name = "btnPhongCho";
            this.btnPhongCho.Size = new System.Drawing.Size(157, 58);
            this.btnPhongCho.TabIndex = 0;
            this.btnPhongCho.Text = "Phòng chờ";
            this.btnPhongCho.UseVisualStyleBackColor = true;
            this.btnPhongCho.Click += new System.EventHandler(this.btnPhongCho_Click);
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.Font = new System.Drawing.Font("Ravie", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaiDat.ForeColor = System.Drawing.Color.Red;
            this.btnCaiDat.Location = new System.Drawing.Point(243, 196);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Size = new System.Drawing.Size(157, 58);
            this.btnCaiDat.TabIndex = 1;
            this.btnCaiDat.Text = "Cài đặt";
            this.btnCaiDat.UseVisualStyleBackColor = true;
            this.btnCaiDat.Click += new System.EventHandler(this.btnCaiDat_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.Font = new System.Drawing.Font("Ravie", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.ForeColor = System.Drawing.Color.Red;
            this.btnProfile.Location = new System.Drawing.Point(243, 269);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(157, 58);
            this.btnProfile.TabIndex = 2;
            this.btnProfile.Text = "Hồ sơ cá nhân";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ravie", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(86, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(494, 86);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game Caro";
            // 
            // GiaoDienChung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 381);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.btnCaiDat);
            this.Controls.Add(this.btnPhongCho);
            this.Name = "GiaoDienChung";
            this.Text = "Game Caro";
            this.Load += new System.EventHandler(this.GiaoDienChung_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPhongCho;
        private System.Windows.Forms.Button btnCaiDat;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Label label1;
    }
}