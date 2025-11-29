namespace GameCaroClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pnlChessBoard = new Panel();
            panel3 = new Panel();
            label4 = new Label();
            prbCoolDown = new ProgressBar();
            picMark = new PictureBox();
            label3 = new Label();
            btnConnect = new Button();
            txtIP = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txtPlayerName = new TextBox();
            menuStrip1 = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            quitToolStripMenuItem = new ToolStripMenuItem();
            tmCoolDown = new System.Windows.Forms.Timer(components);
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMark).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlChessBoard
            // 
            pnlChessBoard.Location = new Point(12, 46);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(651, 627);
            pnlChessBoard.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(label4);
            panel3.Controls.Add(prbCoolDown);
            panel3.Controls.Add(picMark);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(btnConnect);
            panel3.Controls.Add(txtIP);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(txtPlayerName);
            panel3.Location = new Point(682, 46);
            panel3.Name = "panel3";
            panel3.Size = new Size(365, 627);
            panel3.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 466);
            label4.Name = "label4";
            label4.Size = new Size(120, 20);
            label4.TabIndex = 8;
            label4.Text = "Time remaining :";
            // 
            // prbCoolDown
            // 
            prbCoolDown.Location = new Point(19, 504);
            prbCoolDown.Name = "prbCoolDown";
            prbCoolDown.Size = new Size(314, 23);
            prbCoolDown.TabIndex = 7;
            // 
            // picMark
            // 
            picMark.Image = Properties.Resources.DauXXoaNen;
            picMark.Location = new Point(89, 28);
            picMark.Name = "picMark";
            picMark.Size = new Size(175, 117);
            picMark.SizeMode = PictureBoxSizeMode.StretchImage;
            picMark.TabIndex = 6;
            picMark.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 409);
            label3.Name = "label3";
            label3.Size = new Size(162, 20);
            label3.TabIndex = 5;
            label3.Text = "Rule : 5 in a line to win!";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(71, 309);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(205, 46);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(19, 255);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(317, 27);
            txtIP.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 215);
            label2.Name = "label2";
            label2.Size = new Size(144, 20);
            label2.TabIndex = 2;
            label2.Text = "IP address of server :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 148);
            label1.Name = "label1";
            label1.Size = new Size(153, 20);
            label1.TabIndex = 1;
            label1.Text = "This is turn of player : ";
            // 
            // txtPlayerName
            // 
            txtPlayerName.Location = new Point(19, 172);
            txtPlayerName.Name = "txtPlayerName";
            txtPlayerName.Size = new Size(317, 27);
            txtPlayerName.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1068, 28);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, undoToolStripMenuItem, quitToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(58, 24);
            menuToolStripMenuItem.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            newGameToolStripMenuItem.Size = new Size(151, 24);
            newGameToolStripMenuItem.Text = "New Game";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(151, 24);
            undoToolStripMenuItem.Text = "Undo";
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(151, 24);
            quitToolStripMenuItem.Text = "Quit";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1068, 700);
            Controls.Add(panel3);
            Controls.Add(pnlChessBoard);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Game Caro";
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picMark).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlChessBoard;
        private Panel panel3;
        private TextBox txtPlayerName;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private PictureBox picMark;
        private Label label3;
        private Button btnConnect;
        private TextBox txtIP;
        private Label label2;
        private Label label1;
        private Label label4;
        private ProgressBar prbCoolDown;
        private System.Windows.Forms.Timer tmCoolDown;
    }
}
