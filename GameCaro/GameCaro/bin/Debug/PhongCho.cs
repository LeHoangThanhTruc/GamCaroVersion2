using GameCaro;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class PhongCho : Form
    {
        private string userId, roomID, IDOpponent;
        bool roomCreating = false;
        BanCo banCoForm = null;
        private bool isInGame = false;
        public PhongCho(string id)
        {
            InitializeComponent();
            userId = id;
            this.tmTimDoiThu = new System.Windows.Forms.Timer(this.components);
            this.tmTimDoiThu.Enabled = false;
            this.tmTimDoiThu.Interval = 1000;
            this.tmTimDoiThu.Tick += new System.EventHandler(this.tmTimDoiThu_Tick);

        }



        private System.Windows.Forms.Timer matchTimer;
        private bool isWaiting = false;
        private void btnTimDoiThu_Click(object sender, EventArgs e)
        {
            if (isWaiting) return;

            isWaiting = true;
            NetworkClient.Instance.Send($"FIND_MATCH|{userId}");

            matchTimer = new System.Windows.Forms.Timer();
            matchTimer.Interval = 60000; // 60s
            matchTimer.Tick += MatchTimeOut;
            matchTimer.Start();

            //MessageBox.Show("Đang tìm đối thủ...");
            prbTimDoiThu.Value = 0;
            prbTimDoiThu.Maximum = 60;

            tmTimDoiThu.Interval = 1000; // mỗi giây tăng 1
            tmTimDoiThu.Start();
        }
        private void MatchTimeOut(object sender, EventArgs e)
        {
            matchTimer.Stop();
            isWaiting = false;
            NetworkClient.Instance.Send($"CANCEL_FIND_MATCH|{userId}");
            tmTimDoiThu.Stop();
            prbTimDoiThu.Value = 0;
            MessageBox.Show("Không tìm thấy đối thủ trong 1 phút.");
        }
        private void PhongCho_Load(object sender, EventArgs e)
        {
            txtYourID.Text = userId;
            NetworkClient.OnMessageReceived += ClientXuLyPhongCho;
        }
        private void ClientXuLyPhongCho(string msg) 
        {
            if (msg.StartsWith("FOUND_MATCH|"))
            {
                if (isInGame) return;
                if (roomCreating) return;
                roomCreating = true;
                
                string idDoiThu = msg.Split('|')[1];

                this.Invoke(new Action(() =>
                {
                    matchTimer?.Stop();
                    isWaiting = false;
                    tmTimDoiThu.Stop();
                    prbTimDoiThu.Value = 0;
                    txtIDDoiThu.Text = idDoiThu;
                    IDOpponent=idDoiThu;
                    //MessageBox.Show($"Đã tìm được đối thủ: {idDoiThu}");

                    

                    // CHỈ 1 CLIENT ĐƯỢC TẠO PHÒNG
                    if (string.Compare(userId, idDoiThu) < 0)
                    {
                        NetworkClient.Instance.Send(
                            $"CREATE_CARO_ROOM|{userId}|{idDoiThu}"
                        );
                    }
                }));
            }
            else if (msg == "WAITING_FOR_OPPONENT")
            {
                this.Invoke(new Action(() =>
                {
                    //MessageBox.Show("Bạn đang được đưa vào hàng đợi, vui lòng chờ...");
                }));
            }
            else if (msg.StartsWith("CREATE_CARO_ROOM_SUCCESS|"))
            {
                if (isInGame) return;
                isInGame = true;
                roomCreating = false;
                string[] parts = msg.Split('|');
                string roomId = parts[1];
                string opponentId = parts[2];
                bool isFirstPlayer = string.Compare(userId, opponentId) < 0;
                this.Invoke(new Action(() =>
                {
                    IDOpponent = opponentId;

                    banCoForm = new BanCo(userId, IDOpponent);
                    banCoForm.SetRoom(roomId, isFirstPlayer); // role set ở START_GAME
                    banCoForm.Show();
                    this.Hide();
                }));

            }
            else if (msg.StartsWith("CREATE_CARO_ROOM_FAIL|"))
            {
                string reason = msg.Split('|')[1];
                MessageBox.Show("Tạo phòng thất bại: " + reason);
            }
            else if (msg.StartsWith("CREATE_CARO_ROOM_FAIL|"))
            {
                string reason = msg.Split('|')[1];

                if (reason == "ALREADY_IN_GAME")
                {
                    MessageBox.Show(
                        "Bạn đang ở trong một trận khác.\nKhông thể tạo phòng mới.",
                        "Không thể tạo phòng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    // đảm bảo UI trở về trạng thái an toàn
                    btnTimDoiThu.Enabled = true;
                }
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GiaoDienChung gdChung = new GiaoDienChung(userId);


            gdChung.Show();

            this.Close();
        }

        private void PhongCho_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.OnMessageReceived -= ClientXuLyPhongCho;
        }

        private void tmTimDoiThu_Tick(object sender, EventArgs e)
        {
            if (prbTimDoiThu.Value < prbTimDoiThu.Maximum)
                prbTimDoiThu.Value++;
        }
    }
}
/*
 * Mình muốn có danh sách những người đang tìm đối thủ, trong form PhongCho khi bấm btnTimDoiThu thì nó sẽ bắt đầu chọn ngẫu nhiên đối thủ đã bấm 
 * btnTimDoiThu nhưng chưa có người ghép cặp, khi đã chọn xong thì IDUser của đối thủ sẽ được 
 * điền vào txtIDDoiThu*/
