using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GameCaro
{
    public partial class Form1 : Form
    {
        #region Properties
        ChessBoardManager ChessBoard;
        SocketManager socket;
        #endregion
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            ChessBoard = new ChessBoardManager(pnlChessBoard, txtPlayerName, picMark);
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;

            prbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prbCoolDown.Value = 0;

            tmCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;
            socket = new SocketManager();

            NewGame();

            //tmCoolDown.Start();


        }

        void EndGame()
        {
            tmCoolDown.Stop();
            pnlChessBoard.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            //MessageBox.Show("Kết thúc game");
        }
        void NewGame()
        {
            prbCoolDown.Value = 0;
            tmCoolDown.Stop();
            ChessBoard.DrawChessBoard();
            undoToolStripMenuItem.Enabled = true;
        }
        void Undo()
        {
            ChessBoard.Undo();
            prbCoolDown.Value = 0;
        }
        void Quit()
        {
            if (MessageBox.Show("Do you really want to Exit ?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            EndGame();
            socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
        }

        private void ChessBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            tmCoolDown.Start();
            pnlChessBoard.Enabled = false;
            prbCoolDown.Value = 0;  // reset thanh thời gian
            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "",e.ClickedPoint));
            undoToolStripMenuItem.Enabled = false;
            Listen();
        }

        private void picAvartar_Click(object sender, EventArgs e)
        {

        }
        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prbCoolDown.PerformStep();
            if (prbCoolDown.Value >= prbCoolDown.Maximum)
            {
                EndGame();
                socket.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
            }
        }

        private void prbCoolDown_Click(object sender, EventArgs e)
        {

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
            socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
            pnlChessBoard.Enabled = true;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socket.Send(new SocketData((int)SocketCommand.UNDO, "", new Point()));
            Undo();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you really want to Exit ?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                try { 
                    socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                }
                catch { }  
            }
        }
        private void btnLAN_Click(object sender, EventArgs e)
        {

            socket.IP = txtIP.Text;
            if (!socket.ConnectServer())
            {
                socket.isServer = true;
                pnlChessBoard.Enabled = true;
                socket.CreateServer();
                
            }
            else
            {
                socket.isServer = false;
                pnlChessBoard.Enabled = false;
                Listen();
            }


        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            /* txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txtIP.Text))
            {
                txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }*/
            // 1. Lấy IP nội bộ tự động (LAN/Wi-Fi/4G USB)
            //txtIP.Text = socket.GetLocalIPv4();

            // 2. Nếu muốn kết nối Internet, có thể lấy Public IP
            //string publicIP = socket.GetPublicIP();
            //txtIPPublic.Text = publicIP;

            // 3. Nếu muốn hiển thị cả 2 IP
            // txtIP.Text = $"Local IP: {socket.GetLocalIPv4()}  |  Public IP: {socket.GetPublicIP()}";
        }


        void Listen()
        {
          
            Thread listenThread = new Thread(() =>
                {
                    try
                    {
                        SocketData data = (SocketData)socket.Receive();
                        HandlingData(data);
                    }
                    catch(Exception e)
                    {
                    }
                });
            listenThread.IsBackground = true;
            listenThread.Start();
        }
            

        
        private void HandlingData(SocketData data)
        {
            switch (data.Command)
            {
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        prbCoolDown.Value = 0;
                        pnlChessBoard.Enabled = true;
                        tmCoolDown.Start();
                        ChessBoard.OtherPlayerMark(data.Point);
                        undoToolStripMenuItem.Enabled = true;
                    }));
                                       
                    break;
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                        pnlChessBoard.Enabled = false;
                    }));
                    
                    break;
                case (int)SocketCommand.UNDO:
                    Undo();
                    prbCoolDown.Value = 0;     
                    break;
                case (int)SocketCommand.END_GAME:
                    MessageBox.Show("Player had five in a line!");
                    break;
                case (int)SocketCommand.TIME_OUT:
                    MessageBox.Show("Timeout!");
                    break;
                case (int)SocketCommand.QUIT:
                    tmCoolDown.Stop();
                    MessageBox.Show("Your opponent has left", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    break;
            }
            Listen();
        }
    }
}
