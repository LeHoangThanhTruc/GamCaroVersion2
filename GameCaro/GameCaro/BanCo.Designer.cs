namespace GameCaro
{
    partial class BanCo
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
            this.pnlBanCo = new System.Windows.Forms.Panel();
            this.panelNoiNhanTin = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtbSoanTinNhan = new System.Windows.Forms.RichTextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.panelKhungChat = new System.Windows.Forms.Panel();
            this.rtbKhungChatHienThi = new System.Windows.Forms.RichTextBox();
            this.panelLuatChoi = new System.Windows.Forms.Panel();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnThoatTranDau = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.prbTimeRemaining = new System.Windows.Forms.ProgressBar();
            this.pnlOpponentID = new System.Windows.Forms.Panel();
            this.txtOpponentID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlYourID = new System.Windows.Forms.Panel();
            this.txtYourID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picMark = new System.Windows.Forms.PictureBox();
            this.timerRemainingTime = new System.Windows.Forms.Timer(this.components);
            this.panelNoiNhanTin.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelKhungChat.SuspendLayout();
            this.panelLuatChoi.SuspendLayout();
            this.pnlOpponentID.SuspendLayout();
            this.pnlYourID.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMark)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBanCo
            // 
            this.pnlBanCo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlBanCo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBanCo.Location = new System.Drawing.Point(0, 0);
            this.pnlBanCo.Name = "pnlBanCo";
            this.pnlBanCo.Size = new System.Drawing.Size(948, 708);
            this.pnlBanCo.TabIndex = 0;
            // 
            // panelNoiNhanTin
            // 
            this.panelNoiNhanTin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelNoiNhanTin.Controls.Add(this.panel1);
            this.panelNoiNhanTin.Controls.Add(this.panelKhungChat);
            this.panelNoiNhanTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNoiNhanTin.Location = new System.Drawing.Point(948, 0);
            this.panelNoiNhanTin.Name = "panelNoiNhanTin";
            this.panelNoiNhanTin.Size = new System.Drawing.Size(397, 335);
            this.panelNoiNhanTin.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 283);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 52);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtbSoanTinNhan);
            this.panel2.Controls.Add(this.btnSendMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(397, 31);
            this.panel2.TabIndex = 3;
            // 
            // rtbSoanTinNhan
            // 
            this.rtbSoanTinNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSoanTinNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSoanTinNhan.Location = new System.Drawing.Point(77, 0);
            this.rtbSoanTinNhan.Name = "rtbSoanTinNhan";
            this.rtbSoanTinNhan.Size = new System.Drawing.Size(320, 31);
            this.rtbSoanTinNhan.TabIndex = 1;
            this.rtbSoanTinNhan.Text = "";
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSendMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMessage.Location = new System.Drawing.Point(0, 0);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(77, 31);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // panelKhungChat
            // 
            this.panelKhungChat.Controls.Add(this.rtbKhungChatHienThi);
            this.panelKhungChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKhungChat.Location = new System.Drawing.Point(0, 0);
            this.panelKhungChat.Name = "panelKhungChat";
            this.panelKhungChat.Size = new System.Drawing.Size(397, 335);
            this.panelKhungChat.TabIndex = 0;
            // 
            // rtbKhungChatHienThi
            // 
            this.rtbKhungChatHienThi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbKhungChatHienThi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbKhungChatHienThi.Location = new System.Drawing.Point(0, 0);
            this.rtbKhungChatHienThi.Name = "rtbKhungChatHienThi";
            this.rtbKhungChatHienThi.ReadOnly = true;
            this.rtbKhungChatHienThi.Size = new System.Drawing.Size(397, 335);
            this.rtbKhungChatHienThi.TabIndex = 0;
            this.rtbKhungChatHienThi.Text = "";
            // 
            // panelLuatChoi
            // 
            this.panelLuatChoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panelLuatChoi.Controls.Add(this.btnNewGame);
            this.panelLuatChoi.Controls.Add(this.btnThoatTranDau);
            this.panelLuatChoi.Controls.Add(this.label8);
            this.panelLuatChoi.Controls.Add(this.label6);
            this.panelLuatChoi.Controls.Add(this.label4);
            this.panelLuatChoi.Controls.Add(this.label3);
            this.panelLuatChoi.Controls.Add(this.prbTimeRemaining);
            this.panelLuatChoi.Controls.Add(this.pnlOpponentID);
            this.panelLuatChoi.Controls.Add(this.pnlYourID);
            this.panelLuatChoi.Controls.Add(this.picMark);
            this.panelLuatChoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLuatChoi.Location = new System.Drawing.Point(948, 335);
            this.panelLuatChoi.Name = "panelLuatChoi";
            this.panelLuatChoi.Size = new System.Drawing.Size(397, 373);
            this.panelLuatChoi.TabIndex = 2;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(186, 339);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(101, 31);
            this.btnNewGame.TabIndex = 11;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            // 
            // btnThoatTranDau
            // 
            this.btnThoatTranDau.Location = new System.Drawing.Point(293, 339);
            this.btnThoatTranDau.Name = "btnThoatTranDau";
            this.btnThoatTranDau.Size = new System.Drawing.Size(101, 31);
            this.btnThoatTranDau.TabIndex = 10;
            this.btnThoatTranDau.Text = "Leave Match";
            this.btnThoatTranDau.UseVisualStyleBackColor = true;
            this.btnThoatTranDau.Click += new System.EventHandler(this.btnThoatTranDau_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(56, 298);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "Moving";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 303);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(12, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 29);
            this.label4.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Time remaining (1 minute):";
            // 
            // prbTimeRemaining
            // 
            this.prbTimeRemaining.Location = new System.Drawing.Point(15, 261);
            this.prbTimeRemaining.Name = "prbTimeRemaining";
            this.prbTimeRemaining.Size = new System.Drawing.Size(379, 23);
            this.prbTimeRemaining.TabIndex = 3;
            // 
            // pnlOpponentID
            // 
            this.pnlOpponentID.Controls.Add(this.txtOpponentID);
            this.pnlOpponentID.Controls.Add(this.label2);
            this.pnlOpponentID.Location = new System.Drawing.Point(59, 140);
            this.pnlOpponentID.Name = "pnlOpponentID";
            this.pnlOpponentID.Size = new System.Drawing.Size(264, 41);
            this.pnlOpponentID.TabIndex = 2;
            // 
            // txtOpponentID
            // 
            this.txtOpponentID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpponentID.Location = new System.Drawing.Point(123, 6);
            this.txtOpponentID.Name = "txtOpponentID";
            this.txtOpponentID.ReadOnly = true;
            this.txtOpponentID.Size = new System.Drawing.Size(138, 26);
            this.txtOpponentID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Opponent\'s ID:";
            // 
            // pnlYourID
            // 
            this.pnlYourID.Controls.Add(this.txtYourID);
            this.pnlYourID.Controls.Add(this.label1);
            this.pnlYourID.Location = new System.Drawing.Point(59, 187);
            this.pnlYourID.Name = "pnlYourID";
            this.pnlYourID.Size = new System.Drawing.Size(264, 41);
            this.pnlYourID.TabIndex = 1;
            // 
            // txtYourID
            // 
            this.txtYourID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYourID.Location = new System.Drawing.Point(123, 6);
            this.txtYourID.Name = "txtYourID";
            this.txtYourID.ReadOnly = true;
            this.txtYourID.Size = new System.Drawing.Size(138, 26);
            this.txtYourID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your ID:";
            // 
            // picMark
            // 
            this.picMark.Location = new System.Drawing.Point(115, 6);
            this.picMark.Name = "picMark";
            this.picMark.Size = new System.Drawing.Size(136, 128);
            this.picMark.TabIndex = 0;
            this.picMark.TabStop = false;
            // 
            // timerRemainingTime
            // 
            this.timerRemainingTime.Tick += new System.EventHandler(this.timerRemainingTime_Tick);
            // 
            // BanCo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 708);
            this.Controls.Add(this.panelLuatChoi);
            this.Controls.Add(this.panelNoiNhanTin);
            this.Controls.Add(this.pnlBanCo);
            this.Name = "BanCo";
            this.Text = "BanCo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BanCo_FormClosing);
            this.Load += new System.EventHandler(this.BanCo_Load);
            this.Shown += new System.EventHandler(this.BanCo_Shown);
            this.panelNoiNhanTin.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelKhungChat.ResumeLayout(false);
            this.panelLuatChoi.ResumeLayout(false);
            this.panelLuatChoi.PerformLayout();
            this.pnlOpponentID.ResumeLayout(false);
            this.pnlOpponentID.PerformLayout();
            this.pnlYourID.ResumeLayout(false);
            this.pnlYourID.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBanCo;
        private System.Windows.Forms.Panel panelNoiNhanTin;
        private System.Windows.Forms.Panel panelLuatChoi;
        private System.Windows.Forms.Panel panelKhungChat;
        private System.Windows.Forms.RichTextBox rtbSoanTinNhan;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtbKhungChatHienThi;
        private System.Windows.Forms.Panel pnlYourID;
        private System.Windows.Forms.Panel pnlOpponentID;
        private System.Windows.Forms.TextBox txtOpponentID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYourID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picMark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar prbTimeRemaining;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnThoatTranDau;
        private System.Windows.Forms.Timer timerRemainingTime;
        private System.Windows.Forms.Button btnNewGame;
    }
}