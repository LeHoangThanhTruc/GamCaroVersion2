namespace GameCaro
{
    partial class CaiDat
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
            this.checkBoxAmNhac = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQuayLaiGiaoDienChung = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBoxAmNhac
            // 
            this.checkBoxAmNhac.AutoSize = true;
            this.checkBoxAmNhac.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAmNhac.Location = new System.Drawing.Point(31, 110);
            this.checkBoxAmNhac.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxAmNhac.Name = "checkBoxAmNhac";
            this.checkBoxAmNhac.Size = new System.Drawing.Size(143, 36);
            this.checkBoxAmNhac.TabIndex = 0;
            this.checkBoxAmNhac.Text = "Âm nhạc";
            this.checkBoxAmNhac.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 164);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "Thay đổi thông tin người chơi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(108, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 69);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cài Đặt";
            // 
            // btnQuayLaiGiaoDienChung
            // 
            this.btnQuayLaiGiaoDienChung.Location = new System.Drawing.Point(413, 466);
            this.btnQuayLaiGiaoDienChung.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuayLaiGiaoDienChung.Name = "btnQuayLaiGiaoDienChung";
            this.btnQuayLaiGiaoDienChung.Size = new System.Drawing.Size(100, 28);
            this.btnQuayLaiGiaoDienChung.TabIndex = 6;
            this.btnQuayLaiGiaoDienChung.Text = "Quay Lại";
            this.btnQuayLaiGiaoDienChung.UseVisualStyleBackColor = true;
            this.btnQuayLaiGiaoDienChung.Click += new System.EventHandler(this.btnQuayLaiGiaoDienChung_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 221);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 32);
            this.label3.TabIndex = 7;
            this.label3.Text = "Thay đổi mật khẩu";
            // 
            // CaiDat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 498);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnQuayLaiGiaoDienChung);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxAmNhac);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CaiDat";
            this.Text = "Game Caro";
            this.Load += new System.EventHandler(this.CaiDat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAmNhac;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuayLaiGiaoDienChung;
        private System.Windows.Forms.Label label3;
    }
}