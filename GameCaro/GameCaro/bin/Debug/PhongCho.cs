using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class PhongCho : Form
    {
        private string userId;
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
            // Lấy dữ liệu từ TextBox
            /*string yourID = txtYourID.Text.Trim();
            string opponentID = txtIDDoiThu.Text.Trim();

            // Kiểm tra rỗng, với code tìm đối thủ hiện tại thì việc kiểm tra rỗng không cần thiết lắm           /* if (string.IsNullOrEmpty(yourID))
            {
                MessageBox.Show("Your ID không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng, không chuyển form
            }

            if (string.IsNullOrEmpty(opponentID))
            {
                MessageBox.Show("ID Đối Thủ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            */
            if (isWaiting) return;

            isWaiting = true;
            NetworkClient.Instance.Send($"FIND_MATCH|{userId}");

            matchTimer = new System.Windows.Forms.Timer();
            matchTimer.Interval = 60000; // 60s
            matchTimer.Tick += MatchTimeOut;
            matchTimer.Start();

            MessageBox.Show("Đang tìm đối thủ...");
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
                string idDoiThu = msg.Split('|')[1];

                this.Invoke(new Action(() =>
                {
                    matchTimer?.Stop();
                    isWaiting = false;
                    tmTimDoiThu.Stop();
                    prbTimDoiThu.Value = 0;
                    txtIDDoiThu.Text = idDoiThu;
                    MessageBox.Show($"Đã tìm được đối thủ: {idDoiThu}");

                    // Tự mở form chơi
                    Form1 f = new Form1(userId, idDoiThu);
                    f.Show();
                    this.Hide();
                }));
            }
            else if (msg == "WAITING_FOR_OPPONENT")
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Bạn đang được đưa vào hàng đợi, vui lòng chờ...");
                }));
            }
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