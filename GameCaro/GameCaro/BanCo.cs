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
using System.Media;
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
        double maxTime = 60.0;        // 1 phút 
        double timeRemaining;
        private bool isWaitingServerConfirm = false;
        private SoundPlayer soundPlayer;
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

            InitializeSound();


        }
        public BanCo()
        {
            InitializeComponent();
        }
        private void InitializeSound()
        {
            try
            {
                // CÁCH 1: Nếu file âm thanh nằm trong Resources
                soundPlayer = new SoundPlayer(Properties.Resources.click);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi load âm thanh: " + ex.Message);
            }
        }

        private void PlayClickSound()
        {
            try
            {
                if (soundPlayer != null)
                {
                    soundPlayer.Play(); // Phát không đồng bộ (không chặn UI)
                    // Hoặc dùng soundPlayer.PlaySync(); để phát đồng bộ
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi phát âm thanh: " + ex.Message);
            }
        }

        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
        private void btnThoatTranDau_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn thoát trận?","Xác nhận",MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (rs != DialogResult.OK)
                return;

            string timeEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // GỬI – KHÔNG TỰ Ý THOÁT FORM
            NetworkClient.Instance.Send(
                $"THOAT_TRAN|{roomID}|{userID}|{timeEnd}"
            );

            // CHỈ KHÓA BÀN CỜ
            EnableBoard(false);
            isWaitingServerConfirm = true;
        }
        public void SetRoom(string roomID, bool isFirst)
        {
            this.roomID = roomID;
            this.isFirstPlayer = isFirst;
            this.isMyTurn = isFirst;
        }
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string noiDung = rtbSoanTinNhan.Text.Trim();
            if (string.IsNullOrEmpty(noiDung))
                return;

            string idGui = txtYourID.Text;
            string idNhan = txtOpponentID.Text;

            string packet = $"CHAT|{idGui}|{idNhan}|{noiDung}";
            NetworkClient.Instance.Send(packet);

            rtbSoanTinNhan.Clear();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            NetworkClient.OnMessageReceived -= OnServerMessage;

            // GIẢI PHÓNG tài nguyên âm thanh
            if (soundPlayer != null)
            {
                soundPlayer.Dispose();
                soundPlayer = null;
            }

            base.OnFormClosed(e);
        }

        private void BanCo_Load(object sender, EventArgs e)
        {
            NetworkClient.OnMessageReceived -= OnServerMessage;
            NetworkClient.OnMessageReceived += OnServerMessage;

            InitTurnTimer();
        }
        void InitTurnTimer()
        {
            timeRemaining = maxTime;

            prbTimeRemaining.Minimum = 0;
            prbTimeRemaining.Maximum = (int)maxTime;
            prbTimeRemaining.Value = (int)maxTime;

            timerRemainingTime = new Timer();
            timerRemainingTime.Interval = 50; 
            timerRemainingTime.Tick += timerRemainingTime_Tick;
        }
        void StopTurnTimer()
        {
            timerRemainingTime.Stop();
        }

        void ResetTurnTimer()
        {
            timeRemaining = maxTime;
            prbTimeRemaining.Value = (int)maxTime;
            timerRemainingTime.Start();
        }


        private void BanCo_Shown(object sender, EventArgs e)
        {
            DrawChessBoard();
        }

        private void timerRemainingTime_Tick(object sender, EventArgs e)
        {
            timeRemaining -= 0.05;

            if (timeRemaining < 0)
                timeRemaining = 0;

            prbTimeRemaining.Value = (int)Math.Ceiling(timeRemaining);

            if (timeRemaining <= 0)
            {
                timerRemainingTime.Stop();
                EnableBoard(false);

                // Báo server: user hiện tại bị hết giờ
                NetworkClient.Instance.Send(
                    $"TIME_OUT|{roomID}|{userID}"
                );
            }
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

        private void BanCo_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= OnServerMessage;
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

                PlayClickSound();

                isMyTurn = false;
                EnableBoard(false);
                pnlYourID.BackColor = Color.Transparent;
                pnlOpponentID.BackColor = Color.LightGreen;
                StopTurnTimer();
                UpdatePicMark();
            }
            else if (parts[0] == "OPPONENT_MOVE")
            {
                int row = int.Parse(parts[2]);
                int col = int.Parse(parts[3]);

                DrawMove(row, col, !isFirstPlayer);

                PlayClickSound();

                isMyTurn = true;
                EnableBoard(true);
                pnlYourID.BackColor = Color.LightGreen;
                pnlOpponentID.BackColor = Color.Transparent;
                ResetTurnTimer();
                UpdatePicMark();
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
                    ResetTurnTimer();
                }
                else
                {
                    isFirstPlayer = false;
                    isMyTurn = false;
                    EnableBoard(false);
                    pnlYourID.BackColor = Color.Transparent;
                    pnlOpponentID.BackColor = Color.LightGreen;
                    StopTurnTimer();
                }
                UpdatePicMark();
            }
            if (msg.StartsWith("GAME_OVER|"))
            {
                string[] p = msg.Split('|');
                string winnerId = p[1];
                string loserId = p[2];

                this.Invoke(new Action(() =>
                {
                    StopTurnTimer();
                    EnableBoard(false);

                    if (userID == winnerId)
                        MessageBox.Show("Bạn đã THẮNG!");
                    else
                        MessageBox.Show("Bạn đã THUA!");
                }));
            }
            else if (parts[0] == "TIMEOUT_ABORT")
            {
                string timeoutUser = parts[1];

                StopTurnTimer();
                EnableBoard(false);

                if (timeoutUser == userID)
                {
                    MessageBox.Show("Bạn đã hết thời gian cho lượt đi.\nVán đấu bị dừng." );
                }
                else
                {
                    MessageBox.Show($"Đối thủ ({timeoutUser}) đã hết thời gian.\nVán đấu bị dừng.");
                }
            }
            else if (msg.StartsWith("CHAT|"))
            {
                // CHAT|idNguoiGui|noiDung
                string[] p = msg.Split(new char[] { '|' }, 3);

                string idNguoiGui = p[1];
                string noiDung = p[2];

                if (idNguoiGui == txtYourID.Text)
                {
                    rtbKhungChatHienThi.AppendText($"me: {noiDung}\n");
                }
                else
                {
                    rtbKhungChatHienThi.AppendText($"{idNguoiGui}: {noiDung}\n");
                }
            }
            else if (msg.StartsWith("OPPONENT_LEFT|"))
            {
                string userThoat = msg.Split('|')[1];

                MessageBox.Show(
                    $"Người chơi {userThoat} đã thoát trận.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ThoatVeGiaoDienChung();
            }

            else if (msg == "LEFT_MATCH_OK")
            {
                if (isWaitingServerConfirm)
                {
                    isWaitingServerConfirm = false;
                    ThoatVeGiaoDienChung();
                }

            }
            else if (msg.StartsWith("CREATE_CARO_ROOM_FAIL"))
            {
                MessageBox.Show(
                    "Không thể bắt đầu ván đấu.\nVui lòng quay lại giao diện chung",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                ThoatVeGiaoDienChung();
            }


        }
        private void ThoatVeGiaoDienChung()
        {
            NetworkClient.OnMessageReceived -= OnServerMessage;

            this.Hide();

            GiaoDienChung main = new GiaoDienChung(userID);
            main.Show();

            this.Close();
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
        void UpdatePicMark()
        {
            if (isMyTurn)
            {
                // lượt của mình -> hiện ký hiệu của mình
                picMark.Image = isFirstPlayer ? imgX : imgO;
            }
            else
            {
                // lượt đối thủ -> hiện ký hiệu của đối thủ
                picMark.Image = isFirstPlayer ? imgO : imgX;
            }

            picMark.SizeMode = PictureBoxSizeMode.StretchImage;
        }


    }
}
