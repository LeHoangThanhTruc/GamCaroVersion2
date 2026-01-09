namespace GameCaro
{
    partial class FormXacThucOTP
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
            this.lblThongTin = new System.Windows.Forms.Label();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnGuiLai = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblThongTin
            // 
            this.lblThongTin.AutoSize = true;
            this.lblThongTin.Location = new System.Drawing.Point(32, 11);
            this.lblThongTin.Name = "lblThongTin";
            this.lblThongTin.Size = new System.Drawing.Size(44, 16);
            this.lblThongTin.TabIndex = 0;
            this.lblThongTin.Text = "label1";
            // 
            // txtOTP
            // 
            this.txtOTP.Location = new System.Drawing.Point(239, 34);
            this.txtOTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOTP.MaxLength = 6;
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(199, 22);
            this.txtOTP.TabIndex = 1;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnXacNhan.ForeColor = System.Drawing.Color.Green;
            this.btnXacNhan.Location = new System.Drawing.Point(112, 111);
            this.btnXacNhan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(120, 34);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnGuiLai
            // 
            this.btnGuiLai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnGuiLai.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnGuiLai.Location = new System.Drawing.Point(319, 111);
            this.btnGuiLai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuiLai.Name = "btnGuiLai";
            this.btnGuiLai.Size = new System.Drawing.Size(120, 34);
            this.btnGuiLai.TabIndex = 3;
            this.btnGuiLai.Text = "Resend (60s)";
            this.btnGuiLai.UseVisualStyleBackColor = false;
            this.btnGuiLai.Click += new System.EventHandler(this.btnGuiLai_Click);
            // 
            // FormXacThucOTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 182);
            this.Controls.Add(this.btnGuiLai);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtOTP);
            this.Controls.Add(this.lblThongTin);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormXacThucOTP";
            this.Text = "FormXacThucOTP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormXacThucOTP_FormClosing);
            this.Load += new System.EventHandler(this.FormXacThucOTP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblThongTin;
        private System.Windows.Forms.TextBox txtOTP;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnGuiLai;
    }
}