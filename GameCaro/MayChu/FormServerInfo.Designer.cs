namespace MayChu
{
    partial class FormServerInfo
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblIPLabel = new System.Windows.Forms.Label();
            this.lblPortLabel = new System.Windows.Forms.Label();
            this.lblPortInfo = new System.Windows.Forms.Label();
            this.txtFullAddress = new System.Windows.Forms.TextBox();
            this.btnCopyIP = new System.Windows.Forms.Button();
            this.btnCopyFull = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnShowAllIPs = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(482, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(80, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(105, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thông tin Server";
            // 
            // lblIPLabel
            // 
            this.lblIPLabel.AutoSize = true;
            this.lblIPLabel.Location = new System.Drawing.Point(30, 100);
            this.lblIPLabel.Name = "lblIPLabel";
            this.lblIPLabel.Size = new System.Drawing.Size(65, 16);
            this.lblIPLabel.TabIndex = 1;
            this.lblIPLabel.Text = "Địa chỉ IP:";
            // 
            // lblPortLabel
            // 
            this.lblPortLabel.AutoSize = true;
            this.lblPortLabel.Location = new System.Drawing.Point(30, 140);
            this.lblPortLabel.Name = "lblPortLabel";
            this.lblPortLabel.Size = new System.Drawing.Size(34, 16);
            this.lblPortLabel.TabIndex = 3;
            this.lblPortLabel.Text = "Port:";
            // 
            // lblPortInfo
            // 
            this.lblPortInfo.AutoSize = true;
            this.lblPortInfo.Location = new System.Drawing.Point(150, 135);
            this.lblPortInfo.Name = "lblPortInfo";
            this.lblPortInfo.Size = new System.Drawing.Size(38, 16);
            this.lblPortInfo.TabIndex = 4;
            this.lblPortInfo.Text = "9998:";
            // 
            // txtFullAddress
            // 
            this.txtFullAddress.Location = new System.Drawing.Point(30, 180);
            this.txtFullAddress.Name = "txtFullAddress";
            this.txtFullAddress.Size = new System.Drawing.Size(440, 22);
            this.txtFullAddress.TabIndex = 5;
            this.txtFullAddress.Text = "192.168.1.1:9998";
            this.txtFullAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCopyIP
            // 
            this.btnCopyIP.Location = new System.Drawing.Point(30, 225);
            this.btnCopyIP.Name = "btnCopyIP";
            this.btnCopyIP.Size = new System.Drawing.Size(105, 40);
            this.btnCopyIP.TabIndex = 6;
            this.btnCopyIP.Text = "Copy IP";
            this.btnCopyIP.UseVisualStyleBackColor = true;
            this.btnCopyIP.Click += new System.EventHandler(this.btnCopyIP_Click);
            // 
            // btnCopyFull
            // 
            this.btnCopyFull.Location = new System.Drawing.Point(145, 225);
            this.btnCopyFull.Name = "btnCopyFull";
            this.btnCopyFull.Size = new System.Drawing.Size(105, 40);
            this.btnCopyFull.TabIndex = 7;
            this.btnCopyFull.Text = "Copy All";
            this.btnCopyFull.UseVisualStyleBackColor = true;
            this.btnCopyFull.Click += new System.EventHandler(this.btnCopyFull_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(260, 225);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 40);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnShowAllIPs
            // 
            this.btnShowAllIPs.Location = new System.Drawing.Point(375, 225);
            this.btnShowAllIPs.Name = "btnShowAllIPs";
            this.btnShowAllIPs.Size = new System.Drawing.Size(95, 40);
            this.btnShowAllIPs.TabIndex = 9;
            this.btnShowAllIPs.Text = "All IPs";
            this.btnShowAllIPs.UseVisualStyleBackColor = true;
            this.btnShowAllIPs.Click += new System.EventHandler(this.btnShowAllIPs_Click);
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(30, 285);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(82, 16);
            this.lblInstructions.TabIndex = 10;
            this.lblInstructions.Text = "Hướng dẫn...";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Location = new System.Drawing.Point(150, 95);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(72, 16);
            this.lblServerIP.TabIndex = 2;
            this.lblServerIP.Text = "192.168.1.1";
            // 
            // FormServerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 463);
            this.Controls.Add(this.lblServerIP);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnShowAllIPs);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnCopyFull);
            this.Controls.Add(this.btnCopyIP);
            this.Controls.Add(this.txtFullAddress);
            this.Controls.Add(this.lblPortInfo);
            this.Controls.Add(this.lblPortLabel);
            this.Controls.Add(this.lblIPLabel);
            this.Controls.Add(this.panelHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormServerInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin Server - Game Caro";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormServerInfo_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblIPLabel;
        private System.Windows.Forms.Label lblPortLabel;
        private System.Windows.Forms.Label lblPortInfo;
        private System.Windows.Forms.TextBox txtFullAddress;
        private System.Windows.Forms.Button btnCopyIP;
        private System.Windows.Forms.Button btnCopyFull;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnShowAllIPs;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblServerIP;
    }
}