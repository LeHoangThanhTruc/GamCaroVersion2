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
            this.label3 = new System.Windows.Forms.Label();
            this.prbTimDoiThu = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtYourID
            // 
            this.txtYourID.Location = new System.Drawing.Point(285, 212);
            this.txtYourID.Margin = new System.Windows.Forms.Padding(4);
            this.txtYourID.Name = "txtYourID";
            this.txtYourID.ReadOnly = true;
            this.txtYourID.Size = new System.Drawing.Size(336, 22);
            this.txtYourID.TabIndex = 0;
            // 
            // txtIDDoiThu
            // 
            this.txtIDDoiThu.Location = new System.Drawing.Point(285, 262);
            this.txtIDDoiThu.Margin = new System.Windows.Forms.Padding(4);
            this.txtIDDoiThu.Name = "txtIDDoiThu";
            this.txtIDDoiThu.ReadOnly = true;
            this.txtIDDoiThu.Size = new System.Drawing.Size(336, 22);
            this.txtIDDoiThu.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(213, 216);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Your ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(197, 266);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID đối thủ";
            // 
            // btnTimDoiThu
            // 
            this.btnTimDoiThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnTimDoiThu.Font = new System.Drawing.Font("UTM Cookies", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimDoiThu.ForeColor = System.Drawing.Color.Green;
            this.btnTimDoiThu.Location = new System.Drawing.Point(359, 377);
            this.btnTimDoiThu.Margin = new System.Windows.Forms.Padding(4);
            this.btnTimDoiThu.Name = "btnTimDoiThu";
            this.btnTimDoiThu.Size = new System.Drawing.Size(123, 50);
            this.btnTimDoiThu.TabIndex = 4;
            this.btnTimDoiThu.Text = "Tìm đối thủ";
            this.btnTimDoiThu.UseVisualStyleBackColor = false;
            this.btnTimDoiThu.Click += new System.EventHandler(this.btnTimDoiThu_Click);
            // 
            // tmTimDoiThu
            // 
            this.tmTimDoiThu.Tick += new System.EventHandler(this.tmTimDoiThu_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 332);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Đang tìm đối thủ. . .";
            // 
            // prbTimDoiThu
            // 
            this.prbTimDoiThu.Location = new System.Drawing.Point(189, 308);
            this.prbTimDoiThu.Margin = new System.Windows.Forms.Padding(4);
            this.prbTimDoiThu.Name = "prbTimDoiThu";
            this.prbTimDoiThu.Size = new System.Drawing.Size(451, 51);
            this.prbTimDoiThu.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button1.Font = new System.Drawing.Font("UTM Cookies", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.Location = new System.Drawing.Point(239, 377);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 50);
            this.button1.TabIndex = 10;
            this.button1.Text = "Quay Lại";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PhongCho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GameCaro.Properties.Resources.PHONGCHO_01;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(812, 544);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.prbTimDoiThu);
            this.Controls.Add(this.btnTimDoiThu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIDDoiThu);
            this.Controls.Add(this.txtYourID);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar prbTimDoiThu;
        private System.Windows.Forms.Button button1;
    }
}