using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Kích cỡ button 33,33 ; Location ban đầu 3, 3
//Kích cỡ bàn cờ 948, 708
namespace GameCaro
{
    public partial class BanCo : Form
    {
        public string RoomID { get; set; }
        int chieuRongBanCo,chieuCaoBanCo,chieuRongQuanCo,chieuCaoQuanCo;
        Image imgX;
        Image imgO;
        string userID, IDdoithu,roomID;
        bool isMyTurn;          // có được đánh không
        bool isFirstPlayer;    // X hay O

        public BanCo(string userID,string IDdoithu)
        {
            InitializeComponent();
            this.Load += BanCo_Load;
            this.userID = userID;
            this.IDdoithu = IDdoithu;
            chieuRongBanCo = pnlBanCo.Width;
            chieuCaoBanCo = pnlBanCo.Height;
            Button QuanCo = new Button();
            QuanCo.Width = 33;
            QuanCo.Height = 33;
            chieuRongQuanCo = QuanCo.Width;
            chieuCaoQuanCo = QuanCo.Height;
            imgX = Properties.Resources.X_Black;
            imgO = Properties.Resources.O_Black;
            txtOpponentID.Text= IDdoithu;
            txtYourID.Text= userID;
            //MessageBox.Show("Test 2");
            this.Shown += BanCo_Shown;
            

        }
        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
        public void SetRoom(string roomID, bool isFirst)
        {
            this.roomID = roomID;
            this.isFirstPlayer = isFirst;
            this.isMyTurn = isFirst;
        }
        private void btnSendMessage_Click(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            NetworkClient.OnMessageReceived -= OnServerMessage;
            base.OnFormClosed(e);
        }

        private void BanCo_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= OnServerMessage;
            NetworkClient.OnMessageReceived += OnServerMessage;
        }

       

        private void BanCo_Shown(object sender, EventArgs e)
        {
            DrawChessBoard();
        }
        public void DrawChessBoard()
        {
            pnlBanCo.SuspendLayout();

            pnlBanCo.Controls.Clear();
            Matrix = new List<List<Button>>();

            int size = 33;

            int soDong = pnlBanCo.Height / size;
            int soCot = pnlBanCo.Width / size;

            for (int i = 0; i < soDong; i++)
            {
                Matrix.Add(new List<Button>());

                for (int j = 0; j < soCot; j++)
                {
                    Button btn = new Button
                    {
                        Width = size,
                        Height = size,
                        Location = new Point(j * size, i * size),
                        Tag = new Point(i, j)
                    };

                    btn.Click += Btn_Click;

                    pnlBanCo.Controls.Add(btn);
                    Matrix[i].Add(btn);
                }
            }

            pnlBanCo.ResumeLayout();
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            if (!isMyTurn) return;

            Button btn = sender as Button;
            if (btn.BackgroundImage != null) return; // đã đánh rồi

            Point pos = (Point)btn.Tag;
            int row = pos.X;
            int col = pos.Y;

            // Gửi nước đi cho server
            NetworkClient.Instance.Send(
                $"MOVE|{roomID}|{userID}|{row}|{col}"
            );

            // TẠM THỜI khóa bàn – chờ server xác nhận
            //EnableBoard(false);
        }
        
        private void OnServerMessage(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnServerMessage(msg)));
                return;
            }

            string[] parts = msg.Split('|');

            if (parts[0] == "MOVE_OK")
            {
                int row = int.Parse(parts[2]);
                int col = int.Parse(parts[3]);

                DrawMove(row, col, isFirstPlayer);
                isMyTurn = false;
                EnableBoard(false);
                pnlYourID.BackColor = Color.Transparent;
                pnlOpponentID.BackColor = Color.LightGreen;

            }
            else if (parts[0] == "OPPONENT_MOVE")
            {
                int row = int.Parse(parts[2]);
                int col = int.Parse(parts[3]);

                DrawMove(row, col, !isFirstPlayer);
                isMyTurn = true;
                EnableBoard(true);
                pnlYourID.BackColor = Color.LightGreen;
                pnlOpponentID.BackColor = Color.Transparent;

            }
            else if (parts[0] == "START_GAME")
            {
                roomID = parts[1];
                string role = parts[2];

                if (role == "FIRST")
                {
                    isFirstPlayer = true;
                    isMyTurn = true;
                    EnableBoard(true);
                    pnlYourID.BackColor = Color.LightGreen;
                    pnlOpponentID.BackColor = Color.Transparent;
                }
                else
                {
                    isFirstPlayer = false;
                    isMyTurn = false;
                    EnableBoard(false);
                    pnlYourID.BackColor = Color.Transparent;
                    pnlOpponentID.BackColor = Color.LightGreen;
                }
            }

        }
        private void DrawMove(int row, int col, bool isX)
        {
            Button btn = Matrix[row][col];
            btn.BackgroundImage = isX ? imgX : imgO;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void EnableBoard(bool enable)
        {
            foreach (var row in Matrix)
                foreach (var btn in row)
                    btn.Enabled = enable;
        }


    }
}
