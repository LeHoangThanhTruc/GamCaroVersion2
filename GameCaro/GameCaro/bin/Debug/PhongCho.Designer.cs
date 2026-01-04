namespace GameCaro
{
    partial class PhongCho
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
            this.components = new System.ComponentModel.Container();
            this.txtYourID = new System.Windows.Forms.TextBox();
            this.txtIDDoiThu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTimDoiThu = new System.Windows.Forms.Button();
            this.tmTimDoiThu = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.prbTimDoiThu = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // txtYourID
            // 
            this.txtYourID.Location = new System.Drawing.Point(296, 98);
            this.txtYourID.Name = "txtYourID";
            this.txtYourID.ReadOnly = true;
            this.txtYourID.Size = new System.Drawing.Size(272, 20);
            this.txtYourID.TabIndex = 0;
            // 
            // txtIDDoiThu
            // 
            this.txtIDDoiThu.Location = new System.Drawing.Point(296, 154);
            this.txtIDDoiThu.Name = "txtIDDoiThu";
            this.txtIDDoiThu.ReadOnly = true;
            this.txtIDDoiThu.Size = new System.Drawing.Size(272, 20);
            this.txtIDDoiThu.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Your ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID đối thủ";
            // 
            // btnTimDoiThu
            // 
            this.btnTimDoiThu.Location = new System.Drawing.Point(315, 255);
            this.btnTimDoiThu.Name = "btnTimDoiThu";
            this.btnTimDoiThu.Size = new System.Drawing.Size(94, 46);
            this.btnTimDoiThu.TabIndex = 4;
            this.btnTimDoiThu.Text = "Tìm đối thủ";
            this.btnTimDoiThu.UseVisualStyleBackColor = true;
            this.btnTimDoiThu.Click += new System.EventHandler(this.btnTimDoiThu_Click);
            // 
            // tmTimDoiThu
            // 
            this.tmTimDoiThu.Tick += new System.EventHandler(this.tmTimDoiThu_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ravie", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(158, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(502, 86);
            this.label4.TabIndex = 7;
            this.label4.Text = "PHÒNG CHỜ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Đang tìm đối thủ. . .";
            // 
            // prbTimDoiThu
            // 
            this.prbTimDoiThu.Location = new System.Drawing.Point(192, 197);
            this.prbTimDoiThu.Name = "prbTimDoiThu";
            this.prbTimDoiThu.Size = new System.Drawing.Size(376, 40);
            this.prbTimDoiThu.TabIndex = 5;
            // 
            // PhongCho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 341);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.prbTimDoiThu);
            this.Controls.Add(this.btnTimDoiThu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIDDoiThu);
            this.Controls.Add(this.txtYourID);
            this.Name = "PhongCho";
            this.Text = "Phòng Chờ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PhongCho_FormClosing);
            this.Load += new System.EventHandler(this.PhongCho_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtYourID;
        private System.Windows.Forms.TextBox txtIDDoiThu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTimDoiThu;
        private System.Windows.Forms.Timer tmTimDoiThu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar prbTimDoiThu;
    }
}